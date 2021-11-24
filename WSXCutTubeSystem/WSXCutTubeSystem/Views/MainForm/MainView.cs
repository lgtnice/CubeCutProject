using DevExpress.XtraBars;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.Enums;
using WSX.CommomModel.Utilities;
using WSX.Draw3D.Common;
using WSX.GlobalData.Messenger;
using WSX.GlobalData.Model;
using WSX.ViewModels;
using WSX.WSXCut.Views.CustomControl;
using WSXCutTubeSystem.Manager;
using WSXCutTubeSystem.Models;
using WSXCutTubeSystem.Views;
using WSXCutTubeSystem.Views.Forms;
using WSXCutTubeSystem.Views.UCControl;

namespace WSXCutTubeSystem
{
    public partial class MainView : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public string ProjectFilePath { get; internal set; }
        private readonly string TITLE_FORMAT = "{0} - 万顺兴激光切管平台" + VersionInfo.SoftVersion + "{1}";
        private readonly UCStatusBarInfo statusInfo;
        public MainView()
        {
            DispatcherHelper.Initialize(this);
            InitializeComponent();
            this.ucLayerParaBar1.OnLayerIdChangedEvent += (sender, e) => this.drawComponent1.OnLayerIdChanged();

            this.statusInfo = new UCStatusBarInfo
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = System.Drawing.Color.Transparent
            };
            this.statusInfo.Location = new Point(this.ribbonStatusBar1.Width - this.statusInfo.Width - 10, 0);
            this.ribbonStatusBar1.Controls.Add(this.statusInfo);
        }
        #region 主窗体事件
        private void MainView_Load(object sender, EventArgs e)
        {
            if (!mvvmContext1.IsDesignMode)
            {
                InitializeBindings();
            }
            //this.Text = string.Format(TITLE_FORMAT, FileManager.DEFAULT_FILE_NAME, SystemContext.IsDummyMode ? "(演示版)" : null);
            this.Text = string.Format(TITLE_FORMAT, FileManager.DEFAULT_FILE_NAME, true ? "(演示版)" : null);
            FileManager.Instance.OnFileNameChanged += (fileName) =>
            {
                //this.Text = string.Format(TITLE_FORMAT, fileName, SystemContext.IsDummyMode ? "(演示版)" : null);
                this.Text = string.Format(TITLE_FORMAT, fileName, true ? "(演示版)" : null);
            };
            if (!string.IsNullOrEmpty(this.ProjectFilePath))
            {
                FileManager.Instance.OpenFile(this.ProjectFilePath, this.drawComponent1.DataModel, true);
            }
        }
        private void MainView_DragDrop(object sender, DragEventArgs e)
        {
            object[] file = (object[])e.Data.GetData(DataFormats.FileDrop);
            if (file != null && file.Length > 0)
            {
                FileManager.Instance.OpenFile(file[0].ToString(), this.drawComponent1.DataModel, true);
                this.drawComponent1.SwitchViewModel(ViewModel.EastSouth);
                this.drawComponent1.DoOpenGLDraw();
            }
        }

        private void MainView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))    //判断拖来的是否是文件
            {
                object[] file = (object[])e.Data.GetData(DataFormats.FileDrop);
                if (file != null && file.Length > 0)
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(file[0].ToString());
                    if ((info.Attributes & System.IO.FileAttributes.Directory) == 0)
                    {
                        string exFileName = Path.GetExtension(file[0].ToString());
                        if (exFileName.ToUpper().Equals(".WTF") || exFileName.ToUpper().Equals(".IGS"))
                        {
                            e.Effect = DragDropEffects.All;                //是则将拖动源中的数据连接到控件
                        }
                    }
                }
            }
            else e.Effect = DragDropEffects.None;
        }
        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<MainViewModel>();
            fluent.SetBinding(this.ckShowOutLines, c => c.Checked, x => x.AdditionalInfo.IsShowBoundRect);
            fluent.SetBinding(this.ckShowSN, c => c.Checked, x => x.AdditionalInfo.IsShowFigureSN);
            fluent.SetBinding(this.ckShowStartPoint, c => c.Checked, x => x.AdditionalInfo.IsShowStartMovePoint);
            fluent.SetBinding(this.ckShowMachinePath, c => c.Checked, x => x.AdditionalInfo.IsShowMachinePath);
            fluent.SetBinding(this.ckShowEmptyMovePath, c => c.Checked, x => x.AdditionalInfo.IsShowVacantPath);
            fluent.SetBinding(this.ckShowSection, c => c.Checked, x => x.AdditionalInfo.IsShowSection);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //保存配置信息
            SerializeUtil.JsonWriteToFile(GlobalModel.Params, GlobalModel.ConfigFileName);
        }
        #endregion

        #region 视图
        private void btnDown_Click(object sender, EventArgs e)
        {
            this.drawComponent1.SwitchViewModel(ViewModel.Down);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            this.drawComponent1.SwitchViewModel(ViewModel.Up);
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            this.drawComponent1.SwitchViewModel(ViewModel.Main);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.drawComponent1.SwitchViewModel(ViewModel.Back);
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            this.drawComponent1.SwitchViewModel(ViewModel.Left);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            this.drawComponent1.SwitchViewModel(ViewModel.Right);
        }

        private void btnWestSouth_Click(object sender, EventArgs e)
        {
            this.drawComponent1.SwitchViewModel(ViewModel.WestSouth);
        }

        private void btnEastSouth_Click(object sender, EventArgs e)
        {
            this.drawComponent1.SwitchViewModel(ViewModel.EastSouth);
        }

        private void btnEastNorth_Click(object sender, EventArgs e)
        {
            this.drawComponent1.SwitchViewModel(ViewModel.EastNorth);
        }

        private void btnWestNorth_Click(object sender, EventArgs e)
        {
            this.drawComponent1.SwitchViewModel(ViewModel.WestSouth);
        }
        #endregion

        #region 模式选择
        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.drawComponent1.SwitchOperationModel(OperationMode.Select);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            this.drawComponent1.SwitchOperationModel(OperationMode.View);
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            this.drawComponent1.SwitchOperationModel(OperationMode.Sort);
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            this.drawComponent1.SwitchOperationModel(OperationMode.Move);
        }

        #endregion

        #region 文件主菜单
        private void btnOpenFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileManager.Instance.OpenFileDialog(this.drawComponent1.DataModel);
            this.drawComponent1.SwitchViewModel(ViewModel.EastSouth);
            this.drawComponent1.DoOpenGLDraw();
        }
        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileManager.Instance.SaveFile(this.drawComponent1.DataModel);
        }

        private void btnSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileManager.Instance.SaveFileDialog(this.drawComponent1.DataModel, ".WTF");
        }

        private void btnUserParam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmUserConfig frm = new FrmUserConfig();
            frm.ShowDialog();
        }

        private void btnBackupParam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnBCS100Monitor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnControlCardMonitor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnExtendIOMonitor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnDAFocusCorrection_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnGasDACorrection_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        #endregion

        #region 查看
        private FrmStandardTube frmStandardTube;
        private void btnStandardTube_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (FileManager.Instance.New(this.drawComponent1.DataModel))
            {
                this.frmStandardTube = new FrmStandardTube();
                if (this.frmStandardTube.ShowDialog() == DialogResult.OK)
                {
                    this.drawComponent1.DrawNewObject(this.frmStandardTube.standardTubeMode);
                    this.btnEastSouth_Click(null, null);
                }
            }
        }

        private void btn3DWarpper_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.drawComponent1.OnSettingCommandTypeNone();
        }

        private void btnSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnInvertSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnUnselect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnSelectNotCloseFigure_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void ckShowItems_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.drawComponent1.DoOpenGLDraw();
        }

        //private void ckShowSection_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{

        //}

        //private void ckShowSN_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{

        //}

        //private void ckShowStartPoint_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{

        //}

        //private void ckShowProcessPath_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{

        //}

        //private void ckShowEmptyMovePath_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{

        //}

        private void btnTubeInformation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IModel model = this.drawComponent1.DataModel;
            FrmTubeInfo frm = new FrmTubeInfo(model.TubeMode);
            if (frm.ShowDialog() == DialogResult.OK && model.TubeMode != null)
            {
                //this.drawComponent1.DrawNewObject(model.TubeMode);//防止对画布中源图形的改变,目前只显示信息不做修改信息功能
            }
        }
        #endregion

        #region 工艺设置
        private void btnLeadWire_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmLeadWireParams frm = new FrmLeadWireParams();
            frm.ShowDialog();
        }

        private void btnClearLeadLines_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.drawComponent1.DataModel.Clear(ClearCommandType.LeadLine);
            this.drawComponent1.DoOpenGLDraw();
        }

        private void btnClearMicroConnect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.drawComponent1.DataModel.Clear(ClearCommandType.MicroConnect);
            this.drawComponent1.DoOpenGLDraw();
        }
        private void btnClearCoolPoint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.drawComponent1.DataModel.Clear(ClearCommandType.CoolPoint);
            this.drawComponent1.DoOpenGLDraw();
        }
        private void btnCancelCompensation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.drawComponent1.DataModel.Clear(ClearCommandType.Compensation);
            this.drawComponent1.DoOpenGLDraw();
        }

        private void btnCancelGap_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.drawComponent1.DataModel.Clear(ClearCommandType.Gap);
            this.drawComponent1.DoOpenGLDraw();
        }

        private void btnStartPoint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.drawComponent1.OnSettingStartPoint();
        }

        private void btnDock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmDockPosition frm = new FrmDockPosition();
            frm.ShowDialog();
        }

        private void btnCompensation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.drawComponent1.OnSettingCommandTypeNone();
            FrmGapCompensation frm = new FrmGapCompensation(GlobalModel.Params.CompensationParam);
            DialogResult result = frm.ShowDialog(); 
            if(DialogResult.OK == result)
            {
                this.drawComponent1.DataModel.DoCompensation();
                this.drawComponent1.DoOpenGLDraw();
            }
        }

        private void btnInnerOrOutter_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.drawComponent1.OnSettingCommandTypeNone();
            this.drawComponent1.DataModel.ChangeInnerOutterCut();
            this.drawComponent1.DoOpenGLDraw();
        }

        private void btnReverse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.drawComponent1.DataModel.Reverse();
            this.drawComponent1.DoOpenGLDraw();
        }

        private void btnMicroConnect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmMicroConnectParams frm = new FrmMicroConnectParams(GlobalModel.Params.MicroConnectParam);
            DialogResult result = frm.ShowDialog();
            if(DialogResult.OK == result)
            {
                this.drawComponent1.OnSettingMicroConnect();
            }
        }

        private void btnMergeConnectLines_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmMergeConnectLine frm = new FrmMergeConnectLine();
            frm.ShowDialog();
        }

        private void btnGap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmGapSizeSet frm = new FrmGapSizeSet(GlobalModel.Params.GapParam);
            DialogResult result = frm.ShowDialog();
            if (DialogResult.OK == result)
            {
                this.drawComponent1.DataModel.SetGap();
                this.drawComponent1.DoOpenGLDraw();
            }
        }

        private void btnCoolPoint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.drawComponent1.OnSettingCoolPoint();
        }

        private void btnCutUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        #endregion

        #region 排序
        private void btnSortItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void ckSortAccordYSmallToLarge_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ckSortAccordYLargeToSmall.Checked = false;
            ckSortBySide.Checked = false;
            this.drawComponent1.DataModel.Sort(SortMode.YSmallToLarge);
            this.drawComponent1.DoOpenGLDraw();
        }

        private void ckSortAccordYLargeToSmall_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ckSortAccordYSmallToLarge.Checked = false;
            ckSortBySide.Checked = false;
            this.drawComponent1.DataModel.Sort(SortMode.YLargeToSmall);
            this.drawComponent1.DoOpenGLDraw();
        }

        private void ckSortBySide_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ckSortAccordYLargeToSmall.Checked = false;
            ckSortAccordYSmallToLarge.Checked = false;

            //按面排序?
            this.drawComponent1.DataModel.Sort(SortMode.BySide);
            this.drawComponent1.DoOpenGLDraw();
        }
        
        private void btnSortReverseQueue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.drawComponent1.DataModel.Sort(SortMode.Reverse);
            this.drawComponent1.DoOpenGLDraw();
        }
        #endregion 排序end

        #region 工具
        private void btnIntersectPipe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnCutOff_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.drawComponent1.DataModel.TubeMode == null)
                return;
            this.drawComponent1.OnSettingCutOff();
            //Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, new LogInfoEntity("命令:切断"));
            //Messenger.Instance.Send(MainEvent.OnAddLogDrawInfos, new LogInfoEntity("[请输入切断线离左端距离(mm)]:",true));
        }

        private void btnReplaceToPoint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReplaceToPoint frm = new FrmReplaceToPoint();
            frm.ShowDialog();
        }

        private void btnWeldCompensation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnMeasure_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.drawComponent1.OnSettingMeasure();
        }

        private void btnArray_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.drawComponent1.DataModel.DrawLayer.Objects.Count == 0)
                return;
            int selectCount = this.drawComponent1.DataModel.DrawLayer.Objects.Count(x => x.IsSelected);
            GlobalModel.Params.ArrayParam.TubeLength = this.drawComponent1.DataModel.TubeMode.TubeLength;
            GlobalModel.Params.ArrayParam.HasSelectedObject = selectCount > 0;
            FrmArray frm = new FrmArray(GlobalModel.Params.ArrayParam);
            DialogResult result = frm.ShowDialog(); 
            if(DialogResult.OK == result)
            {
                this.drawComponent1.DoArray();
            }
        }
        #endregion

        #region 参数设置
        private void btnLayer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmLayerConfig frm = new FrmLayerConfig();
            frm.ShowDialog();
        }

        private void ckLockBackground_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void ckLockLayer_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Tag != null)
            {
                int lockLayerId = Convert.ToInt32(e.Item.Tag);
            }
        }


        private void ckShowBackground_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void ckShowLayer_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Tag != null)
            {
                int showLayerId = Convert.ToInt32(e.Item.Tag);
            }
        }


        private void btnShowAllLayer_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnOnlyShowLayer_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item.Tag != null)
            {
                int onlyShowLayerId = Convert.ToInt32(e.Item.Tag);
            }
        }
        #endregion

        #region 数控
        private void btnPLCProcessItem_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnEdgeSeek_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnSeekCenter_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnGoOriginAllAxis_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnGoOriginX_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnGoOriginY_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnGoOriginW_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void ckGoOriginFollowerFirst_CheckedChanged(object sender, ItemClickEventArgs e)
        {

        }

        private void btnQCW_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnAutoFeed_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnDisableAutoFeed_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        #endregion

        private void MainView_SizeChanged(object sender, EventArgs e)
        {
            if(statusInfo != null)
            {
                this.statusInfo.Width = this.ribbonStatusBar1.Width;
                this.statusInfo.Location = new Point(this.ribbonStatusBar1.Width - this.statusInfo.Width - 10, 0);
            }
        }
    }
}
