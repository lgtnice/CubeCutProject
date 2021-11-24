using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel.Compensation;

namespace WSXCutTubeSystem.Views.Forms
{
    public partial class FrmGapCompensation : Form
    {
        public CompensationModel Model { private set; get; }

        private FrmGapCompensation()
        {
            InitializeComponent();
        }

        public FrmGapCompensation(CompensationModel model):this()
        {
            this.Model = model;
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model != null)
                {
                    this.mvvmContext1.SetViewModel(typeof(CompensationModel), this.Model);
                }
                else
                {
                    this.Model = this.mvvmContext1.GetViewModel<CompensationModel>();
                }
                InitialezeBindings();
            }
        }

        private void InitialezeBindings()
        {
            var fluent = mvvmContext1.OfType<CompensationModel>();

            fluent.SetBinding(this.ucNumberSize, e => e.Number, x => x.Size);

            Func<bool, int> func1 = p => { return p ? 0 : 1; };
            Func<int, bool> func2 = p => { return p == 0; };
            fluent.SetBinding(this.comboIsEnableForSelected, e => e.SelectedIndex, x => x.IsEnableForSelected, func1, func2);

            fluent.SetBinding(this.radioCancel, e => e.Checked, x => x.Style, m => { return m == CompensationType.Cancel; }, r => { return CompensationType.Cancel; });
            fluent.SetBinding(this.radioAuto, e => e.Checked, x => x.Style, m => { return m == CompensationType.Auto; }, r => { return CompensationType.Auto; });
            fluent.SetBinding(this.radioAllInner, e => e.Checked, x => x.Style, m => { return m == CompensationType.AllInner; }, r => { return CompensationType.AllInner; });
            fluent.SetBinding(this.radioAllOuter, e => e.Checked, x => x.Style, m => { return m == CompensationType.AllOuter; }, r => { return CompensationType.AllOuter; });
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.btnOk.Focus();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
