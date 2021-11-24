using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;
using WSX.Draw3D.Common;
using WSX.Draw3D.DrawTools;
using WSX.Draw3D.MathTools;
using WSX.GlobalData.Model;

namespace WSX.Draw3D.Utils
{
    /// <summary>
    /// 标准管数据转换，转为绘制图层使用的数据格式
    /// </summary>
    public class StandardTubeConverter
    {
        //private static readonly float segLength = 1.5f;// 椭圆取点，每段的长度
        //private static readonly float maxStep = (float)Math.PI / 50;//最大步长
        private static readonly float sectionDistance = 5;// 截面距离
        //private float radius;
        //private float leftAngle;
        //private float rightAngle;
        //private float aLeft;
        //private float aRight;
        //private float offsetShort;
        //private float offsetLLong;
        //private float offsetRLong;

        public StandardTubeMode TubeModel { get; }
        public StandardTubeConverter(StandardTubeMode tube)
        {
            this.TubeModel = tube;
        }
        /*
        private void Init()
        {
            radius = TubeModel.CircleRadius;
            leftAngle = (float)HitUtil.DegreesToRadians(TubeModel.LeftAngle);
            rightAngle = (float)HitUtil.DegreesToRadians(TubeModel.RightAngle);
            float cosLeft = (float)Math.Cos(leftAngle);
            float cosRight = (float)Math.Cos(rightAngle);
            aLeft = (float)(radius / cosLeft);
            aRight = (float)(radius / cosRight);
            offsetShort = (TubeModel.ShortSideLength - 2 * radius) / 2;
            offsetLLong = ((TubeModel.LongSideLength / cosLeft) - aLeft * 2) / 2;
            offsetRLong = ((TubeModel.LongSideLength / cosRight) - aRight * 2) / 2;
            if (TubeModel.ShortSideLength == 0) { offsetShort = 0; }
            if (TubeModel.LongSideLength == 0) { offsetLLong = offsetRLong = 0; }
        }

        /// <summary>
        /// 获取加工图形
        /// </summary>
        /// <returns></returns>
        public List<IDrawObject> GetDrawObjects()
        {
            float length = (float)EllipseHelper.GetEllipseLength(aLeft, radius);
            float step = (float)(2 * Math.PI / (length / segLength));
            if (step > maxStep) { step = maxStep; }
            //左截面加工数据
            var arc1Left = EllipseHelper.GetEllipsePoint(aLeft, radius, 0, (float)HitUtil.DegreesToRadians(90), step);
            var arc2Left = EllipseHelper.GetEllipsePoint(aLeft, radius, (float)HitUtil.DegreesToRadians(90), (float)HitUtil.DegreesToRadians(180), step);
            var arc3Left = EllipseHelper.GetEllipsePoint(aLeft, radius, (float)HitUtil.DegreesToRadians(180), (float)HitUtil.DegreesToRadians(270), step);
            var arc4Left = EllipseHelper.GetEllipsePoint(aLeft, radius, (float)HitUtil.DegreesToRadians(270), (float)HitUtil.DegreesToRadians(360), step);
            //平移
            Parallel.ForEach(arc1Left, p => p.Translate(offsetLLong, offsetShort, 0));
            Parallel.ForEach(arc2Left, p => p.Translate(-offsetLLong, offsetShort, 0));
            Parallel.ForEach(arc3Left, p => p.Translate(-offsetLLong, -offsetShort, 0));
            Parallel.ForEach(arc4Left, p => p.Translate(offsetLLong, -offsetShort, 0));

            Polyline3D polyLeft = new Polyline3D() { SN = 1, IsClosed = true, IsLineBold = true, LayerId = (int)LayerId.One };
            polyLeft.Points.AddRange(arc1Left);
            polyLeft.Points.AddRange(arc2Left);
            polyLeft.Points.AddRange(arc3Left);
            polyLeft.Points.AddRange(arc4Left);
            polyLeft.FigureUnit = FigureUnit.StartSection;
            polyLeft.IsInnerCut = false;
            //旋转
            Parallel.ForEach(polyLeft.Points, p => { p.Rotate(0, leftAngle, 0); });

            //右截面加工数据
            length = (float)EllipseHelper.GetEllipseLength(aRight, radius);
            step = (float)(2 * Math.PI / (length / segLength));
            if (step > maxStep) { step = maxStep; }
            var arc1Right = EllipseHelper.GetEllipsePoint(aRight, radius, 0, (float)HitUtil.DegreesToRadians(90), step);
            var arc2Right = EllipseHelper.GetEllipsePoint(aRight, radius, (float)HitUtil.DegreesToRadians(90), (float)HitUtil.DegreesToRadians(180), step);
            var arc3Right = EllipseHelper.GetEllipsePoint(aRight, radius, (float)HitUtil.DegreesToRadians(180), (float)HitUtil.DegreesToRadians(270), step);
            var arc4Right = EllipseHelper.GetEllipsePoint(aRight, radius, (float)HitUtil.DegreesToRadians(270), (float)HitUtil.DegreesToRadians(360), step);
            //平移
            Parallel.ForEach(arc1Right, p => p.Translate(offsetRLong, offsetShort, 0));
            Parallel.ForEach(arc2Right, p => p.Translate(-offsetRLong, offsetShort, 0));
            Parallel.ForEach(arc3Right, p => p.Translate(-offsetRLong, -offsetShort, 0));
            Parallel.ForEach(arc4Right, p => p.Translate(offsetRLong, -offsetShort, 0));

            Polyline3D polyRight = new Polyline3D() { SN = 2, IsClosed = true, IsLineBold = true, LayerId = (int)LayerId.One };
            polyRight.Points.AddRange(arc1Right);
            polyRight.Points.AddRange(arc2Right);
            polyRight.Points.AddRange(arc3Right);
            polyRight.Points.AddRange(arc4Right);
            polyRight.FigureUnit = FigureUnit.EndSection;
            polyRight.IsInnerCut = true;
            //旋转后z方向平移
            Parallel.ForEach(polyRight.Points, p =>
            {
                p.Rotate(0, rightAngle, 0);
                p.Translate(0, 0, TubeModel.TubeLength);
            });

            //返回加工数据
            var result = new List<IDrawObject>();
            //result.Add(polyLeft);
            //result.Add(polyRight);
            result.Add(new Line3D() { LayerId = (int)LayerId.One, P1 = arc4Left[arc4Left.Count - 1], P2 = arc1Left[0] });
            result.Add(new Line3D() { LayerId = (int)LayerId.One, P1 = arc1Left[arc1Left.Count - 1], P2 = arc2Left[0] });
            result.Add(new Line3D() { LayerId = (int)LayerId.One, P1 = arc2Left[arc2Left.Count - 1], P2 = arc3Left[0] });
            result.Add(new Line3D() { LayerId = (int)LayerId.One, P1 = arc3Left[arc3Left.Count - 1], P2 = arc4Left[0] });
            result.Add(new Line3D() { LayerId = (int)LayerId.One, P1 = arc4Right[arc4Right.Count - 1], P2 = arc1Right[0] });
            result.Add(new Line3D() { LayerId = (int)LayerId.One, P1 = arc1Right[arc1Right.Count - 1], P2 = arc2Right[0] });
            result.Add(new Line3D() { LayerId = (int)LayerId.One, P1 = arc2Right[arc2Right.Count - 1], P2 = arc3Right[0] });
            result.Add(new Line3D() { LayerId = (int)LayerId.One, P1 = arc3Right[arc3Right.Count - 1], P2 = arc4Right[0] });
            Parallel.ForEach(result, p => p.Update());

            #region test
            Point3D point1 = new Point3D(0, 0, 0);
            Point3D point2 = new Point3D(0, 0, this.TubeModel.TubeLength);
            float pii = (float)Math.PI;
            result.Add(new SquareCircle3D(
                this.TubeModel.CircleRadius,
                (float)HitUtil.DegreesToRadians(this.TubeModel.LeftAngle), point1,
                this.TubeModel.LongSideLength - this.TubeModel.CircleRadius * 2,
                this.TubeModel.ShortSideLength - this.TubeModel.CircleRadius * 2, true, false)
            { LayerId = (int)LayerId.One });
            result.Add(new SquareCircle3D(
               this.TubeModel.CircleRadius,
               (float)HitUtil.DegreesToRadians(this.TubeModel.LeftAngle), point1,
               this.TubeModel.LongSideLength - this.TubeModel.CircleRadius * 2,
               this.TubeModel.ShortSideLength - this.TubeModel.CircleRadius * 2, false, false)
            { LayerId = (int)LayerId.One });
            result.Add(new SquareCircle3D(
               this.TubeModel.CircleRadius,
              (float)HitUtil.DegreesToRadians(this.TubeModel.LeftAngle), point1,
               this.TubeModel.LongSideLength - this.TubeModel.CircleRadius * 2,
               this.TubeModel.ShortSideLength - this.TubeModel.CircleRadius * 2, false, true)
            { LayerId = (int)LayerId.One });
            result.Add(new SquareCircle3D(
               this.TubeModel.CircleRadius,
               (float)HitUtil.DegreesToRadians(this.TubeModel.LeftAngle), point1,
               this.TubeModel.LongSideLength - this.TubeModel.CircleRadius * 2,
               this.TubeModel.ShortSideLength - this.TubeModel.CircleRadius * 2, true, true)
            { LayerId = (int)LayerId.One });
            //右边

            result.Add(new SquareCircle3D(
                this.TubeModel.CircleRadius,
                (float)HitUtil.DegreesToRadians(this.TubeModel.RightAngle), point2,
                this.TubeModel.LongSideLength - this.TubeModel.CircleRadius * 2,
                this.TubeModel.ShortSideLength - this.TubeModel.CircleRadius * 2, true, false)
            { LayerId = (int)LayerId.One });
            result.Add(new SquareCircle3D(
               this.TubeModel.CircleRadius,
               (float)HitUtil.DegreesToRadians(this.TubeModel.RightAngle), point2,
               this.TubeModel.LongSideLength - this.TubeModel.CircleRadius * 2,
               this.TubeModel.ShortSideLength - this.TubeModel.CircleRadius * 2, false, false)
            { LayerId = (int)LayerId.One });
            result.Add(new SquareCircle3D(
               this.TubeModel.CircleRadius,
              (float)HitUtil.DegreesToRadians(this.TubeModel.RightAngle), point2,
               this.TubeModel.LongSideLength - this.TubeModel.CircleRadius * 2,
               this.TubeModel.ShortSideLength - this.TubeModel.CircleRadius * 2, false, true)
            { LayerId = (int)LayerId.One });
            result.Add(new SquareCircle3D(
               this.TubeModel.CircleRadius,
               (float)HitUtil.DegreesToRadians(this.TubeModel.RightAngle), point2,
               this.TubeModel.LongSideLength - this.TubeModel.CircleRadius * 2,
               this.TubeModel.ShortSideLength - this.TubeModel.CircleRadius * 2, true, true)
            { LayerId = (int)LayerId.One });
            //Console.WriteLine(ddd.GetBasicFuncValue(0.25f,3,3));
            //Console.WriteLine(ddd.GetBasicFuncValue(0.5f,3,3));
            //Console.WriteLine(ddd.GetBasicFuncValue(0.8f,2,3));
            //Console.WriteLine(ddd.GetBasicFuncValue(0.6f,4,3));
            #endregion


            return result;
        }
        /// <summary>
        /// 获取管长辅助线
        /// </summary>
        /// <returns></returns>
        public List<IDrawObject> GetMarkObjects()
        {
            var result = new List<IDrawObject>();
            float longSideLength = TubeModel.LongSideLength == 0 ? radius : TubeModel.LongSideLength / 2;
            float shortSideLength = TubeModel.ShortSideLength == 0 ? radius : TubeModel.ShortSideLength / 2;

            //第一象限的xy平面坐标
            float x1 = longSideLength;
            float y1 = shortSideLength - radius;
            float x2 = longSideLength - radius;
            float y2 = shortSideLength;

            float sideOffsetL = (float)Math.Tan(leftAngle) * x2;
            float radiusOffsetL = (float)Math.Tan(leftAngle) * x1;
            float sideOffsetR = (float)Math.Tan(rightAngle) * x2;
            float radiusOffsetR = (float)Math.Tan(rightAngle) * x1;

            DrawTools.Line3D line1 = new DrawTools.Line3D() { SN = 1, LayerId = (int)LayerId.Background };
            DrawTools.Line3D line2 = new DrawTools.Line3D() { SN = 2, LayerId = (int)LayerId.Background };
            DrawTools.Line3D line3 = new DrawTools.Line3D() { SN = 3, LayerId = (int)LayerId.Background };
            DrawTools.Line3D line4 = new DrawTools.Line3D() { SN = 4, LayerId = (int)LayerId.Background };
            DrawTools.Line3D line5 = new DrawTools.Line3D() { SN = 5, LayerId = (int)LayerId.Background };
            DrawTools.Line3D line6 = new DrawTools.Line3D() { SN = 6, LayerId = (int)LayerId.Background };
            DrawTools.Line3D line7 = new DrawTools.Line3D() { SN = 7, LayerId = (int)LayerId.Background };
            DrawTools.Line3D line8 = new DrawTools.Line3D() { SN = 8, LayerId = (int)LayerId.Background };

            line1.P1 = new Point3D(x1, y1, radiusOffsetL);
            line1.P2 = new Point3D(x1, y1, TubeModel.TubeLength + radiusOffsetR);

            line2.P1 = new Point3D(x2, y2, sideOffsetL);
            line2.P2 = new Point3D(x2, y2, TubeModel.TubeLength + sideOffsetR);

            line3.P1 = new Point3D(-x2, y2, -sideOffsetL);
            line3.P2 = new Point3D(-x2, y2, TubeModel.TubeLength - sideOffsetR);

            line4.P1 = new Point3D(-x1, y1, -radiusOffsetL);
            line4.P2 = new Point3D(-x1, y1, TubeModel.TubeLength - radiusOffsetR);

            line5.P1 = new Point3D(-x1, -y1, -radiusOffsetL);
            line5.P2 = new Point3D(-x1, -y1, TubeModel.TubeLength - radiusOffsetR);

            line6.P1 = new Point3D(-x2, -y2, -sideOffsetL);
            line6.P2 = new Point3D(-x2, -y2, TubeModel.TubeLength - sideOffsetR);

            line7.P1 = new Point3D(x2, -y2, sideOffsetL);
            line7.P2 = new Point3D(x2, -y2, TubeModel.TubeLength + sideOffsetR);

            line8.P1 = new Point3D(x1, -y1, radiusOffsetL);
            line8.P2 = new Point3D(x1, -y1, TubeModel.TubeLength + radiusOffsetR);

            if (TubeModel.TubeTypes == StandardTubeMode.TubeType.Circle)
            {
                result.Add(line1);
                result.Add(line5);
            }
            else if (TubeModel.TubeTypes == StandardTubeMode.TubeType.Sport)
            {
                result.Add(line2);
                result.Add(line3);
                result.Add(line6);
                result.Add(line7);
            }
            else
            {
                result.Add(line1);
                result.Add(line2);
                result.Add(line3);
                result.Add(line4);
                result.Add(line5);
                result.Add(line6);
                result.Add(line7);
                result.Add(line8);
            }

            return result;
        }
        */
        /// <summary>
        /// 获取标准管图形,返回元组《加工图形，背景图形》
        /// </summary>
        /// <returns></returns>
        public Tuple<List<IDrawObject>, List<IDrawObject>> GetTubeObjects()
        {
            List<IDrawObject> draws = new List<IDrawObject>();
            List<IDrawObject> marks = new List<IDrawObject>();

            float radius = TubeModel.CircleRadius;
            float leftAngle = (float)HitUtil.DegreesToRadians(TubeModel.LeftAngle);
            float rightAngle = (float)HitUtil.DegreesToRadians(TubeModel.RightAngle);
            float width = this.TubeModel.LongSideLength - this.TubeModel.CircleRadius * 2;
            float height = this.TubeModel.ShortSideLength - this.TubeModel.CircleRadius * 2;

            float longSideLength = TubeModel.LongSideLength == 0 ? radius : TubeModel.LongSideLength / 2;
            float shortSideLength = TubeModel.ShortSideLength == 0 ? radius : TubeModel.ShortSideLength / 2;

            //第一象限的xy平面坐标
            float x1 = longSideLength;
            float y1 = shortSideLength - radius;
            float x2 = longSideLength - radius;
            float y2 = shortSideLength;

            float sideOffsetL = (float)Math.Tan(leftAngle) * x2;
            float radiusOffsetL = (float)Math.Tan(leftAngle) * x1;
            float sideOffsetR = (float)Math.Tan(rightAngle) * x2;
            float radiusOffsetR = (float)Math.Tan(rightAngle) * x1;
            //背景层
            DrawTools.Line3D line1 = new DrawTools.Line3D() { SN = 1, LayerId = (int)LayerId.Background };
            DrawTools.Line3D line2 = new DrawTools.Line3D() { SN = 2, LayerId = (int)LayerId.Background };
            DrawTools.Line3D line3 = new DrawTools.Line3D() { SN = 3, LayerId = (int)LayerId.Background };
            DrawTools.Line3D line4 = new DrawTools.Line3D() { SN = 4, LayerId = (int)LayerId.Background };
            DrawTools.Line3D line5 = new DrawTools.Line3D() { SN = 5, LayerId = (int)LayerId.Background };
            DrawTools.Line3D line6 = new DrawTools.Line3D() { SN = 6, LayerId = (int)LayerId.Background };
            DrawTools.Line3D line7 = new DrawTools.Line3D() { SN = 7, LayerId = (int)LayerId.Background };
            DrawTools.Line3D line8 = new DrawTools.Line3D() { SN = 8, LayerId = (int)LayerId.Background };

            line1.P1 = new Point3D(x1, y1, radiusOffsetL);
            line1.P2 = new Point3D(x1, y1, TubeModel.TubeLength + radiusOffsetR);

            line2.P1 = new Point3D(x2, y2, sideOffsetL);
            line2.P2 = new Point3D(x2, y2, TubeModel.TubeLength + sideOffsetR);

            line3.P1 = new Point3D(-x2, y2, -sideOffsetL);
            line3.P2 = new Point3D(-x2, y2, TubeModel.TubeLength - sideOffsetR);

            line4.P1 = new Point3D(-x1, y1, -radiusOffsetL);
            line4.P2 = new Point3D(-x1, y1, TubeModel.TubeLength - radiusOffsetR);

            line5.P1 = new Point3D(-x1, -y1, -radiusOffsetL);
            line5.P2 = new Point3D(-x1, -y1, TubeModel.TubeLength - radiusOffsetR);

            line6.P1 = new Point3D(-x2, -y2, -sideOffsetL);
            line6.P2 = new Point3D(-x2, -y2, TubeModel.TubeLength - sideOffsetR);

            line7.P1 = new Point3D(x2, -y2, sideOffsetL);
            line7.P2 = new Point3D(x2, -y2, TubeModel.TubeLength + sideOffsetR);

            line8.P1 = new Point3D(x1, -y1, radiusOffsetL);
            line8.P2 = new Point3D(x1, -y1, TubeModel.TubeLength + radiusOffsetR);


            Point3D vector1 = new Point3D(0, 0, 0);
            Point3D vector2 = new Point3D(0, 0, this.TubeModel.TubeLength);
            switch (this.TubeModel.TubeTypes)
            {
                case StandardTubeMode.TubeType.Circle:
                    {
                        marks.Add(line1);
                        marks.Add(line5);
                        Ellipse3D left = new Ellipse3D(radius, leftAngle, vector1) { LayerId = (int)LayerId.One, SN = 1 };
                        Ellipse3D right = new Ellipse3D(radius, rightAngle, vector2) { LayerId = (int)LayerId.One, SN = 2 };
                        left.UpdatePolyline();
                        right.UpdatePolyline();

                        draws.Add(left);
                        draws.Add(right);

                    }
                    break;
                case StandardTubeMode.TubeType.Square:
                case StandardTubeMode.TubeType.Rectangle:
                    {
                        //背景
                        marks.AddRange(new List<IDrawObject> { line1, line2, line3, line4, line5, line6, line7, line8 });
                        //加工图
                        Line3D leftLine1 = new Line3D() { P1 = new Point3D(line2.P1), P2 = new Point3D(line3.P1) };
                        Line3D leftLine2 = new Line3D() { P1 = new Point3D(line4.P1), P2 = new Point3D(line5.P1) };
                        Line3D leftLine3 = new Line3D() { P1 = new Point3D(line6.P1), P2 = new Point3D(line7.P1) };
                        Line3D leftLine4 = new Line3D() { P1 = new Point3D(line8.P1), P2 = new Point3D(line1.P1) };

                        Line3D rightLine1 = new Line3D() { P1 = new Point3D(line2.P2), P2 = new Point3D(line3.P2) };
                        Line3D rightLine2 = new Line3D() { P1 = new Point3D(line4.P2), P2 = new Point3D(line5.P2) };
                        Line3D rightLine3 = new Line3D() { P1 = new Point3D(line6.P2), P2 = new Point3D(line7.P2) };
                        Line3D rightLine4 = new Line3D() { P1 = new Point3D(line8.P2), P2 = new Point3D(line1.P2) };

                        SquareCircle3D leftArc1 = new SquareCircle3D(radius, leftAngle, vector1, width, height, true, true);
                        SquareCircle3D leftArc2 = new SquareCircle3D(radius, leftAngle, vector1, width, height, false, true);
                        SquareCircle3D leftArc3 = new SquareCircle3D(radius, leftAngle, vector1, width, height, false, false);
                        SquareCircle3D leftArc4 = new SquareCircle3D(radius, leftAngle, vector1, width, height, true, false);
                        leftArc1.ControlPoints.Reverse();
                        leftArc3.ControlPoints.Reverse();
                        //右边
                        SquareCircle3D rightArc1 = new SquareCircle3D(radius, rightAngle, vector2, width, height, true, true);
                        SquareCircle3D rightArc2 = new SquareCircle3D(radius, rightAngle, vector2, width, height, false, true);
                        SquareCircle3D rightArc3 = new SquareCircle3D(radius, rightAngle, vector2, width, height, false, false);
                        SquareCircle3D rightArc4 = new SquareCircle3D(radius, rightAngle, vector2, width, height, true, false);
                        rightArc1.ControlPoints.Reverse();
                        rightArc3.ControlPoints.Reverse();

                        GeoCurve3D left = new GeoCurve3D() { LayerId = (int)LayerId.One, SN = 1 };
                        left.Geometry.Add(leftArc1);
                        left.Geometry.Add(leftLine1);
                        left.Geometry.Add(leftArc2);
                        left.Geometry.Add(leftLine2);
                        left.Geometry.Add(leftArc3);
                        left.Geometry.Add(leftLine3);
                        left.Geometry.Add(leftArc4);
                        left.Geometry.Add(leftLine4);
                        left.UpdatePolyline();

                        GeoCurve3D right = new GeoCurve3D() { LayerId = (int)LayerId.One, SN = 2 };
                        right.Geometry.Add(rightArc1);
                        right.Geometry.Add(rightLine1);
                        right.Geometry.Add(rightArc2);
                        right.Geometry.Add(rightLine2);
                        right.Geometry.Add(rightArc3);
                        right.Geometry.Add(rightLine3);
                        right.Geometry.Add(rightArc4);
                        right.Geometry.Add(rightLine4);
                        right.UpdatePolyline();

                        draws.Add(left);
                        draws.Add(right);
                    }
                    break;
                case StandardTubeMode.TubeType.Sport:
                    {
                        marks.Add(line2);
                        marks.Add(line3);
                        marks.Add(line6);
                        marks.Add(line7);
                        Line3D leftLine1 = new Line3D() { P1 = new Point3D(line2.P1), P2 = new Point3D(line3.P1) };
                        Line3D leftLine2 = new Line3D() { P1 = new Point3D(line6.P1), P2 = new Point3D(line7.P1) };
                        Line3D rightLine1 = new Line3D() { P1 = new Point3D(line2.P2), P2 = new Point3D(line3.P2) };
                        Line3D rightLine2 = new Line3D() { P1 = new Point3D(line6.P2), P2 = new Point3D(line7.P2) };

                        HalfCircle3D leftHalf1 = new HalfCircle3D(radius, leftAngle, vector1, width, true);
                        HalfCircle3D leftHalf2 = new HalfCircle3D(radius, leftAngle, vector1, width, false);
                        //右边
                        HalfCircle3D rightHalf1 = new HalfCircle3D(radius, rightAngle, vector2, width, true);
                        HalfCircle3D rightHalf2 = new HalfCircle3D(radius, rightAngle, vector2, width, false);

                        GeoCurve3D left = new GeoCurve3D() { LayerId = (int)LayerId.One, SN = 1 };
                        left.Geometry.Add(leftHalf1);
                        left.Geometry.Add(leftLine1);
                        left.Geometry.Add(leftHalf2);
                        left.Geometry.Add(leftLine2);
                        left.UpdatePolyline();

                        GeoCurve3D right = new GeoCurve3D() { LayerId = (int)LayerId.One, SN = 2 };
                        right.Geometry.Add(rightHalf1);
                        right.Geometry.Add(rightLine1);
                        right.Geometry.Add(rightHalf2);
                        right.Geometry.Add(rightLine2);
                        right.UpdatePolyline();

                        draws.Add(left);
                        draws.Add(right);
                    }
                    break;
                default:
                    break;
            }
            draws.ForEach(e => e.IsLineBold = true);
            return Tuple.Create(draws, marks);
        }
        ///// <summary>
        ///// 获取管截面数据
        ///// </summary>
        ///// <returns></returns>
        //public IDrawObject GetSectionObject()
        //{
        //    float length = (float)EllipseHelper.GetEllipseLength(radius, radius);
        //    float step = (float)(2 * Math.PI / (length / segLength));
        //    if (step > maxStep) { step = maxStep; }
        //    //截面数据
        //    var arc1 = EllipseHelper.GetEllipsePoint(radius, radius, 0, (float)HitUtil.DegreesToRadians(90), step);
        //    var arc2 = EllipseHelper.GetEllipsePoint(radius, radius, (float)HitUtil.DegreesToRadians(90), (float)HitUtil.DegreesToRadians(180), step);
        //    var arc3 = EllipseHelper.GetEllipsePoint(radius, radius, (float)HitUtil.DegreesToRadians(180), (float)HitUtil.DegreesToRadians(270), step);
        //    var arc4 = EllipseHelper.GetEllipsePoint(radius, radius, (float)HitUtil.DegreesToRadians(270), (float)HitUtil.DegreesToRadians(360), step);

        //    //平移
        //    float offsetX = (TubeModel.LongSideLength / 2 - radius);
        //    float offsetY = (TubeModel.ShortSideLength / 2 - radius);
        //    float biasLen = TubeModel.LongSideLength == 0 ? TubeModel.CircleRadius * 2 : TubeModel.LongSideLength;
        //    float offsetZ = -(float)(sectionDistance + biasLen * Math.Tan(leftAngle) / 2);
        //    if (TubeModel.ShortSideLength == 0) { offsetY = 0; }
        //    if (TubeModel.LongSideLength == 0) { offsetX = 0; }
        //    Parallel.ForEach(arc1, p => p.Translate(offsetX, offsetY, offsetZ));
        //    Parallel.ForEach(arc2, p => p.Translate(-offsetX, offsetY, offsetZ));
        //    Parallel.ForEach(arc3, p => p.Translate(-offsetX, -offsetY, offsetZ));
        //    Parallel.ForEach(arc4, p => p.Translate(offsetX, -offsetY, offsetZ));

        //    Polyline3D poly = new Polyline3D() { IsClosed = true };
        //    poly.Points.AddRange(arc1);
        //    poly.Points.AddRange(arc2);
        //    poly.Points.AddRange(arc3);
        //    poly.Points.AddRange(arc4);

        //    return poly;
        //}

        /// <summary>
        /// 获取管截面数据
        /// </summary>
        /// <returns></returns>
        public IDrawObject GetSectionObject()
        {
            float radius = TubeModel.CircleRadius;
            float leftAngle = (float)HitUtil.DegreesToRadians(TubeModel.LeftAngle);
            float rightAngle = (float)HitUtil.DegreesToRadians(TubeModel.RightAngle);
            float width = this.TubeModel.LongSideLength - this.TubeModel.CircleRadius * 2;
            float height = this.TubeModel.ShortSideLength - this.TubeModel.CircleRadius * 2;

            //截面偏移
            float biasLen = TubeModel.LongSideLength == 0 ? TubeModel.CircleRadius * 2 : TubeModel.LongSideLength;
            float offsetZ = -(float)(sectionDistance + biasLen * Math.Tan(leftAngle) / 2);

            float longSideLength = TubeModel.LongSideLength == 0 ? radius : TubeModel.LongSideLength / 2;
            float shortSideLength = TubeModel.ShortSideLength == 0 ? radius : TubeModel.ShortSideLength / 2;

            //第一象限的xy平面坐标
            float x1 = longSideLength;
            float y1 = shortSideLength - radius;
            float x2 = longSideLength - radius;
            float y2 = shortSideLength;

            float sideOffsetL = (float)Math.Tan(leftAngle) * x2;
            float radiusOffsetL = (float)Math.Tan(leftAngle) * x1;
            float sideOffsetR = (float)Math.Tan(rightAngle) * x2;
            float radiusOffsetR = (float)Math.Tan(rightAngle) * x1;

            Point3D vector1 = new Point3D(0, 0, offsetZ);
            switch (this.TubeModel.TubeTypes)
            {
                case StandardTubeMode.TubeType.Circle:
                    {
                        Ellipse3D ellipse = new Ellipse3D(radius, 0, vector1);
                        //ellipse.FigureUnit = FigureUnit.Section;
                        ellipse.UpdatePolyline();
                        return ellipse;
                    }
                case StandardTubeMode.TubeType.Square:
                case StandardTubeMode.TubeType.Rectangle:
                    {
                        Line3D leftLine1 = new Line3D() { P1 = new Point3D(x2, y2, offsetZ), P2 = new Point3D(-x2, y2, offsetZ) };
                        Line3D leftLine2 = new Line3D() { P1 = new Point3D(-x1, y1, offsetZ), P2 = new Point3D(-x1, -y1, offsetZ) };
                        Line3D leftLine3 = new Line3D() { P1 = new Point3D(-x2, -y2, offsetZ), P2 = new Point3D(x2, -y2, offsetZ) };
                        Line3D leftLine4 = new Line3D() { P1 = new Point3D(x1, -y1, offsetZ), P2 = new Point3D(x1, y1, offsetZ) };

                        SquareCircle3D leftArc1 = new SquareCircle3D(radius, 0, vector1, width, height, true, true);
                        SquareCircle3D leftArc2 = new SquareCircle3D(radius, 0, vector1, width, height, false, true);
                        SquareCircle3D leftArc3 = new SquareCircle3D(radius, 0, vector1, width, height, false, false);
                        SquareCircle3D leftArc4 = new SquareCircle3D(radius, 0, vector1, width, height, true, false);
                        leftArc1.ControlPoints.Reverse();
                        leftArc3.ControlPoints.Reverse();
                        GeoCurve3D geo = new GeoCurve3D() { LayerId = (int)LayerId.One, SN = 1 };
                        geo.Geometry.Add(leftArc1);
                        geo.Geometry.Add(leftLine1);
                        geo.Geometry.Add(leftArc2);
                        geo.Geometry.Add(leftLine2);
                        geo.Geometry.Add(leftArc3);
                        geo.Geometry.Add(leftLine3);
                        geo.Geometry.Add(leftArc4);
                        geo.Geometry.Add(leftLine4);
                        //geo.FigureUnit = FigureUnit.Section;
                        geo.UpdatePolyline();
                        return geo;
                    }
                case StandardTubeMode.TubeType.Sport:
                    {
                        Line3D leftLine1 = new Line3D() { P1 = new Point3D(x2, y2, offsetZ), P2 = new Point3D(-x2, y2, offsetZ) };
                        Line3D leftLine2 = new Line3D() { P1 = new Point3D(-x2, -y2, offsetZ), P2 = new Point3D(x2, -y2, offsetZ) };
                        HalfCircle3D leftHalf1 = new HalfCircle3D(radius, 0, vector1, width, true);
                        HalfCircle3D leftHalf2 = new HalfCircle3D(radius, 0, vector1, width, false);
                        GeoCurve3D geo = new GeoCurve3D() { LayerId = (int)LayerId.One, SN = 1 };
                        geo.Geometry.Add(leftHalf1);
                        geo.Geometry.Add(leftLine1);
                        geo.Geometry.Add(leftHalf2);
                        geo.Geometry.Add(leftLine2);
                        //geo.FigureUnit = FigureUnit.Section;
                        geo.UpdatePolyline();
                        return geo;
                    }
                default:
                    break;
            }
            return null;
        }
    }
}
