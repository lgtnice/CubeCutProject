using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.Draw3D.Common;

namespace WSX.Draw3D.Utils.Undo
{
    public class EditCommandArray:EditCommandBase
    {
        private List<IDrawObject> delObjects = new List<IDrawObject>();
        private List<IDrawObject> addObjects = new List<IDrawObject>();

        public EditCommandArray(List<IDrawObject> addObjects, List<IDrawObject> delObjects)
        {
            this.delObjects = delObjects;
            this.addObjects = addObjects;
        }

        public override bool DoUndo(IModel dataModel)
        {
            dataModel.DoUpdate(this.delObjects, this.addObjects, this);
            return true;
        }

        public override bool DoRedo(IModel dataModel)
        {
            dataModel.DoUpdate(this.addObjects, this.delObjects, this);
            return true;
        }
    }
}
