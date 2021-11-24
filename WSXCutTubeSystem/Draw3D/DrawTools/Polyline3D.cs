using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SharpGL;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.DrawModel.Compensation;
using WSX.CommomModel.DrawModel.MicroConn;
using WSX.CommomModel.Enums;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;
using WSX.Draw3D.Common;
using WSX.Draw3D.MathTools;
using WSX.Draw3D.Utils;
using WSX.GlobalData.Model;

namespace WSX.Draw3D.DrawTools
{
    /// <summary>
    /// 多段线(直线相连)
    /// </summary>
    public class Polyline3D : DrawObjectBase
    {
        public Polyline3D()
        {
            Type = FigureType.Polyline;
            MicroConnParams = new List<MicroConnectModel>();
        }
        public List<Point3D> Points { get; set; } = new List<Point3D>();
        public List<Point3D> MicroConnectPoints { get; set; }
        public List<Point3D> CompensationPoints { get; set; }


        public List<MicroUnitPoint> MicroStartPoints { get; set; } = new List<MicroUnitPoint>();
        /// <summary>
        /// 起始点所处位置参数
        /// </summary>
        public float PathStartParam = float.NaN;

        private float PathStartParamMicroConnect;

        //private CompensationModel CompensationParam;

        /// <summary>
        /// 总长度
        /// </summary>
        public override float SizeLength { get; protected set; }

        //public List<MicroConnectModel> MicroConnParams { private set; get; } = new List<MicroConnectModel>();

        public override void Draw(OpenGL gl, float[] color)
        {
            if (Points == null || Points.Count < 2) return;
            gl.PushMatrix();
            if (IsSelected)
            {
                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                gl.LineStipple(1, 0x739C);
            }
            gl.LineWidth(IsSelected || !IsLineBold ? 1 : 2);
            gl.Color(color);
            //gl.Begin(this.IsClosed ? OpenGL.GL_LINE_LOOP : OpenGL.GL_LINE_STRIP);
            gl.Begin(OpenGL.GL_LINE_STRIP);
            if (this.MicroConnectPoints != null && this.MicroConnectPoints.Count > 0)
            {
                MicroConnectPoints.ForEach(e =>
                {
                    gl.Vertex(e.X, e.Y, e.Z);
                    if (e.HasMicroConn)
                    {
                        gl.End();
                        gl.Begin(OpenGL.GL_LINE_STRIP);
                    }
                });
                if (this.IsClosed)
                {
                    gl.Vertex(Points[0].X, Points[0].Y, Points[0].Z);
                }
            }
            else
            {
                Points.ForEach(e => gl.Vertex(e.X, e.Y, e.Z));
                if (this.IsClosed)
                {
                    gl.Vertex(Points[0].X, Points[0].Y, Points[0].Z);
                }
            }
            gl.End();

            //补偿
            if (this.CompensationPoints != null && this.CompensationPoints.Count > 0)
            {
                //gl.Begin(this.IsClosed ? OpenGL.GL_LINE_LOOP : OpenGL.GL_LINE_STRIP);
                gl.Begin(OpenGL.GL_LINE_STRIP);
                gl.Color(1.0f, 1.0, 1.0);

                this.CompensationPoints.ForEach(e =>
                {
                    gl.Vertex(e.X, e.Y, e.Z);
                    if (e.HasMicroConn)
                    {
                        gl.End();
                        gl.Begin(OpenGL.GL_LINE_STRIP);
                    }
                });
                if (this.IsClosed)
                {
                    gl.Vertex(CompensationPoints[0].X, CompensationPoints[0].Y, CompensationPoints[0].Z);
                }
                gl.Color(color);
                gl.End();
            }

            gl.Disable(OpenGL.GL_LINE_STIPPLE);
            gl.PopMatrix();

            this.ShowCoolPoint(gl);
            //this.ShowStartMovePoint(gl);
            //this.ShowMultiLoopFlag(gl);
        }

        public override void ShowStartMovePoint(OpenGL gl)
        {
            //if (GlobalModel.Params.AdditionalInfo.IsShowStartMovePoint && StartMovePoint != null)
            if (StartMovePoint != null)
            {
                gl.PushMatrix();
                gl.PointSize(5.5f);
                gl.Color(1, 1, 1f);
                gl.Begin(OpenGL.GL_POINTS);
                gl.Vertex(StartMovePoint.X, StartMovePoint.Y, StartMovePoint.Z);
                gl.End();
                gl.PointSize(1f);
                gl.PopMatrix();
            }
        }

        public override void ShowFigureSN(OpenGL gl)
        {
            //if (GlobalModel.Params.AdditionalInfo.IsShowFigureSN && StartMovePoint != null)
            if (StartMovePoint != null)
            {
                PointF startPoint2D = CoordinateUtil.Point3dToScreenPoint(StartMovePoint);
                gl.DrawText((int)(startPoint2D.X) + 5, (int)startPoint2D.Y + 5, 1, 1, 1f, "微软雅黑", 12, this.SN.ToString());
            }
        }
        public void ShowCoolPoint(OpenGL gl)
        {
            if (this.MicroStartPoints == null)
                return;
            List<MicroUnitPoint> coolPoints = MicroStartPoints.Where(
                e => e.Flags == MicroConnectFlags.CoolingPoint || e.Flags == MicroConnectFlags.CoolingLeadIn).ToList();
            if (coolPoints.Count == 0)
                return;

            gl.PushMatrix();
            gl.PointSize(7.0f);
            gl.Color(1, 1, 1f);
            gl.Begin(OpenGL.GL_POINTS);

            coolPoints.ForEach(e =>
            {
                gl.Vertex(e.Point.X, e.Point.Y, e.Point.Z);
            });

            gl.End();
            gl.PointSize(1f);
            gl.PopMatrix();
        }

        public override void ShowMachinePath(float[] matrix, OpenGL gl, float[] color)
        {
            List<Tuple<Point3D, Point3D>> arrows = ArrowUtil.CalShowArrowsPoints(this);
            foreach (var points in arrows)
            {
                ArrowUtil.DrawArrow(points.Item1, points.Item2, matrix, color, gl);
            }
        }

        public override IDrawObject Clone()
        {
            Polyline3D newObj = new Polyline3D();
            newObj.Copy(this);
            return newObj;
        }

        public override void Copy(IDrawObject source)
        {
            var data = source as Polyline3D;
            base.Copy(data);
            this.Points = CopyUtil.DeepCopy(data.Points);
            this.PathStartParam = data.PathStartParam;
            this.MicroConnParams = CopyUtil.DeepCopy(data.MicroConnParams);
            this.CompensationParam = CopyUtil.DeepCopy(data.CompensationParam);
            this.Update();
        }

        public override bool ObjectInRectangle(float[] matrix, RectangleF rect, bool anyPoint)
        {
            if (anyPoint)
            {
                List<Point3D> pfloats = new List<Point3D>();
                this.Points.ForEach(p =>
                {
                    var vertex = MatrixHelper.Multi4x4with4x1(matrix, new float[] { p.X, p.Y, p.Z, 1 });
                    pfloats.Add(new Point3D(vertex[0], vertex[1]));
                });
                for (int i = 0; i < pfloats.Count - 1; i++)
                {
                    if (HitUtil.LineIntersectWithRect(pfloats[i], pfloats[i + 1], rect))
                    {
                        return true;
                    }
                }
                if (HitUtil.LineIntersectWithRect(pfloats[pfloats.Count - 1], pfloats[0], rect))
                {
                    return true;
                }
                return false;
            }
            else
            {
                return HitUtil.ObjectInRectangle(matrix, rect, this.Points);
            }
        }

        public override bool PointInObject(float[] matrix, PointF point)
        {
            if (this.MicroConnectPoints != null && this.MicroConnectPoints.Count > 0)
            {
                return HitUtil.PointInObject(matrix, 0, point, this.MicroConnectPoints, this.IsClosed);
            }
            return HitUtil.PointInObject(matrix, 0, point, this.Points, this.IsClosed);
        }

        private void Move(float offsetX, float offsetY, float offsetZ)
        {
            foreach (var point in this.Points)
            {
                point.Translate(offsetX, offsetY, offsetZ);
            }
            this.Update();
        }

        public override void MoveAxisZ(float offset)
        {
            this.Move(0f, 0f, offset);
        }

        public override void SetPathStartParam(PointF hitPoint)
        {
            //if(this.FigureUnit == FigureUnit.Section)
            //{
            //    this.StartMovePoint = null;
            //    return;
            //}
            PathStartParam = HitUtil.CalPolylinePosition(GlobalModel.ModelMatrix2, hitPoint, this.Points, this.IsClosed);
            this.Update();
        }

        public override void SetCoolPoint(PointF hitPoint)
        {
            float position = HitUtil.CalPolylinePosition(GlobalModel.ModelMatrix2, hitPoint, this.Points, this.IsClosed);
            MicroConnectModel coolPointParam = new MicroConnectModel()
            {
                Size = 0,
                Position = position,
                Flags = MicroConnectFlags.CoolingPoint,
            };
            this.MicroConnParams.Add(coolPointParam);
            this.Update();
        }

        public override void Update()
        {
            if (this.Points.Count <= 0) return;
            MinZ = this.Points.Min(e => e.Z);
            MaxZ = this.Points.Max(e => e.Z);

            UpdateMicroConnect();
            UpdateSizeLength();
            UpdateStartMovePoint();
            UpdateEndMovePoint();
            UpdateCompensation();
            UpdateCoolPoint();
        }

        /// <summary>
        /// 更新起始点
        /// </summary>
        /// <param name="accumulative">参数为各个点到起始点的累加距离</param>
        private void UpdateStartMovePoint()
        {
            //if(this.FigureUnit == FigureUnit.Section)
            //{
            //    this.StartMovePoint = null;
            //    return;
            //}

            //默认为第一个点
            this.StartMovePoint = CopyUtil.DeepCopy(this.Points[0]);

            if (this.MicroConnectPoints != null && this.PathStartParamMicroConnect > 0.0f)
            {
                this.StartMovePoint = this.GetPointByPosition(PathStartParamMicroConnect);
            }
            //另外设置了起始点 更新起始点
            else if (PathStartParam > 0.0f)
            {
                this.StartMovePoint = this.GetPointByPosition(PathStartParam);
            }

            if (this.CompensationParam != null)
            {
                this.StartMovePoint.Z += (float)this.CompensationParam.Size * (float)CompensationSign();
            }

            //如果有引入线？
        }
        private void UpdateEndMovePoint()
        {
            //暂时为零
            this.EndMovePoint = this.Points[this.Points.Count - 1];
        }

        private void UpdateCoolPoint()
        {
            MicroStartPoints?.RemoveAll(e => e.Flags == MicroConnectFlags.CoolingPoint || e.Flags == MicroConnectFlags.CoolingLeadIn);

            if (this.MicroConnParams == null)
                return;

            List<MicroConnectModel> coolPointParams = MicroConnParams.Where(
                e => e.Flags == MicroConnectFlags.CoolingPoint || e.Flags == MicroConnectFlags.CoolingLeadIn).ToList();
            if (coolPointParams.Count == 0)
                return;

            MicroStartPoints = MicroStartPoints == null ? new List<MicroUnitPoint>() : MicroStartPoints;

            Point3D coolPoint3D = null;
            MicroUnitPoint coolPoint = null;
            Point3D coolPoint3D2 = null;
            MicroUnitPoint coolPoint2 = null;
            coolPointParams.ForEach(e =>
            {
                coolPoint3D = this.GetPointByPosition(e.Position);
                coolPoint = new MicroUnitPoint()
                {
                    Flags = e.Flags,
                    Point = coolPoint3D,
                    SizeLength = 0,
                    IsBasePoint = true,
                };
                this.MicroStartPoints.Add(coolPoint);

                //需先执行 UpdateCompensation计算出 this.CompensationPoints
                if (this.CompensationParam != null && this.CompensationPoints != null)
                {
                    coolPoint3D2 = this.GetPointByPosition(e.Position, true);
                    coolPoint2 = new MicroUnitPoint()
                    {
                        Flags = e.Flags,
                        Point = coolPoint3D2,
                        SizeLength = 0,
                        IsBasePoint = false,
                    };
                    this.MicroStartPoints.Add(coolPoint2);
                }

                //if(coolPoint3D2 != null && (coolPoint3D.X != coolPoint3D2.X || coolPoint3D.Y != coolPoint3D2.Y)){
                //    int xxx = 000;
                //}
            });
        }

        private Point3D GetPointByPosition(float position, bool isCompensation = false)
        {
            List<Point3D> points = Points;// isCompensation ? this.CompensationPoints : this.Points;

            if (position < 0 || (this.IsClosed && position >= points.Count) || (!this.IsClosed && position >= points.Count - 1))
                return null;
            int startIdx = (int)Math.Floor(position);
            float ratio = position - (float)startIdx;
            int endIdx = (startIdx == points.Count - 1) ? 0 : startIdx + 1;

            Point3D point3D = null;
            if (ratio <= 0.00001)
            {
                point3D = CopyUtil.DeepCopy(points[startIdx]);
            }
            else
            {
                double distance = HitUtil.Distance(points[startIdx], points[endIdx]) * ratio;
                point3D = HitUtil.GetLinePointByDistance(points[startIdx], points[endIdx], (float)distance);
            }

            if (isCompensation)
            {
                point3D.Z += (float)this.CompensationParam.Size * (float)CompensationSign();
            }
            return point3D;
        }

        /// <summary>
        /// 更新图形总长度
        /// </summary>
        private void UpdateSizeLength()
        {
            if (this.Points == null || this.Points.Count == 0)
            {
                this.SizeLength = 0;

            }
            double sum = 0.0f;
            double size = 0.0f;
            for (int i = 1; i < this.Points.Count; i++)
            {
                size = HitUtil.Distance(this.Points[i - 1], this.Points[i]);
                sum += size;
            }
            if (this.IsClosed)
            {
                size = HitUtil.Distance(this.Points[this.Points.Count - 1], this.Points[0]);
                sum += size;
            }
            this.SizeLength = (float)sum;
        }

        public override void SetMicroConnect(PointF hitPoint, MicroConnectParam connectParam)
        {
            float position = HitUtil.CalPolylinePosition(GlobalModel.ModelMatrix2, hitPoint, this.Points, this.IsClosed);
            MicroConnectModel microConnectParam = new MicroConnectModel()
            {
                Position = position,
                Size = connectParam.MicroSize,
                Flags = MicroConnectFlags.MicroConnect,
            };
            if (MicroConnParams == null)
                MicroConnParams = new List<MicroConnectModel>();

            this.MicroConnParams.Add(microConnectParam);

            //如果微连缺口处有冷却点则删除
            this.MicroConnParams.RemoveAll(e =>
                e.Flags == MicroConnectFlags.CoolingPoint && MicroConnectUtil.IsPosInMicroConnect(e.Position, microConnectParam, this.Points, this.IsClosed)
            );

            this.Update();
        }

        private List<float[]> microSections = null;
        private void UpdateMicroConnect()
        {
            if (this.MicroConnParams == null || MicroConnParams.Count == 0)
            {
                MicroConnectPoints = null;
                return;
            }

            var microParams = MicroConnParams.Where(e => e.Flags == MicroConnectFlags.MicroConnect || e.Flags == MicroConnectFlags.GapPoint).OrderBy(e => e.Position).ToList();
            if (microParams.Count <= 0)
            {
                MicroConnectPoints = null;
                return;
            }
            //List<float[]> microSections = null;
            this.MicroConnectPoints = MicroConnectUtil.GetMicroConnectPoints(this.Points, microParams, this.PathStartParam, this.IsClosed, out this.microSections);
            if (microSections == null || microSections.Count == 0)
            {
                this.PathStartParamMicroConnect = this.PathStartParam;
            }
            else
            {
                this.PathStartParamMicroConnect = MicroConnectUtil.GetMicroConnectPathStartParam(this.PathStartParam, microSections);
            }
        }

        public override void SetGap(GapModel gapModel)
        {
            if (this.MicroConnParams != null)
            {
                this.MicroConnParams.RemoveAll(e => e.Flags == MicroConnectFlags.GapPoint);
            }
            else
            {
                this.MicroConnParams = new List<MicroConnectModel>();
            }

            if (gapModel.GapSize > 0)
            {
                MicroConnectModel gapParam = new MicroConnectModel();
                gapParam.Size = gapModel.GapSize;
                gapParam.Flags = MicroConnectFlags.GapPoint;
                //update 的时候在根据PathStartParam以及GapSize计算position
                this.MicroConnParams.Add(gapParam);

                //如果缺口处有冷却点则删除
                this.MicroConnParams.RemoveAll(e =>
                    e.Flags == MicroConnectFlags.CoolingPoint && MicroConnectUtil.IsPosInMicroConnect(e.Position, gapParam, this.Points, this.IsClosed)
                );
            }

            this.Update();
        }

        public override void DoCompensation(CompensationModel param)
        {
            this.CompensationParam = param;
            if (param.Style == CompensationType.Cancel)
            {
                this.CompensationParam = null;
                this.CompensationPoints = null;
            }
            this.Update();
        }

        private int CompensationSign()
        {
            int sign = 0;
            if (this.CompensationParam.Style == CompensationType.AllOuter)
            {
                sign = -1;
            }
            else if (this.CompensationParam.Style == CompensationType.AllInner)
            {
                sign = 1;
            }
            else if (this.CompensationParam.Style == CompensationType.Auto && !this.IsInnerCut)
            {
                sign = -1;
            }
            else if (this.CompensationParam.Style == CompensationType.Auto && this.IsInnerCut)
            {
                sign = 1;
            }
            return sign;
        }

        private void UpdateCompensation()
        {
            if (this.CompensationParam == null || this.Points == null || this.FigureUnit == FigureUnit.Coated)
                return;

            int sign = this.CompensationSign();

            Point3D newPoint = null;
            this.CompensationPoints = new List<Point3D>();
            if (this.MicroConnectPoints != null && this.MicroConnectPoints.Count > 0)
            {
                this.MicroConnectPoints.ForEach(p =>
                {
                    newPoint = new Point3D(p.X, p.Y, p.Z + (float)sign * (float)this.CompensationParam.Size);
                    newPoint.HasMicroConn = p.HasMicroConn;
                    CompensationPoints.Add(newPoint);
                });
            }
            else
            {
                this.Points.ForEach(p =>
                {
                    newPoint = new Point3D(p.X, p.Y, p.Z + (float)sign * (float)this.CompensationParam.Size);
                    CompensationPoints.Add(newPoint);
                });
            }
        }

        public override void Reverse()
        {
            //Points反向
            this.Points.Reverse();
            if (this.IsClosed)
            {
                this.Points.Insert(0, this.Points[this.Points.Count - 1]);
                this.Points.RemoveAt(this.Points.Count - 1);
            }

            //起点
            if (!float.IsNaN(this.PathStartParam) && this.PathStartParam != 0f)
            {
                this.PathStartParam = this.ReversePos(this.PathStartParam, this.Points.Count, this.IsClosed);
            }

            //微连、冷却点
            this.MicroConnParams?.ForEach(e =>
            {
                if (e.Flags != MicroConnectFlags.GapPoint)
                {
                    e.Position = this.ReversePos(e.Position, this.Points.Count, this.IsClosed);
                }
            });

            this.Update();
        }

        private float ReversePos(float position, int pointCount, bool isClosed)
        {
            //int index = (int)Math.Floor(position);
            //float ratio = position - (float)index;

            if (IsClosed)
            {
                return (float)this.Points.Count - position; //(this.Points.Count - 1 - index) + (1.0f - ratio);
            }
            else
            {
                return (float)this.Points.Count - 1 - position; //(this.Points.Count - 2 - index) + (1.0f - ratio);
            }
        }

        public override void Clear(ClearCommandType commandType)
        {
            switch (commandType)
            {
                case ClearCommandType.Compensation:
                    this.CompensationParam = null;
                    this.CompensationPoints = null;
                    break;
                case ClearCommandType.CoolPoint:
                    this.MicroConnParams?.RemoveAll(e => e.Flags == MicroConnectFlags.CoolingLeadIn || e.Flags == MicroConnectFlags.CoolingPoint);
                    break;
                case ClearCommandType.MicroConnect:
                    this.MicroConnParams?.RemoveAll(e => e.Flags == MicroConnectFlags.MicroConnect);
                    break;
                case ClearCommandType.Gap:
                    this.MicroConnParams?.RemoveAll(e => e.Flags == MicroConnectFlags.GapPoint);
                    break;
                case ClearCommandType.LeadLine:

                    //未实现
                    break;
                default:
                    break;
            }
            this.Update();
        }
    }
}