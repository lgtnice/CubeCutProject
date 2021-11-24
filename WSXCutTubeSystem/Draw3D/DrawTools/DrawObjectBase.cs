using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.DrawModel.Compensation;
using WSX.CommomModel.DrawModel.MicroConn;
using WSX.CommomModel.Enums;
using WSX.CommomModel.ParaModel;
using WSX.Draw3D.Common;

namespace WSX.Draw3D.DrawTools
{
    public class DrawObjectBase : IDrawObject
    {
        public FigureType Type { get; set; }
        public FigureUnit FigureUnit { set; get; }
        public int SN { get; set; }

        public virtual float MinZ { set; get; }
        public virtual float MaxZ { set; get; }

        public virtual int LayerId { get; set; }
        public virtual bool IsLineBold { get; set; }
        public virtual bool IsSelected { get; set; }
        public virtual bool IsVisible { get; set; }
        public virtual bool IsClosed { get; set; }
        public virtual bool IsLocked { get; set; }
        public bool IsInnerCut { get; set; }
        public virtual float SizeLength { get; protected set; }
        public Point3D StartMovePoint { set; get; }
        public Point3D EndMovePoint { set; get; }
        public CompensationModel CompensationParam { get; set; }
        public List<MicroConnectModel> MicroConnParams { set; get; }

        public DrawObjectBase()
        {
            IsVisible = true;
        }
        public virtual void Draw(OpenGL gl, float[] color)
        {

        }

        public virtual bool ObjectInRectangle(float[] matrix, RectangleF rect, bool anyPoint)
        {
            return false;
        }

        public virtual bool PointInObject(float[] matrix, PointF point)
        {
            return false;
        }
        public virtual void ShowMachinePath(float[] matrix, OpenGL gl, float[] color)
        {

        }
        public virtual void ShowBoundRect(OpenGL gl) { }
        public virtual void ShowFigureSN(OpenGL gl) { }
        public virtual void ShowStartMovePoint(OpenGL gl) { }
        public virtual void Update()
        {

        }

        public virtual IDrawObject Clone()
        {
            return null;
        }
        public virtual void Copy(IDrawObject source)
        {
            this.Type = source.Type;
            this.SN = source.SN;
            this.MinZ = source.MinZ;
            this.MaxZ = source.MaxZ;
            this.LayerId = source.LayerId;
            this.IsLineBold = source.IsLineBold;
            this.IsSelected = source.IsSelected;
            this.IsVisible = source.IsVisible;
            this.IsClosed = source.IsClosed;
            this.IsLocked = source.IsLocked;
            this.IsInnerCut = source.IsInnerCut;
        }

        public virtual void Clear(ClearCommandType commandType)
        {
        }

        public virtual void MoveAxisZ(float offset)
        {
        }

        public virtual void SetPathStartParam(PointF hitPoint)
        {
        }

        public virtual void SetCoolPoint(PointF hitPoint)
        {
        }

        public virtual void SetMicroConnect(PointF hitPoint, MicroConnectParam connectParam)
        {
        }

        public virtual void Reverse()
        {
        }

        public virtual void SetGap(GapModel gapModel)
        {
        }

        public virtual void DoCompensation(CompensationModel param)
        {
        }
    }
}