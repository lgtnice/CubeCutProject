namespace WSXCutTubeSystem.Views.Forms
{
    partial class FrmArray
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ucNumberCount = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCanel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButtonOffset = new System.Windows.Forms.RadioButton();
            this.radioButtonGap = new System.Windows.Forms.RadioButton();
            this.ucNumberDistance = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.checkBoxOnlySelected = new System.Windows.Forms.CheckBox();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(421, 74);
            this.panel1.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "在拉伸方向上快速复制零件。";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(27, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "阵列";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 31;
            this.label3.Text = "数量：";
            // 
            // ucNumberCount
            // 
            this.ucNumberCount.Format = null;
            this.ucNumberCount.IsInterger = false;
            this.ucNumberCount.Location = new System.Drawing.Point(144, 97);
            this.ucNumberCount.Margin = new System.Windows.Forms.Padding(0);
            this.ucNumberCount.Max = 100D;
            this.ucNumberCount.Min = 0D;
            this.ucNumberCount.Name = "ucNumberCount";
            this.ucNumberCount.Number = 5D;
            this.ucNumberCount.ReadOnly = false;
            this.ucNumberCount.Size = new System.Drawing.Size(83, 20);
            this.ucNumberCount.Suffix = "";
            this.ucNumberCount.TabIndex = 30;
            this.ucNumberCount.TextSize = 9F;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(252, 97);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(157, 118);
            this.panel2.TabIndex = 32;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // btnCanel
            // 
            this.btnCanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCanel.Location = new System.Drawing.Point(288, 244);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new System.Drawing.Size(75, 23);
            this.btnCanel.TabIndex = 33;
            this.btnCanel.Text = "取消";
            this.btnCanel.UseVisualStyleBackColor = true;
            this.btnCanel.Click += new System.EventHandler(this.btnCanel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.Location = new System.Drawing.Point(207, 244);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 34;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 31;
            this.label4.Text = "位置：";
            // 
            // radioButtonOffset
            // 
            this.radioButtonOffset.AutoSize = true;
            this.radioButtonOffset.Location = new System.Drawing.Point(85, 133);
            this.radioButtonOffset.Name = "radioButtonOffset";
            this.radioButtonOffset.Size = new System.Drawing.Size(47, 16);
            this.radioButtonOffset.TabIndex = 35;
            this.radioButtonOffset.TabStop = true;
            this.radioButtonOffset.Text = "偏移";
            this.radioButtonOffset.UseVisualStyleBackColor = true;
            this.radioButtonOffset.CheckedChanged += new System.EventHandler(this.radioButtonOffset_CheckedChanged);
            // 
            // radioButtonGap
            // 
            this.radioButtonGap.AutoSize = true;
            this.radioButtonGap.Location = new System.Drawing.Point(144, 133);
            this.radioButtonGap.Name = "radioButtonGap";
            this.radioButtonGap.Size = new System.Drawing.Size(47, 16);
            this.radioButtonGap.TabIndex = 35;
            this.radioButtonGap.TabStop = true;
            this.radioButtonGap.Text = "间距";
            this.radioButtonGap.UseVisualStyleBackColor = true;
            this.radioButtonGap.CheckedChanged += new System.EventHandler(this.radioButtonGap_CheckedChanged);
            // 
            // ucNumberDistance
            // 
            this.ucNumberDistance.Format = null;
            this.ucNumberDistance.IsInterger = false;
            this.ucNumberDistance.Location = new System.Drawing.Point(144, 158);
            this.ucNumberDistance.Margin = new System.Windows.Forms.Padding(0);
            this.ucNumberDistance.Max = 100D;
            this.ucNumberDistance.Min = 0D;
            this.ucNumberDistance.Name = "ucNumberDistance";
            this.ucNumberDistance.Number = 5D;
            this.ucNumberDistance.ReadOnly = false;
            this.ucNumberDistance.Size = new System.Drawing.Size(83, 20);
            this.ucNumberDistance.Suffix = "mm";
            this.ucNumberDistance.TabIndex = 30;
            this.ucNumberDistance.TextSize = 9F;
            // 
            // checkBoxOnlySelected
            // 
            this.checkBoxOnlySelected.AutoSize = true;
            this.checkBoxOnlySelected.Location = new System.Drawing.Point(29, 199);
            this.checkBoxOnlySelected.Name = "checkBoxOnlySelected";
            this.checkBoxOnlySelected.Size = new System.Drawing.Size(108, 16);
            this.checkBoxOnlySelected.TabIndex = 36;
            this.checkBoxOnlySelected.Text = "仅复制选中图形";
            this.checkBoxOnlySelected.UseVisualStyleBackColor = true;
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // FrmArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 280);
            this.Controls.Add(this.checkBoxOnlySelected);
            this.Controls.Add(this.radioButtonGap);
            this.Controls.Add(this.radioButtonOffset);
            this.Controls.Add(this.btnCanel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ucNumberDistance);
            this.Controls.Add(this.ucNumberCount);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmArray";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "阵列";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private WSX.ControlLibrary.Common.UCNumberInputer ucNumberCount;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCanel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButtonOffset;
        private System.Windows.Forms.RadioButton radioButtonGap;
        private WSX.ControlLibrary.Common.UCNumberInputer ucNumberDistance;
        private System.Windows.Forms.CheckBox checkBoxOnlySelected;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
    }
}