using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEast.Domain
{
	[SugarTable("BO01")]
	class Bo01
	{
		[SugarColumn(IsPrimaryKey = true)]
		public string bo001 { get; set; }   //业务编号

		public string bo003 { get; set; }   //业务名称
		public string bo004 { get; set; }   //业务对象类型 w-窗口 x-xtratabpage对象
	}
}
