namespace WSX.Draw3D
{
    partial class DrawComponent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawComponent));
            this.tsSelect = new System.Windows.Forms.ToolStrip();
            this.tsSelectMode = new System.Windows.Forms.ToolStripButton();
            this.tsViewMode = new System.Windows.Forms.ToolStripButton();
            this.tsMoveMode = new System.Windows.Forms.ToolStripButton();
            this.tsSortMode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMain = new System.Windows.Forms.ToolStripButton();
            this.tsBack = new System.Windows.Forms.ToolStripButton();
            this.tsLeft = new System.Windows.Forms.ToolStripButton();
            this.tsRight = new System.Windows.Forms.ToolStripButton();
            this.tsTop = new System.Windows.Forms.ToolStripButton();
            this.tsUp = new System.Windows.Forms.ToolStripButton();
            this.tsSouthEast = new System.Windows.Forms.ToolStripButton();
            this.tsSouthWest = new System.Windows.Forms.ToolStripButton();
            this.tsNorthEast = new System.Windows.Forms.ToolStripButton();
            this.tsNorthWest = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ucCanvas1 = new WSX.Draw3D.UCCanvas();
            this.tsSelect.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsSelect
            // 
            this.tsSelect.Dock = System.Windows.Forms.DockStyle.Left;
            this.tsSelect.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSelectMode,
            this.tsViewMode,
            this.tsMoveMode,
            this.tsSortMode,
            this.toolStripSeparator1,
            this.tsMain,
            this.tsBack,
            this.tsLeft,
            this.tsRight,
            this.tsTop,
            this.tsUp,
            this.tsSouthEast,
            this.tsSouthWest,
            this.tsNorthEast,
            this.tsNorthWest});
            this.tsSelect.Location = new System.Drawing.Point(0, 0);
            this.tsSelect.Name = "tsSelect";
            this.tsSelect.Size = new System.Drawing.Size(32, 613);
            this.tsSelect.TabIndex = 1;
            // 
            // tsSelectMode
            // 
            this.tsSelectMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSelectMode.Image = ((System.Drawing.Image)(resources.GetObject("tsSelectMode.Image")));
            this.tsSelectMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSelectMode.Name = "tsSelectMode";
            this.tsSelectMode.Size = new System.Drawing.Size(29, 20);
            this.tsSelectMode.Text = "toolStripButton1";
            this.tsSelectMode.ToolTipText = "选择";
            this.tsSelectMode.Click += new System.EventHandler(this.tsSelectMode_Click);
            // 
            // tsViewMode
            // 
            this.tsViewMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsViewMode.Image = ((System.Drawing.Image)(resources.GetObject("tsViewMode.Image")));
            this.tsViewMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsViewMode.Name = "tsViewMode";
            this.tsViewMode.Size = new System.Drawing.Size(29, 20);
            this.tsViewMode.Text = "toolStripButton1";
            this.tsViewMode.ToolTipText = "旋转";
            this.tsViewMode.Click += new System.EventHandler(this.tsViewMode_Click);
            // 
            // tsMoveMode
            // 
            this.tsMoveMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsMoveMode.Image = ((System.Drawing.Image)(resources.GetObject("tsMoveMode.Image")));
            this.tsMoveMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMoveMode.Name = "tsMoveMode";
            this.tsMoveMode.Size = new System.Drawing.Size(29, 20);
            this.tsMoveMode.Text = "toolStripButton1";
            this.tsMoveMode.ToolTipText = "平移";
            this.tsMoveMode.Click += new System.EventHandler(this.tsMoveMode_Click);
            // 
            // tsSortMode
            // 
            this.tsSortMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSortMode.Image = ((System.Drawing.Image)(resources.GetObject("tsSortMode.Image")));
            this.tsSortMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSortMode.Name = "tsSortMode";
            this.tsSortMode.Size = new System.Drawing.Size(29, 20);
            this.tsSortMode.Text = "toolStripButton1";
            this.tsSortMode.ToolTipText = "排序";
            this.tsSortMode.Click += new System.EventHandler(this.tsSortMode_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(29, 6);
            // 
            // tsMain
            // 
            this.tsMain.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsMain.Image = global::WSX.Draw3D.Properties.Resources.main;
            this.tsMain.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(29, 20);
            this.tsMain.ToolTipText = "主视图";
            this.tsMain.Click += new System.EventHandler(this.tsMain_Click);
            // 
            // tsBack
            // 
            this.tsBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBack.Image = global::WSX.Draw3D.Properties.Resources.back;
            this.tsBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBack.Name = "tsBack";
            this.tsBack.Size = new System.Drawing.Size(29, 20);
            this.tsBack.ToolTipText = "后视图";
            this.tsBack.Click += new System.EventHandler(this.tsBack_Click);
            // 
            // tsLeft
            // 
            this.tsLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsLeft.Image = global::WSX.Draw3D.Properties.Resources.left;
            this.tsLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsLeft.Name = "tsLeft";
            this.tsLeft.Size = new System.Drawing.Size(29, 20);
            this.tsLeft.ToolTipText = "左视图";
            this.tsLeft.Click += new System.EventHandler(this.tsLeft_Click);
            // 
            // tsRight
            // 
            this.tsRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsRight.Image = global::WSX.Draw3D.Properties.Resources.right;
            this.tsRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRight.Name = "tsRight";
            this.tsRight.Size = new System.Drawing.Size(29, 20);
            this.tsRight.ToolTipText = "右视图";
            this.tsRight.Click += new System.EventHandler(this.tsRight_Click);
            // 
            // tsTop
            // 
            this.tsTop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsTop.Image = global::WSX.Draw3D.Properties.Resources.top;
            this.tsTop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsTop.Name = "tsTop";
            this.tsTop.Size = new System.Drawing.Size(29, 20);
            this.tsTop.ToolTipText = "俯视图";
            this.tsTop.Click += new System.EventHandler(this.tsTop_Click);
            // 
            // tsUp
            // 
            this.tsUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsUp.Image = global::WSX.Draw3D.Properties.Resources.bottom;
            this.tsUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsUp.Name = "tsUp";
            this.tsUp.Size = new System.Drawing.Size(29, 20);
            this.tsUp.ToolTipText = "仰视图";
            this.tsUp.Click += new System.EventHandler(this.tsUp_Click);
            // 
            // tsSouthEast
            // 
            this.tsSouthEast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSouthEast.Image = global::WSX.Draw3D.Properties.Resources.southeast;
            this.tsSouthEast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSouthEast.Name = "tsSouthEast";
            this.tsSouthEast.Size = new System.Drawing.Size(29, 20);
            this.tsSouthEast.ToolTipText = "东南等轴";
            this.tsSouthEast.Click += new System.EventHandler(this.tsSouthEast_Click);
            // 
            // tsSouthWest
            // 
            this.tsSouthWest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSouthWest.Image = global::WSX.Draw3D.Properties.Resources.southwest;
            this.tsSouthWest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSouthWest.Name = "tsSouthWest";
            this.tsSouthWest.Size = new System.Drawing.Size(29, 20);
            this.tsSouthWest.ToolTipText = "西南等轴";
            this.tsSouthWest.Click += new System.EventHandler(this.tsSouthWest_Click);
            // 
            // tsNorthEast
            // 
            this.tsNorthEast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNorthEast.Image = global::WSX.Draw3D.Properties.Resources.northeast;
            this.tsNorthEast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNorthEast.Name = "tsNorthEast";
            this.tsNorthEast.Size = new System.Drawing.Size(29, 20);
            this.tsNorthEast.ToolTipText = "东北等轴";
            this.tsNorthEast.Click += new System.EventHandler(this.tsNorthEast_Click);
            // 
            // tsNorthWest
            // 
            this.tsNorthWest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNorthWest.Image = global::WSX.Draw3D.Properties.Resources.northwest;
            this.tsNorthWest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNorthWest.Name = "tsNorthWest";
            this.tsNorthWest.Size = new System.Drawing.Size(29, 20);
            this.tsNorthWest.ToolTipText = "西北等轴";
            this.tsNorthWest.Click += new System.EventHandler(this.tsNorthWest_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tsSelect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(21, 613);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ucCanvas1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(21, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(825, 613);
            this.panel2.TabIndex = 4;
            // 
            // ucCanvas1
            // 
            this.ucCanvas1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCanvas1.Location = new System.Drawing.Point(0, 0);
            this.ucCanvas1.Name = "ucCanvas1";
            this.ucCanvas1.Size = new System.Drawing.Size(825, 613);
            this.ucCanvas1.TabIndex = 0;
            // 
            // DrawComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DrawComponent";
            this.Size = new System.Drawing.Size(846, 613);
            this.tsSelect.ResumeLayout(false);
            this.tsSelect.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UCCanvas ucCanvas1;
        private System.Windows.Forms.ToolStrip tsSelect;
        private System.Windows.Forms.ToolStripButton tsSelectMode;
        private System.Windows.Forms.ToolStripButton tsViewMode;
        private System.Windows.Forms.ToolStripButton tsMoveMode;
        private System.Windows.Forms.ToolStripButton tsSortMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsMain;
        private System.Windows.Forms.ToolStripButton tsBack;
        private System.Windows.Forms.ToolStripButton tsLeft;
        private System.Windows.Forms.ToolStripButton tsRight;
        private System.Windows.Forms.ToolStripButton tsTop;
        private System.Windows.Forms.ToolStripButton tsUp;
        private System.Windows.Forms.ToolStripButton tsSouthEast;
        private System.Windows.Forms.ToolStripButton tsSouthWest;
        private System.Windows.Forms.ToolStripButton tsNorthEast;
        private System.Windows.Forms.ToolStripButton tsNorthWest;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
