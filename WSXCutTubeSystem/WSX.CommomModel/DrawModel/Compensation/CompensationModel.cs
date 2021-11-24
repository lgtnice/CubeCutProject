using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WSX.CommomModel.DrawModel.Compensation
{
    /// <summary>
    /// 补偿参数
    /// </summary>
    [Serializable]
    public class CompensationModel
    {
        /// <summary>
        /// 补偿样式
        /// </summary>
        [XmlAttribute("Style")]
        public CompensationType Style { get; set; } = CompensationType.Auto;
        /// <summary>
        /// 补偿宽度
        /// </summary>
        [XmlAttribute("Size")]
        public double Size { get; set; } = 1.0d;

        /// <summary>
        /// 是否只对选中图形起效
        /// </summary>
        public bool IsEnableForSelected { get; set; } = false;

        ///// <summary>
        ///// 拐角处理方式，直角-false,圆角-true
        ///// </summary>
        //[XmlAttribute("Smooth")]
        //public bool IsSmooth { get; set; }
    }
}
