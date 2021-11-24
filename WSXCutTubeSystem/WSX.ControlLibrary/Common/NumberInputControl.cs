using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace WSX.ControlLibrary.Common
{

    public partial class NumberInputControl : UserControl
    {
        public double Number { get; private set; }
        public event Action<object, CloseEventArgs> OnClosed;
        public bool FirstAppend { get; set; } = false;
        /// <summary>
        /// 是否为正数
        /// </summary>
        public bool IsPositive { get; set; }=true;

        public NumberInputControl()
        {
            InitializeComponent();
        }

        public void SetNumber(double number)
        {
            this.labelContent.Text = number.ToString();
            this.Number = number;
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {         
            string text = (sender as SimpleButton).Text;
            string content = this.GetContent() + text;
            this.ParseInput(content);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.labelContent.Text = "0";
            this.Number = 0;
            this.IsPositive = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.OnClosed?.Invoke(this, new CloseEventArgs { Result = DialogResult.No });
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        private void NumberInputControl_KeyDown(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyCode;
            bool isValid = true;
            string content = this.GetContent();

            if (key == Keys.Back)
            {
                if (!(string.IsNullOrEmpty(content) || string.IsNullOrWhiteSpace(content)))
                {                  
                    content = content.Remove(content.Length - 1);
                    if (string.IsNullOrEmpty(content))
                    {
                        content = "0";
                    }                
                }
            }
            else if (key == Keys.Enter)
            {
                isValid = false;
                this.Finish();
            }
            else if (key >= Keys.D0 && key <= Keys.D9)
            {
                int diff = (int)key - (int)Keys.D0;
                content += diff.ToString();
            }
            else if (key >= Keys.NumPad0 && key < Keys.NumPad9)
            {
                int diff = (int)key - (int)Keys.NumPad0;
                content += diff.ToString();
            }
            else if (key == Keys.OemPeriod || key == Keys.Decimal)
            {
                content += ".";
            }
            else if(key==Keys.Add)
            {
                if(IsPositive==false)
                {
                    IsPositive = true;
                }               
            }
            else if(key==Keys.Subtract)
            {
                if(IsPositive==true)
                {
                    IsPositive = false;
                }
                
            }
            else
            {
                isValid = false;
            }         

            if (isValid)
            {
                this.ParseInput(content);
            }           
        }

        private void ParseInput(string content)
        {
            double number;           
            if (double.TryParse(content, out number))
            {
                this.Number =(IsPositive? number:(-number)) ;
                string tmp = this.Number.ToString();
                if (content.LastIndexOf(".") == content.Length - 1)
                {
                    tmp += ".";
                }
                this.labelContent.Text = (IsPositive? tmp:($"{tmp}"));
            }
        }


        private string GetContent()
        {
            if (this.FirstAppend)
            {
                this.FirstAppend = false;
                this.labelContent.Text = "";
            }
            else
            {
                if (IsPositive == false)
                {
                    if (this.labelContent.Text.Trim().Length > 1)
                    { return this.labelContent.Text.Substring(1); }
                }
            }
             return this.labelContent.Text;
        }

        private void Finish()
        {
            if (string.IsNullOrEmpty(this.labelContent.Text))
            {
                this.Number = 0;
            }
            this.OnClosed?.Invoke(this, new CloseEventArgs { Result = DialogResult.Yes });
        }

        private void btnpm_Click(object sender, EventArgs e)
        {
          string content=  GetContent();
            IsPositive = !IsPositive;
            ParseInput(content);
        }
    }

    public class CloseEventArgs : EventArgs
    {
        public DialogResult Result { get; set; }
    }
}
