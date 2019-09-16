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
	public partial class Frm_business07 : MyDialog
	{
		DataView dv_lc;
		Sa01_ds sa01_ds = null;
		string AC001 = string.Empty;
 

		public Frm_business07()
		{
			InitializeComponent();
		}

		private void Frm_business07_Load(object sender, EventArgs e)
		{
			sa01_ds = this.swapdata["dataset"] as Sa01_ds;
			AC001 = this.swapdata["AC001"].ToString();

			dv_lc = new DataView(sa01_ds.Si01);
			dv_lc.RowFilter = "item_type='07' ";

			//为下拉列表赋数据源
			glookup_lc.Properties.DataSource = dv_lc;
			glookup_lc.Properties.DisplayMember = "ITEM_TEXT";
			glookup_lc.Properties.ValueMember = "ITEM_ID";
		}

		private void B_ok_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(glookup_lc.EditValue.ToString()))
			{
				glookup_lc.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				glookup_lc.ErrorText = "请先选择灵车!";
				return;
			}

			string s_si001 = glookup_lc.EditValue.ToString();     //灵车编号

			int result = FireAction.FireSales_07(AC001,
												  s_si001,
												  Envior.cur_userId
				);
			if (result > 0)
			{
				DialogResult = DialogResult.OK;
				this.Dispose();
			}
		}

		private void B_exit_Click(object sender, EventArgs e)
		{
			this.Dispose();
		}
	}
}