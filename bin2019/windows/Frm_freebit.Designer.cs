namespace JEast.windows
{
	partial class Frm_freebit
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
			DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
			this.lookup_room = new DevExpress.XtraEditors.LookUpEdit();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.imageListBoxControl1 = new DevExpress.XtraEditors.ImageListBoxControl();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
			this.panelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lookup_room.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.imageListBoxControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControl1
			// 
			this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			gridLevelNode1.RelationName = "Level1";
			this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
			this.gridControl1.Location = new System.Drawing.Point(154, 44);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.Size = new System.Drawing.Size(997, 678);
			this.gridControl1.TabIndex = 7;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// gridView1
			// 
			this.gridView1.ActiveFilterEnabled = false;
			this.gridView1.Appearance.Row.Options.UseTextOptions = true;
			this.gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.IndicatorWidth = 45;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridView1.OptionsBehavior.Editable = false;
			this.gridView1.OptionsCustomization.AllowColumnMoving = false;
			this.gridView1.OptionsCustomization.AllowColumnResizing = false;
			this.gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
			this.gridView1.OptionsSelection.UseIndicatorForSelection = false;
			this.gridView1.OptionsView.ColumnAutoWidth = false;
			this.gridView1.OptionsView.ShowColumnHeaders = false;
			this.gridView1.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			this.gridView1.RowHeight = 60;
			this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.GridView1_CustomDrawRowIndicator);
			this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.GridView1_RowCellStyle);
			this.gridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GridView1_MouseDown);
			// 
			// panelControl1
			// 
			this.panelControl1.Controls.Add(this.lookup_room);
			this.panelControl1.Controls.Add(this.labelControl1);
			this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelControl1.Location = new System.Drawing.Point(154, 0);
			this.panelControl1.Name = "panelControl1";
			this.panelControl1.Size = new System.Drawing.Size(997, 44);
			this.panelControl1.TabIndex = 6;
			// 
			// lookup_room
			// 
			this.lookup_room.Location = new System.Drawing.Point(95, 11);
			this.lookup_room.Name = "lookup_room";
			this.lookup_room.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lookup_room.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RG003", "")});
			this.lookup_room.Properties.DisplayMember = "RG003";
			this.lookup_room.Properties.NullText = "";
			this.lookup_room.Properties.ShowHeader = false;
			this.lookup_room.Properties.ValueMember = "RG001";
			this.lookup_room.Size = new System.Drawing.Size(376, 24);
			this.lookup_room.TabIndex = 2;
			this.lookup_room.EditValueChanged += new System.EventHandler(this.Lookup_room_EditValueChanged);
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(16, 14);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(45, 18);
			this.labelControl1.TabIndex = 1;
			this.labelControl1.Text = "寄存室";
			// 
			// imageListBoxControl1
			// 
			this.imageListBoxControl1.Cursor = System.Windows.Forms.Cursors.Default;
			this.imageListBoxControl1.Dock = System.Windows.Forms.DockStyle.Left;
			this.imageListBoxControl1.Location = new System.Drawing.Point(0, 0);
			this.imageListBoxControl1.Name = "imageListBoxControl1";
			this.imageListBoxControl1.Size = new System.Drawing.Size(154, 722);
			this.imageListBoxControl1.TabIndex = 5;
			this.imageListBoxControl1.SelectedIndexChanged += new System.EventHandler(this.ImageListBoxControl1_SelectedIndexChanged);
			// 
			// Frm_freebit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1151, 722);
			this.Controls.Add(this.gridControl1);
			this.Controls.Add(this.panelControl1);
			this.Controls.Add(this.imageListBoxControl1);
			this.Name = "Frm_freebit";
			this.Text = "选择号位";
			this.Load += new System.EventHandler(this.Frm_freebit_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
			this.panelControl1.ResumeLayout(false);
			this.panelControl1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.lookup_room.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.imageListBoxControl1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraEditors.PanelControl panelControl1;
		private DevExpress.XtraEditors.LookUpEdit lookup_room;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.ImageListBoxControl imageListBoxControl1;
	}
}