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
using System.Drawing.Drawing2D;

namespace WSXCutTubeSystem.Views.UCControl
{
    public partial class UCSportTube : UserControl
    {
        public UCSportTube(StandardTubeMode standardTubeMode)
        {
            InitializeComponent();
        }

        private void txtSportHeight_NumberChanged(object arg1, EventArgs arg2)
        {
            float result = 0.01f;
            bool invalid = float.TryParse(this.txtSportHeight.Text.Trim(), out result);
            if (invalid)
            {
                this.txtSportRadius.Text = (result / 2).ToString("#.##");
            }
            this.OnPaint(null);
        }

        private void txtSportWidth_NumberChanged(Object arg1, EventArgs arg2)
        {
            this.OnPaint(null);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.DrawPanel();
            base.OnPaint(e);
        }

        private void DrawPanel()
        {
            int width = this.panel1.Width;
            int height = this.panel1.Height;
            Bitmap img = new Bitmap(width, height);
            Graphics gs = Graphics.FromImage(img);
            gs.Clear(Color.White);
            Pen p = new Pen(Color.Blue, 3f);
            gs.DrawLine(p, new PointF(66, 36), new PointF(202, 36));
            gs.DrawLine(p, new PointF(66, 96), new PointF(202, 96));
            RectangleF rf1 = new RectangleF(172, 36, 60, 60);
            gs.DrawArc(p, rf1, -90, 180);
            RectangleF rf2 = new RectangleF(36, 36, 60, 60);
            gs.DrawArc(p, rf2, 90, 180);
            Pen p1 = new Pen(Color.Black, 1);
            p1.CustomEndCap = new AdjustableArrowCap(3, 3);
            p1.CustomStartCap = new AdjustableArrowCap(3, 3);
            gs.DrawLine(Pens.Black, new PointF(34, 66), new PointF(34, 20));
            gs.DrawLine(Pens.Black, new PointF(234, 66), new PointF(234, 20));
            gs.DrawLine(p1, new PointF(36, 30), new PointF(232, 30));
            gs.DrawString(string.Format("宽度:={0}", this.txtSportWidth.Text), new Font("微软雅黑", 8), new SolidBrush(Color.Black), 110, 13);
            gs.DrawLine(p1, new PointF(66, 36), new PointF(66, 96));
            gs.DrawString(string.Format("高度:={0}", this.txtSportHeight.Text), new Font("微软雅黑", 8), new SolidBrush(Color.Black), 80, 60);
            using (Graphics tg = this.panel1.CreateGraphics())
            {
                tg.DrawImage(img, 0, 0);
            }
        }
    }
}
