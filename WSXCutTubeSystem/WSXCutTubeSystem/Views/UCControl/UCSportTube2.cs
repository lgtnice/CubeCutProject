using System;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;
using WSX.Draw3D.Utils;

namespace WSXCutTubeSystem.Views.UCControl
{
    public partial class UCSportTube2 : UserControl
    {
        private StandardTubeMode standardTubeMode;
        public UCSportTube2(StandardTubeMode standardTubeMode)
        {
            InitializeComponent();
            this.standardTubeMode = standardTubeMode;
        }

        private void txtSportTubeLength_NumberChanged(object arg1, EventArgs arg2)
        {
            float result = 0.01f;
            bool invalid = float.TryParse(this.txtSportTubeLength.Text.Trim(), out result);
            if (invalid)
            {
                this.CalTotalLength();
            }
        }

        private void txtSportLeftAngle_NumberChanged(object arg1, EventArgs arg2)
        {
            float result = 0.01f;
            bool invalid = float.TryParse(this.txtSportLeftAngle.Text.Trim(), out result);
            if (invalid)
            {
                this.CalTotalLength();
                this.ucTubeTiltAngleShow1.LeftAngle = this.CalLimit(result);
                this.ucTubeTiltAngleShow1.Invalidate();
            }
        }

        private void txtSportRightAngle_NumberChanged(object arg1, EventArgs arg2)
        {
            float result = 0.01f;
            bool invalid = float.TryParse(this.txtSportRightAngle.Text.Trim(), out result);
            if (invalid)
            {
                this.CalTotalLength();
                this.ucTubeTiltAngleShow1.RightAngle = this.CalLimit(result);
                this.ucTubeTiltAngleShow1.Invalidate();
            }
        }

        private void CalTotalLength()//计算与bc不一致,最小值没有？此处是 根据短边计算的最小长度,与设置的管长叠加！
        {
            float len, leftAngle, rightAngle;
            len = Convert.ToSingle(this.txtSportTubeLength.Text.Trim());
            leftAngle = Convert.ToSingle(this.txtSportLeftAngle.Text.Trim());
            rightAngle = Convert.ToSingle(this.txtSportRightAngle.Text.Trim());
            this.txtSportTubeTotalLen.Text = (len + Math.Tan(HitUtil.DegreesToRadians(Math.Abs(leftAngle))) * this.standardTubeMode.CircleRadius +
                Math.Tan(HitUtil.DegreesToRadians(Math.Abs(rightAngle))) * this.standardTubeMode.CircleRadius).ToString("#.##");
        }

        private void UCSportTube2_VisibleChanged(object sender, EventArgs e)
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
