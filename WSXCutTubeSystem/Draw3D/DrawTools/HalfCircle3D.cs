using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;

namespace WSX.Draw3D.DrawTools
{
    public class HalfCircle3D : Spline3D
    {
        /// <summary>
        /// 长轴
        /// </summary>
        public float RadiusA { get; set; } = 0.0f;
        /// <summary>
        /// 短轴
        /// </summary>
        public float RadiusB { get; set; } = 0.0f;
        /// <summary>
        /// 
        /// </summary>
        public Point3D CenterPoint { get; set; } = new Point3D();
        /// <summary>
        /// 长轴方向单位向量
        /// </summary>
        public Point3D UnitVectorU { get; set; } = new Point3D();
        /// <summary>
        /// 短轴方向单位向量
        /// </summary>
        public Point3D UnitVectorV { get; set; } = new Point3D();

        private HalfCircle3D()
        {

        }

        public HalfCircle3D(float radius, float angle, Point3D translateDistance, float rectangleWidth,bool rightOrLeft)
        {
            float LongSide = (float)(radius / Math.Cos(angle));
            float ShortSide = radius;
            Type = FigureType.Spline;
            IsClosed = false;
            float K = 2 - (float)Math.Sqrt(2);
            float weight = (float)((2 * Math.Sqrt(2) - 3) / (21 - 15 * Math.Sqrt(2)));

            //float K = (float)((2 * Math.Sqrt(2) - 2.5d) / 1.5d);
            //float weight = 0.5f;

            Point3D unitVectorLong = new Point3D((float)Math.Cos(angle), 0, (float)Math.Sin(angle));
            Point3D unitVectorShort = new Point3D(0, 1, 0);
            Knots.AddRange(new float[] { 0, 0, 0, 0, 0.5f, 0.5f, 0.5f, 1, 1, 1, 1 });

            //长轴上的位移
            Point3D lMove = new Point3D();
            //短轴上的位移
            Point3D sMove = new Point3D();
            //管长平移量
            Point3D translateMove = translateDistance;
            //因为矩形而产生的位移
            Point3D recMove = unitVectorLong * (rectangleWidth / 2.0f / (float)Math.Cos(angle));

            lMove = new Point3D();
            sMove = unitVectorShort * -1.0f * ShortSide;
            ControlPoints.Add((lMove + sMove + recMove) * (rightOrLeft ? 1.0f : -1.0f) + translateMove);

            lMove = unitVectorLong * (K * LongSide);
            sMove = unitVectorShort * -1.0f * ShortSide;
            ControlPoints.Add((lMove + sMove + recMove) * (rightOrLeft ? 1.0f : -1.0f) + translateMove);

            lMove = unitVectorLong * LongSide;
            sMove = unitVectorShort * -1.0f * K * ShortSide;
            ControlPoints.Add((lMove + sMove + recMove) * (rightOrLeft ? 1.0f : -1.0f) + translateMove);

            lMove = unitVectorLong * LongSide;
            sMove = new Point3D();
            ControlPoints.Add((lMove + sMove + recMove) * (rightOrLeft ? 1.0f : -1.0f) + translateMove);

            lMove = unitVectorLong * LongSide;
            sMove = unitVectorShort * K * ShortSide;
            ControlPoints.Add((lMove + sMove + recMove) * (rightOrLeft ? 1.0f : -1.0f) + translateMove);

            lMove = unitVectorLong * (K * LongSide);
            sMove = unitVectorShort * ShortSide;
            ControlPoints.Add((lMove + sMove + recMove) * (rightOrLeft ? 1.0f : -1.0f) + translateMove);

            lMove = new Point3D();
            sMove = unitVectorShort * ShortSide;
            ControlPoints.Add((lMove + sMove + recMove) * (rightOrLeft ? 1.0f : -1.0f) + translateMove);

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

    }
}
