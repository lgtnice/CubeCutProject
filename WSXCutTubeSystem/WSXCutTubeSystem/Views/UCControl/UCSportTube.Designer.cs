namespace WSXCutTubeSystem.Views.UCControl
{
    partial class UCSportTube
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
            this.txtSportHeight = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtSportWidth = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSportRadius = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // txtSportHeight
            // 
            this.txtSportHeight.Format = null;
            this.txtSportHeight.IsInterger = false;
            this.txtSportHeight.Location = new System.Drawing.Point(355, 105);
            this.txtSportHeight.Margin = new System.Windows.Forms.Padding(0);
            this.txtSportHeight.Max = 10000D;
            this.txtSportHeight.Min = 0.1D;
            this.txtSportHeight.Name = "txtSportHeight";
            this.txtSportHeight.Number = 40D;
            this.txtSportHeight.ReadOnly = false;
            this.txtSportHeight.Size = new System.Drawing.Size(101, 20);
            this.txtSportHeight.Suffix = null;
            this.txtSportHeight.TabIndex = 14;
            this.txtSportHeight.TextSize = 9F;
            this.txtSportHeight.NumberChanged += new System.Action<object, System.EventArgs>(this.txtSportHeight_NumberChanged);
            // 
            // txtSportWidth
            // 
            this.txtSportWidth.Format = null;
            this.txtSportWidth.IsInterger = false;
            this.txtSportWidth.Location = new System.Drawing.Point(355, 52);
            this.txtSportWidth.Margin = new System.Windows.Forms.Padding(0);
            this.txtSportWidth.Max = 999D;
            this.txtSportWidth.Min = 1D;
            this.txtSportWidth.Name = "txtSportWidth";
            this.txtSportWidth.Number = 80D;
            this.txtSportWidth.ReadOnly = false;
            this.txtSportWidth.Size = new System.Drawing.Size(101, 20);
            this.txtSportWidth.Suffix = null;
            this.txtSportWidth.TabIndex = 15;
            this.txtSportWidth.TextSize = 9F;
            this.txtSportWidth.NumberChanged += new System.Action<object, System.EventArgs>(this.txtSportWidth_NumberChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(323, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "跑道半径:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(323, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "高度或直径:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(323, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "宽度(含倒角):";
            // 
            // txtSportRadius
            // 
            this.txtSportRadius.Location = new System.Drawing.Point(355, 167);
            this.txtSportRadius.Name = "txtSportRadius";
            this.txtSportRadius.ReadOnly = true;
            this.txtSportRadius.Size = new System.Drawing.Size(100, 21);
            this.txtSportRadius.TabIndex = 16;
            this.txtSportRadius.Text = "20";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(28, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 136);
            this.panel1.TabIndex = 17;
            // 
            // UCSportTube
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtSportRadius);
            this.Controls.Add(this.txtSportHeight);
            this.Controls.Add(this.txtSportWidth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UCSportTube";
            this.Size = new System.Drawing.Size(500, 208);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal WSX.ControlLibrary.Common.UCNumberInputer txtSportHeight;
        internal WSX.ControlLibrary.Common.UCNumberInputer txtSportWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox txtSportRadius;
        private System.Windows.Forms.Panel panel1;
    }
}
