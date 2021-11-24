using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 阵列参数
    /// </summary>
    public class ArrayModel
    {
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { set; get; } = 5;

        public ArrayMode ArrayMode { set; get; } = ArrayMode.Gap;

        /// <summary>
        /// 距离(偏移/间距)
        /// </summary>
        public float Distance { set; get; } = 0;

        /// <summary>
        /// 管道长度
        /// </summary>
        public float TubeLength { set; get; }

        /// <summary>
        /// 仅复制选中图像
        /// </summary>
        public bool IsOnlyCopySelected { set; get; } = false;

        /// <summary>
        /// 是否有选中的图形
        /// </summary>
        public bool HasSelectedObject { set; get; }
    }

    public enum ArrayMode
    {
        /// <summary>
        /// 间距
        /// </summary>
        Gap,

        /// <summary>
        /// 偏移
        /// </summary>
        Offset
    }
}
