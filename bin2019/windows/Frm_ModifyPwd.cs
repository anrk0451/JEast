using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using JEast.BaseObject;
using Oracle.ManagedDataAccess.Client;
using JEast.Misc;
using JEast.Action;
namespace JEast.windows
{
	public partial class Frm_ModifyPwd : MyDialog
	{
		public Frm_ModifyPwd()
		{
			InitializeComponent();
		}

		private void Frm_ModifyPwd_Load(object sender, EventArgs e)
		{
			 
		}

		private void B_ok_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textEdit1.Text))
			{
				textEdit1.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				textEdit1.ErrorText = "请输入原密码!";
				return;
			}
			if (string.IsNullOrEmpty(textEdit2.Text))
			{
				textEdit2.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				textEdit2.ErrorText = "请输入新密码!";
				return;
			}
			if (string.IsNullOrEmpty(textEdit3.Text) || textEdit2.Text != textEdit3.Text)
			{
				textEdit3.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				textEdit3.ErrorText = "密码不一致!";
				return;
			}

			string s_orig_pwd = string.Empty;

			OracleParameter op_uc001 = new OracleParameter("uc001", OracleDbType.Varchar2, 10);
			op_uc001.Value = Envior.cur_userId;

			s_orig_pwd = SqlAssist.ExecuteScalar("select uc004 from uc01 where uc001 = :uc001", op_uc001).ToString();
			if (Tools.EncryptWithMD5(textEdit1.Text) != s_orig_pwd)
			{
				textEdit1.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				textEdit1.ErrorText = "原密码不正确!";
				return;
			}

			string s_new_pwd = Tools.EncryptWithMD5(textEdit2.Text);
			if(MiscAction.Modify_Pwd(Envior.cur_userId,s_new_pwd) == 1)
			{
				MessageBox.Show("修改密码成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.Dispose();
			}
		}

		private void B_exit_Click(object sender, EventArgs e)
		{
			this.Dispose();
		}
	}
}