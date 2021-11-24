namespace WSXCutTubeSystem.Views.UCControl
{
    partial class UCRectangleTube2
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
            this.txtRectTubeTotalLen = new System.Windows.Forms.TextBox();
            this.txtRectRightAngle = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtRectLeftAngle = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtRectTubeLength = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ucTubeTiltAngleShow1 = new WSXCutTubeSystem.Views.UCControl.UCTubeTiltAngleShow();
            this.SuspendLayout();
            // 
            // txtRectTubeTotalLen
            // 
            this.txtRectTubeTotalLen.Location = new System.Drawing.Point(344, 137);
            this.txtRectTubeTotalLen.Name = "txtRectTubeTotalLen";
            this.txtRectTubeTotalLen.ReadOnly = true;
            this.txtRectTubeTotalLen.Size = new System.Drawing.Size(100, 21);
            this.txtRectTubeTotalLen.TabIndex = 10;
            this.txtRectTubeTotalLen.Text = "80";
            // 
            // txtRectRightAngle
            // 
            this.txtRectRightAngle.Format = null;
            this.txtRectRightAngle.IsInterger = false;
            this.txtRectRightAngle.Location = new System.Drawing.Point(344, 106);
            this.txtRectRightAngle.Margin = new System.Windows.Forms.Padding(0);
            this.txtRectRightAngle.Max = 60D;
            this.txtRectRightAngle.Min = -60D;
            this.txtRectRightAngle.Name = "txtRectRightAngle";
            this.txtRectRightAngle.Number = 0D;
            this.txtRectRightAngle.ReadOnly = false;
            this.txtRectRightAngle.Size = new System.Drawing.Size(101, 20);
            this.txtRectRightAngle.Suffix = null;
            this.txtRectRightAngle.TabIndex = 7;
            this.txtRectRightAngle.TextSize = 9F;
            this.txtRectRightAngle.NumberChanged += new System.Action<object, System.EventArgs>(this.txtRectRightAngle_NumberChanged);
            // 
            // txtRectLeftAngle
            // 
            this.txtRectLeftAngle.Format = null;
            this.txtRectLeftAngle.IsInterger = false;
            this.txtRectLeftAngle.Location = new System.Drawing.Point(344, 77);
            this.txtRectLeftAngle.Margin = new System.Windows.Forms.Padding(0);
            this.txtRectLeftAngle.Max = 60D;
            this.txtRectLeftAngle.Min = -60D;
            this.txtRectLeftAngle.Name = "txtRectLeftAngle";
            this.txtRectLeftAngle.Number = 0D;
            this.txtRectLeftAngle.ReadOnly = false;
            this.txtRectLeftAngle.Size = new System.Drawing.Size(101, 20);
            this.txtRectLeftAngle.Suffix = null;
            this.txtRectLeftAngle.TabIndex = 8;
            this.txtRectLeftAngle.TextSize = 9F;
            this.txtRectLeftAngle.NumberChanged += new System.Action<object, System.EventArgs>(this.txtRectLeftAngle_NumberChanged);
            // 
            // txtRectTubeLength
            // 
            this.txtRectTubeLength.Format = null;
            this.txtRectTubeLength.IsInterger = false;
            this.txtRectTubeLength.Location = new System.Drawing.Point(344, 48);
            this.txtRectTubeLength.Margin = new System.Windows.Forms.Padding(0);
            this.txtRectTubeLength.Max = 10000D;
            this.txtRectTubeLength.Min = 0.1D;
            this.txtRectTubeLength.Name = "txtRectTubeLength";
            this.txtRectTubeLength.Number = 80D;
            this.txtRectTubeLength.ReadOnly = false;
            this.txtRectTubeLength.Size = new System.Drawing.Size(101, 20);
            this.txtRectTubeLength.Suffix = null;
            this.txtRectTubeLength.TabIndex = 9;
            this.txtRectTubeLength.TextSize = 9F;
            this.txtRectTubeLength.NumberChanged += new System.Action<object, System.EventArgs>(this.txtRectTubeLength_NumberChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(282, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "圆管总长:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(282, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "右端倾角:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(282, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "左端倾角:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(282, 48);
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
            this.ucTubeTiltAngleShow1.TabIndex = 11;
            // 
            // UCRectangleTube2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ucTubeTiltAngleShow1);
            this.Controls.Add(this.txtRectTubeTotalLen);
            this.Controls.Add(this.txtRectRightAngle);
            this.Controls.Add(this.txtRectLeftAngle);
            this.Controls.Add(this.txtRectTubeLength);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UCRectangleTube2";
            this.Size = new System.Drawing.Size(500, 208);
            this.VisibleChanged += new System.EventHandler(this.UCRectangleTube2_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtRectTubeTotalLen;
        internal WSX.ControlLibrary.Common.UCNumberInputer txtRectRightAngle;
        internal WSX.ControlLibrary.Common.UCNumberInputer txtRectLeftAngle;
        internal WSX.ControlLibrary.Common.UCNumberInputer txtRectTubeLength;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private UCTubeTiltAngleShow ucTubeTiltAngleShow1;
    }
}
