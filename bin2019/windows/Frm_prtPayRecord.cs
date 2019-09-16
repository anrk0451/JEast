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
	public partial class Frm_prtPayRecord : MyDialog
	{
		private string rc001 = string.Empty;
		private DataTable dt_rc04 = new DataTable("RC04");
		private OracleDataAdapter rc04Adapter = new OracleDataAdapter("", SqlAssist.conn);

		public Frm_prtPayRecord()
		{
			InitializeComponent();
		}

		private void Frm_prtPayRecord_Load(object sender, EventArgs e)
		{
			rc001 = this.swapdata["RC001"].ToString();
			//检索数据
			rc04Adapter.SelectCommand.CommandText = "select * from v_rc04 where rc001='" + rc001 + "' order by rc010";
			rc04Adapter.Fill(dt_rc04);

			gridControl1.DataSource = dt_rc04;
		}


		private void GridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			if (e.Column.FieldName == "RC031")
			{
				if (e.Value.ToString() == "0")
					e.DisplayText = "原始登记";
				else if (e.Value.ToString() == "1")
					e.DisplayText = "正常";
			}
			else if (e.Column.FieldName == "RC100")
			{
				e.DisplayText = MiscAction.Mapper_operator(e.Value.ToString());
			}
		}

		private void GridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
		{
			if (e.Action == CollectionChangeAction.Add)
			{
				int row = gridView1.FocusedRowHandle;
				for (int i = 0; i < gridView1.RowCount; i++)
				{
					if (i == row) continue;
					gridView1.UnselectRow(i);
				}
			}
			else if (e.Action == CollectionChangeAction.Refresh && gridView1.SelectedRowsCount > 0)
			{

			}
		}

		/// <summary>
		/// 绘制行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
		{
			e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			if (e.Info.IsRowIndicator)
			{
				if (e.RowHandle >= 0)
				{
					e.Info.DisplayText = (e.RowHandle + 1).ToString();
				}
				else if (e.RowHandle < 0 && e.RowHandle > -1000)
				{
					e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
					e.Info.DisplayText = "G" + e.RowHandle.ToString();
				}
			}
		}

		/// <summary>
		/// 打印缴费记录
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void B_ok_Click(object sender, EventArgs e)
		{
			if (gridView1.SelectedRowsCount == 0)
			{
				MessageBox.Show("请先选择要打印的缴费记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
 
			int row = gridView1.GetSelectedRows()[0];
			string fa001 = string.Empty;

			if (row >= 0)
			{
				MessageBox.Show("现在打印第" + row.ToString() + "条记录!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
				fa001 = gridView1.GetRowCellValue(row, "RC010").ToString();
				PrtServAction.PrtPayrecord_BD(fa001, this.Handle.ToInt32());
			}
		}

		private void Sb_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}