using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.ParaModel;

namespace WSX.DataCollection.Document
{
    public class DXFDocument : IDocument<FigureBase2DModel>
    {
        [XmlElement("Entities")]
        public EntityCollection<FigureBase2DModel> Entity { get; set; } = new EntityCollection<FigureBase2DModel>();
        [XmlIgnore]
        public StandardTubeMode TubeMode { get; set; }
        public DXFDocument()
        {

        }

        public  bool ReadFromFile(string fileName)
        {
            return true;
        }

        public  bool WriteToFile(string fileName)
        {
            return true;
        }
    }
}
