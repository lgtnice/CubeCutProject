using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;
using WSX.Draw3D.DrawTools;
using WSX.GlobalData.Model;

namespace WSX.Draw3D.MathTools
{
    public class EllipseHelper
    {
        /// <summary>
        /// 获取椭圆的周长
        /// </summary>
        /// <param name="a">长轴</param>
        /// <param name="b">短轴</param>
        /// <returns></returns>
        public static double GetEllipseLength(float a, float b)
        {
            //L=2πb+4(a-b)
            return 2 * Math.PI * b + 4 * (a - b);
        }
        /// <summary>
        /// 基于xy平面,椭圆（圆）取点
        /// </summary>
        /// <param name="a">长轴</param>
        /// <param name="b">短轴</param>
        /// <param name="startAngle">开始弧度</param>
        /// <param name="endAngle">结束弧度</param>
        /// <param name="step">取点步长</param>
        /// <returns></returns>
        public static List<Point3D> GetEllipsePoint(float a, float b, float startAngle, float endAngle, float step = 0.01f)
        {
            List<Point3D> result = new List<Point3D>();
            double pi = Math.PI;
            double pi12 = pi / 2;
            double pi23 = pi * 1.5;
            double pi2 = pi * 2;
            for (float angle = startAngle; angle < endAngle; angle += step)
            {
                float tanTheta = (float)Math.Tan(angle);
                float e = b * b + a * a * tanTheta * tanTheta;
                float x = (float)(a * b / Math.Sqrt(e));
                float y = (float)(a * b * tanTheta / Math.Sqrt(e));
                if (0 <= angle && angle < pi12 || pi23 < angle && angle < pi2)
                {
                    if (Math.Abs(angle - pi12) < step)
                    {
                        result.Add(new Point3D(0, b, 0));
                    }
                    else
                    {
                        result.Add(new Point3D(x, y, 0));
                    }
                }
                else if (pi12 < angle && angle < pi23)
                {
                    if (Math.Abs(angle - pi23) < step)
                    {
                        result.Add(new Point3D(0, -b, 0));
                    }
                    else
                    {
                        result.Add(new Point3D(-x, -y, 0));
                    }
                }
            }
            return result;
        }

    }
}
