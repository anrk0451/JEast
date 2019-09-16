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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using JEast.BusinessObject;

namespace JEast.windows
{
	public partial class Frm_fromFire : MyDialog
	{
		private DataTable dt_ac01 = new DataTable("AC01");
		private OracleDataAdapter ac01Adapter = new OracleDataAdapter("", SqlAssist.conn);
		private Register_brow bo = null;

		private string sql = @"select ac001,ac002,ac003,ac004,ac014,ac015,ac020,ac050,ac051 from ac01 where status = '1' and 
                                pkg_business.fun_FireIsSettled(ac001) = '1' and not exists ( select 1 from rc01 where rc001 = ac001 and rc01.status in ('1','3')) ";

		public Frm_fromFire()
		{
			InitializeComponent();
		}

		private void Frm_fromFire_Load(object sender, EventArgs e)
		{
			bo = this.swapdata["BusinessObject"] as Register_brow;
			gridControl1.DataSource = dt_ac01;
		}

		private void GridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			if (e.Column.FieldName == "AC002")
			{
				if (e.Value.ToString() == "0")
					e.DisplayText = "男";
				else if (e.Value.ToString() == "1")
					e.DisplayText = "女";
				else
					e.DisplayText = "未知";
			}
		}

		private void B_ok_Click(object sender, EventArgs e)
		{
			string s_sql = string.Empty;
			if (txtedit_ac001.EditValue == null && txtedit_ac003.EditValue == null && txtedit_ac050.EditValue == null && comboBoxEdit1.Text == "全部")
			{
				MessageBox.Show("请至少输入一个条件！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			s_sql = sql;
			if (txtedit_ac001.EditValue != null)
			{
				s_sql += @" and ac001='" + txtedit_ac001.Text + "'";
			}
			else
			{
				if (txtedit_ac003.EditValue != null)
				{
					s_sql += @" and ac003  like '%" + txtedit_ac003.Text + "%'";
				}
				if (txtedit_ac050.EditValue != null)
				{
					s_sql += @" and ac050 like '%" + txtedit_ac050.Text + "%'";
				}
				if (comboBoxEdit1.Text == "当日火化")
				{
					s_sql += @" and trunc(ac015) = trunc(sysdate) ";
				}
				else if (comboBoxEdit1.Text == "三日内火化")
				{
					s_sql += @" and trunc(sysdate) - trunc(ac015) <=2 ";
				}
				else if (comboBoxEdit1.Text == "一个月内火化")
				{
					s_sql += @" and trunc(sysdate) - trunc(ac015) <=30 ";
				}
			}

			ac01Adapter.SelectCommand.CommandText = s_sql;
			ac01Adapter.Fill(dt_ac01);
			if (dt_ac01.Rows.Count <= 0)
			{
				MessageBox.Show("没有找到记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
		}

		private void GridView1_MouseDown(object sender, MouseEventArgs e)
		{
			GridHitInfo hInfo = gridView1.CalcHitInfo(new Point(e.X, e.Y));
			if (e.Button == MouseButtons.Left && e.Clicks == 2)
			{
				//判断光标是否在行范围内  
				if (hInfo.InRow)
				{
					Do(gridView1.FocusedRowHandle);
				}
			}
		}

		private void SimpleButton1_Click(object sender, EventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
				Do(rowHandle);
		}

		private void Do(int rowHandle)
		{
			string s_ac001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
			bo.swapdata["AC001"] = s_ac001;
			DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}