using System;
using System.Xml.Serialization;
using WSX.CommomModel.DrawModel;

namespace WSX.CommomModel.Figure2DModel
{
    [Serializable]
    public class TextModel : FigureBase2DModel
    {
        public TextModel()
        {
            Type = FigureType.Text;
        }

        /// <summary>
        /// 文字
        /// </summary>
        public String Text { set; get; } = "深圳";

        /// <summary>
        /// 字体
        /// </summary>
        [XmlAttribute("FontName")]
        public String FontName { set; get; } = "宋体";

        /// <summary>
        /// 宽度
        /// </summary>
        [XmlAttribute("Width")]
        public double Width { set; get; } = 100.0;
        /// <summary>
        /// 高度
        /// </summary>
        [XmlAttribute("Hight")]
        public double Hight { set; get; } = 10.0;

        /// <summary>
        /// 文字倾斜角度
        /// </summary>
        [XmlAttribute("Angle")]
        public double Angle { set; get; } = 0.0;

        /// <summary>
        /// 位置
        /// </summary>
        public UnitPoint Location { set; get; } = new UnitPoint(0.0, 0.0);

        /// <summary>
        /// 文字旋转角度
        /// </summary>
        [XmlAttribute("RotateAngle")]
        public double RotateAngle { set; get; } = 0;

        /// <summary>
        /// 垂直镜像
        /// </summary>
        [XmlAttribute("Mirror")]
        public bool Mirror { set; get; } = false;
    }
}
