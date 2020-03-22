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
using JEast.windows;
using JEast.Action;
using DevExpress.XtraPrinting;

namespace JEast.BusinessObject
{
	public partial class Report_ItemStat : BaseBusiness
	{
		private DataTable dt_cs = new DataTable();
		private OracleDataAdapter csAdapter =
			new OracleDataAdapter("select * from v_itemstat ", SqlAssist.conn);

		private string s_begin = string.Empty;
		private string s_end = string.Empty;
		private string[] classArry;

		public Report_ItemStat()
		{
			InitializeComponent();
		}

		private void Report_ItemStat_Load(object sender, EventArgs e)
		{
			gridControl1.DataSource = dt_cs;
            this.DisplayContion();
        }

		private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
            this.DisplayContion();
		}

        /// <summary>
        /// 显示查询条件窗口
        /// </summary>
        private void DisplayContion()
        {
            Frm_Report_ClassStat frm_stat = new Frm_Report_ClassStat();
            frm_stat.swapdata["BusinessObject"] = this;

            if (frm_stat.ShowDialog() == DialogResult.OK)
            {
                frm_stat.Dispose();

                classArry = this.swapdata["class"] as string[];

                if (this.swapdata["dbegin"] == null || this.swapdata["dbegin"] is System.DBNull)
                {
                    s_begin = "1900-01-01";
                }
                else
                {
                    s_begin = Convert.ToDateTime(this.swapdata["dbegin"]).ToString("yyyy-MM-dd");
                }

                if (this.swapdata["dend"] == null || this.swapdata["dend"] is System.DBNull)
                {
                    s_end = "9999-12-31";
                }
                else
                {
                    s_end = Convert.ToDateTime(this.swapdata["dend"]).ToString("yyyy-MM-dd");
                }

                this.RefreshData();

            }
        }

		/// <summary>
		/// 刷新数据
		/// </summary>
		private void RefreshData()
		{
			this.Cursor = Cursors.WaitCursor;
			int re = MiscAction.ClassStat(s_begin, s_end, classArry);
			if (re > 0)
			{

				gridView1.BeginUpdate();
				dt_cs.Rows.Clear();
				csAdapter.Fill(dt_cs);

				gridColumn4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn4.SummaryItem.DisplayFormat = "合计 = {0:N2}";

                gridColumn6.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gridColumn6.SummaryItem.DisplayFormat = "税额合计 = {0:N2}";

                gridColumn8.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gridColumn8.SummaryItem.DisplayFormat = "不含税合计 = {0:N2}";

                bs_bs.Caption = "           共有收费笔数:" + MiscAction.GetClassStat_BS().ToString() + "笔";

				//gridColumn1.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
				//gridColumn1.SummaryItem.DisplayFormat = "合计 = {0:N2}";

				gridView1.EndUpdate();

			}
			this.Cursor = Cursors.Arrow;
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

		private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.RefreshData();
		}

		private void BarButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

		private void BarButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (dt_cs.Rows.Count > 0)
				PrtServAction.Print_Report_ItemStat(s_begin, s_end, Envior.mform.Handle.ToInt32());
		}
	}
}
