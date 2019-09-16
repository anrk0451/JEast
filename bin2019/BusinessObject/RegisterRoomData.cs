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
using DevExpress.XtraSplashScreen;
using JEast.Action;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using JEast.windows;

namespace JEast.BusinessObject
{
	public partial class RegisterRoomData : BaseBusiness
	{
		private DataTable gridTable = new DataTable("grid");

		private DataTable dt_rg01 = new DataTable("RG01");
		private OracleDataAdapter rg01Adapter = new OracleDataAdapter("select * from v_rg01 order by rg002,rg001", SqlAssist.conn);

		private DataTable dt_bi01 = new DataTable("BI01");
		private OracleDataAdapter bi01Adapter =
			new OracleDataAdapter(@"select * from bi01 where rg001 = :rg001 order by bi005 desc,bi008", SqlAssist.conn);
		private OracleParameter op_rg001 = new OracleParameter("rg001", OracleDbType.Varchar2, 10);


		private DataTable dt_bitInfo = new DataTable("BitInfo");
		private OracleDataAdapter bitInfoAdapter = new OracleDataAdapter("select * from v_bitreport", SqlAssist.conn);

		private string curRegionId = string.Empty;

		public RegisterRoomData()
		{
			InitializeComponent();
			SplashScreenManager.ShowDefaultWaitForm("请等待", "处理中....");

			rg01Adapter.Fill(dt_rg01);

			treeList1.DataSource = dt_rg01;
			treeList1.ExpandToLevel(1);

			op_rg001.Direction = ParameterDirection.Input;
			bi01Adapter.SelectCommand.Parameters.Add(op_rg001);

			gridControl2.DataSource = dt_bitInfo;
			SplashScreenManager.CloseDefaultWaitForm();
		}

		private void RegisterRoomData_Load(object sender, EventArgs e)
		{

		}

		private void TreeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
		{
			if (e.Node == null) return;

			this.WriteRgTitle(e.Node.GetValue("RG001").ToString());

			if (e.Node.Level < 3)
			{
				pictureBox1.Visible = true;
				gridControl1.Visible = false;
				return;
			}
			else
			{
				pictureBox1.Visible = false;
				gridControl1.Visible = true;
				curRegionId = e.Node.GetValue("RG001").ToString();
			}
			SplashScreenManager.ShowDefaultWaitForm("请等待", "处理中....");

			DrawGrid(e.Node);
			SplashScreenManager.CloseDefaultWaitForm();
		}

		/// <summary>
		/// 描述寄存结构信息
		/// </summary>
		/// <param name="rg001"></param>
		private void WriteRgTitle(string rg001)
		{
			lc_all.Text = RegisterAction.GetRgAllBits(rg001).ToString();
			lc_free.Text = RegisterAction.GetRgFreeBits(rg001).ToString();
			lc_used.Text = RegisterAction.GetRgUsedBits(rg001).ToString();
			lc_debt.Text = RegisterAction.GetRgDebtBits(rg001).ToString();
		}

		/// <summary>
		/// 绘制寄存号位
		/// </summary>
		private void DrawGrid(TreeListNode regionNode)
		{
			int rows = int.Parse(regionNode.GetValue("RG020").ToString());  //层数
			int cols = int.Parse(regionNode.GetValue("RG021").ToString());

			gridView1.BeginUpdate();

			/////////清除所有数据
			gridTable.Rows.Clear();
			gridTable.Columns.Clear();


			////生成列
			DataColumn col = null;
			DataRow row = null;
			for (int i = 1; i <= cols; i++)
			{
				col = new DataColumn("col" + i.ToString(), typeof(string));
				col.ReadOnly = true;
				gridTable.Columns.Add(col);
			}

			dt_bi01.Clear();
			op_rg001.Value = regionNode.GetValue("RG001").ToString();
			bi01Adapter.Fill(dt_bi01);

			int bitIndex = 0;
			for (int i = 1; i <= rows; i++)
			{
				row = gridTable.NewRow();
				for (int j = 1; j <= cols; j++)
				{
					row.SetField(j - 1, dt_bi01.Rows[bitIndex]["BI003"]);
					bitIndex++;
				}
				gridTable.Rows.Add(row);
			}

			gridControl1.DataSource = gridTable;
			gridView1.PopulateColumns();

			//设置列宽 
			for (int i = 1; i <= cols; i++)
			{
				gridView1.Columns[i - 1].Width = 50;
			}
			//grid标题
			TreeListNode hall_node = regionNode.ParentNode.ParentNode;
			TreeListNode room_node = regionNode.ParentNode;
			gridView1.ViewCaption = hall_node.GetDisplayText("RG003") + "-" + room_node.GetDisplayText("RG003") + "-" + regionNode.GetDisplayText("RG003");

			gridView1.EndUpdate();

		}

		private void GridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
		{
			string s_bitStatus = RegisterAction.GetBitStatus(curRegionId, e.CellValue.ToString());
			if (s_bitStatus == "9")       //空闲
			{
				e.Appearance.BackColor = Color.Green;
				e.Appearance.ForeColor = Color.White;
			}
			else if (s_bitStatus == "0")
			{
				e.Appearance.BackColor = Color.White;
				e.Appearance.ForeColor = Color.White;
			}
			else if (s_bitStatus == "1")  //占用
			{
				e.Appearance.BackColor = Color.Yellow;
				e.Appearance.ForeColor = Color.Black;
			}
			else if (s_bitStatus == "2")  //欠费
			{
				e.Appearance.BackColor = Color.Red;
				e.Appearance.ForeColor = Color.White;
			}
		}

		private void GridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
		{
			e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			if (e.Info.IsRowIndicator)
			{
				if (e.RowHandle >= 0)
				{
					e.Info.DisplayText = (gridView1.RowCount - e.RowHandle).ToString(); // (e.RowHandle + 1).ToString();
				}
				else if (e.RowHandle < 0 && e.RowHandle > -1000)
				{
					e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
					e.Info.DisplayText = "G" + e.RowHandle.ToString();
				}
			}
		}

		/// <summary>
		/// 刷新数据
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SplashScreenManager.ShowDefaultWaitForm("请等待", "刷新数据....");
			treeList1.BeginUpdate();
			treeList1.ClearNodes();

			dt_rg01.Rows.Clear();
			rg01Adapter.Fill(dt_rg01);
			treeList1.ExpandAll();

			treeList1.EndUpdate();
			SplashScreenManager.CloseDefaultWaitForm();
		}

		/// <summary>
		/// 导出寄存信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			string s_rg001 = string.Empty;

			if (treeList1.FocusedNode.Level == 0)        //寄存结构
			{
				bitInfoAdapter.SelectCommand.CommandText =
					@"select * from v_bitReport";
			}
			else if (treeList1.FocusedNode.Level == 1)  //寄存楼
			{
				s_rg001 = treeList1.FocusedNode.GetValue("RG001").ToString();
				bitInfoAdapter.SelectCommand.CommandText =
					@"select * from v_bitReport where bi030 = '" + s_rg001 + "'";
			}
			else if (treeList1.FocusedNode.Level == 2)  //寄存室
			{
				s_rg001 = treeList1.FocusedNode.GetValue("RG001").ToString();
				bitInfoAdapter.SelectCommand.CommandText =
					@"select * from v_bitReport where bi020 = '" + s_rg001 + "'";
			}
			else if (treeList1.FocusedNode.Level == 3)  //寄存架
			{
				s_rg001 = treeList1.FocusedNode.GetValue("RG001").ToString();
				bitInfoAdapter.SelectCommand.CommandText =
					@"select * from v_bitReport where rg001 = '" + s_rg001 + "'";
			}

			dt_bitInfo.Rows.Clear();
			bitInfoAdapter.Fill(dt_bitInfo);

			SaveFileDialog fileDialog = new SaveFileDialog();
			fileDialog.Title = "导出Excel";
			fileDialog.Filter = "Excel文件(*.xlsx)|*.xls";
			DialogResult dialogResult = fileDialog.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
				gridControl2.ExportToXls(fileDialog.FileName);
				DevExpress.XtraEditors.XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// 双击 显示号位情况
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GridView1_MouseDown(object sender, MouseEventArgs e)
		{
			GridHitInfo hInfo = gridView1.CalcHitInfo(new Point(e.X, e.Y));
			string s_bi003 = string.Empty;

			if (e.Button == MouseButtons.Left && e.Clicks == 2)
			{	
				//判断光标是否在行范围内  
				if (hInfo.InRow)
				{
					if (!string.IsNullOrEmpty(curRegionId))
					{
						s_bi003 = gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, gridView1.FocusedColumn).ToString();
						if(s_bi003.Substring(0,1) != "#")
						{
							Frm_bitInfo frm_bitInfo = new Frm_bitInfo(curRegionId, s_bi003);
							frm_bitInfo.ShowDialog();
							frm_bitInfo.Dispose();								 
						}
						
					}

				}
			}
		}
	}
}
