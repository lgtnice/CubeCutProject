using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.TubeMode;
using WSX.CommomModel.Utilities;

namespace WSX.Draw3D.DrawTools
{
    public class DrawRectangleUtils
    {
        public  void DrawRectangleTube(OpenGL openGl, StandardTubeMode mode)
        {
            float[] colorLeft = new float[] { 1f, 0f, 0f };
            float[] color = new float[] { 0f, 1f, 0f };
            float[] colorLine = new float[] { 1f, 1f, 0f };
            float radius = mode.CircleRadius;
            float offsetShort = (mode.ShortSideLength - 2 * radius) / 2;

            float longAxisL = (float)(radius / Math.Cos(MathUtils.DegreeToRad(mode.LeftAngle)));
            float offsetLLong = (float)(mode.LongSideLength / Math.Cos(MathUtils.DegreeToRad(mode.LeftAngle)) - longAxisL * 2) / 2;

            float longAxisR = (float)(radius / Math.Cos(MathUtils.DegreeToRad(mode.RightAngle)));
            float offsetRLong = (float)(mode.LongSideLength / Math.Cos(MathUtils.DegreeToRad(mode.RightAngle)) - longAxisR * 2) / 2;

            openGl.PushMatrix();
            DrawLine(openGl, mode.LongSideLength, mode.ShortSideLength, mode.CircleRadius, mode.LeftAngle);
            openGl.Rotate(mode.LeftAngle, 0f, 1, 0);
            CommonUtils.DrawArc(openGl, radius, offsetLLong, offsetShort, colorLeft, mode.LeftAngle);
            openGl.PopMatrix();

            openGl.PushMatrix();
            openGl.Translate(0, 0, mode.TubeTotalLength);
            DrawLine(openGl, mode.LongSideLength, mode.ShortSideLength, radius, mode.RightAngle);
            
            openGl.Rotate(mode.RightAngle, 0.0f, 1, 0);
            CommonUtils.DrawArc(openGl, radius, offsetRLong, offsetShort, color, mode.RightAngle);
            openGl.PopMatrix();

            DrawLine2(openGl, mode.LongSideLength, mode.ShortSideLength, radius, mode.LeftAngle, mode.RightAngle, mode.TubeTotalLength);
        }

        private  void DrawArc(OpenGL openGl, float radius, float startAngle, float endAngle, float x, float y, float z, float angle)
        {
            openGl.PushMatrix();
            openGl.Translate(x, y, z);
            openGl.Rotate(angle, 1.0f, 0, 0);

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
        private  void DrawLine(OpenGL gL, float longLen, float shortLen, float radius, float angle)
        {
            float offsetX = (longLen - radius * 2) / 2;
            float posX = longLen / 2;
            float offsetY = (shortLen - radius * 2) / 2;
            float posY = shortLen / 2;

            float sideOffset = (float)Math.Tan(MathUtils.DegreeToRad(angle)) * (longLen / 2 - radius);
            float radiusOffset = (float)Math.Tan(MathUtils.DegreeToRad(angle)) * longLen / 2;

            gL.PushMatrix();
            gL.Begin(OpenGL.GL_LINES);
            gL.Color(.0f, 1.0f, .0f);

            gL.Vertex(-offsetX, posY, sideOffset);
            gL.Vertex(offsetX, posY, -sideOffset);

            gL.Vertex(posX, offsetY, -radiusOffset);
            gL.Vertex(posX, -offsetY, -radiusOffset);

            gL.Vertex(offsetX, -posY, -sideOffset);
            gL.Vertex(-offsetX, -posY, sideOffset);

            gL.Vertex(-posX, -offsetY, radiusOffset);
            gL.Vertex(-posX, offsetY, radiusOffset);

            gL.End();
            gL.PopMatrix();
        }
        private  void DrawLine(OpenGL openGL, float radius, float sideLen, float shortLen, float zLen, float zRadius)
        {
            openGL.PushMatrix();
            openGL.Begin(OpenGL.GL_LINES);
            openGL.Color(.0f, 1.0f, .0f);
            openGL.Vertex(sideLen / 2 - radius, shortLen / 2, zLen);
            openGL.Vertex(-sideLen / 2 + radius, shortLen / 2, zLen);

            openGL.Vertex(-sideLen / 2, shortLen / 2 - radius, zLen - zRadius / 2);
            openGL.Vertex(-sideLen / 2, -shortLen / 2 + radius, -zLen + zRadius / 2);

            //openGL.Vertex(-sideLen / 2 + radius, -shortLen / 2, -zLen);
            //openGL.Vertex(sideLen / 2 - radius, -shortLen / 2, -zLen);

            //openGL.Vertex(sideLen / 2, -shortLen / 2 + radius, -zLen - zRadius);
            //openGL.Vertex(sideLen / 2, shortLen / 2 - radius, zLen + zRadius);
            openGL.End();
            openGL.PopMatrix();
        }

        private  void DrawLine2(OpenGL gl, float longLen, float shortLen, float radius, float leftAngle, float rightAngle, float totalLen)
        {
            float offsetX = (longLen - radius * 2) / 2;
            float posX = longLen / 2;
            float offsetY = (shortLen - radius * 2) / 2;
            float posY = shortLen / 2;

            float sideOffsetL = (float)Math.Tan(MathUtils.DegreeToRad(leftAngle)) * (longLen / 2 - radius);
            float radiusOffsetL = (float)Math.Tan(MathUtils.DegreeToRad(leftAngle)) * longLen / 2;
            float sideOffsetR = (float)Math.Tan(MathUtils.DegreeToRad(rightAngle)) * (longLen / 2 - radius);
            float radiusOffsetR = (float)Math.Tan(MathUtils.DegreeToRad(rightAngle)) * longLen / 2;

            gl.PushMatrix();
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(1.0f, 1.0f, 1.0f);

            gl.Vertex(-offsetX, posY, sideOffsetL);
            gl.Vertex(-offsetX, posY, totalLen + sideOffsetR);

            gl.Vertex(offsetX, posY, -sideOffsetL);
            gl.Vertex(offsetX, posY, totalLen - sideOffsetR);

            gl.Vertex(posX, offsetY, -radiusOffsetL);
            gl.Vertex(posX, offsetY, totalLen - radiusOffsetR);

            gl.Vertex(posX, -offsetY, -radiusOffsetL);
            gl.Vertex(posX, -offsetY, totalLen - radiusOffsetR);

            gl.Vertex(offsetX, -posY, -sideOffsetL);
            gl.Vertex(offsetX, -posY, totalLen - sideOffsetR);

            gl.Vertex(-offsetX, -posY, sideOffsetL);
            gl.Vertex(-offsetX, -posY, totalLen + sideOffsetR);

            gl.Vertex(-posX, -offsetY, radiusOffsetL);
            gl.Vertex(-posX, -offsetY, totalLen + radiusOffsetR);

            gl.Vertex(-posX, offsetY, radiusOffsetL);
            gl.Vertex(-posX, offsetY, totalLen + radiusOffsetR);

            gl.End();
            gl.PopMatrix();
        }
    }
}
