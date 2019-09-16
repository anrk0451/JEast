namespace JEast.BaseObject
{
	partial class CheckedListBoxOnlyOne
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.checkedListBoxControl1 = new DevExpress.XtraEditors.CheckedListBoxControl();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// checkedListBoxControl1
			// 
			this.checkedListBoxControl1.CheckMode = DevExpress.XtraEditors.CheckMode.Single;
			this.checkedListBoxControl1.CheckOnClick = true;
			this.checkedListBoxControl1.Cursor = System.Windows.Forms.Cursors.Default;
			this.checkedListBoxControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkedListBoxControl1.Location = new System.Drawing.Point(0, 0);
			this.checkedListBoxControl1.Name = "checkedListBoxControl1";
			this.checkedListBoxControl1.Size = new System.Drawing.Size(422, 415);
			this.checkedListBoxControl1.TabIndex = 1;
			this.checkedListBoxControl1.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.CheckedListBoxControl1_ItemCheck);
			// 
			// CheckedListBoxOnlyOne
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.checkedListBoxControl1);
			this.Name = "CheckedListBoxOnlyOne";
			this.Size = new System.Drawing.Size(422, 415);
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControl1;
	}
}
