using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;

namespace WSX.CommomModel.Figure3DModel
{
    public class GeoCurve3DModel : FigureBase3DModel
    {
        public List<FigureBase3DModel> Geometry { get; set; }
        public GeoCurve3DModel()
        {
            Type = FigureType.GeoCurve;
            Geometry = new List<FigureBase3DModel>();
        }
    }
}
