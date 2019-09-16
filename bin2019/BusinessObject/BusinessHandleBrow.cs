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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using JEast.Misc;

namespace JEast.BusinessObject
{
	public partial class BusinessHandleBrow : BaseBusiness
	{
		HavingNow_ds having_ds = new HavingNow_ds();

		public BusinessHandleBrow()
		{
			InitializeComponent();
		}

		private void BusinessHandleBrow_Load(object sender, EventArgs e)
		{
			gridControl1.DataSource = having_ds.Ac01;
			having_ds.Fill_ac01();
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

		private void GridView1_MouseDown(object sender, MouseEventArgs e)
		{
			GridHitInfo hInfo = gridView1.CalcHitInfo(new Point(e.X, e.Y));
			if (e.Button == MouseButtons.Left && e.Clicks == 2)
			{
				//判断光标是否在行范围内  
				if (hInfo.InRow)
				{
					Business(gridView1.FocusedRowHandle);
				}
			}
		}

		private void Business(int rowHandle)
		{
			string s_ac001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
			(Envior.mform as RibbonForm).openBusinessObject("FireBusiness", s_ac001);
		}

		private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle < 0) return;

			this.Business(rowHandle);
		}

		private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			gridView1.BeginUpdate();
			having_ds.Fill_ac01();
			gridView1.EndUpdate();
		}

		private void GridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			if (e.Column.FieldName == "AC070")
			{
				if (e.Value.ToString() == "0")
					e.DisplayText = "高档炉";
				else if (e.Value.ToString() == "1")
					e.DisplayText = "普通炉";
				else
					e.DisplayText = "";
			}
		}
	}
}
