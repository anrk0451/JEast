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
using JEast.DataSet;
using JEast.Misc;
using JEast.Action;
using JEast.Domain;

namespace JEast.windows
{
	public partial class Frm_fireSettle : MyDialog
	{
		Sa01_ds sa01_ds = null;
		string AC001 = string.Empty;
		List<int> rowList;
		DataTable dt_source;

		public Frm_fireSettle()
		{
			InitializeComponent();
		}

		private void Frm_fireSettle_Load(object sender, EventArgs e)
		{
			AC001 = this.swapdata["AC001"].ToString();
			sa01_ds = this.swapdata["dataset"] as Sa01_ds;
			rowList = this.swapdata["rowList"] as List<int>;

			///拷贝要结算的记录!!!
			dt_source = sa01_ds.Sa01.Clone();
			foreach (int i in rowList)
			{
				dt_source.Rows.Add(sa01_ds.Sa01.Rows[i].ItemArray);
			}

			gridControl1.DataSource = dt_source;
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

		private void B_ok_Click(object sender, EventArgs e)
		{
			string settleId = Tools.GetEntityPK("FA01");
			List<string> sa001_list = new List<string>();
			foreach (DataRow r in dt_source.Rows)
			{
				sa001_list.Add(r["SA001"].ToString());
			}

			int result = FireAction.FireBusinessSettle(settleId,
													   AC001,
													   sa001_list.ToArray(),
													   Envior.cur_userId
			);
			if (result > 0)
			{
				b_ok.Enabled = false;

				MessageBox.Show("结算办理成功!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);

				int fire_row = gridView1.LocateByValue("SA002", "06");
				//如果有火化,打印火化证明
				if (fire_row >= 0)
				{   //打印火化证明
					if(MessageBox.Show("现在打印火化证明!", "提示", MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == DialogResult.Yes)
						PrtServAction.Print_HHZM(AC001, this.Handle.ToInt32());
				}

				if (MessageBox.Show("现在打印【发票】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					if (!Envior.canInvoice)
					{
						MessageBox.Show("当前用户没有打印发票权限!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					}
					else if (!Envior.TAX_READY)
					{
						MessageBox.Show("金税卡没有打开!","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					}
					else
					{
						Frm_taxClientInfo frm_client = new Frm_taxClientInfo();
						frm_client.swapdata["parent"] = this;
						frm_client.swapdata["title"] = FireAction.Get_PassbyName(AC001);
						if(frm_client.ShowDialog(this) == DialogResult.OK)
						{
							InvoiceInfo invClient = this.swapdata["clientinfo"] as InvoiceInfo;

							//打印发票
							//PrtServAction.Print_Fireinvoice(settleId, invClient, this.Handle.ToInt32() );		
							PrtServAction.Print_Invoice(settleId, invClient);
						}
						frm_client.Dispose();
					}
				}

				//打印付货单
				int jp_row = gridView1.LocateByValue("SA002", "12");  //
				//如果有祭品 则打印付货单
				if (jp_row >= 0)
				{ 
					if(MessageBox.Show("现在打印【付货单】吗?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
					{
						PrtServAction.Print_FHD(settleId,this.Handle.ToInt32());
					}
				}

				DialogResult = DialogResult.OK;
				this.Dispose();
			}
		}
	}
}