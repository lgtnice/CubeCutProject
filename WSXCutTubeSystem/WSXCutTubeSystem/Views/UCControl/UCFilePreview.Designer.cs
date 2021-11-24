namespace WSXCutTubeSystem.Views.UCControl
{
    partial class UCFilePreview
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ckPreView = new System.Windows.Forms.CheckBox();
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ckPreView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(402, 35);
            this.panel1.TabIndex = 5;
            this.panel1.Tag = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 6;
            // 
            // ckPreView
            // 
            this.ckPreView.AutoSize = true;
            this.ckPreView.Checked = true;
            this.ckPreView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckPreView.Location = new System.Drawing.Point(315, 8);
            this.ckPreView.Name = "ckPreView";
            this.ckPreView.Size = new System.Drawing.Size(48, 16);
            this.ckPreView.TabIndex = 5;
            this.ckPreView.Text = "预览";
            this.ckPreView.UseVisualStyleBackColor = true;
            // 
            // openGLControl1
            // 
            this.openGLControl1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.openGLControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openGLControl1.DrawFPS = false;
            this.openGLControl1.ForeColor = System.Drawing.SystemColors.Control;
            this.openGLControl1.Location = new System.Drawing.Point(0, 35);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl1.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl1.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl1.Size = new System.Drawing.Size(402, 321);
            this.openGLControl1.TabIndex = 0;
            // 
            // UCFilePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.openGLControl1);
            this.Controls.Add(this.panel1);
            this.Name = "UCFilePreview";
            this.Size = new System.Drawing.Size(402, 356);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox ckPreView;
        private SharpGL.OpenGLControl openGLControl1;
        private System.Windows.Forms.Label label1;
    }
}
