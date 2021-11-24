using System;
using System.Drawing;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;
using WSX.Draw3D.Common;
using WSX.Draw3D.DrawTools;
namespace WSX.Draw3D.Utils
{
    /// <summary>
    /// 切断工具类
    /// </summary>
    public class CutOffUtil
    {
        public IDrawObject CutOffFigure { private set; get; }

        public StandardTubeMode TubeMode { private set; get; }

        public float ZValue { private set; get; }

        public CutOffUtil(StandardTubeMode tubeMode, float zvalue)
        {
            this.TubeMode = CopyUtil.DeepCopy(tubeMode);
            this.ZValue = zvalue;

            this.TubeMode.RightAngle = 0.0f;
            this.TubeMode.LeftAngle = 0.0f;

            this.ConvertToCutOffFigure();
        }

        private void ConvertToCutOffFigure()
        {
            StandardTubeConverter converter = new StandardTubeConverter(this.TubeMode);
            IDrawObject cutOffFigure = converter.GetSectionObject();
            cutOffFigure.IsSelected = true;
            this.CutOffFigure = cutOffFigure;
            this.UpdateLocation(this.ZValue);
        }

        private void UpdateLocation(float zvalue)
        {
            this.ZValue = zvalue;
            if (this.CutOffFigure is Polyline3D)
            {
                Polyline3D polyline3D = this.CutOffFigure as Polyline3D;
                polyline3D.Points.ForEach(p => p.Z = this.ZValue);
                polyline3D.Update();
            }
        }

        public void SetZValue(float zvalue)
        {
            this.ZValue = zvalue;
            this.UpdateLocation(this.ZValue);
        }

        public void CalZValue(PointF mousePoint)
        {
            Point3D pd1 = new Point3D(0, 0, 0);
            Point3D pd2 = new Point3D(0, 0, 50);

            PointF p1 = CoordinateUtil.Point3dToCenterPoint(pd1);
            PointF p2 = CoordinateUtil.Point3dToCenterPoint(pd2);

            PointF zPoint = HitUtil.CalFootPoint(p1, p2, mousePoint);

            float d12 = HitUtil.Distance(p1, p2);
            float d1z = HitUtil.Distance(p1, zPoint);
            float d2z = HitUtil.Distance(p2, zPoint);

            float value = float.NaN;
            if (d2z > d1z && d2z > d12)
            {
                value = -1 * (pd2.Z - pd1.Z) * (d1z / d12);
            }
            else
            {
                value = (pd2.Z - pd1.Z) * (d1z / d12);
            }

            if(float.IsInfinity(value) || float.IsNegativeInfinity(value) || float.IsNaN(value))
            {
                //俯视图时计算结果为无穷大 不更新
            }else
            {
                this.ZValue = value;
                this.UpdateLocation(this.ZValue);
            }
        }
    }
}

