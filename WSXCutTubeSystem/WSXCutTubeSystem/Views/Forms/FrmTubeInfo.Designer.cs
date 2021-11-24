namespace WSXCutTubeSystem.Views.Forms
{
    partial class FrmTubeInfo
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCanel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ucNumInput11 = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.lbl10 = new System.Windows.Forms.Label();
            this.lbl00 = new System.Windows.Forms.Label();
            this.lbl01 = new System.Windows.Forms.Label();
            this.lbl30 = new System.Windows.Forms.Label();
            this.ucNumInput31 = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.lbl20 = new System.Windows.Forms.Label();
            this.ucNumInput21 = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(366, 360);
            this.panel1.TabIndex = 0;
            // 
            // btnCanel
            // 
            this.btnCanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCanel.Location = new System.Drawing.Point(456, 321);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new System.Drawing.Size(75, 23);
            this.btnCanel.TabIndex = 4;
            this.btnCanel.Text = "取消";
            this.btnCanel.UseVisualStyleBackColor = true;
            this.btnCanel.Click += new System.EventHandler(this.btnCanel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.Location = new System.Drawing.Point(375, 321);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.26191F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.73809F));
            this.tableLayoutPanel1.Controls.Add(this.ucNumInput11, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl10, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl00, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl01, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl30, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.ucNumInput31, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbl20, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ucNumInput21, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(366, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(168, 87);
            this.tableLayoutPanel1.TabIndex = 6;
            this.tableLayoutPanel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FrmTubeInfo_MouseClick);
            // 
            // ucNumInput11
            // 
            this.ucNumInput11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ucNumInput11.Format = null;
            this.ucNumInput11.IsInterger = false;
            this.ucNumInput11.Location = new System.Drawing.Point(71, 22);
            this.ucNumInput11.Margin = new System.Windows.Forms.Padding(0);
            this.ucNumInput11.Max = 100D;
            this.ucNumInput11.Min = 0D;
            this.ucNumInput11.Name = "ucNumInput11";
            this.ucNumInput11.Number = 0D;
            this.ucNumInput11.ReadOnly = true;
            this.ucNumInput11.Size = new System.Drawing.Size(96, 20);
            this.ucNumInput11.Suffix = "mm";
            this.ucNumInput11.TabIndex = 6;
            this.ucNumInput11.TextSize = 9F;
            this.ucNumInput11.Visible = false;
            // 
            // lbl10
            // 
            this.lbl10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl10.AutoSize = true;
            this.lbl10.Location = new System.Drawing.Point(4, 26);
            this.lbl10.Name = "lbl10";
            this.lbl10.Size = new System.Drawing.Size(0, 12);
            this.lbl10.TabIndex = 4;
            // 
            // lbl00
            // 
            this.lbl00.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl00.AutoSize = true;
            this.lbl00.Location = new System.Drawing.Point(4, 5);
            this.lbl00.Name = "lbl00";
            this.lbl00.Size = new System.Drawing.Size(53, 12);
            this.lbl00.TabIndex = 1;
            this.lbl00.Text = "参数名称";
            // 
            // lbl01
            // 
            this.lbl01.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl01.AutoSize = true;
            this.lbl01.Location = new System.Drawing.Point(74, 5);
            this.lbl01.Name = "lbl01";
            this.lbl01.Size = new System.Drawing.Size(17, 12);
            this.lbl01.TabIndex = 0;
            this.lbl01.Text = "值";
            // 
            // lbl30
            // 
            this.lbl30.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl30.AutoSize = true;
            this.lbl30.Location = new System.Drawing.Point(4, 69);
            this.lbl30.Name = "lbl30";
            this.lbl30.Size = new System.Drawing.Size(0, 12);
            this.lbl30.TabIndex = 5;
            // 
            // ucNumInput31
            // 
            this.ucNumInput31.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ucNumInput31.Format = null;
            this.ucNumInput31.IsInterger = false;
            this.ucNumInput31.Location = new System.Drawing.Point(71, 65);
            this.ucNumInput31.Margin = new System.Windows.Forms.Padding(0);
            this.ucNumInput31.Max = 100D;
            this.ucNumInput31.Min = 0D;
            this.ucNumInput31.Name = "ucNumInput31";
            this.ucNumInput31.Number = 0D;
            this.ucNumInput31.ReadOnly = false;
            this.ucNumInput31.Size = new System.Drawing.Size(96, 20);
            this.ucNumInput31.Suffix = "mm";
            this.ucNumInput31.TabIndex = 3;
            this.ucNumInput31.TextSize = 9F;
            this.ucNumInput31.Visible = false;
            // 
            // lbl20
            // 
            this.lbl20.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl20.AutoSize = true;
            this.lbl20.Location = new System.Drawing.Point(4, 47);
            this.lbl20.Name = "lbl20";
            this.lbl20.Size = new System.Drawing.Size(0, 12);
            this.lbl20.TabIndex = 4;
            // 
            // ucNumInput21
            // 
            this.ucNumInput21.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ucNumInput21.Format = null;
            this.ucNumInput21.IsInterger = false;
            this.ucNumInput21.Location = new System.Drawing.Point(71, 43);
            this.ucNumInput21.Margin = new System.Windows.Forms.Padding(0);
            this.ucNumInput21.Max = 100D;
            this.ucNumInput21.Min = 0D;
            this.ucNumInput21.Name = "ucNumInput21";
            this.ucNumInput21.Number = 0D;
            this.ucNumInput21.ReadOnly = true;
            this.ucNumInput21.Size = new System.Drawing.Size(96, 20);
            this.ucNumInput21.Suffix = "mm";
            this.ucNumInput21.TabIndex = 6;
            this.ucNumInput21.TextSize = 9F;
            this.ucNumInput21.Visible = false;
            // 
            // FrmTubeInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 360);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnCanel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTubeInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "管材信息";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FrmTubeInfo_MouseClick);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCanel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl00;
        private System.Windows.Forms.Label lbl01;
        private WSX.ControlLibrary.Common.UCNumberInputer ucNumInput31;
        private System.Windows.Forms.Label lbl30;
        private System.Windows.Forms.Label lbl10;
        private WSX.ControlLibrary.Common.UCNumberInputer ucNumInput11;
        private System.Windows.Forms.Label lbl20;
        private WSX.ControlLibrary.Common.UCNumberInputer ucNumInput21;
    }
}