using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using WSX.GlobalData.Model;

namespace WSXCutTubeSystem.Views.Operation
{
    public partial class UCLayerParaBar : UserControl
    {
        private bool enabled = true;

        public event EventHandler OnLayerIdChangedEvent;
        public event EventHandler OnLayerWindowShow;
        public event Action<object, MachineEnabledEventArgs> OnMachineEnabledChanged;
     
        public UCLayerParaBar()
        {
            InitializeComponent();
            this.btnPara.ShowToolTips = true;
        }

        private void UCLayerParaBar_Load(object sender, EventArgs e)
        {
            //OperationEngine.Instance.OnStatusChanged += x =>
            //{
            //    this.enabled = x == EngineStatus.Idle;
            //};
        }

        private void btnLayer_Click(object sender, EventArgs e)
        {
            if (this.enabled)
            {
                GlobalModel.CurrentLayerId = (LayerId)Convert.ToInt32((sender as SimpleButton).Tag);
                this.OnLayerIdChangedEvent?.Invoke(sender, e);
            }
        }

        private void btnPara_Click(object sender, EventArgs e)
        {
            if (this.enabled)
            {
                this.OnLayerWindowShow?.Invoke(sender, e);
            }
        }
    }

    public class MachineEnabledEventArgs : EventArgs
    {
        public bool IsMachineEnabled { get; set; }
    }
}
