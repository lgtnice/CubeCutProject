using System;
using System.Collections.Generic;
using WSX.CommomModel.DrawModel;

namespace WSX.CommomModel.Figure2DModel
{
    /// <summary>
    /// 贝塞尔曲线
    /// </summary>
    [Serializable]
    public class PolyBezierModel : FigureBase2DModel
    {
        /// <summary>
        /// 曲线点集合
        /// </summary>
        public List<UnitPoint> Points { get; set; } = new List<UnitPoint>();
        public PolyBezierModel()
        {
            Type = FigureType.PolyBezier;
        }
    }
}
