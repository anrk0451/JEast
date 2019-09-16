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

namespace JEast.BaseObject
{
	public partial class CheckedListBoxOnlyOne : DevExpress.XtraEditors.XtraUserControl
	{
		bool flag = true;
		public CheckedListBoxControl checklist { get; set; }	

		public CheckedListBoxOnlyOne()
		{
			InitializeComponent();
			checklist = checkedListBoxControl1;
		}

		private void CheckedListBoxControl1_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
		{
			if (e.State == CheckState.Checked && flag)
			{
				flag = false;
				checkedListBoxControl1.BeginUpdate();
				checkedListBoxControl1.UnCheckAll();
				checkedListBoxControl1.SetItemChecked(e.Index, true);
				checkedListBoxControl1.EndUpdate();
				flag = true;
			}
		}
	}
}
