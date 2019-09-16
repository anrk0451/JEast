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
using JEast.Domain;
using Oracle.ManagedDataAccess.Client;
using JEast.Misc;

namespace JEast.windows
{
	public partial class Frm_taxClientInfo : MyDialog
	{
		InvoiceInfo clientInfo = new InvoiceInfo();
		MyDialog frm_parent = null;

		OracleDataReader reader =
				SqlAssist.ExecuteReader("select * from uc01 where uc001 ='" + Envior.cur_userId + "'");

		public Frm_taxClientInfo()
		{
			InitializeComponent();
		}

		private void Frm_taxClientInfo_Load(object sender, EventArgs e)
		{
			frm_parent = this.swapdata["parent"] as MyDialog;

			if(this.swapdata.ContainsKey("title"))
				txtedit_clientName.EditValue = this.swapdata["title"].ToString();

			while (reader.Read())
			{
				txtedit_infocashier.EditValue = reader["UC003"];   //收款人
				txtedit_infochecker.EditValue = reader["UC010"];   //复核人	
			}
			reader.Dispose();
		}

		private void B_ok_Click(object sender, EventArgs e)
		{
			if(txtedit_clientName.EditValue == null)
			{
				txtedit_clientName.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtedit_clientName.ErrorText = "【购方名称】必须输入!";
				return;
			}
			clientInfo.InfoClientName = txtedit_clientName.Text;					//客户名称
			clientInfo.InfoClientTaxCode = txtedit_InfoClientTaxCode.Text;          //税号
			clientInfo.infoclientbankaccount = txtedit_infoclientbankaccount.Text;  //客户银行账户
			clientInfo.infoclientaddressphone = txtedit_infoclientaddressphone.Text;//客户地址及电话
			clientInfo.infocashier = txtedit_infocashier.Text;                      //收款人
			clientInfo.infochecker = txtedit_infochecker.Text;                      //复核人

			if (frm_parent != null)
				frm_parent.swapdata["clientinfo"] = clientInfo;
			else
				Envior.mform.swapdata["clientinfo"] = clientInfo;

			DialogResult = DialogResult.OK;
			this.Close();

		}
	}
}