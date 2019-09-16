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
using DevExpress.XtraBars.Ribbon;

namespace JEast.BaseObject
{
	public partial class BaseBusiness : DevExpress.XtraEditors.XtraUserControl
	{		
		public Dictionary<string, object> swapdata { get; set; }    //交换数据	 

		public BaseBusiness()
		{
			swapdata = new Dictionary<string, object>();
			InitializeComponent();
		}

		//业务对象初始化
		public virtual void Business_Init()
		{

		}
	}
}
