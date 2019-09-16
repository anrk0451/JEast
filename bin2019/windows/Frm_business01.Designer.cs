namespace JEast.windows
{
	partial class Frm_business01
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
			this.dateEdit_so005 = new DevExpress.XtraEditors.DateEdit();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.b_exit = new DevExpress.XtraEditors.SimpleButton();
			this.b_ok = new DevExpress.XtraEditors.SimpleButton();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.glookup_slt = new DevExpress.XtraEditors.GridLookUpEdit();
			this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.txtedit_nums = new DevExpress.XtraEditors.SpinEdit();
			((System.ComponentModel.ISupportInitialize)(this.dateEdit_so005.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEdit_so005.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.glookup_slt.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_nums.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// dateEdit_so005
			// 
			this.dateEdit_so005.EditValue = null;
			this.dateEdit_so005.Location = new System.Drawing.Point(119, 70);
			this.dateEdit_so005.Name = "dateEdit_so005";
			this.dateEdit_so005.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dateEdit_so005.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dateEdit_so005.Properties.Mask.EditMask = "f";
			this.dateEdit_so005.Properties.Mask.UseMaskAsDisplayFormat = true;
			this.dateEdit_so005.Size = new System.Drawing.Size(170, 24);
			this.dateEdit_so005.TabIndex = 51;
			// 
			// labelControl3
			// 
			this.labelControl3.Location = new System.Drawing.Point(34, 73);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(60, 18);
			this.labelControl3.TabIndex = 50;
			this.labelControl3.Text = "开始时间";
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
			this.b_exit.Location = new System.Drawing.Point(324, 66);
			this.b_exit.Name = "b_exit";
			this.b_exit.Size = new System.Drawing.Size(104, 29);
			this.b_exit.TabIndex = 48;
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
			this.b_ok.Location = new System.Drawing.Point(324, 27);
			this.b_ok.Name = "b_ok";
			this.b_ok.Size = new System.Drawing.Size(104, 29);
			this.b_ok.TabIndex = 49;
			this.b_ok.Text = "确定";
			this.b_ok.Click += new System.EventHandler(this.B_ok_Click);
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(34, 111);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(30, 18);
			this.labelControl2.TabIndex = 46;
			this.labelControl2.Text = "天数";
			// 
			// glookup_slt
			// 
			this.glookup_slt.EditValue = "";
			this.glookup_slt.Location = new System.Drawing.Point(119, 28);
			this.glookup_slt.Name = "glookup_slt";
			this.glookup_slt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.glookup_slt.Properties.NullText = "";
			this.glookup_slt.Properties.PopupView = this.gridLookUpEdit1View;
			this.glookup_slt.Size = new System.Drawing.Size(170, 24);
			this.glookup_slt.TabIndex = 45;
			// 
			// gridLookUpEdit1View
			// 
			this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
			this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
			this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
			// 
			// gridColumn1
			// 
			this.gridColumn1.Caption = "编号";
			this.gridColumn1.FieldName = "ITEM_ID";
			this.gridColumn1.Name = "gridColumn1";
			// 
			// gridColumn2
			// 
			this.gridColumn2.Caption = "名称";
			this.gridColumn2.FieldName = "ITEM_TEXT";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 0;
			// 
			// gridColumn3
			// 
			this.gridColumn3.Caption = "单价";
			this.gridColumn3.DisplayFormat.FormatString = "n2";
			this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn3.FieldName = "PRICE";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 1;
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(34, 31);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(45, 18);
			this.labelControl1.TabIndex = 44;
			this.labelControl1.Text = "守灵厅";
			// 
			// txtedit_nums
			// 
			this.txtedit_nums.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.txtedit_nums.Location = new System.Drawing.Point(177, 108);
			this.txtedit_nums.Name = "txtedit_nums";
			this.txtedit_nums.Properties.Appearance.Options.UseTextOptions = true;
			this.txtedit_nums.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.txtedit_nums.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.txtedit_nums.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
			this.txtedit_nums.Properties.Mask.EditMask = "n1";
			this.txtedit_nums.Properties.Mask.UseMaskAsDisplayFormat = true;
			this.txtedit_nums.Size = new System.Drawing.Size(112, 24);
			this.txtedit_nums.TabIndex = 47;
			// 
			// Frm_business01
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(457, 158);
			this.Controls.Add(this.dateEdit_so005);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.b_exit);
			this.Controls.Add(this.b_ok);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.glookup_slt);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.txtedit_nums);
			this.Name = "Frm_business01";
			this.Text = "守灵办理";
			this.Load += new System.EventHandler(this.Frm_business01_Load);
			((System.ComponentModel.ISupportInitialize)(this.dateEdit_so005.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEdit_so005.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.glookup_slt.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_nums.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.DateEdit dateEdit_so005;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private DevExpress.XtraEditors.SimpleButton b_exit;
		private DevExpress.XtraEditors.SimpleButton b_ok;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.GridLookUpEdit glookup_slt;
		private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.SpinEdit txtedit_nums;
	}
}