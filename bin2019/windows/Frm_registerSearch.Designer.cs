namespace JEast.windows
{
	partial class Frm_registerSearch
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
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			this.b_exit = new DevExpress.XtraEditors.SimpleButton();
			this.lookup_rc110 = new DevExpress.XtraEditors.LookUpEdit();
			this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
			this.txtEdit_rc050 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
			this.txtedit_rc003 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.txtedit_rc109 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.txtedit_rc001 = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.lookup_rc110.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc050.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_rc003.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_rc109.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_rc001.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// simpleButton1
			// 
			this.simpleButton1.Appearance.BackColor = System.Drawing.Color.Lime;
			this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.White;
			this.simpleButton1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			this.simpleButton1.Appearance.Options.UseBackColor = true;
			this.simpleButton1.Appearance.Options.UseForeColor = true;
			this.simpleButton1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.simpleButton1.Location = new System.Drawing.Point(379, 173);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(157, 29);
			this.simpleButton1.TabIndex = 56;
			this.simpleButton1.Text = "确定";
			this.simpleButton1.Click += new System.EventHandler(this.SimpleButton1_Click);
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
			this.b_exit.Location = new System.Drawing.Point(543, 173);
			this.b_exit.Name = "b_exit";
			this.b_exit.Size = new System.Drawing.Size(72, 29);
			this.b_exit.TabIndex = 55;
			this.b_exit.Text = "退出";
			this.b_exit.Click += new System.EventHandler(this.B_exit_Click);
			// 
			// lookup_rc110
			// 
			this.lookup_rc110.Location = new System.Drawing.Point(123, 119);
			this.lookup_rc110.Name = "lookup_rc110";
			this.lookup_rc110.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lookup_rc110.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RG003", "字体1")});
			this.lookup_rc110.Properties.NullText = "";
			this.lookup_rc110.Properties.ShowHeader = false;
			this.lookup_rc110.Size = new System.Drawing.Size(148, 24);
			this.lookup_rc110.TabIndex = 54;
			// 
			// labelControl5
			// 
			this.labelControl5.Location = new System.Drawing.Point(43, 122);
			this.labelControl5.Name = "labelControl5";
			this.labelControl5.Size = new System.Drawing.Size(45, 18);
			this.labelControl5.TabIndex = 53;
			this.labelControl5.Text = "寄存室";
			// 
			// txtEdit_rc050
			// 
			this.txtEdit_rc050.Location = new System.Drawing.Point(457, 68);
			this.txtEdit_rc050.Name = "txtEdit_rc050";
			this.txtEdit_rc050.Size = new System.Drawing.Size(148, 24);
			this.txtEdit_rc050.TabIndex = 52;
			// 
			// labelControl4
			// 
			this.labelControl4.Location = new System.Drawing.Point(346, 71);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(87, 18);
			this.labelControl4.TabIndex = 51;
			this.labelControl4.Text = "家属(联系人)";
			// 
			// txtedit_rc003
			// 
			this.txtedit_rc003.Location = new System.Drawing.Point(123, 68);
			this.txtedit_rc003.Name = "txtedit_rc003";
			this.txtedit_rc003.Size = new System.Drawing.Size(148, 24);
			this.txtedit_rc003.TabIndex = 50;
			// 
			// labelControl3
			// 
			this.labelControl3.Location = new System.Drawing.Point(43, 71);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(60, 18);
			this.labelControl3.TabIndex = 49;
			this.labelControl3.Text = "逝者姓名";
			// 
			// txtedit_rc109
			// 
			this.txtedit_rc109.Location = new System.Drawing.Point(457, 20);
			this.txtedit_rc109.Name = "txtedit_rc109";
			this.txtedit_rc109.Size = new System.Drawing.Size(148, 24);
			this.txtedit_rc109.TabIndex = 48;
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(373, 23);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(60, 18);
			this.labelControl2.TabIndex = 47;
			this.labelControl2.Text = "寄存证号";
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(43, 23);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(60, 18);
			this.labelControl1.TabIndex = 46;
			this.labelControl1.Text = "逝者编号";
			// 
			// txtedit_rc001
			// 
			this.txtedit_rc001.Location = new System.Drawing.Point(123, 20);
			this.txtedit_rc001.Name = "txtedit_rc001";
			this.txtedit_rc001.Properties.Mask.EditMask = "d";
			this.txtedit_rc001.Size = new System.Drawing.Size(148, 24);
			this.txtedit_rc001.TabIndex = 45;
			this.txtedit_rc001.EditValueChanged += new System.EventHandler(this.Txtedit_rc001_EditValueChanged);
			// 
			// Frm_registerSearch
			// 
			this.AcceptButton = this.simpleButton1;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.b_exit;
			this.ClientSize = new System.Drawing.Size(658, 222);
			this.Controls.Add(this.simpleButton1);
			this.Controls.Add(this.b_exit);
			this.Controls.Add(this.lookup_rc110);
			this.Controls.Add(this.labelControl5);
			this.Controls.Add(this.txtEdit_rc050);
			this.Controls.Add(this.labelControl4);
			this.Controls.Add(this.txtedit_rc003);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.txtedit_rc109);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.txtedit_rc001);
			this.Name = "Frm_registerSearch";
			this.Text = "寄存查询条件......";
			this.Load += new System.EventHandler(this.Frm_registerSearch_Load);
			((System.ComponentModel.ISupportInitialize)(this.lookup_rc110.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc050.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_rc003.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_rc109.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_rc001.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton simpleButton1;
		private DevExpress.XtraEditors.SimpleButton b_exit;
		private DevExpress.XtraEditors.LookUpEdit lookup_rc110;
		private DevExpress.XtraEditors.LabelControl labelControl5;
		private DevExpress.XtraEditors.TextEdit txtEdit_rc050;
		private DevExpress.XtraEditors.LabelControl labelControl4;
		private DevExpress.XtraEditors.TextEdit txtedit_rc003;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private DevExpress.XtraEditors.TextEdit txtedit_rc109;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.TextEdit txtedit_rc001;
	}
}