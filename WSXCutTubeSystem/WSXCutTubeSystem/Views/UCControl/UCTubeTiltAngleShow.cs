using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSX.CommomModel.Utilities;
using WSX.Draw3D.Utils;

namespace WSXCutTubeSystem.Views.UCControl
{
    public partial class UCTubeTiltAngleShow : UserControl
    {
        private double leftAngle=0;
        private double rightAngle = 0;
        public double LeftAngle
        {
            set { this.leftAngle = value; }
            get { return this.leftAngle; }
        }

        public double RightAngle
        {
            set { this.rightAngle = value; }
            get { return this.rightAngle; }
        }

        public UCTubeTiltAngleShow()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawPanel();
            base.OnPaint(e);
        }

        private void DrawPanel()
        {
            int width = this.panel1.Width;
            int height = this.panel1.Height;
            Bitmap img = new Bitmap(width, height);
            Graphics gs = Graphics.FromImage(img);
            gs.Clear(Color.White);
            gs.DrawString(string.Format("LeftAngle={0}", this.leftAngle), new Font("微软雅黑", 8), new SolidBrush(Color.Black), 15, 45);
            gs.DrawString(string.Format("RightAngle={0}", this.rightAngle), new Font("微软雅黑", 8), new SolidBrush(Color.Black), 160, 120);
            gs.DrawLine(new Pen(Color.Red, 2f), new PointF(80, 65), new PointF(80, 115));
            gs.DrawLine(new Pen(Color.Red, 2f), new PointF(200, 65), new PointF(200, 115));
            double x1 = 80 - 25 * Math.Tan(HitUtil.DegreesToRadians(leftAngle));
            PointF p1 = new PointF((float)x1, 65);
            double x4 = 80 + 25 * Math.Tan(HitUtil.DegreesToRadians(leftAngle));
            PointF p4 = new PointF((float)x4, 115);
            double x2 = 200 - 25 * Math.Tan(HitUtil.DegreesToRadians(rightAngle));
            PointF p2 = new PointF((float)x2, 65);
            double x3 = 200 + 25 * Math.Tan(HitUtil.DegreesToRadians(rightAngle));
            PointF p3 = new PointF((float)x3, 115);
            gs.DrawLine(new Pen(Color.Blue, 2f),p1, p2);
            gs.DrawLine(new Pen(Color.Blue, 2f),p3, p4);
            if (leftAngle != 0)
            {
                gs.DrawLine(new Pen(Color.Blue, 2f), p1, p4);
                RectangleF rf2 = new RectangleF(new PointF(70, 80), new SizeF(20, 20));
                if (leftAngle > 0)
                {
                    gs.DrawArc(new Pen(Color.Red, 2f), rf2, (float)(-90 - leftAngle), (float)leftAngle);
                }
                else
                {
                    gs.DrawArc(new Pen(Color.Red, 2f), rf2, -90, (float)Math.Abs(rightAngle));
                }
            }
            if (rightAngle != 0)
            {
                gs.DrawLine(new Pen(Color.Blue, 2f), p2, p3);
                RectangleF rf1 = new RectangleF(new PointF(190, 80), new SizeF(20, 20));
                if (rightAngle > 0)
                {
                    gs.DrawArc(new Pen(Color.Red, 2f), rf1, (float)(90- rightAngle), (float)rightAngle);
                }
                else
                {
                    gs.DrawArc(new Pen(Color.Red, 2f), rf1, 90, (float)Math.Abs(rightAngle));
                }
            }
            using (Graphics tg = this.panel1.CreateGraphics())
            {
                tg.DrawImage(img, 0, 0);
            }
        }
    }
}
