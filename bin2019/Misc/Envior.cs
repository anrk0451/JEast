using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEast.Misc
{
	class Envior
	{
		public static RibbonForm mform { get; set; }       //当前主窗口
        public static string cur_user { get; set; }        //当前登录用户
        public static string cur_userId { get; set; }      //当前登录用户Id
        public static string cur_userName { get; set; }    //当前登录用户名

		public static string cur_invoicer { get; set; }	   //税务开票人	

		public static string cur_checker { get; set; }	   //税务开票复核人	

        public static string[] rolearry { get; set; }      //所属角色组
        public static char loginMode { get; set; }         //登陆模式

        //public static bool printable { get; set; }         //打印进程是否启动
		public static bool canInvoice { get; set; }		   //当前的用户允许开发票
        public static IntPtr prtservHandle { get; set; }   //打印服务窗口Handle
        public static int prtConnId { get; set; }          //打印会话连接Id 

		public static bool TAX_READY { get; set; }		   // 金税卡状态
 
	}
}
