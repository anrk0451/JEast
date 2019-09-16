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
using JEast.Action;
using JEast.Misc;

namespace JEast.windows
{
	public partial class Frm_oreg : MyDialog
	{
		private Register_ds register_ds;
 

		private string regionId = string.Empty;
		private string bitDesc = string.Empty;
		private string bitId = string.Empty;

		public Frm_oreg()
		{
			InitializeComponent();
		}

		private void Frm_oreg_Load(object sender, EventArgs e)
		{
			register_ds = this.swapdata["dataset"] as Register_ds;
			lookUp_rc052.Properties.DataSource = register_ds.Relation;
			lookUp_rc052.Properties.ValueMember = "ST003";
			lookUp_rc052.Properties.DisplayMember = "ST003";
		}

		private void Be_position_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			Frm_freebit frm_free = new Frm_freebit();
			frm_free.swapdata["parent"] = this;

			if (frm_free.ShowDialog() == DialogResult.OK)
			{
				regionId = this.swapdata["regionId"].ToString();
				bitDesc = this.swapdata["bitDesc"].ToString();
				bitId = RegisterAction.GetBitId(regionId, bitDesc);

				be_position.Text = RegisterAction.GetBitFullName(regionId, bitDesc);
			 
			}
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
			if (txtEdit_rc140.EditValue == null)
			{
				txtEdit_rc140.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtEdit_rc140.ErrorText = "寄存时间必须输入!";
				return false;
			}

			if (txtEdit_rc150.EditValue == null)
			{
				txtEdit_rc150.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtEdit_rc150.ErrorText = "寄存截至时间必须输入!";
				return false;
			}

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

			//寄存位置
			if (string.IsNullOrEmpty(be_position.Text) || string.IsNullOrEmpty(bitId))
			{
				be_position.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				be_position.ErrorText = "请选择寄存位置!";
				return false;
			}
			return true;
		}


		private void B_exit_Click(object sender, EventArgs e)
		{
			this.Dispose();
		}

		private void B_ok_Click(object sender, EventArgs e)
		{
			if (!SaveCheck()) return;  //数据合法性校验!!!

			string s_rc001 = Tools.GetEntityPK("RC01");         //逝者编号
			string s_fa001 = Tools.GetEntityPK("FA01");         //结算流水号

			string s_rc109 = RegisterAction.GenRegisterNo("1"); //原始登记寄存证号
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
			DateTime d_rc140 = Convert.ToDateTime(txtEdit_rc140.EditValue);   //寄存日期
			DateTime d_rc150 = Convert.ToDateTime(txtEdit_rc150.EditValue);   //截至日期

			List<string> itemId_List = new List<string>() { "" };
			List<decimal> itemPrice_List = new List<decimal>() { 1 };
			List<int> itemNums_List = new List<int>() { 1 };

			int re = RegisterAction.RegisterEnroll( s_rc001,
													s_rc109,
													s_fa001,
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
													s_rc099,
													bitId,
													0,
													d_rc140,
													d_rc150,
													0,
													"2",													 
													Envior.cur_userId
				);
			if (re > 0)
			{
				txtEdit_rc001.EditValue = s_rc001;
				txtEdit_rc109.EditValue = s_rc109;
				if (MessageBox.Show("办理成功!现在打印【骨灰寄存证】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					PrtServAction.PrtRegisterCert(s_rc001,null,this.Handle.ToInt32()); ;
				}
				if (MessageBox.Show("现在打印【寄存标签】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					PrtServAction.PrtRegisterLabel(s_rc001,this.Handle.ToInt32());
 
				}
				DialogResult = DialogResult.OK;
				this.Close();
			}
		}
	}
}