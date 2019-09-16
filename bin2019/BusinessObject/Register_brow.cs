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
using JEast.DataSet;
using JEast.windows;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using JEast.Misc;
using JEast.Action;

namespace JEast.BusinessObject
{
	public partial class Register_brow : BaseBusiness
	{
		Register_ds register_ds = new Register_ds();

		public Register_brow()
		{
			InitializeComponent();
			/////定义检索参数
			//op_days = new OracleParameter("days", OracleDbType.Int16);
			//op_days.Direction = ParameterDirection.Input;
			//register_ds.rc01Adapter.SelectCommand.Parameters.Add(op_days);
		}

		private void Register_brow_Load(object sender, EventArgs e)
		{
			gridControl1.DataSource = register_ds.Rc01;
			///装入数据
			Load_Data(combo_days.EditValue.ToString());
		}

		private void Load_Data(string filter)
		{
			switch (filter)
			{
				case "今日登记":
					register_ds.rc01Adapter.SelectCommand.CommandText = "select * from v_register where trunc(rc200) = trunc(sysdate)  ";
					break;
				case "近三日登记":
					register_ds.rc01Adapter.SelectCommand.CommandText = "select * from v_register where (trunc(sysdate) - trunc(rc200)) <=2  ";
					break;
				case "一个月内登记":
					register_ds.rc01Adapter.SelectCommand.CommandText = "select * from v_register where (trunc(sysdate) - trunc(rc200)) <=30  ";
					break;
				case "条件查询":
					
					Frm_registerSearch frm_search = new Frm_registerSearch();
					frm_search.swapdata["parent"] = this;

					if (frm_search.ShowDialog() == DialogResult.OK)
					{
						register_ds.rc01Adapter.SelectCommand.CommandText = "select * from v_register where 1=1 " + this.swapdata["sql"].ToString();
					}
					break;
			}
			///this.RefreshData();
			
			this.Cursor = Cursors.WaitCursor;
			gridView1.BeginUpdate();
			register_ds.Rc01.Rows.Clear();
			register_ds.rc01Adapter.Fill(register_ds.Rc01);
			gridView1.EndUpdate();
			this.Cursor = Cursors.Arrow;

		}

		/// <summary>
		/// 刷新数据
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.RefreshData();
		}
		private void RefreshData()
		{
			//this.Cursor = Cursors.WaitCursor;
			//gridView1.BeginUpdate();
			//register_ds.Rc01.Rows.Clear();
			//register_ds.rc01Adapter.Fill(register_ds.Rc01);
			//gridView1.EndUpdate();
			//this.Cursor = Cursors.Arrow;
			Load_Data(combo_days.EditValue.ToString());
		}

		/// <summary>
		/// 外来寄存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Frm_register frm_reg = new Frm_register();
			frm_reg.swapdata["dataset"] = register_ds;
			frm_reg.swapdata["source"] = "1";
			if (frm_reg.ShowDialog() == DialogResult.OK)
			{
				frm_reg.Dispose();
				this.RefreshData();
			}
		}

		/// <summary>
		/// 本馆火化寄存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Frm_fromFire frm_fromFire = new Frm_fromFire();
			frm_fromFire.swapdata["BusinessObject"] = this;
			if (frm_fromFire.ShowDialog() == DialogResult.OK)
			{
				string s_ac001 = this.swapdata["AC001"].ToString();
				frm_fromFire.Dispose();

				Frm_register regform = new Frm_register();
				regform.swapdata["dataset"] = register_ds;
				regform.swapdata["source"] = "0";
				regform.swapdata["AC001"] = s_ac001;
				if (regform.ShowDialog() == DialogResult.OK)
				{
					regform.Dispose();
					this.RefreshData();
				}
			}
		}

		/// <summary>
		/// 原始寄存登记
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Frm_oreg frm_ori = new Frm_oreg();
			frm_ori.swapdata["dataset"] = register_ds;
			if (frm_ori.ShowDialog() == DialogResult.OK)
			{
				frm_ori.Dispose();
				this.RefreshData();
			}
		}

		private void Combo_days_EditValueChanged(object sender, EventArgs e)
		{
			if (combo_days.EditValue != null && !string.IsNullOrWhiteSpace(combo_days.EditValue.ToString()))
			{
				Load_Data(combo_days.EditValue.ToString());
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
					EditData(gridView1.FocusedRowHandle);
				}
			}
		}

		/// <summary>
		/// 修改数据
		/// </summary>
		/// <param name="rowHandle"></param>
		private void EditData(int rowHandle)
		{
			string s_ac001 = gridView1.GetRowCellValue(rowHandle, "RC001").ToString();

			Frm_regedit frm_edit = new Frm_regedit();
			frm_edit.swapdata["dataset"] = register_ds;
			frm_edit.swapdata["AC001"] = s_ac001;
			if (frm_edit.ShowDialog() == DialogResult.OK)
			{
				this.RefreshData();
			}
		}

		/// <summary>
		/// 编辑 寄存信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				this.EditData(rowHandle);
			}
		}

		/// <summary>
		/// 位置变更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				Frm_RegisterMove frm_move = new Frm_RegisterMove();
				frm_move.swapdata["RC001"] = gridView1.GetRowCellValue(rowHandle, "RC001");
				if (frm_move.ShowDialog() == DialogResult.OK)
				{
					this.RefreshData();					
				}
				frm_move.Dispose();
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

		/// <summary>
		/// 续交寄存费
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				Frm_RegisterPay frm_pay = new Frm_RegisterPay();
				frm_pay.swapdata["RC001"] = gridView1.GetRowCellValue(rowHandle, "RC001");
				if (frm_pay.ShowDialog() == DialogResult.OK)
				{
					this.RefreshData();
				}
				frm_pay.Dispose();
			}
		}

		/// <summary>
		/// 补打寄存证
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				string s_rc001 = gridView1.GetRowCellValue(rowHandle, "RC001").ToString();
				PrtServAction.PrtRegisterCertBD(s_rc001, Envior.mform.Handle.ToInt32());
			}
		}

		/// <summary>
		/// 补打寄存标签
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				string s_rc001 = gridView1.GetRowCellValue(rowHandle, "RC001").ToString();
				PrtServAction.PrtRegisterLabel(s_rc001, Envior.mform.Handle.ToInt32());
			}
		}

		/// <summary>
		/// 补打缴费记录
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				string s_rc001 = gridView1.GetRowCellValue(rowHandle, "RC001").ToString();
				Frm_prtPayRecord frm_payrec = new Frm_prtPayRecord();
				frm_payrec.swapdata["RC001"] = s_rc001;

				frm_payrec.ShowDialog();
				frm_payrec.Dispose();				 
			}
		}

		private void BarButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.Load_Data("条件查询");
		}

		/// <summary>
		/// 寄存迁出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			if (rowHandle >= 0)
			{
				string s_rc001 = gridView1.GetRowCellValue(rowHandle, "RC001").ToString();
				Frm_registerOut frm_out = new Frm_registerOut();
				frm_out.swapdata["RC001"] = s_rc001;

				if (frm_out.ShowDialog() == DialogResult.OK)
				{
					this.RefreshData();
				}
				frm_out.Dispose();
			}
		}

		/// <summary>
		/// 寄存时间修正
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = gridView1.FocusedRowHandle;
			string s_rc001 = string.Empty;

			if(rowHandle >= 0)
			{
				if(register_ds.Rc01.Rows[gridView1.GetDataSourceRowIndex(rowHandle)]["SOURCE"].ToString() != "2")
				{
					MessageBox.Show("只有原始寄存可以调整寄存日期!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					return;
				}
				s_rc001 = gridView1.GetRowCellValue(rowHandle, "RC001").ToString();
				if(RegisterAction.GetRegPayRecordNum(s_rc001) > 1)
				{
					MessageBox.Show("原始登记已经缴费,不能继续!","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					return;
				}
				Frm_AdjustRegisterDate frm_1 = new Frm_AdjustRegisterDate();
				frm_1.swapdata["AC001"] = s_rc001;
				frm_1.ShowDialog();
				frm_1.Dispose();
			}
		}
	}
}
