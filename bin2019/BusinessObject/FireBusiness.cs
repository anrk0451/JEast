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
using JEast.Action;
using JEast.windows;
using JEast.Misc;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid;

namespace JEast.BusinessObject
{
    public partial class FireBusiness : BaseBusiness
    {
        Sa01_ds sa01_ds = new Sa01_ds();
        string AC001 = string.Empty;

        OracleParameter parm1 = new OracleParameter("ac001", OracleDbType.Varchar2, 10);
        OracleParameter parm2 = new OracleParameter("ac001", OracleDbType.Varchar2, 10);

        public FireBusiness()
        {
            InitializeComponent();
        }

        private void FireBusiness_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = sa01_ds.Sa01;
 
            parm1.Direction = ParameterDirection.Input;
            parm2.Direction = ParameterDirection.Input;

            sa01_ds.sa01Adapter.SelectCommand.Parameters.Add(parm1);
            sa01_ds.ac01Adapter.SelectCommand.Parameters.Add(parm2);

            //this.Business_Init();

            //与逝者关系
            lookup_ac052.Properties.DataSource = sa01_ds.St01_relation;
            lookup_ac052.Properties.DisplayMember = "ST003";
            lookup_ac052.Properties.ValueMember = "ST001";

            //经办人
            lookup_sa100.DataSource = sa01_ds.Uc01;


            //守灵厅
            lookup_store.Properties.DataSource = sa01_ds.Si01;
            //告别厅
            lookUp_gbt.Properties.DataSource = sa01_ds.Si01;
 
        }

        public override void Business_Init()
        {
            //获取逝者编号
            AC001 = this.swapdata["parm"].ToString();

            //填充逝者个人信息
            parm1.Value = AC001;
            parm2.Value = AC001;

            sa01_ds.Ac01.Rows.Clear();
            sa01_ds.ac01Adapter.Fill(sa01_ds.Ac01);
 

            if (sa01_ds.Ac01.Rows.Count <= 0)
            {
                MessageBox.Show("参数传递错误!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtedit_ac001.EditValue = AC001;
            txtedit_ac003.EditValue = sa01_ds.Ac01.Rows[0]["AC003"];
            txtedit_ac004.EditValue = sa01_ds.Ac01.Rows[0]["AC004"];
            rg_ac002.EditValue = sa01_ds.Ac01.Rows[0]["AC002"];
            txtedit_ac020.EditValue = sa01_ds.Ac01.Rows[0]["AC020"];  //到达中心时间
            txtedit_ac050.EditValue = sa01_ds.Ac01.Rows[0]["AC050"];  //联系人
            txtedit_ac051.EditValue = sa01_ds.Ac01.Rows[0]["AC051"];
            lookup_ac052.EditValue = sa01_ds.Ac01.Rows[0]["AC052"];

            this.Parent.Text = "火化业务办理" + "【" + sa01_ds.Ac01.Rows[0]["AC003"] + "】";
 

            ///刷新销售数据
            this.RefreshSalesData();
        }

        /// <summary>
        /// 刷新销售数据
        /// </summary>
        public void RefreshSalesData()
        {
            gridView1.BeginUpdate();
            sa01_ds.Sa01.Rows.Clear();
            sa01_ds.sa01Adapter.Fill(sa01_ds.Sa01);
            gridView1.EndUpdate();

            this.RefreshPanel();
        }

        /// <summary>
        /// 刷新业务显示面板
        /// </summary>
        private void RefreshPanel()
        {
            int rowHandle = int.MinValue;
            //守灵厅!
            rowHandle = gridView1.LocateByValue("SA002", "01");
            if (rowHandle >= 0)
            {
                lookup_store.EditValue = gridView1.GetRowCellValue(rowHandle, "SA004");
            }
            //冷藏柜
            rowHandle = gridView1.LocateByValue("SA002", "02");
            if (rowHandle >= 0)
            {
                lookup_store.EditValue = gridView1.GetRowCellValue(rowHandle, "SA004");
            }
            //休息室
            txtedit_xxs.EditValue = FireAction.GetRestRoomList(AC001);

            //告别厅
            rowHandle = gridView1.LocateByValue("SA002", "04");
            if (rowHandle >= 0)
            {
                lookUp_gbt.EditValue = gridView1.GetRowCellValue(rowHandle, "SA004");
            }

            //告别时间
            txtedit_ac018.EditValue = FireAction.GetGBTime(AC001);
            //火化时间
            txtedit_ac015.EditValue = FireAction.GetHHTime(AC001);

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
        /// 守灵厅办理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (FireAction.FireIsSettled(AC001) == "1")
            {
                MessageBox.Show("已经办理火化且结算完成,不能继续办理业务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //检查是否已有
            if (gridView1.LocateByValue("SA002", "01") >= 0 || gridView1.LocateByValue("SA002", "02") >= 0)
            {
                MessageBox.Show("已经办理守灵或冷藏业务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Frm_business01 frm_slt = new Frm_business01();
            frm_slt.swapdata["dataset"] = this.sa01_ds;
            frm_slt.swapdata["AC001"] = AC001;

            if (frm_slt.ShowDialog() == DialogResult.OK)
            {
                RefreshSalesData();
            }
        }

		/// <summary>
		/// 冷藏办理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (FireAction.FireIsSettled(AC001) == "1")
			{
				MessageBox.Show("已经办理火化且结算完成,不能继续办理业务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			//检查是否已有
			if (gridView1.LocateByValue("SA002", "01") >= 0 || gridView1.LocateByValue("SA002", "02") >= 0)
			{
				MessageBox.Show("已经办理守灵或冷藏业务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			Frm_business02 frm_lcg = new Frm_business02();
			frm_lcg.swapdata["dataset"] = sa01_ds;
			frm_lcg.swapdata["AC001"] = AC001;

			if (frm_lcg.ShowDialog() == DialogResult.OK)
			{
				RefreshSalesData();
			}
		}

		/// <summary>
		/// 标志转换
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			if (e.Column.FieldName == "SA008")
			{
				if (e.Value.ToString() == "1")
					e.DisplayText = "已结算";
				else
					e.DisplayText = "未结算"; 
			}
		}

		/// <summary>
		/// 休息室办理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (FireAction.FireIsSettled(AC001) == "1")
			{
				MessageBox.Show("已经办理火化且结算完成,不能继续办理业务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			Frm_business03 frm_xxs = new Frm_business03();
			frm_xxs.swapdata["businessObject"] = this;
			frm_xxs.swapdata["dataset"] = sa01_ds;
			frm_xxs.swapdata["AC001"] = AC001;

			if (frm_xxs.ShowDialog() == DialogResult.OK)
			{
				List<string> itemIdList = this.swapdata["xxs"] as List<string>;
				int result = 0;
				foreach (string s in itemIdList)
				{
					result = FireAction.FireSales_03(AC001,
													 s,
													 Envior.cur_userId
					);
				}
				RefreshSalesData();
			}
		}

		private void BarButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (FireAction.FireIsSettled(AC001) == "1")
			{
				MessageBox.Show("已经办理火化且结算完成,不能继续办理业务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			//检查是否已有
			int row = gridView1.LocateByValue("SA002", "04");
			if (row >= 0)
			{
				if (gridView1.GetRowCellValue(row, "SA008").ToString() == "1")  //已经结算
				{
					MessageBox.Show("告别已经办理且已结算!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (MessageBox.Show("已经办理告别业务,是否替换?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					return;
			}

			Frm_business04 frm_gbt = new Frm_business04();
			frm_gbt.swapdata["dataset"] = sa01_ds;
			frm_gbt.swapdata["AC001"] = AC001;

			if (frm_gbt.ShowDialog() == DialogResult.OK)
			{
				RefreshSalesData();
			}
		}

		private void BarButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (FireAction.FireIsSettled(AC001) == "1")
			{
				MessageBox.Show("已经办理火化且结算完成,不能继续办理业务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			//检查是否已有
			int row = gridView1.LocateByValue("SA002", "07");
			if (row >= 0)
			{
				if (gridView1.GetRowCellValue(row, "SA008").ToString() == "1")  //已经结算
				{
					MessageBox.Show("灵车已经办理且已结算!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (MessageBox.Show("已经办理灵车业务,是否替换?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					return;
			}
			Frm_business07 frm_lc = new Frm_business07();
			frm_lc.swapdata["dataset"] = sa01_ds;
			frm_lc.swapdata["AC001"] = AC001;

			if (frm_lc.ShowDialog() == DialogResult.OK)
			{
				RefreshSalesData();
			}
		}

		/// <summary>
		/// 火化办理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//检查是否已有
			int row = gridView1.LocateByValue("SA002", "06");
			if (row >= 0)
			{
				if (gridView1.GetRowCellValue(row, "SA008").ToString() == "1")  //已经结算
				{
					MessageBox.Show("火化已经办理且已结算!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (MessageBox.Show("已经办理火化业务,是否替换?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					return;
			}
			Frm_business06 frm_hh = new Frm_business06();
			frm_hh.swapdata["dataset"] = sa01_ds;
			frm_hh.swapdata["AC001"] = AC001;

			if (frm_hh.ShowDialog() == DialogResult.OK)
			{
				RefreshSalesData();
			}
		}

		/// <summary>
		/// 商品服务
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (FireAction.FireIsSettled(AC001) == "1")
			{
				MessageBox.Show("已经办理火化且结算完成,不能继续办理业务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			Frm_businessMisc frm_misc = new Frm_businessMisc();
			frm_misc.swapdata["businessObject"] = this;
			frm_misc.swapdata["dataset"] = sa01_ds;

			if (frm_misc.ShowDialog() == DialogResult.OK)
			{
				List<string> itemId_list = this.swapdata["itemIdList"] as List<string>;
				List<string> itemType_list = this.swapdata["itemTypeList"] as List<string>;
				List<decimal> price_list = this.swapdata["priceList"] as List<decimal>;
				List<int> nums_list = this.swapdata["numsList"] as List<int>;
				int re = 0;

				for (int i = 0; i < itemId_list.Count; i++)
				{
					if (itemType_list[i] == "10" || itemType_list[i] == "11")
					{
						re = gridView1.LocateByValue("SA002", itemType_list[i]);
						if (re > 0)
						{
							//如果已经办理 谷类或纸类并且已经结算,则跳过
							if (gridView1.GetRowCellValue(re, "SA008").ToString() == "1")
								continue;
							else
							{
								if (itemType_list[i] == "10")
								{
									if (MessageBox.Show("已经选择【骨灰盒】,是否要替换?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) continue;
								}
								else if (itemId_list[i] == "11")
								{
									if (MessageBox.Show("已经选择【纸棺】,是否要替换?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) continue;
								}
							}
						}
					}

					re = gridView1.LocateByValue("SA004", itemId_list[i]);
					if (re >= 0)
					{
						if (MessageBox.Show("【" + gridView1.GetRowCellValue(re, "SA003").ToString() + "】已经存在,要继续选择吗?",
							"提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) continue;
					}
					re = FireAction.FireSales_Misc(AC001,
												   itemId_list[i],
												   nums_list[i],
												   Envior.cur_userId
					);
					if (re < 0) return;
				}
				RefreshSalesData();
			}
		}

		private void BarButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
				this.SalesEdit(rowHandle);
		}

		private void GridView1_DoubleClick(object sender, EventArgs e)
		{
			int row = -1;
			if ((row = (sender as ColumnView).FocusedRowHandle) >= 0)
			{
				this.SalesEdit(row);
			}
		}

		/// <summary>
		/// 业务项目编辑
		/// </summary>
		/// <param name="rowIndex"></param>
		private void SalesEdit(int rowIndex)
		{
			if (gridView1.GetRowCellValue(rowIndex, "SA008").ToString() == "1")
			{
				MessageBox.Show("结算完成的记录不能修改!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			//权限检查
			//string s_right = Tools.GetRight(Envior.cur_userId, accountId == "1" ? "01060" : "07060");
			//if (s_right == "0" || (s_right == "1" && gridView1.GetRowCellValue(rowIndex, "SA100").ToString() != Envior.cur_userId))
			//{
			//	MessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//	return;
			//}
 
			string sa001 = gridView1.GetRowCellValue(rowIndex, "SA001").ToString();
			int index = gridView1.GetDataSourceRowIndex(rowIndex);
			Frm_salesEdit frm_edit = new Frm_salesEdit();

			frm_edit.swapdata["DATAROW"] = sa01_ds.Sa01.Rows[index];

			if (frm_edit.ShowDialog() == DialogResult.OK)
			{
				this.RefreshSalesData();
			}
		}

		/// <summary>
		/// 删除项目
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//权限检查
			//string accountId = FireAction.GetAccountIdByAc001(AC001);
			//if (Tools.GetRight(Envior.cur_userId, accountId == "1" ? "01070" : "07070") == "0")
			//{
			//	MessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//	return;
			//}

			if (gridView1.SelectedRowsCount == 0)
			{
				MessageBox.Show("请现在选择记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			string sa001;
			int re;

			if (MessageBox.Show("确认要删除吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

			foreach (int i in gridView1.GetSelectedRows())
			{
				sa001 = gridView1.GetRowCellValue(i, "SA001").ToString();
				re = FireAction.FireBusinessRemove(sa001);
				if (re < 0) return;
			}

			this.RefreshSalesData();
		}

		/// <summary>
		/// 刷新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			RefreshSalesData();
		}


		/// <summary>
		/// 应用套餐
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (FireAction.FireIsSettled(AC001) == "1")
			{
				MessageBox.Show("已经办理火化且结算完成,不能继续办理业务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			Frm_comboSelect frm_combo = new Frm_comboSelect();
			frm_combo.swapdata["AC001"] = AC001;

			if (frm_combo.ShowDialog() == DialogResult.OK)
			{
				RefreshSalesData();
			}
		}

		/// <summary>
		/// 结算
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//权限检查
			//if (Tools.GetRight(Envior.cur_userId, accountId == "1" ? "01080" : "07080") == "0")
			//{
			//	MessageBox.Show("权限不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//	return;
			//}

			int row1 = GridControl.InvalidRowHandle;

			if (gridView1.SelectedRowsCount == 0)
			{
				MessageBox.Show("请选择要结算的记录!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			//如果选择火化 必须结算所有业务!
			row1 = gridView1.LocateByValue("SA002", "06");
			if (row1 >=0 && gridView1.IsRowSelected(row1))
			{
				for(int i=0;i<gridView1.RowCount; i++)
				{
					if(gridView1.GetRowCellValue(i,"SA008").ToString() == "0" && (!gridView1.IsRowSelected(i)))
					{
						MessageBox.Show("【火化】业务必须最后结算!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
				}
			}

			List<int> rowList = new List<int>();
			foreach (int i in gridView1.GetSelectedRows())
			{
				if (Convert.ToDecimal(gridView1.GetRowCellValue(i, "PRICE")) == 0)
				{
					MessageBox.Show("结算的项目尚未输入价格!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				rowList.Add(gridView1.GetDataSourceRowIndex(i));
			}

			Frm_fireSettle frm_settle = new Frm_fireSettle();
			frm_settle.swapdata["dataset"] = sa01_ds;
			frm_settle.swapdata["AC001"] = AC001;
			frm_settle.swapdata["rowList"] = rowList;

			if (frm_settle.ShowDialog() == DialogResult.OK)
			{
				this.RefreshSalesData();
			}
		}

		private void GridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
		{
			if (e.Action == CollectionChangeAction.Add)
			{
				int row = gridView1.FocusedRowHandle;
				if (gridView1.GetRowCellValue(row, "SA008").ToString() == "1")
				{
					MessageBox.Show("已结算数据不能修改!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					gridView1.UnselectRow(row);
				}
			}
			else if (e.Action == CollectionChangeAction.Refresh && gridView1.SelectedRowsCount > 0)
			{
				gridView1.BeginUpdate();
				for (int i = 0; i < gridView1.RowCount; i++)
				{
					if (gridView1.GetRowCellValue(i, "SA008").ToString() == "1")
					{
						gridView1.UnselectRow(i);
					}
				}
				gridView1.EndUpdate();
			}else if(e.Action == CollectionChangeAction.Remove)
			{
				
			}
		}

        private void Txtedit_ac003_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void Txtedit_ac050_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void Lookup_store_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void Txtedit_ac018_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
