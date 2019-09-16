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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace JEast.windows
{
	public partial class Frm_freebit : MyDialog
	{
		private DataTable dt_room = new DataTable("room");
		private DataTable dt_region = new DataTable("region");
		private DataTable dt_bit = new DataTable("bit");

		private DataTable gridTable = new DataTable("grid");

		private OracleDataAdapter roomAdapter =
			new OracleDataAdapter("select rg001,pkg_business.fun_getRoomName(rg001) rg003 from rg01 where status = '1' and rg002 = '2' order by rg001", SqlAssist.conn);
		private OracleDataAdapter regionAdapter =
			new OracleDataAdapter("select * from rg01 where rg009 = :roomId and status = '1' and rg002 = '3'  ", SqlAssist.conn);
		private OracleDataAdapter bitAdapter =
			new OracleDataAdapter("select * from bi01 where rg001 = :regionId  order by bi005 desc,bi008", SqlAssist.conn);


		private OracleParameter op_roomId = new OracleParameter("roomId", OracleDbType.Varchar2, 10);
		private OracleParameter op_regionId = new OracleParameter("regionId", OracleDbType.Varchar2, 10);

		private string curRoomId = string.Empty;
		private string curRegionId = string.Empty;
		private int selIndex;

		public Frm_freebit()
		{
			InitializeComponent();
			regionAdapter.Requery = true;
			bitAdapter.Requery = true;

			op_roomId.Direction = ParameterDirection.Input;
			regionAdapter.SelectCommand.Parameters.Add(op_roomId);

			op_regionId.Direction = ParameterDirection.Input;
			bitAdapter.SelectCommand.Parameters.Add(op_regionId);
		}

		private void Frm_freebit_Load(object sender, EventArgs e)
		{
			roomAdapter.Fill(dt_room);
			lookup_room.Properties.DataSource = dt_room;
			lookup_room.Properties.DisplayMember = "RG003";
			lookup_room.Properties.ValueMember = "RG001";
			if (dt_room.Rows.Count > 0)
			{
				lookup_room.EditValue = dt_room.Rows[dt_room.Rows.Count - 1]["RG001"].ToString();
			}
		}

		private void Lookup_room_EditValueChanged(object sender, EventArgs e)
		{
			curRoomId = lookup_room.EditValue.ToString();
			dt_region.Rows.Clear();
			imageListBoxControl1.Items.Clear();

			op_roomId.Value = curRoomId;
			regionAdapter.Fill(dt_region);
			foreach (DataRow r in dt_region.Rows)
			{
				imageListBoxControl1.Items.Add(r["RG003"].ToString());
			}
		}

		/// <summary>
		/// 区排选择
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImageListBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			selIndex = (sender as ImageListBoxControl).SelectedIndex;
			if (selIndex < 0) return;
			curRegionId = dt_region.Rows[selIndex]["RG001"].ToString();

			op_regionId.Value = curRegionId;
			dt_bit.Rows.Clear();
			bitAdapter.Fill(dt_bit);

			DrawGrid();
		}


		/// <summary>
		/// 绘制寄存号位
		/// </summary>
		private void DrawGrid()
		{
			string s_bitStatus = string.Empty;
			int rows = int.Parse(dt_region.Rows[selIndex]["RG020"].ToString());  //层数
			int cols = int.Parse(dt_region.Rows[selIndex]["RG021"].ToString());  //列数

			gridView1.BeginUpdate();

			/////////清除所有数据
			gridTable.Clear();
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
 

			int bitIndex = 0;
			for (int i = 1; i <= rows; i++)
			{
				row = gridTable.NewRow();
				for (int j = 1; j <= cols; j++)
				{
					row.SetField(j - 1, dt_bit.Rows[bitIndex]["BI003"]);
					bitIndex++;
				}
				gridTable.Rows.Add(row);
			}

			gridControl1.DataSource = gridTable;
			gridView1.PopulateColumns();

			//设置列宽 
			for (int i = 1; i <= cols; i++)
			{
				gridView1.Columns[i - 1].Width = 60;
			}
			//grid标题
			//TreeListNode hall_node = regionNode.ParentNode.ParentNode;
			//TreeListNode room_node = regionNode.ParentNode;
			//gridView1.ViewCaption = hall_node.GetDisplayText("RG003") + "-" + room_node.GetDisplayText("RG003") + "-" + regionNode.GetDisplayText("RG003");

			gridView1.EndUpdate();
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

		private void GridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
		{
			string s_bitStatus = RegisterAction.GetBitStatus(curRegionId, e.CellValue.ToString());
			if (s_bitStatus == "9")
			{
				e.Appearance.BackColor = Color.Green;
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
			else if (s_bitStatus == "0")
			{
				e.Appearance.BackColor = Color.White;
				e.Appearance.ForeColor = Color.White;
			}
			else
			{
				MessageBox.Show(curRegionId, "排号");
				MessageBox.Show(e.CellValue.ToString());
				e.Appearance.BackColor = Color.Blue;
				e.Appearance.ForeColor = Color.White;
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
					int rowHandle = gridView1.FocusedRowHandle;
					string fieldName = gridView1.FocusedColumn.FieldName;
					string bi003 = gridView1.GetRowCellValue(rowHandle, fieldName).ToString();
					string s_status = RegisterAction.GetBitStatus(curRegionId, bi003);

					if (s_status == "9")  //空闲
					{
						(this.swapdata["parent"] as MyDialog).swapdata["regionId"] = curRegionId;
						(this.swapdata["parent"] as MyDialog).swapdata["bitDesc"] = bi003;
						DialogResult = DialogResult.OK;
						this.Close();
					}
					else
					{
						MessageBox.Show("此号位当前被占用!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}

				}
			}
		}
	}
}