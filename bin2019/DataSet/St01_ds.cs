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
	class St01_ds : System.Data.DataSet
	{
		public DataTable St01 { get; }
		public OracleDataAdapter st01Adapter { get; }
        OracleCommandBuilder builder = null;

		public St01_ds()
		{
            DataColumn col_st001 = new DataColumn("ST001", typeof(string));   // 数据字典编号
            DataColumn col_st002 = new DataColumn("ST002", typeof(string));   // 数据字典类别
            DataColumn col_st003 = new DataColumn("ST003", typeof(string));   // 数据字典值
            DataColumn col_sortId = new DataColumn("SORTID", typeof(int));    // 排序号
            DataColumn col_status = new DataColumn("STATUS", typeof(string)); // 状态
            St01 = new DataTable("St01");
            St01.Columns.AddRange(new DataColumn[]
                {col_st001,col_st002,col_st003,col_sortId,col_status});
            St01.PrimaryKey = new DataColumn[] { col_st001 };                //设置主键

            this.Tables.Add(St01);

			st01Adapter = new OracleDataAdapter("select * from st01 where st002 = :st002  order by st002,sortId", SqlAssist.conn);
           
            builder = new OracleCommandBuilder(st01Adapter);
		}
	}
}
