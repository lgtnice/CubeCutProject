using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;

namespace WSXCutTubeSystem.Views.UCControl
{
    public partial class UCCircleTube : UserControl
    {
        private StandardTubeMode standardTubeMode;
        public UCCircleTube(StandardTubeMode standardTubeMode)
        {
            InitializeComponent();
            this.standardTubeMode = standardTubeMode;
        }
    }
}
