namespace JEast.windows
{
    partial class Login
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit_pwd = new DevExpress.XtraEditors.TextEdit();
            this.b_ok = new DevExpress.XtraEditors.SimpleButton();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.b_exit = new DevExpress.XtraEditors.SimpleButton();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.textEdit_user = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_pwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_user.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("新宋体", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.LineVisible = true;
            this.labelControl1.Location = new System.Drawing.Point(226, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 38);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "欢迎";
            // 
            // textEdit_pwd
            // 
            this.textEdit_pwd.EditValue = "";
            this.textEdit_pwd.Location = new System.Drawing.Point(102, 130);
            this.textEdit_pwd.Name = "textEdit_pwd";
            this.textEdit_pwd.Properties.Appearance.Font = new System.Drawing.Font("新宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textEdit_pwd.Properties.Appearance.Options.UseFont = true;
            this.textEdit_pwd.Properties.ContextImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("textEdit_pwd.Properties.ContextImageOptions.Image")));
            this.textEdit_pwd.Properties.PasswordChar = '*';
            this.textEdit_pwd.Size = new System.Drawing.Size(334, 24);
            this.textEdit_pwd.TabIndex = 3;
            // 
            // b_ok
            // 
            this.b_ok.Appearance.BackColor = System.Drawing.Color.Lime;
            this.b_ok.Appearance.ForeColor = System.Drawing.Color.White;
            this.b_ok.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.b_ok.Appearance.Options.UseBackColor = true;
            this.b_ok.Appearance.Options.UseForeColor = true;
            this.b_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.b_ok.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_ok.Location = new System.Drawing.Point(166, 210);
            this.b_ok.Name = "b_ok";
            this.b_ok.Size = new System.Drawing.Size(143, 31);
            this.b_ok.TabIndex = 5;
            this.b_ok.Text = "登陆";
            this.b_ok.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(102, 172);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.checkEdit1.Properties.Appearance.Options.UseForeColor = true;
            this.checkEdit1.Properties.Caption = "记住密码";
            this.checkEdit1.Size = new System.Drawing.Size(90, 22);
            this.checkEdit1.TabIndex = 4;
            this.checkEdit1.Visible = false;
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
            this.b_exit.Location = new System.Drawing.Point(315, 210);
            this.b_exit.Name = "b_exit";
            this.b_exit.Size = new System.Drawing.Size(63, 31);
            this.b_exit.TabIndex = 6;
            this.b_exit.Text = "退出";
            this.b_exit.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // textEdit_user
            // 
            this.textEdit_user.Location = new System.Drawing.Point(102, 89);
            this.textEdit_user.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textEdit_user.Name = "textEdit_user";
            this.textEdit_user.Properties.ContextImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("textEdit_user.Properties.ContextImageOptions.Image")));
            this.textEdit_user.Size = new System.Drawing.Size(334, 24);
            this.textEdit_user.TabIndex = 2;
            // 
            // Login
            // 
            this.AcceptButton = this.b_ok;
            this.Appearance.BackColor = System.Drawing.Color.LightGray;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Center;
            this.BackgroundImageStore = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImageStore")));
            this.CancelButton = this.b_exit;
            this.ClientSize = new System.Drawing.Size(537, 264);
            this.Controls.Add(this.textEdit_user);
            this.Controls.Add(this.b_exit);
            this.Controls.Add(this.checkEdit1);
            this.Controls.Add(this.b_ok);
            this.Controls.Add(this.textEdit_pwd);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登陆";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Login_Load);
            this.Shown += new System.EventHandler(this.Login_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_pwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_user.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEdit_pwd;
        private DevExpress.XtraEditors.SimpleButton b_ok;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.SimpleButton b_exit;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.TextEdit textEdit_user;
    }
}