using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.Draw3D.Common;

namespace WSX.Draw3D.Utils.Undo
{
    public class UndoRedoBuffer
    {
        private List<EditCommandBase> undoBuffer = new List<EditCommandBase>();
        private List<EditCommandBase> redoBuffer = new List<EditCommandBase>();

        //public bool Dirty { set; get; }
        public bool CanCapture { private set; get; } = true;
        public bool CanUndo { get { return this.undoBuffer.Count > 0; } }
        public bool CanRedo { get { return this.redoBuffer.Count > 0; } }

        public void Clear()
        {
            this.undoBuffer.Clear();
            this.redoBuffer.Clear();
        }

        public void AddCommand(EditCommandBase editCommandBase)
        {
            if(this.CanCapture && editCommandBase != null)
            {
                this.undoBuffer.Add(editCommandBase);
                this.redoBuffer.Clear();
                //this.Dirty = true;
            }
        }

        public EditCommandBase GetUndoCommand()
        {
            if (this.undoBuffer.Count == 0)
                return null;
            EditCommandBase editCommandBase = this.undoBuffer[this.undoBuffer.Count - 1];
            return editCommandBase;
        }

        public bool DoUndo(IModel dataModel)
        {
            if (!this.CanUndo)
                return false;
            this.CanCapture = false;
            try
            {
                EditCommandBase editCommand = this.GetUndoCommand();
                bool result = editCommand.DoUndo(dataModel);
                this.undoBuffer.RemoveAt(this.undoBuffer.Count - 1);
                this.redoBuffer.Add(editCommand);
                return result;
            }
            finally
            {
                this.CanCapture = true;
                //this.Dirty = true;
            }
        }

        public bool DoRedo(IModel dataModel)
        {
            if (!this.CanRedo)
                return false;

            this.CanCapture = false;
            try
            {
                EditCommandBase editCommand = this.redoBuffer[this.redoBuffer.Count - 1];
                bool result = editCommand.DoRedo(dataModel);
                this.redoBuffer.RemoveAt(this.redoBuffer.Count - 1);
                this.undoBuffer.Add(editCommand);
                return result;
            }
            finally
            {
                this.CanCapture = true;
                //this.Dirty = true;
            }
        }
    }
}