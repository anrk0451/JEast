using JEast.Misc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JEast.Action
{
	class MiscAction
	{
		/// <summary>
		/// 返回服务或商品名
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static string GetItemFullName(string itemId)
		{
			OracleParameter op_itemId = new OracleParameter("ic_itemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return SqlAssist.ExecuteScalar("select pkg_business.fun_getItemFullName(:itemId) from dual", new OracleParameter[] { op_itemId }).ToString();
		}


		public static decimal GetItemFixPrice(string itemId)
		{
			OracleParameter op_itemId = new OracleParameter("ic_itemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return decimal.Parse(SqlAssist.ExecuteScalar("select pkg_business.fun_getFixPrice(:itemId) from dual", new OracleParameter[] { op_itemId }).ToString());
		}

		/// <summary>
		/// 保存开票客户端信息
		/// </summary>
		/// <param name="addr"></param>
		/// <param name="bank"></param>
		/// <param name="fplx"></param>
		/// <param name="cert"></param>
		/// <param name="ver"></param>
		/// <returns></returns>
		public static int SaveTaxInfo(string addr,string bank,string fplx,string cert,string ver)
		{
			//销方地址电话
			OracleParameter op_addr = new OracleParameter("ic_addr", OracleDbType.Varchar2, 100);
			op_addr.Direction = ParameterDirection.Input;
			op_addr.Value = addr;

			//销方银行账号
			OracleParameter op_bank = new OracleParameter("ic_bank", OracleDbType.Varchar2, 100);
			op_bank.Direction = ParameterDirection.Input;
			op_bank.Value = bank;

			//发票类型
			OracleParameter op_fplx = new OracleParameter("ic_fplx", OracleDbType.Varchar2, 3);
			op_fplx.Direction = ParameterDirection.Input;
			op_fplx.Value = fplx;

			//证书密码
			OracleParameter op_cert = new OracleParameter("ic_cert", OracleDbType.Varchar2, 50);
			op_cert.Direction = ParameterDirection.Input;
			op_cert.Value = cert;

			//税收分类编码版本
			OracleParameter op_ver = new OracleParameter("ic_ver", OracleDbType.Varchar2, 50);
			op_ver.Direction = ParameterDirection.Input;
			op_ver.Value = ver;
 
			return SqlAssist.ExecuteProcedure("pkg_business.prc_SaveTaxInfo", 
				new OracleParameter[] {op_addr,op_bank,op_fplx,op_cert,op_ver});
		}

		/// <summary>
		/// 操作员姓名映射
		/// </summary>
		/// <param name="uc001"></param>
		/// <returns></returns>
		public static string Mapper_operator(string uc001)
		{
			OracleParameter op_uc001 = new OracleParameter("uc001", OracleDbType.Varchar2, 10);
			op_uc001.Direction = ParameterDirection.Input;
			op_uc001.Value = uc001;
			Object re = SqlAssist.ExecuteScalar("select uc003 from uc01 where uc001 = :uc001", new OracleParameter[] { op_uc001 });
			return re.ToString();
		}

		/// <summary>
		/// 财务收款作废
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static int FinanceRemove(string fa001,string handler)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			//结算流水号
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = fa001;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_FinanceRemove",
				new OracleParameter[] {op_fa001,op_handler});
		}

		public static int Modify_Pwd(string uc001,string newpwd)
		{
			//用户编号
			OracleParameter op_uc001 = new OracleParameter("ic_uc001", OracleDbType.Varchar2, 10);
			op_uc001.Direction = ParameterDirection.Input;
			op_uc001.Value = uc001;

			//新密码
			OracleParameter op_newpwd = new OracleParameter("ic_newPwd", OracleDbType.Varchar2, 50);
			op_newpwd.Direction = ParameterDirection.Input;
			op_newpwd.Value = newpwd;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_Modify_Pwd",
				new OracleParameter[] { op_uc001, op_newpwd});
		}


		/// <summary>
		/// 返回服务器 系统日期 字符串 yyyy-mm-dd
		/// </summary>
		/// <returns></returns>
		public static string GetServerDateString()
		{
			return SqlAssist.ExecuteScalar("select to_char(sysdate,'yyyy-mm-dd') from dual").ToString();
		}


		/// <summary>
		/// 返回销方银行账号
		/// </summary>
		/// <returns></returns>
		public static string GetTaxSellerBank()
		{
			return SqlAssist.ExecuteScalar("select sp005 from sp01 where sp002 = 'InfoSellerBankAccount' ").ToString();
		}

		/// <summary>
		/// 返回销方地址电话
		/// </summary>
		public static string GetTaxSellerAddress()
		{
			return SqlAssist.ExecuteScalar("select sp005 from sp01 where sp002 = 'InfoSellerAddressPhone' ").ToString();
		}

		/// <summary>
		/// 返回项目税务名称
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static string GetItemTaxName(string itemId)
		{
			return SqlAssist.ExecuteScalar("select tx003 from v_allItem where item_id ='" + itemId + "'" ).ToString();
		}

		////财务类别统计
		public static int ClassStat(string dbegin, string dend, string[] class_arry)
		{
			OracleCommand cmd = new OracleCommand("pkg_report.prc_ClassStat", SqlAssist.conn);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			OracleTransaction trans = null;
		 
			//统计日期1
			OracleParameter op_begin = new OracleParameter("ic_begin", OracleDbType.Varchar2, 16);
			op_begin.Direction = ParameterDirection.Input;
			op_begin.Value = dbegin;
			//统计日期2
			OracleParameter op_end = new OracleParameter("ic_end", OracleDbType.Varchar2, 16);
			op_end.Direction = ParameterDirection.Input;
			op_end.Value = dend;

			//销售记录编号数组
			OracleParameter op_class_arry = new OracleParameter("ic_class", OracleDbType.Varchar2);
			op_class_arry.Direction = ParameterDirection.Input;
			op_class_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_class_arry.Value = class_arry;

			//收费员
			//OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			//op_handler.Direction = ParameterDirection.Input;
			//op_handler.Value = handler;

			OracleParameter appcode = new OracleParameter("on_appcode", OracleDbType.Int16);
			appcode.Direction = ParameterDirection.Output;
			OracleParameter apperror = new OracleParameter("oc_error", OracleDbType.Varchar2, 100);
			apperror.Direction = ParameterDirection.Output;

			try
			{
				trans = SqlAssist.conn.BeginTransaction();
				cmd.Parameters.AddRange(new OracleParameter[] {  op_begin, op_end, op_class_arry, appcode, apperror });
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

		/// <summary>
		/// 返回 分类统计笔数
		/// </summary>
		/// <returns></returns>
		public static int GetClassStat_BS()
		{
			return Convert.ToInt32(SqlAssist.ExecuteScalar("select pkg_report.fun_GetClassStat_BS from dual"));
		}


	}
}
