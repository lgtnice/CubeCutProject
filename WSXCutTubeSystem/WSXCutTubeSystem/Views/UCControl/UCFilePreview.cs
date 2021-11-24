using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using SharpGL;
using WSX.CommomModel.Enums;
using WSX.Draw3D.Common;
using WSX.CommomModel.ParaModel;
using WSX.Draw3D.MathTools;
using WSX.Draw3D.Utils;
using WSX.DataCollection.Document;
using WSXCutTubeSystem.Manager;
using WSX.Draw3D;

namespace WSXCutTubeSystem.Views.UCControl
{
    public partial class UCFilePreview : UserControl
    {
        private int x = 1, y = 1, z = 1;
        private float offsetX = 0.0f;
        private float offsetY = 0.0f;
        private float zoom = 1.0f;
        private IModel dataModel;
        private PointF mouseDownPoint;
        private OperationMode operationMode = OperationMode.Move;
        public bool IsPreView { get { return this.ckPreView.Checked; } }
        public UCFilePreview()
        {
            InitializeComponent();
            this.dataModel = new DataModel();
            this.openGLControl1.OpenGLInitialized += openGLControl1_OpenGLInitialized;
            this.openGLControl1.Resized += openGLControl1_Resized;
            this.openGLControl1.MouseMove += openGLControl1_MouseMove;
            this.openGLControl1.MouseUp += openGLControl1_MouseUp;
            this.openGLControl1.MouseDown += openGLControl1_MouseDown;
        }



        internal void FigurePreview(IDocument<FigureBase3DModel> doc)
        {
            this.label1.Text = string.Empty;
            this.dataModel.DrawLayer.Clear();
            this.dataModel.MarkLayer.Clear();
            if (doc != null)
            {
                var draws = doc.Entity.Figures.Where(e => !e.IsMark).ToList();
                var marks = doc.Entity.Figures.Where(e => e.IsMark).ToList();
                this.dataModel.DrawLayer.AddRange(FigureManager.ToIDrawObjects(draws));
                this.dataModel.MarkLayer.AddRange(FigureManager.ToIDrawObjects(marks));
                this.dataModel.TubeMode = doc.TubeMode;
                this.label1.Text = this.dataModel.TubeMode.ToString();
            }
            this.OpenGLDraw();
        }

        /// <summary>
        /// 图形绘制
        /// </summary>
        public void OpenGLDraw()
        {
            this.BeginInvoke(new Action(() => { this.Draw(); }));
        }

        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl1.OpenGL;
            gl.ClearColor(0, 0, 0, 0);
            gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_LINE);
            this.CalLookAtParams(this.x, this.y, this.z);
        }


        private void openGLControl1_Resized(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl1.OpenGL;
            gl.Viewport(0, 0, Width, Height);
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            if (Width != 0 || Height != 0)
            {
                gl.Ortho(-Width / 4, Width / 4, -Height / 4, Height / 4, -100000, 100000);
            }
            gl.LookAt(0, 0, z, 0, 0, 0, 0, 1, 0);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
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
            PointF originalPoint = this.OpenGLToScreen(new PointF(0, 0));
            this.transWheelX += (originalPoint.X - point.X) * scale;
            this.transWheelY += -(originalPoint.Y - point.Y) * scale;
            OpenGLDraw();
        }
        private void openGLControl1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.mouseDownPoint = new PointF(e.X, e.Y);
            PointF point = this.ScreenPointToOpenGLPoint(this.mouseDownPoint);
            this.isLeftDown = true;
        }
        private void openGLControl1_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            PointF point = this.ScreenPointToOpenGLPoint(e.Location);
            switch (this.operationMode)
            {
                case OperationMode.Select:

                    break;
                case OperationMode.View:

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

        private void openGLControl1_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.isLeftDown = false;
            switch (this.operationMode)
            {
                case OperationMode.Select:
                    break;
                case OperationMode.View:
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


        private float lastOffsetX, lastOffsetY;
        private void DoMove(Point point)
        {
            this.offsetX = point.X - this.mouseDownPoint.X;
            this.offsetY = point.Y - this.mouseDownPoint.Y;
            transOffX = offsetX + this.lastOffsetX;
            transOffY = -offsetY + this.lastOffsetY;
            OpenGLDraw();
        }
        #endregion

        float[] modelMatrix = new float[16];
        float[] modelMatrix2 = new float[16];

        private void Draw()
        {
            //float angleX = rotationX + this.lastRotationX;
            //float angelY = rotationY + this.lastRotationY;

            OpenGL gl = openGLControl1.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            gl.Translate(transOffX + transWheelX, transOffY + transWheelY, 0);

            gl.LookAt(x, y, z, 0, 0, 0, 0, 1, 0);

            //gl.Rotate(angleX, 0.0f, 1.0f, 0.0f);
            //gl.Rotate(angelY, 1.0f, 0.0f, 0.0f);

            gl.GetFloat(OpenGL.GL_MODELVIEW_MATRIX, modelMatrix);
            gl.Scale(zoom, zoom, zoom);

            //this.dataModel?.Draw(gl);
            this.dataModel.MarkLayer.Draw(gl);
            this.dataModel.DrawLayer.Draw(gl);

            gl.GetFloat(OpenGL.GL_MODELVIEW_MATRIX, modelMatrix2);
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

        private PointF GetMousePoint()
        {
            Point point = this.PointToClient(Control.MousePosition);
            return point;
        }

        private PointF ScreenPointToOpenGLPoint(PointF screenPoint)
        {
            PointF result = new PointF();
            result.X = (screenPoint.X - this.Width / 2);
            result.Y = (-screenPoint.Y + this.Height / 2);
            return result;
        }

        private PointF OpenGLToScreen(PointF point)
        {
            float[] result = MatrixHelper.Multi4x4with4x1(this.modelMatrix2, new float[] { point.X, point.Y, 0, 1 });
            result[0] += this.Width / 2;
            result[1] = this.Height / 2 - result[1];
            return new PointF(result[0], result[1]);
        }
    }
}
