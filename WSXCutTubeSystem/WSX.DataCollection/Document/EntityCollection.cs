using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;

namespace WSX.DataCollection.Document
{
    public class EntityCollection<T>
    {
        public List<T> Figures { get; set; } = new List<T>();
    }
}
