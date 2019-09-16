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
using JEast.DataSet;
using Oracle.ManagedDataAccess.Client;
using JEast.Misc;
using JEast.Action;

namespace JEast.windows
{
	public partial class Frm_regedit : MyDialog
	{
		private string ac001 = string.Empty;
		private Register_ds register_ds = null;

		public Frm_regedit()
		{
			InitializeComponent();
		}

		private void Frm_regedit_Load(object sender, EventArgs e)
		{
			ac001 = this.swapdata["AC001"].ToString();
			register_ds = this.swapdata["dataset"] as Register_ds;

			OracleDataReader reader = SqlAssist.ExecuteReader("select * from rc01 where rc001='" + ac001 + "'");
			while (reader.Read())
			{
				txtEdit_rc001.EditValue = reader["RC001"];
				txtEdit_rc109.EditValue = reader["RC109"];
				txtEdit_rc003.EditValue = reader["RC003"];
				txtEdit_rc303.EditValue = reader["RC303"];
				be_position.EditValue = RegisterAction.GetRegPathName(ac001);

				rg_rc002.EditValue = reader["RC002"];
				rg_rc202.EditValue = reader["RC202"];
				txtEdit_rc004.EditValue = reader["RC004"];
				txtEdit_rc404.EditValue = reader["RC404"];
				txtedit_rc014.EditValue = reader["RC014"];
				txtEdit_rc050.EditValue = reader["RC050"];
				txtEdit_rc051.EditValue = reader["RC051"];
				lookUp_rc052.EditValue = reader["RC052"];
				txtEdit_ac055.EditValue = reader["RC055"];
				mem_rc099.EditValue = reader["RC099"];
			}
 
			lookUp_rc052.Properties.DataSource = register_ds.Relation;
			lookUp_rc052.Properties.ValueMember = "ST003";
			lookUp_rc052.Properties.DisplayMember = "ST003";
		}

		private void Rg_rc002_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (rg_rc002.EditValue.ToString() == "0")
			{
				rg_rc202.EditValue = "1";
			}
			else if (rg_rc002.EditValue.ToString() == "1")
			{
				rg_rc202.EditValue = "0";
			}
		}

		private void Txtedit_rc014_Validating(object sender, CancelEventArgs e)
		{
			string s_idcard = txtedit_rc014.Text.Trim();
			if (string.IsNullOrWhiteSpace(s_idcard)) return;

			if (s_idcard.Length != 15 && s_idcard.Length != 18)
			{
				txtedit_rc014.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtedit_rc014.ErrorText = "身份证号位数错误!";
				e.Cancel = true;
			}
			else if (s_idcard.Length == 15)
			{
				if (!Tools.CheckIDCard15(s_idcard))
				{
					txtedit_rc014.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
					txtedit_rc014.ErrorText = "身份证号错误!";
					e.Cancel = true;
				}
			}
			else if (s_idcard.Length == 18)
			{
				if (!Tools.CheckIDCard18(s_idcard))
				{
					txtedit_rc014.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
					txtedit_rc014.ErrorText = "身份证号错误!";
					e.Cancel = true;
				}
			}
		}

		/// <summary>
		/// 保存前检查
		/// </summary>
		/// <returns></returns>
		private bool SaveCheck()
		{
			//逝者姓名
			if (string.IsNullOrWhiteSpace(txtEdit_rc003.Text.Trim()))
			{
				txtEdit_rc003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtEdit_rc003.ErrorText = "逝者姓名必须输入!";
				txtEdit_rc003.Focus();
				return false;
			}
			//年龄
			if (string.IsNullOrWhiteSpace(txtEdit_rc004.Text.Trim()))
			{
				txtEdit_rc004.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtEdit_rc004.ErrorText = "年龄必须输入!";
				txtEdit_rc004.Focus();
				return false;
			}

			if (!string.IsNullOrWhiteSpace(txtEdit_rc303.Text.Trim()) && string.IsNullOrWhiteSpace(txtEdit_rc404.Text.Trim()))
			{
				txtEdit_rc404.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtEdit_rc404.ErrorText = "年龄必须输入!";
				txtEdit_rc404.Focus();
				return false;
			}


			//联系人
			if (string.IsNullOrWhiteSpace(txtEdit_rc050.Text))
			{
				txtEdit_rc050.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtEdit_rc050.ErrorText = "联系人必须输入!";
				txtEdit_rc050.Focus();
				return false;
			}
			//与逝者关系
			if (lookUp_rc052.EditValue == null)
			{
				lookUp_rc052.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				lookUp_rc052.ErrorText = "与逝者关系必须输入!";
				lookUp_rc052.Focus();
				return false;
			}
			//联系电话
			if (string.IsNullOrWhiteSpace(txtEdit_rc051.Text))
			{
				txtEdit_rc051.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtEdit_rc051.ErrorText = "联系人必须输入!";
				txtEdit_rc051.Focus();
				return false;
			}
			return true;
		}

		private void B_ok_Click(object sender, EventArgs e)
		{
			if (!SaveCheck()) return;  //数据合法性校验!!!

			string s_rc002 = rg_rc002.EditValue.ToString();     //性别
			string s_rc202 = rg_rc202.EditValue.ToString();     //性别2
			string s_rc003 = txtEdit_rc003.Text;                //逝者姓名
			string s_rc303 = txtEdit_rc303.Text;                //逝者姓名2
			int rc004 = int.Parse(txtEdit_rc004.Text);          //年龄
			int rc404;
			if (!string.IsNullOrEmpty(txtEdit_rc404.Text))
				rc404 = int.Parse(txtEdit_rc404.Text);
			else
				rc404 = 0;

			string s_rc014 = txtedit_rc014.Text;                  //身份证号
			string s_rc050 = txtEdit_rc050.Text;                  //联系人
			string s_rc051 = txtEdit_rc051.Text;                  //联系电话
			string s_rc052 = lookUp_rc052.EditValue.ToString();   //与逝者关系
			string s_rc055 = txtEdit_ac055.Text;                  //联系地址
			string s_rc099 = mem_rc099.Text;                      //备注

			int re = RegisterAction.RegisterEdit(ac001,
												  s_rc002,
												  s_rc202,
												  s_rc003,
												  s_rc303,
												  rc004,
												  rc404,
												  s_rc014,
												  s_rc050,
												  s_rc051,
												  s_rc052,
												  s_rc055,
												  s_rc099
					);
			if (re > 0)
			{
				MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void B_exit_Click(object sender, EventArgs e)
		{
			this.Dispose();
		}
	}
}