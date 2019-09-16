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
using JEast.Misc;
using Oracle.ManagedDataAccess.Client;
using JEast.windows;
using JEast.Action;
using JEast.DataSet;
using DevExpress.XtraGrid.Views.Base;
using JEast.Domain;

namespace JEast.BusinessObject
{
	public partial class TempSales : BaseBusiness
	{
		Sa01_ds sa01_ds = new Sa01_ds();

		public TempSales()
		{
			InitializeComponent();
		}

		private void TempSales_Load(object sender, EventArgs e)
		{

			gridControl1.DataSource = sa01_ds.Sa01;
			//sa01_ds.sa01Adapter.Fill(sa01_ds.Sa01);			 
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

		private void BarButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (gridView1.FocusedRowHandle >= 0)
			{
				EditItem(gridView1.FocusedRowHandle);
			}
		}

		private void EditItem(int rowHandle)
		{
			Frm_salesEdit frm_modi = new Frm_salesEdit();
			frm_modi.swapdata["DATAROW"] = sa01_ds.Sa01.Rows[gridView1.GetDataSourceRowIndex(rowHandle)];
			frm_modi.ShowDialog();
		}

		private void GridView1_DoubleClick(object sender, EventArgs e)
		{
			int row = -1;
			if ((row = (sender as ColumnView).FocusedRowHandle) >= 0)
			{
				this.EditItem(row);
			}
		}

		/// <summary>
		/// 删除项目
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;

			if (rowHandle <= 0)
			{
				MessageBox.Show("请先选择要删除的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			gridView1.DeleteRow(rowHandle); 
		}

		/// <summary>
		/// 结算
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (gridView1.RowCount == 0)
			{
				MessageBox.Show("没选择项目!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			///检查是否有价格为0 的项目
			for (int i = 0; i < gridView1.RowCount; i++)
			{
				if (Convert.ToDecimal(gridView1.GetRowCellValue(i, "PRICE")) <= 0)
				{
					MessageBox.Show("尚有未输入价格的项目!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					gridView1.SelectRow(i);
					return;
				}
			}

			string s_cuname;

			if (string.IsNullOrEmpty(textEdit1.Text))
			{
				textEdit1.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				textEdit1.ErrorText = "请输入交款单位!";
				return;
			}
			else
			{
				s_cuname = textEdit1.EditValue.ToString();
			}

			List<string> itemId_List = new List<string>();
			List<string> itemType_List = new List<string>();
			List<decimal> prict_List = new List<decimal>();
			List<decimal> nums_List = new List<decimal>();
			for (int i = 0; i < gridView1.RowCount; i++)
			{
				itemId_List.Add(gridView1.GetRowCellValue(i, "SA004").ToString());
				itemType_List.Add(gridView1.GetRowCellValue(i, "SA002").ToString());
				prict_List.Add(decimal.Parse(gridView1.GetRowCellValue(i, "PRICE").ToString()));
				nums_List.Add(decimal.Parse(gridView1.GetRowCellValue(i, "NUMS").ToString()));
			}
			string settleId = Tools.GetEntityPK("FA01");
			int re = FireAction.TempSalesSettle(
						s_cuname, settleId, itemId_List.ToArray(), itemType_List.ToArray(), prict_List.ToArray(), nums_List.ToArray(), Envior.cur_userId);
			if (re > 0)
			{
				if (MessageBox.Show("办理成功!现在打印【发票】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					if (!Envior.canInvoice)
					{
						MessageBox.Show("当前用户没有打印发票权限!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					if (!Envior.TAX_READY)
					{
						MessageBox.Show("金税卡没有打开!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else
					{
						Frm_taxClientInfo frm_client = new Frm_taxClientInfo();
						frm_client.swapdata["parent"] = Envior.mform;
						frm_client.swapdata["title"] = textEdit1.Text;

						if (frm_client.ShowDialog(Envior.mform) == DialogResult.OK)
						{
							InvoiceInfo invClient = Envior.mform.swapdata["clientinfo"] as InvoiceInfo;

							//打印发票
							//PrtServAction.Print_Fireinvoice(settleId, invClient, Envior.mform.Handle.ToInt32());
							PrtServAction.Print_Invoice(settleId, invClient);
						}
						frm_client.Dispose();
					}					
				}

				//打印付货单
				int jp_row = gridView1.LocateByValue("SA002", "12");  //
				//如果有祭品 则打印付货单
				if (jp_row >= 0)
				{   //打印火化证明
					if (MessageBox.Show("现在打印【付货单】吗?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						PrtServAction.Print_FHD(settleId, this.Handle.ToInt32());
					}
				}

				textEdit1.Text = "";
				sa01_ds.Sa01.Rows.Clear();
			}
		}


		/// <summary>
		/// 新增服务及商品
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
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

							if (itemType_list[i] == "10")
							{
								if (MessageBox.Show("已经选择【骨灰盒】,是否要替换?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) continue;
							}
							else if (itemId_list[i] == "11")
							{
								if (MessageBox.Show("已经选择【纸棺】,是否要替换?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) continue;
							}
							gridView1.DeleteRow(re);
						}
					}

					re = gridView1.LocateByValue("SA004", itemId_list[i]);
					if (re >= 0)
					{
						if (MessageBox.Show("【" + gridView1.GetRowCellValue(re, "SA003").ToString() + "】已经存在,要替换吗?",
							"提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) continue;
						gridView1.DeleteRow(re);
					}

					DataRow dr = sa01_ds.Sa01.Rows.Add();
					dr["SA003"] = MiscAction.GetItemFullName(itemId_list[i]);
					dr["SA002"] = itemType_list[i];
					dr["SA004"] = itemId_list[i];
					dr["PRICE"] = price_list[i];
					dr["SA005"] = "1";
					dr["NUMS"] = nums_list[i];
					dr["SA007"] = price_list[i] * nums_list[i];

					dr.EndEdit();
				}
				//RefreshSalesData();
			}
		}

		/// <summary>
		/// 休息室办理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int re;
			Frm_business03 frm_xxs = new Frm_business03();
			frm_xxs.swapdata["businessObject"] = this;
			frm_xxs.swapdata["dataset"] = sa01_ds;


			if (frm_xxs.ShowDialog() == DialogResult.OK)
			{
				List<string> itemIdList = this.swapdata["xxs"] as List<string>;
				foreach (string s in itemIdList)
				{
					re = gridView1.LocateByValue("SA004", s);
					if (re >= 0)
					{
						if (MessageBox.Show("【" + gridView1.GetRowCellValue(re, "SA003").ToString() + "】已经存在,要替换吗?",
							"提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) continue;
						gridView1.DeleteRow(re);
					}

					DataRow dr = sa01_ds.Sa01.Rows.Add();
					dr["SA003"] = MiscAction.GetItemFullName(s);
					dr["SA002"] = "03";								//类型：休息室
					dr["SA004"] = s;
					dr["PRICE"] = MiscAction.GetItemFixPrice(s);	//单价
					dr["SA005"] = "1";                              //临时性销售
					dr["NUMS"] = 1;
					dr["SA007"] = dr["PRICE"];

					dr.EndEdit();
				}
				
			}
		}
	}
}
