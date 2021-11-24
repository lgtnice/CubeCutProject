using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 缺口参数
    /// </summary>
    public class GapModel
    {
        /// <summary>
        /// 缺口大小
        /// </summary>
        public float GapSize { set; get; } = 2.0f;

        /// <summary>
        /// true 对选中图形有效 false 对所有图形有效
        /// </summary>
        public bool IsEnableForSelected { set; get; }
    }
}
