namespace WSXCutTubeSystem.Views.Forms
{
    partial class FrmGapCompensation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioAllOuter = new System.Windows.Forms.RadioButton();
            this.radioAllInner = new System.Windows.Forms.RadioButton();
            this.radioAuto = new System.Windows.Forms.RadioButton();
            this.radioCancel = new System.Windows.Forms.RadioButton();
            this.btnCanel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.ucNumberSize = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.label4 = new System.Windows.Forms.Label();
            this.comboIsEnableForSelected = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioAllOuter);
            this.groupBox1.Controls.Add(this.radioAllInner);
            this.groupBox1.Controls.Add(this.radioAuto);
            this.groupBox1.Controls.Add(this.radioCancel);
            this.groupBox1.Location = new System.Drawing.Point(28, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(123, 121);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "样式";
            // 
            // radioAllOuter
            // 
            this.radioAllOuter.AutoSize = true;
            this.radioAllOuter.Location = new System.Drawing.Point(20, 98);
            this.radioAllOuter.Name = "radioAllOuter";
            this.radioAllOuter.Size = new System.Drawing.Size(47, 16);
            this.radioAllOuter.TabIndex = 0;
            this.radioAllOuter.TabStop = true;
            this.radioAllOuter.Text = "外扩";
            this.radioAllOuter.UseVisualStyleBackColor = true;
            // 
            // radioAllInner
            // 
            this.radioAllInner.AutoSize = true;
            this.radioAllInner.Location = new System.Drawing.Point(20, 73);
            this.radioAllInner.Name = "radioAllInner";
            this.radioAllInner.Size = new System.Drawing.Size(47, 16);
            this.radioAllInner.TabIndex = 0;
            this.radioAllInner.TabStop = true;
            this.radioAllInner.Text = "内缩";
            this.radioAllInner.UseVisualStyleBackColor = true;
            // 
            // radioAuto
            // 
            this.radioAuto.AutoSize = true;
            this.radioAuto.Location = new System.Drawing.Point(20, 48);
            this.radioAuto.Name = "radioAuto";
            this.radioAuto.Size = new System.Drawing.Size(71, 16);
            this.radioAuto.TabIndex = 0;
            this.radioAuto.TabStop = true;
            this.radioAuto.Text = "自动判断";
            this.radioAuto.UseVisualStyleBackColor = true;
            // 
            // radioCancel
            // 
            this.radioCancel.AutoSize = true;
            this.radioCancel.Location = new System.Drawing.Point(20, 23);
            this.radioCancel.Name = "radioCancel";
            this.radioCancel.Size = new System.Drawing.Size(71, 16);
            this.radioCancel.TabIndex = 0;
            this.radioCancel.TabStop = true;
            this.radioCancel.Text = "取消补偿";
            this.radioCancel.UseVisualStyleBackColor = true;
            // 
            // btnCanel
            // 
            this.btnCanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCanel.Location = new System.Drawing.Point(247, 226);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new System.Drawing.Size(75, 23);
            this.btnCanel.TabIndex = 8;
            this.btnCanel.Text = "取消";
            this.btnCanel.UseVisualStyleBackColor = true;
            this.btnCanel.Click += new System.EventHandler(this.btnCanel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.Location = new System.Drawing.Point(166, 226);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // ucNumberSize
            // 
            this.ucNumberSize.Format = null;
            this.ucNumberSize.IsInterger = false;
            this.ucNumberSize.Location = new System.Drawing.Point(230, 95);
            this.ucNumberSize.Margin = new System.Windows.Forms.Padding(0);
            this.ucNumberSize.Max = 999999D;
            this.ucNumberSize.Min = 0D;
            this.ucNumberSize.Name = "ucNumberSize";
            this.ucNumberSize.Number = 0D;
            this.ucNumberSize.ReadOnly = false;
            this.ucNumberSize.Size = new System.Drawing.Size(82, 20);
            this.ucNumberSize.Suffix = "mm";
            this.ucNumberSize.TabIndex = 13;
            this.ucNumberSize.TextSize = 9F;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(157, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "割缝宽度：";
            // 
            // comboIsEnableForSelected
            // 
            this.comboIsEnableForSelected.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboIsEnableForSelected.FormattingEnabled = true;
            this.comboIsEnableForSelected.Items.AddRange(new object[] {
            "对选中图形生效",
            "对所有图形生效"});
            this.comboIsEnableForSelected.Location = new System.Drawing.Point(159, 129);
            this.comboIsEnableForSelected.Name = "comboIsEnableForSelected";
            this.comboIsEnableForSelected.Size = new System.Drawing.Size(153, 20);
            this.comboIsEnableForSelected.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(348, 74);
            this.panel1.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "为图形生成指定参数的补偿刀路。";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(27, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "割缝补偿";
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // FrmGapCompensation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(348, 268);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.comboIsEnableForSelected);
            this.Controls.Add(this.ucNumberSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCanel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGapCompensation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "割缝补偿";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioAllInner;
        private System.Windows.Forms.RadioButton radioAuto;
        private System.Windows.Forms.RadioButton radioCancel;
        private System.Windows.Forms.RadioButton radioAllOuter;
        private System.Windows.Forms.Button btnCanel;
        private System.Windows.Forms.Button btnOk;
        private WSX.ControlLibrary.Common.UCNumberInputer ucNumberSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboIsEnableForSelected;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
    }
}