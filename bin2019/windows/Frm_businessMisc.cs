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
using JEast.BusinessObject;
using JEast.DataSet;
using DevExpress.XtraGrid.Views.Base;

namespace JEast.windows
{
	public partial class Frm_businessMisc : MyDialog
	{
		BaseBusiness bo = null;
		Sa01_ds sa01_ds = null;

		public Frm_businessMisc()
		{
			InitializeComponent();
		}

		private void Frm_businessMisc_Load(object sender, EventArgs e)
		{
			if (this.swapdata["businessObject"] is FireBusiness)
				bo = this.swapdata["businessObject"] as FireBusiness;
			else if(this.swapdata["businessObject"] is TempSales)
				bo = this.swapdata["businessObject"] as TempSales;

			sa01_ds = this.swapdata["dataset"] as Sa01_ds;
 
			gridControl1.DataSource = sa01_ds.Si01;
			gridControl2.DataSource = sa01_ds.Si01;
			gridControl3.DataSource = sa01_ds.Si01;
			gridControl4.DataSource = sa01_ds.Si01;
			 
			gridView1.ActiveFilterString = "item_type = '05' ";
			gridView2.ActiveFilterString = "item_type = '10' ";
			gridView3.ActiveFilterString = "item_type = '11' ";
			gridView4.ActiveFilterString = "item_type = '12' ";
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

		private void GridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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

		private void GridView3_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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

		private void GridView4_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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

		private void GridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
		{
			if (e.Column.FieldName == "NUMS" && e.IsGetData)
			{
				e.Value = 1;
			}
		}

		private void GridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
		{
			string colName = (sender as ColumnView).FocusedColumn.FieldName.ToUpper();
			if (colName == "NUMS")
			{
				if (e.Value == null || e.Value is System.DBNull)
				{
					e.Valid = false;
					e.ErrorText = "请输入数量!";
					return;
				}
				else if (int.Parse(e.Value.ToString()) <= 0)
				{
					e.Valid = false;
					e.ErrorText = "数量必须大于0！";
					return;
				}
			}
		}

		private void GridView2_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
		{
			if (e.Action == CollectionChangeAction.Add && gridView2.SelectedRowsCount > 1)
			{
				int row = e.ControllerRow;
				gridView2.BeginUpdate();
				gridView2.ClearSelection();
				gridView2.SelectRow(row);
				gridView2.EndUpdate();
			}
		}

		private void GridView3_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
		{
			if (e.Action == CollectionChangeAction.Add && gridView3.SelectedRowsCount > 1)
			{
				int row = e.ControllerRow;
				gridView3.BeginUpdate();
				gridView3.ClearSelection();
				gridView3.SelectRow(row);
				gridView3.EndUpdate();
			}
		}

		private void GridView4_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
		{
			string colName = (sender as ColumnView).FocusedColumn.FieldName.ToUpper();
			if (colName == "NUMS")
			{
				if (e.Value == null || e.Value is System.DBNull)
				{
					e.Valid = false;
					e.ErrorText = "请输入数量!";
					return;
				}
				else if (int.Parse(e.Value.ToString()) <= 0)
				{
					e.Valid = false;
					e.ErrorText = "数量必须大于0！";
					return;
				}
			}
		}

		private void Sb_ok_Click(object sender, EventArgs e)
		{
			List<string> itemId_list = new List<string>();
			List<string> itemType_list = new List<string>();
			List<decimal> price_list = new List<decimal>();
			List<int> nums_list = new List<int>();

			if (!gridView1.PostEditor()) return;
			if (!gridView1.UpdateCurrentRow()) return;
			if (!gridView4.PostEditor()) return;
			if (!gridView4.UpdateCurrentRow()) return;


			Int32[] selectedRowHandles = gridView1.GetSelectedRows();
			for (int i = 0; i < selectedRowHandles.Length; i++)
			{
				int selectedRowHandle = selectedRowHandles[i];
				if (selectedRowHandle >= 0)
				{
					itemId_list.Add(gridView1.GetRowCellValue(selectedRowHandle, "ITEM_ID").ToString());
					itemType_list.Add(gridView1.GetRowCellValue(selectedRowHandle, "ITEM_TYPE").ToString());
					price_list.Add(decimal.Parse(gridView1.GetRowCellValue(selectedRowHandle, "PRICE").ToString()));
					nums_list.Add(int.Parse(gridView1.GetRowCellValue(selectedRowHandle, "NUMS").ToString()));
				}
			}

			selectedRowHandles = gridView2.GetSelectedRows();
			for (int i = 0; i < selectedRowHandles.Length; i++)
			{
				int selectedRowHandle = selectedRowHandles[i];
				if (selectedRowHandle >= 0)
				{
					itemId_list.Add(gridView2.GetRowCellValue(selectedRowHandle, "ITEM_ID").ToString());
					itemType_list.Add(gridView2.GetRowCellValue(selectedRowHandle, "ITEM_TYPE").ToString());
					price_list.Add(decimal.Parse(gridView2.GetRowCellValue(selectedRowHandle, "PRICE").ToString()));
					nums_list.Add(int.Parse(gridView2.GetRowCellValue(selectedRowHandle, "NUMS").ToString()));
				}
			}

			selectedRowHandles = gridView3.GetSelectedRows();
			for (int i = 0; i < selectedRowHandles.Length; i++)
			{
				int selectedRowHandle = selectedRowHandles[i];
				if (selectedRowHandle >= 0)
				{
					itemId_list.Add(gridView3.GetRowCellValue(selectedRowHandle, "ITEM_ID").ToString());
					itemType_list.Add(gridView3.GetRowCellValue(selectedRowHandle, "ITEM_TYPE").ToString());
					price_list.Add(decimal.Parse(gridView3.GetRowCellValue(selectedRowHandle, "PRICE").ToString()));
					nums_list.Add(int.Parse(gridView3.GetRowCellValue(selectedRowHandle, "NUMS").ToString()));
				}
			}

			selectedRowHandles = gridView4.GetSelectedRows();
			for (int i = 0; i < selectedRowHandles.Length; i++)
			{
				int selectedRowHandle = selectedRowHandles[i];
				if (selectedRowHandle >= 0)
				{
					itemId_list.Add(gridView4.GetRowCellValue(selectedRowHandle, "ITEM_ID").ToString());
					itemType_list.Add(gridView4.GetRowCellValue(selectedRowHandle, "ITEM_TYPE").ToString());
					price_list.Add(decimal.Parse(gridView4.GetRowCellValue(selectedRowHandle, "PRICE").ToString()));
					nums_list.Add(int.Parse(gridView4.GetRowCellValue(selectedRowHandle, "NUMS").ToString()));
				}
			}
			if (itemId_list.Count == 0)
			{
				MessageBox.Show("请选择记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			bo.swapdata["itemIdList"] = itemId_list;
			bo.swapdata["priceList"] = price_list;
			bo.swapdata["numsList"] = nums_list;
			bo.swapdata["itemTypeList"] = itemType_list;

			DialogResult = DialogResult.OK;
			this.Dispose();
		}
	}
}