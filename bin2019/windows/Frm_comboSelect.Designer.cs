namespace JEast.windows
{
	partial class Frm_comboSelect
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
			this.ck = new JEast.BaseObject.CheckedListBoxOnlyOne();
			this.sb_cancel = new DevExpress.XtraEditors.SimpleButton();
			this.sb_ok = new DevExpress.XtraEditors.SimpleButton();
			this.SuspendLayout();
			// 
			// ck
			// 
			this.ck.Location = new System.Drawing.Point(9, 8);
			this.ck.Name = "ck";
			this.ck.Size = new System.Drawing.Size(356, 356);
			this.ck.TabIndex = 0;
			// 
			// sb_cancel
			// 
			this.sb_cancel.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.sb_cancel.Appearance.ForeColor = System.Drawing.Color.SlateGray;
			this.sb_cancel.Appearance.Options.UseBackColor = true;
			this.sb_cancel.Appearance.Options.UseForeColor = true;
			this.sb_cancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.sb_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.sb_cancel.Location = new System.Drawing.Point(386, 48);
			this.sb_cancel.LookAndFeel.UseDefaultLookAndFeel = false;
			this.sb_cancel.Name = "sb_cancel";
			this.sb_cancel.Size = new System.Drawing.Size(117, 30);
			this.sb_cancel.TabIndex = 14;
			this.sb_cancel.Text = "关闭";
			// 
			// sb_ok
			// 
			this.sb_ok.Appearance.BackColor = System.Drawing.Color.Lime;
			this.sb_ok.Appearance.ForeColor = System.Drawing.Color.White;
			this.sb_ok.Appearance.Options.UseBackColor = true;
			this.sb_ok.Appearance.Options.UseForeColor = true;
			this.sb_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.sb_ok.Location = new System.Drawing.Point(386, 12);
			this.sb_ok.LookAndFeel.UseDefaultLookAndFeel = false;
			this.sb_ok.Name = "sb_ok";
			this.sb_ok.Size = new System.Drawing.Size(117, 30);
			this.sb_ok.TabIndex = 13;
			this.sb_ok.Text = "确定";
			this.sb_ok.Click += new System.EventHandler(this.Sb_ok_Click);
			// 
			// Frm_comboSelect
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.sb_cancel;
			this.ClientSize = new System.Drawing.Size(524, 373);
			this.Controls.Add(this.sb_cancel);
			this.Controls.Add(this.sb_ok);
			this.Controls.Add(this.ck);
			this.Name = "Frm_comboSelect";
			this.Text = "套餐选择";
			this.Load += new System.EventHandler(this.Frm_comboSelect_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private BaseObject.CheckedListBoxOnlyOne ck;
		private DevExpress.XtraEditors.SimpleButton sb_cancel;
		private DevExpress.XtraEditors.SimpleButton sb_ok;
	}
}