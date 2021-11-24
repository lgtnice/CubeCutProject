namespace WSXCutTubeSystem.Views.UCControl
{
    partial class UCSquareTube
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
            this.txtSideLen = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRoundRadius = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.SuspendLayout();
            // 
            // txtSideLen
            // 
            this.txtSideLen.Format = null;
            this.txtSideLen.IsInterger = false;
            this.txtSideLen.Location = new System.Drawing.Point(323, 77);
            this.txtSideLen.Margin = new System.Windows.Forms.Padding(0);
            this.txtSideLen.Max = 10000D;
            this.txtSideLen.Min = 0.01D;
            this.txtSideLen.Name = "txtSideLen";
            this.txtSideLen.Number = 80D;
            this.txtSideLen.ReadOnly = false;
            this.txtSideLen.Size = new System.Drawing.Size(101, 20);
            this.txtSideLen.Suffix = null;
            this.txtSideLen.TabIndex = 5;
            this.txtSideLen.TextSize = 9F;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::WSXCutTubeSystem.Properties.Resources.square;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(77, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(128, 128);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(291, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "边长(含倒角):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(291, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "倒角半径:";
            // 
            // txtRoundRadius
            // 
            this.txtRoundRadius.Format = null;
            this.txtRoundRadius.IsInterger = false;
            this.txtRoundRadius.Location = new System.Drawing.Point(323, 146);
            this.txtRoundRadius.Margin = new System.Windows.Forms.Padding(0);
            this.txtRoundRadius.Max = 10000D;
            this.txtRoundRadius.Min = 0.1D;
            this.txtRoundRadius.Name = "txtRoundRadius";
            this.txtRoundRadius.Number = 0.1D;
            this.txtRoundRadius.ReadOnly = false;
            this.txtRoundRadius.Size = new System.Drawing.Size(101, 20);
            this.txtRoundRadius.Suffix = null;
            this.txtRoundRadius.TabIndex = 5;
            this.txtRoundRadius.TextSize = 9F;
            // 
            // UCSquareTube
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtRoundRadius);
            this.Controls.Add(this.txtSideLen);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UCSquareTube";
            this.Size = new System.Drawing.Size(500, 208);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal WSX.ControlLibrary.Common.UCNumberInputer txtSideLen;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal WSX.ControlLibrary.Common.UCNumberInputer txtRoundRadius;
    }
}
