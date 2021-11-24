using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.GlobalData.Messenger
{
    public class MainEvent
    {

        /// <summary>
        /// 画图日志框 开始输入参数
        /// </summary>
        public readonly static string OnStartInputParam = "OnStartInputParam";
        /// <summary>
        /// 画图日志框 结束输入参数(鼠标操作方式:显示鼠标操作的参数，显示"结束")
        /// </summary>
        public readonly static string OnMouseDownEndInput = "OnMouseDownEndInput";
        /// <summary>
        /// 添加画图日志
        /// </summary>
        public readonly static string OnAddLogDrawInfos = "OnAddLogDrawInfos";
        /// <summary>
        /// 添加系统日志
        /// </summary>
        public readonly static string OnAddLogSystemInfos = "OnAddLogSystemInfos";
        /// <summary>
        /// 添加警告日志
        /// </summary>
        public readonly static string OnAddLogAlarmInfos = "OnAddLogAlarmInfos";
    }
}
