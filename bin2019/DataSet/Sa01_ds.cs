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
    class Sa01_ds : System.Data.DataSet
    {
        public DataTable Ac01 { get; }
        public DataTable Sa01 { get; }
        public DataTable St01 { get; }
        public DataTable Uc01 { get; }
        public DataTable Si01 { get; }

        public OracleDataAdapter ac01Adapter { get; }
        public OracleDataAdapter sa01Adapter { get; }
        public OracleDataAdapter st01Adapter { get; }
        public OracleDataAdapter uc01Adapter { get; }
        public OracleDataAdapter si01Adapter { get; }

        public DataView St01_relation { get; }

        public Sa01_ds()
        {
            //1.Sa01
            DataColumn col_sa001= new DataColumn("SA001", typeof(string));    // 销售流水号
            DataColumn col_ac001 = new DataColumn("AC001", typeof(string));   // 逝者编号
            DataColumn col_sa002 = new DataColumn("SA002", typeof(string));   // 服务或商品类别
            DataColumn col_sa003 = new DataColumn("SA003", typeof(string));   // 服务或商品名称
            DataColumn col_sa004 = new DataColumn("SA004", typeof(string));   // 服务或商品编号
            DataColumn col_sa005 = new DataColumn("SA005", typeof(string));   // 销售类别 0-火化业务 1-临时性销售 2骨灰寄存
            DataColumn col_price = new DataColumn("PRICE", typeof(decimal));  // 单价
            DataColumn col_nums = new DataColumn("NUMS", typeof(decimal));    // 数量
            DataColumn col_sa007 = new DataColumn("SA007", typeof(decimal));  // 销售金额
            DataColumn col_sa006 = new DataColumn("SA006", typeof(decimal));  // 原始单价
            DataColumn col_sa008 = new DataColumn("SA008", typeof(string));   // 结算状态 0-未结算 1-已结算 2-退费
            DataColumn col_sa010 = new DataColumn("SA010", typeof(string));   // 结算流水号
            DataColumn col_sa100 = new DataColumn("SA100", typeof(string));   // 经办人
            DataColumn col_status = new DataColumn("STATUS", typeof(string)); // 0-删除 1-正常

            Sa01 = new DataTable("Sa01");
            Sa01.Columns.AddRange(new DataColumn[] { col_sa001,col_ac001,col_sa002,col_sa003,col_sa004,col_sa005,col_price,col_nums,col_sa007,col_sa006,
                col_sa008,col_sa010,col_sa100,col_status
            });
            //Sa01.PrimaryKey = new DataColumn[] { col_sa001 };                 //设置主键
            this.Tables.Add(Sa01);
            sa01Adapter = new OracleDataAdapter("select * from sa01 where status <> '0' and  sa005 = '0' and ac001 = :ac001 order by sa002", SqlAssist.conn);
            sa01Adapter.Requery = true;

            //2.Ac01
            Ac01 = new DataTable("Ac01");
            this.Tables.Add(Ac01);
            ac01Adapter = new OracleDataAdapter("select * from ac01 where ac001 = :ac001",SqlAssist.conn);
            ac01Adapter.Requery = true;

            //3.St01
            St01 = new DataTable("St01");
            this.Tables.Add(St01);
            st01Adapter = new OracleDataAdapter("select * from st01 order by sortId", SqlAssist.conn);
            st01Adapter.Fill(St01);

            //4.DataView
            St01_relation = new DataView(St01);
            St01_relation.RowFilter = "ST002='RELATION'";

            //5.Uc01
            Uc01 = new DataTable("Uc01");
            this.Tables.Add(Uc01);
            uc01Adapter = new OracleDataAdapter("select * from uc01",SqlAssist.conn);
            uc01Adapter.Fill(Uc01);

            //6.Si01
            Si01 = new DataTable("Si01");
            this.Tables.Add(Si01);
            si01Adapter = new OracleDataAdapter("select item_type,item_id,item_text,price,sortId,zjf,1 nums from v_allvaliditem  order by item_type,sortId", SqlAssist.conn);
            si01Adapter.Fill(Si01);
            
        }
    }
}
