using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WSX.GlobalData;
using WSX.GlobalData.Messenger;

namespace WSX.ControlLibrary.Common
{
    public partial class UCNumberInputer : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void OnInputCompleteDelegate(double value);
        public OnInputCompleteDelegate OnInputComplete;
        /// <summary>
        /// 当前值
        /// </summary>
        [Browsable(true)]
        public override string Text
        {
            get
            {
                if (string.IsNullOrEmpty(this.PopupEdit.Text) || this.PopupEdit.Text.Trim().Equals(""))
                {
                    return "0";
                }
                else
                {
                    return this.PopupEdit.Text;
                }
            }
            set
            {
                this.PopupEdit.Text = value;
                if (double.TryParse(value, out double res))
                {
                    this.Number = res;
                }
            }
        }
        private NumberInputControl numberInputControl;
        private PopupContainerControl popupContainerControl;

        private float textSize = 9.0f;
        private double number = 0;
        private string suffix;

        public event Action<object, EventArgs> NumberChanged;

        public UCNumberInputer()
        {
            InitializeComponent();
            this.PopupEdit.TextChanged += (sender, e) =>
            {
                if (double.TryParse(this.PopupEdit.Text, out double num))
                {
                    if(this.Number != num)
                    {
                        this.Number = num;
                    }
                }                  
            };
        }

        public bool IsInterger { get; set; }

        public float TextSize
        {
            get
            {
                return this.textSize;
            }
            set
            {
                this.textSize = value;
                this.PopupEdit.Font = new Font("Tahoma", value);
            }
        }

        public bool ReadOnly
        {
            get => this.PopupEdit.ReadOnly;
            set => this.PopupEdit.ReadOnly = value;
        }
      
        public string Format { get; set; }
        

        [Bindable(true)]
        public double Number
        {
            get
            {
                return this.number;
            }
            set
            {
                this.number = value;
                this.UpdateText();
                this.NumberChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public string Suffix
        {
            get
            {
                return this.suffix;
            }
            set
            {
                this.suffix = value;
                this.UpdateText();
            }
        }

        public double Max { get; set; } = 100;
        public double Min { get; set; } = 0;

        private void UpdateText()
        {
            string numStr = string.IsNullOrEmpty(this.Format) ? this.Number.ToString() : this.Number.ToString(this.Format);
            this.PopupEdit.Text = numStr + this.Suffix;
        }
      
        private void UCNumberInputer_Load(object sender, EventArgs e)
        {
            //this.numberInputControl = new NumberInputControl();// { Width = 20};
            //this.popupContainerControl = new PopupContainerControl();
            //this.numberInputControl.Dock = DockStyle.Fill;
            //this.numberInputControl.OnClosed += NumberInputControl_OnClosed;
            //this.popupContainerControl.Controls.Add(this.numberInputControl);
            //this.PopupEdit.Properties.PopupControl = this.popupContainerControl;
        }

        private void NumberInputControl_OnClosed(object sender, CloseEventArgs e)
        {
            this.PopupEdit.ClosePopup();
            if (e.Result == DialogResult.Yes)
            {
                this.Number = this.numberInputControl.Number;
                //    if (this.Number < this.Min)
                //    {
                //        this.Number = this.Min;
                //    }
                //    if (this.Number > this.Max)
                //    {
                //        this.Number = this.Max;
                //    }

                //    if (this.IsInterger)
                //    {
                //        this.Number = Math.Round(this.Number);
                //    }
                //    this.OnInputComplete?.Invoke(this.Number);
            }
        }

        private void popupContainerEdit1_Popup(object sender, EventArgs e)
        {          
            this.numberInputControl.SetNumber(this.Number);
            this.numberInputControl.FirstAppend = true;
        }

        private void PopupEdit_Properties_QueryPopUp(object sender, CancelEventArgs e)
        {
            if (this.numberInputControl == null)
            {
                this.numberInputControl = new NumberInputControl();// { Width = 20};
                this.popupContainerControl = new PopupContainerControl();
                this.numberInputControl.Dock = DockStyle.Fill;
                this.numberInputControl.OnClosed += NumberInputControl_OnClosed;
                this.popupContainerControl.Controls.Add(this.numberInputControl);
                this.PopupEdit.Properties.PopupControl = this.popupContainerControl;
            }
        }

        private void PopupEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = (int)e.KeyChar;
            if (key==8||key == 43 || key == 45 || key == 46 || (key <= 57 && key >= 48))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }            
        }

        private void PopupEdit_Leave(object sender, EventArgs e)
        {
            Messenger.Instance.Send("OnTextInput", false);
            double input = 0;
            if (string.IsNullOrEmpty(this.Suffix) == false)
            {
                double.TryParse(this.PopupEdit.Text.Replace(this.Suffix, ""), out  input);
            }
            else
            {
                 double.TryParse(this.PopupEdit.Text, out  input);
            }
            this.Number = input;
            if (this.Number < this.Min)
            {
                this.Number = this.Min;
            }
            if (this.Number > this.Max)
            {
                this.Number = this.Max;
            }

            if (this.IsInterger)
            {
                this.Number = Math.Round(this.Number);
            }
          //  this.PopupEdit.Text = this.Number.ToString();
            this.OnInputComplete?.Invoke(this.Number);
        }

        private void PopupEdit_KeyDown(object sender, KeyEventArgs e)
        {
             Messenger.Instance.Send("OnTextInput", true);
        }
    }
}
