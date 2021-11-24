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
    public class Spline3D : Polyline3D//DrawObjectBase
    {
        private IntPtr nurbsRenderer = IntPtr.Zero;
        private int degree = 3;
        private bool isPeriodic = false;

        /// <summary>
        /// 结点
        /// </summary>
        public List<float> Knots { get; set; } = new List<float>();
        /// <summary>
        /// 控制点
        /// </summary>
        public List<Point3D> ControlPoints { get; set; } = new List<Point3D>();
        /// <summary>
        /// 是否考虑权值
        /// </summary>
        public bool HasWeight { get; set; } = true;

        public Spline3D()
        {
            Type = FigureType.Spline;
        }
        /*
        public override void Draw(OpenGL gl, float[] color)
        {
            if (nurbsRenderer == IntPtr.Zero) { nurbsRenderer = gl.NewNurbsRenderer(); }
            gl.PushMatrix();
            if (IsSelected)
            {
                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                gl.LineStipple(1, 0x739C);
            }
            gl.LineWidth(IsSelected || !IsLineBold ? 1 : 2);
            gl.Color(color);
            gl.BeginCurve(nurbsRenderer);
            gl.NurbsCurve(nurbsRenderer, Knots.Count, Knots.ToArray(), HasWeight ? 4 : 3, ControlPointsToFloatArray(), Knots.Count - ControlPoints.Count, OpenGL.GL_MAP1_VERTEX_4);
            gl.EndCurve(nurbsRenderer);
            gl.Disable(OpenGL.GL_LINE_STIPPLE);
            gl.PopMatrix();

            //gl.LineWidth(1);
            //List<Point3D> zz = GetPointsByDistance(0, (float)Math.PI * 2, (float)Math.PI * 2 * RadiusA * (1.0f / 8.0f));
            //for (int i = 0; i < zz.Count; i++)
            //{
            //    gl.Begin(OpenGL.GL_LINES);
            //    gl.Vertex(0, 0, 0);
            //    gl.Vertex(zz[i].X, zz[i].Y, zz[i].Z);
            //    gl.End();
            //}
            //Point3D zzz = CenterPoint + UnitVectorU * RadiusA * (float)Math.Cos(Math.PI / 4.0f) + UnitVectorV * RadiusB * (float)Math.Sin(Math.PI / 4.0f);
            //gl.Begin(OpenGL.GL_LINES);
            //gl.Vertex(0, 0, 0);
            //gl.Vertex(zzz.X, zzz.Y, zzz.Z);
            //gl.End();
        }*/

        public void UpdatePolyline()
        {
            int precision = 100;
            int count = (int)(Knots.Count * precision);
            count = count >= 10000 ? 10000 : count;
            IEnumerable<Point3D> vertexes = this.PolygonalVertexes(precision);
            this.Points.Clear();
            this.Points.AddRange(vertexes);
        }
        public float[] ControlPointsToFloatArray()
        {

            float[] floats = new float[ControlPoints.Count * (HasWeight ? 4 : 3)];
            if (HasWeight)
            {
                int index = 0;
                foreach (Point3D pt in ControlPoints)
                {
                    floats[index++] = pt.X * pt.Weight;
                    floats[index++] = pt.Y * pt.Weight;
                    floats[index++] = pt.Z * pt.Weight;
                    floats[index++] = pt.Weight;
                }
            }
            else
            {
                int index = 0;
                foreach (Point3D pt in ControlPoints)
                {
                    floats[index++] = pt.X;
                    floats[index++] = pt.Y;
                    floats[index++] = pt.Z;
                }
            }
            return floats;
        }
        /* public override void ShowMachinePath(float[] matrix, OpenGL gl, float[] color)
         {

         }
         public override bool ObjectInRectangle(float[] matrix, RectangleF rect, bool anyPoint)
         {

             return false;
         }

         public override bool PointInObject(float[] matrix, PointF point)
         {
             List<Point3D> list = GetPointsByKnots(0.0f, 1.0f, 0.001f);
             return HitUtil.PointInObject(matrix, 0, point, list, this.IsClosed);
         }
         */
        public override IDrawObject Clone()
        {
            Spline3D newObj = new Spline3D();
            newObj.Copy(this);
            return newObj;
        }

        public override void Copy(IDrawObject source)
        {
            var data = source as Spline3D;
            base.Copy(data);
            this.ControlPoints = CopyUtil.DeepCopy(data.ControlPoints);
            this.Knots = CopyUtil.DeepCopy(data.Knots);
            this.HasWeight = data.HasWeight;
            //this.Update();
        }

        public override void MoveAxisZ(float offset)
        {
            base.MoveAxisZ(offset);
            this.ControlPoints.ForEach(c => c.Z += offset);
        }

        /// <summary>
        /// 根据u的值求出对应基函数的值
        /// </summary>
        /// <param name="u"></param>
        /// <param name="i">基函数序号</param>
        /// <param name="m">次数</param>
        /// <returns></returns>
        public float GetBasicFuncValue(float u, int i, int m)
        {
            float result = 0.0f;
            float[] knots = Knots.ToArray();

            if (u < knots[0] || u > knots[knots.Length - 1]) { return 0; }
            if (i < 0 || m < 0 || ((i + m) >= (knots.Length - 1))) { return 0; }

            if (m == 0)
            {
                if (u >= knots[i] && u < knots[i + 1]) { return 1; }
                else { return 0; }
            }
            if (Math.Abs(knots[i + m] - knots[i]) > 0.0001f)
            {
                result += (u - knots[i]) * GetBasicFuncValue(u, i, m - 1) / (knots[i + m] - knots[i]);
            }
            if (Math.Abs(knots[i + m + 1] - knots[i + 1]) > 0.0001f)
            {
                result += (knots[i + m + 1] - u) * GetBasicFuncValue(u, i + 1, m - 1) / (knots[i + m + 1] - knots[i + 1]);
            }

            return result;
        }

        public Point3D GetFuncValue(float u)
        {
            int m = Knots.Count - ControlPoints.Count - 1;
            Point3D result = new Point3D();
            if (HasWeight)
            {
                double numerator_x = 0.0d;
                double denominator = 0.0d;
                double numerator_y = 0.0d;
                double numerator_z = 0.0d;
                for (int i = 0; i < ControlPoints.Count; i++)
                {
                    float temp = GetBasicFuncValue(u, i, m);
                    numerator_x += temp * ControlPoints[i].Weight * ControlPoints[i].X;
                    numerator_y += temp * ControlPoints[i].Weight * ControlPoints[i].Y;
                    numerator_z += temp * ControlPoints[i].Weight * ControlPoints[i].Z;
                    denominator += temp * ControlPoints[i].Weight;
                }
                result.X = (float)(numerator_x / denominator);
                result.Y = (float)(numerator_y / denominator);
                result.Z = (float)(numerator_z / denominator);
            }
            else
            {
                double x = 0.0d;
                double y = 0.0d;
                double z = 0.0d;
                for (int i = 0; i < ControlPoints.Count; i++)
                {
                    float temp = GetBasicFuncValue(u, i, m);
                    x += temp * ControlPoints[i].X;
                    y += temp * ControlPoints[i].Y;
                    z += temp * ControlPoints[i].Z;
                }
                result.X = (float)x;
                result.Y = (float)y;
                result.Z = (float)z;
            }
            return result;
        }

        #region 取点方法
        /// <summary>
        /// 通过节点knot的等差 递增，来求对应的点的集合
        /// </summary>
        /// <param name="startDistanceRate">[0,1]之间的数</param>
        /// <param name="endDistanceRate">[0,1]之间的数</param>
        /// <param name="step"></param>
        /// <returns></returns>
        public virtual List<Point3D> GetPointsByKnots(float startDistanceRate, float endDistanceRate, float step)
        {
            List<Point3D> result = new List<Point3D>();
            float currentRate = startDistanceRate;
            while (currentRate < endDistanceRate)
            {
                Point3D point = new Point3D();
                point = GetFuncValue(currentRate);
                result.Add(point);
                currentRate += step;
            }
            return result;
        }
        #endregion


        #region Nurbs evaluator provided by mikau16 based on Michael V. implementation, roughly follows the notation of http://cs.mtu.edu/~shene/PUBLICATIONS/2004/NURBS.pdf

        public List<Point3D> PolygonalVertexes(int precision)
        {
            if (this.ControlPoints.Count == 0)
                throw new NotSupportedException("A spline entity with control points is required.");

            double u_start;
            double u_end;
            List<Point3D> vertexes = new List<Point3D>();

            // added a few fixes to make it work for open, closed, and periodic closed splines.
            if (!this.IsClosed)
            {
                precision -= 1;
                u_start = this.Knots[0];
                u_end = this.Knots[this.Knots.Count - 1];
            }
            else if (this.isPeriodic)
            {
                u_start = this.Knots[this.degree];
                u_end = this.Knots[this.Knots.Count - this.degree - 1];
            }
            else
            {
                u_start = this.Knots[0];
                u_end = this.Knots[this.Knots.Count - 1];
            }

            double u_delta = (u_end - u_start) / precision;

            for (int i = 0; i < precision; i++)
            {
                double u = u_start + u_delta * i;
                vertexes.Add(this.C(u));
            }

            if (!this.IsClosed)
                vertexes.Add(this.ControlPoints[this.ControlPoints.Count - 1]);

            return vertexes;
        }

        public Polyline3D ToPolyline(int precision)
        {
            IEnumerable<Point3D> vertexes = this.PolygonalVertexes(precision);
            Polyline3D poly = new Polyline3D();
            poly.Points.AddRange(vertexes);
            return poly;
        }

        private Point3D C(double u)
        {
            Point3D vectorSum = new Point3D(0, 0, 0);
            double denominatorSum = 0.0;

            // optimization suggested by ThVoss
            for (int i = 0; i < this.ControlPoints.Count; i++)
            {
                double n = this.N(i, this.degree, u);
                denominatorSum += n * this.ControlPoints[i].Weight;
                vectorSum += this.ControlPoints[i].Weight * n * this.ControlPoints[i];
            }

            // avoid possible divided by zero error, this should never happen
            if (Math.Abs(denominatorSum) < double.Epsilon)
                return Point3D.Zero;

            return (1.0 / denominatorSum) * vectorSum;
        }

        private double N(int i, int p, double u)
        {
            if (p <= 0)
            {
                if (this.Knots[i] <= u && u < this.Knots[i + 1])
                    return 1;
                return 0.0;
            }

            double leftCoefficient = 0.0;
            if (!(Math.Abs(this.Knots[i + p] - this.Knots[i]) < double.Epsilon))
                leftCoefficient = (u - this.Knots[i]) / (this.Knots[i + p] - this.Knots[i]);

            double rightCoefficient = 0.0; // article contains error here, denominator is Knots[i + p + 1] - Knots[i + 1]
            if (!(Math.Abs(this.Knots[i + p + 1] - this.Knots[i + 1]) < double.Epsilon))
                rightCoefficient = (this.Knots[i + p + 1] - u) / (this.Knots[i + p + 1] - this.Knots[i + 1]);

            return leftCoefficient * this.N(i, p - 1, u) + rightCoefficient * this.N(i + 1, p - 1, u);
        }

        #endregion
    }
}
