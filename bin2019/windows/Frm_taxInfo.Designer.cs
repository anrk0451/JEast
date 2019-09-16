namespace JEast.windows
{
	partial class Frm_taxInfo
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
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.txtedit_addr = new DevExpress.XtraEditors.TextEdit();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.txtedit_cert = new DevExpress.XtraEditors.TextEdit();
			this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
			this.txtedit_ver = new DevExpress.XtraEditors.TextEdit();
			this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
			this.b_exit = new DevExpress.XtraEditors.SimpleButton();
			this.b_ok = new DevExpress.XtraEditors.SimpleButton();
			this.txtedit_bank2 = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_addr.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_cert.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_ver.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_bank2.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(37, 32);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(60, 18);
			this.labelControl1.TabIndex = 0;
			this.labelControl1.Text = "发票类型";
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(197, 32);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(89, 22);
			this.radioButton1.TabIndex = 1;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "普通发票";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(354, 32);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(89, 22);
			this.radioButton2.TabIndex = 2;
			this.radioButton2.Text = "专用发票";
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(37, 83);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(90, 18);
			this.labelControl2.TabIndex = 3;
			this.labelControl2.Text = "销方银行账号";
			// 
			// txtedit_addr
			// 
			this.txtedit_addr.Location = new System.Drawing.Point(197, 129);
			this.txtedit_addr.Name = "txtedit_addr";
			this.txtedit_addr.Size = new System.Drawing.Size(493, 24);
			this.txtedit_addr.TabIndex = 6;
			// 
			// labelControl3
			// 
			this.labelControl3.Location = new System.Drawing.Point(37, 132);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(105, 18);
			this.labelControl3.TabIndex = 5;
			this.labelControl3.Text = "销方地址、电话";
			// 
			// txtedit_cert
			// 
			this.txtedit_cert.Location = new System.Drawing.Point(197, 179);
			this.txtedit_cert.Name = "txtedit_cert";
			this.txtedit_cert.Size = new System.Drawing.Size(493, 24);
			this.txtedit_cert.TabIndex = 8;
			// 
			// labelControl4
			// 
			this.labelControl4.Location = new System.Drawing.Point(37, 182);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(105, 18);
			this.labelControl4.TabIndex = 7;
			this.labelControl4.Text = "金税卡证书密码";
			// 
			// txtedit_ver
			// 
			this.txtedit_ver.Location = new System.Drawing.Point(197, 231);
			this.txtedit_ver.Name = "txtedit_ver";
			this.txtedit_ver.Size = new System.Drawing.Size(493, 24);
			this.txtedit_ver.TabIndex = 10;
			// 
			// labelControl5
			// 
			this.labelControl5.Location = new System.Drawing.Point(37, 234);
			this.labelControl5.Name = "labelControl5";
			this.labelControl5.Size = new System.Drawing.Size(120, 18);
			this.labelControl5.TabIndex = 9;
			this.labelControl5.Text = "税收分类编码版本";
			// 
			// b_exit
			// 
			this.b_exit.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.b_exit.Appearance.ForeColor = System.Drawing.Color.White;
			this.b_exit.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			this.b_exit.Appearance.Options.UseBackColor = true;
			this.b_exit.Appearance.Options.UseForeColor = true;
			this.b_exit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.b_exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.b_exit.Location = new System.Drawing.Point(625, 284);
			this.b_exit.Name = "b_exit";
			this.b_exit.Size = new System.Drawing.Size(63, 31);
			this.b_exit.TabIndex = 74;
			this.b_exit.Text = "退出";
			// 
			// b_ok
			// 
			this.b_ok.Appearance.BackColor = System.Drawing.Color.Lime;
			this.b_ok.Appearance.ForeColor = System.Drawing.Color.White;
			this.b_ok.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			this.b_ok.Appearance.Options.UseBackColor = true;
			this.b_ok.Appearance.Options.UseForeColor = true;
			this.b_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.b_ok.Location = new System.Drawing.Point(449, 284);
			this.b_ok.Name = "b_ok";
			this.b_ok.Size = new System.Drawing.Size(170, 31);
			this.b_ok.TabIndex = 73;
			this.b_ok.Text = "确定";
			this.b_ok.Click += new System.EventHandler(this.B_ok_Click);
			// 
			// txtedit_bank2
			// 
			this.txtedit_bank2.Location = new System.Drawing.Point(197, 80);
			this.txtedit_bank2.Name = "txtedit_bank2";
			this.txtedit_bank2.Size = new System.Drawing.Size(493, 24);
			this.txtedit_bank2.TabIndex = 4;
			// 
			// Frm_taxInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.b_exit;
			this.ClientSize = new System.Drawing.Size(725, 339);
			this.Controls.Add(this.b_exit);
			this.Controls.Add(this.b_ok);
			this.Controls.Add(this.txtedit_ver);
			this.Controls.Add(this.labelControl5);
			this.Controls.Add(this.txtedit_cert);
			this.Controls.Add(this.labelControl4);
			this.Controls.Add(this.txtedit_addr);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.txtedit_bank2);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.radioButton2);
			this.Controls.Add(this.radioButton1);
			this.Controls.Add(this.labelControl1);
			this.Name = "Frm_taxInfo";
			this.Text = "税务发票基础信息";
			this.Load += new System.EventHandler(this.Frm_taxInfo_Load);
			((System.ComponentModel.ISupportInitialize)(this.txtedit_addr.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_cert.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_ver.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_bank2.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.LabelControl labelControl1;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.TextEdit txtedit_addr;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private DevExpress.XtraEditors.TextEdit txtedit_cert;
		private DevExpress.XtraEditors.LabelControl labelControl4;
		private DevExpress.XtraEditors.TextEdit txtedit_ver;
		private DevExpress.XtraEditors.LabelControl labelControl5;
		private DevExpress.XtraEditors.SimpleButton b_exit;
		private DevExpress.XtraEditors.SimpleButton b_ok;
		private DevExpress.XtraEditors.TextEdit txtedit_bank2;
	}
}