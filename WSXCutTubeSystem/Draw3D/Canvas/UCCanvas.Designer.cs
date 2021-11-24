namespace WSX.Draw3D
{
    partial class UCCanvas
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
            this.components = new System.ComponentModel.Container();
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCancelCommand = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openGLControl1
            // 
            this.openGLControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openGLControl1.DrawFPS = false;
            this.openGLControl1.Location = new System.Drawing.Point(0, 0);
            this.openGLControl1.Margin = new System.Windows.Forms.Padding(4);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl1.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl1.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl1.Size = new System.Drawing.Size(822, 613);
            this.openGLControl1.TabIndex = 0;
            this.openGLControl1.OpenGLInitialized += new System.EventHandler(this.openGLControl1_OpenGLInitialized);
            this.openGLControl1.Resized += new System.EventHandler(this.openGLControl1_Resized);
            this.openGLControl1.Load += new System.EventHandler(this.openGLControl1_Load);
            this.openGLControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.openGLControl1_KeyDown);
            this.openGLControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseDown);
            this.openGLControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseMove);
            this.openGLControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsUndo,
            this.tsRedo,
            this.tsCancelCommand});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
            // 
            // tsUndo
            // 
            this.tsUndo.Image = global::WSX.Draw3D.Properties.Resources.Undo;
            this.tsUndo.Name = "tsUndo";
            this.tsUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.tsUndo.Size = new System.Drawing.Size(145, 22);
            this.tsUndo.Text = "撤销";
            this.tsUndo.Click += new System.EventHandler(this.tsUndo_Click);
            // 
            // tsRedo
            // 
            this.tsRedo.Image = global::WSX.Draw3D.Properties.Resources.Redo;
            this.tsRedo.Name = "tsRedo";
            this.tsRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.tsRedo.Size = new System.Drawing.Size(145, 22);
            this.tsRedo.Text = "重做";
            this.tsRedo.Click += new System.EventHandler(this.tsRedo_Click);
            // 
            // tsCancelCommand
            // 
            this.tsCancelCommand.Name = "tsCancelCommand";
            this.tsCancelCommand.Size = new System.Drawing.Size(180, 22);
            this.tsCancelCommand.Text = "取消命令";
            this.tsCancelCommand.Click += new System.EventHandler(this.tsCancelCommand_Click);
            // 
            // UCCanvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.openGLControl1);
            this.Name = "UCCanvas";
            this.Size = new System.Drawing.Size(822, 613);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsUndo;
        private System.Windows.Forms.ToolStripMenuItem tsRedo;
        private System.Windows.Forms.ToolStripMenuItem tsCancelCommand;
    }
}
