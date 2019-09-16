namespace JEast.windows
{
	partial class Frm_ModifyPwd
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
			this.b_exit = new DevExpress.XtraEditors.SimpleButton();
			this.b_ok = new DevExpress.XtraEditors.SimpleButton();
			this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// b_exit
			// 
			this.b_exit.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.b_exit.Appearance.ForeColor = System.Drawing.Color.SlateGray;
			this.b_exit.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			this.b_exit.Appearance.Options.UseBackColor = true;
			this.b_exit.Appearance.Options.UseForeColor = true;
			this.b_exit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.b_exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.b_exit.Location = new System.Drawing.Point(348, 159);
			this.b_exit.Name = "b_exit";
			this.b_exit.Size = new System.Drawing.Size(63, 31);
			this.b_exit.TabIndex = 15;
			this.b_exit.Text = "取消";
			this.b_exit.Click += new System.EventHandler(this.B_exit_Click);
			// 
			// b_ok
			// 
			this.b_ok.Appearance.BackColor = System.Drawing.Color.Lime;
			this.b_ok.Appearance.ForeColor = System.Drawing.Color.White;
			this.b_ok.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			this.b_ok.Appearance.Options.UseBackColor = true;
			this.b_ok.Appearance.Options.UseForeColor = true;
			this.b_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.b_ok.Location = new System.Drawing.Point(214, 159);
			this.b_ok.Name = "b_ok";
			this.b_ok.Size = new System.Drawing.Size(128, 31);
			this.b_ok.TabIndex = 16;
			this.b_ok.Text = "确定";
			this.b_ok.Click += new System.EventHandler(this.B_ok_Click);
			// 
			// textEdit3
			// 
			this.textEdit3.Location = new System.Drawing.Point(156, 112);
			this.textEdit3.Name = "textEdit3";
			this.textEdit3.Properties.PasswordChar = '#';
			this.textEdit3.Size = new System.Drawing.Size(258, 24);
			this.textEdit3.TabIndex = 14;
			// 
			// labelControl3
			// 
			this.labelControl3.Location = new System.Drawing.Point(55, 115);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(60, 18);
			this.labelControl3.TabIndex = 13;
			this.labelControl3.Text = "重复密码";
			// 
			// textEdit2
			// 
			this.textEdit2.Location = new System.Drawing.Point(156, 66);
			this.textEdit2.Name = "textEdit2";
			this.textEdit2.Properties.PasswordChar = '#';
			this.textEdit2.Size = new System.Drawing.Size(258, 24);
			this.textEdit2.TabIndex = 12;
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(55, 69);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(45, 18);
			this.labelControl2.TabIndex = 11;
			this.labelControl2.Text = "新密码";
			// 
			// textEdit1
			// 
			this.textEdit1.Location = new System.Drawing.Point(156, 19);
			this.textEdit1.Name = "textEdit1";
			this.textEdit1.Properties.PasswordChar = '#';
			this.textEdit1.Size = new System.Drawing.Size(258, 24);
			this.textEdit1.TabIndex = 10;
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(55, 22);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(45, 18);
			this.labelControl1.TabIndex = 9;
			this.labelControl1.Text = "原密码";
			// 
			// Frm_ModifyPwd
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(469, 209);
			this.Controls.Add(this.b_exit);
			this.Controls.Add(this.b_ok);
			this.Controls.Add(this.textEdit3);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.textEdit2);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.textEdit1);
			this.Controls.Add(this.labelControl1);
			this.Name = "Frm_ModifyPwd";
			this.Text = "修改密码";
			this.Load += new System.EventHandler(this.Frm_ModifyPwd_Load);
			((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton b_exit;
		private DevExpress.XtraEditors.SimpleButton b_ok;
		private DevExpress.XtraEditors.TextEdit textEdit3;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private DevExpress.XtraEditors.TextEdit textEdit2;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.TextEdit textEdit1;
		private DevExpress.XtraEditors.LabelControl labelControl1;
	}
}