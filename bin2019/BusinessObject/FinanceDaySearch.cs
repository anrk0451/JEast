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
using JEast.windows;
using JEast.Action;
using System.Globalization;
using JEast.Domain;

namespace JEast.BusinessObject
{
	public partial class FinanceDaySearch : BaseBusiness
	{
		private DataTable dt_finance = new DataTable("FINANCE");
	 
		private OracleDataAdapter finAdapter =
			new OracleDataAdapter("select * from v_financeDay where (to_char(fa200,'yyyy-mm-dd') between :begin and :end) and fa003 like :fa003 ", SqlAssist.conn);

		private DataTable dt_detail = new DataTable("DETAIL");
		private OracleDataAdapter deAdapter =
			new OracleDataAdapter("select * from v_findetail where sa010 = :sa010", SqlAssist.conn);

		OracleParameter op_begin = null;
		OracleParameter op_end = null;
		OracleParameter op_fa003 = null;
		OracleParameter op_sa010 = null;
		OracleParameter op_fa100 = null;

		public FinanceDaySearch()
		{
			InitializeComponent();
		}

		private void FinanceDaySearch_Load(object sender, EventArgs e)
		{
			op_begin = new OracleParameter("begin", OracleDbType.Varchar2, 20);
			op_begin.Direction = ParameterDirection.Input;

			op_end = new OracleParameter("end", OracleDbType.Varchar2, 20);
			op_end.Direction = ParameterDirection.Input;

			op_fa003 = new OracleParameter("fa003", OracleDbType.Varchar2, 80);
			op_fa003.Direction = ParameterDirection.Input;

			////////// 除了管理员,只能查看自己的收费记录 //////////////
			op_fa100 = new OracleParameter("fa100", OracleDbType.Varchar2, 10);
			op_fa100.Direction = ParameterDirection.Input;
			if (Envior.cur_userId == AppInfo.ROOTID)
			{
				op_fa100.Value = "%";
			}
			else
			{
				op_fa100.Value = Envior.cur_userId;
			}
			/////////////////////////////////////////////////////////////

			op_sa010 = new OracleParameter("sa010", OracleDbType.Varchar2, 10);
			op_sa010.Direction = ParameterDirection.Input;

			finAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end, op_fa003 });
			deAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_sa010 });

			gridControl1.DataSource = dt_finance;
			gridControl2.DataSource = dt_detail;

			gridControl1.Visible = true;

			this.Show_Condition();

		}

		/// <summary>
		/// 刷新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.RefreshData();
		}

		private void RefreshData()
		{
			this.Cursor = Cursors.WaitCursor;
			gridView1.BeginUpdate();
			dt_finance.Rows.Clear();

			finAdapter.Fill(dt_finance);
			gridView1.EndUpdate();
			this.Cursor = Cursors.Arrow;
		}


		private void Show_Condition()
		{
			Frm_financeDaySearch frm_1 = new Frm_financeDaySearch();
			frm_1.swapdata["BusinessObject"] = this;
			if (frm_1.ShowDialog() == DialogResult.OK)
			{
				frm_1.Dispose();
				string s_begin = string.Empty;
				string s_end = string.Empty;
				string s_fa003 = string.Empty;

				if (this.swapdata["dbegin"] == null)
				{
					s_begin = "1900/01/01";
				}
				else
				{
					s_begin = Convert.ToDateTime(this.swapdata["dbegin"]).ToString("yyyy/MM/dd");
				}

				if (this.swapdata["dend"] == null)
				{
					s_end = "9999/12/31";
				}
				else
				{
					s_end = Convert.ToDateTime(this.swapdata["dend"]).ToString("yyyy/MM/dd");
				}

				if (this.swapdata["FA003"] == null || string.IsNullOrEmpty(this.swapdata["FA003"].ToString()))
				{
					s_fa003 = "%";
				}
				else
				{
					s_fa003 = this.swapdata["FA003"].ToString() + "%";
				}


				op_begin.Value = s_begin;
				op_end.Value = s_end;
				op_fa003.Value = s_fa003;

				gridView1.BeginUpdate();
				dt_finance.Rows.Clear();

				finAdapter.Fill(dt_finance);

				gridCol_Fa004.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridCol_Fa004.SummaryItem.DisplayFormat = "合计 = {0:N2}";

				gridColumn5.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
				gridColumn5.SummaryItem.DisplayFormat = "共计 = {0:N0}笔";

				gridView1.EndUpdate();
			}
		}

		/// <summary>
		/// 输入查询条件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.Show_Condition();
		}

		private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (!gridView1.IsFindPanelVisible)
				gridView1.ShowFindPanel();
			else
				gridView1.HideFindPanel();
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
		/// 行焦点改变
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if (e.FocusedRowHandle >= 0)
			{
				this.RetrieveDetail(e.FocusedRowHandle);
			}
		}

		/// <summary>
		/// 检索明细
		/// </summary>
		/// <param name="rowHandle"></param>
		private void RetrieveDetail(int rowHandle)
		{
			if (rowHandle >= 0)
			{
				string s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
				op_sa010.Value = s_fa001;
				gridView2.BeginUpdate();
				dt_detail.Rows.Clear();
				deAdapter.Fill(dt_detail);
				gridView2.EndUpdate();
			}
		}

		/// <summary>
		/// 收款作废
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			DateTime dt_fa200;     //收费日期

			

			if (rowHandle >= 0)
			{
				//只能作废当日收费记录
				DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
				dtFormat.ShortDatePattern = "yyyy-MM-dd";
				dt_fa200 = Convert.ToDateTime(gridView1.GetRowCellValue(rowHandle, "FA200").ToString(), dtFormat);
				if(String.Compare(dt_fa200.ToString("yyyy-MM-dd"), MiscAction.GetServerDateString()) < 0  && Envior.cur_userId != AppInfo.ROOTID )
				{
					MessageBox.Show("只能作废当天的收费记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
 
				if (MessageBox.Show("确认要作废吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
				string s_rc001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
				if (Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "FA004")) < 0)
				{
					MessageBox.Show("退费业务不能作废!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (gridView1.GetRowCellValue(rowHandle, "FA002").ToString() == "2")  //寄存业务
				{

					decimal count = (decimal)SqlAssist.ExecuteScalar("select count(*) from v_rc04 where rc001='" + s_rc001 + "'", null);
					if (count <= 1)
					{
						if (MessageBox.Show("此记录是唯一一次交费记录,作废此记录将删除寄存登记信息,是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
					}
				}

				string s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
				string s_fa005 = gridView1.GetRowCellValue(rowHandle,"FA005").ToString();    //电子发票号
				string s_retCode = string.Empty;

				int re = MiscAction.FinanceRemove(s_fa001, Envior.cur_userId);
				if (re > 0)
				{
					////////// 发票作废 ///////////////////////////////////////////////////
					if(!string.IsNullOrEmpty(s_fa005))
					{
						if (!Envior.TAX_READY)
						{
							MessageBox.Show("金税卡没有打开!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						}
						else
						{
							s_retCode = PrtServAction.InvoiceRemoved(s_fa001, Envior.mform.Handle.ToInt32());
						}
					}
					///////////////////////////////////////////////////////////////////////

					if (!string.IsNullOrEmpty(s_fa005) && s_retCode == "6011")
						MessageBox.Show("作废成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
					else if (!string.IsNullOrEmpty(s_fa005) && s_retCode != "6011")
						MessageBox.Show("作废成功但未作废发票!" + s_retCode, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					else
						MessageBox.Show("作废成功!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);


					//MessageBox.Show("作废成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
					dt_finance.Rows.RemoveAt(gridView1.GetDataSourceRowIndex(rowHandle));
					if (gridView1.RowCount == 0)
					{
						dt_detail.Rows.Clear();
					}
					else
					{
						this.RetrieveDetail(gridView1.FocusedRowHandle);
					}
					return;
				}
			}
		}

		/// <summary>
		/// 补开发票
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			string s_fa002 = string.Empty;
			string s_fa001 = string.Empty;
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				//if(string.IsNullOrEmpty(gridView1.GetRowCellValue(rowHandle,"FA005").ToString()) )
				//{
				//	MessageBox.Show("当前记录已开税务发票!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				//	return;
				//}
				if (!Envior.canInvoice)
				{
					MessageBox.Show("当前用户没有打印发票权限!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (!Envior.TAX_READY)
				{
					MessageBox.Show("金税卡没有打开!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				s_fa002 = gridView1.GetRowCellValue(rowHandle, "FA002").ToString();
				s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();

				Frm_taxClientInfo frm_client = new Frm_taxClientInfo();

				frm_client.swapdata["parent"] = this;
				frm_client.swapdata["title"] = gridView1.GetRowCellValue(rowHandle, "FA003").ToString();

				if (frm_client.ShowDialog(this) == DialogResult.OK)
				{
					InvoiceInfo invClient = Envior.mform.swapdata["clientinfo"] as InvoiceInfo;

					//if (s_fa002.Equals("0") || s_fa002.Equals("1"))  //火化收费 or 临时性销售
					//{
					//	PrtServAction.Print_Fireinvoice(s_fa001, invClient, Envior.mform.Handle.ToInt32());
					//}else if (s_fa002.Equals("2"))					 //寄存收费
					//{
					//	PrtServAction.Print_RegisterInvoice(s_fa001, invClient, Envior.mform.Handle.ToInt32());
					//}
					PrtServAction.Print_Invoice(s_fa001, invClient);
				}
				frm_client.Dispose();
				this.RefreshData();
			}
		}

		/// <summary>
		/// 打印发票
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				if (gridView1.GetRowCellValue(rowHandle, "FA005") == null)
				{
					MessageBox.Show("当前记录尚未开税务发票!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				string s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
				string s_tax_code = PrtServAction.GetTaxCode(s_fa001);
				string s_tax_num = PrtServAction.GetTaxNum(s_fa001);

				if(MessageBox.Show(@"打印当前发票？\r\n【类型】: " + s_tax_code + @"\r\n" + "【票号】:" + s_tax_num , "提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
				{
					//PrtServAction.PrintInvoice(s_fa001, Envior.mform.Handle.ToInt32());
					Envior.mform.goldTax.InfoKind = 2;							    //发票类型	
					Envior.mform.goldTax.InfoTypeCode = s_tax_code;					//发票代码
					Envior.mform.goldTax.InfoNumber = Convert.ToInt32(s_tax_num);   //发票号
					Envior.mform.goldTax.InfoShowPrtDlg = 1;                        //是否显示确认对话框
					Envior.mform.goldTax.GoodsListFlag = 0;                         //打印发票
					Envior.mform.goldTax.PrintInv();
				}

			}
		}

		/// <summary>
		/// 打印清单
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				if (gridView1.GetRowCellValue(rowHandle, "FA005") == null)
				{
					MessageBox.Show("当前记录尚未开税务发票!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if(gridView2.RowCount <= 7 /*AppInfo.TAXITEMCOUNT*/)
				{
					MessageBox.Show("此笔业务没有发票清单", "提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					return;
				}

				string s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
				string s_tax_code = PrtServAction.GetTaxCode(s_fa001);
				string s_tax_num = PrtServAction.GetTaxNum(s_fa001);

				if (MessageBox.Show(@"打印当前发票清单？\r\n【类型】: " + s_tax_code + @"\r\n" + "【票号】:" + s_tax_num, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					//PrtServAction.PrintInvoice(s_fa001, Envior.mform.Handle.ToInt32());
					Envior.mform.goldTax.InfoKind = 2;                                       //发票类型	
					Envior.mform.goldTax.InfoTypeCode = s_tax_code;                          //发票代码
					Envior.mform.goldTax.InfoNumber = Convert.ToInt32(s_tax_num);            //发票号
					Envior.mform.goldTax.InfoShowPrtDlg = 1;                                 //是否显示确认对话框
					Envior.mform.goldTax.GoodsListFlag = 1;                                  //打印发票
					Envior.mform.goldTax.PrintInv();
				}

			}
		}
	}
}
