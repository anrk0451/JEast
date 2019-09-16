using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JEast.Misc
{
    static class SqlAssist
    {
        public static OracleConnection conn = null;

        /// <summary>
        /// // 创建数据库连接并 连接数据库 
        /// </summary>
        public static void ConnectDb()
        {

            conn = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            //conn = new OracleConnection("Password=bestunion;User ID=JEast;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.1.80)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)))");
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show("数据库连接失败!\n" + e.ToString(), "错误");

                /////////// 最干净的退出方式 //////////
                System.Environment.Exit(0);
            }
        }

        /// <summary>
        /// 销毁数据库连接
        /// </summary>
        public static void DisConnect()
        {
            conn.Close();
            conn.Dispose();
            //MessageBox.Show("我被销毁!");
        }

        #region ExecuteNonQuery命令

        /// <summary>  
        /// 执行不带参数的sql语句
        /// </summary>  
        /// <param name="safeSql"> Sql语句</param>  
        /// /// <returns>受影响的记录数</returns>  
        public static int ExecuteNonQuery(string safeSql)
        {
            OracleTransaction trans = conn.BeginTransaction();
            try
            {
                OracleCommand cmd = new OracleCommand(safeSql, conn);
                cmd.Transaction = trans;
                int result = cmd.ExecuteNonQuery();
                trans.Commit();
                return result;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "错误");
                trans.Rollback();
                return 0;
            }
        }

        /// <summary>
        /// 执行带参数的sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, OracleParameter[] values)
        {
            OracleTransaction trans = null;
            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                try
                {
                    trans = conn.BeginTransaction();
                    cmd.Transaction = trans;
                    cmd.Parameters.AddRange(values);
                    int result = cmd.ExecuteNonQuery();
                    trans.Commit();
                    return result;

                }
                catch (Exception e)
                {
                    trans.Rollback();
                    MessageBox.Show("执行命令失败!" + e.ToString());
                    return 0;
                }
            }
        }

        #endregion

        #region ExecuteScalar命令

        /// 创建数据读取器 ///
        /// ///////////////////////////////////////
        /// 
        public static OracleDataReader ExecuteReader(string safeSql)
        {
            OracleCommand cmd = new OracleCommand(safeSql, conn);
            OracleDataReader reader = cmd.ExecuteReader();
            cmd.Dispose();
            return reader;
        }
        #endregion

        /////// 创建一个单表的 数据适配器对象  ////////
        public static OracleDataAdapter getSingleTableAdapter(string sql)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleDataAdapter adapter = new OracleDataAdapter(sql, connStr);
            OracleCommandBuilder builder = new OracleCommandBuilder(adapter);
            return adapter;
        }

        /// <summary>
        /// 执行一条SQL语句,返回首行首列
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>首行首列</returns>
        public static object ExecuteScalar(string sql, params OracleParameter[] sp)
        {
            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                if (sp != null) cmd.Parameters.AddRange(sp);
                return cmd.ExecuteScalar();
            }
        }


        public static int ExecuteProcedure(string procname, OracleParameter[] paras)
        {
            OracleCommand cmd = new OracleCommand(procname, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            OracleTransaction trans = null;

            OracleParameter appcode = new OracleParameter("on_appcode", OracleDbType.Int16);
            appcode.Direction = ParameterDirection.Output;
            OracleParameter apperror = new OracleParameter("oc_error", OracleDbType.Varchar2, 100);
            apperror.Direction = ParameterDirection.Output;

            try
            {
                trans = conn.BeginTransaction();

                cmd.Parameters.AddRange(paras);
                cmd.Parameters.Add(appcode);
                cmd.Parameters.Add(apperror);
                cmd.ExecuteNonQuery();

                if (int.Parse(appcode.Value.ToString()) < 0)
                {
                    trans.Rollback();
                    MessageBox.Show(apperror.Value.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }

                trans.Commit();
                return 1;
            }
            catch (InvalidOperationException e)
            {
                trans.Rollback();
                MessageBox.Show("执行过程错误!\n" + e.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            finally
            {
                cmd.Dispose();
            }
        }

		public static object ExecuteFunction(string funcname, OracleParameter[] paras)
		{
			OracleCommand cmd = new OracleCommand(funcname, SqlAssist.conn);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;

			OracleParameter returnValue = new OracleParameter("result", OracleDbType.Varchar2, 500);
			returnValue.Direction = ParameterDirection.ReturnValue;
			 
			try
			{
				cmd.Parameters.Add(returnValue);
				cmd.Parameters.AddRange(paras);
				cmd.ExecuteNonQuery();
			}
			catch (InvalidOperationException e)
			{
				MessageBox.Show("执行过程出错!\n" + e.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				cmd.Dispose();
			}

			return returnValue.Value;
		}


	}
}
