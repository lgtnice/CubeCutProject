using System;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;
using WSX.Draw3D.Utils;

namespace WSXCutTubeSystem.Views.UCControl
{
    public partial class UCCircleTube2 : UserControl
    {
        private StandardTubeMode standardTubeMode;
        public UCCircleTube2(StandardTubeMode standardTubeMode)
        {
            InitializeComponent();
            this.standardTubeMode = standardTubeMode;
        }

        private void txtCircleTubeLength_NumberChanged(object arg1, EventArgs arg2)
        {
            float result = 0.01f;
            bool invalid = float.TryParse(this.txtCircleTubeLength.Text.Trim(), out result);
            if (invalid)
            {
                this.CalTotalLength();
            }
        }
        private void txtCircleTubeLength_Leave(object sender, System.EventArgs e)
        {

        }

        private void txtCircleLeftAngle_NumberChanged(object arg1, EventArgs arg2)
        {
            float result = 0.01f;
            bool invalid = float.TryParse(this.txtCircleLeftAngle.Text.Trim(), out result);
            if (invalid)
            {
                this.CalTotalLength();
                this.ucTubeTiltAngleShow1.LeftAngle = this.CalLimit(result);
                this.ucTubeTiltAngleShow1.Invalidate();
            }
        }
       
        private void txtCircleRightAngle_NumberChanged(object arg1, EventArgs arg2)
        {
            float result = 0.01f;
            bool invalid = float.TryParse(this.txtCircleRightAngle.Text.Trim(), out result);
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
            len = Convert.ToSingle(this.txtCircleTubeLength.Text.Trim());
            leftAngle = Convert.ToSingle(this.txtCircleLeftAngle.Text.Trim());
            rightAngle = Convert.ToSingle(this.txtCircleRightAngle.Text.Trim());
            this.txtCircleTubeTotalLen.Text = (len + Math.Abs(Math.Tan(HitUtil.DegreesToRadians(leftAngle))) * this.standardTubeMode.CircleRadius +
                Math.Abs(Math.Tan(HitUtil.DegreesToRadians(rightAngle))) * this.standardTubeMode.CircleRadius).ToString("#.##");
        }

        private void UCCircleTube2_VisibleChanged(object sender, EventArgs e)
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
