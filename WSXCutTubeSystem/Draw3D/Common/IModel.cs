using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.Enums;
using WSX.CommomModel.ParaModel;
using WSX.Draw3D.Layers;
using WSX.Draw3D.Utils.Undo;

namespace WSX.Draw3D.Common
{
    public interface IModel
    {
        DrawLayer DrawLayer { get; set; }
        MarkLayer MarkLayer { get; set; }
        StandardTubeMode TubeMode { get; set; }
        void Draw(OpenGL gl);

        bool DoRedo();
        bool DoUndo();
        bool CanRedo();
        bool CanUndo();

        void Clear(ClearCommandType commandType);

        void Sort(SortMode sortMode);

        void DoUpdate(List<IDrawObject> addObjects, List<IDrawObject> delObjects, EditCommandBase editCommand);

        void DoArray();

        void DoCompensation();

        void SetGap();

        void Reverse();

        void ChangeInnerOutterCut();
    }
}
