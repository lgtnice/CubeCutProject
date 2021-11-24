using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.Utilities;
using WSX.Draw3D.Utils;
using WSX.Iges.Entities;

namespace WSX.Draw3D.DrawTools
{
    public class DrawIgesUtils
    {
        public static List<IgesEntity> Entities;
        private static IntPtr nurbsRenderer = IntPtr.Zero;
        public static void DrawEntites(OpenGL gl)
        {
            if(nurbsRenderer == IntPtr.Zero)
            {
                nurbsRenderer = gl.NewNurbsRenderer();
            }
            Entities?.ForEach(e =>
            {
                if (e.EntityUseFlag == IgesEntityUseFlag.Geometry && e.EntityType == IgesEntityType.TrimmedParametricSurface)
                {
                    DrawEntity(gl, e);

                }
            });
        }
        
        private static void DrawEntity(OpenGL gl, IgesEntity entity)
        {
            if (gl != null)
            {
                switch (entity.EntityType)
                {
                    case IgesEntityType.Line:
                        {
                            var line = entity as IgesLine;
                            if (line.EntityUseFlag == IgesEntityUseFlag.Geometry)
                            {
                                gl.PushMatrix();
                                gl.Begin(OpenGL.GL_LINES);
                                double x = 0, y = 0, z = 0;
                                x = line.P1.X;
                                y = line.P1.Y;
                                z = line.P1.Z;
                                //CalTransformationMatrix(line.TransformationMatrix, ref x, ref y, ref z);
                                gl.Vertex(x, y, z);
                                x = line.P2.X;
                                y = line.P2.Y;
                                z = line.P2.Z;
                                //CalTransformationMatrix(line.TransformationMatrix, ref x, ref y, ref z);
                                gl.Vertex(x, y, z);
                                gl.End();
                                gl.PopMatrix();
                            }
                        }
                        break;
                    case IgesEntityType.RationalBSplineCurve:
                        {
                            return;
                            
                            var curve = entity as IgesRationalBSplineCurve;
                            var type = curve.CurveType;
                            var knots = curve.KnotValues.Select(e => (float)e).ToArray();
                            var ctrls = curve.ControlPointsToFloatArray();
                            
                            gl.PushMatrix();
                            gl.BeginCurve(nurbsRenderer);
                            gl.NurbsCurve(nurbsRenderer, knots.Length, knots, 3, ctrls, 4, OpenGL.GL_MAP1_VERTEX_3);
                            gl.EndCurve(nurbsRenderer);
                            gl.PopMatrix();
                        }
                        break;
                    case IgesEntityType.CircularArc:
                        {
                            var arc = entity as IgesCircularArc;
                            Point3D center = new Point3D((float)arc.Center.X, (float)arc.Center.Y, (float)arc.Center.Z);
                            Point3D start = new Point3D((float)arc.StartPoint.X, (float)arc.StartPoint.Y, (float)arc.StartPoint.Z);
                            Point3D end = new Point3D((float)arc.EndPoint.X, (float)arc.EndPoint.Y, (float)arc.EndPoint.Z);
                            double radius = HitUtil.Distance(center, start);
                            double startAngle = HitUtil.LineAngleR(center, start, 0);
                            double endAngle = HitUtil.LineAngleR(center, end, 0);
                            double step = 0.01;
                            List<Point3D> temp = new List<Point3D>();
                            //temp.Add(start);
                            double twoPI = Math.PI * 2;
                            if (startAngle < endAngle)
                            {
                                //for (double angle = startAngle; angle <= endAngle; angle += step)
                                for (double angle = 0; angle <= twoPI; angle += step)
                                {
                                    double x1 = center.X + Math.Cos(angle) * radius;
                                    double y1 = center.Y + Math.Sin(angle) * radius;
                                    temp.Add(new Point3D((float)x1, (float)y1));
                                }
                            }
                            else
                            {

                                for (double angle = endAngle; angle <= twoPI; angle += step)
                                {
                                    double x1 = center.X + Math.Cos(angle) * radius;
                                    double y1 = center.Y + Math.Sin(angle) * radius;
                                    temp.Add(new Point3D((float)x1, (float)y1));
                                }
                                for (double angle = 0; angle <= startAngle; angle += step)
                                {
                                    double x1 = center.X + Math.Cos(angle) * radius;
                                    double y1 = center.Y + Math.Sin(angle) * radius;
                                    temp.Add(new Point3D((float)x1, (float)y1));
                                }
                            }
                            //temp.Add(end);
                            double x = 0, y = 0, z = 0;
                            List<Point3D> drawPoints = new List<Point3D>();
                            for (int i = 0; i < temp.Count; i++)
                            {
                                x = temp[i].X;
                                y = temp[i].Y;
                                z = temp[i].Z;
                                CalTransformationMatrix(arc.TransformationMatrix, ref x, ref y, ref z);
                                drawPoints.Add(new Point3D((float)x, (float)y, (float)z));
                            }
                            gl.PushMatrix();

                            //gl.Translate(arc.TransformationMatrix.T1, arc.TransformationMatrix.T2, arc.TransformationMatrix.T3);
                            //gl.Rotate(90, 1, 0, 0);
                            CommonUtils.Draw(gl, drawPoints);
                            gl.PopMatrix();

                        }
                        break;
                    case IgesEntityType.CompositeCurve:
                        {
                            var curve = entity as IgesCompositeCurve;
                            curve.Entities.ForEach(e => DrawEntity(gl, e));
                        }
                        break;
                    case IgesEntityType.TrimmedParametricSurface:
                        {
                            var surface = entity as IgesTrimmedParametricSurface;
                            //DrawEntity(gl, surface.Surface);
                            DrawEntity(gl, surface.OuterBoundary);
                            surface.BoundaryEntities.ForEach(e => DrawEntity(gl, e));
                        }
                        break;
                    case IgesEntityType.ConicArc:
                        {
                            
                            var conic = entity as IgesConicArc;
                            //return;
                            if (conic.ArcType == IgesArcType.Ellipse)
                            {
                                //中心点
                                //x0 = (2CD - BE) / (B ^ 2 - 4AC)
                                //y0 = (2AE - BD) / (B ^ 2 - 4AC)
                                float[] color = new float[] { 0, 1, 0 };
                                double tempBAC = (Math.Pow(conic.CoefficientB, 2) - 4 * conic.CoefficientA * conic.CoefficientC);
                                double centerX = (2 * conic.CoefficientC * conic.CoefficientD - conic.CoefficientB * conic.CoefficientE) / tempBAC;
                                double centerY = (2 * conic.CoefficientA * conic.CoefficientE - conic.CoefficientB * conic.CoefficientD) / tempBAC;
                                double a = Math.Sqrt((-conic.CoefficientF / conic.CoefficientA));
                                double b = Math.Sqrt((-conic.CoefficientF / conic.CoefficientC));
                                Point3D center = new Point3D((float)centerX, (float)centerY, (float)conic.StartPoint.Z);
                                Point3D start = new Point3D((float)conic.StartPoint.X, (float)conic.StartPoint.Y, (float)conic.StartPoint.Z);
                                Point3D end = new Point3D((float)conic.EndPoint.X, (float)conic.EndPoint.Y, (float)conic.EndPoint.Z);
                                double startAngle = HitUtil.LineAngleR(center, start, 0);
                                double endAngle = HitUtil.LineAngleR(center, end, 0);
                                double x = 0, y = 0, z = 0;

                                List<Point3D> points = new List<Point3D>();
                                double twoPI = Math.PI * 2;
                                double step = 0.01;
                                if (startAngle < endAngle)
                                {
                                    for (double t = startAngle; t < endAngle; t += 0.01)
                                    {
                                        x = a * Math.Cos(t);
                                        y = b * Math.Sin(t);
                                        z = 0;
                                        CalTransformationMatrix(conic.TransformationMatrix, ref x, ref y, ref z);
                                        points.Add(new Point3D(-(float)x, (float)y, (float)z));
                                    }
                                }
                                else
                                {
                                    for (double t = endAngle; t <= twoPI; t += step)
                                    {
                                        x = a * Math.Cos(t);
                                        y = b * Math.Sin(t);
                                        z = 0;
                                        CalTransformationMatrix(conic.TransformationMatrix, ref x, ref y, ref z);
                                        points.Add(new Point3D(-(float)x, (float)y, (float)z));
                                    }
                                    for (double t = 0; t <= startAngle; t += step)
                                    {
                                        x = a * Math.Cos(t);
                                        y = b * Math.Sin(t);
                                        z = 0;
                                        CalTransformationMatrix(conic.TransformationMatrix, ref x, ref y, ref z);
                                        points.Add(new Point3D(-(float)x, (float)y, (float)z));
                                    }
                                }

                                CommonUtils.Draw(gl, points);
                            }
                        }
                        break;
                    case IgesEntityType.CurveOnAParametricSurface:
                        {
                            var curve = entity as IgesCurveOnAParametricSurface;
                            DrawEntity(gl, curve.CurveDefinitionB);
                            DrawEntity(gl, curve.CurveDefinitionC);
                        }
                        break;
                    default:
                        break;

                }
            }
        }
        private static void CalTransformationMatrix(IgesTransformationMatrix matrix, ref double x, ref double y, ref double z)
        {
            x = matrix.R11 * x + matrix.R12 * y + matrix.R13 * z + matrix.T1;
            y = matrix.R21 * x + matrix.R22 * y + matrix.R23 * z + matrix.T2;
            z = matrix.R31 * x + matrix.R32 * y + matrix.R33 * z + matrix.T3;
            return;
            if (matrix.R11 != 0 && matrix.R22 != 0 && matrix.R33 != 0)
            {
                x = matrix.R11 * x + matrix.R12 * y + matrix.R13 * z + matrix.T1;
                y = matrix.R21 * x + matrix.R22 * y + matrix.R23 * z + matrix.T2;
                z = matrix.R31 * x + matrix.R32 * y + matrix.R33 * z + matrix.T3;
            }
            else if (matrix.R12 != 0 && matrix.R23 != 0 && matrix.R31 != 0)
            {
                x = matrix.R31 * x + matrix.R32 * y + matrix.R33 * z + matrix.T3;
                y = matrix.R11 * x + matrix.R12 * y + matrix.R13 * z + matrix.T1;
                z = matrix.R21 * x + matrix.R22 * y + matrix.R23 * z + matrix.T2;
            }
            else if (matrix.R13 != 0 && matrix.R21 != 0 && matrix.R32 != 0)
            {
                x = matrix.R21 * x + matrix.R22 * y + matrix.R23 * z + matrix.T2;
                y = matrix.R31 * x + matrix.R32 * y + matrix.R33 * z + matrix.T3;
                z = matrix.R11 * x + matrix.R12 * y + matrix.R13 * z + matrix.T1;
            }
            else if (matrix.R13 != 0 && matrix.R22 != 0 && matrix.R31 != 0)
            {
                x = matrix.R31 * x + matrix.R32 * y + matrix.R33 * z + matrix.T3;
                y = matrix.R21 * x + matrix.R22 * y + matrix.R23 * z + matrix.T2;
                z = matrix.R11 * x + matrix.R12 * y + matrix.R13 * z + matrix.T1;
            }
            else if (matrix.R11 != 0 && matrix.R23 != 0 && matrix.R32 != 0)
            {
                x = matrix.R11 * x + matrix.R12 * y + matrix.R13 * z + matrix.T1;
                y = matrix.R31 * x + matrix.R32 * y + matrix.R33 * z + matrix.T3;
                z = matrix.R21 * x + matrix.R22 * y + matrix.R23 * z + matrix.T2;
            }
            else
            {

            }
        }
    }
}
