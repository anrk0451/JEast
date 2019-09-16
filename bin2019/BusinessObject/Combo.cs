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
using DevExpress.XtraGrid.Views.Grid;
using JEast.Misc;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using System.Reflection;
using DevExpress.Data.Filtering;

namespace JEast.BusinessObject
{
    public partial class Combo : BaseBusiness
    {
        private Dictionary<string, string> cb002_source = new Dictionary<string, string>();  //套餐类别数据源
        Cb01_ds combo_ds = new Cb01_ds();
        //OracleParameter op_cb001 = null;


        public Combo()
        {
            InitializeComponent();
            ///初始化套餐类别数据源
            cb002_source.Add("0", "服务绑定套餐");
            cb002_source.Add("1", "用户定义套餐");

            //op_cb001 = new OracleParameter("st002", OracleDbType.Varchar2, 15);
            //op_cb001.Direction = ParameterDirection.Input;
            //combo_ds.cb02Adapter.SelectCommand.Parameters.Add(op_cb001);
        }

        private void Combo_Load(object sender, EventArgs e)
        {
            ///设置数据源
            gridControl1.DataSource = combo_ds.Cb01;
            gridControl2.DataSource = combo_ds.Cb02;
 
            repository_cb002.DataSource = cb002_source;
            repository_cb002.PopulateColumns();
            repository_cb002.ShowHeader = false;

            repository_cb005.DataSource = combo_ds.BindingService;
            repository_cb005.DisplayMember = "SERVICENAME";
            repository_cb005.ValueMember = "SERVICEID";
            repository_cb005.PopulateColumns();
            repository_cb005.ShowHeader = false;

            combo_ds.allItemAdapter.Fill(combo_ds.AllItem);
            repository_cb021.DataSource = combo_ds.AllItem;
            repository_cb021.DisplayMember = "ITEM_TEXT";
            repository_cb021.ValueMember = "ITEM_ID";

            repository_cb021.View.OptionsView.AllowCellMerge = true;
            //repository_cb021.PopulateViewColumns();

            GridColumn col_itemid = repository_cb021.View.Columns.AddField("ITEM_ID");
            col_itemid.Visible = false;

            GridColumn col_itemtype = repository_cb021.View.Columns.AddField("ITEM_TYPE_TEXT");
            col_itemtype.Caption = "类别";
            col_itemtype.VisibleIndex = 0;
            col_itemtype.Width = 50;
            col_itemtype.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;

            GridColumn col_itemtext = repository_cb021.View.Columns.AddField("ITEM_TEXT");
            col_itemtext.Caption = "名称";
            col_itemtext.VisibleIndex = 1;
            col_itemtext.Width = 50;

            GridColumn col_price = repository_cb021.View.Columns.AddField("PRICE");
            col_price.Caption = "单价";
            col_price.VisibleIndex = 2;
            col_price.Width = 50;
            col_price.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            col_price.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            col_price.DisplayFormat.FormatString = "N2";

            GridColumn col_zjf = repository_cb021.View.Columns.AddField("ZJF");
            col_zjf.Caption = "助记符";
            col_zjf.VisibleIndex = 3;
            col_zjf.Width = 50;



            //装入数据
            combo_ds.cb01Adapter.Fill(combo_ds.Cb01);
            combo_ds.cb02Adapter.Fill(combo_ds.Cb02);
            combo_ds.bindingAdapter.Fill(combo_ds.BindingService);
            
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
            gridView1.FocusedColumn = gridView1.Columns["CB003"];
            gridView1.ShowEditor();
        }

        /// <summary>
        /// 行初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            string cb001 = Tools.GetEntityPK("CB01");
            int currow = view.FocusedRowHandle;
            view.SetRowCellValue(e.RowHandle, view.Columns["CB001"], cb001);
            view.SetRowCellValue(e.RowHandle, view.Columns["STATUS"], "1");
 
        }

        private void GridView1_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (!gridView2.PostEditor() || !gridView1.UpdateCurrentRow())
            {
                e.Allow = false;
            }
        }

        /// <summary>
        /// 套餐类型改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "CB002")  //套餐类别
            {
                if (e.Value.ToString() == "1")
                {
                    (sender as ColumnView).SetRowCellValue(e.RowHandle, "CB005", null);
                }
            }
        }

        /// <summary>
        /// 套餐 行焦点改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            int rowHandle = e.FocusedRowHandle;
            string s_filter = null;
            object s_cb001 = gridView1.GetRowCellValue(rowHandle, "CB001");

            gridView2.BeginUpdate();
            gridView2.ActiveFilter.Clear();
            if (s_cb001 != null && !(s_cb001 is System.DBNull))
            {
                s_filter = "[CB001]='" + s_cb001 + "'";
            }
            else
            {
                s_filter = "[CB001]='0000000000'";
            }
            gridView2.ActiveFilter.Add(gridView2.Columns["CB001"], new ColumnFilterInfo(s_filter));
            gridView2.EndUpdate();
        }

        private void GridView1_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            DataRowView rowdata = e.Row as DataRowView;
            if (rowdata == null) return;
            if (rowdata["CB002"] != null && rowdata["CB002"].ToString() == "0" && rowdata["CB005"] is System.DBNull)
            {
                e.Valid = false;
                e.ErrorText = "关联服务必须选择!";
            }
            else if (rowdata["CB003"] == null || string.IsNullOrEmpty(rowdata["CB003"].ToString()))
            {
                e.Valid = false;
                e.ErrorText = "套餐名称必须输入!";
            }
            else if (rowdata["CB002"] == null || rowdata["CB002"] is System.DBNull)
            {
                e.Valid = false;
                e.ErrorText = "套餐类别必须输入!";
            }
        }

        private void GridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            string colName = (sender as ColumnView).FocusedColumn.FieldName.ToUpper();
            if (colName.Equals("CB003"))
            {
                if (String.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Valid = false;
                    e.ErrorText = "套餐名称不能为空!";
                }
                else
                {
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        if (i == (sender as ColumnView).FocusedRowHandle) continue;
                        if (gridView1.GetRowCellValue(i, "CB003") == null) continue;

                        //如果名字相同,则校验不通过!                        
                        if (String.Equals(gridView1.GetRowCellValue(i, "CB003").ToString(), e.Value.ToString()))
                        {
                            e.Valid = false;
                            e.ErrorText = "名称已经存在!";
                            break;
                        }
                    }
                }
            }
        }

        private void GridView2_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            int combo_handle = gridView1.FocusedRowHandle;
            object s_cb001 = gridView1.GetRowCellValue(combo_handle, "CB001");

            if (s_cb001 != null && !(s_cb001 is System.DBNull))
            {
                gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "CB001", s_cb001.ToString());
            }

            gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "CB201", Tools.GetEntityPK("CB01"));
            gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "CB030", 1);
        }

        private void GridControl2_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if(e.Button.ButtonType == NavigatorButtonType.Append)
            {
                int row = gridView1.FocusedRowHandle;
                if (row < 0 || gridView1.GetRowCellValue(row, "CB001") == null)
                {
                    e.Handled = true;
                    return;
                }


                gridView2.AddNewRow();
                int rowno = gridView1.FocusedRowHandle;
                /////// 设置焦点 开始编辑 !!!
                gridView2.FocusedColumn = gridView2.Columns["CB021"];
                gridView2.ShowEditor();
                e.Handled = true;
            }else if(e.Button.ButtonType == NavigatorButtonType.Remove)
            {
                DialogResult result = MessageBox.Show("确认删除?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    e.Handled = true;
                    return;
                }
                gridView2.DeleteRow(gridView2.FocusedRowHandle);              
                e.Handled = true;
            }
        }

        private void GridView2_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            DataRowView rowdata = e.Row as DataRowView;
            if (rowdata == null) return;

            if (rowdata["CB021"] is System.DBNull  )
            {
                e.ErrorText = "请选择项目!";
                e.Valid = false;
                return;
            }
            else if (rowdata["CB030"] is System.DBNull )
            {
                e.ErrorText = "请输入数量!";
                e.Valid = false;
                return;

            }
            else if (decimal.Parse(rowdata["CB030"].ToString()) <= 0)
            {
                e.ErrorText = "数量必须大于0!";
                e.Valid = false;
                return;
            }
        }

        private void GridView2_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            int rowHandle = view.FocusedRowHandle;
            string colname = view.FocusedColumn.FieldName;

            if (colname == "CB030")
            {
                if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.ErrorText = "数量必须输入!";
                    e.Valid = false;
                }
            }
            else if (colname == "CB021")  //项目名
            {
                if (e.Value == null || string.IsNullOrWhiteSpace(e.Value.ToString())) return;
                for (int i = 0; i < view.RowCount - 1; i++)
                {
                    if (i == rowHandle) continue;
                    if (gridView2.GetRowCellValue(i, "CB021") == null || gridView2.GetRowCellValue(i, "CB021") is System.DBNull || gridView2.GetRowCellValue(i, "CB021") == null) continue;

                    //如果相同,则校验不通过!                        
                    if (String.Equals(gridView2.GetRowCellValue(i, "CB021").ToString(), e.Value.ToString()))
                    {
                        e.Valid = false;
                        e.ErrorText = "项目已经存在!";
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 删除套餐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            /////  删除记录 
            if (rowHandle >= 0)
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
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.RowCount == 0 && gridView2.RowCount == 0) return;
            if (!gridView1.PostEditor() || !gridView2.PostEditor()) return;
            if (!gridView1.UpdateCurrentRow() || !gridView2.UpdateCurrentRow()) return;

            //检查套餐明细
            for(int i = 0; i < gridView2.RowCount; i++ )
            {
                //if(gridView1.GetRowCellValue(i,"CB001") == null)
                //{
                //    MessageBox.Show("请输入套餐信息!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                //    return;
                //}
            }


            OracleTransaction trans = SqlAssist.conn.BeginTransaction();

            try
            {
                combo_ds.cb01Adapter.Update(combo_ds.Cb01);
                combo_ds.cb02Adapter.Update(combo_ds.Cb02); 

                trans.Commit();
                MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ee)
            {
                trans.Rollback();
                MessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BarButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshData();
        }

        private void RefreshData()
        {
            gridView1.BeginUpdate();
            gridView2.BeginUpdate();

            combo_ds.Cb01.Rows.Clear();
            combo_ds.Cb02.Rows.Clear();

            combo_ds.cb01Adapter.Fill(combo_ds.Cb01);
            combo_ds.cb02Adapter.Fill(combo_ds.Cb02); 

            gridView1.EndUpdate();
            gridView2.EndUpdate();
        }

        private void FilterLookup(object sender)
        {
            Text += " ! ";
            GridLookUpEdit edit = sender as GridLookUpEdit;
            GridView gridView = edit.Properties.View;
            FieldInfo fi = gridView.GetType().GetField("extraFilter", BindingFlags.NonPublic | BindingFlags.Instance);
            Text = edit.AutoSearchText;
            BinaryOperator op1 = new BinaryOperator("ITEM_TEXT", edit.AutoSearchText + "%", BinaryOperatorType.Like);
            BinaryOperator op2 = new BinaryOperator("ZJF", edit.AutoSearchText + "%", BinaryOperatorType.Like);
            string filterCondition = new GroupOperator(GroupOperatorType.Or, new CriteriaOperator[] { op1, op2 }).ToString();
            fi.SetValue(gridView, filterCondition);

            MethodInfo mi = gridView.GetType().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic | BindingFlags.Instance);
            mi.Invoke(gridView, null);
        }

        private void Repository_cb021_Popup(object sender, EventArgs e)
        {
            FilterLookup(sender);
        }

        private void Repository_cb021_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            this.BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate
            {
                FilterLookup(sender);
            }));
        }

        private void GridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            if (view.FocusedColumn.FieldName == "CB005")
            {
                object cb002 = view.GetRowCellValue(view.FocusedRowHandle, "CB002");
                if (cb002 != null && cb002.ToString() == "1")
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
