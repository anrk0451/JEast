using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JEast.Domain
{
    class Ac01
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string ac001 { get; set; } //逝者编号

        public string ac003 { get; set; } //逝者姓名
        public string ac002 { get; set; } //性别 0-男 1-女 2-不详
        public int ac004 { get; set; }    //年龄
        public string ac014 { get; set; } //身份证号
        public string ac005 { get; set; } //死亡原因
        public string ac006 { get; set; } //骨灰处理方式
        public string ac007 { get; set; } //所属区县
        public string ac008 { get; set; } //详细地址
        public DateTime? ac010 { get; set; } //死亡时间
        public string ac009 { get; set; } //接灵地址
        public DateTime? ac020 { get; set; } //到达中心时间
        public DateTime? ac015 { get; set; } //火化时间
        public DateTime? ac018 { get; set; } //告别时间
        public DateTime? ac019 { get; set; } //开光时间
        public string ac022 { get; set; }   //主持人
        public string ac050 { get; set; }   //联系人
        public string ac051 { get; set; }   //联系电话
        public string ac052 { get; set; }   //与逝者关系
        public string ac055 { get; set; }   //联系地址
        public string ac060 { get; set; }   //灵车车号

		public string ac070 { get; set; }	//火化炉标准
		public int? ac080 { get; set; }		//火化序号

 
        public string ac100 { get; set; }   //经办人
        public DateTime? ac200 { get; set; } //经办日期
        public string ac110 { get; set; }   //最后经办人
        public DateTime? ac220 { get; set; } //最后修改日期
        public string ac099 { get; set; }   //备注
 
        public string status { get; set; }  //当前状态  1-正常 0-删除

        public Ac01()
        {

        }
        /// <summary>
        /// 性别 值-文本 映射
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Ac01Ac002_Mapper(string value)
        {
            if (value == null)
                return "";
            else if (value == "0")
                return "男";
            else if (value == "1")
                return "女";
            else if (value == "2")
                return "未知";
            else
                return "";
        }
    }
}
