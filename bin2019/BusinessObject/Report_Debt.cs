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
using DevExpress.XtraPrinting;
using JEast.Misc;

namespace JEast.BusinessObject
{
	public partial class Report_Debt : BaseBusiness
	{
		DataTable dt_debt = new DataTable("DEBT");
		OracleDataAdapter debtAdapter = new OracleDataAdapter("", SqlAssist.conn);

		public Report_Debt()
		{
			InitializeComponent();
		}

		private void Report_Debt_Load(object sender, EventArgs e)
		{
			gridControl1.DataSource = dt_debt;

			//执行查询
			this.HandleSearch(comboBoxEdit1.EditValue.ToString());
		}

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

		private void HandleSearch(string commandText)
		{
			string sql = string.Empty;
			if (commandText == "欠费一年以内")
			{
				sql = @"select * from v_debtReport where diffMonths <= 12";
			}
			else if (commandText == "欠费一年以上")
			{
				sql = @"select * from v_debtReport where diffMonths > 12";
			}
			else if (commandText == "欠费三年以上")
			{
				sql = @"select * from v_debtReport where diffMonths > 36";
			}
			else if (commandText == "全部")
			{
				sql = @"select * from v_debtReport";
			}

			this.Cursor = Cursors.WaitCursor;
			debtAdapter.SelectCommand.CommandText = sql;
			gridView1.BeginUpdate();
			dt_debt.Rows.Clear();
			debtAdapter.Fill(dt_debt);
			gridView1.EndUpdate();
			this.Cursor = Cursors.Arrow;

		}

		private void ComboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.HandleSearch(comboBoxEdit1.EditValue.ToString());
		}

		private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			gridView1.BeginUpdate();
			dt_debt.Rows.Clear();
			debtAdapter.Fill(dt_debt);
			gridView1.EndUpdate();
		}

		private void GridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			if (e.Column.FieldName == "RC002")
			{
				if (e.Value.ToString() == "0")
					e.DisplayText = "男";
				else if (e.Value.ToString() == "1")
					e.DisplayText = "女";
				else
					e.DisplayText = "未知";
			}
		}

		private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SaveFileDialog fileDialog = new SaveFileDialog();
			fileDialog.Title = "导出Excel";
			fileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";

			DialogResult dialogResult = fileDialog.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
				options.TextExportMode = TextExportMode.Text;//设置导出模式为文本


				gridControl1.ExportToXlsx(fileDialog.FileName, options);
				XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}
}
