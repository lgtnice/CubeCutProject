using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WSX.CommomModel.DrawModel;

namespace WSX.CommomModel.Figure3DModel
{
    public class Line3DModel : FigureBase3DModel
    {
        public Line3DModel()
        {
            Type = FigureType.Line;
        }
        public Point3D P1 { get; set; }
        public Point3D P2 { get; set; }
    }
}
