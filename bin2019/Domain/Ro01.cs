using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEast.Domain
{

    class Ro01
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string ro001 { get; set; }   //角色编号
        public string ro003 { get; set; }   //角色名称
        public string ro004 { get; set; }   //角色描述
        public string status { get; set; }  //状态

    }
}
