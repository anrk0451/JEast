using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using JEast.BaseObject;
using Oracle.ManagedDataAccess.Client;
using JEast.Misc;

namespace JEast.windows
{
	public partial class Frm_Report_ClassStat : MyDialog
	{
		private DataTable dt_cast = new DataTable();
		private OracleDataAdapter castAdapter =
			new OracleDataAdapter("select * from castinfo", SqlAssist.conn);

		private BaseBusiness bo = null;

		public Frm_Report_ClassStat()
		{
			InitializeComponent();
		}

		private void Frm_Report_ClassStat_Load(object sender, EventArgs e)
		{
			checkedListBoxControl1.DataSource = dt_cast;
			checkedListBoxControl1.ValueMember = "SERVICESALESTYPE";
			checkedListBoxControl1.DisplayMember = "TYPEDESC";
			castAdapter.Fill(dt_cast);

			bo = this.swapdata["BusinessObject"] as BaseBusiness;

			dateEdit2.EditValue = DateTime.Today;
			dateEdit1.EditValue = DateTime.Today.AddMonths(-1);
			 
			checkedListBoxControl1.CheckAll();
		}

		private void B_ok_Click(object sender, EventArgs e)
		{
			bo.swapdata["dbegin"] = dateEdit1.EditValue;
			bo.swapdata["dend"] = dateEdit2.EditValue;
			List<string> classList = new List<string>();


			foreach (DataRowView item in checkedListBoxControl1.CheckedItems)
			{
				classList.Add(item["SERVICESALESTYPE"].ToString());
			}

			if (classList.Count <= 0)
			{
				MessageBox.Show("请至少选择一个类别!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			bo.swapdata["class"] = classList.ToArray();

			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void B_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
