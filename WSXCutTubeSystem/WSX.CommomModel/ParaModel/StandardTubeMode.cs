using System;
using System.Xml.Serialization;

namespace WSX.CommomModel.ParaModel
{
    [Serializable]
    public class StandardTubeMode
    {
        public enum TubeType { Circle, Square, Rectangle, Sport }
        [XmlAttribute("Type")]
        public TubeType TubeTypes { get; set; }
        [XmlAttribute()]
        public float CircleRadius { get; set; }
        [XmlAttribute()]
        public float TubeLength { get; set; }
        [XmlAttribute()]
        public float LeftAngle { get; set; }
        [XmlAttribute()]
        public float RightAngle { get; set; }
        [XmlAttribute()]
        public float TubeTotalLength { get; set; }
        //public float SideLength { get; set; }
        //public float RoundRadius { get; set; }
        [XmlAttribute()]
        public float LongSideLength { get; set; }
        [XmlAttribute()]
        public float ShortSideLength { get; set; }
        //public float Diameter { get; set; }

        public override String ToString()
        {
            String msg = string.Empty;
            switch (TubeTypes)
            {
                case(TubeType.Circle):
                    msg = String.Format("圆管 R{0}×{1}", CircleRadius, TubeTotalLength);
                    break;
                case (TubeType.Square):
                    msg = String.Format("方形管 {0}×{1} R{2}×{3}", LongSideLength, ShortSideLength, CircleRadius, TubeTotalLength);
                    break;
                case (TubeType.Rectangle):
                    msg = String.Format("矩形管 {0}×{1} R{2}×{3}", LongSideLength, ShortSideLength, CircleRadius, TubeTotalLength);
                    break;
                case (TubeType.Sport):
                    msg = String.Format("跑道管 W{0} R{1}×{2}", LongSideLength, CircleRadius, TubeTotalLength);
                    break;
                default:
                    String.Format("异形管 {0}", TubeTotalLength);
                    break;
            }
            return msg;
        }
    }
}
