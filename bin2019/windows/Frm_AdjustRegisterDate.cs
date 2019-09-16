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
	public partial class Frm_AdjustRegisterDate : MyDialog
	{
		string s_ac001 = string.Empty;
		public Frm_AdjustRegisterDate()
		{
			InitializeComponent();
		}

		private void Frm_AdjustRegisterDate_Load(object sender, EventArgs e)
		{
			s_ac001 = this.swapdata["AC001"].ToString();
			OracleDataReader reader = SqlAssist.ExecuteReader("select * from rc01 where rc001='" + s_ac001 + "'");
			while (reader.Read())
			{
				 
				txtEdit_rc003.EditValue = reader["RC003"];
				txtedit_pos.Text = RegisterAction.GetRegPathName(s_ac001);
				de_begin.EditValue = reader["RC140"];
				de_end.EditValue = reader["RC150"];
				 
			}
		}

		private void B_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void B_ok_Click(object sender, EventArgs e)
		{
			if(DateTime.Compare( Convert.ToDateTime(de_begin.EditValue),Convert.ToDateTime(de_end.EditValue))>=0)
			{
				MessageBox.Show("寄存开始日期必须小于结束日期!","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return;
			}
			if(RegisterAction.AdjustRegisterDate(s_ac001,Convert.ToDateTime(de_begin.EditValue),Convert.ToDateTime(de_end.EditValue)) > 0)
			{
				MessageBox.Show("保存成功!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
				this.Close();
			}
		}
	}
}