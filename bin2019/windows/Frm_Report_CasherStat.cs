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
using JEast.Misc;
using Oracle.ManagedDataAccess.Client;

namespace JEast.windows
{
	public partial class Frm_Report_CasherStat : MyDialog
	{
		DataTable dt_handler = new DataTable();
		OracleDataAdapter adapter_handler = new OracleDataAdapter("select uc001,uc003 from uc01 where status = '1'", SqlAssist.conn);

		public Frm_Report_CasherStat()
		{
			InitializeComponent();
		}

		private void Frm_Report_CasherStat_Load(object sender, EventArgs e)
		{
			//设置收费员
			adapter_handler.Fill(dt_handler);
			lookup_handler.Properties.DataSource = dt_handler;
			lookup_handler.Properties.DisplayMember = "UC003";
			lookup_handler.Properties.ValueMember = "UC001";


			//设置收费日期
			dateEdit2.EditValue = DateTime.Today;
			dateEdit1.EditValue = DateTime.Today;
		}

		private void b_ok_Click(object sender, EventArgs e)
		{
			this.swapdata["dbegin"] = dateEdit1.EditValue;
			this.swapdata["dend"] = dateEdit2.EditValue;
			this.swapdata["FA100"] = lookup_handler.EditValue;

			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void b_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}