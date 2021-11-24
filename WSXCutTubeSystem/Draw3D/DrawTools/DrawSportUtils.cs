using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.TubeMode;

namespace WSX.Draw3D.DrawTools
{
    public class DrawSportUtils
    {
        public  void DrawSportTube(OpenGL openGl, StandardTubeMode standardTubeMode)
        {
            float radius = standardTubeMode.CircleRadius;
            float width = standardTubeMode.LongSideLength;
            float offset = (width - 2 * radius) / 2;
            openGl.PushMatrix();
            DrawArc(openGl, radius, 0, 90, offset, 0, 0);
            DrawArc(openGl, radius, 90, 180, -offset, 0, 0);
            DrawArc(openGl, radius, 180, 270, -offset, 0, 0);
            DrawArc(openGl, radius, 270, 360, offset, 0, 0);
            openGl.PopMatrix();
            DrawLine(openGl, radius, width, 0, 0, 0);

            openGl.PushMatrix();
            openGl.Translate(0, 0, standardTubeMode.TubeTotalLength);
            DrawArc(openGl, radius, 0, 90, offset, 0, 0);
            DrawArc(openGl, radius, 90, 180, -offset, 0, -0);
            DrawArc(openGl, radius, 180, 270, -offset, 0, -0);
            DrawArc(openGl, radius, 270, 360, offset, 0, 0);
            DrawLine(openGl, radius, width, 0, 0, 0);
            openGl.PopMatrix();

            DrawLine2(openGl, radius, width, standardTubeMode.TubeTotalLength, 0, 0, 0);
        }

        private  void DrawArc(OpenGL openGl, float radius, float startAngle, float endAngle, float x, float y, float z)
        {
            openGl.PushMatrix();
            openGl.Translate(x, y, z);
            ArcHelper arcHelper = new ArcHelper();
            List<Point3D> arcPoints = arcHelper.GetPointOfArc(radius, startAngle, endAngle);
            openGl.Begin(OpenGL.GL_LINE_STRIP);
            openGl.Color(0.0f, 1.0f, 0.0f);
            for (int i = 0; i < arcPoints.Count; i++)
            {
                openGl.Vertex(arcPoints[i].X, arcPoints[i].Y, arcPoints[i].Z);
            }
            openGl.End();
            openGl.PopMatrix();
        }

        private  void DrawLine(OpenGL openGL, float radius, float sideLen, float x, float y, float z)
        {
            openGL.PushMatrix();
            openGL.Translate(x, y, z);
            openGL.Begin(OpenGL.GL_LINES);
            openGL.Color(.0f, 1.0f, .0f);

            openGL.Vertex(sideLen / 2 - radius, radius, 0);
            openGL.Vertex(-sideLen / 2 + radius, radius, 0);

            openGL.Vertex(-sideLen / 2 + radius, -radius, 0);
            openGL.Vertex(sideLen / 2 - radius, -radius, 0);

            openGL.End();
            openGL.PopMatrix();
        }

        private  void DrawLine2(OpenGL openGL, float radius, float sideLen, float totalLen, float x, float y, float z)
        {
            openGL.PushMatrix();
            openGL.Translate(x, y, z);
            openGL.Begin(OpenGL.GL_LINES);
            openGL.Color(1.0f, 1.0f, 1.0f);

            openGL.Vertex(radius, sideLen / 2 - radius,0);
            openGL.Vertex( radius, sideLen / 2 - radius, totalLen);

            openGL.Vertex( radius, -sideLen / 2 + radius,0);
            openGL.Vertex( radius, -sideLen / 2 + radius, totalLen);

            openGL.Vertex(-radius, -sideLen / 2 + radius,0);
            openGL.Vertex( -radius, -sideLen / 2 + radius, totalLen);

            openGL.Vertex( -radius, sideLen / 2 - radius,0);
            openGL.Vertex( -radius, sideLen / 2 - radius, totalLen);

            openGL.End();
            openGL.PopMatrix();
        }
    }
}
