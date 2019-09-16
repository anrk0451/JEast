using JEast.Misc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEast.DataSet
{
    class Ac01_ds : System.Data.DataSet
    {
        public DataTable Ac01 { get; }
        public DataTable St01 { get; }
        public DataTable Uc01 { get; }

		public DataTable Ct01 { get; }  //系统代码表

        public DataView St01_reason { get; }
        public DataView St01_district { get; }
        public DataView St01_driver { get; }
        public DataView St01_relation { get; }

		public DataView ct01_ASH_HANDLE { get; }
		public DataView ct01_HHL_TYPE { get; }


		public OracleDataAdapter ac01Adapter { get; }
        public OracleDataAdapter st01Adapter { get; }
        public OracleDataAdapter uc01Adapter { get; }
		public OracleDataAdapter ct01Adapter { get; }

       

        public Ac01_ds()
        {
            //1.Ac01
            DataColumn col_ac001 = new DataColumn("AC001", typeof(string));   // 逝者编号
            col_ac001.Unique = true;
            col_ac001.AllowDBNull = false;
            DataColumn col_ac003 = new DataColumn("AC003", typeof(string));   // 逝者姓名
            DataColumn col_ac002 = new DataColumn("AC002", typeof(string));   // 性别 0-男 1-女 2-不详
            DataColumn col_ac004 = new DataColumn("AC004", typeof(int));      // 年龄
			DataColumn col_ac006 = new DataColumn("AC006", typeof(string));   // 骨灰处理方式

			DataColumn col_ac014 = new DataColumn("AC014", typeof(string));   // 身份证号
            DataColumn col_ac010 = new DataColumn("AC010", typeof(DateTime)); // 死亡时间
            DataColumn col_ac015 = new DataColumn("AC015", typeof(DateTime)); // 火化时间
            DataColumn col_ac005 = new DataColumn("AC005", typeof(string));   // 死亡原因
            DataColumn col_ac007 = new DataColumn("AC007", typeof(string));   // 所属区县
            DataColumn col_ac008 = new DataColumn("AC008", typeof(string));   // 详细地址
            DataColumn col_ac009 = new DataColumn("AC009", typeof(string));   // 接灵地址
            DataColumn col_ac020 = new DataColumn("AC020", typeof(DateTime)); // 到达中心时间
            DataColumn col_ac018 = new DataColumn("AC018", typeof(DateTime)); // 告别时间
            DataColumn col_ac019 = new DataColumn("AC019", typeof(DateTime)); // 开光时间
            DataColumn col_ac022 = new DataColumn("AC022", typeof(string));   // 主持人
            DataColumn col_ac050 = new DataColumn("AC050", typeof(string));   // 联系人
            DataColumn col_ac051 = new DataColumn("AC051", typeof(string));   // 联系电话
            DataColumn col_ac052 = new DataColumn("AC052", typeof(string));   // 与逝者关系
            DataColumn col_ac055 = new DataColumn("AC055", typeof(string));   // 联系地址
            DataColumn col_ac060 = new DataColumn("AC060", typeof(string));   // 灵车车号
			DataColumn col_ac070 = new DataColumn("AC070", typeof(string));   // 火化炉标准 0-高档炉 1-普通炉 9-待定
			DataColumn col_ac080 = new DataColumn("AC080", typeof(decimal));  // 火化序号
			DataColumn col_ac100 = new DataColumn("AC100", typeof(string));   // 登记经办人
            DataColumn col_ac200 = new DataColumn("AC200", typeof(DateTime)); // 登记时间
            DataColumn col_ac110 = new DataColumn("AC110", typeof(string));   // 最后修改人
            DataColumn col_ac220 = new DataColumn("AC220", typeof(DateTime)); // 最后修改日期
            DataColumn col_ac099 = new DataColumn("AC099", typeof(string));   // 备注
            DataColumn col_status = new DataColumn("STATUS", typeof(string)); // 当前状态  1-正常 0-删除
 
            Ac01 = new DataTable("Ac01");
            Ac01.Columns.AddRange(new DataColumn[] {col_ac001,col_ac003,col_ac002,col_ac004,col_ac006,col_ac014,col_ac010,col_ac015,col_ac005,col_ac007,col_ac008,col_ac009,col_ac020,
                col_ac018,col_ac019,col_ac022,col_ac050,col_ac051,col_ac052,col_ac055,col_ac060,col_ac070,col_ac080,col_ac100,col_ac200,col_ac110,col_ac220,col_ac099,col_status
            });
            Ac01.PrimaryKey = new DataColumn[] { col_ac001 };                 //设置主键
            this.Tables.Add(Ac01);
            ac01Adapter = new OracleDataAdapter("select * from ac01 where status <> '0'   ", SqlAssist.conn);

            //2.St01
            St01 = new DataTable("St01");
            this.Tables.Add(St01);
            st01Adapter = new OracleDataAdapter("select * from st01 order by sortId",SqlAssist.conn);
            st01Adapter.Fill(St01);

            //3.Uc01
            Uc01 = new DataTable("Uc01");
            this.Tables.Add(Uc01);
            uc01Adapter = new OracleDataAdapter("select * from uc01",SqlAssist.conn);

			//4. Ct01
			Ct01 = new DataTable("Ct01");
			this.Tables.Add(Ct01);
			ct01Adapter = new OracleDataAdapter("select * from ct01", SqlAssist.conn);
			ct01Adapter.Fill(Ct01);

			//5.DataView

			St01_reason = new DataView(St01);
            St01_reason.RowFilter = "ST002='DIEREASON'";

            St01_driver = new DataView(St01);
            St01_driver.RowFilter = "ST002='DRIVER'";

            St01_district = new DataView(St01);
            St01_district.RowFilter = "ST002='DISTRICT'";

            St01_relation = new DataView(St01);
            St01_relation.RowFilter = "ST002='RELATION'";

			ct01_ASH_HANDLE = new DataView(Ct01);
			ct01_ASH_HANDLE.RowFilter = "CT002='ASH_HANDLE'";
			ct01_ASH_HANDLE.Sort = "CT001 ASC";

			ct01_HHL_TYPE = new DataView(Ct01);
			ct01_HHL_TYPE.RowFilter = "CT002='HHL_TYPE'";
			ct01_HHL_TYPE.Sort = "CT001 ASC";
		}

        public void Fill_ac01()
        {
            Ac01.Rows.Clear();
            ac01Adapter.Fill(Ac01);           
        }
    }
}
