using System;
using System.Collections.Generic;
using System.Drawing;
using WSX.CommomModel.DrawModel;
using WSX.Draw3D.MathTools;

namespace WSX.Draw3D.Utils
{
    public class HitUtil
    {
        public static bool IsPointInLine(PointF p1, PointF p2, PointF testPoint, float halfLineWidth)
        {
            //range 判断的的误差，不需要误差则赋值0
            //点在线段首尾两端之外则return false

            double cross = (p2.X - p1.X) * (testPoint.X - p1.X) + (p2.Y - p1.Y) * (testPoint.Y - p1.Y);
            //检查是否命中端点
            if (CircleHitPoint(p1, halfLineWidth, testPoint)) return true;
            if (CircleHitPoint(p2, halfLineWidth, testPoint)) return true;
            //double cross = (p2.X - p1.X) * (testPoint.Y - p1.Y) - (testPoint.X - p1.X) * (p2.Y - p1.Y);
            if (cross < 0) return false;
            double d2 = (p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y);
            if (cross >= d2 + halfLineWidth / 2)
                return false;
            double r = cross / d2;
            double px = p1.X + (p2.X - p1.X) * r;
            double py = p1.Y + (p2.Y - p1.Y) * r;
            //判断距离是否小于误差
            double errorValue = Math.Sqrt((testPoint.X - px) * (testPoint.X - px) + (py - testPoint.Y) * (py - testPoint.Y));
            return errorValue <= halfLineWidth;
        }

        public static bool CircleHitPoint(PointF center, float radius, PointF hitPoint)
        {
            //检查所绑定的矩形，这样比创建一个新的矩形并调用其Contains()方法更快
            double leftPoint = center.X - radius;
            double rightPoint = center.X + radius;
            if (hitPoint.X < leftPoint || hitPoint.X > rightPoint) return false;
            double bottomPoint = center.Y - radius;
            double topPoint = center.Y + radius;
            if (hitPoint.Y < bottomPoint || hitPoint.Y > topPoint) return false;
            return true;
        }

        public static float Distance(PointF p1, PointF p2)
        {
            float offsetX = p2.X - p1.X;
            float offsetY = p2.Y - p1.Y;
            return (float)Math.Sqrt(offsetX * offsetX + offsetY * offsetY);
        }
        public static double Distance(Point3D p1, Point3D p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2) + Math.Pow(p2.Z - p1.Z, 2));
        }
        public static double RadiansToDegrees(double radians)
        {
            return radians * (180 / Math.PI);
        }

        public static double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
        public static double LineAngleR(Point3D p1, Point3D p2, double roundToAngleR)
        {
            if (p1.X == p2.X)
            {
                if (p1.Y > p2.Y)
                {
                    return Math.PI * 6 / 4;
                }
                if (p1.Y < p2.Y)
                {
                    return Math.PI / 2;
                }
                return 0;
            }
            double adjacent = p2.X - p1.X;
            double oppsite = p2.Y - p1.Y;
            double A = Math.Atan(oppsite / adjacent);
            if (adjacent < 0)//在第二第三象限
            {
                A += Math.PI;
            }
            if (adjacent > 0 && oppsite < 0)//第4象限
            {
                A += Math.PI * 2;
            }
            if (roundToAngleR != 0)
            {
                double roundUnit = Math.Round(A / roundToAngleR);
                A = roundToAngleR * roundUnit;
            }
            return A;
        }

        /// <summary>
        /// 求通过p1 p2的直线上的与p1的距离为d的点
        /// </summary>
        /// <param name="p1">p1点</param>
        /// <param name="p2">p2点</param>
        /// <param name="d">与p1点的距离</param>
        /// <param name="isInside">true 所求的点与p2点在p1点的同一侧，false 所求点与p2点在p1点的两侧</param>
        /// <returns></returns>
        public static Point3D GetLinePointByDistance(Point3D p1, Point3D p2, float d, bool isInside = true)
        {
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double dz = p2.Z - p1.Z;
            double r = Math.Sqrt((dx * dx + dy * dy + dz * dz));

            if (!isInside)
            {
                double x = p1.X - d * (dx / r);
                double y = p1.Y - d * (dy / r);
                double z = p1.Z - d * (dz / r);
                return new Point3D((float)x, (float)y, (float)z);
            }
            else
            {
                double x = p1.X + d * (dx / r);
                double y = p1.Y + d * (dy / r);
                double z = p1.Z + d * (dz / r);
                return new Point3D((float)x, (float)y, (float)z);
            }
        }

        /// <summary>
        /// 判断线段是否与矩形相交
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool LineIntersectWithRect(Point3D p1, Point3D p2, RectangleF r)
        {
            if (r.Contains(p1.Point))
                return true;
            if (r.Contains(p2.Point))
                return true;

            // the rectangle bottom is top in world units and top is bottom!, confused?
            // check left
            Point3D p3 = new Point3D(r.Left, r.Top);
            Point3D p4 = new Point3D(r.Left, r.Bottom);
            if (LinesIntersect(p1, p2, p3, p4))
                return true;
            // check bottom
            p4.Y = r.Top;
            p4.X = r.Right;
            if (LinesIntersect(p1, p2, p3, p4))
                return true;
            // check right
            p3.X = r.Right;
            p3.Y = r.Top;
            p4.X = r.Right;
            p4.Y = r.Bottom;
            if (LinesIntersect(p1, p2, p3, p4))
                return true;
            return false;
        }
        public static bool LinesIntersect(Point3D p1, Point3D p2, Point3D p3, Point3D p4)
        {
            double x = 0;
            double y = 0;
            return LinesIntersect(p1, p2, p3, p4, ref x, ref y, false, false, false);
        }

        /// <summary>
        /// 判断两条线段是否相交
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="returnpoint"></param>
        /// <param name="extendA"></param>
        /// <param name="extendB"></param>
        /// <returns></returns>
        private static bool LinesIntersect(Point3D p1, Point3D p2, Point3D p3, Point3D p4, ref double x, ref double y,
            bool returnpoint,
            bool extendA,
            bool extendB)
        {
            double x1 = p1.X;
            double x2 = p2.X;
            double x3 = p3.X;
            double x4 = p4.X;
            double y1 = p1.Y;
            double y2 = p2.Y;
            double y3 = p3.Y;
            double y4 = p4.Y;

            double denominator = ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));
            if (denominator == 0) // lines are parallel
                return false;
            double numerator_ua = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3));
            double numerator_ub = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3));
            double ua = numerator_ua / denominator;
            double ub = numerator_ub / denominator;
            // if a line is not extended then ua (or ub) must be between 0 and 1
            if (extendA == false)
            {
                if (ua < 0 || ua > 1)
                    return false;
            }
            if (extendB == false)
            {
                if (ub < 0 || ub > 1)
                    return false;
            }
            if (extendA || extendB) // no need to chck range of ua and ub if check is one on lines 
            {
                x = x1 + ua * (x2 - x1);
                y = y1 + ua * (y2 - y1);
                return true;
            }
            if (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1)
            {
                if (returnpoint)
                {
                    x = x1 + ua * (x2 - x1);
                    y = y1 + ua * (y2 - y1);
                }
                return true;
            }
            return false;
        }

        #region 新增平面方法
        /// <summary>
        /// 得到两点连成的直线中的距离p1为d的点坐标
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="d"></param>
        /// <param name="inside">线段内的点-ture,线段外的点-false</param>
        /// <returns></returns>
        public static PointF GetLinePointByDistance(PointF p1, PointF p2, float d, bool inside = true)
        {
            float dx = p1.X - p2.X;
            float dy = p1.Y - p2.Y;
            float r = (float)Math.Sqrt((dx * dx + dy * dy));

            if (!inside)
            {
                float x = p1.X - (d * (p2.X - p1.X)) / r;
                float y = p1.Y - (d * (p2.Y - p1.Y)) / r;

                return new PointF(x, y);
            }
            else
            {
                float x = (d * (p2.X - p1.X)) / r + p1.X;
                float y = (d * (p2.Y - p1.Y)) / r + p1.Y;

                return new PointF(x, y);
            }
        }

        /// <summary>
        /// p1,p2,为直线，过p1点的距离为d的垂线上的点的坐标。
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Tuple<PointF, PointF> GetLinePointByVerticalLine(PointF p1, PointF p2, float d)
        {
            //1、已知直线上两点求直线的一般式方程
            //已知直线上的两点P1(X1, Y1) P2(X2, Y2)， P1 P2两点不重合。则直线的一般式方程AX + BY + C = 0中，A B C分别等于：
            //A = Y2 - Y1
            //B = X1 - X2
            //C = X2 * Y1 - X1 * Y2
            //2、过直线外一点P0(x0, y0）的垂线方程：y = (B / A) * (x - x0) + y0=> y =(B / A) * x - x0 * (B / A) + y0
            //3、求直线与垂线的交点
            //x = ((B ^ 2) * x0 - A * B * y0 - A * C) / (A ^ 2 + B ^ 2)
            //y = -(A * x + C) / B

            var lx = HitUtil.GetLineEquation(p1, p2);
            float A = lx.Item1;
            float B = lx.Item2;
            float C = lx.Item3;
            if (A == 0)
            {
                float y1 = (Math.Abs(p1.Y) + d) * (p1.Y >= 0 ? 1 : -1);//(Math.Abs(p1.Y) / p1.Y);
                float y2 = (Math.Abs(p1.Y) - d) * (p1.Y >= 0 ? 1 : -1);//(Math.Abs(p1.Y) / p1.Y);
                var point1 = new PointF(p1.X, y1);
                var point2 = new PointF(p1.X, y2);
                if (HitUtil.IsClockwiseByCross(p2, p1, point1))
                {
                    return Tuple.Create(point1, point2);
                }
                else
                {
                    return Tuple.Create(point2, point1);
                }
            }
            // -(B / A) * x + y + x0 * (B / A) - y0 =0
            float x0 = p1.X;
            float y0 = p1.Y;
            float la = -(B / A);
            float lb = 1;
            float lc = x0 * (B / A) - y0;

            //double 
            float c1 = C + d * (float)Math.Sqrt(A * A + B * B);
            float c2 = C - d * (float)Math.Sqrt(A * A + B * B);

            PointF up1 = HitUtil.GetIntersectionPointBy2Line(A, B, c1, la, lb, lc);
            PointF up2 = HitUtil.GetIntersectionPointBy2Line(A, B, c2, la, lb, lc);

            return Tuple.Create(up1, up2);
        }

        public static bool IsClockwiseByCross(PointF startPoint, PointF midPoint, PointF endPoint)
        {
            float result = (midPoint.X - startPoint.X) * (endPoint.Y - midPoint.Y) - (midPoint.Y - startPoint.Y) * (endPoint.X - midPoint.X);
            return result < 0;//顺时针方向
        }

        /// <summary>
        /// 获的两条直线的交点
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="b1"></param>
        /// <param name="c1"></param>
        /// <param name="a2"></param>
        /// <param name="b2"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static PointF GetIntersectionPointBy2Line(float a1, float b1, float c1, float a2, float b2, float c2)
        {
            float d = (a1 * b2 - a2 * b1);
            if (Math.Abs(d) < 0.000001)
            {
                return new PointF(float.NaN, float.NaN);
            }
            float x = (b1 * c2 - b2 * c1) / d;
            float y = (a2 * c1 - a1 * c2) / d;
            return new PointF(x, y);
        }

        /// <summary>
        ///求直线方程的一般方程系数AX+BY+C=0，a,b,c.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>AX+BY+C=0的A,B,C系数</returns>
        public static Tuple<float, float, float> GetLineEquation(PointF p1, PointF p2)
        {
            //求解过程：
            //已知直线上的两点P1(X1, Y1) P2(X2, Y2)， P1 P2两点不重合。
            //对于AX+BY+C=0：
            //当x1=x2时，直线方程为x-x1=0
            //当y1=y2时，直线方程为y-y1=0
            //当x1≠x2，y1≠y2时，直线的斜率k=(y2-y1)/(x2-x1)
            //故直线方程为y-y1=(y2-y1)/(x2-x1)×(x-x1)
            //即x2y-x1y-x2y1+x1y1=(y2-y1)x-x1(y2-y1)
            //即(y2-y1)x-(x2-x1)y-x1(y2-y1)+(x2-x1)y1=0
            //即(y2-y1)x+(x1-x2)y+x2y1-x1y2=0 ①
            //可以发现，当x1=x2或y1=y2时，①式仍然成立。所以直线AX+BY+C=0的一般式方程就是：
            //A = Y2 - Y1
            //B = X1 - X2
            //C = X2* Y1 - X1* Y2
            float a = 0, b = 0, c = 0;
            a = p2.Y - p1.Y;
            b = p1.X - p2.X;
            c = p2.X * p1.Y - p1.X * p2.Y;
            return Tuple.Create(a, b, c);
        }
        #endregion

        public static bool ObjectInRectangle(float[] matrix, RectangleF rectangle, List<Point3D> points)
        {
            List<float[]> pfloats = new List<float[]>();
            points.ForEach(p =>
            {
                var vertex = MatrixHelper.Multi4x4with4x1(matrix, new float[] { p.X, p.Y, p.Z, 1 });
                pfloats.Add(vertex);
            });
            Rectangle rect = HitUtil.GetBoundRect(pfloats);
            return rectangle.Contains(rect);
        }

        private static Rectangle GetBoundRect(List<float[]> points)
        {
            float minX = points[0][0];
            float minY = points[0][1];
            float maxX = points[0][0];
            float maxY = points[0][1];
            for (int i = 1; i < points.Count; i++)
            {
                if (minX > points[i][0]) minX = points[i][0];
                if (minY > points[i][1]) minY = points[i][1];
                if (maxX < points[i][0]) maxX = points[i][0];
                if (maxY < points[i][1]) maxY = points[i][1];
            }
            return new Rectangle((int)minX, (int)minY, (int)(maxX - minX), (int)(maxY - minY));
        }
        public static bool PointInObject(float[] matrix, float zoom, PointF point,  List<Point3D> points, bool isClosed = false)
        {
            List<PointF> vpoints = new List<PointF>();
            List<List<PointF>> vLinePoints = new List<List<PointF>>();
            vLinePoints.Add(vpoints);
            points.ForEach(p =>
            {
                float[] v1 = MatrixHelper.Multi4x4with4x1(matrix, new float[] { p.X, p.Y, p.Z, 1 });
                vpoints.Add(new PointF(v1[0], v1[1]));

                if (p.HasMicroConn)
                {
                    vpoints = new List<PointF>();
                    vLinePoints.Add(vpoints);
                }
            });

            //最后一个vpoints的最后一个点不是微连的起点且为封闭图形，结尾加上起点
            if (!points[points.Count-1].HasMicroConn && isClosed)
            {
                float[] v1 = MatrixHelper.Multi4x4with4x1(matrix, new float[] { points[0].X, points[0].Y, points[0].Z, 1 });
                vpoints.Add(new PointF(v1[0], v1[1]));
            }

            float thresholdWidth = 1.5f;//this.zoom;
            foreach(List<PointF> linePoints in vLinePoints)
            {
                for (int i = 0; i < linePoints.Count - 1; i++)
                {
                    if (HitUtil.IsPointInLine(linePoints[i], linePoints[i + 1], point, thresholdWidth))
                    {
                        return true;
                    }
                }
            }
           
            return false;
        }

        /// <summary>
        /// 求垂足的坐标
        /// </summary>
        /// <param name="p1">直线上的点</param>
        /// <param name="p2">直线上的点</param>
        /// <param name="testPoint"></param>
        /// <returns></returns>
        public static PointF CalFootPoint(PointF p1,PointF p2, PointF testPoint)
        {
            Tuple<float, float, float> equaion = HitUtil.GetLineEquation(p1, p2);

            float A = equaion.Item1, B = equaion.Item2, C = equaion.Item3;

            PointF footPoint = PointF.Empty;
            if (Math.Abs(A * testPoint.X + B * testPoint.Y + C) < 1e-13)
            {
                footPoint = new PointF(testPoint.X, testPoint.Y);
            }
            else
            {
                float x = (B * B * testPoint.X - A * B * testPoint.Y - A * C) / (A * A + B * B);
                float y = (-A * B * testPoint.X + A * A * testPoint.Y - B * C) / (A * A + B * B);

                footPoint = new PointF(x,y);
            }

            return footPoint;
        }

        public static float CalPolylinePosition(float[] matrix, PointF point, List<Point3D> points, bool isClosed = false)
        {
            float position = float.NaN;

            List<PointF> vpoints = new List<PointF>();
            points.ForEach(p =>
            {
                float[] v1 = MatrixHelper.Multi4x4with4x1(matrix, new float[] { p.X, p.Y, p.Z, 1 });
                vpoints.Add(new PointF(v1[0], v1[1]));
            });

            int idx = -1, idxEnd = -1;
            float thresholdWidth = 1.5f;//this.zoom;
            for (int i = 0; i < vpoints.Count - 1; i++)
            {
                if (HitUtil.IsPointInLine(vpoints[i], vpoints[i + 1], point, thresholdWidth))
                {
                    idx = i;
                    idxEnd = i + 1;
                    break;
                }
            }
            if (idx < 0 && isClosed)
            {
                if(HitUtil.IsPointInLine(vpoints[vpoints.Count - 1], vpoints[0], point, thresholdWidth))
                {
                    idx = vpoints.Count - 1;
                    idxEnd = 0;
                }
            }

            if (idx > 0)
            {
                double testStart = HitUtil.Distance(point, vpoints[idx]);
                double testEnd = HitUtil.Distance(point, vpoints[idxEnd]);
                double startEnd = HitUtil.Distance(vpoints[idx], vpoints[idxEnd]);

                double ratio = testStart / startEnd;
                ratio = (testStart - (testStart + testEnd - startEnd) * ratio) / startEnd;
                ratio = ratio < 0.0f ? 0.0f : ratio;
                ratio = ratio > 1.0f ? 1.0f : ratio;

                position = (float)(idx + ratio);
            }

            return position;
        }
    }
}