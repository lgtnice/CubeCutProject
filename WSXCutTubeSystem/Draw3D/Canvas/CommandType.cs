using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.Draw3D.Canvas
{
    public enum CommandType
    {
        None,//无操作
        SetStartPoint,//设置起点
        SetCoolPoint,//设置冷却点
        SetMicroConnect,//设置微连
        Measure,//测量
        CutOff, //切断
                //Draw,//绘图
                //Smothing,//倒圆角
                //UnSmothing,//释圆角
                //MovePosition,//平移图形
                //AnyMirror,//任意角度镜像
                //AnyRotate,//任意角度旋转
                //InteractScale,//交互式缩放
                //AbsAnchor,//绝对停靠
                //ArrayInteractive,//交互式阵列
                //ArrayAnnular,//环形阵列
                //Bridge,//桥接
                //CurveSegment,//曲线分割
    }
}
