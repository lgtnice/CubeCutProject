using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;

namespace WSXCutTubeSystem.Views.Forms
{
    public partial class FrmArray : Form
    {
        public ArrayModel Model { private set; get; }
        public FrmArray()
        {
            InitializeComponent();
        }

        public FrmArray(ArrayModel model):this()
        {
            if (!model.HasSelectedObject)
            {
                model.IsOnlyCopySelected = false;
            }

            this.Model = model;
            if (!this.mvvmContext1.IsDesignMode)
            {
                if(this.Model != null)
                {
                    this.mvvmContext1.SetViewModel(typeof(ArrayModel), this.Model);
                }else
                {
                    this.Model = this.mvvmContext1.GetViewModel<ArrayModel>();
                }
                InitialezeBindings();
            }
        }

        private void InitialezeBindings()
        {
            var fluent = mvvmContext1.OfType<ArrayModel>();
            fluent.SetBinding(this.ucNumberCount, e => e.Number, x => x.Count);
            fluent.SetBinding(this.ucNumberDistance, e => e.Number, x => x.Distance);
            fluent.SetBinding(this.radioButtonOffset, e => e.Checked, x => x.ArrayMode, m=> { return m == ArrayMode.Offset; }, r=> { return ArrayMode.Offset; });
            fluent.SetBinding(this.radioButtonGap, e => e.Checked, x => x.ArrayMode, m => { return m == ArrayMode.Gap; }, r => { return ArrayMode.Gap; });
            fluent.SetBinding(this.checkBoxOnlySelected, e => e.Enabled, x => x.HasSelectedObject);
            fluent.SetBinding(this.checkBoxOnlySelected, e => e.Checked, x => x.IsOnlyCopySelected);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panel2.CreateGraphics();

            Size size = new Size(50, 20);
            Pen pen = new Pen(Color.Black, 1);
            SolidBrush brush = new SolidBrush(Color.LightGray);
            Rectangle rectangleF = new Rectangle(15, panel2.Height / 2 - size.Height / 2, size.Width, size.Height);
            g.FillRectangle(brush, rectangleF);
            g.DrawRectangle(pen, rectangleF);

            Rectangle rectangleF2 = new Rectangle(rectangleF.Location.X + size.Width + 30, panel2.Height / 2 - size.Height / 2, size.Width, size.Height);
            g.FillRectangle(brush, rectangleF2);
            g.DrawRectangle(pen, rectangleF2);

            SolidBrush textBrush = new SolidBrush(Color.Red);
            g.DrawString(Model.TubeLength.ToString("F2"), new Font("宋体", 9), textBrush, 23, panel2.Height / 2 - size.Height / 2 + 4);

            PointF start = PointF.Empty;
            PointF end = PointF.Empty;

            if(this.Model.ArrayMode == ArrayMode.Gap)
            {
                //画间距
                pen = new Pen(Color.Blue, 1);
                start = new PointF(rectangleF.Right, rectangleF.Top);
                end = new PointF(rectangleF.Right, rectangleF.Top - 30);
                g.DrawLine(pen, start, end);

                start = new PointF(rectangleF2.Left, rectangleF2.Top);
                end = new PointF(rectangleF2.Left, rectangleF2.Top - 30);
                g.DrawLine(pen, start, end);

                pen.CustomEndCap = new AdjustableArrowCap(10, 10, false);
                start = new PointF(rectangleF.Right - 20, rectangleF2.Top - 15);
                end = new PointF(rectangleF.Right, rectangleF2.Top - 15);
                g.DrawLine(pen, start, end);

                start = new PointF(rectangleF2.Left + 20, rectangleF2.Top - 15);
                end = new PointF(rectangleF2.Left, rectangleF2.Top - 15);
                g.DrawLine(pen, start, end);
            }
            else
            {
                //画偏移
                pen = new Pen(Color.Orange, 1);
                start = new PointF(rectangleF.Left, rectangleF.Bottom);
                end = new PointF(rectangleF.Left, rectangleF.Bottom + 30);
                g.DrawLine(pen, start, end);

                start = new PointF(rectangleF2.Left, rectangleF.Bottom);
                end = new PointF(rectangleF2.Left, rectangleF.Bottom + 30);
                g.DrawLine(pen, start, end);

                pen.CustomEndCap = new AdjustableArrowCap(10, 10, false);
                start = new PointF(rectangleF.Left + rectangleF.Width / 2, rectangleF.Bottom + 15);
                end = new PointF(rectangleF.Left, rectangleF2.Bottom + 15);
                g.DrawLine(pen, start, end);

                start = new PointF(rectangleF2.Left - rectangleF.Width / 2, rectangleF.Bottom + 15);
                end = new PointF(rectangleF2.Left, rectangleF.Bottom + 15);
                g.DrawLine(pen, start, end);
            }
        }
        private void radioButtonOffset_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Invalidate();
        }

        private void radioButtonGap_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Invalidate();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.btnOk.Focus();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}