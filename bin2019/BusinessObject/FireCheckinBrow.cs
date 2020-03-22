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
using JEast.windows;
using Oracle.ManagedDataAccess.Client;
using JEast.Misc;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using JEast.Action;

namespace JEast.BusinessObject
{
    public partial class FireCheckinBrow : BaseBusiness
    {
        Ac01_ds ac01_ds = new Ac01_ds();
        private DataTable shadow_dt = new DataTable();
        private OracleDataAdapter adapter = new OracleDataAdapter("", SqlAssist.conn);

        public FireCheckinBrow()
        {
            InitializeComponent();

            ///////// 装入下拉结果集 ///////////
            //ac01_ds.st01Adapter.Fill(ac01_ds.St01);
            ac01_ds.uc01Adapter.Fill(ac01_ds.Uc01);

            lookup_ac100.DataSource = ac01_ds.Uc01;
            lookup_ac100.DisplayMember = "UC003";
            lookup_ac100.ValueMember = "UC001";
        }

        private void FireCheckinBrow_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = ac01_ds.Ac01;

            //装入数据
            DataFilter(rangeList.EditValue.ToString());
        }
        
        /// <summary>
        /// 过滤数据
        /// </summary>
        /// <param name="action"></param>
        private void DataFilter(string action)
        {
            switch (action)
            {
                case "今日登记":
                    ac01_ds.ac01Adapter.SelectCommand.CommandText = "select * from ac01 where trunc(ac200) = trunc(sysdate) and status <> '0' ";
                    break;
                case "近三日登记":
                    ac01_ds.ac01Adapter.SelectCommand.CommandText = "select * from ac01 where (trunc(sysdate) - trunc(ac200)) <=2 and status <> '0' ";
                    break;
                case "一个月内登记":
                    ac01_ds.ac01Adapter.SelectCommand.CommandText = "select * from ac01 where (trunc(sysdate) - trunc(ac200)) <=30 and status <> '0' ";
                    break;
            }

            gridView1.BeginUpdate();
            ac01_ds.Fill_ac01();
            gridView1.EndUpdate();
        }

        /// <summary>
        /// 进灵登记-新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            Frm_ac01 frm_ac01 = new Frm_ac01();
            frm_ac01.swapdata["action"] = "add";
            frm_ac01.swapdata["dataset"] = this.ac01_ds;
            frm_ac01.swapdata["businessObject"] = this;

            if(frm_ac01.ShowDialog() == DialogResult.OK)
            {
                string s_ac001 = this.swapdata["AC001"].ToString();

                adapter.SelectCommand.CommandText = "select * from ac01 where ac001='" + s_ac001 + "'";
                adapter.Fill(this.shadow_dt);
                ac01_ds.Ac01.Merge(this.shadow_dt); 
            }
        }

        private void GridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "AC002")
            {
                if (e.Value.ToString() == "0")
                    e.DisplayText = "男";
                else
                    e.DisplayText = "女"; 
            }else if(e.Column.FieldName == "AC070")
			{
				if (e.Value.ToString() == "0")
					e.DisplayText = "高档炉";
				else if (e.Value.ToString() == "1")
					e.DisplayText = "普通炉";
				else
					e.DisplayText = "";
			}
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
            int rowHandle = gridView1.FocusedRowHandle;
            if (rowHandle >= 0)
            {
                Modify(rowHandle);
            }
        }

        private void Modify(int rowHandle)
        {
            if (rowHandle < 0) return;

            //权限检查
            //string s_right = Tools.GetRight(Envior.cur_userId, AC077 == "1" ? "01020" : "07020");
            //if (s_right == "0" || (s_right == "1" && gridView1.GetRowCellValue(rowHandle, "AC100").ToString() != Envior.cur_userId))
            //{
            //    MessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}

            Frm_ac01 frm_ac01 = new Frm_ac01();
            frm_ac01.swapdata["action"] = "edit";
            frm_ac01.swapdata["dataset"] = this.ac01_ds;
            frm_ac01.swapdata["businessObject"] = this;

            string s_ac001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
            frm_ac01.swapdata["AC001"] = s_ac001;

            if (frm_ac01.ShowDialog() == DialogResult.OK)
            {
                frm_ac01.Dispose();
                adapter.SelectCommand.CommandText = "select * from ac01 where ac001='" + s_ac001 + "'";
                adapter.Fill(shadow_dt);
                ac01_ds.Ac01.Merge(this.shadow_dt);  
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
                    Modify(gridView1.FocusedRowHandle);
                }
            }
        }

        /// <summary>
        /// 删除登记记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle; ;
            if (rowHandle < 0) return;

            //权限检查
            //string s_right = Tools.GetRight(Envior.cur_userId, AC077 == "1" ? "01030" : "07030");
            //if (s_right == "0" || (s_right == "1" && gridView1.GetRowCellValue(rowHandle, "AC100").ToString() != Envior.cur_userId))
            //{
            //    MessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}

            string s_ac001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
            if (MessageBox.Show("确认要删除登记信息吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return;

            if (FireAction.RemoveFireCheckin(s_ac001, Envior.cur_userId) > 0)
            {
                this.RefreshData();
            }
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void RefreshData()
        {
            DataFilter(rangeList.EditValue.ToString());
        }

        private void BarButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshData();
        }

        private void BarButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            if (rowHandle < 0) return;

            string s_ac001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();

            (Envior.mform as RibbonForm).openBusinessObject("FireBusiness",s_ac001);
        }

        private void RangeList_EditValueChanged(object sender, EventArgs e)
        {
            if (rangeList.EditValue != null && !string.IsNullOrWhiteSpace(rangeList.EditValue.ToString()))
            {
                DataFilter(rangeList.EditValue.ToString());
            }
        }

		private void BarButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int r_row = gridView1.FocusedRowHandle;
			string s_ac001 = gridView1.GetRowCellValue(r_row,"AC001").ToString();
			PrtServAction.Print_HHZM(s_ac001, this.Handle.ToInt32());
		}

		/// <summary>
		/// 补打火化登记单
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			string s_ac001 = string.Empty;
			if(gridView1.FocusedRowHandle >= 0)
			{
				if(gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"AC080") == null)
				{
					MessageBox.Show("当前记录没有火化序号!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					return;
				}

				if (MessageBox.Show("现在打印【火化登记单】吗", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
				s_ac001 = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AC001").ToString();

				PrtServAction.Print_CheckinNotice(s_ac001, Envior.mform.Handle.ToInt32());
			}
		}
	}
}
