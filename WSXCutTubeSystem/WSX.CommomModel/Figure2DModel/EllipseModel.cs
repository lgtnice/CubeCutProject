﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.ParaModel;

namespace WSX.CommomModel.Figure2DModel
{
    /// <summary>
    /// 椭圆
    /// </summary>
    [Serializable]
    public class EllipseModel  : FigureBase2DModel
    {
        
        ///// <summary>
        ///// 长轴
        ///// </summary>
        //[XmlAttribute("MajorAxis")]
        //public double MajorAxis { get; set; }
        ///// <summary>
        ///// 短轴
        ///// </summary>
        //[XmlAttribute("MinorAxis")]
        //public double MinorAxis { get; set; }
        ///// <summary>
        ///// 旋转值
        ///// </summary>
        //[XmlAttribute("Rotation")]
        //public double Rotation { get; set; }
        ///// <summary>
        ///// 起始角
        ///// </summary>
        //[XmlAttribute("StartAngle")]
        //public double StartAngle { get; set; }
        ///// <summary>
        ///// 结束角
        ///// </summary>
        //[XmlAttribute("EndAngle")]
        //public double EndAngle { get; set; }
        ///// <summary>
        ///// 中心坐标
        ///// </summary>
        //public UnitPoint Center { get; set; }
        public EllipseModel()
        {
            Type = FigureType.Ellipse;
        }

        public List<Segment> BezierPoints { set; get; }



    }
}
