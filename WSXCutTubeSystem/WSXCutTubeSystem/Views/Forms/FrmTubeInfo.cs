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
    public partial class FrmTubeInfo : Form
    {
        private StandardTubeMode stm;
        private float shortSideLength;
        private float shortSideLengthCal;
        private float longSideLength;
        private float longSideLengthCal;
        private float circleRadius;
        private float circleRadiusCal;
        public FrmTubeInfo(StandardTubeMode stm)
        {
            this.stm = stm;
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            if (this.stm == null) return;
            this.ucNumInput11.Visible = true;
            this.ucNumInput21.Visible = true;
            this.ucNumInput31.Visible = true;
            shortSideLengthCal = stm.ShortSideLength;
            longSideLengthCal = stm.LongSideLength;
            circleRadiusCal = stm.CircleRadius;
            switch (stm.TubeTypes)
            {
                case StandardTubeMode.TubeType.Circle:
                    this.lbl10.Text = "半径";
                    this.ucNumInput11.Text= circleRadiusCal.ToString();
                    this.tableLayoutPanel1.Controls.Remove(this.lbl20);
                    this.tableLayoutPanel1.Controls.Remove(this.lbl30);
                    this.tableLayoutPanel1.Controls.Remove(this.ucNumInput21);
                    this.tableLayoutPanel1.Controls.Remove(this.ucNumInput31);
                    break;
                case StandardTubeMode.TubeType.Square:
                    this.lbl10.Text = "边长";
                    this.lbl20.Text = "圆角";
                    this.ucNumInput21.ReadOnly = false;
                    this.ucNumInput11.Text = longSideLengthCal.ToString();
                    this.ucNumInput21.Text = circleRadiusCal.ToString();
                    //添加事件捕捉圆角改变
                    //this.ucNumInput21.OnInputComplete += (a) => { a = CalRoundRadius(a); };
                    this.tableLayoutPanel1.Controls.Remove(this.lbl30);
                    this.tableLayoutPanel1.Controls.Remove(this.ucNumInput31);
                    break;
                case StandardTubeMode.TubeType.Rectangle:
                    this.lbl10.Text = "宽";
                    this.lbl20.Text = "高";
                    this.lbl30.Text = "圆角";
                    //添加事件捕捉圆角改变
                    this.ucNumInput11.Text = longSideLengthCal.ToString();
                    this.ucNumInput21.Text = shortSideLengthCal.ToString();
                    this.ucNumInput31.Text = circleRadiusCal.ToString();
                    //this.ucNumInput31.OnInputComplete += (b) => { b = CalRoundRadius(b); };
                    break;
                case StandardTubeMode.TubeType.Sport:
                    this.lbl10.Text = "宽";
                    this.lbl20.Text = "半径";
                    this.ucNumInput11.Text = longSideLengthCal.ToString();
                    this.ucNumInput21.Text = circleRadiusCal.ToString();
                    this.tableLayoutPanel1.Controls.Remove(this.lbl30);
                    this.tableLayoutPanel1.Controls.Remove(this.ucNumInput31);
                    break;
                default:
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.DrawPanel();
            base.OnPaint(e);
        }

        private void DrawPanel()
        {
            if (this.stm == null) return;
            int width = this.panel1.Width;
            int height = this.panel1.Height;
            Bitmap img = new Bitmap(width, height);
            Graphics gs = Graphics.FromImage(img);
            gs.Clear(Color.Black);
            gs.DrawString("钢管截面", new Font("宋体", 15), new SolidBrush(Color.Yellow), 5, 5);
            shortSideLength = shortSideLengthCal;
            longSideLength = longSideLengthCal;
            circleRadius = circleRadiusCal;
            switch (stm.TubeTypes)//实现倒角半径可变！！！！
            {
                case StandardTubeMode.TubeType.Circle:
                    gs.DrawEllipse(Pens.LimeGreen, 35, 35, 280, 280);//圆的所有演示图形都是一样的,统一使用一个估算的图形进行绘制
                    break;
                case StandardTubeMode.TubeType.Rectangle:
                case StandardTubeMode.TubeType.Square://panel大小为366*360,使用宽边为280作为基数,避免出现当图形太小或者图形太大时绘制显示不正常
                    {
                        if (longSideLength >= shortSideLength)//以(长边/短边中)长的那条边为主,方形管和矩形逻辑一样
                        {
                            if (longSideLength < 280)
                            {
                                circleRadius *= (280 / longSideLength);
                                shortSideLength *= (280 / longSideLength);
                            }
                            if (longSideLength > 280)
                            {
                                circleRadius /= (longSideLength / 280);
                                shortSideLength /= (longSideLength / 280);
                            }
                            longSideLength = 280;
                        }
                        else
                        {
                            if (shortSideLength < 280)
                            {
                                circleRadius *= (280 / shortSideLength);
                                longSideLength *= (280 / shortSideLength);
                            }
                            if (shortSideLength > 280)
                            {
                                circleRadius /= (shortSideLength / 280);
                                longSideLength /= (shortSideLength / 280);
                            }
                            shortSideLength = 280;
                        }
                        float x1 = (width + 2 * circleRadius - longSideLength) / 2;
                        float y1 = (height - shortSideLength) / 2;
                        float x2 = (width - 2 * circleRadius + longSideLength) / 2;
                        float y2 = y1;
                        float x3 = (width + longSideLength) / 2;
                        float y3 = (height + 2 * circleRadius - shortSideLength) / 2;
                        float x4 = x3;
                        float y4 = (height + shortSideLength - 2 * circleRadius) / 2;
                        float x5 = x2;
                        float y5 = (height + shortSideLength) / 2;
                        float x6 = x1;
                        float y6 = y5;
                        float x7 = (width - longSideLength) / 2;
                        float y7 = y4;
                        float x8 = x7;
                        float y8 = y3;
                        gs.DrawLine(Pens.LimeGreen, new PointF(x1, y1), new PointF(x2, y2));
                        RectangleF rf = new RectangleF(new PointF(x2 - circleRadius, y2), new SizeF(2 * circleRadius, 2 * circleRadius));
                        gs.DrawArc(Pens.LimeGreen, rf, -90, 90);
                        gs.DrawLine(Pens.LimeGreen, new PointF(x3, y3), new PointF(x4, y4));
                        RectangleF rf1 = new RectangleF(new PointF(x5 - circleRadius, y4 - circleRadius), new SizeF(2 * circleRadius, 2 * circleRadius));
                        gs.DrawArc(Pens.LimeGreen, rf1, 0, 90);
                        gs.DrawLine(Pens.LimeGreen, new PointF(x5, y5), new PointF(x6, y6));
                        RectangleF rf2 = new RectangleF(new PointF(x7, y7 - circleRadius), new SizeF(2 * circleRadius, 2 * circleRadius));
                        gs.DrawArc(Pens.LimeGreen, rf2, 90, 90);
                        gs.DrawLine(Pens.LimeGreen, new PointF(x7, y7), new PointF(x8, y8));
                        RectangleF rf3 = new RectangleF(new PointF(x8, y1), new SizeF(2 * circleRadius, 2 * circleRadius));
                        gs.DrawArc(Pens.LimeGreen, rf3, 180, 90);
                        break;
                    }
                case StandardTubeMode.TubeType.Sport:
                    {
                        if (longSideLength < 280)
                            circleRadius *= (280 / longSideLength);
                        if (longSideLength > 280)
                            circleRadius /= (longSideLength / 280);
                        longSideLength = 280;
                        float x1 = (width + 2 * circleRadius - longSideLength) / 2;
                        float y1 = (height - 2 * circleRadius) / 2;
                        float x2 = (width - 2 * circleRadius + longSideLength) / 2;
                        float y2 = y1;
                        float x3 = x1;
                        float y3 = (height + 2 * circleRadius) / 2;
                        float x4 = x2;
                        float y4 = y3;
                        gs.DrawLine(Pens.LimeGreen, new PointF(x1, y1), new PointF(x2, y2));
                        RectangleF rf = new RectangleF(new PointF(x2 - circleRadius, y2), new SizeF(2 * circleRadius, 2 * circleRadius));
                        gs.DrawArc(Pens.LimeGreen, rf, -90, 180);
                        gs.DrawLine(Pens.LimeGreen, new PointF(x3, y3), new PointF(x4, y4));
                        RectangleF rf1 = new RectangleF(new PointF(x1 - circleRadius, y1), new SizeF(2 * circleRadius, 2 * circleRadius));
                        gs.DrawArc(Pens.LimeGreen, rf1, 90, 180);
                        break;
                    }
                default:
                    break;
            }
            using (Graphics tg = this.panel1.CreateGraphics())
            {
                tg.DrawImage(img, 0, 0);
            }
        }

        private float CalRoundRadius(double radius)
        {
            if (radius >= shortSideLengthCal / 2)
                radius = shortSideLengthCal / 2;
            if (radius == 0)
                radius = 0.01;
            circleRadiusCal = (float)radius;
            DrawPanel();
            return (float)radius;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if (this.stm != null)
                this.stm.CircleRadius = circleRadiusCal;
            this.Close();
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FrmTubeInfo_MouseClick(object sender, MouseEventArgs e)
        {
            this.lbl00.Focus();//点击空白处让UCNumberInputer失去焦点
        }
    }
}
