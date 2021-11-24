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

namespace WSX.Draw3D.DrawTools
{
    /// <summary>
    /// 组合曲线
    /// </summary>
    public class GeoCurve3D : Polyline3D//DrawObjectBase
    {
        public GeoCurve3D()
        {
            Type = FigureType.GeoCurve;
        }
        public List<IDrawObject> Geometry { get; set; } = new List<IDrawObject>();
        /*public override bool IsSelected
        {
            get { return Geometry.Exists(g => g.IsSelected); }
            set { Geometry.ForEach(g => g.IsSelected = value); }
        }

        public override void Draw(OpenGL gl, float[] color)
        {
            this.Geometry.ForEach(g => g.Draw(gl, color));
        }
        public override bool ObjectInRectangle(float[] matrix, RectangleF rect, bool anyPoint)
        {
            foreach (IDrawObject g in this.Geometry)
            {
                if (g.ObjectInRectangle(matrix, rect, anyPoint))
                {
                    return true;
                }
            }
            return false;
        }

        public override bool PointInObject(float[] matrix, PointF point)
        {
            foreach (IDrawObject g in this.Geometry)
            {
                if (g.PointInObject(matrix, point))
                {
                    return true;
                }
            }
            return false;
        }
        public override void ShowMachinePath(float[] matrix, OpenGL gl, float[] color)
        {
            this.Geometry.ForEach(g => g.ShowMachinePath(matrix, gl, color));
        }
        */
       
        public void UpdatePolyline()
        {
            List<Point3D> vertexes = new List<Point3D>();
            this.Geometry.ForEach(g => vertexes.AddRange(this.ToPoints(g)));
            this.Points.Clear();
            this.Points.AddRange(vertexes);
            this.Update();
        }
        private List<Point3D> ToPoints(IDrawObject draw)
        {
            List<Point3D> points = new List<Point3D>();
            switch (draw.Type)
            {
                case FigureType.Line:
                    {
                        var line = draw as Line3D;
                        points.Add(new Point3D(line.P1));
                        points.Add(new Point3D(line.P2));
                    }
                    break;
                case FigureType.Polyline:
                    {
                        var spline = draw as Polyline3D;
                        points.AddRange(CopyUtil.DeepCopy(spline.Points));
                    }
                    break;
                case FigureType.Spline:
                    {
                        var spline = draw as Spline3D;
                        spline.UpdatePolyline();
                        points.AddRange(CopyUtil.DeepCopy(spline.Points));
                    }
                    break;
                case FigureType.GeoCurve:
                    {
                        var geo = draw as GeoCurve3D;
                        foreach (IDrawObject d in geo.Geometry)
                        {
                            points.AddRange(ToPoints(d));
                        }
                    }
                    break;
                default:
                    break;
            }
            return points;
        }
        public override IDrawObject Clone()
        {
            GeoCurve3D newObj = new GeoCurve3D();
            newObj.Copy(this);
            return newObj;
        }

        public override void Copy(IDrawObject source)
        {
            var data = source as GeoCurve3D;
            base.Copy(data);
            data.Geometry.ForEach(g => this.Geometry.Add(g.Clone()));
            //this.Update();
        }

        public override void MoveAxisZ(float offset)
        {
            base.MoveAxisZ(offset);
            this.Geometry.ForEach(g => g.MoveAxisZ(offset));
            //this.Update();
        }
    }
}
