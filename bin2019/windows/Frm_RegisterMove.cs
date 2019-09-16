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
using Oracle.ManagedDataAccess.Client;
using JEast.Misc;
using JEast.Action;

namespace JEast.windows
{
	public partial class Frm_RegisterMove : MyDialog
	{
		private string rc001 = string.Empty;      //逝者编号
		private string bitId = string.Empty;	  //变更后号位
		private string bitId_Old = string.Empty;  //变更前号位	

		public Frm_RegisterMove()
		{
			InitializeComponent();
		}

		private void Frm_RegisterMove_Load(object sender, EventArgs e)
		{
			rc001 = this.swapdata["RC001"].ToString();

			OracleDataReader reader = SqlAssist.ExecuteReader("select * from rc01 where rc001='" + rc001 + "'");
			while (reader.Read())
			{
				txtEdit_rc001.Text = rc001;
				txtEdit_rc109.EditValue = reader["RC109"];
				txtEdit_rc003.EditValue = reader["RC003"];
				bitId_Old = reader["RC130"].ToString();
				be_position.EditValue = RegisterAction.GetRegPathName(rc001);
			}
		}

		private void B_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 选择新的号位
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Be_newposition_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			Frm_freebit frm_free = new Frm_freebit();
			frm_free.swapdata["parent"] = this;

			if (frm_free.ShowDialog() == DialogResult.OK)
			{
				string regionId, bitDesc;
				regionId = this.swapdata["regionId"].ToString();
				bitDesc = this.swapdata["bitDesc"].ToString();
				bitId = RegisterAction.GetBitId(regionId, bitDesc);
				be_newposition.Text = RegisterAction.GetBitFullName(regionId, bitDesc);
			}
		}

		private void B_ok_Click(object sender, EventArgs e)
		{
			string s_rt003 = string.Empty;

			if (string.IsNullOrEmpty(bitId))
			{
				be_newposition.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				be_newposition.ErrorText = "请选择变更后位置!";
				return;
			}
			s_rt003 = txtedit_rt003.Text;

			if (string.IsNullOrEmpty(s_rt003))
			{
				txtedit_rt003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				txtedit_rt003.ErrorText = "请输入变更原因!";
				return;
			}


			int re = RegisterAction.RegisterMove(rc001, bitId_Old, bitId, s_rt003, Envior.cur_userId);
			if (re > 0)
			{
				MessageBox.Show("办理成功!", "提示");
				DialogResult = DialogResult.OK;
				this.Close();
			}
		}
	}
}