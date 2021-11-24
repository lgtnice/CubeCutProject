namespace WSXCutTubeSystem.Views.UCControl
{
    partial class UCCircleTube
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCircleTube));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCircleRadius = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(271, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "请指定圆管半径:";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(57, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(128, 128);
            this.panel1.TabIndex = 1;
            // 
            // txtCircleRadius
            // 
            this.txtCircleRadius.Format = null;
            this.txtCircleRadius.IsInterger = false;
            this.txtCircleRadius.Location = new System.Drawing.Point(303, 101);
            this.txtCircleRadius.Margin = new System.Windows.Forms.Padding(0);
            this.txtCircleRadius.Max = 100D;
            this.txtCircleRadius.Min = 0D;
            this.txtCircleRadius.Name = "txtCircleRadius";
            this.txtCircleRadius.Number = 20D;
            this.txtCircleRadius.ReadOnly = false;
            this.txtCircleRadius.Size = new System.Drawing.Size(101, 20);
            this.txtCircleRadius.Suffix = null;
            this.txtCircleRadius.TabIndex = 2;
            this.txtCircleRadius.TextSize = 9F;
            // 
            // UCCircleTube
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtCircleRadius);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "UCCircleTube";
            this.Size = new System.Drawing.Size(500, 208);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        internal WSX.ControlLibrary.Common.UCNumberInputer txtCircleRadius;
    }
}
