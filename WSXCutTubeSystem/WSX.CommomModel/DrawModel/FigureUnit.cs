using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.DrawModel
{
    /// <summary>
    /// 图形单元/构成单元(起始、结束截面、切断、包覆)
    /// </summary>
    public enum FigureUnit
    {
        /// <summary>
        /// 起始端面
        /// </summary>
        StartFace,
        /// <summary>
        /// 结束端面
        /// </summary>
        EndSection,
        /// <summary>
        /// 截面
        /// </summary>
        //Section,

        /// <summary>
        /// 切断
        /// </summary>
        CutOff,
        /// <summary>
        /// 包覆
        /// </summary>
        Coated,
    }
}
