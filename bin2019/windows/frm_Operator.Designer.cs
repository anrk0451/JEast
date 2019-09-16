namespace JEast.windows
{
    partial class frm_Operator
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
			this.sb_cancel = new DevExpress.XtraEditors.SimpleButton();
			this.sb_ok = new DevExpress.XtraEditors.SimpleButton();
			this.clbx_roles = new DevExpress.XtraEditors.CheckedListBoxControl();
			this.txtedit_pwd2 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
			this.txtedit_pwd = new DevExpress.XtraEditors.TextEdit();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.txtedit_uc003 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.txtedit_uc002 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
			this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
			this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
			this.txtedit_uc009 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
			this.txtedit_uc010 = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.clbx_roles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_pwd2.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_pwd.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_uc003.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_uc002.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_uc009.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_uc010.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// sb_cancel
			// 
			this.sb_cancel.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.sb_cancel.Appearance.ForeColor = System.Drawing.Color.SlateGray;
			this.sb_cancel.Appearance.Options.UseBackColor = true;
			this.sb_cancel.Appearance.Options.UseForeColor = true;
			this.sb_cancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.sb_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.sb_cancel.Location = new System.Drawing.Point(351, 471);
			this.sb_cancel.LookAndFeel.UseDefaultLookAndFeel = false;
			this.sb_cancel.Name = "sb_cancel";
			this.sb_cancel.Size = new System.Drawing.Size(90, 30);
			this.sb_cancel.TabIndex = 21;
			this.sb_cancel.Text = "关闭";
			this.sb_cancel.Click += new System.EventHandler(this.Sb_cancel_Click);
			// 
			// sb_ok
			// 
			this.sb_ok.Appearance.BackColor = System.Drawing.Color.Lime;
			this.sb_ok.Appearance.ForeColor = System.Drawing.Color.White;
			this.sb_ok.Appearance.Options.UseBackColor = true;
			this.sb_ok.Appearance.Options.UseForeColor = true;
			this.sb_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.sb_ok.Location = new System.Drawing.Point(208, 471);
			this.sb_ok.LookAndFeel.UseDefaultLookAndFeel = false;
			this.sb_ok.Name = "sb_ok";
			this.sb_ok.Size = new System.Drawing.Size(137, 30);
			this.sb_ok.TabIndex = 20;
			this.sb_ok.Text = "确定";
			this.sb_ok.Click += new System.EventHandler(this.Sb_ok_Click);
			// 
			// clbx_roles
			// 
			this.clbx_roles.CheckOnClick = true;
			this.clbx_roles.Cursor = System.Windows.Forms.Cursors.Default;
			this.clbx_roles.DisplayMember = "RO003";
			this.clbx_roles.Location = new System.Drawing.Point(86, 358);
			this.clbx_roles.Name = "clbx_roles";
			this.clbx_roles.Size = new System.Drawing.Size(356, 95);
			this.clbx_roles.TabIndex = 19;
			this.clbx_roles.ValueMember = "RO001";
			// 
			// txtedit_pwd2
			// 
			this.txtedit_pwd2.EditValue = "";
			this.txtedit_pwd2.Location = new System.Drawing.Point(189, 167);
			this.txtedit_pwd2.Name = "txtedit_pwd2";
			this.txtedit_pwd2.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.txtedit_pwd2.Properties.NullText = "请再次输入密码";
			this.txtedit_pwd2.Properties.NullValuePrompt = "请再次输入密码";
			this.txtedit_pwd2.Properties.NullValuePromptShowForEmptyValue = true;
			this.txtedit_pwd2.Properties.PasswordChar = '*';
			this.txtedit_pwd2.Size = new System.Drawing.Size(253, 24);
			this.txtedit_pwd2.TabIndex = 18;
			// 
			// labelControl4
			// 
			this.labelControl4.Location = new System.Drawing.Point(86, 171);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(68, 18);
			this.labelControl4.TabIndex = 17;
			this.labelControl4.Text = "确认密码*";
			// 
			// txtedit_pwd
			// 
			this.txtedit_pwd.EditValue = "";
			this.txtedit_pwd.Location = new System.Drawing.Point(189, 120);
			this.txtedit_pwd.Name = "txtedit_pwd";
			this.txtedit_pwd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.txtedit_pwd.Properties.NullText = "请输入密码";
			this.txtedit_pwd.Properties.NullValuePrompt = "请输入密码";
			this.txtedit_pwd.Properties.NullValuePromptShowForEmptyValue = true;
			this.txtedit_pwd.Properties.PasswordChar = '*';
			this.txtedit_pwd.Size = new System.Drawing.Size(253, 24);
			this.txtedit_pwd.TabIndex = 16;
			// 
			// labelControl3
			// 
			this.labelControl3.Location = new System.Drawing.Point(86, 123);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(38, 18);
			this.labelControl3.TabIndex = 15;
			this.labelControl3.Text = "密码*";
			// 
			// txtedit_uc003
			// 
			this.txtedit_uc003.EditValue = "";
			this.txtedit_uc003.Location = new System.Drawing.Point(189, 69);
			this.txtedit_uc003.Name = "txtedit_uc003";
			this.txtedit_uc003.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.txtedit_uc003.Properties.NullText = "请输入用户姓名";
			this.txtedit_uc003.Properties.NullValuePrompt = "请输入用户姓名";
			this.txtedit_uc003.Properties.NullValuePromptShowForEmptyValue = true;
			this.txtedit_uc003.Size = new System.Drawing.Size(253, 24);
			this.txtedit_uc003.TabIndex = 14;
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(86, 72);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(68, 18);
			this.labelControl2.TabIndex = 13;
			this.labelControl2.Text = "用户姓名*";
			// 
			// txtedit_uc002
			// 
			this.txtedit_uc002.EditValue = "";
			this.txtedit_uc002.Location = new System.Drawing.Point(189, 22);
			this.txtedit_uc002.Name = "txtedit_uc002";
			this.txtedit_uc002.Properties.NullText = "请输入用户登录代码";
			this.txtedit_uc002.Properties.NullValuePrompt = "请输入用户登录代码";
			this.txtedit_uc002.Properties.NullValuePromptShowForEmptyValue = true;
			this.txtedit_uc002.Properties.ShowNullValuePromptWhenFocused = true;
			this.txtedit_uc002.Properties.ValidateOnEnterKey = true;
			this.txtedit_uc002.Size = new System.Drawing.Size(253, 24);
			this.txtedit_uc002.TabIndex = 12;
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(86, 25);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(68, 18);
			this.labelControl1.TabIndex = 11;
			this.labelControl1.Text = "用户代码*";
			// 
			// labelControl5
			// 
			this.labelControl5.Location = new System.Drawing.Point(86, 220);
			this.labelControl5.Name = "labelControl5";
			this.labelControl5.Size = new System.Drawing.Size(75, 18);
			this.labelControl5.TabIndex = 22;
			this.labelControl5.Text = "允许开发票";
			// 
			// checkEdit1
			// 
			this.checkEdit1.Location = new System.Drawing.Point(189, 220);
			this.checkEdit1.Name = "checkEdit1";
			this.checkEdit1.Properties.Caption = "";
			this.checkEdit1.Properties.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.CheckBox;
			this.checkEdit1.Properties.ValueChecked = "1";
			this.checkEdit1.Properties.ValueUnchecked = "0";
			this.checkEdit1.Size = new System.Drawing.Size(94, 19);
			this.checkEdit1.TabIndex = 23;
			this.checkEdit1.CheckedChanged += new System.EventHandler(this.CheckEdit1_CheckedChanged);
			// 
			// labelControl6
			// 
			this.labelControl6.Location = new System.Drawing.Point(86, 267);
			this.labelControl6.Name = "labelControl6";
			this.labelControl6.Size = new System.Drawing.Size(75, 18);
			this.labelControl6.TabIndex = 24;
			this.labelControl6.Text = "映射开票人";
			// 
			// txtedit_uc009
			// 
			this.txtedit_uc009.EditValue = "";
			this.txtedit_uc009.Location = new System.Drawing.Point(188, 263);
			this.txtedit_uc009.Name = "txtedit_uc009";
			this.txtedit_uc009.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.txtedit_uc009.Properties.NullText = "请输入用户姓名";
			this.txtedit_uc009.Properties.NullValuePrompt = "请输入用户姓名";
			this.txtedit_uc009.Properties.NullValuePromptShowForEmptyValue = true;
			this.txtedit_uc009.Size = new System.Drawing.Size(253, 24);
			this.txtedit_uc009.TabIndex = 25;
			// 
			// labelControl7
			// 
			this.labelControl7.Location = new System.Drawing.Point(86, 316);
			this.labelControl7.Name = "labelControl7";
			this.labelControl7.Size = new System.Drawing.Size(45, 18);
			this.labelControl7.TabIndex = 26;
			this.labelControl7.Text = "复核人";
			// 
			// txtedit_uc010
			// 
			this.txtedit_uc010.EditValue = "";
			this.txtedit_uc010.Location = new System.Drawing.Point(189, 313);
			this.txtedit_uc010.Name = "txtedit_uc010";
			this.txtedit_uc010.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.txtedit_uc010.Properties.NullText = "请输入用户姓名";
			this.txtedit_uc010.Properties.NullValuePrompt = "请输入用户姓名";
			this.txtedit_uc010.Properties.NullValuePromptShowForEmptyValue = true;
			this.txtedit_uc010.Size = new System.Drawing.Size(253, 24);
			this.txtedit_uc010.TabIndex = 27;
			// 
			// frm_Operator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(538, 515);
			this.Controls.Add(this.txtedit_uc010);
			this.Controls.Add(this.labelControl7);
			this.Controls.Add(this.txtedit_uc009);
			this.Controls.Add(this.labelControl6);
			this.Controls.Add(this.checkEdit1);
			this.Controls.Add(this.labelControl5);
			this.Controls.Add(this.sb_cancel);
			this.Controls.Add(this.sb_ok);
			this.Controls.Add(this.clbx_roles);
			this.Controls.Add(this.txtedit_pwd2);
			this.Controls.Add(this.labelControl4);
			this.Controls.Add(this.txtedit_pwd);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.txtedit_uc003);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.txtedit_uc002);
			this.Controls.Add(this.labelControl1);
			this.Name = "frm_Operator";
			this.Text = "操作员";
			this.Load += new System.EventHandler(this.Frm_Operator_Load);
			((System.ComponentModel.ISupportInitialize)(this.clbx_roles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_pwd2.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_pwd.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_uc003.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_uc002.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_uc009.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtedit_uc010.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sb_cancel;
        private DevExpress.XtraEditors.SimpleButton sb_ok;
        private DevExpress.XtraEditors.CheckedListBoxControl clbx_roles;
        private DevExpress.XtraEditors.TextEdit txtedit_pwd2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtedit_pwd;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtedit_uc003;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtedit_uc002;
        private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.LabelControl labelControl5;
		private DevExpress.XtraEditors.CheckEdit checkEdit1;
		private DevExpress.XtraEditors.LabelControl labelControl6;
		private DevExpress.XtraEditors.TextEdit txtedit_uc009;
		private DevExpress.XtraEditors.LabelControl labelControl7;
		private DevExpress.XtraEditors.TextEdit txtedit_uc010;
	}
}