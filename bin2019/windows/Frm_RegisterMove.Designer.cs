namespace JEast.windows
{
	partial class Frm_RegisterMove
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
			this.be_newposition = new DevExpress.XtraEditors.ButtonEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.txtEdit_rc003 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.txtEdit_rc109 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
			this.txtEdit_rc001 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
			this.be_position = new DevExpress.XtraEditors.ButtonEdit();
			this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.txtedit_rt003 = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.be_newposition.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc003.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc109.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc001.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.be_position.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_rt003.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// b_exit
			// 
			this.b_exit.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.b_exit.Appearance.ForeColor = System.Drawing.Color.Snow;
			this.b_exit.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			this.b_exit.Appearance.Options.UseBackColor = true;
			this.b_exit.Appearance.Options.UseForeColor = true;
			this.b_exit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.b_exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.b_exit.Location = new System.Drawing.Point(466, 61);
			this.b_exit.Name = "b_exit";
			this.b_exit.Size = new System.Drawing.Size(119, 31);
			this.b_exit.TabIndex = 146;
			this.b_exit.Text = "退出";
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
			this.b_ok.Location = new System.Drawing.Point(466, 19);
			this.b_ok.Name = "b_ok";
			this.b_ok.Size = new System.Drawing.Size(119, 31);
			this.b_ok.TabIndex = 147;
			this.b_ok.Text = "确定";
			this.b_ok.Click += new System.EventHandler(this.B_ok_Click);
			// 
			// be_newposition
			// 
			this.be_newposition.Location = new System.Drawing.Point(105, 154);
			this.be_newposition.Name = "be_newposition";
			this.be_newposition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.be_newposition.Properties.ReadOnly = true;
			this.be_newposition.Size = new System.Drawing.Size(338, 24);
			this.be_newposition.TabIndex = 145;
			this.be_newposition.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.Be_newposition_ButtonClick);
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(19, 157);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(75, 18);
			this.labelControl2.TabIndex = 144;
			this.labelControl2.Text = "变更后位置";
			// 
			// txtEdit_rc003
			// 
			this.txtEdit_rc003.Enabled = false;
			this.txtEdit_rc003.Location = new System.Drawing.Point(105, 64);
			this.txtEdit_rc003.Name = "txtEdit_rc003";
			this.txtEdit_rc003.Size = new System.Drawing.Size(122, 24);
			this.txtEdit_rc003.TabIndex = 143;
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(19, 66);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(60, 18);
			this.labelControl1.TabIndex = 142;
			this.labelControl1.Text = "逝者姓名";
			// 
			// txtEdit_rc109
			// 
			this.txtEdit_rc109.Enabled = false;
			this.txtEdit_rc109.Location = new System.Drawing.Point(321, 23);
			this.txtEdit_rc109.Name = "txtEdit_rc109";
			this.txtEdit_rc109.Size = new System.Drawing.Size(122, 24);
			this.txtEdit_rc109.TabIndex = 141;
			// 
			// labelControl13
			// 
			this.labelControl13.Location = new System.Drawing.Point(239, 26);
			this.labelControl13.Name = "labelControl13";
			this.labelControl13.Size = new System.Drawing.Size(60, 18);
			this.labelControl13.TabIndex = 140;
			this.labelControl13.Text = "寄存证号";
			// 
			// txtEdit_rc001
			// 
			this.txtEdit_rc001.Enabled = false;
			this.txtEdit_rc001.Location = new System.Drawing.Point(105, 23);
			this.txtEdit_rc001.Name = "txtEdit_rc001";
			this.txtEdit_rc001.Size = new System.Drawing.Size(122, 24);
			this.txtEdit_rc001.TabIndex = 139;
			// 
			// labelControl8
			// 
			this.labelControl8.Location = new System.Drawing.Point(19, 26);
			this.labelControl8.Name = "labelControl8";
			this.labelControl8.Size = new System.Drawing.Size(60, 18);
			this.labelControl8.TabIndex = 138;
			this.labelControl8.Text = "逝者编号";
			// 
			// be_position
			// 
			this.be_position.Enabled = false;
			this.be_position.Location = new System.Drawing.Point(105, 107);
			this.be_position.Name = "be_position";
			this.be_position.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.be_position.Properties.ReadOnly = true;
			this.be_position.Size = new System.Drawing.Size(338, 24);
			this.be_position.TabIndex = 137;
			// 
			// labelControl16
			// 
			this.labelControl16.Location = new System.Drawing.Point(19, 109);
			this.labelControl16.Name = "labelControl16";
			this.labelControl16.Size = new System.Drawing.Size(75, 18);
			this.labelControl16.TabIndex = 136;
			this.labelControl16.Text = "现寄存位置";
			// 
			// labelControl3
			// 
			this.labelControl3.Location = new System.Drawing.Point(19, 204);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(60, 18);
			this.labelControl3.TabIndex = 148;
			this.labelControl3.Text = "变更原因";
			// 
			// txtedit_rt003
			// 
			this.txtedit_rt003.Location = new System.Drawing.Point(105, 204);
			this.txtedit_rt003.Name = "txtedit_rt003";
			this.txtedit_rt003.Size = new System.Drawing.Size(338, 24);
			this.txtedit_rt003.TabIndex = 149;
			// 
			// Frm_RegisterMove
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.b_exit;
			this.ClientSize = new System.Drawing.Size(604, 251);
			this.Controls.Add(this.txtedit_rt003);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.b_exit);
			this.Controls.Add(this.b_ok);
			this.Controls.Add(this.be_newposition);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.txtEdit_rc003);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.txtEdit_rc109);
			this.Controls.Add(this.labelControl13);
			this.Controls.Add(this.txtEdit_rc001);
			this.Controls.Add(this.labelControl8);
			this.Controls.Add(this.be_position);
			this.Controls.Add(this.labelControl16);
			this.Name = "Frm_RegisterMove";
			this.Text = "寄存位置变更";
			this.Load += new System.EventHandler(this.Frm_RegisterMove_Load);
			((System.ComponentModel.ISupportInitialize)(this.be_newposition.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc003.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc109.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc001.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.be_position.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_rt003.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton b_exit;
		private DevExpress.XtraEditors.SimpleButton b_ok;
		private DevExpress.XtraEditors.ButtonEdit be_newposition;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.TextEdit txtEdit_rc003;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.TextEdit txtEdit_rc109;
		private DevExpress.XtraEditors.LabelControl labelControl13;
		private DevExpress.XtraEditors.TextEdit txtEdit_rc001;
		private DevExpress.XtraEditors.LabelControl labelControl8;
		private DevExpress.XtraEditors.ButtonEdit be_position;
		private DevExpress.XtraEditors.LabelControl labelControl16;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private DevExpress.XtraEditors.TextEdit txtedit_rt003;
	}
}