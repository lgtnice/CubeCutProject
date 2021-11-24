using System;
using System.Drawing;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.Enums;
using WSX.GlobalData.Messenger;
using WSX.Logger;

namespace WSXCutTubeSystem.Views.UCControl
{
    public partial class UCLogDetail : UserControl
    {
        Font fontCommon = new Font("宋体", 11, FontStyle.Regular);
        Font fontBoldForKeyDown = new Font("宋体", 11, FontStyle.Bold);
        
        public UCLogDetail()
        {
            InitializeComponent();
            RegisterAction();
        }

        private void RegisterAction()
        {
            Messenger.Instance.Register(MainEvent.OnStartInputParam, StartInputParam);
            Messenger.Instance.Register(MainEvent.OnMouseDownEndInput, MouseDownEndInput);

            Messenger.Instance.Register(MainEvent.OnAddLogDrawInfos, AddDrawingInfos);
            Messenger.Instance.Register(MainEvent.OnAddLogSystemInfos, AddSystemInfos);
        }

        #region 画图框显示信息，输入参数，参数输入完成回车后处理
        /// <summary>
        /// 当前处理的参数(输入参数/回车)
        /// </summary>
        private CommandEntity CommandInfo;
        /// <summary>
        /// 输入参数回车后的处理时间
        /// </summary>
        private const String handleActionKey = "ActionAfterEnter";

        /// <summary>
        /// 开始参数输入(注册参数处理事件，显示提示信息)
        /// </summary>
        /// <param name="obj"></param>
        private void StartInputParam(Object obj)
        {
            var commandInfo = obj as CommandEntity;
            if (commandInfo == null)
                return;

            CommandInfo = commandInfo;
            Messenger.Instance.UnRegister(handleActionKey);
            Messenger.Instance.Register(handleActionKey, commandInfo.CallbackAction);

            if (commandInfo.ShowCommandName)
            {
                this.AddDrawingInfos(new LogInfoEntity(String.Format("命令:{0}", commandInfo.CommandName),true));
            }
            this.AddDrawingInfos(new LogInfoEntity(commandInfo.ParameterName, true));
        }

        /// <summary>
        /// 鼠标完成操作(不输入参数)则显示参数及"结束"并且情况当前处理的参数信息CommandInfo
        /// </summary>
        /// <param name="obj"></param>
        private void MouseDownEndInput(Object obj)
        {
            String param = obj as string;
            if (!string.IsNullOrEmpty(param))
            {
                rtxDrawInfos.SelectionColor = Color.Blue;
                rtxDrawInfos.SelectionFont = fontBoldForKeyDown;
                rtxDrawInfos.AppendText(param);
                rtxDrawInfos.AppendText(Environment.NewLine);
            }

            this.CommandInfo = null;
            rtxDrawInfos.SelectionColor = Color.Black;
            rtxDrawInfos.SelectionFont = fontCommon;
            rtxDrawInfos.AppendText("结束");
        }

        /// <summary>
        /// 画图框显示日志
        /// </summary>
        /// <param name="obj"></param>
        private void AddDrawingInfos(Object obj)
        {
            var logInfo = obj as LogInfoEntity;
            if (logInfo == null)
                return;

            this.rtxDrawInfos.Invoke(new Action(() =>
            {
                this.tabLogDetailInfo.SelectedTabPageIndex = 0;

                rtxDrawInfos.SelectionColor = logInfo.Color;
                rtxDrawInfos.Focus();
                rtxDrawInfos.Select(rtxDrawInfos.TextLength, 0);
                rtxDrawInfos.ScrollToCaret();
                if (!string.IsNullOrWhiteSpace(GetLineFromText()))
                {
                    rtxDrawInfos.AppendText(Environment.NewLine);
                }
                rtxDrawInfos.AppendText(logInfo.LogInfo);
                if (logInfo.EndNewLine)
                {
                    rtxDrawInfos.AppendText(Environment.NewLine);
                }
            }));
        }

        /// <summary>
        /// 回车回调处理事件
        /// </summary>
        private void rtxDrawInfos_KeyDown(object sender, KeyEventArgs e)
        {
            rtxDrawInfos.SelectionColor = Color.Blue;
            rtxDrawInfos.SelectionFont = fontBoldForKeyDown;
            if (e.KeyCode != Keys.Enter || this.CommandInfo == null)//Enter
                return;

            string lineStr = GetLineFromText();
            if (string.IsNullOrWhiteSpace(lineStr))
                return;

            dynamic param = null;
            if(this.TryConvert(lineStr,out param))
            {
                this.CommandInfo = null;
                Messenger.Instance.Send(handleActionKey, lineStr);

                rtxDrawInfos.SelectionFont = fontCommon;
                rtxDrawInfos.SelectionColor = Color.Black;
                rtxDrawInfos.AppendText(Environment.NewLine);
                rtxDrawInfos.AppendText("完成");
            }
            else
            {
                //参数无效 提示
                this.ShowErrorMsg(lineStr);
            }
        }

        public void ShowErrorMsg(string param)
        {
            rtxDrawInfos.SelectionFont = fontCommon;
            rtxDrawInfos.SelectionColor = Color.Black;

            rtxDrawInfos.AppendText(Environment.NewLine);
            rtxDrawInfos.AppendText(string.Format(@"参数“{0}”无效", param));
        }

        /// <summary>
        /// 参数校验即转换
        /// </summary>
        /// <param name="paramText"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private bool TryConvert(string paramText, out dynamic param)
        {
            param = null;
            if (string.IsNullOrWhiteSpace(paramText))
                return false;

            bool result = false;
            switch (this.CommandInfo.ParaDataFormat)
            {
                case InputDataFormat.StringFormat:
                    param = paramText.Trim();
                    result = true;
                    break;
                case InputDataFormat.DoubleFormat:
                    double value = double.NaN;
                    result = double.TryParse(paramText.Trim(), out value);
                    param = value;
                    break;
                case InputDataFormat.FloatFormat:
                    float valueF = float.NaN;
                    result = float.TryParse(paramText.Trim(), out valueF);
                    param = valueF;
                    break;
                case InputDataFormat.IntFormat:
                    int valueI = 0;
                    result = Int32.TryParse(paramText.Trim(), out valueI);
                    param = valueI;
                    break;
                case InputDataFormat.PointFormat:
                    String[] texts = paramText.Trim().Split(',');
                    if(texts.Length != 2)
                    {
                        return false;
                    }
                    float v = float.NaN;
                    PointF pf = PointF.Empty;
                    if (float.TryParse(texts[0], out v))
                    {
                        pf.X = v;

                        if (float.TryParse(texts[1], out v))
                        {
                            pf.Y = v;
                            result = true;
                            param = pf;
                        }
                    }
                    break;
                case InputDataFormat.Point3DFormat:
                    String[] texts1 = paramText.Trim().Split(',');
                    if (texts1.Length != 3)
                    {
                        return false;
                    }
                    float v3d = float.NaN;
                    Point3D p3d = new Point3D();
                    if (float.TryParse(texts1[0], out v3d))
                    {
                        p3d.X = v3d;

                        if (float.TryParse(texts1[1], out v3d))
                        {
                            p3d.Y = v3d;
                            if (float.TryParse(texts1[2], out v3d))
                            {
                                p3d.Z = v3d;
                                result = true;
                                param = p3d;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            return result;
        }

        private string GetLineFromText()
        {
            int index = rtxDrawInfos.GetFirstCharIndexOfCurrentLine();
            int line = rtxDrawInfos.GetLineFromCharIndex(index);
            string lineStr = "";
            if (line >= 0 && rtxDrawInfos.Lines.Length >=line+1)
            {
                lineStr = rtxDrawInfos.Lines[line];
            }
            return lineStr;
        }
        #endregion

        /// <summary>
        /// 增加系统消息
        /// </summary>
        /// <param name="obj"></param>

        private void AddSystemInfos(object obj)
        {
            if (obj is object[])
            {
                #region Code by Liang
                object[] msgs = obj as object[];
                string time = $"({DateTime.Now.ToString("MM/dd HH:mm:ss")})";
                string content = msgs[0].ToString() + Environment.NewLine;
                //string msg = string.Format(@"({0}){1}{2}", time, msgs[0].ToString(), Environment.NewLine);
                string msg = time + content;
                LogLevel logLevel = (LogLevel)msgs[1];
                Color color = GetColor(logLevel);

                this.rtxSystemInfos.BeginInvoke(new Action(() =>
                {
                    rtxSystemInfos.AppendText(time);
                    rtxSystemInfos.Select(rtxSystemInfos.TextLength, 0);
                    rtxSystemInfos.SelectionColor = color;
                    rtxSystemInfos.AppendText(content);
                }));
                this.WriteLog(msg, logLevel);
                #endregion
            }
            else
            {
                #region Code by HB
                var logInfo = obj as Tuple<string, Color>;
                string time = $"({DateTime.Now.ToString("MM/dd HH:mm:ss")})";
                string content = logInfo.Item1.ToString() + Environment.NewLine;
                string msg = time + content;
                this.rtxSystemInfos.BeginInvoke(new Action(() =>
                {
                    rtxSystemInfos.AppendText(time);
                    rtxSystemInfos.Select(rtxSystemInfos.TextLength, 0);
                    rtxSystemInfos.SelectionColor = logInfo.Item2;
                    rtxSystemInfos.AppendText(content);
                }));
                this.WriteLog(msg, LogLevel.Info);
                #endregion
            }
        }

        private void WriteLog(string msg, LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug: LogUtil.Instance.Debug(msg); break;
                case LogLevel.Error: LogUtil.Instance.Error(msg); break;
                case LogLevel.Fatal: LogUtil.Instance.Fatal(msg); break;
                case LogLevel.Info: LogUtil.Instance.Info(msg); break;
                case LogLevel.Warn: LogUtil.Instance.Warn(msg); break;
                default: break;
            }
        }

        private Color GetColor(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug: return Color.Blue;
                case LogLevel.Error: return Color.Red;
                case LogLevel.Fatal: return Color.GreenYellow;
                case LogLevel.Info: return Color.Green;
                case LogLevel.Warn: return Color.LightSkyBlue;
                default: return Color.Black;
            }
        }
    }
}
