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
	//系统数据字典
	class Ro01_ds : System.Data.DataSet
	{
		public DataTable Ro01 { get; }
		public OracleDataAdapter ro01Adapter { get; }
        OracleCommandBuilder builder = null;

		public Ro01_ds()
		{
            DataColumn col_ro001 = new DataColumn("RO001", typeof(string));   // 角色编号             
            DataColumn col_ro003 = new DataColumn("RO003", typeof(string));   // 角色名
            DataColumn col_ro004 = new DataColumn("RO004", typeof(string));   // 角色描述
            DataColumn col_status = new DataColumn("STATUS", typeof(string)); // 状态

            Ro01 = new DataTable("Ro01");
            Ro01.Columns.AddRange(new DataColumn[]
                {col_ro001,col_ro003,col_ro004,col_status});
            Ro01.PrimaryKey = new DataColumn[] { col_ro001 };                //设置主键

            this.Tables.Add(Ro01);

            ro01Adapter = new OracleDataAdapter("select * from ro01 where status = '1' order by ro001", SqlAssist.conn);
           
            builder = new OracleCommandBuilder(ro01Adapter);
		}
	}
}
