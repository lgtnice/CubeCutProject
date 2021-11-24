using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;

namespace WSX.CommomModel.Figure3DModel
{
    public class Spline3DModel : FigureBase3DModel
    {
        public List<float> Knots { get; set; } 
        public List<Point3D> ControlPoints { get;  set; }
        public Spline3DModel()
        {
            Type = FigureType.Spline;
        }
    }
}
