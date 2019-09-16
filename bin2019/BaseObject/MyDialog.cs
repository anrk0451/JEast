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
using JEast.Action;

namespace JEast.BaseObject
{
    public partial class MyDialog : DevExpress.XtraEditors.XtraForm
    {
        ///窗体间交换数据
        public Dictionary<string, Object> swapdata { get; set; }


        public MyDialog()
        {
            swapdata = new Dictionary<string, object>();
            InitializeComponent();
        }

        /// <summary>
        /// 回车转tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyDialog_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Convert.ToInt32(e.KeyChar) == 13)
            {
                System.Windows.Forms.SendKeys.Send("{tab}");
            }
        }

		protected override void DefWndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case 10001:
					int commandNum = m.WParam.ToInt32();
					string responseText = PrtServAction.GetResponseText(commandNum);
					MessageBox.Show(responseText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					break;
				default:
					base.DefWndProc(ref m);///调用基类函数处理非自定义消息。
					break;
			}
		}

		private void MyDialog_Load(object sender, EventArgs e)
		{

		}
	}
}