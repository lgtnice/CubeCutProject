using SharpGL;
using System;
using System.Collections.Generic;
using WSX.CommomModel.TubeMode;
using WSX.CommomModel.Utilities;

namespace WSX.Draw3D.DrawTools
{
    public class DrawCircleUtils
    {
        public  void DrawCircle(OpenGL gl, StandardTubeMode standardTubeMode)
        {
            float[] color = new float[] { 1f, 0f, 0f };
            float[] colorLine = new float[] { 1f, 1f, 0f };
            gl.PushMatrix();
            //gl.Rotate(standardTubeMode.LeftAngle, 1.0, 0.0f, 0);          

            gl.Rotate(standardTubeMode.LeftAngle, 0, 1, 0);
            CommonUtils.DrawCircle(gl, standardTubeMode.CircleRadius, color, standardTubeMode.LeftAngle);

            gl.PopMatrix();

            gl.PushMatrix();
            gl.Translate(0.0f, 0, standardTubeMode.TubeTotalLength);
            //gl.Rotate(standardTubeMode.RightAngle,1.0,0.0f, 0);
            gl.Rotate(standardTubeMode.RightAngle, 0, 1, 0);

            CommonUtils.DrawCircle(gl, standardTubeMode.CircleRadius, color, standardTubeMode.RightAngle);
            gl.PopMatrix();

            DrawLine(gl, standardTubeMode, colorLine);
        }



        private  void DrawLine(OpenGL gl, StandardTubeMode mode, float[] color)
        {
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(color);
            float totalLen = mode.TubeTotalLength;
            float leftLen = (float)Math.Tan(MathUtils.DegreeToRad(mode.LeftAngle)) * mode.CircleRadius;
            float rightLen = (float)Math.Tan(MathUtils.DegreeToRad(mode.RightAngle)) * mode.CircleRadius;
            float radius = mode.CircleRadius;

            gl.Vertex(-radius, 0, leftLen);
            gl.Vertex(-radius, 0, totalLen + rightLen);

            gl.Vertex(radius, 0, -leftLen);
            gl.Vertex(radius, 0, totalLen - rightLen);

 			//gl.Vertex(0f, -radius, -leftLen);
            //gl.Vertex(0,-radius, totalLen - rightLen);

            //gl.Vertex(0f, radius, leftLen);
            //gl.Vertex(0, radius, totalLen + rightLen);

            gl.End();
        }
    }
}
