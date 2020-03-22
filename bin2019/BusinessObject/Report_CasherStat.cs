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
using JEast.Misc;
using Oracle.ManagedDataAccess.Client;
using JEast.windows;
using JEast.Action;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;

namespace JEast.BusinessObject
{
	public partial class Report_CasherStat : BaseBusiness
	{
		DataTable dt_casherStat = new DataTable();
		OracleDataAdapter statAdapter = new OracleDataAdapter("select * from rep_casherStat order by uc001", SqlAssist.conn);

		DataTable dt_normal = new DataTable();
		OracleDataAdapter norAdapter = new OracleDataAdapter("select * from v_financeday where  fa100 = :fa100 and (to_char(fa200,'yyyy-mm-dd') between :begin and :end) and fa004 > 0", SqlAssist.conn);

		OracleParameter op_begin = new OracleParameter("begin", OracleDbType.Varchar2, 20);
		OracleParameter op_end = new OracleParameter("end", OracleDbType.Varchar2, 20);
		OracleParameter op_fa100 = new OracleParameter("fa100", OracleDbType.Varchar2, 10);

		string s_begin = string.Empty;
		string s_end = string.Empty;
		string s_fa100 = string.Empty;


		public Report_CasherStat()
		{
			InitializeComponent();
		}

		private void Report_CasherStat_Load(object sender, EventArgs e)
		{
			op_end.Direction = ParameterDirection.Input;
			op_begin.Direction = ParameterDirection.Input;
			op_fa100.Direction = ParameterDirection.Input;

			norAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_fa100, op_begin, op_end });
			 
			gridControl_center.DataSource = dt_casherStat;
			gridControl1.DataSource = dt_normal;
		}

		/// <summary>
		/// 查询条件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Frm_Report_CasherStat frm_1 = new Frm_Report_CasherStat();
			if (frm_1.ShowDialog() == DialogResult.OK)
			{
				if (frm_1.swapdata["dbegin"] == null || frm_1.swapdata["dbegin"] is System.DBNull)
				{
					s_begin = "1900-01-01";
				}
				else
				{
					s_begin = Convert.ToDateTime(frm_1.swapdata["dbegin"]).ToString("yyyy-MM-dd");
				}

				if (frm_1.swapdata["dend"] == null || frm_1.swapdata["dend"] is System.DBNull)
				{
					s_end = "9999-12-31";
				}
				else
				{
					s_end = Convert.ToDateTime(frm_1.swapdata["dend"]).ToString("yyyy-MM-dd");
				}

				if (frm_1.swapdata["FA100"] == null)
					s_fa100 = "%";
				else
					s_fa100 = frm_1.swapdata["FA100"].ToString();

				op_begin.Value = s_begin;
				op_end.Value = s_end;
				op_fa100.Value = s_fa100;

				this.RefreshData();
			}
			frm_1.Dispose();
		}

		/// <summary>
		/// 刷新数据
		/// </summary>
		private void RefreshData()
		{
			if (MiscAction.CasherStat(s_begin, s_end, s_fa100) > 0)
			{
				this.Cursor = Cursors.WaitCursor;

				dt_casherStat.Rows.Clear();
				statAdapter.Fill(dt_casherStat);

				gridColumn15.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn15.SummaryItem.DisplayFormat = "{0:N0}";
				gridColumn16.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn16.SummaryItem.DisplayFormat = "{0:N2}";

				groupControl1.Text = "统计日期 " + s_begin + "至" + s_end;

				this.Cursor = Cursors.Arrow;
			}
		}

		/// <summary>
		/// 查找
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			gridView1.ShowFindPanel();
		}

		/// <summary>
		/// 刷新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.RefreshData();
		}

		/// <summary>
		/// 导出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			GridControl grid = gridControl1;
			SaveFileDialog fileDialog = new SaveFileDialog();
			fileDialog.Title = "导出Excel";
			fileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";

			DialogResult dialogResult = fileDialog.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
				options.TextExportMode = TextExportMode.Text;//设置导出模式为文本
				grid.ExportToXlsx(fileDialog.FileName, options);
				XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// 绘制行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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

		private void gridView_center_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			int rowHandle = e.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				string s_fa100 = gridView_center.GetRowCellValue(rowHandle, "UC001").ToString();
				op_fa100.Value = s_fa100;
				dt_normal.Rows.Clear();
				norAdapter.Fill(dt_normal);				 
			}
		}
	}
}
