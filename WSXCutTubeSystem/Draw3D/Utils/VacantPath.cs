using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;
using WSX.GlobalData.Model;

namespace WSX.Draw3D.Utils
{
    public class VacantPath
    {
        public int LayerId { get; set; }
        public List<Point3D> Points { get; set; } = new List<Point3D>();
        public void Draw(OpenGL gl, float[] color)
        {
            gl.PushMatrix();
            gl.Enable(OpenGL.GL_LINE_STIPPLE);
            gl.LineStipple(1, 0x739C);
            gl.Color(color);
            gl.LineWidth(1);
            gl.Begin(OpenGL.GL_LINE_STRIP);
            Points.ForEach(e => gl.Vertex(e.X, e.Y, e.Z));
            gl.End();
            gl.Disable(OpenGL.GL_LINE_STIPPLE);
            gl.PopMatrix();
        }
        public void ShowMachinePath(float[] matrix, OpenGL gl, float[] color)
        {
            for (int i = 0; i < Points.Count - 1; i++)
            {
                var center = (Points[i] + Points[i + 1]) / 2;
                ArrowUtil.DrawArrow(Points[i], center, matrix, color, gl);
            }
        }
    }
}
