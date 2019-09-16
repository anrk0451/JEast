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
	class Uc01_ds : System.Data.DataSet
	{
		public DataTable Uc01 { get; }
		public OracleDataAdapter uc01Adapter { get; }
        OracleCommandBuilder builder = null;

		public Uc01_ds()
		{
            DataColumn col_uc001 = new DataColumn("UC001", typeof(string));   // 操作员编号      
            DataColumn col_uc002 = new DataColumn("UC002", typeof(string));   // 操作员代码            
            DataColumn col_uc003 = new DataColumn("UC003", typeof(string));   // 操作员姓名
            DataColumn col_uc004 = new DataColumn("UC004", typeof(string));   // 密码
			DataColumn col_uc005 = new DataColumn("UC005", typeof(string));   // 允许税务开票 1-yes 0-no
			DataColumn col_uc009 = new DataColumn("UC009", typeof(string));   // 映射税务端开票人
			DataColumn col_uc010 = new DataColumn("UC010", typeof(string));   // 税务端复核人
			DataColumn col_status = new DataColumn("STATUS", typeof(string)); // 状态
            DataColumn col_roleslist = new DataColumn("ROLESLIST", typeof(string)); // 所属角色列表

            Uc01 = new DataTable("Uc01");
            Uc01.Columns.AddRange(new DataColumn[]
                {col_uc001,col_uc002,col_uc003,col_uc004,col_uc005,col_uc009,col_uc010,col_status,col_roleslist});
            Uc01.PrimaryKey = new DataColumn[] { col_uc001 };                //设置主键

            this.Tables.Add(Uc01);

            uc01Adapter = new OracleDataAdapter("select * from v_uc01 where status = '1' order by uc001", SqlAssist.conn);
           
            builder = new OracleCommandBuilder(uc01Adapter);
		}
	}
}
