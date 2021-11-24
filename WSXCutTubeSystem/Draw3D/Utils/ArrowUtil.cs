using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using WSX.CommomModel.DrawModel;
using WSX.Draw3D.DrawTools;
using WSX.Draw3D.MathTools;
using WSX.Draw3D.Utils;
using WSX.GlobalData.Model;

namespace WSX.Draw3D.Utils
{
    public class ArrowUtil
    {
        public static void DrawArrow(Point3D p1, Point3D p2, float[] modelMatrix, float[] color, OpenGL gl)
        {
            float width = 10.0f;
            float height = 12.4f;
            Tuple<PointF, PointF, PointF> array = ArrowUtil.GetArrowPoint(p1, p2, modelMatrix, width, height);

            if (float.IsNaN(array.Item2.X) || float.IsNaN(array.Item2.Y) || float.IsNaN(array.Item3.X) || float.IsNaN(array.Item3.Y))
                return;

            //二维坐标系画直线(箭头的两个翅膀)
            XorGDI.DrawLine(gl, color, array.Item1.X, array.Item1.Y, array.Item2.X, array.Item2.Y);
            XorGDI.DrawLine(gl, color, array.Item1.X, array.Item1.Y, array.Item3.X, array.Item3.Y);
        }

        private static Tuple<PointF, PointF, PointF> GetArrowPoint(Point3D p1, Point3D p2, float[] modelMatrix, float width, float height)
        {
            float[] result = { 0, 0, 0, 0 };

            //分别求出P1 P2在以屏幕中心为原点的平面坐标系中的坐标点 centerP1 centerP2
            float[] vertexPoint = { p1.X, p1.Y, p1.Z, 1 };
            result = MatrixHelper.Multi4x4with4x1(modelMatrix, vertexPoint);
            PointF centerP1 = new PointF(result[0], result[1]);

            vertexPoint = new float[] { p2.X, p2.Y, p2.Z, 1 };
            result = MatrixHelper.Multi4x4with4x1(modelMatrix, vertexPoint);
            PointF centerP2 = new PointF(result[0], result[1]);

            //求出与centerP2的距离为width的点centerP3
            PointF centerP3 = HitUtil.GetLinePointByDistance(centerP2, centerP1, width, true);

            //求出过centerP3点且垂直于过centerP3 centerP2的直线的两个点
            Tuple<PointF, PointF> verticalPoints = HitUtil.GetLinePointByVerticalLine(centerP3, centerP2, height / 2);

            Tuple<PointF, PointF, PointF> ret = new Tuple<PointF, PointF, PointF>(centerP2, verticalPoints.Item1, verticalPoints.Item2);

            return ret;
        }

        /// <summary>
        /// 计算多段线 显示方向(箭头)的位置
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<Tuple<Point3D,Point3D>> CalShowArrowsPoints(Polyline3D polyline3D)
        {
            List<Tuple<Point3D, Point3D>> ret = new List<Tuple<Point3D, Point3D>>();
            //图形缩小到一定程度就不显示箭头了(不在计算)
            if (polyline3D.SizeLength * GlobalModel.Zoom < 100 || polyline3D.Points == null || polyline3D.Points.Count < 2)
                return ret;

            //还是缩小比较多，只显示起点处(不在计算)
            if (polyline3D.SizeLength * GlobalModel.Zoom < 150)
            {
                ret.Add(new Tuple<Point3D, Point3D>(polyline3D.Points[0], polyline3D.Points[1]));
                return ret;
            }

            //按原始线段显示
            //for (int i = 1; i < polyline3D.Points.Count; i++)
            //{
            //    ret.Add(new Tuple<Point3D, Point3D>(polyline3D.Points[i - 1], polyline3D.Points[i]));
            //}
            //return ret;

            //ret.Add(new Tuple<Point3D, Point3D>(polyline3D.Points[0], polyline3D.Points[1]));
            //根据距离计算
            double size = 0.0f, sum = 0.0f, spaceFirst=20, spaceCommon = 50;//箭头间隔第一个10 其他space 50
            Point3D prePoint = polyline3D.Points[0];
            Point3D current = null;
            int close = polyline3D.IsClosed ? 1 : 0;

            double space = GetSpace(ret.Count, polyline3D.SizeLength, spaceFirst, spaceCommon);
            for (int i=1;i<polyline3D.Points.Count + close; i++)
            {
                if (i == polyline3D.Points.Count)
                {
                    current = polyline3D.Points[0];//封闭图形 考虑最后一个点到起始点的线段
                }
                else
                {
                    current = polyline3D.Points[i];
                }
                size = HitUtil.Distance(polyline3D.Points[i-1], current);
                sum += size;
                if(sum >= space-1 && sum <= space + 1)
                {
                    //间隔误差在1以内就不分割了
                    sum = 0;
                    ret.Add(new Tuple<Point3D, Point3D>(polyline3D.Points[i - 1], current));
                    space = GetSpace(ret.Count, polyline3D.SizeLength, spaceFirst, spaceCommon);
                }
                else if (sum > space + 1)
                {
                    double surplus = sum - space;
                    Point3D endPoint = HitUtil.GetLinePointByDistance(current, polyline3D.Points[i-1], (float)surplus);
                    ret.Add(new Tuple<Point3D, Point3D>(polyline3D.Points[i - 1], endPoint));
                    space = GetSpace(ret.Count, polyline3D.SizeLength, spaceFirst, spaceCommon);

                    prePoint = endPoint;
                    while (surplus > space)
                    {
                        endPoint = HitUtil.GetLinePointByDistance(prePoint, current, (float)space);
                        ret.Add(new Tuple<Point3D, Point3D>(prePoint, endPoint));
                        surplus -= space;
                        prePoint = endPoint;
                        space = GetSpace(ret.Count, polyline3D.SizeLength, spaceFirst, spaceCommon);
                    }

                    sum = surplus;
                }
            }
            return ret;
        }

        private static double GetSpace(int arrowCount, double figureSize, double firstSpace, double commonSpace)
        {
            if(arrowCount == 0)
            {
                return firstSpace;
            }

            double surplus = figureSize - (firstSpace + (arrowCount - 1) * commonSpace);
            if (surplus > (commonSpace - firstSpace) && surplus < commonSpace + (commonSpace - firstSpace))
            {
                return (surplus + firstSpace) / 2.0d;
            }
            return commonSpace;
        }
    }
}