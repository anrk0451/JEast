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
    /// 服务、商品定价结果集
    /// </summary>
    class ServiceProduct_ds : System.Data.DataSet
    {
        public DataTable Si01 { get; }
        public DataTable Gi01 { get; }
 

        public OracleDataAdapter si01Adapter { get; }
        public OracleDataAdapter gi01Adapter { get; }
 

        OracleCommandBuilder sibuilder = null;
        OracleCommandBuilder gibuilder = null;
 

        public ServiceProduct_ds()
        {
            DataColumn col_si001 = new DataColumn("SI001", typeof(string));   // 服务编号
            DataColumn col_si002 = new DataColumn("SI002", typeof(string));   // 服务类别
            DataColumn col_si003 = new DataColumn("SI003", typeof(string));   // 服务项目名称
            DataColumn col_price = new DataColumn("PRICE", typeof(decimal));  // 单价
            DataColumn col_si005 = new DataColumn("SI005", typeof(string));   // 占用标志
            DataColumn col_si088 = new DataColumn("SI088", typeof(string));   // 助记符
            DataColumn col_sortId = new DataColumn("SORTID", typeof(int));    // 排序号
			DataColumn col_tx000 = new DataColumn("TX000", typeof(string));   // 免税标志 1-免 0-不免 
			DataColumn col_tx001 = new DataColumn("TX001", typeof(string));   // 税务发票编码 
			DataColumn col_tx003 = new DataColumn("TX003", typeof(string));   // 税务发票-映射名称
			DataColumn col_tx005 = new DataColumn("TX005", typeof(int));	  // 税率
			DataColumn col_status = new DataColumn("STATUS", typeof(string)); // 状态

            Si01 = new DataTable("Si01");
            Si01.Columns.AddRange(new DataColumn[]
                {col_si001,col_si002,col_si003,col_price,col_si005,col_si088,col_sortId,col_tx000,col_tx001,col_tx003,col_tx005,col_status});
            Si01.PrimaryKey = new DataColumn[] { col_si001 };                //设置主键

            this.Tables.Add(Si01);

            si01Adapter = new OracleDataAdapter("select * from si01 where si002 = :si002  order by si002,sortId", SqlAssist.conn);
            si01Adapter.Requery = true;

            sibuilder = new OracleCommandBuilder(si01Adapter);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            DataColumn col_gi001 = new DataColumn("GI001", typeof(string));   // 商品编号
            DataColumn col_gi002 = new DataColumn("GI002", typeof(string));   // 商品类别
            DataColumn col_gi003 = new DataColumn("GI003", typeof(string));   // 商品名称
            DataColumn col_price2 = new DataColumn("PRICE", typeof(decimal)); // 商品单价      
            DataColumn col_gi088 = new DataColumn("GI088", typeof(string));   // 助记符
            DataColumn col_sortId2 = new DataColumn("SORTID", typeof(int));   // 排序号

			DataColumn col_tx000_2 = new DataColumn("TX000", typeof(string));  // 免税标志 1-免 0-不免 
			DataColumn col_tx001_2 = new DataColumn("TX001", typeof(string));  // 税务发票编码 
			DataColumn col_tx003_2 = new DataColumn("TX003", typeof(string));  // 税务发票-映射名称
			DataColumn col_tx005_2 = new DataColumn("TX005", typeof(int));     // 税率
			DataColumn col_status_2 = new DataColumn("STATUS", typeof(string));// 状态
 
            Gi01 = new DataTable("Gi01");
            Gi01.Columns.AddRange(new DataColumn[]
                {col_gi001,col_gi002,col_gi003,col_price2,col_gi088,col_sortId2,col_tx000_2,col_tx001_2,col_tx003_2,col_tx005_2,col_status_2});
            Gi01.PrimaryKey = new DataColumn[] { col_gi001 };                //设置主键

            this.Tables.Add(Gi01);

            gi01Adapter = new OracleDataAdapter("select * from gi01 where gi002 = :gi002  order by gi002,sortId", SqlAssist.conn);
            gi01Adapter.Requery = true;

            gibuilder = new OracleCommandBuilder(gi01Adapter);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //DataColumn col_in001_3 = new DataColumn("IN001", typeof(string));   // 财政发票编号
            //DataColumn col_in002 = new DataColumn("IN002", typeof(string));     // 项目代码
            //DataColumn col_in003 = new DataColumn("IN003", typeof(string));     // 财政发票项目名
            //DataColumn col_status3 = new DataColumn("STATUS", typeof(string));  // 状态
            //In01 = new DataTable("In01");
            //In01.Columns.AddRange(new DataColumn[]
            //    {col_in001_3,col_in002, col_in003,col_status3 });
            //In01.PrimaryKey = new DataColumn[] { col_in001_3 };                //设置主键

            //this.Tables.Add(In01);

            //in01Adapter = new OracleDataAdapter("select * from in01 order by in001", SqlAssist.conn);

            //inbuilder = new OracleCommandBuilder(in01Adapter);


        }

        public void Fill_Si01()
        {
            Si01.Rows.Clear();
            this.si01Adapter.Fill(Si01);
        }

        public void Fill_Gi01()
        {
            Gi01.Rows.Clear();
            this.gi01Adapter.Fill(Gi01);
        }
    }
}
