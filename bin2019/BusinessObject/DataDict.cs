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
using JEast.DataSet;
using Oracle.ManagedDataAccess.Client;
using JEast.Misc;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;

namespace JEast.BusinessObject
{
	public partial class DataDict : BaseBusiness
	{
		int curIndex = 0;
		Boolean LOCK = true;
		St01_ds st01_ds = new St01_ds();
		OracleParameter op_st002 = null;

		public DataDict()
		{
			InitializeComponent();
		}

		private void DataDict_Load(object sender, EventArgs e)
		{
            gridControl1.DataSource = st01_ds.St01;
  
            ///查询参数
            op_st002 = new OracleParameter("st002", OracleDbType.Varchar2, 15);
			op_st002.Direction = ParameterDirection.Input;
			st01_ds.st01Adapter.SelectCommand.Parameters.Add(op_st002);


			//设置初始选择
			imageListBoxControl1.SetSelected(0, true);
			curIndex = 0;
 
            //设置自动过滤(过滤掉删除行:此操作应该在数据集装入数据后)
            gridView1.ActiveFilter.Clear();
            gridView1.ActiveFilterString =  "STATUS <> '0'";

            /// 设置排序列
            gridView1.Columns["SORTID"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
			gridView1.Focus();
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
		/// 新增
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			gridView1.AddNewRow();
			int rowno = gridView1.FocusedRowHandle;
			/////// 设置焦点 开始编辑 !!!
			gridView1.FocusedColumn = gridView1.Columns["ST003"];
			gridView1.ShowEditor();
		}

		private void ImageListBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ((st01_ds.St01 as DataTable).GetChanges() != null && LOCK)
			{
				if (MessageBox.Show("数据已经改变,是否保存?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					if (Save())
					{
						curIndex = imageListBoxControl1.SelectedIndex;
						op_st002.Value = GetCurTypeStr(curIndex);
                        st01_ds.St01.Rows.Clear();
						st01_ds.st01Adapter.Fill(st01_ds.St01);
					}
					else
					{
						LOCK = false;
						imageListBoxControl1.SetSelected(curIndex, true);
					}
				}
				else
				{
					curIndex = imageListBoxControl1.SelectedIndex;
					op_st002.Value = GetCurTypeStr(curIndex);
                    st01_ds.St01.Rows.Clear();
                    st01_ds.st01Adapter.Fill(st01_ds.St01);                    
                }

			}
			else if (LOCK)
			{
				curIndex = imageListBoxControl1.SelectedIndex;
				op_st002.Value = GetCurTypeStr(curIndex);
                st01_ds.St01.Rows.Clear();
                st01_ds.st01Adapter.Fill(st01_ds.St01);
			}
			else
			{
				LOCK = true;
			}
		}

		////保存过程 
		private Boolean Save()
		{
			if (!gridView1.PostEditor()) return false;
			if (!gridView1.UpdateCurrentRow()) return false;

			try
			{
				st01_ds.st01Adapter.Update(st01_ds.St01);
				MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return true;
			}
			catch (Exception ee)
			{
				MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}

		////根据索引获取当前类别
		private String GetCurTypeStr(int index)
		{
			string result = string.Empty;
			switch (index)
			{
				case 0:
					result = "DIEREASON";
					break;
				case 1:
					result = "DISTRICT";
					break;
				case 2:
					result = "RELATION";
					break;
				case 3:
					result = "DRIVER";
					break;
			}
			return result;
		}

		//初始化新行
		private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
		{
			//// 初始化新行时触发(当在新行中)
			GridView view = sender as GridView;
			string st001 = Tools.GetEntityPK("ST01");

			gridView1.SetRowCellValue(e.RowHandle, "ST002", GetCurTypeStr(curIndex));
			gridView1.SetRowCellValue(e.RowHandle, "ST001", st001);
			gridView1.SetRowCellValue(e.RowHandle, "STATUS", "1");
			gridView1.SetRowCellValue(e.RowHandle, "SORTID", Convert.ToInt32(st001));
		}

		private void GridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
		{
			string value = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ST003").ToString();
			if (String.IsNullOrEmpty(value))
			{
				e.Valid = false;
				(sender as ColumnView).SetColumnError(gridView1.Columns["ST003"], "数据项不能为空!");
			}
		}

        private void GridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            string colName = (sender as ColumnView).FocusedColumn.FieldName.ToUpper();
            if (colName.Equals("ST003"))       //数据项值
            {
                if (String.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Valid = false;
                    e.ErrorText = "数据项值不能为空!";
                }
                else
                {
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        if (i == (sender as ColumnView).FocusedRowHandle) continue;
                        if (gridView1.GetRowCellValue(i, "ST003") == null) continue;

                        //如果名字相同,则校验不通过!                        
                        if (String.Equals(gridView1.GetRowCellValue(i, "ST003").ToString(), e.Value.ToString()))
                        {
                            e.Valid = false;
                            e.ErrorText = "值已经存在!";
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                if (MessageBox.Show("确认要删除当前的记录吗", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }

            }
            gridView1.SetFocusedRowCellValue("STATUS", "0");
            gridView1.UpdateCurrentRow();
        }

        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int row = gridView1.FocusedRowHandle;
            if (row <= 0) return;

            int prior_sortId = int.Parse(gridView1.GetRowCellValue(row - 1, "SORTID").ToString());
            int cur_sortId = int.Parse(gridView1.GetRowCellValue(row, "SORTID").ToString());

            gridView1.SetRowCellValue(row, "SORTID", prior_sortId);
            gridView1.SetRowCellValue(row - 1, "SORTID", cur_sortId);
        }

        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int row = gridView1.FocusedRowHandle;
            if ((row >= gridView1.RowCount - 2) || row < 0) return;

            int next_sortId = int.Parse(gridView1.GetRowCellValue(row + 1, "SORTID").ToString());
            int cur_sortId = int.Parse(gridView1.GetRowCellValue(row, "SORTID").ToString());

            gridView1.SetRowCellValue(row, "SORTID", next_sortId);
            gridView1.SetRowCellValue(row + 1, "SORTID", cur_sortId);
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.BeginUpdate();
            st01_ds.St01.Rows.Clear();
            st01_ds.st01Adapter.Fill(st01_ds.St01);
            gridView1.EndUpdate();
        }

        private void BarButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }
         
    }
}
