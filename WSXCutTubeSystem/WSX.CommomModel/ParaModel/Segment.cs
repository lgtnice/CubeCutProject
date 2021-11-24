using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;

namespace WSX.CommomModel.ParaModel
{
    [Serializable]
    public class Segment
    {
        /// <summary>
        /// 一段贝塞尔曲线的几个点: 首段 4个点，其他段3个点(与前一段的最后一个点共4个点确定一条贝塞尔曲线)
        /// </summary>
        public List<UnitPoint> UnitPoints { set; get; }
    }
}
