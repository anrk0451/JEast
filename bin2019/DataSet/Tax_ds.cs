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
	class Tax_ds : System.Data.DataSet
	{
		public DataTable taxinfo { get; }
		public OracleDataAdapter txAdapter { get; }

		public Tax_ds()
		{
			taxinfo = new DataTable();
			this.Tables.Add(taxinfo);
			txAdapter = new OracleDataAdapter("select * from SP01", SqlAssist.conn);
			txAdapter.Fill(taxinfo);
		}
	}
}
