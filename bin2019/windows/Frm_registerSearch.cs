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
	public partial class Frm_registerSearch : MyDialog
	{
		private DataTable dt_room = new DataTable("room");
		private OracleDataAdapter roomAdapter = new OracleDataAdapter("select * from rg01 where status = '1' and rg002 = '2' order by rg001", SqlAssist.conn);

		public Frm_registerSearch()
		{
			InitializeComponent();
		}

		private void Frm_registerSearch_Load(object sender, EventArgs e)
		{
			lookup_rc110.Properties.DataSource = dt_room;
			lookup_rc110.Properties.ValueMember = "RG001";
			lookup_rc110.Properties.DisplayMember = "RG003";

			roomAdapter.Fill(dt_room);

			txtedit_rc109.Focus();
		}

		private void SimpleButton1_Click(object sender, EventArgs e)
		{
			string sql = string.Empty;
			if (txtedit_rc001.EditValue == null && txtedit_rc109.EditValue == null && txtedit_rc003.EditValue == null && txtEdit_rc050.EditValue == null && lookup_rc110.EditValue == null)
			{
				if (MessageBox.Show("未输入任何查询条件,将查询所有记录，是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					sql = " and 1>0";
				}
				else
					return;
			}
			else
			{
				if (txtedit_rc001.EditValue != null)
				{
					sql = @" and rc001 = '" + txtedit_rc001.Text + "'";
				}
				else
				{
					if (txtedit_rc109.EditValue != null)
					{
						sql = @" and rc109 ='" + txtedit_rc109.Text + "'";
					}
					if (txtedit_rc003.EditValue != null)
					{
						sql += " and rc003 like '" + txtedit_rc003.Text + "%'";
					}
					if (txtEdit_rc050.EditValue != null)
					{
						sql += " and rc050 like '" + txtEdit_rc050.Text + "%'";
					}
					if (lookup_rc110.EditValue != null)
					{
						sql += " and rc110 ='" + lookup_rc110.EditValue + "' ";
					}
				}
			}

			
			
			
			(this.swapdata["parent"] as BaseBusiness).swapdata["sql"] = sql;
			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void B_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 逝者编号 补前导0
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Txtedit_rc001_EditValueChanged(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtedit_rc001.Text)) return;
			txtedit_rc001.Text = txtedit_rc001.Text.PadLeft(10, '0');
		}
	}
}