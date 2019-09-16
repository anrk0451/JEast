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
	public partial class Frm_comboSelect : MyDialog
	{
		private string AC001;
		private DataTable cb01 = new DataTable();
		private OracleDataAdapter cb01Adapter = 
			new OracleDataAdapter("select * from cb01 where cb002 = '1' and status = '1'", SqlAssist.conn);
		public Frm_comboSelect()
		{
			InitializeComponent();
		}

		private void Frm_comboSelect_Load(object sender, EventArgs e)
		{
			AC001 = this.swapdata["AC001"].ToString();
  
			cb01Adapter.Fill(cb01);
			ck.checklist.DataSource = cb01;
			ck.checklist.ValueMember = "CB001";
			ck.checklist.DisplayMember = "CB003";
		}

		private void Sb_ok_Click(object sender, EventArgs e)
		{
			if (ck.checklist.CheckedItemsCount == 0)
			{
				MessageBox.Show("请先选择项目!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			string cb001 = ck.checklist.SelectedValue.ToString();
			int result = FireAction.FireApplyUserCombo(AC001,
													   cb001,
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