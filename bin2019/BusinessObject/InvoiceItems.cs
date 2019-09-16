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
using JEast.DataSet;
using JEast.BaseObject;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using JEast.Misc;

namespace JEast.BusinessObject
{
    public partial class InvoiceItems : BaseBusiness
    {
        In01_ds in01_ds = new In01_ds();
        public InvoiceItems()
        {
            InitializeComponent();
            gridControl1.DataSource = in01_ds.In01;
        }

        private void Roles_Load(object sender, EventArgs e)
        {
            gridView1.ActiveFilter.Clear();
            gridView1.ActiveFilterString = "STATUS <> '0'";

            in01_ds.in01Adapter.Fill(in01_ds.In01);
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
        /// 编辑验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            string colName = (sender as ColumnView).FocusedColumn.FieldName.ToUpper();
            if (colName.Equals("IN003"))
            {
                if (String.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Valid = false;
                    e.ErrorText = "项目名称不能为空!";
                }
                else
                {
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        if (i == (sender as ColumnView).FocusedRowHandle) continue;
                        if (gridView1.GetRowCellValue(i, "IN003") == null) continue;

                        //如果名称相同,则校验不通过!                        
                        if (String.Equals(gridView1.GetRowCellValue(i, "IN003").ToString(), e.Value.ToString()))
                        {
                            e.Valid = false;
                            e.ErrorText = "项目名称已经存在!";
                            break;
                        }
                    }
                }
            }
            else if (colName.Equals("IN002"))
            {
                if (String.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Valid = false;
                    e.ErrorText = "项目代码不能为空!";
                }
                else
                {
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        if (i == (sender as ColumnView).FocusedRowHandle) continue;
                        if (gridView1.GetRowCellValue(i, "IN002") == null) continue;

                        //如果名称相同,则校验不通过!                        
                        if (String.Equals(gridView1.GetRowCellValue(i, "IN002").ToString(), e.Value.ToString()))
                        {
                            e.Valid = false;
                            e.ErrorText = "项目代码已经存在!";
                            break;
                        }
                    }
                }
            }
        }

        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //// 初始化新行时触发(当在新行中)
            GridView view = sender as GridView;
            string in001 = Tools.GetEntityPK("IN01");
            int currow = view.FocusedRowHandle;
            view.SetRowCellValue(e.RowHandle, view.Columns["IN001"], in001);
            view.SetRowCellValue(e.RowHandle, view.Columns["STATUS"], "1");
        }

        /// <summary>
        /// 新增行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.AddNewRow();
            int rowno = gridView1.FocusedRowHandle;
            /////// 设置焦点 开始编辑 !!!
            gridView1.FocusedColumn = gridView1.Columns["IN002"];
            gridView1.ShowEditor();
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.BeginUpdate();
            in01_ds.In01.Rows.Clear();
            in01_ds.in01Adapter.Fill(in01_ds.In01);
            gridView1.EndUpdate();
        }

        /// <summary>
        /// 删除记录
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
        /// 行校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView1_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            string value = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "IN003").ToString();
            if (String.IsNullOrEmpty(value))
            {
                e.Valid = false;
                (sender as ColumnView).SetColumnError(gridView1.Columns["IN003"], "项目名称不能为空!");
            }

            value = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "IN002").ToString();
            if (String.IsNullOrEmpty(value))
            {
                e.Valid = false;
                (sender as ColumnView).SetColumnError(gridView1.Columns["IN002"], "项目代码不能为空!");
            }

        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!gridView1.PostEditor()) return;
            if (!gridView1.UpdateCurrentRow()) return;

            try
            {
                in01_ds.in01Adapter.Update(in01_ds.In01);                 
                MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
