using System.Collections.Generic;
using WSX.CommomModel.DrawModel;
using WSX.Draw3D.Common;
using WSX.Draw3D.Layers;

namespace WSX.Draw3D.Utils
{
    public class MoveUtil
    {
        private UCCanvas ucCanvas;
        private DrawLayer drawLayer;
        private List<IDrawObject> originals = new List<IDrawObject>();

        public UnitPoint OriginalPoint { get; private set; } = UnitPoint.Empty;
        public UnitPoint LastPoint { get; private set; } = UnitPoint.Empty;
        public List<IDrawObject> Copies { get; } = new List<IDrawObject>();

        public MoveUtil(UCCanvas uCCanvas, DrawLayer DrawLayer)
        {
            this.ucCanvas = uCCanvas;
            this.drawLayer = DrawLayer;
        }

        public bool IsEmpty
        {
            get
            {
                return this.Copies.Count == 0;
            }
        }

        public bool HandleMouseMoveForMove(UnitPoint mousePoint)
        {
            if (this.originals.Count == 0)
            {
                return false;
            }
            double x = mousePoint.X - this.LastPoint.X;
            double y = mousePoint.Y - this.LastPoint.Y;
            UnitPoint offset = new UnitPoint(x, y);
            this.LastPoint = mousePoint;
            foreach (IDrawObject obj in this.Copies)
            {
                //obj.Move(offset);
            }
            this.ucCanvas.OpenGLDraw();
            return true;
        }

        public void HandleMouseDownForMove(UnitPoint mousePoint, UnitPoint snappoint)
        {
            UnitPoint p = snappoint.IsEmpty ? mousePoint : snappoint;
            
            if (this.originals.Count == 0) // first step of move
            {
                foreach (IDrawObject obj in this.drawLayer.SelectObjects)
                {
                    this.originals.Add(obj);
                    this.Copies.Add(obj.Clone());
                }
                
                this.OriginalPoint = p;
                this.LastPoint = p;
            }
            else // move complete
            {
                double x = p.X - this.OriginalPoint.X;
                double y = p.Y - this.OriginalPoint.Y;
                UnitPoint offset = new UnitPoint(x, y);
                //if ((Control.ModifierKeys & Keys.Control) == Keys.Control)//do copy
                //{
                //    this.ucCanvas.Model.CopyObjects(offset, this.originals);
                //}
                //else // do move
                //{
                //    this.ucCanvas.Model.MoveObjects(offset, this.originals);
                //    foreach (IDrawObject obj in this.originals)
                //    {
                //        if (obj is TextDraw && !(obj as TextDraw).IsCompleteDraw)
                //        {
                //            (obj as TextDraw).IsCompleteDraw = true;
                //        }
                //        this.ucCanvas.Model.AddSelectedObject(obj);
                //    }
                //}
                this.originals.Clear();
                this.Copies.Clear();
            }
            this.ucCanvas.OpenGLDraw();
        }

        public void HandleCancelMove()
        {
            //if (this.originals != null && this.originals.Count > 0)
            //{
            //    this.ucCanvas.Model.DeleteObjects(originals);
            //}
            //foreach (IDrawObject obj in this.Copies)
            //{
            //    this.ucCanvas.Model.AddSelectedObject(obj);
            //    this.ucCanvas.Model.AddObjectOnRedoUndo(obj);
            //}
            //this.originals.Clear();
            //this.Copies.Clear();
            //this.ucCanvas.DoInvalidate(true);
        }
    }
}
