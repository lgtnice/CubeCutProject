using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.DrawModel
{
    public enum FigureType
    {/// <summary>
     /// 单点
     /// </summary>
        Point,
        /// <summary>
        /// 直线
        /// </summary>
        Line,
        /// <summary>
        /// 圆弧
        /// </summary>
        Arc,
        /// <summary>
        /// 圆
        /// </summary>
        Circle,
        /// <summary>
        /// 多线段
        /// </summary>
        Polyline,
        /// <summary>
        /// 贝塞尔曲线
        /// </summary>
        PolyBezier,
        /// <summary>
        /// 椭圆
        /// </summary>
        Ellipse,
        /// <summary>
        /// 文字
        /// </summary>
        Text,
        /// <summary>
        /// 曲线
        /// </summary>
        Spline,
        /// <summary>
        /// 组合曲线
        /// </summary>
        GeoCurve,
    }
}
