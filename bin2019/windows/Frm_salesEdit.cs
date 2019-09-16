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
using JEast.Action;
using JEast.Misc;

namespace JEast.windows
{
	public partial class Frm_salesEdit : MyDialog
	{
		DataRow dr = null;
		public Frm_salesEdit()
		{
			InitializeComponent();
		}

		private void Frm_salesEdit_Load(object sender, EventArgs e)
		{
			dr = this.swapdata["DATAROW"] as DataRow;
			txtedit_sa003.EditValue = dr["SA003"];
			txtedit_price.EditValue = dr["PRICE"];
			txtedit_nums.EditValue = dr["NUMS"];

			RightSetup();
		}


		/// <summary>
		/// 根据权限设置可修改项
		/// </summary>
		private void RightSetup()
		{
			///设置权限.......
			///
			string sa002 = dr["SA002"].ToString();
			if (sa002 == "10" || sa002 == "11" || sa002 == "04" || sa002 == "06" || sa002 == "07")
			{
				txtedit_nums.ReadOnly = true;
			}
		}

		private void Txtedit_price_Validating(object sender, CancelEventArgs e)
		{
			if (txtedit_price.EditValue == null || txtedit_price.EditValue is System.DBNull || decimal.Parse(txtedit_price.Text) <= 0)
			{
				txtedit_price.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtedit_price.ErrorText = "请输入单价!";
				e.Cancel = true;
				return;
			}
		}

		private void Txtedit_nums_Validating(object sender, CancelEventArgs e)
		{
			if (txtedit_nums.EditValue == null || txtedit_nums.EditValue is System.DBNull || decimal.Parse(txtedit_nums.Text) <= 0)
			{
				txtedit_nums.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtedit_nums.ErrorText = "请输入数量!";
				e.Cancel = true;
				return;
			}

			string sa002 = dr["SA002"].ToString();
			if (sa002 == "01" || sa002 == "02")
			{
				if (decimal.Parse(txtedit_nums.Text) - Math.Floor(decimal.Parse(txtedit_nums.Text)) != new decimal(0.5))
				{
					txtedit_nums.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
					txtedit_nums.ErrorText = "存放天数只能为整数或者半日!";
					e.Cancel = true;
					return;
				}
			}
		}

		private void Sb_ok_Click(object sender, EventArgs e)
		{
			decimal price, nums;
			if (txtedit_price.EditValue == null || txtedit_price.EditValue is System.DBNull)
			{
				txtedit_price.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtedit_price.ErrorText = "请输入单价!";
				return;
			}
			if (txtedit_nums.EditValue == null || txtedit_nums.EditValue is System.DBNull)
			{
				txtedit_nums.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtedit_nums.ErrorText = "请输入数量!";
				return;
			}
			price = decimal.Parse(txtedit_price.Text);
			nums = decimal.Parse(txtedit_nums.Text);

			if (dr["SA005"].ToString() == "0")  //火化业务
			{
				int result = FireAction.FireSalesEdit(dr["SA001"].ToString(),
													  price,
													  nums,
													  Envior.cur_userId
				);
				if (result > 0)
				{
					DialogResult = DialogResult.OK;
					this.Dispose();
				}
			}
			else if (dr["SA005"].ToString() == "1") //临时性销售
			{
				dr["PRICE"] = price;
				dr["NUMS"] = nums;
				dr["SA007"] = price * nums;
				this.Dispose();
			}
		}
	}
}