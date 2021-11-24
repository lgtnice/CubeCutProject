namespace WSXCutTubeSystem.Views.UCControl
{
    partial class UCCircleTube2
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtCircleTubeLength = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCircleLeftAngle = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCircleRightAngle = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCircleTubeTotalLen = new System.Windows.Forms.TextBox();
            this.ucTubeTiltAngleShow1 = new WSXCutTubeSystem.Views.UCControl.UCTubeTiltAngleShow();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(280, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "管件长度:";
            // 
            // txtCircleTubeLength
            // 
            this.txtCircleTubeLength.Format = null;
            this.txtCircleTubeLength.IsInterger = false;
            this.txtCircleTubeLength.Location = new System.Drawing.Point(342, 53);
            this.txtCircleTubeLength.Margin = new System.Windows.Forms.Padding(0);
            this.txtCircleTubeLength.Max = 100D;
            this.txtCircleTubeLength.Min = 0D;
            this.txtCircleTubeLength.Name = "txtCircleTubeLength";
            this.txtCircleTubeLength.Number = 50D;
            this.txtCircleTubeLength.ReadOnly = false;
            this.txtCircleTubeLength.Size = new System.Drawing.Size(101, 20);
            this.txtCircleTubeLength.Suffix = null;
            this.txtCircleTubeLength.TabIndex = 1;
            this.txtCircleTubeLength.TextSize = 9F;
            this.txtCircleTubeLength.NumberChanged += new System.Action<object, System.EventArgs>(this.txtCircleTubeLength_NumberChanged);
            this.txtCircleTubeLength.Leave += new System.EventHandler(this.txtCircleTubeLength_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(280, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "左端倾角:";
            // 
            // txtCircleLeftAngle
            // 
            this.txtCircleLeftAngle.Format = null;
            this.txtCircleLeftAngle.IsInterger = false;
            this.txtCircleLeftAngle.Location = new System.Drawing.Point(342, 82);
            this.txtCircleLeftAngle.Margin = new System.Windows.Forms.Padding(0);
            this.txtCircleLeftAngle.Max = 60D;
            this.txtCircleLeftAngle.Min = -60D;
            this.txtCircleLeftAngle.Name = "txtCircleLeftAngle";
            this.txtCircleLeftAngle.Number = 0D;
            this.txtCircleLeftAngle.ReadOnly = false;
            this.txtCircleLeftAngle.Size = new System.Drawing.Size(101, 20);
            this.txtCircleLeftAngle.Suffix = null;
            this.txtCircleLeftAngle.TabIndex = 1;
            this.txtCircleLeftAngle.TextSize = 9F;
            this.txtCircleLeftAngle.NumberChanged += new System.Action<object, System.EventArgs>(this.txtCircleLeftAngle_NumberChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(280, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "右端倾角:";
            // 
            // txtCircleRightAngle
            // 
            this.txtCircleRightAngle.Format = null;
            this.txtCircleRightAngle.IsInterger = false;
            this.txtCircleRightAngle.Location = new System.Drawing.Point(342, 111);
            this.txtCircleRightAngle.Margin = new System.Windows.Forms.Padding(0);
            this.txtCircleRightAngle.Max = 60D;
            this.txtCircleRightAngle.Min = -60D;
            this.txtCircleRightAngle.Name = "txtCircleRightAngle";
            this.txtCircleRightAngle.Number = 0D;
            this.txtCircleRightAngle.ReadOnly = false;
            this.txtCircleRightAngle.Size = new System.Drawing.Size(101, 20);
            this.txtCircleRightAngle.Suffix = null;
            this.txtCircleRightAngle.TabIndex = 1;
            this.txtCircleRightAngle.TextSize = 9F;
            this.txtCircleRightAngle.NumberChanged += new System.Action<object, System.EventArgs>(this.txtCircleRightAngle_NumberChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(280, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "圆管总长:";
            // 
            // txtCircleTubeTotalLen
            // 
            this.txtCircleTubeTotalLen.Location = new System.Drawing.Point(342, 142);
            this.txtCircleTubeTotalLen.Name = "txtCircleTubeTotalLen";
            this.txtCircleTubeTotalLen.ReadOnly = true;
            this.txtCircleTubeTotalLen.Size = new System.Drawing.Size(100, 21);
            this.txtCircleTubeTotalLen.TabIndex = 2;
            this.txtCircleTubeTotalLen.Text = "50";
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
            // UCCircleTube2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ucTubeTiltAngleShow1);
            this.Controls.Add(this.txtCircleTubeTotalLen);
            this.Controls.Add(this.txtCircleRightAngle);
            this.Controls.Add(this.txtCircleLeftAngle);
            this.Controls.Add(this.txtCircleTubeLength);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UCCircleTube2";
            this.Size = new System.Drawing.Size(500, 208);
            this.VisibleChanged += new System.EventHandler(this.UCCircleTube2_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        internal WSX.ControlLibrary.Common.UCNumberInputer txtCircleTubeLength;
        internal WSX.ControlLibrary.Common.UCNumberInputer txtCircleLeftAngle;
        internal WSX.ControlLibrary.Common.UCNumberInputer txtCircleRightAngle;
        internal System.Windows.Forms.TextBox txtCircleTubeTotalLen;
        private UCTubeTiltAngleShow ucTubeTiltAngleShow1;
    }
}
