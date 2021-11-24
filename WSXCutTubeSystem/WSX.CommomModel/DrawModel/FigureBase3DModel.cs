using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.DrawModel.Compensation;
using WSX.CommomModel.DrawModel.MicroConn;
using WSX.CommomModel.Figure3DModel;

namespace WSX.CommomModel.DrawModel
{
    /// <summary>
    /// 图形基类
    /// </summary>
    public abstract class FigureBase3DModel
    {
        /// <summary>
        /// 图形类型
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public FigureType Type { get; protected set; }
        [XmlAttribute]
        public int SN { get; set; }
        [XmlAttribute]
        public int LayerId { get; set; }
        [XmlAttribute]
        public bool IsLineBold { get; set; }
        [XmlAttribute]
        public bool IsSelected { get; set; }
        [XmlAttribute]
        public bool IsVisible { get; set; }
        [XmlAttribute]
        public bool IsLocked { get; set; }
        [XmlAttribute]
        public bool IsClosed { get; set; }
        /// <summary>
        /// 是否是辅助线
        /// </summary>
        [XmlAttribute]
        public bool IsMark { get; set; }
        [XmlAttribute]
        public bool IsInnerCut { get; set; }
        public CompensationModel CompensationParam { get; set; }
        public List<MicroConnectModel> MicroConnParams { set; get; }

    }
}
