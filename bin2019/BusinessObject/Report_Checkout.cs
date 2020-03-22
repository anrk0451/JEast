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
using DevExpress.XtraPrinting;
using JEast.Action;

namespace JEast.BusinessObject
{

	public partial class Report_Checkout : BaseBusiness
	{
		private DataTable dt_out = new DataTable();
		private OracleDataAdapter outAdapter =
			new OracleDataAdapter("select * from v_Checkout where (to_char(ac015,'yyyy-mm-dd') between :begin and :end) and ac003 like :ac003 and ac007_2 like :ac007", SqlAssist.conn);

		OracleParameter op_begin = null;
		OracleParameter op_end = null;
		OracleParameter op_ac003 = null;
		OracleParameter op_ac007 = null;

		public Report_Checkout()
		{
			InitializeComponent();
		}

		private void Report_Checkout_Load(object sender, EventArgs e)
		{
			//this.DisplayCondition();
			op_begin = new OracleParameter("begin", OracleDbType.Varchar2, 20);
			op_begin.Direction = ParameterDirection.Input;

			op_end = new OracleParameter("end", OracleDbType.Varchar2, 20);
			op_end.Direction = ParameterDirection.Input;

			op_ac003 = new OracleParameter("ac003", OracleDbType.Varchar2, 20);
			op_ac003.Direction = ParameterDirection.Input;

			op_ac007 = new OracleParameter("ac007", OracleDbType.Varchar2, 20);
			op_ac007.Direction = ParameterDirection.Input;

			outAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end, op_ac003, op_ac007 });
			gridControl1.DataSource = dt_out;
		}

		/// <summary>
		/// 查询条件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.DisplayCondition();
		}



		/// <summary>
		/// 显示查询条件窗口
		/// </summary>
		private void DisplayCondition()
		{
			Frm_Report_Checkout frm_out = new Frm_Report_Checkout();
			frm_out.swapdata["BusinessObject"] = this;
			if (frm_out.ShowDialog() == DialogResult.OK)
			{
				
				string s_begin = string.Empty;
				string s_end = string.Empty;
				string s_ac003 = string.Empty;
				string s_ac007 = string.Empty;

				if (this.swapdata["dbegin"] == null || this.swapdata["dbegin"] is System.DBNull)
				{
					s_begin = "1900/01/01";
				}
				else
				{
					s_begin = Convert.ToDateTime(this.swapdata["dbegin"]).ToString("yyyy/MM/dd");
				}

				if (this.swapdata["dend"] == null || this.swapdata["dend"] is System.DBNull)
				{
					s_end = "9999/12/31";
				}
				else
				{
					s_end = Convert.ToDateTime(this.swapdata["dend"]).ToString("yyyy/MM/dd");
				}

				if (this.swapdata["AC003"] == null || string.IsNullOrEmpty(this.swapdata["AC003"].ToString()))
				{
					s_ac003 = "%";
				}
				else
				{
					s_ac003 = this.swapdata["AC003"].ToString() + "%";
				}

				s_ac007 = this.swapdata["AC007"].ToString();

				op_begin.Value = s_begin;
				op_end.Value = s_end;
				op_ac003.Value = s_ac003;
				op_ac007.Value = s_ac007;

				this.Cursor = Cursors.WaitCursor;
				gridView1.BeginUpdate();
				dt_out.Rows.Clear();
				outAdapter.Fill(dt_out);
				gridView1.EndUpdate();
				this.Cursor = Cursors.Arrow;
			}
			frm_out.Dispose();
		}

		/// <summary>
		/// 刷新数据
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.RefreshData();
		}

		/// <summary>
		/// 刷新数据过程
		/// </summary>
		private void RefreshData()
		{
			gridView1.BeginUpdate();
			dt_out.Rows.Clear();
			outAdapter.Fill(dt_out);
			gridView1.EndUpdate();
		}

		/// <summary>
		/// 导出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

		/// <summary>
		/// 查找
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) =>
			//显示查找对话框
			gridView1.ShowFindPanel();


		/// <summary>
		/// 已出灵数据修改
		/// </summary>
		/// <param name="rowHandle"></param>
		private void Edit(int rowHandle)
		{
			//FireOutEdit frm_edit = new FireOutEdit();
			//frm_edit.cdata["AC001"] = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
			//frm_edit.cdata["BusinessObject"] = this;
			//if (frm_edit.ShowDialog() == DialogResult.OK)
			//{
			//	frm_edit.Dispose();
			//	this.RefreshData();
			//}
		}

		/// <summary>
		/// 已办业务查询
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle < 0) return;

			string s_ac001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
 
		    Envior.mform.openBusinessObject("FireBusiness", s_ac001);
			 
		}

		///补打火化证明
		private void BarButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle < 0) return;

			string s_ac001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
			if (MessageBox.Show("现在打印【火化证明】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
			{
				PrtServAction.Print_HHZM(s_ac001, Envior.mform.Handle.ToInt32());
			}
		}

		private void GridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{

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

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
