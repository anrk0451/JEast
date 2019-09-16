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

namespace JEast.windows
{
	public partial class Frm_bitInfo : MyDialog
	{
		DataTable dt_bit = new DataTable();
		OracleDataAdapter bitAdapter = new OracleDataAdapter("", SqlAssist.conn);

		public Frm_bitInfo(string regionId, string bitDesc)
		{
			InitializeComponent();
			bitAdapter.SelectCommand.CommandText = "select bi003,bi009,pkg_business.fun_getBitFullName(rg001,bi003) position,rc003,rc140,rc150,a.status status from bi01 a,rc01 b " +
														"where a.bi001 = b.rc130(+)  and rg001 = :rg001 and bi003= :bi003 ";
			OracleParameter op_rg001 = new OracleParameter("rg001", OracleDbType.Varchar2, 10);
			op_rg001.Direction = ParameterDirection.Input;
			op_rg001.Value = regionId;

			OracleParameter op_bi003 = new OracleParameter("bi003", OracleDbType.Varchar2, 50);
			op_bi003.Direction = ParameterDirection.Input;
			op_bi003.Value = bitDesc;

			bitAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_rg001, op_bi003 });
			bitAdapter.Fill(dt_bit);
		}

		private void Frm_bitInfo_Load(object sender, EventArgs e)
		{
			if (dt_bit.Rows.Count > 0)
			{
				textEdit1.EditValue = dt_bit.Rows[0]["position"];  //位置
				textEdit2.EditValue = dt_bit.Rows[0]["BI009"];     //价格
				textEdit3.EditValue = dt_bit.Rows[0]["STATUS"];    //状态
				if (dt_bit.Rows[0]["STATUS"].ToString() == "1")
				{
					textEdit4.EditValue = dt_bit.Rows[0]["RC003"];     //逝者姓名
					dateEdit1.EditValue = dt_bit.Rows[0]["RC140"];     //寄存日期"  
					dateEdit2.EditValue = dt_bit.Rows[0]["RC150"];     //过期日期"  
				}

			}
		}

		private void TextEdit3_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
		{
			if (e.Value == null)
				return;
			else if (e.Value.ToString() == "1")
				e.DisplayText = "占用";
			else if (e.Value.ToString() == "9")
				e.DisplayText = "空闲";
			else if (e.Value.ToString() == "0")
				e.DisplayText = "未用";
		}
	}
}