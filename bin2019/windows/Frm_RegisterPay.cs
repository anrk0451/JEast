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
using JEast.Domain;

namespace JEast.windows
{
	public partial class Frm_RegisterPay : MyDialog
	{
		private string rc001 = string.Empty;				//逝者编号
		private decimal bitprice = decimal.Zero;			//号位单价 
		private DataTable dt_rc04 = new DataTable("RC04");  //缴费记录
		private OracleDataAdapter rc04Adapter = new OracleDataAdapter("", SqlAssist.conn);


		public Frm_RegisterPay()
		{
			InitializeComponent();
		}

		private void Frm_RegisterPay_Load(object sender, EventArgs e)
		{
			string s_rc130 = string.Empty;

			rc001 = this.swapdata["RC001"].ToString();

			OracleDataReader reader = SqlAssist.ExecuteReader("select * from rc01 where rc001='" + rc001 + "'");
			while (reader.Read())
			{
				txtEdit_rc001.Text = rc001;
				txtEdit_rc109.EditValue = reader["RC109"];
				txtEdit_rc003.EditValue = reader["RC003"];
				txtEdit_rc303.EditValue = reader["RC303"];
				txtEdit_rc004.EditValue = reader["RC004"];
				txtEdit_rc404.EditValue = reader["RC404"];
				rg_rc002.EditValue = reader["RC002"];
				rg_rc202.EditValue = reader["RC202"];
				be_position.Text = RegisterAction.GetRegPathName(rc001);

				s_rc130 = reader["RC130"].ToString();
				bitprice = Convert.ToDecimal(SqlAssist.ExecuteScalar("select bi009 from bi01 where bi001='" + s_rc130 + "'", null));
				txtedit_price.EditValue = bitprice;
			}

			rc04Adapter.SelectCommand.CommandText = "select * from v_rc04 where rc001='" + rc001 + "' order by rc020";
			rc04Adapter.Fill(dt_rc04);
			gridControl1.DataSource = dt_rc04;

			comboBox1.Text = "";
		}

		/// <summary>
		/// 显示缴费类型
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			if (e.Column.FieldName == "RC031")  //缴费类型
			{
				if (e.Value.ToString() == "1")
					e.DisplayText = "正常";
				else if (e.Value.ToString() == "0")
				{
					e.DisplayText = "原始登记";
				}
			}
		}

		/// <summary>
		/// 缴费年限变更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ComboBox1_TextChanged(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(comboBox1.Text)) return;
			decimal nums = decimal.Parse(comboBox1.Text);
			if (nums > 0 && bitprice > 0)
			{
				txtedit_regfee.EditValue = nums * bitprice;
			}
		}

		/// <summary>
		/// 缴费年限校验
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ComboBox1_Validating(object sender, CancelEventArgs e)
		{
			decimal nums;
			if (!decimal.TryParse(comboBox1.Text, out nums))
			{
				e.Cancel = true;
				MessageBox.Show("请输入正确的缴费年限!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (nums - Math.Truncate(nums) > 0 && nums - Math.Truncate(nums) != new decimal(0.5))
			{
				e.Cancel = true;
				MessageBox.Show("缴费年限只能是整年或半年!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
		}

		private void B_ok_Click(object sender, EventArgs e)
		{
			decimal nums;
			if (!decimal.TryParse(comboBox1.Text, out nums))
			{
				MessageBox.Show("请输入正确的缴费年限!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (!(bitprice > 0))
			{
				MessageBox.Show("参数传递错误!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			string fa001 = Tools.GetEntityPK("FA01");
			int re = RegisterAction.RegisterPay(rc001, fa001, bitprice, nums, Envior.cur_userId);
			if (re > 0)
			{
				dt_rc04.Rows.Clear();
				rc04Adapter.Fill(dt_rc04);

				if (MessageBox.Show("缴费成功!现在打印【发票】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
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
						frm_client.swapdata["parent"] = this;
						frm_client.swapdata["title"] = FireAction.Get_PassbyName(rc001);
						if (frm_client.ShowDialog(this) == DialogResult.OK)
						{
							InvoiceInfo invClient = this.swapdata["clientinfo"] as InvoiceInfo;

							//打印发票
							//PrtServAction.Print_RegisterInvoice(fa001, invClient, this.Handle.ToInt32());
							PrtServAction.Print_Invoice(fa001,invClient);
						}
						frm_client.Dispose();
					}
				}

				if (MessageBox.Show("现在打印缴费记录吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					//打印缴费记录
					PrtServAction.PrtRegisterPayRecord(fa001, this.Handle.ToInt32());
				}
				DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void B_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}