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
    class In01_ds : System.Data.DataSet
    {
        public DataTable In01 { get; }
        public OracleDataAdapter in01Adapter { get; }
        OracleCommandBuilder builder = null;

        public In01_ds()
        {
            DataColumn col_in001 = new DataColumn("IN001", typeof(string));   // 财政发票项目编号     
            DataColumn col_in003 = new DataColumn("IN003", typeof(string));   // 财政发票项目名
            DataColumn col_status = new DataColumn("STATUS", typeof(string)); // 状态

            In01 = new DataTable("In01");
            In01.Columns.AddRange(new DataColumn[]
                {col_in001,col_in003,col_status});
            In01.PrimaryKey = new DataColumn[] { col_in001 };                //设置主键

            this.Tables.Add(In01);

            in01Adapter = new OracleDataAdapter("select * from in01 where status = '1' order by in001", SqlAssist.conn);

            builder = new OracleCommandBuilder(in01Adapter);
        }
    }
}
