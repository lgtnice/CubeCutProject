using System.Drawing;

namespace WSX.CommomModel.DrawModel
{
    public class LogInfoEntity
    {
        public LogInfoEntity(string logInfo)
        {
            this.LogInfo = logInfo;
            this.Color = Color.Black;
            this.EndNewLine = false;
        }

        public LogInfoEntity(string logInfo, Color color)
        {
            this.LogInfo = logInfo;
            this.Color = color;
            this.EndNewLine = false;
        }

        public LogInfoEntity(string logInfo, bool endNewLine)
        {
            this.LogInfo = logInfo;
            this.Color = Color.Black;
            this.EndNewLine = endNewLine;
        }

        public LogInfoEntity(string logInfo, Color color, bool endNewLine)
        {
            this.LogInfo = logInfo;
            this.Color = color;
            this.EndNewLine = endNewLine;
        }

        public string LogInfo { set; get; }
        public Color Color { set; get; }

        /// <summary>
        /// 结尾是否换行(如:输入参数一般换行)
        /// </summary>
        public bool EndNewLine { set; get; }
    }
}
