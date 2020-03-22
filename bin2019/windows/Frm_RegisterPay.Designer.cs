namespace JEast.windows
{
	partial class Frm_RegisterPay
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
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.b_ok = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtEdit_rc109 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.txtEdit_rc001 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtedit_regfee = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtedit_price = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.be_position = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.txtEdit_rc404 = new DevExpress.XtraEditors.TextEdit();
            this.rg_rc202 = new DevExpress.XtraEditors.RadioGroup();
            this.txtEdit_rc303 = new DevExpress.XtraEditors.TextEdit();
            this.txtEdit_rc004 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.rg_rc002 = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtEdit_rc003 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc109.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc001.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtedit_regfee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtedit_price.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.be_position.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc404.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_rc202.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc303.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc004.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_rc002.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc003.Properties)).BeginInit();
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
            this.b_exit.Location = new System.Drawing.Point(578, 432);
            this.b_exit.Name = "b_exit";
            this.b_exit.Size = new System.Drawing.Size(77, 31);
            this.b_exit.TabIndex = 138;
            this.b_exit.Text = "退出";
            this.b_exit.Click += new System.EventHandler(this.B_exit_Click);
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "金额";
            this.gridColumn6.DisplayFormat.FormatString = "N2";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "RC030";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "缴费年限";
            this.gridColumn5.DisplayFormat.FormatString = "N1";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "NUMS";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "单价";
            this.gridColumn4.DisplayFormat.FormatString = "N2";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "PRICE";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "类型";
            this.gridColumn3.FieldName = "RC031";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "截止日期";
            this.gridColumn2.DisplayFormat.FormatString = "d";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn2.FieldName = "RC022";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "开始日期";
            this.gridColumn1.DisplayFormat.FormatString = "d";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn1.FieldName = "RC020";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.GridView1_CustomColumnDisplayText);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(6, 30);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(629, 163);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // b_ok
            // 
            this.b_ok.Appearance.BackColor = System.Drawing.Color.Lime;
            this.b_ok.Appearance.ForeColor = System.Drawing.Color.White;
            this.b_ok.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.b_ok.Appearance.Options.UseBackColor = true;
            this.b_ok.Appearance.Options.UseForeColor = true;
            this.b_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.b_ok.Location = new System.Drawing.Point(439, 432);
            this.b_ok.Name = "b_ok";
            this.b_ok.Size = new System.Drawing.Size(133, 31);
            this.b_ok.TabIndex = 137;
            this.b_ok.Text = "确定";
            this.b_ok.Click += new System.EventHandler(this.B_ok_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Location = new System.Drawing.Point(14, 212);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(640, 210);
            this.groupControl1.TabIndex = 136;
            this.groupControl1.Text = "缴费记录";
            // 
            // txtEdit_rc109
            // 
            this.txtEdit_rc109.Enabled = false;
            this.txtEdit_rc109.Location = new System.Drawing.Point(515, 14);
            this.txtEdit_rc109.Name = "txtEdit_rc109";
            this.txtEdit_rc109.Size = new System.Drawing.Size(122, 24);
            this.txtEdit_rc109.TabIndex = 135;
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(445, 17);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(60, 18);
            this.labelControl13.TabIndex = 134;
            this.labelControl13.Text = "寄存证号";
            // 
            // txtEdit_rc001
            // 
            this.txtEdit_rc001.Enabled = false;
            this.txtEdit_rc001.Location = new System.Drawing.Point(122, 14);
            this.txtEdit_rc001.Name = "txtEdit_rc001";
            this.txtEdit_rc001.Size = new System.Drawing.Size(122, 24);
            this.txtEdit_rc001.TabIndex = 133;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(28, 17);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(60, 18);
            this.labelControl8.TabIndex = 132;
            this.labelControl8.Text = "逝者编号";
            // 
            // txtedit_regfee
            // 
            this.txtedit_regfee.Enabled = false;
            this.txtedit_regfee.Location = new System.Drawing.Point(517, 94);
            this.txtedit_regfee.Name = "txtedit_regfee";
            this.txtedit_regfee.Properties.Appearance.Options.UseTextOptions = true;
            this.txtedit_regfee.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtedit_regfee.Properties.Mask.EditMask = "N2";
            this.txtedit_regfee.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtedit_regfee.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtedit_regfee.Size = new System.Drawing.Size(120, 24);
            this.txtedit_regfee.TabIndex = 131;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(445, 97);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(60, 18);
            this.labelControl7.TabIndex = 130;
            this.labelControl7.Text = "寄存费用";
            // 
            // txtedit_price
            // 
            this.txtedit_price.Enabled = false;
            this.txtedit_price.Location = new System.Drawing.Point(324, 94);
            this.txtedit_price.Name = "txtedit_price";
            this.txtedit_price.Properties.Appearance.Options.UseTextOptions = true;
            this.txtedit_price.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtedit_price.Properties.Mask.EditMask = "N2";
            this.txtedit_price.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtedit_price.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtedit_price.Size = new System.Drawing.Size(81, 24);
            this.txtedit_price.TabIndex = 129;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(271, 97);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(30, 18);
            this.labelControl6.TabIndex = 128;
            this.labelControl6.Text = "单价";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "10"});
            this.comboBox1.Location = new System.Drawing.Point(123, 94);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(89, 26);
            this.comboBox1.TabIndex = 127;
            this.comboBox1.Text = "1";
            this.comboBox1.TextChanged += new System.EventHandler(this.ComboBox1_TextChanged);
            this.comboBox1.Validating += new System.ComponentModel.CancelEventHandler(this.ComboBox1_Validating);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(28, 97);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(68, 18);
            this.labelControl5.TabIndex = 126;
            this.labelControl5.Text = "缴费年限*";
            // 
            // be_position
            // 
            this.be_position.Location = new System.Drawing.Point(123, 56);
            this.be_position.Name = "be_position";
            this.be_position.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.be_position.Properties.ReadOnly = true;
            this.be_position.Size = new System.Drawing.Size(514, 24);
            this.be_position.TabIndex = 125;
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(28, 58);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(68, 18);
            this.labelControl16.TabIndex = 124;
            this.labelControl16.Text = "寄存位置*";
            // 
            // txtEdit_rc404
            // 
            this.txtEdit_rc404.Enabled = false;
            this.txtEdit_rc404.Location = new System.Drawing.Point(518, 178);
            this.txtEdit_rc404.Name = "txtEdit_rc404";
            this.txtEdit_rc404.Properties.Mask.BeepOnError = true;
            this.txtEdit_rc404.Properties.Mask.EditMask = "n0";
            this.txtEdit_rc404.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtEdit_rc404.Size = new System.Drawing.Size(119, 24);
            this.txtEdit_rc404.TabIndex = 123;
            // 
            // rg_rc202
            // 
            this.rg_rc202.EditValue = "1";
            this.rg_rc202.Enabled = false;
            this.rg_rc202.Location = new System.Drawing.Point(290, 178);
            this.rg_rc202.Name = "rg_rc202";
            this.rg_rc202.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rg_rc202.Properties.Appearance.Options.UseBackColor = true;
            this.rg_rc202.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rg_rc202.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "男"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "女"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2", "未知")});
            this.rg_rc202.Size = new System.Drawing.Size(165, 24);
            this.rg_rc202.TabIndex = 122;
            // 
            // txtEdit_rc303
            // 
            this.txtEdit_rc303.Enabled = false;
            this.txtEdit_rc303.Location = new System.Drawing.Point(123, 178);
            this.txtEdit_rc303.Name = "txtEdit_rc303";
            this.txtEdit_rc303.Size = new System.Drawing.Size(89, 24);
            this.txtEdit_rc303.TabIndex = 121;
            // 
            // txtEdit_rc004
            // 
            this.txtEdit_rc004.Enabled = false;
            this.txtEdit_rc004.Location = new System.Drawing.Point(518, 139);
            this.txtEdit_rc004.Name = "txtEdit_rc004";
            this.txtEdit_rc004.Properties.Mask.BeepOnError = true;
            this.txtEdit_rc004.Properties.Mask.EditMask = "n0";
            this.txtEdit_rc004.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtEdit_rc004.Size = new System.Drawing.Size(119, 24);
            this.txtEdit_rc004.TabIndex = 120;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(468, 143);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(38, 18);
            this.labelControl3.TabIndex = 119;
            this.labelControl3.Text = "年龄*";
            // 
            // rg_rc002
            // 
            this.rg_rc002.EditValue = "0";
            this.rg_rc002.Enabled = false;
            this.rg_rc002.Location = new System.Drawing.Point(290, 139);
            this.rg_rc002.Name = "rg_rc002";
            this.rg_rc002.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rg_rc002.Properties.Appearance.Options.UseBackColor = true;
            this.rg_rc002.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rg_rc002.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "男"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "女"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2", "未知")});
            this.rg_rc002.Size = new System.Drawing.Size(165, 24);
            this.rg_rc002.TabIndex = 118;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(246, 143);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(38, 18);
            this.labelControl2.TabIndex = 117;
            this.labelControl2.Text = "性别*";
            // 
            // txtEdit_rc003
            // 
            this.txtEdit_rc003.Enabled = false;
            this.txtEdit_rc003.Location = new System.Drawing.Point(123, 139);
            this.txtEdit_rc003.Name = "txtEdit_rc003";
            this.txtEdit_rc003.Size = new System.Drawing.Size(89, 24);
            this.txtEdit_rc003.TabIndex = 116;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(28, 143);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(68, 18);
            this.labelControl1.TabIndex = 115;
            this.labelControl1.Text = "逝者姓名*";
            // 
            // Frm_RegisterPay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 477);
            this.Controls.Add(this.b_exit);
            this.Controls.Add(this.b_ok);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.txtEdit_rc109);
            this.Controls.Add(this.labelControl13);
            this.Controls.Add(this.txtEdit_rc001);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.txtedit_regfee);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.txtedit_price);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.be_position);
            this.Controls.Add(this.labelControl16);
            this.Controls.Add(this.txtEdit_rc404);
            this.Controls.Add(this.rg_rc202);
            this.Controls.Add(this.txtEdit_rc303);
            this.Controls.Add(this.txtEdit_rc004);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.rg_rc002);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtEdit_rc003);
            this.Controls.Add(this.labelControl1);
            this.Name = "Frm_RegisterPay";
            this.Text = "寄存缴费";
            this.Load += new System.EventHandler(this.Frm_RegisterPay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc109.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc001.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtedit_regfee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtedit_price.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.be_position.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc404.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_rc202.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc303.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc004.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_rc002.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit_rc003.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton b_exit;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraEditors.SimpleButton b_ok;
		private DevExpress.XtraEditors.GroupControl groupControl1;
		private DevExpress.XtraEditors.TextEdit txtEdit_rc109;
		private DevExpress.XtraEditors.LabelControl labelControl13;
		private DevExpress.XtraEditors.TextEdit txtEdit_rc001;
		private DevExpress.XtraEditors.LabelControl labelControl8;
		private DevExpress.XtraEditors.TextEdit txtedit_regfee;
		private DevExpress.XtraEditors.LabelControl labelControl7;
		private DevExpress.XtraEditors.TextEdit txtedit_price;
		private DevExpress.XtraEditors.LabelControl labelControl6;
		private System.Windows.Forms.ComboBox comboBox1;
		private DevExpress.XtraEditors.LabelControl labelControl5;
		private DevExpress.XtraEditors.ButtonEdit be_position;
		private DevExpress.XtraEditors.LabelControl labelControl16;
		private DevExpress.XtraEditors.TextEdit txtEdit_rc404;
		private DevExpress.XtraEditors.RadioGroup rg_rc202;
		private DevExpress.XtraEditors.TextEdit txtEdit_rc303;
		private DevExpress.XtraEditors.TextEdit txtEdit_rc004;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private DevExpress.XtraEditors.RadioGroup rg_rc002;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.TextEdit txtEdit_rc003;
		private DevExpress.XtraEditors.LabelControl labelControl1;
	}
}