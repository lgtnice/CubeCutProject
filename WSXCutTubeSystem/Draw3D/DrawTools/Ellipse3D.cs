using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.Utilities;
using WSX.Draw3D.Common;
using WSX.Draw3D.Utils;


namespace WSX.Draw3D.DrawTools
{
    public class Ellipse3D : Spline3D
    {
        /// <summary>
        /// 
        /// </summary>
        public Point3D CenterPoint { get; set; } = new Point3D();
        /// <summary>
        /// 长轴
        /// </summary>
        public float RadiusA { get; set; } = 0.0f;
        /// <summary>
        /// 短轴
        /// </summary>
        public float RadiusB { get; set; } = 0.0f;
        /// <summary>
        /// 长轴方向单位向量
        /// </summary>
        public Point3D UnitVectorU { get; set; } = new Point3D();
        /// <summary>
        /// 短轴方向单位向量
        /// </summary>
        public Point3D UnitVectorV { get; set; } = new Point3D();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="angle">倾斜角</param>
        /// <param name="translateDistance"></param>
        public Ellipse3D(float radius, float angle, Point3D translateDistance)
        {
            float LongSide = (float)(radius / Math.Cos(angle));
            float ShortSide = radius;
            Type = FigureType.Spline;
            IsClosed = true;

            float K = 2.0f;
            float weight = 1.0f / 3.0f;

            Point3D unitVectorLong = new Point3D((float)Math.Cos(angle), 0, (float)Math.Sin(angle));
            Point3D unitVectorShort = new Point3D(0, 1, 0);
            Knots.AddRange(new float[] { 0, 0, 0, 0, 0.5f, 0.5f, 0.5f, 1, 1, 1, 1 });

            //长轴上的位移
            Point3D lMove = new Point3D();
            //短轴上的位移
            Point3D sMove = new Point3D();
            //管长平移量
            Point3D translateMove = translateDistance;

            lMove = unitVectorLong * LongSide;
            sMove = new Point3D();
            ControlPoints.Add(lMove + sMove + translateMove);

            lMove = unitVectorLong * LongSide;
            sMove = unitVectorShort * K * ShortSide;
            ControlPoints.Add(lMove + sMove + translateMove);

            lMove = unitVectorLong * -1.0f * LongSide;
            sMove = unitVectorShort * K * ShortSide;
            ControlPoints.Add(lMove + sMove + translateMove);

            lMove = unitVectorLong * -1.0f * LongSide;
            sMove = new Point3D();
            ControlPoints.Add(lMove + sMove + translateMove);

            lMove = unitVectorLong * -1.0f * LongSide;
            sMove = unitVectorShort * -1.0f * K * ShortSide;
            ControlPoints.Add(lMove + sMove + translateMove);

            lMove = unitVectorLong * LongSide;
            sMove = unitVectorShort * -1.0f * K * ShortSide;
            ControlPoints.Add(lMove + sMove + translateMove);

            lMove = unitVectorLong * LongSide;
            sMove = new Point3D();
            ControlPoints.Add(lMove + sMove + translateMove);

            ControlPoints[1].Weight = weight;
            ControlPoints[2].Weight = weight;
            ControlPoints[4].Weight = weight;
            ControlPoints[5].Weight = weight;

            CenterPoint = translateDistance;
            RadiusA = LongSide;
            RadiusB = ShortSide;
            UnitVectorU = unitVectorLong;
            UnitVectorV = unitVectorShort;
        }
        private Ellipse3D()
        {

        }

        public override bool ObjectInRectangle(float[] matrix, RectangleF rect, bool anyPoint)
        {
            return false;
        }
        public override bool PointInObject(float[] matrix, PointF point)
        {
            List<Point3D> list = GetPointsByAngle(0,(float)(2 * Math.PI),0.01f);
            return HitUtil.PointInObject(matrix, 0, point, list, this.IsClosed);
        }

        public override IDrawObject Clone()
        {
            Ellipse3D newObj = new Ellipse3D();
            this.Copy(newObj);
            return newObj;
        }

        public override void Copy(IDrawObject newObj)
        {
            var nObj = newObj as Ellipse3D;
            base.Copy(nObj);
            nObj.UnitVectorU = CopyUtil.DeepCopy(this.UnitVectorU);
            nObj.UnitVectorU = CopyUtil.DeepCopy(this.UnitVectorU);
            nObj.CenterPoint = CopyUtil.DeepCopy(this.CenterPoint);
            nObj.RadiusA = this.RadiusA;
            nObj.RadiusB = this.RadiusB;
            nObj.Update();
        }

        /// <summary>
        /// 根据节点值和控制点坐标值 求 曲线对应数轴上的坐标值
        /// </summary>
        /// <param name="u"></param>
        /// <param name="ControlPointCoordinateValue"></param>
        /// <returns></returns>
        public Point3D GetFuncValue(float u,Point3D[] controlPoints)
        {
            float[] baseFunctionValue;
            float numerator_x = 0.0f;
            float numerator_y = 0.0f;
            float numerator_z = 0.0f;
            float denominator = 0.0f;
            if (u < 0.5f)
            {
                baseFunctionValue = new float[]
                {
                    (float)Math.Pow(1.0f - (2.0f*u),3.0f),
                    (float)Math.Pow(1.0f - (2.0f*u),2.0f) * 6 * u,
                    (float)Math.Pow(u,2.0f) * (1.0f - (2.0f*u)) * 12,
                    (float)Math.Pow(u,3.0f) * 8,
                };
            }
            else
            {
                baseFunctionValue = new float[]
                {
                    (float)Math.Pow(2.0f - (2.0f*u),3.0f),
                    (float)Math.Pow(2.0f - (2.0f*u),2.0f) * 3 * (2 * u - 1),
                    (float)Math.Pow((2 * u) - 1,2.0f) * (2.0f - (2.0f*u)) * 3,
                    (float)Math.Pow((2 * u) - 1,3.0f),
                };
            }
            float[] stride = new float[] { 1.0f,1.0f / 3.0f, 1.0f / 3.0f ,1.0f};

            for (int i = 0; i < baseFunctionValue.Length; i++)
            {
                numerator_x += baseFunctionValue[i] * stride[i] * controlPoints[i].X;
                numerator_y += baseFunctionValue[i] * stride[i] * controlPoints[i].Y;
                numerator_z += baseFunctionValue[i] * stride[i] * controlPoints[i].Z;
                denominator += baseFunctionValue[i] * stride[i];
            }

            return new Point3D(numerator_x / denominator , numerator_y / denominator ,numerator_z / denominator);
        }

        /// <summary>
        /// 通过节点knot的等差 递增，来求对应的点的集合
        /// </summary>
        /// <param name="startDistanceRate">[0,1]之间的数</param>
        /// <param name="endDistanceRate">[0,1]之间的数</param>
        /// <param name="step"></param>
        /// <returns></returns>
        public override List<Point3D> GetPointsByKnots(float startDistanceRate,float endDistanceRate,float step)
        {
            List<Point3D> result = new List<Point3D>();
            float currentRate = startDistanceRate;
            while (currentRate < endDistanceRate)
            {
                Point3D point = new Point3D();
                if (currentRate < 0.5f)
                {
                    point = GetFuncValue(currentRate,new Point3D[] { ControlPoints[0] , ControlPoints[1], ControlPoints[2], ControlPoints[3]});
                }
                else
                {
                    point = GetFuncValue(currentRate, new Point3D[] { ControlPoints[3], ControlPoints[4], ControlPoints[5], ControlPoints[6] });
                }
                result.Add(point);
                currentRate += step;
            }
            return result;
        }

        /// <summary>
        /// 从开始角度到终止角度逆时针旋转
        /// </summary>
        /// <param name="startAngle">（弧度为单位）</param>
        /// <param name="endAngle">（弧度为单位）</param>
        /// <param name="step"></param>
        /// <returns></returns>
        public List<Point3D> GetPointsByAngle(float startAngle, float endAngle, float step)
        {
            List<Point3D> result = new List<Point3D>();
            float angle = startAngle;
            while (angle <= endAngle)
            {
                result.Add(CenterPoint + UnitVectorU * (float)(Math.Cos(angle) * RadiusA) + UnitVectorV * (float)(Math.Sin(angle) * RadiusB));
                angle += step;
            }
            return result;
        }

        /// <summary>
        /// 利用 数值近似法 求等曲线弧长的点集
        /// </summary>
        /// <param name="startAngle">（弧度为单位）</param>
        /// <param name="endAngle">（弧度为单位）</param>
        /// <param name="distance">等距离的长度</param>
        /// /// <param name="accur">求弧长积分的精度</param>
        /// <returns></returns>
        public List<Point3D> GetPointsByDistance(float startAngle, float endAngle, float distance, float accur = 0.001f)
        {
            List<Point3D> result = new List<Point3D>();
            //求定积分的精度
            float accuracy = distance / (2 * (float)Math.PI * RadiusA) * accur;
            //误差精度
            float toleranceAccuracy = distance * accur * 1.1f;
            int pointsCount = 0;
            double totalArea = 0.0d;
            double currentAngle = startAngle;
            Func<double, double> func = IntegralFormula;
            bool symbol = true;

            while (currentAngle < endAngle)
            {
                totalArea += func(currentAngle) * accuracy;
                double tolerance = (totalArea - pointsCount * distance) * (symbol ? 1 : -1);
                if ((tolerance < toleranceAccuracy) && (tolerance > 0))
                {
                    result.Add(CenterPoint + UnitVectorU * (float)(Math.Cos(currentAngle) * RadiusA) + UnitVectorV * (float)(Math.Sin(currentAngle) * RadiusB));
                    pointsCount++;
                    symbol = !symbol;
                }
                currentAngle += accuracy;
            }
            return result;
        }

        /// <summary>
        /// 被积函数
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private double IntegralFormula(double x)
        {
            return Math.Sqrt(Math.Pow(RadiusA, 2) * Math.Pow(Math.Sin(x), 2) + Math.Pow(RadiusB, 2) * Math.Pow(Math.Cos(x), 2));
        }
    }
}
