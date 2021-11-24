namespace WSXCutTubeSystem.Views.UCControl
{
    partial class UCSportTube2
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
            this.txtSportTubeTotalLen = new System.Windows.Forms.TextBox();
            this.txtSportRightAngle = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtSportLeftAngle = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtSportTubeLength = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ucTubeTiltAngleShow1 = new WSXCutTubeSystem.Views.UCControl.UCTubeTiltAngleShow();
            this.SuspendLayout();
            // 
            // txtSportTubeTotalLen
            // 
            this.txtSportTubeTotalLen.Location = new System.Drawing.Point(369, 138);
            this.txtSportTubeTotalLen.Name = "txtSportTubeTotalLen";
            this.txtSportTubeTotalLen.ReadOnly = true;
            this.txtSportTubeTotalLen.Size = new System.Drawing.Size(100, 21);
            this.txtSportTubeTotalLen.TabIndex = 18;
            this.txtSportTubeTotalLen.Text = "50";
            // 
            // txtSportRightAngle
            // 
            this.txtSportRightAngle.Format = null;
            this.txtSportRightAngle.IsInterger = false;
            this.txtSportRightAngle.Location = new System.Drawing.Point(369, 107);
            this.txtSportRightAngle.Margin = new System.Windows.Forms.Padding(0);
            this.txtSportRightAngle.Max = 60D;
            this.txtSportRightAngle.Min = -60D;
            this.txtSportRightAngle.Name = "txtSportRightAngle";
            this.txtSportRightAngle.Number = 0D;
            this.txtSportRightAngle.ReadOnly = false;
            this.txtSportRightAngle.Size = new System.Drawing.Size(101, 20);
            this.txtSportRightAngle.Suffix = null;
            this.txtSportRightAngle.TabIndex = 15;
            this.txtSportRightAngle.TextSize = 9F;
            this.txtSportRightAngle.NumberChanged += new System.Action<object, System.EventArgs>(this.txtSportRightAngle_NumberChanged);
            // 
            // txtSportLeftAngle
            // 
            this.txtSportLeftAngle.Format = null;
            this.txtSportLeftAngle.IsInterger = false;
            this.txtSportLeftAngle.Location = new System.Drawing.Point(369, 78);
            this.txtSportLeftAngle.Margin = new System.Windows.Forms.Padding(0);
            this.txtSportLeftAngle.Max = 60D;
            this.txtSportLeftAngle.Min = -60D;
            this.txtSportLeftAngle.Name = "txtSportLeftAngle";
            this.txtSportLeftAngle.Number = 0D;
            this.txtSportLeftAngle.ReadOnly = false;
            this.txtSportLeftAngle.Size = new System.Drawing.Size(101, 20);
            this.txtSportLeftAngle.Suffix = null;
            this.txtSportLeftAngle.TabIndex = 16;
            this.txtSportLeftAngle.TextSize = 9F;
            this.txtSportLeftAngle.NumberChanged += new System.Action<object, System.EventArgs>(this.txtSportLeftAngle_NumberChanged);
            // 
            // txtSportTubeLength
            // 
            this.txtSportTubeLength.Format = null;
            this.txtSportTubeLength.IsInterger = false;
            this.txtSportTubeLength.Location = new System.Drawing.Point(369, 49);
            this.txtSportTubeLength.Margin = new System.Windows.Forms.Padding(0);
            this.txtSportTubeLength.Max = 10000D;
            this.txtSportTubeLength.Min = 0.1D;
            this.txtSportTubeLength.Name = "txtSportTubeLength";
            this.txtSportTubeLength.Number = 50D;
            this.txtSportTubeLength.ReadOnly = false;
            this.txtSportTubeLength.Size = new System.Drawing.Size(101, 20);
            this.txtSportTubeLength.Suffix = null;
            this.txtSportTubeLength.TabIndex = 17;
            this.txtSportTubeLength.TextSize = 9F;
            this.txtSportTubeLength.NumberChanged += new System.Action<object, System.EventArgs>(this.txtSportTubeLength_NumberChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(307, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "圆管总长:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(307, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "右端倾角:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(307, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "左端倾角:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(307, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "管件长度:";
            // 
            // ucTubeTiltAngleShow1
            // 
            this.ucTubeTiltAngleShow1.LeftAngle = 0D;
            this.ucTubeTiltAngleShow1.Location = new System.Drawing.Point(3, 3);
            this.ucTubeTiltAngleShow1.Name = "ucTubeTiltAngleShow1";
            this.ucTubeTiltAngleShow1.RightAngle = 0D;
            this.ucTubeTiltAngleShow1.Size = new System.Drawing.Size(280, 180);
            this.ucTubeTiltAngleShow1.TabIndex = 19;
            // 
            // UCSportTube2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ucTubeTiltAngleShow1);
            this.Controls.Add(this.txtSportTubeTotalLen);
            this.Controls.Add(this.txtSportRightAngle);
            this.Controls.Add(this.txtSportLeftAngle);
            this.Controls.Add(this.txtSportTubeLength);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UCSportTube2";
            this.Size = new System.Drawing.Size(500, 208);
            this.VisibleChanged += new System.EventHandler(this.UCSportTube2_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtSportTubeTotalLen;
        internal WSX.ControlLibrary.Common.UCNumberInputer txtSportRightAngle;
        internal WSX.ControlLibrary.Common.UCNumberInputer txtSportLeftAngle;
        internal WSX.ControlLibrary.Common.UCNumberInputer txtSportTubeLength;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private UCTubeTiltAngleShow ucTubeTiltAngleShow1;
    }
}
