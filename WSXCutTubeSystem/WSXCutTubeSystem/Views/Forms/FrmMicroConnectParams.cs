using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;

namespace WSXCutTubeSystem.Views.Forms
{
    public partial class FrmMicroConnectParams : Form
    {
        public MicroConnectParam Model { private set; get; }
        private FrmMicroConnectParams()
        {
            InitializeComponent();
        }

        public FrmMicroConnectParams(MicroConnectParam model):this()
        {
            this.Model = model;

            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model != null)
                {
                    this.mvvmContext1.SetViewModel(typeof(MicroConnectParam), this.Model);
                }
                else
                {
                    this.Model = this.mvvmContext1.GetViewModel<MicroConnectParam>();
                }
                InitialezeBindings();
            }
        }

        private void InitialezeBindings()
        {
            var fluent = mvvmContext1.OfType<MicroConnectParam>();
            fluent.SetBinding(this.ucNumberMicroSize, e => e.Number, x => x.MicroSize);
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