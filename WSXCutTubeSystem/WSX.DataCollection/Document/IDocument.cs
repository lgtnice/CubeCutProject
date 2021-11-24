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
    public interface IDocument<T>
    {
        EntityCollection<T> Entity { get; set; } 
        StandardTubeMode TubeMode { get; set; }
        bool ReadFromFile(string fileName);
        bool WriteToFile(string fileName);
    }
}
