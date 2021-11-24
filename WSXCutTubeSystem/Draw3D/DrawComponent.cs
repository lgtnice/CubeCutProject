using SharpGL;
using System;
using System.Drawing;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.Enums;
using WSX.CommomModel.ParaModel;
using WSX.Draw3D.Canvas;
using WSX.Draw3D.Common;
using WSX.Draw3D.Utils;
using WSX.GlobalData.Messenger;
using WSX.GlobalData.Model;

namespace WSX.Draw3D
{
    public partial class DrawComponent : UserControl
    {
        public IModel DataModel { get; private set; }
        public DrawComponent()
        {
            InitializeComponent();
            this.DataModel = new DataModel();
            this.ucCanvas1.SetDataModel(this.DataModel);
        }

        /// <summary>
        /// 重新绘制图形
        /// </summary>
        public void DoOpenGLDraw()
        {
            this.ucCanvas1.OpenGLDraw();
        }

        public void SwitchViewModel(ViewModel viewModel)
        {
            this.ucCanvas1.SwitchViewModel(viewModel);
        }

        public void DrawNewObject(StandardTubeMode standardTubeMode)
        {
            StandardTubeConverter converter = new StandardTubeConverter(standardTubeMode);
            this.DataModel.DrawLayer.Clear();
            this.DataModel.MarkLayer.Clear();
            var tube = converter.GetTubeObjects();
            this.DataModel.DrawLayer.AddRange(tube.Item1);
            this.DataModel.MarkLayer.AddRange(tube.Item2);
            this.DataModel.MarkLayer.SectionObject = converter.GetSectionObject();
            this.DataModel.TubeMode = standardTubeMode;
            this.ucCanvas1.OpenGLDraw();
        }

        public void OnLayerIdChanged()
        {
            var objects = this.DataModel.DrawLayer.SelectObjects;
            if (objects.Count > 0)
            {
                objects.ForEach(e => e.LayerId = (int)GlobalModel.CurrentLayerId);
                this.ucCanvas1.OpenGLDraw();
            }
        }

        public void SwitchOperationModel(OperationMode operationMode)
        {
            this.ucCanvas1.SwitchOperationModel(operationMode);
        }

        public void DoArray()
        {
            this.DataModel.DoArray();
            this.DoOpenGLDraw();
        }

        #region 设置操作指令(起点、微连、冷却点、测量、None)
        public void OnSettingStartPoint()
        {
            this.ucCanvas1.SetCommandType(Canvas.CommandType.SetStartPoint);
        }
        public void OnSettingMicroConnect()
        {
            this.ucCanvas1.SetCommandType(Canvas.CommandType.SetMicroConnect);
            this.ucCanvas1.SetCursor(CommandType.SetMicroConnect);
        }
        public void OnSettingMeasure()
        {
            this.ucCanvas1.SetCommandType(Canvas.CommandType.Measure);
        }
        public void OnSettingCoolPoint()
        {
            this.ucCanvas1.SetCommandType(Canvas.CommandType.SetCoolPoint);
            this.ucCanvas1.SetCursor(CommandType.SetCoolPoint);
        }
        public void OnSettingCommandTypeNone()
        {
            this.ucCanvas1.SetCommandType(Canvas.CommandType.None);
        }

        public void OnSettingCutOff()
        {
            this.ucCanvas1.SetCommandType(Canvas.CommandType.CutOff);
            this.ucCanvas1.StartSetCutOff();
            this.DoOpenGLDraw();

            //Messenger.Instance.UnRegister("keyInputAction");
            //Messenger.Instance.Register("keyInputAction", CutOffInputAction);
            Messenger.Instance.Send(MainEvent.OnStartInputParam, new CommandEntity()
            {
                CommandName = "切断",
                ParaDataFormat = InputDataFormat.FloatFormat,
                ParameterName = "[请输入切断线离左端面的距离(mm)]",
                ShowCommandName = true,
                CallbackAction = CutOffInputAction
            });
        }

        private void CutOffInputAction(Object obj)
        {
            // 校验

            float z = float.NaN;
            float.TryParse(obj as string, out z);

            this.ucCanvas1.KeyInputCutOff(z);
        }
        #endregion

        #region 模式切换(选择，旋转，手工排序，移动)
        private void tsSelectMode_Click(object sender, EventArgs e)
        {
            this.ucCanvas1.SwitchOperationModel(OperationMode.Select);
        }

        private void tsViewMode_Click(object sender, EventArgs e)
        {
            this.ucCanvas1.SwitchOperationModel(OperationMode.View);
        }

        private void tsMoveMode_Click(object sender, EventArgs e)
        {
            this.ucCanvas1.SwitchOperationModel(OperationMode.Move);
        }

        private void tsSortMode_Click(object sender, EventArgs e)
        {
            this.ucCanvas1.SwitchOperationModel(OperationMode.Sort);
        }

        #endregion

        #region 视角选择
        private void tsMain_Click(object sender, EventArgs e)
        {
            this.ucCanvas1.SwitchViewModel(ViewModel.Main);
        }

        private void tsBack_Click(object sender, EventArgs e)
        {
            this.ucCanvas1.SwitchViewModel(ViewModel.Back);
        }

        private void tsLeft_Click(object sender, EventArgs e)
        {
            this.ucCanvas1.SwitchViewModel(ViewModel.Left);
        }

        private void tsRight_Click(object sender, EventArgs e)
        {
            this.ucCanvas1.SwitchViewModel(ViewModel.Right);
        }

        private void tsTop_Click(object sender, EventArgs e)
        {
            this.ucCanvas1.SwitchViewModel(ViewModel.Down);
        }

        private void tsUp_Click(object sender, EventArgs e)
        {
            this.ucCanvas1.SwitchViewModel(ViewModel.Up);
        }

        private void tsSouthEast_Click(object sender, EventArgs e)
        {
            this.ucCanvas1.SwitchViewModel(ViewModel.EastSouth);
        }

        private void tsSouthWest_Click(object sender, EventArgs e)
        {
            this.ucCanvas1.SwitchViewModel(ViewModel.WestSouth);
        }

        private void tsNorthEast_Click(object sender, EventArgs e)
        {
            this.ucCanvas1.SwitchViewModel(ViewModel.EastNorth);
        }

        private void tsNorthWest_Click(object sender, EventArgs e)
        {
            this.ucCanvas1.SwitchViewModel(ViewModel.WestNorth);
        }
        #endregion
    }
}
