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
	class HavingNow_ds : System.Data.DataSet
	{
		public DataTable Ac01 { get; }
		public OracleDataAdapter ac01Adapter { get; }

		public HavingNow_ds()
		{
			Ac01 = new DataTable("Ac01");			 
			this.Tables.Add(Ac01);
			ac01Adapter = new OracleDataAdapter("select * from  v_having", SqlAssist.conn);
 
		}

		public void Fill_ac01()
		{
			Ac01.Rows.Clear();
			ac01Adapter.Fill(Ac01);
		}


	}
}
