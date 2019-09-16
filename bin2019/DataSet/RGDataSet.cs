using JEast.Misc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JEast.DataSet
{
    /// <summary>
    /// 寄存结构Dataset
    /// </summary>
    public class RGDataSet : System.Data.DataSet
    {
        public DataTable Bi01 { get; set; }
        public DataTable Rg01 { get; set; }
        public DataTable Ly01 { get; set; }

        public OracleDataAdapter bi01Adapter { get; set; }
        public OracleDataAdapter rg01Adapter { get; set; }
        public OracleDataAdapter ly01Adapter { get; set; }

        OracleCommandBuilder builder = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public RGDataSet()
        {
            ///1.Bi01
            DataColumn BI001 = new DataColumn("BI001", typeof(string));  //号位编号
            BI001.Unique = true;

            DataColumn RG001_2 = new DataColumn("RG001", typeof(string));//架编号
            DataColumn BI020 = new DataColumn("BI020", typeof(string));  //寄存室编号
            DataColumn BI030 = new DataColumn("BI030", typeof(string));  //寄存楼编号
            DataColumn BI002 = new DataColumn("BI002", typeof(int));     //号位数字编号
            DataColumn BI003 = new DataColumn("BI003", typeof(string));  //号位文字描述
            DataColumn BI005 = new DataColumn("BI005", typeof(int));     //层数
            DataColumn BI007 = new DataColumn("BI007", typeof(string));  //价格锁 0-否 1-是
            DataColumn BI008 = new DataColumn("BI008", typeof(int));     //列数
            DataColumn BI009 = new DataColumn("BI009", typeof(decimal)); //价格
            DataColumn BI010 = new DataColumn("BI010", typeof(string));  //寄存逝者编号
            DataColumn STATUS = new DataColumn("STATUS", typeof(string));//状态 0-未用 1-占用 9-空闲

            Bi01 = new DataTable("Bi01");
            Bi01.Columns.AddRange(new DataColumn[]
                {BI001,RG001_2,BI020,BI030,BI002,BI003,BI005,BI007,BI008,BI009,BI010,STATUS});
            Bi01.PrimaryKey = new DataColumn[] { BI001 };  //设置主键
            this.Tables.Add(Bi01);
            /////////////////////////////////////////////////////////////////////////////////////////////

            bi01Adapter = new OracleDataAdapter("select * from bi01", SqlAssist.conn);
            builder = new OracleCommandBuilder(bi01Adapter);

            /// 2. Rg01
            Rg01 = new DataTable("Rg01");
            DataColumn RG001 = new DataColumn("RG001", typeof(string));  //结构编号
            DataColumn RG002 = new DataColumn("RG002", typeof(string));  //类型 0-顶级节点 1-寄存堂 2-寄存室 3-排
            DataColumn RG003 = new DataColumn("RG003", typeof(string));  //结构单元描述
            DataColumn RG009 = new DataColumn("RG009", typeof(string));  //父节点编号
            DataColumn RG010 = new DataColumn("RG010", typeof(int));     //单元起始编号
            DataColumn RG011 = new DataColumn("RG011", typeof(int));     //单元截至编号
            DataColumn RG020 = new DataColumn("RG020", typeof(int));     //层数
            DataColumn RG021 = new DataColumn("RG021", typeof(int));     //每层号位数
            DataColumn RG030 = new DataColumn("RG030", typeof(string));  //起始位置 0-左上 1-左下 2-右上 3-右下
            DataColumn RG031 = new DataColumn("RG031", typeof(string));  //起始方向 0-行 1-列
            DataColumn RG033 = new DataColumn("RG033", typeof(string));  //排列顺序 0-顺序 1-蛇形
            DataColumn STATUS_2 = new DataColumn("STATUS", typeof(string));

            Rg01.Columns.AddRange(new DataColumn[]
                { RG001,RG002,RG003,RG009,RG010,RG011,RG020,RG021,RG030,RG031,RG033,STATUS_2});
            Rg01.PrimaryKey = new DataColumn[] { RG001 };  //设置主键

            this.Tables.Add(Rg01);
            rg01Adapter = new OracleDataAdapter("select * from rg01 order by rg001", SqlAssist.conn);
            builder = new OracleCommandBuilder(rg01Adapter);

            ////3. Ly01
            DataColumn LY001 = new DataColumn("LY001", typeof(string));    //层编号
            DataColumn RG001_3 = new DataColumn("RG001", typeof(string));  //架编号
            DataColumn LY002 = new DataColumn("LY002", typeof(int));       //层号
            DataColumn PRICE = new DataColumn("PRICE", typeof(decimal));   //层定价
            Ly01 = new DataTable("Ly01");

            Ly01.Columns.AddRange(new DataColumn[]
                { LY001,RG001_3,LY002,PRICE});

            this.Tables.Add(Ly01);
            ly01Adapter = new OracleDataAdapter("select * from ly01", SqlAssist.conn);
            builder = new OracleCommandBuilder(ly01Adapter);


        }

    }
}
