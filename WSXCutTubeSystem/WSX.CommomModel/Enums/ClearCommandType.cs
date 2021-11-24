using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.Enums
{
    /// <summary>
    /// 清除操作类型
    /// </summary>
    public enum ClearCommandType
    {
        /// <summary>
        /// 引线
        /// </summary>
        LeadLine,
        /// <summary>
        /// 微连
        /// </summary>
        MicroConnect,
        /// <summary>
        /// 冷却点
        /// </summary>
        CoolPoint,
        /// <summary>
        /// 补偿
        /// </summary>
        Compensation,
        /// <summary>
        /// 缺口
        /// </summary>
        Gap
    }
}
