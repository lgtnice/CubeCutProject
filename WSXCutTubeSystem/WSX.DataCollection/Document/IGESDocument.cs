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
    public class IGESDocument : IDocument<FigureBase3DModel>
    {
        [XmlElement("Entities")]
        public EntityCollection<FigureBase3DModel> Entity { get; set; } = new EntityCollection<FigureBase3DModel>();
        public StandardTubeMode TubeMode { get; set; }
        public IGESDocument()
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
