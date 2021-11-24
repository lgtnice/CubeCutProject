using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using WSX.CommomModel.TubeMode;

namespace WSX.Draw3D.DrawTools
{
    public class DrawSquareUtils//:IDrawObject
    {
        private List<List<Point3D>> slicePoints = new List<List<Point3D>>();
        private List<Point3D> vertexPoints;
        public  void DrawSquareTube(OpenGL gl, StandardTubeMode tubeMode)
        {
            float radius = tubeMode.CircleRadius;
            float sideLen = tubeMode.LongSideLength - 2 * radius;
            float offset = sideLen / 2;

            DrawFaceFramework(gl, radius, offset, tubeMode.LongSideLength);
            DrawLineInBody(gl, radius, tubeMode.LongSideLength, tubeMode.TubeTotalLength, 0, 0, 0);
            gl.Translate(0,0,tubeMode.TubeTotalLength);
            DrawFaceFramework(gl, radius, offset, tubeMode.LongSideLength);
        }

        private  void DrawFaceFramework(OpenGL gl,float radius,float offset,float sideLen)
        {
            this.vertexPoints = new List<Point3D>();
            DrawArc(gl, radius, 0, 90, offset, offset, 0);
            DrawArc(gl, radius, 90, 180, -offset, offset, 0);
            DrawArc(gl, radius, 180, 270, -offset, -offset, 0);
            DrawArc(gl, radius, 270, 360, offset, -offset, 0);
            DrawLineInFace(gl, radius, sideLen, 0, 0, 0);
            this.slicePoints.Add(vertexPoints);
        }

        private  void DrawArc(OpenGL gl, float radius, float startAngle,float endAngle, float x, float y, float z)
        {
            gl.PushMatrix();
            gl.Translate(x, y, z);
            ArcHelper arcHelper = new ArcHelper();
            List<Point3D> arcPoints = arcHelper.GetPointOfArc(radius, startAngle, endAngle);
            this.vertexPoints = arcPoints;
            gl.Begin(OpenGL.GL_LINE_STRIP);
            gl.Color(0.0f, 1.0f, 0.0f);
            for (int i = 0; i < arcPoints.Count; i++)
            {
                gl.Vertex(arcPoints[i].X, arcPoints[i].Y, arcPoints[i].Z);
            }
            gl.End();
            gl.PopMatrix();
        }

        private  void DrawLineInFace(OpenGL gl,float radius, float sideLen, float x, float y, float z)
        {
            gl.PushMatrix();
            gl.Translate(x, y, z);
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(.0f, 1.0f, .0f);
            gl.Vertex(sideLen/2, sideLen / 2-radius,0);
            gl.Vertex(sideLen / 2, -sideLen / 2+radius,0);

            gl.Vertex(sideLen / 2-radius, -sideLen/2,0);
            gl.Vertex(-sideLen/2+radius, -sideLen/2,0);

            gl.Vertex(-sideLen/2, -sideLen/2+radius,0);
            gl.Vertex(-sideLen/2, sideLen/2-radius,0);

            gl.Vertex(-sideLen/2+radius, sideLen/2,0);
            gl.Vertex(sideLen / 2 - radius, sideLen / 2,0);
            gl.End();
            gl.PopMatrix();
        }

        private  void DrawLineInBody(OpenGL gl, float radius, float sideLen, float totalLen, float x, float y, float z)
        {
            gl.PushMatrix();
            gl.Translate(x, y, z);
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(1.0f, 1.0f, 1.0f);
            gl.Vertex(sideLen / 2, sideLen / 2 - radius, 0);
            gl.Vertex(sideLen / 2, sideLen / 2 - radius, totalLen);

            gl.Vertex(sideLen / 2, -sideLen / 2 + radius, 0);
            gl.Vertex(sideLen / 2, -sideLen / 2 + radius, totalLen);

            gl.Vertex(sideLen / 2 - radius, -sideLen / 2, 0);
            gl.Vertex(sideLen / 2 - radius, -sideLen / 2, totalLen);

            gl.Vertex(-sideLen / 2 + radius, -sideLen / 2, 0);
            gl.Vertex(-sideLen / 2 + radius, -sideLen / 2, totalLen);

            gl.Vertex(-sideLen / 2, -sideLen / 2 + radius, 0);
            gl.Vertex(-sideLen / 2, -sideLen / 2 + radius, totalLen);

            gl.Vertex(-sideLen / 2, sideLen / 2 - radius, 0);
            gl.Vertex(-sideLen / 2, sideLen / 2 - radius, totalLen);

            gl.Vertex(-sideLen / 2 + radius, sideLen / 2, 0);
            gl.Vertex(-sideLen / 2 + radius, sideLen / 2, totalLen);

            gl.Vertex(sideLen / 2 - radius, sideLen / 2, 0);
            gl.Vertex(sideLen / 2 - radius, sideLen / 2, totalLen);

            gl.End();
            gl.PopMatrix();
        }

        public bool ObjectInRectangle(Rectangle rect)
        {
            throw new NotImplementedException();
        }

        public bool PointInObject(PointF point)
        {
            throw new NotImplementedException();
        }
    }
}
