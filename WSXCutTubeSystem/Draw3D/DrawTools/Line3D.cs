using System;
using System.Collections.Generic;
using System.Drawing;
using SharpGL;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.Utilities;
using WSX.Draw3D.Common;
using WSX.Draw3D.Utils;

namespace WSX.Draw3D.DrawTools
{
    public class Line3D : DrawObjectBase
    {
        public Line3D()
        {
            Type = FigureType.Line;
        }
        public Point3D P1 { get; set; }
        public Point3D P2 { get; set; }
        public override float SizeLength { get; protected set; }
        public override void Draw(OpenGL gl, float[] color)
        {
            gl.PushMatrix();
            if (IsSelected)
            {
                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                gl.LineStipple(1, 0x739C);
            }
            gl.Color(color);
            gl.LineWidth(IsSelected || !IsLineBold ? 1 : 2);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(P1.X, P1.Y, P1.Z);
            gl.Vertex(P2.X, P2.Y, P2.Z);
            gl.End();
            gl.Disable(OpenGL.GL_LINE_STIPPLE);
            gl.PopMatrix();
        }
        public override void ShowMachinePath(float[] matrix, OpenGL gl, float[] color)
        {
            var centerPoint = (P1 + P2) / 2;
            ArrowUtil.DrawArrow(P1, centerPoint, matrix, color, gl);
        }

        public override bool ObjectInRectangle(float[] matrix, RectangleF rect, bool anyPoint)
        {
            var points = new List<Point3D> { this.P1, this.P2 };
            if (anyPoint)
            {
                return HitUtil.LineIntersectWithRect(this.P1, this.P2, rect);
            }
            else
            {
                return HitUtil.ObjectInRectangle(matrix, rect, points);
            }
        }

        public override bool PointInObject(float[] matrix, PointF point)
        {
            var points = new List<Point3D> { this.P1, this.P2 };
            return HitUtil.PointInObject(matrix, 0, point, points);
        }

        private void Move(float offsetX, float offsetY, float offsetZ)
        {
            this.P1.Translate(offsetX, offsetY, offsetZ);
            this.P2.Translate(offsetX, offsetY, offsetZ);
            this.Update();
        }
        public override void Update()
        {
            this.UpdateSizeLength();
            base.Update();
        }
        private void UpdateSizeLength()
        {
            this.SizeLength = (float)HitUtil.Distance(this.P1, this.P2);
        }
        private void UpdateEndStartMovePoint()
        {
            this.StartMovePoint = this.P1;
            this.EndMovePoint = this.P2;
        }
        public override void MoveAxisZ(float offset)
        {
            this.Move(0f, 0f, offset);
        }

        public override IDrawObject Clone()
        {
            Line3D newObj = new Line3D();
            newObj.Copy(this);
            return newObj;
        }

        public override void Copy(IDrawObject source)
        {
            var data = source as Line3D;
            base.Copy(data);
            this.P1 = new Point3D(data.P1);
            this.P2 = new Point3D(data.P2);
            this.Update();
        }
    }
}
