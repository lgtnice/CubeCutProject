using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.Utilities;

namespace WSX.Draw3D.DrawTools
{
    public class CommonUtils
    {
        #region breshmen绘制椭圆
        private static void PutPixel(OpenGL opengl, float xc, float yc, float x, float y)
        {
            opengl.Vertex(0.0f, xc + x, yc + y);
        }

        public static void DrawEllipse(OpenGL opengl, float xc, float yc, float a, float b)
        {
            opengl.Begin(OpenGL.GL_POINTS);
            float x = 0, y = b;
            float d1, d2;
            d1 = (float)(b * b + a * a * (-b + 0.25));
            PutPixel(opengl, xc, yc, x, y);
            PutPixel(opengl, xc, yc, -x, -y);
            PutPixel(opengl, xc, yc, -x, y);
            PutPixel(opengl, xc, yc, x, -y);
            while (b * b * (x + 1) < a * a * (y - 0.5))
            {
                if (d1 < 0)
                {
                    d1 += b * b * (2 * x + 3);
                    x++;
                }
                else
                {
                    d1 += b * b * (2 * x + 3) + a * a * (-2 * y + 2);
                    x++;
                    y--;
                }
                PutPixel(opengl, xc, yc, x, y);
                PutPixel(opengl, xc, yc, -x, -y);
                PutPixel(opengl, xc, yc, -x, y);
                PutPixel(opengl, xc, yc, x, -y);
            }

            d2 = (float)(b * b * (x + 0.5) * (x + 0.5) + a * a * (y - 1) * (y - 1) - a * a * b * b);
            while (y > 0)
            {
                if (d2 <= 0)
                {
                    d2 += b * b * (2 * x + 2) + a * a * (-2 * y + 3);
                    x++;
                    y--;
                }
                else
                {
                    d2 += a * a * (-2 * y + 3);
                    y--;
                }
                PutPixel(opengl, xc, yc, x, y);
                PutPixel(opengl, xc, yc, -x, -y);
                PutPixel(opengl, xc, yc, -x, y);
                PutPixel(opengl, xc, yc, x, -y);
            }
            opengl.End();
        }
        #endregion

        public static void DrawCircle(OpenGL gl, float radius, float[] color, float angle)
        {
            float a = (float)(radius / Math.Cos(MathUtils.DegreeToRad(angle)));
            float b = radius;
            DrawEllipse(gl, a, b);
        }
        public static void DrawEllipse(OpenGL opengl, float a, float b)
        {
            opengl.PushMatrix();
            List<Point3D> result = GetEllipsePoint(a, b, 0, (float)MathUtils.DegreeToRad(90));
            Draw(opengl, result);
            result = GetEllipsePoint(a, b, (float)MathUtils.DegreeToRad(90), (float)MathUtils.DegreeToRad(180));
            Draw(opengl, result);
            result = GetEllipsePoint(a, b, (float)MathUtils.DegreeToRad(180), (float)MathUtils.DegreeToRad(270));
            Draw(opengl, result);
            result = GetEllipsePoint(a, b, (float)MathUtils.DegreeToRad(270), (float)MathUtils.DegreeToRad(360));
            Draw(opengl, result);
            opengl.PopMatrix();
        }
        public static void DrawArc(OpenGL gl, float radius, float offsetX, float offsetY, float[] color, float angle)
        {
            float a = (float)(radius / Math.Cos(MathUtils.DegreeToRad(angle)));
            float b = radius;
            float offsetZ = 0.0f;
            DrawEllipse(gl, a, b, offsetX, offsetY, offsetZ, color, angle);
        }

        public static void DrawEllipse(OpenGL opengl, float a, float b, float offsetX, float offsetY, float offsetZ, float[] color, float angle)
        {
            //opengl.PushMatrix();
            List<Point3D> result = GetEllipsePoint(a, b, 0, (float)MathUtils.DegreeToRad(90));
            Draw(opengl, result, offsetX, offsetY, offsetZ, color, angle);
            result = GetEllipsePoint(a, b, (float)MathUtils.DegreeToRad(90), (float)MathUtils.DegreeToRad(180));
            Draw(opengl, result, -offsetX, offsetY, offsetZ, color, angle);
            result = GetEllipsePoint(a, b, (float)MathUtils.DegreeToRad(180), (float)MathUtils.DegreeToRad(270));
            Draw(opengl, result, -offsetX, -offsetY, offsetZ, color, angle);
            result = GetEllipsePoint(a, b, (float)MathUtils.DegreeToRad(270), (float)MathUtils.DegreeToRad(360));
            Draw(opengl, result, offsetX, -offsetY, offsetZ, color, angle);
            //opengl.PopMatrix();
        }

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
                    result.Add(new Point3D(-x, -y, 0));
                }
            }
            return result;
        }

        public static void Draw(OpenGL opengl, List<Point3D> points)
        {
            opengl.PushMatrix();
            opengl.Begin(OpenGL.GL_LINE_STRIP);
            opengl.Color(0.0f, 1.0f, 0.0f);
            for (int i = 0; i < points.Count; i++)
            {
                opengl.Vertex(points[i].X, points[i].Y, points[i].Z);
            }
            opengl.End();
            opengl.PopMatrix();
        }

        private static void Draw(OpenGL opengl, List<Point3D> points, float offsetX, float offsetY, float offsetZ, float[] color, float angle)
        {
            opengl.PushMatrix();
            opengl.Translate(offsetX, offsetY, offsetZ);
            opengl.Begin(OpenGL.GL_LINE_STRIP);
            opengl.Color(color);
            for (int i = 0; i < points.Count; i++)
            {
                opengl.Vertex(points[i].X, points[i].Y, points[i].Z);
            }
            opengl.End();
            opengl.PopMatrix();
        }
    }
}
