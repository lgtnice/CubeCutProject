using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.Draw3D.Common;

namespace WSX.Draw3D.Utils.Undo
{
    public class EditCommandBase
    {
        public virtual bool DoUndo(IModel dataModel)
        {
            return false;
        }

        public virtual bool DoRedo(IModel dataModel)
        {
            return false;
        }
    }
}
