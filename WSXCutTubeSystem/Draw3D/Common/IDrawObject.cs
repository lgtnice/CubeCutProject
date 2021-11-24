using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.DrawModel.Compensation;
using WSX.CommomModel.DrawModel.MicroConn;
using WSX.CommomModel.Enums;
using WSX.CommomModel.ParaModel;

namespace WSX.Draw3D.Common
{
    public interface IDrawObject
    {
        FigureType Type { get; set; }
        FigureUnit FigureUnit { set; get; }
        float MinZ { set; get; }
        float MaxZ { set; get; }
        int SN { get; set; }
        int LayerId { get; set; }
        bool IsLineBold { get; set; }//绘图加粗显示
        bool IsSelected { get; set; }
        bool IsVisible { get; set; }//隐藏或者显示
        bool IsClosed { get; set; }
        bool IsLocked { get; set; }//是否锁定
        bool IsInnerCut { get; set; }
        float SizeLength { get; }
        Point3D StartMovePoint { set; get; }
        Point3D EndMovePoint { set; get; }
        CompensationModel CompensationParam { get; set; }
        List<MicroConnectModel> MicroConnParams { set; get; }
        void Draw(OpenGL gl, float[] color);

        bool PointInObject(float[] matrix, PointF point);
        bool ObjectInRectangle(float[] matrix, RectangleF rect,bool anyPoint);
		void Update();

        IDrawObject Clone();

        void Clear(ClearCommandType commandType);

        void MoveAxisZ(float offSet);

        void SetPathStartParam(PointF hitPoint);

        void SetCoolPoint(PointF hitPoint);

        void SetGap(GapModel gapModel);

        void Reverse();

        void SetMicroConnect(PointF hitPoint, MicroConnectParam connectParam);

        void DoCompensation(CompensationModel param);

        void ShowMachinePath(float[] matrix, OpenGL gl,  float[] color);
        void ShowBoundRect(OpenGL gl);
        void ShowFigureSN(OpenGL gl);
        void ShowStartMovePoint(OpenGL gl);
    }
}
