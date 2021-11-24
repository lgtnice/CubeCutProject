using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;
using WSX.DataCollection.Utilities;

namespace WSX.DataCollection.Document
{
    public class BaseDocument<T>
    {
        [XmlElement("Entities")]
        public EntityCollection<T> Entity { get; set; }
        public StandardTubeMode TubeMode { get; set; }

        public virtual bool ReadFromFile(string fileName)
        {
            var baseDoc = XmlSerializer<BaseDocument<T>>.DeserializeFromFile(fileName);
            this.Entity = baseDoc.Entity;
            return true;
        }

        public virtual bool WriteToFile(string fileName)
        {
            XmlSerializer<BaseDocument<T>>.SerializeToFile(this,fileName);
            return true;
        }
    }
}
