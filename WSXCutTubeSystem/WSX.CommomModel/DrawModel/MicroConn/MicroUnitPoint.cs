using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.DrawModel.MicroConn
{
    public class MicroUnitPoint
    {
        //public UnitPointBulge Point { get; set; }

        public Point3D Point { get; set; }
        public MicroConnectFlags Flags { get; set; }
        /// <summary>
        /// 与起点的距离
        /// </summary>
        public double StartLength { get; set; }
        /// <summary>
        /// 当前跨过的长度size,结束点默认为零
        /// </summary>
        public double SizeLength { get; set; }
        public int OwerPos { get; set; }

        /// <summary>
        /// true 图形上的点 false 补偿图形上的点
        /// </summary>
        public bool IsBasePoint { get; set; } = true;
    }
}
