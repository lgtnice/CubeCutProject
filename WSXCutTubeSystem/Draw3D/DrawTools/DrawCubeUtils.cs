using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.Draw3D.DrawTools
{
    public class DrawCubeUtils
    {
        private static void DrawPlane(OpenGL gl, float size, float step,float[] color)
        {
            gl.Begin(OpenGL.GL_QUADS);

            gl.Normal(0.0f, -1.0f, 0.0f);
            for (float z = 0.0f; z < size; z += step)
            {
                for (float x = 0.0f; x < size; x += step)
                {
                    gl.Color(color);
                    gl.Vertex(x, 0.0f, z);
                    gl.Vertex(x + step, 0.0f, z);
                    gl.Vertex(x + step, 0.0f, z + step);
                    gl.Vertex(x, 0.0f, z + step);
                }
            }
            gl.End();
        }

        public static void DrawCube(OpenGL gl,float size, int resolution)
        {
            float step = size / resolution;

            gl.PushMatrix();
            gl.Translate(-size / 2.0f, -size / 2f, -size / 2f);

            //top
            gl.PushMatrix();
            gl.Translate(0.0f, size, 0.0f);
            gl.Scale(1.0f, -1.0f, 1.0f);
            DrawPlane(gl, size, step, new float[] { 1, .0f, 0});
            gl.PopMatrix();

            //left
            gl.PushMatrix();
            gl.Rotate(90.0f, 0.0f, 0.0f, 1.0f);
            gl.Scale(1.0f, -1.0f, 1.0f);
            DrawPlane(gl,size, step, new float[] { 0, 1.0f, 0});
            gl.PopMatrix();

            //right
            gl.PushMatrix();
            gl.Translate(size, 0f, 0f);
            gl.Rotate(90.0f, 0.0f, 0.0f, 1.0f);
            DrawPlane(gl,size, step, new float[] { 0, .0f, 1 });
            gl.PopMatrix();

            //bottom
            DrawPlane(gl,size, step, new float[] { 1, 1.0f, 0, });

            //front
            gl.PushMatrix();
            gl.Translate(0f, 0f, size);
            gl.Rotate(90.0f, -1.0f, 0.0f, 0.0f);
            DrawPlane(gl,size, step, new float[] { 1, .0f, 1 });
            gl.PopMatrix();

            //back
            gl.PushMatrix();
            gl.Rotate(90.0f, -1.0f, 0.0f, 0.0f);
            gl.Scale(1.0f, -1.0f, 1.0f);
            DrawPlane(gl,size, step, new float[] { 0, 1.0f, 1.0f });
            gl.PopMatrix();


            gl.PopMatrix();
        }
    }
}