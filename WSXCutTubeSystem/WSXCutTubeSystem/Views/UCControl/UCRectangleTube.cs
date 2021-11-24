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
    public partial class UCRectangleTube : UserControl
    {
        public UCRectangleTube(StandardTubeMode standardTubeMode)
        {
            InitializeComponent();
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
            gs.DrawRectangle(new Pen(Color.Blue, 3f), 66, 36, 136, 60);
            gs.DrawLine(Pens.Black, new PointF(66, 36), new PointF(66, 25));
            gs.DrawLine(Pens.Black, new PointF(202, 36), new PointF(202, 25));
            Pen p = new Pen(Color.Black, 1);
            p.CustomEndCap = new AdjustableArrowCap(3, 3);
            p.CustomStartCap = new AdjustableArrowCap(3, 3);
            gs.DrawLine(p, new PointF(66, 30), new PointF(202, 30));
            gs.DrawString(string.Format("长边长={0}", this.txtLongSideLen.Text), new Font("微软雅黑", 8), new SolidBrush(Color.Black), 106, 13);
            gs.DrawLine(p, new PointF(96, 36), new PointF(96, 96));
            gs.DrawString(string.Format("短边长={0}", this.txtShortSideLen.Text), new Font("微软雅黑", 8), new SolidBrush(Color.Black), 100, 60);
            using (Graphics tg = this.panel1.CreateGraphics())
            {
                tg.DrawImage(img, 0, 0);
            }
        }

        private void txtLongSideLen_NumberChanged(object arg1, EventArgs arg2)
        {
            this.OnPaint(null);
        }

        private void txtShortSideLen_NumberChanged(object arg1, EventArgs arg2)
        {
            this.OnPaint(null);
        }
    }
}
