using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEast.Misc
{
    /// <summary>
    /// 应用信息
    /// </summary>
    class AppInfo
    {
        private static string _AppTitle = "殡仪馆管理信息系统";     //应用标题
        private static string _AppVersion = "19.0914004";           //应用版本号
        private static string _UnitName = "佳木斯东郊殡仪馆";       //使用单位    
        private static string _ROOTID = "0000000000";               //root用户Id

		private static int _TAXITEMCOUNT = 7;                       //打印发票清单阈值
		private static string _REG_TAX_NAME = "寄存费";				//寄存费税务名称

        public static string UnitName
        {
            get { return AppInfo._UnitName; }
        }

        public static string AppTitle
        {
            get { return _AppTitle; }
        }

        public static string AppVersion
        {
            get { return _AppVersion; }
        }

        public static string ROOTID
        {
            get { return _ROOTID; }
        }
 
		public static int TAXITEMCOUNT
		{
			get { return _TAXITEMCOUNT; }
		}

		public static string REG_TAX_NAME
		{
			get { return _REG_TAX_NAME; }
		}

	}
}
