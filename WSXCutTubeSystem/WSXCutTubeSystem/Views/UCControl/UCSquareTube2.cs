using System;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;
using WSX.Draw3D.Utils;

namespace WSXCutTubeSystem.Views.UCControl
{
    public partial class UCSquareTube2 : UserControl
    {
        private StandardTubeMode standardTubeMode;
        public UCSquareTube2(StandardTubeMode standardTubeMode)
        {
            InitializeComponent();
            this.standardTubeMode = standardTubeMode;
        }

        private void txtSquareTubeLength_NumberChanged(object arg1, EventArgs arg2)
        {
            float result = 0.01f;
            bool invalid = float.TryParse(this.txtSquareTubeLength.Text.Trim(), out result);
            if (invalid)
            {
                this.CalTotalLength();
            }
        }

        private void txtSquareLeftAngle_NumberChanged(object arg1, EventArgs arg2)
        {
            float result = 0.01f;
            bool invalid = float.TryParse(this.txtSquareLeftAngle.Text.Trim(), out result);
            if (invalid)
            {
                this.CalTotalLength();
                this.ucTubeTiltAngleShow1.LeftAngle = this.CalLimit(result);
                this.ucTubeTiltAngleShow1.Invalidate();
            }
        }

        private void txtSquareRightAngle_NumberChanged(object arg1, EventArgs arg2)
        {
            float result = 0.01f;
            bool invalid = float.TryParse(this.txtSquareRightAngle.Text.Trim(), out result);
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
            len = Convert.ToSingle(this.txtSquareTubeLength.Text.Trim());
            leftAngle = Convert.ToSingle(this.txtSquareLeftAngle.Text.Trim());
            rightAngle = Convert.ToSingle(this.txtSquareRightAngle.Text.Trim());
            this.txtSquareTubeTotalLen.Text = (len + Math.Tan(HitUtil.DegreesToRadians(Math.Abs(leftAngle))) * this.standardTubeMode.LongSideLength/2 +
                Math.Tan(HitUtil.DegreesToRadians(Math.Abs(rightAngle))) * this.standardTubeMode.ShortSideLength/2).ToString("#.##");
        }

        private void UCSquareTube2_VisibleChanged(object sender, EventArgs e)
        {
            this.CalTotalLength();
        }
        private float CalLimit(float a)
        {
            if (a > 60)
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
