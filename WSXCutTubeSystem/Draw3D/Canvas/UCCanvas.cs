using System;
using System.Drawing;
using System.Windows.Forms;
using SharpGL;
using WSX.Draw3D.DrawTools;
using WSX.CommomModel.Enums;
using WSX.CommomModel.DrawModel;
using WSX.Draw3D.MathTools;
using WSX.Draw3D.Utils;
using System.Collections.Generic;
using WSX.Draw3D.Common;
using WSX.GlobalData.Model;
using WSX.CommomModel.ParaModel;
using WSX.Draw3D.Canvas;
using WSX.Draw3D.Resources;
using WSX.Draw3D.Utils.Undo;
using WSX.GlobalData.Messenger;

namespace WSX.Draw3D
{
    public partial class UCCanvas : UserControl
    {
        private int x, y, z = 2,upy=1,upz;
        private float rotationX = 0.0f;
        private float rotationY = 0.0f;
        private float offsetX = 0.0f;
        private float offsetY = 0.0f;
        private float zoom = 1.0f;
        private ViewModel view;
        private OperationMode operationMode;
        private CommandType commandType = CommandType.None;
        private CursorCollection cursorCollection = new CursorCollection();
        private IModel dataModel;
        private SelectionRectangle selectionRectangle;
        private CutOffUtil cutOffUtil;
        public UCCanvas()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 图形绘制
        /// </summary>
        public void OpenGLDraw()
        {
            this.BeginInvoke(new Action(()=> { this.Draw(); }));
        }

        private void openGLControl1_Load(object sender, EventArgs e)
        {
            cursorCollection.InitialCursors();
        }
        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            GlobalModel.CanvasSize = new SizeF(Width, Height);
            OpenGL gl = openGLControl1.OpenGL;
            gl.ClearColor(0, 0, 0, 0);
            
            //gl.CullFace(OpenGL.GL_BACK);
            //gl.Enable(OpenGL.GL_CULL_FACE);

            //gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_LINE);
            //gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_FILL);
            this.Draw();
        }
        internal void SetDataModel(IModel model)
        {
            this.dataModel = model;
        }


        private void openGLControl1_Resized(object sender, EventArgs e)
        {
            GlobalModel.CanvasSize = new SizeF(Width, Height);
            OpenGL gl = openGLControl1.OpenGL;
            gl.Viewport(0, 0, Width, Height);
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            if (Width != 0 || Height != 0)
            {
                gl.Ortho(-Width / 2, Width / 2, -Height / 2, Height / 2, -100000, 100000);
            }
            //gl.LookAt(0, 0, 1, 0, 0, 0, 0, 1, 0);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            this.Draw();
        }

        #region 鼠标事件
        private bool isLeftDown = false;
        float transOffX, transOffY;
        float transWheelX, transWheelY;
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            PointF point = GetMousePoint();
            float wheeldeltatick = 120;
            float zoomdelta = (1.2f * (Math.Abs(e.Delta) / wheeldeltatick));
            float scale = 0.2f;
            if (e.Delta < 0)
            {
                float newZoom = this.zoom / zoomdelta;
                if (newZoom < 0.01)
                {
                    Console.WriteLine("已无法进一步缩小");
                    return;
                }
                this.zoom = newZoom;
                scale = -scale;
            }
            else
            {
                float newZoom = this.zoom * zoomdelta;
                if (newZoom > 100)
                {
                    Console.WriteLine("已无法进一步放大");
                    return;
                }
                this.zoom = newZoom;
            }
            GlobalModel.Zoom = this.zoom;
            PointF originalPoint = this.OpenGLToScreen(new PointF(0, 0));
            this.transWheelX += (originalPoint.X - point.X) * scale;
            this.transWheelY += -(originalPoint.Y - point.Y) * scale;
            this.Draw();
        }

        private void openGLControl1_MouseDown(object sender, MouseEventArgs e)
        {
            this.mouseDownPoint = new PointF(e.X, e.Y);
            PointF point = this.ScreenPointToOpenGLPoint(this.mouseDownPoint);

            this.isLeftDown = (e.Button == MouseButtons.Left);//true;

            base.OnMouseDown(e);

            if (this.isLeftDown)
            {
                if (this.commandType != CommandType.None)
                {
                    switch (this.commandType)
                    {
                        case CommandType.SetStartPoint:
                            this.MouseDownSetStartMovePoint(point);
                            break;
                        case CommandType.SetCoolPoint:
                            this.MouseDownSetCoolPoint(point);
                            break;
                        case CommandType.SetMicroConnect:
                            this.MouseDownSetMicroConnectPoint(point);
                            break;
                        case CommandType.CutOff:
                            this.MouseDownCutOff(true);
                            break;
                        case CommandType.Measure:
                            this.MouseDownMeasure(point);
                            break;
                    }
                }
                else
                {
                    switch (this.operationMode)
                    {
                        case OperationMode.Select:
                            this.MouseDownSelect(point);
                            break;
                        case OperationMode.View:

                            break;
                        case OperationMode.Sort:
                            break;
                        case OperationMode.Move:

                            break;
                    }
                }
            }else
            {
                this.HandleRightMenu(e.Location);
            }
        }

        private void HandleRightMenu(Point point)
        {
            if(this.commandType != CommandType.None)
            {
                this.tsCancelCommand.Text = this.GetCancelCommanButtonText(this.commandType);
                this.tsCancelCommand.Visible = true;
                this.tsRedo.Visible = false;
                this.tsUndo.Visible = false;
            }
            else
            {
                this.tsCancelCommand.Visible = false;
                this.tsRedo.Visible = this.dataModel.CanRedo();
                this.tsUndo.Visible = this.dataModel.CanUndo();
            }

            this.contextMenuStrip1.Show(this, point);
        }

        private String GetCancelCommanButtonText(CommandType command)
        {
            switch (command)
            {
                case CommandType.SetStartPoint:
                    return "退出设置起点";
                case CommandType.SetCoolPoint:
                    return "退出设置冷却点";
                case CommandType.SetMicroConnect:
                    return "退出设置微连";
                case CommandType.Measure:
                    return "退出测量";
                case CommandType.CutOff:
                    return "退出切断";
                default:
                    return string.Empty;
            }
        }

        private void MouseDownSelect(PointF point)
        {
            this.Draw();
            this.selectionRectangle = new SelectionRectangle(point);
        }

        //private bool PointInObject(PointF point)
        //{
        //    PointF p1 = new PointF(20, 20);
        //    PointF p2 = new PointF(30, 20);
        //    PointF p3 = new PointF(20, 40);

        //    float[][] result = MatrixHelper.ConvertOneToTwoDimension(this.modelMatrix2);
        //    float[][] invModelMatrix2 = MatrixHelper.InverseMatrix(result);
        //    float[] result1 = MatrixHelper.ConvertTwoDimensionToOne(invModelMatrix2);
        //    float[] originalV = MatrixHelper.Multi4x4with4x1(result1, new float[] { point.X, point.Y, 0, 1 });

        //    float[] v = MatrixHelper.Multi4x4with4x1(this.modelMatrix2, new float[] { point.X / this.zoom, point.Y / this.zoom, 0, 1 });
        //    PointF testPoint = new PointF(originalV[0], originalV[1]);
        //    //PointF testPoint = new PointF(v[0], v[1]);

        //    float[] v1 = MatrixHelper.Multi4x4with4x1(this.modelMatrix2, new float[] { p1.X, p1.Y, -20, 1 });
        //    float[] v2 = MatrixHelper.Multi4x4with4x1(this.modelMatrix2, new float[] { p2.X, p2.Y, -20, 1 });
        //    float[] v3 = MatrixHelper.Multi4x4with4x1(this.modelMatrix2, new float[] { p3.X, p3.Y, 0, 1 });

        //    Console.WriteLine();
        //    Console.WriteLine($"{point.X},{point.Y}");
        //    //Console.WriteLine($"originalV: {originalV[0]},{originalV[1]}");
        //    //Console.WriteLine($"v: {v[0]},{v[1]}");
        //    Console.WriteLine($"v1: {v1[0]},{v1[1]}");
        //    Console.WriteLine($"v2: {v2[0]},{v2[1]}");
        //    Console.WriteLine($"v3: {v3[0]},{v3[1]}");

        //    List<PointF> points = new List<PointF>();
        //    points.Add(new PointF(v1[0], v1[1]));
        //    points.Add(new PointF(v2[0], v2[1]));
        //    points.Add(new PointF(v3[0], v3[1]));
        //    points.Add(new PointF(v1[0], v1[1]));
        //    float thresholdWidth = this.zoom;
        //    for (int i = 0; i < points.Count - 1; i++)
        //    {
        //        if (HitUtil.IsPointInLine(new PointF(points[i].X, points[i].Y), new PointF(points[i + 1].X, points[i + 1].Y), point, thresholdWidth))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        private void openGLControl1_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            PointF point = this.ScreenPointToOpenGLPoint(e.Location);

            if(this.commandType == CommandType.CutOff)
            {
                this.cutOffUtil.CalZValue(point);
                this.OpenGLDraw();
                return;
            }

            switch (this.operationMode)
            {
                case OperationMode.Select:
                    if (this.isLeftDown)
                    {
                        this.MouseMoveSelect(e);
                    }
                    break;
                case OperationMode.View:
                    if (this.isLeftDown)
                    {
                        this.DoView(e.Location);
                    }
                    break;
                case OperationMode.Sort:
                    break;
                case OperationMode.Move:
                    if (this.isLeftDown)
                    {
                        this.DoMove(e.Location);
                    }
                    break;
            }
        }

        private void MouseMoveSelect(MouseEventArgs e)
        {
            if (this.selectionRectangle != null)
            {
                this.Draw();
                PointF point = this.ScreenPointToOpenGLPoint(e.Location);
                this.selectionRectangle.SetMousePoint(this.openGLControl1.OpenGL, point);
            }
        }

        private void openGLControl1_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.isLeftDown = false;
            switch (this.operationMode)
            {
                case OperationMode.Select:
                    this.MouseUpSelect(e);
                    break;
                case OperationMode.View:
                    this.lastRotationX = (this.lastRotationX + this.rotationX) % 360;
                    this.lastRotationY = (this.lastRotationY + this.rotationY) % 360;
                    this.rotationX = this.rotationY = 0;
                    break;
                case OperationMode.Sort:
                    break;
                case OperationMode.Move:
                    this.lastOffsetX += this.offsetX;
                    this.lastOffsetY -= this.offsetY;
                    this.offsetX = this.offsetY = 0;
                    break;
            }
        }

        private void MouseUpSelect(MouseEventArgs e)
        {
            if (this.selectionRectangle != null)
            {
                this.dataModel.DrawLayer.ClearSelectedStatus();
                Rectangle rect = this.selectionRectangle.ScreenRect();
                List<IDrawObject> hits = null;
                if (rect != RectangleF.Empty)
                {
                    hits = this.dataModel.DrawLayer.GetHitObjects(this.modelMatrix2, rect,this.selectionRectangle.AnyPoint());
                }
                else
                {
                    PointF point = this.ScreenPointToOpenGLPoint(this.mouseDownPoint);
                    hits = this.dataModel.DrawLayer.GetHitObjects(this.modelMatrix2, point);
                }
                hits?.ForEach(p => p.IsSelected = true);
                this.Draw();
                this.selectionRectangle = null;
            }
        }
        private float lastRotationX, lastRotationY;
        private void DoView(Point point)
        {
            this.rotationX = (point.X - this.mouseDownPoint.X) % 360;
            this.rotationY = (point.Y - this.mouseDownPoint.Y) % 360;
            this.Draw();
        }

        private float lastOffsetX, lastOffsetY;
        private void DoMove(Point point)
        {
            this.offsetX = point.X - this.mouseDownPoint.X;
            this.offsetY = point.Y - this.mouseDownPoint.Y;
            transOffX = offsetX + this.lastOffsetX;
            transOffY = -offsetY + this.lastOffsetY;
            this.Draw();
        }
        #endregion
        float[] modelMatrix = new float[16];
        float[] modelMatrix2 = new float[16];

        private void Draw()
        {
            float angleX = rotationX + this.lastRotationX;
            float angelY = rotationY + this.lastRotationY;

            OpenGL gl = openGLControl1.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            gl.Translate(transOffX + transWheelX, transOffY + transWheelY, 0);
            this.DrawCenterCross();
            gl.LookAt(x, y, z, 0, 0, 0, 0, this.upy, this.upz);
            gl.Rotate(angleX, 0.0f, 1.0f, 0.0f);
            gl.Rotate(angelY, 1.0f, 0.0f, 0.0f);

            gl.GetFloat(OpenGL.GL_MODELVIEW_MATRIX, modelMatrix);
            GlobalModel.ModelMatrix = modelMatrix;

            this.DrawAxis(modelMatrix);
            if (this.dataModel != null)
            {
                //显示管道描述信息
                XorGDI.DrawTubeMessage1(this.dataModel.TubeMode, gl); 
            }

            gl.Scale(zoom, zoom, zoom);
            gl.GetFloat(OpenGL.GL_MODELVIEW_MATRIX, modelMatrix2);
            GlobalModel.ModelMatrix2 = modelMatrix2;

            this.dataModel?.Draw(gl);

            this.DrawCutOff(gl);
        }

        private void DrawCutOff(OpenGL gl)
        {
            if (commandType == CommandType.CutOff)
            {
                //222,75,18
                cutOffUtil.CutOffFigure.Draw(gl, new float[] { 222f / 256f, 75f / 256f, 18f / 256f });

                XorGDI.DrawCutOffLocation(cutOffUtil.ZValue, gl);
            }
        }

        private void tsUndo_Click(object sender, EventArgs e)
        {
            this.dataModel.DoUndo();
            this.OpenGLDraw();
        }

        private void tsRedo_Click(object sender, EventArgs e)
        {
            this.dataModel.DoRedo();
            this.OpenGLDraw();
        }

        private void openGLControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if((Control.ModifierKeys == Keys.Control))
            {
                if(e.KeyCode == Keys.Z)
                {
                    tsUndo_Click(null, null);
                }else if(e.KeyCode == Keys.R)
                {
                    tsRedo_Click(null, null);
                }
            }
        }

        private void DrawCenterCross()
        {
            OpenGL gl = this.openGLControl1.OpenGL;
            float halfCrossLen = 8f;
            gl.LineWidth(2);
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(0.4f, 0.4f, 0.4f);
            gl.Vertex(-halfCrossLen, 0, 0f);
            gl.Vertex(halfCrossLen, 0, 0f);
            gl.Vertex(0, -halfCrossLen, 0f);
            gl.Vertex(0, halfCrossLen, 0f);
            gl.End();
            gl.LineWidth(1);
        }

        private void DrawAxis(float[] modelMatrix)
        {
            OpenGL gl = this.openGLControl1.OpenGL;
            float axisLen = 50;//轴长
            float arrowsLen = 5;//箭头长  

            float[] vertexPoint = { axisLen+ arrowsLen, 0, 0, 1 };
            float[] result = { 0, 0, 0, 0 };

            result = MatrixHelper.Multi4x4with4x1(modelMatrix, vertexPoint);
            PointF pointF = this.CenterPointToOpenGLPoint(new PointF(result[0], result[1]));
            gl.DrawText((int)(pointF.X), (int)pointF.Y, 1, 0, 0f, "微软雅黑", 12, "X");

            vertexPoint = new float[] { 0, axisLen+ arrowsLen, 0, 1 };
            result = MatrixHelper.Multi4x4with4x1(modelMatrix, vertexPoint);
            pointF = this.CenterPointToOpenGLPoint(new PointF(result[0], result[1]));
            gl.DrawText((int)(pointF.X), (int)pointF.Y, 1, 1, 0f, "微软雅黑", 12, "Y");

            vertexPoint = new float[] { 0, 0, axisLen+ arrowsLen, 1 };
            result = MatrixHelper.Multi4x4with4x1(modelMatrix, vertexPoint);
            pointF = this.CenterPointToOpenGLPoint(new PointF(result[0], result[1]));
            gl.DrawText((int)(pointF.X), (int)pointF.Y, 1, 0f, 1, "微软雅黑", 12, "Z");

            float[] modelMatrix2 = new float[16];
            gl.GetFloat(OpenGL.GL_MODELVIEW_MATRIX, modelMatrix2);

            Point3D start = new Point3D(0, 0, 0);
            Point3D end = new Point3D(axisLen, 0, 0);
            float[] color = new float[] { 1.0f, 0f, 0f };
            ArrowUtil.DrawArrow(start, end, modelMatrix2, color, gl);

            end = new Point3D(0, axisLen, 0);
            color = new float[] { 1.0f, 1.0f, 0f };
            ArrowUtil.DrawArrow(start, end, modelMatrix2, color, gl);

            end = new Point3D(0, 0, axisLen);
            color = new float[] { 1.0f, 0f, 1.0f };
            ArrowUtil.DrawArrow(start, end, modelMatrix2, color, gl);

            gl.Begin(OpenGL.GL_LINES);

            gl.Color(1.0f, 0, 0);
            gl.Vertex(0, 0, 0f);
            gl.Vertex(axisLen, 0, 0f);//X轴
            //gl.Vertex(axisLen - arrowsLen, arrowsLen, 0.0);
            //gl.Vertex(axisLen, 0, 0f);
            //gl.Vertex(axisLen - arrowsLen, -arrowsLen, 0.0);
            //gl.Vertex(axisLen, 0, 0f);

            gl.Color(1.0f, 1, 0);
            gl.Vertex(0, 0, 0f);
            gl.Vertex(0, axisLen, 0f);//Y轴
            //gl.Vertex(-arrowsLen, axisLen - arrowsLen, 0f);
            //gl.Vertex(0, axisLen, 0f);
            //gl.Vertex(arrowsLen, axisLen - arrowsLen, 0f);
            //gl.Vertex(0, axisLen, 0f);

            gl.Color(1.0f, 0, 1);
            gl.Vertex(0, 0, 0f);
            gl.Vertex(0, 0, axisLen);//Z轴
            //gl.Vertex(-arrowsLen, 0, axisLen - arrowsLen);
            //gl.Vertex(0, 0, axisLen);
            //gl.Vertex(arrowsLen, 0, axisLen - arrowsLen);
            //gl.Vertex(0, 0, axisLen);

            gl.End();
        }

        private void CalLookAtParams(int xCoff, int yCoff, int zCoff)
        {
            if (this.dataModel.TubeMode != null)
            {
                int max = 0;
                switch (this.dataModel.TubeMode.TubeTypes)
                {
                    case StandardTubeMode.TubeType.Circle:
                        max = (int)Math.Max(this.dataModel.TubeMode.CircleRadius, this.dataModel.TubeMode.TubeTotalLength);
                        break;
                    case StandardTubeMode.TubeType.Square:
                        max = (int)Math.Max(this.dataModel.TubeMode.LongSideLength, this.dataModel.TubeMode.TubeTotalLength);
                        break;
                    case StandardTubeMode.TubeType.Rectangle:
                        max = (int)Math.Max(this.dataModel.TubeMode.LongSideLength, this.dataModel.TubeMode.TubeTotalLength);
                        break;
                    case StandardTubeMode.TubeType.Sport:
                        max = (int)Math.Max(this.dataModel.TubeMode.LongSideLength, this.dataModel.TubeMode.TubeTotalLength);
                        break;
                }
                this.x = 3 * xCoff * max;
                this.y = 3 * yCoff * max;
                this.z = 3 * zCoff * max;
            }
        }

        public void SetCursor(object key)
        {
            this.Cursor = this.cursorCollection.GetCursor(key);
        }

        public void SwitchViewModel(ViewModel view)
        {
            this.view = view;
            this.x = this.y = this.z = this.upz=0;
            this.rotationX = this.rotationY = this.lastRotationX = this.lastRotationY = 0;
            this.zoom = this.upy = 1;
            this.offsetX = this.lastOffsetX = 0;
            switch (view)
            {
                case ViewModel.Main:
                    z = 1;
                    break;
                case ViewModel.Up://此处与bc的左右视图一致(我们的左右/俯仰视图与bc的俯仰/左右视图一致)
                    this.upz=y = 1;
                    this.upy = 0;
                    break;
                case ViewModel.Down:
                    this.upz=this.y = -1;
                    this.upy = 0;
                    break;
                case ViewModel.Back:
                    z = -1;
                    break;
                case ViewModel.Left://此处与bc的俯仰试图一致
                    x = -1;
                    break;
                case ViewModel.Right:
                    x = 1;
                    break;
                case ViewModel.WestSouth:
                    x = -1;
                    y = 1;
                    z = 1;
                    break;
                case ViewModel.EastSouth:
                    x = 1;
                    y = 1;
                    z = 1;
                    break;
                case ViewModel.EastNorth:
                    x = 1;
                    y = 1;
                    z = -1;
                    break;
                case ViewModel.WestNorth:
                    x = -1;
                    y = 1;
                    z = -1;
                    break;
            }
            this.CalLookAtParams(this.x, this.y, this.z);           
            this.Draw();
        }

        public void SetCommandType(CommandType commandType)
        {
            this.commandType = commandType;
            this.Cursor = this.cursorCollection.GetCursor(commandType);
        }
        private void tsCancelCommand_Click(object sender, EventArgs e)
        {
            if(commandType == CommandType.CutOff)
            {
                this.cutOffUtil = null;
                this.OpenGLDraw();
            }
            //右键 取消命令(起点、冷却点 微连 测量..)
            this.SetCommandType(CommandType.None);
        }

        public void StartSetCutOff()
        {
            this.cutOffUtil = new CutOffUtil(this.dataModel.TubeMode, this.dataModel.TubeMode.TubeLength / 2);
        }

        private void MouseDownCutOff(bool isMouseDown)
        {
            IDrawObject newObj = cutOffUtil.CutOffFigure;
            newObj.LayerId = (int)LayerId.One;
            newObj.SN = this.dataModel.DrawLayer.Objects.Count + 1;
            newObj.IsLineBold = true;
            newObj.IsSelected = false;
            newObj.FigureUnit = FigureUnit.CutOff;
            newObj.Update();
            this.UpdateIDrawObjct(newObj, null);

            this.SetCommandType(CommandType.None);
            this.OpenGLDraw();

            if (isMouseDown)
            {
                string param = cutOffUtil.ZValue.ToString("f4");
                Messenger.Instance.Send(MainEvent.OnMouseDownEndInput, param);
            }
        }

        public void KeyInputCutOff(float z)
        {
            cutOffUtil.SetZValue(z);
            this.MouseDownCutOff(false);
        }

        private void MouseDownSetStartMovePoint(PointF mousePoint)
        {
            PointF point = this.ScreenPointToOpenGLPoint(this.mouseDownPoint);
            var hitObjects = this.dataModel.DrawLayer.GetHitObjects(this.modelMatrix2, point);
            if (hitObjects.Count == 0)
                return;
            if (hitObjects.Count > 1)
            {
                MessageBox.Show("当前视角以及光标位置存在重叠的图形，请旋转视角后在设置");
                return;
            }

            //设置起点
            IDrawObject oldObject = hitObjects[0];
            IDrawObject newObject = oldObject.Clone();
            newObject.SetPathStartParam(point);

            this.UpdateIDrawObjct(newObject, oldObject);
        }

        private void MouseDownSetCoolPoint(PointF mousePoint)
        {
            PointF point = this.ScreenPointToOpenGLPoint(this.mouseDownPoint);
            var hitObjects = this.dataModel.DrawLayer.GetHitObjects(this.modelMatrix2, point);
            if (hitObjects.Count == 0)
                return;
            if (hitObjects.Count > 1)
            {
                MessageBox.Show("当前视角以及光标位置存在重叠的图形，请旋转视角后在设置");
                return;
            }

            //设置冷却点
            IDrawObject oldObject = hitObjects[0];
            IDrawObject newObject = oldObject.Clone();
            newObject.SetCoolPoint(point);

            this.UpdateIDrawObjct(newObject, oldObject);
        }

        private void UpdateIDrawObjct(IDrawObject newObj, IDrawObject oldObj)
        {
            List<IDrawObject> newObjs = newObj == null ? null : new List<IDrawObject>() { newObj };
            List<IDrawObject> oldObjs = oldObj == null ? null : new List<IDrawObject>() { oldObj };
            EditCommandUpdate editCommand = new EditCommandUpdate(newObjs, oldObjs);
            this.dataModel.DoUpdate(newObjs, oldObjs, editCommand);
            this.OpenGLDraw();
        }

        private void MouseDownSetMicroConnectPoint(PointF mousePoint)
        {
            PointF point = this.ScreenPointToOpenGLPoint(this.mouseDownPoint);
            var hitObjects = this.dataModel.DrawLayer.GetHitObjects(this.modelMatrix2, point);
            if (hitObjects.Count == 0)
                return;
            if (hitObjects.Count > 1)
            {
                MessageBox.Show("当前视角以及光标位置存在重叠的图形，请旋转视角后在设置");
                return;
            }

            //设置冷却点
            IDrawObject oldObject = hitObjects[0];
            IDrawObject newObject = oldObject.Clone();
            newObject.SetMicroConnect(point, GlobalModel.Params.MicroConnectParam);

            this.UpdateIDrawObjct(newObject, oldObject);
        }

        private void MouseDownMeasure(PointF mousePoint)
        {
            MessageBox.Show("未实现测量");
        }

        public void SwitchOperationModel(OperationMode operationMode)
        {
            this.commandType = CommandType.None;
            this.operationMode = operationMode;
            this.Cursor = this.cursorCollection.GetCursor(operationMode);
        }

        private PointF CenterPointToOpenGLPoint(PointF pointF)
        {
            float centerX = this.Width / 2;
            float centerY = this.Height / 2;
            centerX = centerX + pointF.X;
            centerY = centerY + pointF.Y;
            return new PointF(centerX, centerY);
        }
    }
}