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
    class Cb01_ds : System.Data.DataSet
    {
        public DataTable Cb01 { get; }
        public DataTable Cb02 { get; }
        public DataTable AllItem { get; }

        public DataTable BindingService { get; }    //绑定服务

        public OracleDataAdapter cb01Adapter { get; }
        public OracleDataAdapter cb02Adapter { get; }
        public OracleDataAdapter allItemAdapter { get; }

        public OracleDataAdapter bindingAdapter { get; }

        OracleCommandBuilder builder = null;

        public Cb01_ds()
        {
            DataColumn col_cb001 = new DataColumn("CB001", typeof(string));   // 套餐编号             
            DataColumn col_cb002 = new DataColumn("CB002", typeof(string));   // 套餐类别 0-服务套餐 1-用户定义套餐
            DataColumn col_cb003 = new DataColumn("CB003", typeof(string));   // 套餐名称
            DataColumn col_cb005 = new DataColumn("CB005", typeof(string));   // 关联服务类别
            DataColumn col_status = new DataColumn("STATUS", typeof(string)); // 状态

            Cb01 = new DataTable("Cb01");
            Cb01.Columns.AddRange(new DataColumn[]
                {col_cb001,col_cb002,col_cb003,col_cb005,col_status});
            Cb01.PrimaryKey = new DataColumn[] { col_cb001 };                //设置主键

            this.Tables.Add(Cb01);

            cb01Adapter = new OracleDataAdapter("select * from cb01 where status = '1' order by cb001", SqlAssist.conn);

            builder = new OracleCommandBuilder(cb01Adapter);


            DataColumn col_cb201 = new DataColumn("CB201", typeof(string));   // 套餐明细编号             
            DataColumn col_cb001_2 = new DataColumn("CB001", typeof(string)); // 套餐编号
            DataColumn col_cb021 = new DataColumn("CB021", typeof(string));   // 服务或商品编号
            DataColumn col_cb022 = new DataColumn("CB022", typeof(string));   // 服务类别 01-守灵厅 02-冷藏柜 03-休息室 04-告别厅 05-殡仪服务 06-火化 07-灵车 10-谷类 11-纸类 12-祭品
            DataColumn col_cb030 = new DataColumn("STATUS", typeof(int));     // 数量

            Cb02 = new DataTable("Cb02");
            Cb02.Columns.AddRange(new DataColumn[]
                {col_cb201,col_cb001_2,col_cb021,col_cb022,col_cb030});
            Cb02.PrimaryKey = new DataColumn[] { col_cb201 };                //设置主键

            this.Tables.Add(Cb02);

            cb02Adapter = new OracleDataAdapter("select * from cb02  order by cb201", SqlAssist.conn);

            builder = new OracleCommandBuilder(cb02Adapter);

            BindingService = new DataTable("BINDINGSERVICE");
            this.Tables.Add(BindingService);

            bindingAdapter = new OracleDataAdapter("select * from V_BINDINGSERVICE ",SqlAssist.conn);

            AllItem = new DataTable("AllItem");
            this.Tables.Add(AllItem);
            allItemAdapter = new OracleDataAdapter("select * from v_allvaliditem order by item_type,sortId", SqlAssist.conn);
        }
    }
}
