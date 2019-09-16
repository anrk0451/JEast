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
	/// <summary>
	/// 进灵登记查询
	/// </summary>
	class Report_CheckinDs : System.Data.DataSet
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

		public Report_CheckinDs()
		{
			string sql = string.Empty;
			Ac01 = new DataTable("Ac01");
			St01 = new DataTable("St01");
			Uc01 = new DataTable("Uc01");
			Ct01 = new DataTable("Ct01");

			this.Tables.AddRange(new DataTable[] { Ac01, St01, Uc01, Ct01 });

			sql = @"select * from ac01 where status <> '0' and ((ac200,'yyyy-mm-dd') between :begin and :end ) and aac007 like :aac007 ";
			ac01Adapter = new OracleDataAdapter(sql, SqlAssist.conn);

			st01Adapter = new OracleDataAdapter("select * from st01 order by sortId", SqlAssist.conn);
			st01Adapter.Fill(St01);

			uc01Adapter = new OracleDataAdapter("select * from uc01", SqlAssist.conn);
			uc01Adapter.Fill(Uc01);

			ct01Adapter = new OracleDataAdapter("select * from ct01", SqlAssist.conn);
			ct01Adapter.Fill(Ct01);

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

	}
}
