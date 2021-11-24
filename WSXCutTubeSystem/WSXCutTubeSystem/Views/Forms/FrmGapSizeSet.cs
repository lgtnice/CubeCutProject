using System;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;

namespace WSXCutTubeSystem.Views.Forms
{
    public partial class FrmGapSizeSet : Form
    {
        public GapModel Model { private set; get; }
        private FrmGapSizeSet()
        {
            InitializeComponent();
        }

        public FrmGapSizeSet(GapModel model) : this()
        {
            this.Model = model;
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model != null)
                {
                    this.mvvmContext1.SetViewModel(typeof(GapModel), this.Model);
                }
                else
                {
                    this.Model = this.mvvmContext1.GetViewModel<GapModel>();
                }
                InitialezeBindings();
            }
        }

        private void InitialezeBindings()
        {
            var fluent = mvvmContext1.OfType<GapModel>();

            fluent.SetBinding(this.ucNumberGapSize, e => e.Number, x => x.GapSize);

            Func<bool, int> func1 = p => { return p ? 0 : 1; };
            Func<int, bool> func2 = p => { return p == 0; };
            fluent.SetBinding(this.comboBoxEnableForSelected, e => e.SelectedIndex, x => x.IsEnableForSelected, func1, func2);
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
