using System;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;
using WSX.ControlLibrary.Common;
using WSX.Draw3D.Utils;

namespace WSXCutTubeSystem.Views.UCControl
{
    public partial class UCRectangleTube2 : UserControl
    {
        private StandardTubeMode standardTubeMode;
        public UCRectangleTube2(StandardTubeMode standardTubeMode)
        {
            InitializeComponent();
            this.standardTubeMode = standardTubeMode;
        }

        private void txtRectTubeLength_NumberChanged(object arg1, EventArgs arg2)
        {
            float result = 0.01f;
            bool invalid = float.TryParse(this.txtRectTubeLength.Text.Trim(), out result);
            if (invalid)
            {
                this.CalTotalLength();
            }
        }

        private void txtRectLeftAngle_NumberChanged(object arg1, EventArgs arg2)
        {
            float result = 0.01f;
            bool invalid = float.TryParse(this.txtRectLeftAngle.Text.Trim(), out result);
            if (invalid)
            {
                this.CalTotalLength();
                this.ucTubeTiltAngleShow1.LeftAngle = this.CalLimit(result);
                this.ucTubeTiltAngleShow1.Invalidate();
            }
        }

        private void txtRectRightAngle_NumberChanged(object arg1, EventArgs arg2)
        {
            float result = 0.01f;
            bool invalid = float.TryParse(this.txtRectRightAngle.Text.Trim(), out result);
            if (invalid)
            {
                this.CalTotalLength();
                this.ucTubeTiltAngleShow1.RightAngle = this.CalLimit(result);
                this.ucTubeTiltAngleShow1.Invalidate();
            }
        }

        private void CalTotalLength()
        {
            float len, leftAngle, rightAngle;
            len = Convert.ToSingle(this.txtRectTubeLength.Text.Trim());
            leftAngle = Convert.ToSingle(this.txtRectLeftAngle.Text.Trim());
            rightAngle = Convert.ToSingle(this.txtRectRightAngle.Text.Trim());
            this.txtRectTubeTotalLen.Text = (len + Math.Tan(HitUtil.DegreesToRadians(Math.Abs(leftAngle))) * this.standardTubeMode.ShortSideLength +
                Math.Tan(HitUtil.DegreesToRadians(Math.Abs(rightAngle))) * this.standardTubeMode.ShortSideLength).ToString("#.##");
        }

        private void UCRectangleTube2_VisibleChanged(object sender, EventArgs e)
        {
            this.CalTotalLength();
        }

        private float CalLimit(float a)
        {
            if ( a > 60)
            {
                a = 60;
            }
            if (a < -60)
            {
                a = -60;
            }
            return a;
        }
    }
}
