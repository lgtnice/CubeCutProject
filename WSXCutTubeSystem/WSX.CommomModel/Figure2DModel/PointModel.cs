using System;
using System.Xml.Serialization;
using WSX.CommomModel.DrawModel;

namespace WSX.CommomModel.Figure2DModel
{
    /// <summary>
    /// 单点
    /// </summary>
    [Serializable]
    public class PointModel : FigureBase2DModel
    {
        [XmlElement("Location")]
        public UnitPoint Point { get; set; }
        public PointModel()
        {
            Type = FigureType.Point;
        }
    }
}
