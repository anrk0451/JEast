namespace JEast.windows
{
	partial class Frm_AdjustRegisterDate
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
			this.txtEdit_rc003 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.txtedit_pos = new DevExpress.XtraEditors.TextEdit();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.de_begin = new DevExpress.XtraEditors.DateEdit();
			this.de_end = new DevExpress.XtraEditors.DateEdit();
			this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
			this.b_exit = new DevExpress.XtraEditors.SimpleButton();
			this.b_ok = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc003.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_pos.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.de_begin.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.de_begin.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.de_end.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.de_end.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(25, 30);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(65, 18);
			this.labelControl1.TabIndex = 0;
			this.labelControl1.Text = "逝者姓名:";
			// 
			// txtEdit_rc003
			// 
			this.txtEdit_rc003.Location = new System.Drawing.Point(107, 24);
			this.txtEdit_rc003.Name = "txtEdit_rc003";
			this.txtEdit_rc003.Properties.ReadOnly = true;
			this.txtEdit_rc003.Size = new System.Drawing.Size(125, 24);
			this.txtEdit_rc003.TabIndex = 1;
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(25, 84);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(65, 18);
			this.labelControl2.TabIndex = 2;
			this.labelControl2.Text = "寄存位置:";
			// 
			// txtedit_pos
			// 
			this.txtedit_pos.Location = new System.Drawing.Point(107, 80);
			this.txtedit_pos.Name = "txtedit_pos";
			this.txtedit_pos.Properties.ReadOnly = true;
			this.txtedit_pos.Size = new System.Drawing.Size(391, 24);
			this.txtedit_pos.TabIndex = 3;
			// 
			// labelControl3
			// 
			this.labelControl3.Location = new System.Drawing.Point(25, 138);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(65, 18);
			this.labelControl3.TabIndex = 4;
			this.labelControl3.Text = "寄存日期:";
			// 
			// de_begin
			// 
			this.de_begin.EditValue = null;
			this.de_begin.Location = new System.Drawing.Point(107, 135);
			this.de_begin.Name = "de_begin";
			this.de_begin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.de_begin.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.de_begin.Size = new System.Drawing.Size(125, 24);
			this.de_begin.TabIndex = 5;
			// 
			// de_end
			// 
			this.de_end.EditValue = null;
			this.de_end.Location = new System.Drawing.Point(285, 135);
			this.de_end.Name = "de_end";
			this.de_end.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.de_end.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.de_end.Size = new System.Drawing.Size(125, 24);
			this.de_end.TabIndex = 6;
			// 
			// labelControl4
			// 
			this.labelControl4.Location = new System.Drawing.Point(251, 138);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(15, 18);
			this.labelControl4.TabIndex = 7;
			this.labelControl4.Text = "至";
			// 
			// b_exit
			// 
			this.b_exit.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.b_exit.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
			this.b_exit.Appearance.ForeColor = System.Drawing.Color.SlateGray;
			this.b_exit.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			this.b_exit.Appearance.Options.UseBackColor = true;
			this.b_exit.Appearance.Options.UseFont = true;
			this.b_exit.Appearance.Options.UseForeColor = true;
			this.b_exit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.b_exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.b_exit.Location = new System.Drawing.Point(201, 200);
			this.b_exit.Name = "b_exit";
			this.b_exit.Size = new System.Drawing.Size(63, 31);
			this.b_exit.TabIndex = 74;
			this.b_exit.Text = "退出";
			this.b_exit.Click += new System.EventHandler(this.B_exit_Click);
			// 
			// b_ok
			// 
			this.b_ok.Appearance.BackColor = System.Drawing.Color.Lime;
			this.b_ok.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
			this.b_ok.Appearance.ForeColor = System.Drawing.Color.White;
			this.b_ok.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			this.b_ok.Appearance.Options.UseBackColor = true;
			this.b_ok.Appearance.Options.UseFont = true;
			this.b_ok.Appearance.Options.UseForeColor = true;
			this.b_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.b_ok.Location = new System.Drawing.Point(25, 200);
			this.b_ok.Name = "b_ok";
			this.b_ok.Size = new System.Drawing.Size(170, 31);
			this.b_ok.TabIndex = 73;
			this.b_ok.Text = "确定";
			this.b_ok.Click += new System.EventHandler(this.B_ok_Click);
			// 
			// Frm_AdjustRegisterDate
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(518, 256);
			this.Controls.Add(this.b_exit);
			this.Controls.Add(this.b_ok);
			this.Controls.Add(this.labelControl4);
			this.Controls.Add(this.de_end);
			this.Controls.Add(this.de_begin);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.txtedit_pos);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.txtEdit_rc003);
			this.Controls.Add(this.labelControl1);
			this.Name = "Frm_AdjustRegisterDate";
			this.Text = "调整寄存日期";
			this.Load += new System.EventHandler(this.Frm_AdjustRegisterDate_Load);
			((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc003.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_pos.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.de_begin.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.de_begin.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.de_end.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.de_end.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.TextEdit txtEdit_rc003;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.TextEdit txtedit_pos;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private DevExpress.XtraEditors.DateEdit de_begin;
		private DevExpress.XtraEditors.DateEdit de_end;
		private DevExpress.XtraEditors.LabelControl labelControl4;
		private DevExpress.XtraEditors.SimpleButton b_exit;
		private DevExpress.XtraEditors.SimpleButton b_ok;
	}
}