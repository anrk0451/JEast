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

namespace JEast.windows
{
	public partial class Frm_financeDaySearch : MyDialog
	{
		private BaseBusiness bo = null;
	 
		public Frm_financeDaySearch()
		{
			InitializeComponent();
		}

		private void Frm_financeDaySearch_Load(object sender, EventArgs e)
		{
			bo = this.swapdata["BusinessObject"] as BaseBusiness;
	 
			dateEdit2.EditValue = DateTime.Today;
			dateEdit1.EditValue = DateTime.Today;
		}

		private void B_ok_Click(object sender, EventArgs e)
		{
			bo.swapdata["dbegin"] = dateEdit1.EditValue;
			bo.swapdata["dend"] = dateEdit2.EditValue;
			bo.swapdata["FA003"] = textEdit1.EditValue;

			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void B_exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}