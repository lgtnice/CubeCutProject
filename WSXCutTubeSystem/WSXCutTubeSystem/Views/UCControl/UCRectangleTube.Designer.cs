namespace WSXCutTubeSystem.Views.UCControl
{
    partial class UCRectangleTube
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
            this.txtShortSideLen = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtLongSideLen = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRoundRadius = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // txtShortSideLen
            // 
            this.txtShortSideLen.Format = null;
            this.txtShortSideLen.IsInterger = false;
            this.txtShortSideLen.Location = new System.Drawing.Point(353, 101);
            this.txtShortSideLen.Margin = new System.Windows.Forms.Padding(0);
            this.txtShortSideLen.Max = 10000D;
            this.txtShortSideLen.Min = 0.1D;
            this.txtShortSideLen.Name = "txtShortSideLen";
            this.txtShortSideLen.Number = 50D;
            this.txtShortSideLen.ReadOnly = false;
            this.txtShortSideLen.Size = new System.Drawing.Size(101, 20);
            this.txtShortSideLen.Suffix = null;
            this.txtShortSideLen.TabIndex = 8;
            this.txtShortSideLen.TextSize = 9F;
            this.txtShortSideLen.NumberChanged += new System.Action<object, System.EventArgs>(this.txtShortSideLen_NumberChanged);
            // 
            // txtLongSideLen
            // 
            this.txtLongSideLen.Format = null;
            this.txtLongSideLen.IsInterger = false;
            this.txtLongSideLen.Location = new System.Drawing.Point(353, 48);
            this.txtLongSideLen.Margin = new System.Windows.Forms.Padding(0);
            this.txtLongSideLen.Max = 10000D;
            this.txtLongSideLen.Min = 0.1D;
            this.txtLongSideLen.Name = "txtLongSideLen";
            this.txtLongSideLen.Number = 80D;
            this.txtLongSideLen.ReadOnly = false;
            this.txtLongSideLen.Size = new System.Drawing.Size(101, 20);
            this.txtLongSideLen.Suffix = null;
            this.txtLongSideLen.TabIndex = 9;
            this.txtLongSideLen.TextSize = 9F;
            this.txtLongSideLen.NumberChanged += new System.Action<object, System.EventArgs>(this.txtLongSideLen_NumberChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(321, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "短边长(含倒角):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(321, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "长边长(含倒角):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(321, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "倒角半径:";
            // 
            // txtRoundRadius
            // 
            this.txtRoundRadius.Format = null;
            this.txtRoundRadius.IsInterger = false;
            this.txtRoundRadius.Location = new System.Drawing.Point(353, 157);
            this.txtRoundRadius.Margin = new System.Windows.Forms.Padding(0);
            this.txtRoundRadius.Max = 100D;
            this.txtRoundRadius.Min = 0D;
            this.txtRoundRadius.Name = "txtRoundRadius";
            this.txtRoundRadius.Number = 5D;
            this.txtRoundRadius.ReadOnly = false;
            this.txtRoundRadius.Size = new System.Drawing.Size(101, 20);
            this.txtRoundRadius.Suffix = null;
            this.txtRoundRadius.TabIndex = 8;
            this.txtRoundRadius.TextSize = 9F;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(28, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 136);
            this.panel1.TabIndex = 10;
            // 
            // UCRectangleTube
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtRoundRadius);
            this.Controls.Add(this.txtShortSideLen);
            this.Controls.Add(this.txtLongSideLen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UCRectangleTube";
            this.Size = new System.Drawing.Size(500, 208);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal WSX.ControlLibrary.Common.UCNumberInputer txtShortSideLen;
        internal WSX.ControlLibrary.Common.UCNumberInputer txtLongSideLen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        internal WSX.ControlLibrary.Common.UCNumberInputer txtRoundRadius;
        private System.Windows.Forms.Panel panel1;
    }
}
