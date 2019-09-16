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
	public partial class ServiceProduct : BaseBusiness
	{
		int curIndex = 0;
		Boolean LOCK = true;
        GridView gv = null;

        ServiceProduct_ds sp_ds = new ServiceProduct_ds();
		OracleParameter op_si002 = null;
        OracleParameter op_gi002 = null;

		public ServiceProduct()
		{
			InitializeComponent();
		}

		private void DataDict_Load(object sender, EventArgs e)
		{
            gridControl1.DataSource = sp_ds.Si01;
            gridControl2.DataSource = sp_ds.Gi01;
  
            ///查询参数
            op_si002 = new OracleParameter("si002", OracleDbType.Varchar2, 15);
			op_si002.Direction = ParameterDirection.Input;
			sp_ds.si01Adapter.SelectCommand.Parameters.Add(op_si002);

            op_gi002 = new OracleParameter("gi002", OracleDbType.Varchar2, 15);
            op_gi002.Direction = ParameterDirection.Input;
            sp_ds.gi01Adapter.SelectCommand.Parameters.Add(op_gi002);


            //设置初始选择
            imageListBoxControl1.SetSelected(0, true);
			curIndex = 0;
 
            //设置自动过滤(过滤掉删除行:此操作应该在数据集装入数据后)
            gridView1.ActiveFilter.Clear();
            gridView1.ActiveFilterString =  "STATUS <> '0'";
            gridView2.ActiveFilter.Clear();
            gridView2.ActiveFilterString = "STATUS <> '0'";

            /// 设置排序列
            gridView1.Columns["SORTID"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            gridView2.Columns["SORTID"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
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
        /// 绘制行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView2_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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
            gv.AddNewRow();
            int rowno = gv.FocusedRowHandle;

            /////// 设置焦点 开始编辑 !!!
            if (curIndex <= 6)
                gv.FocusedColumn = gv.Columns["SI003"];
            else
                gv.FocusedColumn = gv.Columns["GI003"];

            gv.ShowEditor();
        }

		private void ImageListBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
            curIndex = imageListBoxControl1.SelectedIndex;
            if (curIndex <= 3)
            {
                gv = gridView1;
            }
            else
            {
                gv = gridView2;
            }


            if (((sp_ds.Si01 as DataTable).GetChanges() != null || (sp_ds.Gi01 as DataTable).GetChanges() != null) && LOCK)
            {
                if (MessageBox.Show("数据已经改变,是否保存?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (Save())
                    {
                        curIndex = imageListBoxControl1.SelectedIndex;
                        if (curIndex <= 3)
                        {
                            gridControl1.Visible = true;
                            gridControl2.Visible = false;

                            gv = gridView1;
                            op_si002.Value = GetCurTypeStr(curIndex);
                            sp_ds.Fill_Si01();
                            //sp_ds.si01Adapter.Fill(sp_ds.Si01);                            
                        }
                        else
                        {
                            gridControl1.Visible = false;
                            gridControl2.Visible = true;

                            gv = gridView2;
                            op_gi002.Value = GetCurTypeStr(curIndex);
                            sp_ds.Fill_Gi01();
                            //sp_ds.gi01Adapter.Fill(sp_ds.Gi01);
                        }
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
                    if (curIndex <= 3)
                    {
                        gridControl1.Visible = true;
                        gridControl2.Visible = false;

                        gv = gridView1;
                        op_si002.Value = GetCurTypeStr(curIndex);
                        sp_ds.Fill_Si01();
                        //sp_ds.si01Adapter.Fill(sp_ds.Si01);                        
                    }
                    else
                    {
                        gridControl1.Visible = false;
                        gridControl2.Visible = true;

                        gv = gridView2;
                        op_gi002.Value = GetCurTypeStr(curIndex);
                        sp_ds.Fill_Gi01();
                        //sp_ds.gi01Adapter.Fill(sp_ds.Gi01);
                    }
                }

            }
            else if (LOCK)
            {
                curIndex = imageListBoxControl1.SelectedIndex;
                if (curIndex <= 3)
                {
                    gridControl1.Visible = true;
                    gridControl2.Visible = false;

                    gv = gridView1;
                    op_si002.Value = GetCurTypeStr(curIndex);
                    sp_ds.Fill_Si01();
                    //sp_ds.si01Adapter.Fill(sp_ds.Si01);
                }
                else
                {
                    gridControl1.Visible = false;
                    gridControl2.Visible = true;

                    gv = gridView2;
                    op_gi002.Value = GetCurTypeStr(curIndex);
                    sp_ds.Fill_Gi01();
                    //sp_ds.gi01Adapter.Fill(sp_ds.Gi01);
                }
            }
            else
            {
                LOCK = true;
            }
			////////////// 设置 税务 可编辑性  /////////////
			this.SetTaxEditable();
        }


		/// <summary>
		/// 设置税务信息 可编辑性
		/// </summary>
		private void SetTaxEditable()
		{
			if(curIndex == 0 || curIndex == 1 || curIndex == 2 || curIndex == 3 || curIndex == 4 || curIndex == 5)
			{
				gridView1.Columns["TX000"].OptionsColumn.AllowEdit = true;
				gridView1.Columns["TX001"].OptionsColumn.AllowEdit = true;
				gridView1.Columns["TX005"].OptionsColumn.AllowEdit = true;
				gridView1.Columns["TX003"].OptionsColumn.AllowEdit = true;
			}
			else
			{
				gridView1.Columns["TX000"].OptionsColumn.AllowEdit = true;
				gridView1.Columns["TX001"].OptionsColumn.AllowEdit = true;
				gridView1.Columns["TX005"].OptionsColumn.AllowEdit = true;
				gridView1.Columns["TX003"].OptionsColumn.AllowEdit = true;
			}
		}

		////保存过程 
		private Boolean Save()
		{
			gv.ClearColumnErrors();
            if (!gv.PostEditor()) return false;
            if (!gv.UpdateCurrentRow()) return false;

			///检查税率、映射名 ///////////////////////
			for (int i = 0; i < gv.RowCount; i++)
			{
				if (gv.GetRow(i) == null) continue;
				//if(gv.GetRowCellValue(i,"TX000").ToString() == "0")  //不免税
				//{
				//	if (string.IsNullOrEmpty(gv.GetRowCellValue(i, "TX003").ToString()))
				//	{
				//		gv.FocusedRowHandle = i;
				//		gv.SetColumnError(gv.Columns["TX003"],"请输入【税务发票-映射名称】!");
				//		return false;
				//	}
				//	if (string.IsNullOrEmpty(gv.GetRowCellValue(i, "TX001").ToString()))
				//	{
				//		gv.FocusedRowHandle = i;
				//		gv.SetColumnError(gv.Columns["TX001"], "请输入【税务发票编码】!");
				//		return false;
				//	}
				//	if (decimal.Parse(gv.GetRowCellValue(i,"TX005").ToString()) <= 0)
				//	{
				//		gv.FocusedRowHandle = i;
				//		gv.SetColumnError(gv.Columns["TX005"], "请输入【税率】!");
				//		return false;
				//	}
				//}
			}
			///////////////////////////////////////////

			try
            {
                if (curIndex <= 3)
                    sp_ds.si01Adapter.Update(sp_ds.Si01);
                else
                    sp_ds.gi01Adapter.Update(sp_ds.Gi01);

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
                //case 0:  //守灵厅
                //    result = "01";
                //    break;
                //case 1:  //冷藏柜
                //    result = "02";
                //    break;
                case 0:  //休息室
                    result = "03";
                    break;
                //case 3:  //告别厅
                //    result = "04";
                //    break;
                case 1:  //火化
                    result = "06";
                    break;
                case 2:  //灵车
                    result = "07";
                    break;
                case 3:  //殡仪服务
                    result = "05";
                    break;
                case 4:  //谷类
                    result = "10";
                    break;
                case 5:  //纸类
                    result = "11";
                    break;
                case 6:  //祭品
                    result = "12";
                    break;
                case 7: //寄存附品
                    result = "13";
                    break;
            }
            return result;
        }



		//初始化新行
		private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
		{
            string newkey = string.Empty;
            newkey = Tools.GetEntityPK("SI01");
            gridView1.SetRowCellValue(e.RowHandle, "SI002", GetCurTypeStr(curIndex));
            gridView1.SetRowCellValue(e.RowHandle, "SI001", newkey);
            gridView1.SetRowCellValue(e.RowHandle, "SI005", "0");   //占用标志
            gridView1.SetRowCellValue(e.RowHandle, "STATUS", "1");
            gridView1.SetRowCellValue(e.RowHandle, "PRICE", 0.00);
            gridView1.SetRowCellValue(e.RowHandle, "SORTID", Convert.ToInt32(newkey));
			////四项基本服务免费!
			if(curIndex == 0 || curIndex == 1 || curIndex == 2 /*|| curIndex == 3 || curIndex == 4 || curIndex == 5*/)
			{
				gridView1.SetRowCellValue(e.RowHandle, "TX000", "1");
				gridView1.SetRowCellValue(e.RowHandle, "TX001", "");
				gridView1.SetRowCellValue(e.RowHandle, "TX003", "");
				gridView1.SetRowCellValue(e.RowHandle, "TX005", "0");
			}
			else
			{
				gridView1.SetRowCellValue(e.RowHandle, "TX000", "0");
				gridView1.SetRowCellValue(e.RowHandle, "TX005", "0");
			}
 

		}

        /// <summary>
        /// 新行初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView2_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            string newkey = string.Empty;
            newkey = Tools.GetEntityPK("GI01");
            gridView2.SetRowCellValue(e.RowHandle, "GI002", GetCurTypeStr(curIndex));
            gridView2.SetRowCellValue(e.RowHandle, "GI001", newkey);
            gridView2.SetRowCellValue(e.RowHandle, "STATUS", "1");
            gridView2.SetRowCellValue(e.RowHandle, "PRICE", 0.00);
            gridView2.SetRowCellValue(e.RowHandle, "SORTID", Convert.ToInt32(newkey));
			gridView2.SetRowCellValue(e.RowHandle, "TX000", "0");
			gridView2.SetRowCellValue(e.RowHandle, "TX005", "0");
		}



 
        private void GridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            string colName = (sender as ColumnView).FocusedColumn.FieldName.ToUpper();
            if (colName.Equals("SI003"))       //服务名称
            {
                if (String.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Valid = false;
                    e.ErrorText = "服务项目名称不能为空!";
                }
                else
                {
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        if (i == (sender as ColumnView).FocusedRowHandle) continue;
                        if (gridView1.GetRowCellValue(i, "SI003") == null) continue;

                        //如果名字相同,则校验不通过!                        
                        if (String.Equals(gridView1.GetRowCellValue(i, "SI003").ToString(), e.Value.ToString()))
                        {
                            e.Valid = false;
                            e.ErrorText = "名称已经存在!";
                            break;
                        }
                    }
                }
            }
            else if (colName.Equals("PRICE"))   //单价
            {
                if (Decimal.Parse(e.Value.ToString()) < 0)
                {
                    e.Valid = false;
                    e.ErrorText = "单价不能小于0!";
                }
            }
			else if(colName.Equals("TX005"))    //税率
			{
				int rowHandle = gridView1.FocusedRowHandle;
				if(gridView1.GetRowCellValue(rowHandle,"TX000").ToString() == "0")
				{
					if(Decimal.Parse(e.Value.ToString()) <=0)
					{
						e.Valid = false;
						e.ErrorText = "税率必须大于0!";
					}
				}
			}
        }

        /// <summary>
        /// 单元格编辑验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView2_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            string colName = (sender as ColumnView).FocusedColumn.FieldName.ToUpper();
            if (colName.Equals("GI003"))       //项目名称
            {
                if (String.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Valid = false;
                    e.ErrorText = "项目名称不能为空!";
                }
                else
                {
                    for (int i = 0; i < gridView2.RowCount - 1; i++)
                    {
                        if (i == (sender as ColumnView).FocusedRowHandle) continue;
                        if (gridView2.GetRowCellValue(i, "GI003") == null) continue;

                        //如果名字相同,则校验不通过!                        
                        if (String.Equals(gridView2.GetRowCellValue(i, "GI003").ToString(), e.Value.ToString()))
                        {
                            e.Valid = false;
                            e.ErrorText = "名称已经存在!";
                            break;
                        }
                    }
                }
            }
            else if (colName.Equals("PRICE"))   //单价
            {
                if (Decimal.Parse(e.Value.ToString()) < 0)
                {
                    e.Valid = false;
                    e.ErrorText = "单价不能小于0!";
                }
            }
			else if (colName.Equals("TX005"))    //税率
			{
				int rowHandle = gridView2.FocusedRowHandle;
				if (gridView2.GetRowCellValue(rowHandle, "TX000").ToString() == "0")
				{
					if (Decimal.Parse(e.Value.ToString()) <= 0)
					{
						e.Valid = false;
						e.ErrorText = "税率必须大于0!";
					}
				}
			}
		}

        /// <summary>
        /// 行验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            string value = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SI003").ToString();
            if (String.IsNullOrEmpty(value))
            {
                e.Valid = false;
                (sender as ColumnView).SetColumnError(gridView1.Columns["SI003"], "名称不能为空!");
            }
        }

        /// <summary>
        /// 行验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView2_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            string value = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "GI003").ToString();
            if (String.IsNullOrEmpty(value))
            {
                e.Valid = false;
                (sender as ColumnView).SetColumnError(gridView2.Columns["GI003"], "名称不能为空!");
            }
        }



        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gv.FocusedRowHandle >= 0)
            {
                if (MessageBox.Show("确认要删除当前的记录吗", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }

            }
            gv.SetFocusedRowCellValue("STATUS", "0");
            gv.UpdateCurrentRow();
        }

        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int row = gv.FocusedRowHandle;
            if (row <= 0) return;

            int prior_sortId = int.Parse(gv.GetRowCellValue(row - 1, "SORTID").ToString());
            int cur_sortId = int.Parse(gv.GetRowCellValue(row, "SORTID").ToString());

            gv.SetRowCellValue(row, "SORTID", prior_sortId);
            gv.SetRowCellValue(row - 1, "SORTID", cur_sortId);
        }

        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int row = gv.FocusedRowHandle;
            if ((row >= gv.RowCount - 2) || row < 0) return;

            int next_sortId = int.Parse(gv.GetRowCellValue(row + 1, "SORTID").ToString());
            int cur_sortId = int.Parse(gv.GetRowCellValue(row, "SORTID").ToString());

            gv.SetRowCellValue(row, "SORTID", next_sortId);
            gv.SetRowCellValue(row + 1, "SORTID", cur_sortId);
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshData();
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void RefreshData()
        {
            if (curIndex <= 6)
            {
                gridView1.BeginUpdate();
                sp_ds.Si01.Rows.Clear();
                op_si002.Value = GetCurTypeStr(curIndex);
                sp_ds.Fill_Si01();
                //sp_ds.si01Adapter.Fill(sp_ds.Si01);             
                gridView1.EndUpdate();
            }
            else
            {
                gridView2.BeginUpdate();
                sp_ds.Gi01.Rows.Clear();
                op_gi002.Value = GetCurTypeStr(curIndex);
                sp_ds.Fill_Gi01();
                //sp_ds.gi01Adapter.Fill(sp_ds.Gi01);
                gridView2.EndUpdate();
            }
        }

        private void BarButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        /// <summary>
        /// 设置助记符
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SI003")
            {
                if (e.Value != System.DBNull.Value)
                {
                    gridView1.SetFocusedRowCellValue("SI088", Tools.GetPYString(e.Value.ToString().Trim()));
					if(gridView1.GetRowCellValue(e.RowHandle,"TX000").ToString() == "0" && gridView1.GetRowCellValue(e.RowHandle, "TX003") != null)  //不免税
					{
						gridView1.SetRowCellValue(e.RowHandle, "TX003", e.Value);
					}
                }
                else
                {
                    gridView1.SetFocusedRowCellValue("SI088", System.DBNull.Value);
                }
            }else if(e.Column.FieldName == "TX000")
			{	//免税
				if(e.Value.ToString() == "1")
				{
					gridView1.SetRowCellValue(e.RowHandle,"TX003","");
					gridView1.SetRowCellValue(e.RowHandle, "TX005", 0);
				}//不免税
				else
				{
					if(gridView1.GetRowCellValue(e.RowHandle,"TX003") != null)
					{
						gridView1.SetRowCellValue(e.RowHandle, "TX003", gridView1.GetRowCellValue(e.RowHandle, "SI003"));
					}
				}
			}
        }

        private void GridView2_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "GI003")
            {
                if (e.Value != System.DBNull.Value)
                {
                    gridView2.SetFocusedRowCellValue("GI088", Tools.GetPYString(e.Value.ToString().Trim()));
					if (gridView2.GetRowCellValue(e.RowHandle, "TX000").ToString() == "0" && gridView2.GetRowCellValue(e.RowHandle, "TX003") != null)  //不免税
					{
						gridView2.SetRowCellValue(e.RowHandle, "TX003", e.Value);
					}
				}
                else
                {
                    gridView2.SetFocusedRowCellValue("GI088", System.DBNull.Value);
                }
            }
			else if (e.Column.FieldName == "TX000")
			{   //免税
				if (e.Value.ToString() == "1")
				{
					gridView2.SetRowCellValue(e.RowHandle, "TX003", "");
					gridView2.SetRowCellValue(e.RowHandle, "TX005", 0);
				}//不免税
				else
				{
					if (gridView2.GetRowCellValue(e.RowHandle, "TX003") != null)
					{
						gridView2.SetRowCellValue(e.RowHandle, "TX003", gridView1.GetRowCellValue(e.RowHandle, "GI003"));
					}
				}
			}
		}

		private void BarButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			for (int i = 0; i < gridView2.RowCount; i++)
			{
				gridView2.SetRowCellValue(i, "GI088", Tools.GetPYString(gridView2.GetRowCellValue(i, "GI003").ToString()));
			}
		}
	}
}
