using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using WSX.CommomModel.Enums;
using WSX.CommomModel.ParaModel;
using WSX.Draw3D.Common;
using WSX.Draw3D.Layers;
using WSX.Draw3D.Utils;
using WSX.Draw3D.Utils.Undo;
using WSX.GlobalData.Model;

namespace WSX.Draw3D
{
    public class DataModel : IModel
    {
        public DrawLayer DrawLayer { get; set; } = new DrawLayer();
        public MarkLayer MarkLayer { get; set; } = new MarkLayer();
        public StandardTubeMode TubeMode { get; set; }

        private UndoRedoBuffer undoRedoBuffer = new UndoRedoBuffer();

        public DataModel()
        {
            
        }

        public void Draw(OpenGL gl)
        {
            this.DrawLayer.Draw(gl);
            this.MarkLayer.Draw(gl);
            this.DrawLayer.DrawFlags(gl);
            this.DrawLayer.DrawVacantPath(gl);
        }

        public bool CanUndo()
        {
            return this.undoRedoBuffer.CanUndo;
        }
        public bool CanRedo()
        {
            return this.undoRedoBuffer.CanRedo;
        }
        public bool DoUndo()
        {
            return this.undoRedoBuffer.DoUndo(this);
        }
        public bool DoRedo()
        {
            return this.undoRedoBuffer.DoRedo(this);
        }

        public void Sort(SortMode sortMode)
        {
            if (this.DrawLayer.Objects == null && this.DrawLayer.Objects.Count < 2)
                return;

            var oldObjcts = new List<IDrawObject>();
            this.DrawLayer.Objects.ForEach(e => oldObjcts.Add(e));
            var newObjcts = SortUtil.Sort(oldObjcts, sortMode);

            this.DoUpdate(newObjcts, oldObjcts, new EditCommandUpdate(newObjcts, oldObjcts));
        }

        public void DoUpdate(List<IDrawObject> addObjects, List<IDrawObject> delObjects, EditCommandBase editCommand)
        {
            if (this.undoRedoBuffer.CanCapture && editCommand != null)
            {
                this.undoRedoBuffer.AddCommand(editCommand);
            }

            List<IDrawObject> objects = new List<IDrawObject>();
            if (delObjects != null)
            {
                objects = delObjects.Where(e => e.LayerId > 0).ToList();
                foreach(var obj in objects)
                {
                    this.DrawLayer.Remove(obj);
                }
                objects = delObjects.Where(e => e.LayerId <= 0).ToList();
                foreach (var obj in objects)
                {
                    this.MarkLayer.Remove(obj);
                }
            }

            if (addObjects != null)
            {
                objects = addObjects.Where(e => e.LayerId > 0).ToList();
                foreach (var obj in objects)
                {
                    this.DrawLayer.Add(obj);
                }
                objects = addObjects.Where(e => e.LayerId <= 0).ToList();
                foreach (var obj in objects)
                {
                    this.MarkLayer.Add(obj);
                }
            }

            GlobalModel.TotalDrawObjectCount = this.DrawLayer.Objects.Count;
        }

        /// <summary>
        /// 切换阴切阳切
        /// </summary>
        public void ChangeInnerOutterCut()
        {
            if (DrawLayer.Objects.Count == 0)
                return;

            var oldObjcts = DrawLayer.Objects.Where(e =>e.IsSelected).ToList();
            if (oldObjcts.Count == 0)
                return;

            IDrawObject newObj = null;
            var newObjcts = new List<IDrawObject>();
            oldObjcts.ForEach(e =>
            {
                newObj = e.Clone();
                newObj.IsInnerCut = !e.IsInnerCut;
                newObj.Update();

                newObjcts.Add(newObj);
            });

            this.DoUpdate(newObjcts, oldObjcts,new EditCommandUpdate(newObjcts,oldObjcts));
        }

        public void SetGap()
        {
            if (DrawLayer.Objects.Count == 0)
                return;
            
            var oldObjcts = DrawLayer.Objects.Where(e => !GlobalModel.Params.GapParam.IsEnableForSelected || e.IsSelected).ToList() ;
            if (oldObjcts.Count == 0)
                return;

            IDrawObject newObj = null;
            var newObjcts = new List<IDrawObject>();
            oldObjcts.ForEach(e =>
            {
                newObj = e.Clone();
                newObj.SetGap(GlobalModel.Params.GapParam);
                newObjcts.Add(newObj);
            });

            this.DoUpdate(newObjcts, oldObjcts, new EditCommandUpdate(newObjcts, oldObjcts));
        }

        /// <summary>
        /// 补偿
        /// </summary>
        public void DoCompensation()
        {
            if (DrawLayer.Objects.Count == 0)
                return;
            var oldObjcts = DrawLayer.Objects.Where(e => !GlobalModel.Params.GapParam.IsEnableForSelected || e.IsSelected).ToList();
            if (oldObjcts.Count == 0)
                return;

            IDrawObject newObj = null;
            var newObjcts = new List<IDrawObject>();
            oldObjcts.ForEach(e =>
            {
                newObj = e.Clone();
                newObj.DoCompensation(GlobalModel.Params.CompensationParam);
                newObjcts.Add(newObj);
            });

            this.DoUpdate(newObjcts, oldObjcts, new EditCommandUpdate(newObjcts, oldObjcts));
        }

        public void Reverse()
        {
            if (DrawLayer.Objects.Count == 0)
                return;

            var oldObjcts = DrawLayer.Objects.Where(e => e.IsSelected).ToList();
            if (oldObjcts.Count == 0)
                return;

            IDrawObject newObj = null;
            var newObjcts = new List<IDrawObject>();
            oldObjcts.ForEach(e =>
            {
                newObj = e.Clone();
                newObj.Reverse();
                newObjcts.Add(newObj);
            });

            this.DoUpdate(newObjcts, oldObjcts, new EditCommandUpdate(newObjcts, oldObjcts));
        }

        public void Clear(ClearCommandType commandType)
        {
            if (DrawLayer.Objects.Count == 0)
                return;

            var oldObjcts = DrawLayer.Objects.Where(e => e.IsSelected).ToList();
            if (oldObjcts.Count == 0)
                return;

            IDrawObject newObj = null;
            var newObjcts = new List<IDrawObject>();
            oldObjcts.ForEach(e =>
            {
                newObj = e.Clone();
                newObj.Clear(commandType);
                newObjcts.Add(newObj);
            });

            this.DoUpdate(newObjcts, oldObjcts, new EditCommandUpdate(newObjcts, oldObjcts));
        }

        /// <summary>
        ///  阵列
        /// </summary>
        public void DoArray()
        {
            if (DrawLayer.Objects.Count == 0)
                return;

            List<IDrawObject> addObjects = ArrayHelper.CalArray(DrawLayer.Objects, MarkLayer.Objects);

            this.DoUpdate(addObjects, null, new EditCommandArray(addObjects, null));
        }
    }
}