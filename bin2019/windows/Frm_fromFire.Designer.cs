namespace JEast.windows
{
	partial class Frm_fromFire
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
			this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
			this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			this.b_exit = new DevExpress.XtraEditors.SimpleButton();
			this.b_ok = new DevExpress.XtraEditors.SimpleButton();
			this.txtedit_ac050 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.txtedit_ac003 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.txtedit_ac001 = new DevExpress.XtraEditors.TextEdit();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_ac050.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_ac003.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_ac001.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
			this.groupControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// repositoryItemComboBox1
			// 
			this.repositoryItemComboBox1.AutoHeight = false;
			this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemComboBox1.Items.AddRange(new object[] {
            "男",
            "女",
            "不详"});
			this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
			// 
			// comboBoxEdit1
			// 
			this.comboBoxEdit1.EditValue = "全部";
			this.comboBoxEdit1.Location = new System.Drawing.Point(444, 99);
			this.comboBoxEdit1.Name = "comboBoxEdit1";
			this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
            "当日火化",
            "三日内火化",
            "一个月内火化",
            "全部"});
			this.comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEdit1.Size = new System.Drawing.Size(148, 24);
			this.comboBoxEdit1.TabIndex = 44;
			// 
			// labelControl4
			// 
			this.labelControl4.Location = new System.Drawing.Point(361, 102);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(60, 18);
			this.labelControl4.TabIndex = 43;
			this.labelControl4.Text = "火化日期";
			// 
			// simpleButton1
			// 
			this.simpleButton1.Appearance.BackColor = System.Drawing.Color.Lime;
			this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.White;
			this.simpleButton1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
			this.simpleButton1.Appearance.Options.UseBackColor = true;
			this.simpleButton1.Appearance.Options.UseForeColor = true;
			this.simpleButton1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.simpleButton1.Location = new System.Drawing.Point(708, 50);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(96, 29);
			this.simpleButton1.TabIndex = 42;
			this.simpleButton1.Text = "选择确定";
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
			this.b_exit.Location = new System.Drawing.Point(811, 50);
			this.b_exit.Name = "b_exit";
			this.b_exit.Size = new System.Drawing.Size(72, 29);
			this.b_exit.TabIndex = 40;
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
			this.b_ok.Location = new System.Drawing.Point(604, 50);
			this.b_ok.Name = "b_ok";
			this.b_ok.Size = new System.Drawing.Size(96, 29);
			this.b_ok.TabIndex = 41;
			this.b_ok.Text = "查询";
			this.b_ok.Click += new System.EventHandler(this.B_ok_Click);
			// 
			// txtedit_ac050
			// 
			this.txtedit_ac050.Location = new System.Drawing.Point(110, 99);
			this.txtedit_ac050.Name = "txtedit_ac050";
			this.txtedit_ac050.Size = new System.Drawing.Size(148, 24);
			this.txtedit_ac050.TabIndex = 5;
			// 
			// labelControl3
			// 
			this.labelControl3.Location = new System.Drawing.Point(30, 102);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(30, 18);
			this.labelControl3.TabIndex = 4;
			this.labelControl3.Text = "家属";
			// 
			// txtedit_ac003
			// 
			this.txtedit_ac003.Location = new System.Drawing.Point(444, 51);
			this.txtedit_ac003.Name = "txtedit_ac003";
			this.txtedit_ac003.Size = new System.Drawing.Size(148, 24);
			this.txtedit_ac003.TabIndex = 3;
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(361, 54);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(60, 18);
			this.labelControl2.TabIndex = 2;
			this.labelControl2.Text = "逝者姓名";
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(30, 54);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(60, 18);
			this.labelControl1.TabIndex = 1;
			this.labelControl1.Text = "逝者编号";
			// 
			// txtedit_ac001
			// 
			this.txtedit_ac001.Location = new System.Drawing.Point(110, 51);
			this.txtedit_ac001.Name = "txtedit_ac001";
			this.txtedit_ac001.Properties.Mask.EditMask = "d";
			this.txtedit_ac001.Size = new System.Drawing.Size(148, 24);
			this.txtedit_ac001.TabIndex = 0;
			// 
			// gridControl1
			// 
			this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl1.Location = new System.Drawing.Point(0, 140);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1});
			this.gridControl1.Size = new System.Drawing.Size(890, 409);
			this.gridControl1.TabIndex = 6;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// gridView1
			// 
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8});
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsCustomization.AllowFilter = false;
			this.gridView1.OptionsView.ColumnAutoWidth = false;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			this.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.GridView1_CustomColumnDisplayText);
			this.gridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GridView1_MouseDown);
			// 
			// gridColumn1
			// 
			this.gridColumn1.Caption = "逝者编号";
			this.gridColumn1.FieldName = "AC001";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.OptionsColumn.AllowEdit = false;
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 0;
			this.gridColumn1.Width = 97;
			// 
			// gridColumn2
			// 
			this.gridColumn2.Caption = "逝者姓名";
			this.gridColumn2.FieldName = "AC003";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.AllowEdit = false;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 1;
			// 
			// gridColumn3
			// 
			this.gridColumn3.Caption = "性别";
			this.gridColumn3.ColumnEdit = this.repositoryItemComboBox1;
			this.gridColumn3.FieldName = "AC002";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.OptionsColumn.AllowEdit = false;
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 2;
			this.gridColumn3.Width = 54;
			// 
			// gridColumn4
			// 
			this.gridColumn4.Caption = "年龄";
			this.gridColumn4.FieldName = "AC004";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.OptionsColumn.AllowEdit = false;
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 3;
			this.gridColumn4.Width = 46;
			// 
			// gridColumn5
			// 
			this.gridColumn5.Caption = "身份证号";
			this.gridColumn5.FieldName = "AC014";
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.OptionsColumn.AllowEdit = false;
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 4;
			this.gridColumn5.Width = 153;
			// 
			// gridColumn6
			// 
			this.gridColumn6.Caption = "火化时间";
			this.gridColumn6.DisplayFormat.FormatString = "g";
			this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.gridColumn6.FieldName = "AC015";
			this.gridColumn6.Name = "gridColumn6";
			this.gridColumn6.OptionsColumn.AllowEdit = false;
			this.gridColumn6.Visible = true;
			this.gridColumn6.VisibleIndex = 5;
			this.gridColumn6.Width = 125;
			// 
			// gridColumn7
			// 
			this.gridColumn7.Caption = "联系人";
			this.gridColumn7.FieldName = "AC050";
			this.gridColumn7.Name = "gridColumn7";
			this.gridColumn7.OptionsColumn.AllowEdit = false;
			this.gridColumn7.Visible = true;
			this.gridColumn7.VisibleIndex = 6;
			// 
			// gridColumn8
			// 
			this.gridColumn8.Caption = "联系电话";
			this.gridColumn8.FieldName = "AC051";
			this.gridColumn8.Name = "gridColumn8";
			this.gridColumn8.OptionsColumn.AllowEdit = false;
			this.gridColumn8.Visible = true;
			this.gridColumn8.VisibleIndex = 7;
			this.gridColumn8.Width = 164;
			// 
			// groupControl1
			// 
			this.groupControl1.Controls.Add(this.comboBoxEdit1);
			this.groupControl1.Controls.Add(this.labelControl4);
			this.groupControl1.Controls.Add(this.simpleButton1);
			this.groupControl1.Controls.Add(this.b_exit);
			this.groupControl1.Controls.Add(this.b_ok);
			this.groupControl1.Controls.Add(this.txtedit_ac050);
			this.groupControl1.Controls.Add(this.labelControl3);
			this.groupControl1.Controls.Add(this.txtedit_ac003);
			this.groupControl1.Controls.Add(this.labelControl2);
			this.groupControl1.Controls.Add(this.labelControl1);
			this.groupControl1.Controls.Add(this.txtedit_ac001);
			this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupControl1.Location = new System.Drawing.Point(0, 0);
			this.groupControl1.Name = "groupControl1";
			this.groupControl1.Size = new System.Drawing.Size(890, 140);
			this.groupControl1.TabIndex = 5;
			this.groupControl1.Text = "查询条件";
			// 
			// Frm_fromFire
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(890, 549);
			this.Controls.Add(this.gridControl1);
			this.Controls.Add(this.groupControl1);
			this.Name = "Frm_fromFire";
			this.Text = "选择本馆火化记录";
			this.Load += new System.EventHandler(this.Frm_fromFire_Load);
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_ac050.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_ac003.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_ac001.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
			this.groupControl1.ResumeLayout(false);
			this.groupControl1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
		private DevExpress.XtraEditors.LabelControl labelControl4;
		private DevExpress.XtraEditors.SimpleButton simpleButton1;
		private DevExpress.XtraEditors.SimpleButton b_exit;
		private DevExpress.XtraEditors.SimpleButton b_ok;
		private DevExpress.XtraEditors.TextEdit txtedit_ac050;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private DevExpress.XtraEditors.TextEdit txtedit_ac003;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.TextEdit txtedit_ac001;
		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
		private DevExpress.XtraEditors.GroupControl groupControl1;
	}
}