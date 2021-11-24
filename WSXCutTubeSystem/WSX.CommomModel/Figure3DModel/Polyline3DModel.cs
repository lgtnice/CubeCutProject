using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WSX.CommomModel.DrawModel;

namespace WSX.CommomModel.Figure3DModel
{
    public class Polyline3DModel : FigureBase3DModel
    {
        public Polyline3DModel()
        {
            Type = FigureType.Polyline;
        }
        public List<Point3D> Points { get; set; } = new List<Point3D>();
    }
}
