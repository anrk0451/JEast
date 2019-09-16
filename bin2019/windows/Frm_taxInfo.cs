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

namespace JEast.windows
{
	public partial class Frm_taxInfo : MyDialog
	{
		Tax_ds tax_ds = new Tax_ds();
		public Frm_taxInfo()
		{
			InitializeComponent();
		}

		private void Frm_taxInfo_Load(object sender, EventArgs e)
		{
			foreach (DataRow row in tax_ds.taxinfo.Rows)
			{	//发票类型
				if(row["SP002"].ToString() == "TaxInvoiceKind")
				{
					if (row["SP005"].ToString() == "0")
						radioButton1.Checked = true;
					else
						radioButton2.Checked = true;
				}
				//税务金卡证书口令
				if (row["SP002"].ToString() == "CertPassWord")
				{
					txtedit_cert.EditValue = row["SP005"];
				}
				//销方地址电话
				if (row["SP002"].ToString() == "InfoSellerAddressPhone")
				{
					txtedit_addr.EditValue = row["SP005"];
				}
				//销方银行账号
				if (row["SP002"].ToString() == "InfoSellerBankAccount")
				{
					
					txtedit_bank2.Text = row["SP005"].ToString();
				}
				//税收分类编码版本
				if (row["SP002"].ToString() == "TaxCodeVersion")
				{
					txtedit_ver.EditValue = row["SP005"];
				}

			}
		}

		private void B_ok_Click(object sender, EventArgs e)
		{
			////// 保存过程 
			string s_addr = txtedit_addr.Text;
			string s_bank = txtedit_bank2.Text;
			string s_fplx = radioButton1.Checked ? "0" : "1";
			string s_cert = txtedit_cert.Text;
			string s_ver = txtedit_ver.Text;

			if(MiscAction.SaveTaxInfo(s_addr,s_bank,s_fplx,s_cert,s_ver) > 0)
			{
				MessageBox.Show("保存成功!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
				this.Dispose();
			}
		}
	}
}