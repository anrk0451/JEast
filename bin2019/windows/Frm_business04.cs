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
	public partial class Frm_business04 : MyDialog
	{
		DataView dv_gbt;
		Sa01_ds sa01_ds = null;
		string AC001 = string.Empty;
 

		public Frm_business04()
		{
			InitializeComponent();
		}

		private void Frm_business04_Load(object sender, EventArgs e)
		{
			sa01_ds = this.swapdata["dataset"] as Sa01_ds;
			AC001 = this.swapdata["AC001"].ToString();
 

			dv_gbt = new DataView(sa01_ds.Si01);
			dv_gbt.RowFilter = "item_type='04' ";

			//为下拉列表赋数据源
			glookup_slt.Properties.DataSource = dv_gbt;
			glookup_slt.Properties.DisplayMember = "ITEM_TEXT";
			glookup_slt.Properties.ValueMember = "ITEM_ID";

			dateEdit_so005.EditValue = DateTime.Now;
		}

		private void B_exit_Click(object sender, EventArgs e)
		{
			this.Dispose();
		}

		private void B_ok_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(glookup_slt.EditValue.ToString()))
			{
				glookup_slt.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				glookup_slt.ErrorText = "请先选择一个告别厅!";
				return;
			}
			if (dateEdit_so005.EditValue == null || string.IsNullOrEmpty(dateEdit_so005.EditValue.ToString()))
			{
				dateEdit_so005.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				dateEdit_so005.ErrorText = "请输入告别时间!";
				return;
			}

			string s_si001 = glookup_slt.EditValue.ToString();     //告别厅编号
			DateTime so005 = (DateTime)dateEdit_so005.EditValue;   //告别日期

			int result = FireAction.FireSales_04(AC001,
												  s_si001,
												  so005,
												  Envior.cur_userId
				);
			if (result > 0)
			{
				DialogResult = DialogResult.OK;
				this.Dispose();
			}
		}
	}
}