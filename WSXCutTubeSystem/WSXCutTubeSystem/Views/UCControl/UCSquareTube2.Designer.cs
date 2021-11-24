namespace WSXCutTubeSystem.Views.UCControl
{
    partial class UCSquareTube2
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSquareTubeTotalLen = new System.Windows.Forms.TextBox();
            this.txtSquareRightAngle = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtSquareLeftAngle = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtSquareTubeLength = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ucTubeTiltAngleShow1 = new WSXCutTubeSystem.Views.UCControl.UCTubeTiltAngleShow();
            this.SuspendLayout();
            // 
            // txtSquareTubeTotalLen
            // 
            this.txtSquareTubeTotalLen.Location = new System.Drawing.Point(349, 144);
            this.txtSquareTubeTotalLen.Name = "txtSquareTubeTotalLen";
            this.txtSquareTubeTotalLen.ReadOnly = true;
            this.txtSquareTubeTotalLen.Size = new System.Drawing.Size(100, 21);
            this.txtSquareTubeTotalLen.TabIndex = 10;
            this.txtSquareTubeTotalLen.Text = "15";
            // 
            // txtSquareRightAngle
            // 
            this.txtSquareRightAngle.Format = null;
            this.txtSquareRightAngle.IsInterger = false;
            this.txtSquareRightAngle.Location = new System.Drawing.Point(349, 113);
            this.txtSquareRightAngle.Margin = new System.Windows.Forms.Padding(0);
            this.txtSquareRightAngle.Max = 60D;
            this.txtSquareRightAngle.Min = -60D;
            this.txtSquareRightAngle.Name = "txtSquareRightAngle";
            this.txtSquareRightAngle.Number = 0D;
            this.txtSquareRightAngle.ReadOnly = false;
            this.txtSquareRightAngle.Size = new System.Drawing.Size(101, 20);
            this.txtSquareRightAngle.Suffix = null;
            this.txtSquareRightAngle.TabIndex = 7;
            this.txtSquareRightAngle.TextSize = 9F;
            this.txtSquareRightAngle.NumberChanged += new System.Action<object, System.EventArgs>(this.txtSquareRightAngle_NumberChanged);
            // 
            // txtSquareLeftAngle
            // 
            this.txtSquareLeftAngle.Format = null;
            this.txtSquareLeftAngle.IsInterger = false;
            this.txtSquareLeftAngle.Location = new System.Drawing.Point(349, 84);
            this.txtSquareLeftAngle.Margin = new System.Windows.Forms.Padding(0);
            this.txtSquareLeftAngle.Max = 60D;
            this.txtSquareLeftAngle.Min = -60D;
            this.txtSquareLeftAngle.Name = "txtSquareLeftAngle";
            this.txtSquareLeftAngle.Number = 0D;
            this.txtSquareLeftAngle.ReadOnly = false;
            this.txtSquareLeftAngle.Size = new System.Drawing.Size(101, 20);
            this.txtSquareLeftAngle.Suffix = null;
            this.txtSquareLeftAngle.TabIndex = 8;
            this.txtSquareLeftAngle.TextSize = 9F;
            this.txtSquareLeftAngle.NumberChanged += new System.Action<object, System.EventArgs>(this.txtSquareLeftAngle_NumberChanged);
            // 
            // txtSquareTubeLength
            // 
            this.txtSquareTubeLength.Format = null;
            this.txtSquareTubeLength.IsInterger = false;
            this.txtSquareTubeLength.Location = new System.Drawing.Point(349, 55);
            this.txtSquareTubeLength.Margin = new System.Windows.Forms.Padding(0);
            this.txtSquareTubeLength.Max = 10000D;
            this.txtSquareTubeLength.Min = 0.1D;
            this.txtSquareTubeLength.Name = "txtSquareTubeLength";
            this.txtSquareTubeLength.Number = 15D;
            this.txtSquareTubeLength.ReadOnly = false;
            this.txtSquareTubeLength.Size = new System.Drawing.Size(101, 20);
            this.txtSquareTubeLength.Suffix = null;
            this.txtSquareTubeLength.TabIndex = 9;
            this.txtSquareTubeLength.TextSize = 9F;
            this.txtSquareTubeLength.NumberChanged += new System.Action<object, System.EventArgs>(this.txtSquareTubeLength_NumberChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(287, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "圆管总长:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(287, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "右端倾角:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(287, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "左端倾角:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(287, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "管件长度:";
            // 
            // ucTubeTiltAngleShow1
            // 
            this.ucTubeTiltAngleShow1.LeftAngle = 0D;
            this.ucTubeTiltAngleShow1.Location = new System.Drawing.Point(3, 3);
            this.ucTubeTiltAngleShow1.Name = "ucTubeTiltAngleShow1";
            this.ucTubeTiltAngleShow1.RightAngle = 0D;
            this.ucTubeTiltAngleShow1.Size = new System.Drawing.Size(280, 180);
            this.ucTubeTiltAngleShow1.TabIndex = 12;
            // 
            // UCSquareTube2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ucTubeTiltAngleShow1);
            this.Controls.Add(this.txtSquareTubeTotalLen);
            this.Controls.Add(this.txtSquareRightAngle);
            this.Controls.Add(this.txtSquareLeftAngle);
            this.Controls.Add(this.txtSquareTubeLength);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UCSquareTube2";
            this.Size = new System.Drawing.Size(500, 208);
            this.VisibleChanged += new System.EventHandler(this.UCSquareTube2_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtSquareTubeTotalLen;
        internal WSX.ControlLibrary.Common.UCNumberInputer txtSquareRightAngle;
        internal WSX.ControlLibrary.Common.UCNumberInputer txtSquareLeftAngle;
        internal WSX.ControlLibrary.Common.UCNumberInputer txtSquareTubeLength;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private UCTubeTiltAngleShow ucTubeTiltAngleShow1;
    }
}
