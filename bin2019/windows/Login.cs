using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;
using System.Threading;
using JEast.Misc;
using JEast.Dao;
using JEast.Domain;
using DevExpress.XtraSplashScreen;

namespace JEast.windows
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public Login()
        {             
            InitializeComponent(); 
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
			if (Envior.cur_userId == null)
				Application.Exit();
			else
				this.Dispose();
        }

        /// <summary>
        /// 验证登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string s_userCode, s_pwd;
            s_userCode = textEdit_user.Text;
            s_pwd = textEdit_pwd.Text;

            if (string.IsNullOrEmpty(s_userCode))
            {                
                MessageBox.Show("请输入用户代码!","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                textEdit_user.Focus();
                return;
            }        
            if (string.IsNullOrEmpty(s_pwd))
            {
                MessageBox.Show("请输入密码!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textEdit_pwd.Focus();
                return;
            }
            /////////////////////  检索 密码  ///////////////////////////////
            Uc01_dao uc01_dao = new Uc01_dao();
            Uc01 uc01 = uc01_dao.GetSingle(s => s.uc002 == s_userCode);
            if(uc01 == null)
            {
                textEdit_user.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                textEdit_user.ErrorText = "用户不存在!";
                return;
            }else if(Tools.EncryptWithMD5(s_pwd) != uc01.uc004)
            {
                textEdit_pwd.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                textEdit_pwd.ErrorText = "密码错误!";
                return;
            }
            else
            {
                Envior.cur_userId = uc01.uc001;
                Envior.cur_userName = uc01.uc003;
				Envior.cur_invoicer = uc01.uc009;
				Envior.cur_checker = uc01.uc010;

				ConfigurationManager.OpenExeConfiguration(System.Windows.Forms.Application.ExecutablePath).AppSettings.Settings["lastusername"].Value.ToString();

				//设置打印权限 //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				if (uc01.uc005.ToString() == "1")
					Envior.canInvoice = true;
				else
					Envior.canInvoice = false;

				 
				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                DialogResult = DialogResult.OK;
                this.Dispose();
            }

        }

        private void Login_Load(object sender, EventArgs e)
        {
			this.Focus();
            textEdit_user.Focus();
            #region It's for test!
            //textEdit_user.Text = "root";
            //textEdit_pwd.Text = "root";
            #endregion
        }

         
    }
}