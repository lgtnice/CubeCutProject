using System;
using System.Drawing;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;
using WSXCutTubeSystem.Views.UCControl;

namespace WSXCutTubeSystem.Views
{
    public partial class FrmStandardTube : Form
    {
        private int currentPage = 0;
        public StandardTubeMode standardTubeMode;
        private UserControl currentUserControl;
        private UCCircleTube uCCircleTube;
        private UCRectangleTube uCRectangleTube;
        private UCSquareTube uCSquareTube;
        private UCSportTube uCSportTube;
        private UCCircleTube2 uCCircleTube2;
        private UCRectangleTube2 uCRectangleTube2;
        private UCSquareTube2 uCSquareTube2;
        private UCSportTube2 uCSportTube2;
        public FrmStandardTube()
        {
            InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            this.standardTubeMode = new StandardTubeMode();
            this.uCCircleTube = new UCCircleTube(this.standardTubeMode);
            this.panelContext.Controls.Add(this.uCCircleTube);
            this.uCCircleTube.Visible = false;

            this.uCCircleTube2 = new UCCircleTube2(this.standardTubeMode);
            this.panelContext.Controls.Add(this.uCCircleTube2);
            this.uCCircleTube2.Visible = false;

            this.uCSquareTube = new UCSquareTube(this.standardTubeMode);
            this.panelContext.Controls.Add(this.uCSquareTube);
            this.uCSquareTube.Visible = false;

            this.uCSquareTube2 = new UCSquareTube2(this.standardTubeMode);
            this.panelContext.Controls.Add(this.uCSquareTube2);
            this.uCSquareTube2.Visible = false;

            this.uCSportTube = new UCSportTube(this.standardTubeMode);
            this.panelContext.Controls.Add(this.uCSportTube);
            this.uCSportTube.Visible = false;

            this.uCSportTube2 = new UCSportTube2(this.standardTubeMode);
            this.panelContext.Controls.Add(this.uCSportTube2);
            this.uCSportTube2.Visible = false;

            this.uCRectangleTube = new UCRectangleTube(this.standardTubeMode);
            this.panelContext.Controls.Add(this.uCRectangleTube);
            this.uCRectangleTube.Visible = false;

            this.uCRectangleTube2 = new UCRectangleTube2(this.standardTubeMode);
            this.panelContext.Controls.Add(this.uCRectangleTube2);
            this.uCRectangleTube2.Visible = false;
        }

        private void btnSquare_Click(object sender, EventArgs e)
        {
            this.ToggleButtonVisible(false, 1);
            this.uCSquareTube.Visible = true;
            this.currentUserControl = this.uCSquareTube;
            this.standardTubeMode.TubeTypes = StandardTubeMode.TubeType.Square;
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            this.ToggleButtonVisible(false, 1);
            this.uCRectangleTube.Visible = true;
            this.currentUserControl = this.uCRectangleTube;
            this.standardTubeMode.TubeTypes = StandardTubeMode.TubeType.Rectangle;
        }

        private void btnSport_Click(object sender, EventArgs e)
        {
            this.ToggleButtonVisible(false, 1);
            this.uCSportTube.Visible = true;
            this.currentUserControl = this.uCSportTube;
            this.standardTubeMode.TubeTypes = StandardTubeMode.TubeType.Sport;
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            this.ToggleButtonVisible(false, 1);
            this.uCCircleTube.Visible = true;
            this.currentUserControl = this.uCCircleTube;
            this.standardTubeMode.TubeTypes = StandardTubeMode.TubeType.Circle;
        }

        private void ToggleButtonVisible(bool visible, int page)
        {
            this.btnSquare.Visible = visible;
            this.btnRectangle.Visible = visible;
            this.btnSport.Visible = visible;
            this.btnCircle.Visible = visible;
            this.panelHeader.Visible = visible;
            this.btnReturn.Visible = !visible;
            this.btnNext.Visible = !visible;
            this.currentPage = page;
        }


        #region 鼠标效果
        private void btnSquare_MouseHover(object sender, EventArgs e)
        {
            (sender as Button).BackgroundImage = Properties.Resources.squareSelected;
        }

        private void btnSquare_MouseLeave(object sender, EventArgs e)
        {
            (sender as Button).BackgroundImage = Properties.Resources.square;
        }

        private void btnRectangle_MouseHover(object sender, EventArgs e)
        {
            (sender as Button).BackgroundImage = Properties.Resources.rectSelected;
        }

        private void btnRectangle_MouseLeave(object sender, EventArgs e)
        {
            (sender as Button).BackgroundImage = Properties.Resources.rectangle;
        }
        private void btnSport_MouseHover(object sender, EventArgs e)
        {
            (sender as Button).BackgroundImage = Properties.Resources.sportSelected;
        }
        private void btnSport_MouseLeave(object sender, EventArgs e)
        {
            (sender as Button).BackgroundImage = Properties.Resources.sport;
        }

        private void btnCircle_MouseHover(object sender, EventArgs e)
        {
            (sender as Button).BackgroundImage = Properties.Resources.circleSelected;
        }

        private void btnCircle_MouseLeave(object sender, EventArgs e)
        {
            (sender as Button).BackgroundImage = Properties.Resources.circle;
        }
        #endregion

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (this.currentPage == 1)
            {
                this.ToggleButtonVisible(true, 0);
                switch (this.currentUserControl.Name)
                {
                    case "UCRectangleTube":
                        this.uCRectangleTube.Visible = false;
                        break;
                    case "UCSportTube":
                        this.uCSportTube.Visible = false;
                        break;
                    case "UCSquareTube":
                        this.uCSquareTube.Visible = false;
                        break;
                    case "UCCircleTube":
                        this.uCCircleTube.Visible = false;
                        break;
                }
            }
            else
            {
                this.currentPage = 1;
                this.btnOk.Visible = false;
                this.btnNext.Visible = true;
                switch (this.currentUserControl.Name)
                {
                    case "UCRectangleTube2":
                        this.currentUserControl = this.uCRectangleTube;
                        this.uCRectangleTube.Visible = true;
                        this.uCRectangleTube2.Visible = false;
                        break;
                    case "UCSportTube2":
                        this.currentUserControl = this.uCSportTube;
                        this.uCSportTube.Visible = true;
                        this.uCSportTube2.Visible = false;
                        break;
                    case "UCSquareTube2":
                        this.currentUserControl = this.uCSquareTube;
                        this.uCSquareTube.Visible = true;
                        this.uCSquareTube2.Visible = false;
                        break;
                    case "UCCircleTube2":
                        this.currentUserControl = this.uCCircleTube;
                        this.uCCircleTube.Visible = true;
                        this.uCCircleTube2.Visible = false;
                        break;
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bool flag = true;
            switch (this.currentUserControl.Name)
            {
                case "UCRectangleTube":
                    this.standardTubeMode.LongSideLength = Convert.ToSingle(this.uCRectangleTube.txtLongSideLen.Text.Trim());
                    this.standardTubeMode.ShortSideLength = Convert.ToSingle(this.uCRectangleTube.txtShortSideLen.Text.Trim());
                    this.standardTubeMode.CircleRadius = Convert.ToSingle(this.uCRectangleTube.txtRoundRadius.Text.Trim());
                    if (this.standardTubeMode.ShortSideLength <= this.standardTubeMode.CircleRadius * 2 || this.standardTubeMode.LongSideLength <= this.standardTubeMode.CircleRadius * 2)
                    {
                        flag = false;
                        break;
                    }
                    this.currentUserControl = this.uCRectangleTube2;
                    this.uCRectangleTube2.Visible = true;
                    this.uCRectangleTube.Visible = false;
                    break;
                case "UCSportTube":
                    this.standardTubeMode.LongSideLength = Convert.ToSingle(this.uCSportTube.txtSportWidth.Text.Trim());
                    this.standardTubeMode.ShortSideLength = 0;
                    this.standardTubeMode.CircleRadius = Convert.ToSingle(this.uCSportTube.txtSportRadius.Text.Trim());
                    if(this.standardTubeMode.LongSideLength<=this.standardTubeMode.CircleRadius*2)
                    {
                        flag = false;
                        break;
                    }
                    this.currentUserControl = this.uCSportTube2;
                    this.uCSportTube2.Visible = true;
                    this.uCSportTube.Visible = false;
                    break;
                case "UCSquareTube":
                    this.standardTubeMode.LongSideLength = Convert.ToSingle(this.uCSquareTube.txtSideLen.Text.Trim());
                    this.standardTubeMode.ShortSideLength = this.standardTubeMode.LongSideLength;
                    this.standardTubeMode.CircleRadius = Convert.ToSingle(this.uCSquareTube.txtRoundRadius.Text.Trim());
                    if(this.standardTubeMode.LongSideLength<=this.standardTubeMode.CircleRadius*2)
                    {
                        flag = false;
                        break;
                    }
                    this.currentUserControl = this.uCSquareTube2;
                    this.uCSquareTube2.Visible = true;
                    this.uCSquareTube.Visible = false;
                    break;
                case "UCCircleTube":
                    this.currentUserControl = this.uCCircleTube2;
                    this.uCCircleTube2.Visible = true;
                    this.uCCircleTube.Visible = false;
                    this.standardTubeMode.LongSideLength = 0;
                    this.standardTubeMode.ShortSideLength = 0;
                    this.standardTubeMode.CircleRadius = Convert.ToSingle(this.uCCircleTube.txtCircleRadius.Text.Trim());
                    break;
            }
            if (flag)
            {
                this.currentPage = 2;
                this.btnOk.Visible = true;
                this.btnNext.Visible = false;
            }
            else
            {
                MessageBox.Show("参数错误,请重新设置参数", "请确认", MessageBoxButtons.OK);
            }
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            switch (this.currentUserControl.Name)
            {
                case "UCRectangleTube2":
                    this.standardTubeMode.TubeLength = Convert.ToSingle(this.uCRectangleTube2.txtRectTubeLength.Text.Trim());
                    this.standardTubeMode.LeftAngle = Convert.ToSingle(this.uCRectangleTube2.txtRectLeftAngle.Text.Trim());
                    this.standardTubeMode.RightAngle = Convert.ToSingle(this.uCRectangleTube2.txtRectRightAngle.Text.Trim());
                    this.standardTubeMode.TubeTotalLength = Convert.ToSingle(this.uCRectangleTube2.txtRectTubeTotalLen.Text.Trim());
                    break;
                case "UCSportTube2":
                    this.standardTubeMode.TubeLength = Convert.ToSingle(this.uCSportTube2.txtSportTubeLength.Text.Trim());
                    this.standardTubeMode.LeftAngle = Convert.ToSingle(this.uCSportTube2.txtSportLeftAngle.Text.Trim());
                    this.standardTubeMode.RightAngle = Convert.ToSingle(this.uCSportTube2.txtSportRightAngle.Text.Trim());
                    this.standardTubeMode.TubeTotalLength = Convert.ToSingle(this.uCSportTube2.txtSportTubeTotalLen.Text.Trim());
                    break;
                case "UCSquareTube2":
                    this.standardTubeMode.TubeLength = Convert.ToSingle(this.uCSquareTube2.txtSquareTubeLength.Text.Trim());
                    this.standardTubeMode.LeftAngle = Convert.ToSingle(this.uCSquareTube2.txtSquareLeftAngle.Text.Trim());
                    this.standardTubeMode.RightAngle = Convert.ToSingle(this.uCSquareTube2.txtSquareRightAngle.Text.Trim());
                    this.standardTubeMode.TubeTotalLength = Convert.ToSingle(this.uCSquareTube2.txtSquareTubeTotalLen.Text.Trim());
                    break;
                case "UCCircleTube2":
                    this.standardTubeMode.TubeLength = Convert.ToSingle(this.uCCircleTube2.txtCircleTubeLength.Text.Trim());
                    this.standardTubeMode.LeftAngle = Convert.ToSingle(this.uCCircleTube2.txtCircleLeftAngle.Text.Trim());
                    this.standardTubeMode.RightAngle = Convert.ToSingle(this.uCCircleTube2.txtCircleRightAngle.Text.Trim());
                    this.standardTubeMode.TubeTotalLength = Convert.ToSingle(this.uCCircleTube2.txtCircleTubeTotalLen.Text.Trim());
                    break;
            }
            //this.btnReturn_Click(null, null);
            //this.btnReturn_Click(null, null);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
