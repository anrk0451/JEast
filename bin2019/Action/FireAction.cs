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
    class FireAction
    {
        /// <summary>
        /// 火化登记记录删除
        /// </summary>
        /// <param name="ac001"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static int RemoveFireCheckin(string ac001, string handler)
        {
            //逝者编号
            OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
            op_ac001.Direction = ParameterDirection.Input;
            op_ac001.Value = ac001;

            //经办人
            OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
            op_handler.Direction = ParameterDirection.Input;
            op_handler.Value = handler;

            return SqlAssist.ExecuteProcedure("pkg_business.prc_removeFireCheckin", new OracleParameter[] { op_ac001, op_handler });
        }

        //获取逝者休息室列表
        public static string GetRestRoomList(string ac001)
        {
            OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
            op_ac001.Direction = ParameterDirection.Input;
            op_ac001.Value = ac001;

            return SqlAssist.ExecuteScalar("select pkg_business.fun_getRestRoomList(:ac001) from dual", new OracleParameter[] { op_ac001 }).ToString();
        }

        //获取逝者告别时间
        public static Object GetGBTime(string ac001)
        {
            OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
            op_ac001.Direction = ParameterDirection.Input;
            op_ac001.Value = ac001;
            Object re = SqlAssist.ExecuteScalar("select ac018 from ac01 where ac001 = :ac001", new OracleParameter[] { op_ac001 });
            return re;
        }

        //获取逝者火化时间
        public static Object GetHHTime(string ac001)
        {
            OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
            op_ac001.Direction = ParameterDirection.Input;
            op_ac001.Value = ac001;
            Object re = SqlAssist.ExecuteScalar("select ac015 from ac01 where ac001 = :ac001", new OracleParameter[] { op_ac001 });
            return re;
        }

        /// <summary>
        /// 判断火化业务是否结算
        /// </summary>
        /// <param name="ac001"></param>
        /// <returns></returns>
        public static string FireIsSettled(string ac001)
        {
            OracleCommand cmd = new OracleCommand("pkg_business.fun_FireIsSettled", SqlAssist.conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter returnValue = new OracleParameter("result", OracleDbType.Varchar2, 3);
            returnValue.Direction = ParameterDirection.ReturnValue;

            OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
            op_ac001.Direction = ParameterDirection.Input;
            op_ac001.Size = 10;
            op_ac001.Value = ac001;

            try
            {
                cmd.Parameters.Add(returnValue);
                cmd.Parameters.Add(op_ac001);

                cmd.ExecuteNonQuery();
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show("执行过程错误!\n" + e.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmd.Dispose();
            }

            return returnValue.Value.ToString();
        }

		/// <summary>
		/// 守灵厅办理
		/// </summary>
		/// <param name="ac001"></param>
		/// <param name="si001"></param>
		/// <param name="nums"></param>
		/// <param name="so005"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static int FireSales_01(string ac001, string si001, decimal nums, DateTime so005, string handler)
		{
			//逝者编号
			OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;

			//守灵厅编号
			OracleParameter op_si001 = new OracleParameter("ic_si001", OracleDbType.Varchar2, 10);
			op_si001.Direction = ParameterDirection.Input;
			op_si001.Value = si001;

			//占用天数
			OracleParameter op_nums = new OracleParameter("in_nums", OracleDbType.Int16);
			op_nums.Direction = ParameterDirection.Input;
			op_nums.Value = nums;

			//存放开始时间
			OracleParameter op_so005 = new OracleParameter("id_so005", OracleDbType.Date);
			op_so005.Direction = ParameterDirection.Input;
			op_so005.Value = so005;

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_FireBusiness01", 
				new OracleParameter[] { op_ac001,op_si001,op_nums,op_so005, op_handler });
		}

		/// <summary>
		/// 冷藏柜办理
		/// </summary>
		/// <param name="ac001"></param>
		/// <param name="si001"></param>
		/// <param name="nums"></param>
		/// <param name="so005"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static int FireSales_02(string ac001, string si001, decimal nums, DateTime so005, string handler)
		{
			//逝者编号
			OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;

			//冷藏编号
			OracleParameter op_si001 = new OracleParameter("ic_si001", OracleDbType.Varchar2, 10);
			op_si001.Direction = ParameterDirection.Input;
			op_si001.Value = si001;

			//占用天数
			OracleParameter op_nums = new OracleParameter("in_nums", OracleDbType.Int16);
			op_nums.Direction = ParameterDirection.Input;
			op_nums.Value = nums;

			//存放开始时间
			OracleParameter op_so005 = new OracleParameter("id_so005", OracleDbType.Date);
			op_so005.Direction = ParameterDirection.Input;
			op_so005.Value = so005;

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_FireBusiness02",
				new OracleParameter[] { op_ac001, op_si001, op_nums, op_so005, op_handler });
		}


		/// <summary>
		/// 休息室办理
		/// </summary>
		/// <param name="ac001"></param>
		/// <param name="si001"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static int FireSales_03(string ac001, string si001,string handler)
		{
			//逝者编号
			OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;

			//休息室编号
			OracleParameter op_si001 = new OracleParameter("ic_si001", OracleDbType.Varchar2, 10);
			op_si001.Direction = ParameterDirection.Input;
			op_si001.Value = si001;

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_FireBusiness03",
				new OracleParameter[] { op_ac001, op_si001, op_handler });
		}

		/// <summary>
		/// 告别办理
		/// </summary>
		/// <param name="ac001"></param>
		/// <param name="si001"></param>
		/// <param name="so005"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static int FireSales_04(string ac001, string si001, DateTime so005, string handler)
		{
			//逝者编号
			OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;

			//告别厅编号
			OracleParameter op_si001 = new OracleParameter("ic_si001", OracleDbType.Varchar2, 10);
			op_si001.Direction = ParameterDirection.Input;
			op_si001.Value = si001;

			//存放开始时间
			OracleParameter op_so005 = new OracleParameter("id_so005", OracleDbType.Date);
			op_so005.Direction = ParameterDirection.Input;
			op_so005.Value = so005;

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_FireBusiness04",
				new OracleParameter[] { op_ac001, op_si001,op_so005, op_handler });
		}


		public static int FireSales_07(string ac001, string si001, string handler)
		{
			//逝者编号
			OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;

			//告别厅编号
			OracleParameter op_si001 = new OracleParameter("ic_si001", OracleDbType.Varchar2, 10);
			op_si001.Direction = ParameterDirection.Input;
			op_si001.Value = si001;
 
			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_FireBusiness07",
				new OracleParameter[] { op_ac001, op_si001, op_handler });
		}

		public static int FireSales_06(string ac001, string si001, DateTime so005, string handler)
		{
			//逝者编号
			OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;

			//火化编号
			OracleParameter op_si001 = new OracleParameter("ic_si001", OracleDbType.Varchar2, 10);
			op_si001.Direction = ParameterDirection.Input;
			op_si001.Value = si001;

			//火化时间
			OracleParameter op_so005 = new OracleParameter("id_so005", OracleDbType.Date);
			op_so005.Direction = ParameterDirection.Input;
			op_so005.Value = so005;

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_FireBusiness06",
				new OracleParameter[] { op_ac001, op_si001, op_so005, op_handler });
		}

		public static int FireSales_Misc(string ac001, string itemId, int nums, string handler)
		{
			//逝者编号
			OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;

			//编号
			OracleParameter op_itemId = new OracleParameter("ic_itemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			//数量
			OracleParameter op_nums = new OracleParameter("in_nums", OracleDbType.Int16);
			op_nums.Direction = ParameterDirection.Input;
			op_nums.Value = nums;

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_FireBusinessMisc",
				new OracleParameter[] { op_ac001, op_itemId, op_nums, op_handler });
		}

		public static int FireSalesEdit(string sa001, decimal price, decimal nums, string handler)
		{
			//逝者编号
			OracleParameter op_sa001 = new OracleParameter("ic_sa001", OracleDbType.Varchar2, 10);
			op_sa001.Direction = ParameterDirection.Input;
			op_sa001.Value = sa001;

			//单价
			OracleParameter op_price = new OracleParameter("in_price", OracleDbType.Decimal);
			op_price.Direction = ParameterDirection.Input;
			op_price.Value = price;

			//数量
			OracleParameter op_nums = new OracleParameter("in_nums", OracleDbType.Decimal);
			op_nums.Direction = ParameterDirection.Input;
			op_nums.Value = nums;

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_FireBusinessEdit",
				new OracleParameter[] { op_sa001, op_price, op_nums, op_handler });
		}


		/// <summary>
		/// 火化业务-删除业务项目!!!
		/// </summary>
		/// <param name="sa001"></param>
		/// <returns></returns>
		public static int FireBusinessRemove(string sa001)
		{
			//销售记录编号
			OracleParameter op_sa001 = new OracleParameter("ic_sa001", OracleDbType.Varchar2, 10);
			op_sa001.Direction = ParameterDirection.Input;
			op_sa001.Value = sa001;
 
			return SqlAssist.ExecuteProcedure("pkg_business.prc_FireBusinessRemove",
				new OracleParameter[] { op_sa001 });
		}

		public static int FireApplyUserCombo(string ac001, string cb001,string handler)
		{
			//逝者编号
			OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;

			//套餐编号
			OracleParameter op_cb001 = new OracleParameter("ic_cb001", OracleDbType.Varchar2, 10);
			op_cb001.Direction = ParameterDirection.Input;
			op_cb001.Value = cb001;

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_ApplyUserCombo",
				new OracleParameter[] { op_ac001,op_cb001,op_handler });
		}

		/// <summary>
		/// 火化业务结算
		/// </summary>
		/// <param name="settleId"></param>
		/// <param name="ac001"></param>
		/// <param name="sa001_arry"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static int FireBusinessSettle(string settleId, string ac001, string[] sa001_arry, string handler)
		{
			//结算流水号
			OracleParameter op_settleId = new OracleParameter("ic_settleId", OracleDbType.Varchar2, 10);
			op_settleId.Direction = ParameterDirection.Input;
			op_settleId.Value = settleId;
			//逝者编号
			OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;
			//销售记录编号数组
			OracleParameter op_sa001_arry = new OracleParameter("ic_sa001_arry", OracleDbType.Varchar2);
			op_sa001_arry.Direction = ParameterDirection.Input;
			op_sa001_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_sa001_arry.Value = sa001_arry;

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_FireBusinessSettle",
				new OracleParameter[] { op_settleId, op_ac001, op_sa001_arry, op_handler });

		}

		/// <summary>
		/// 临时性销售结算
		/// </summary>
		/// <param name="cuname"></param>
		/// <param name="settleId"></param>
		/// <param name="itemId_arry"></param>
		/// <param name="itemType_arry"></param>
		/// <param name="price_arry"></param>
		/// <param name="nums_arry"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static int TempSalesSettle(string cuname, string settleId, string[] itemId_arry,string[] itemType_arry,decimal[] price_arry,decimal[] nums_arry, string handler)
		{
			//客户名称
			OracleParameter op_cuname = new OracleParameter("ic_cuname", OracleDbType.Varchar2, 100);
			op_cuname.Direction = ParameterDirection.Input;
			op_cuname.Value = cuname;
			//结算流水号
			OracleParameter op_settleId = new OracleParameter("ic_settleId", OracleDbType.Varchar2);
			op_settleId.Direction = ParameterDirection.Input;
			op_settleId.Value = settleId;

			//销售项目编号数组
			OracleParameter op_itemId_arry = new OracleParameter("ic_itemId_arry", OracleDbType.Varchar2);
			op_itemId_arry.Direction = ParameterDirection.Input;
			op_itemId_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_itemId_arry.Value = itemId_arry;

			//销售项目类型数组
			OracleParameter op_itemType_arry = new OracleParameter("ic_itemType_arry", OracleDbType.Varchar2);
			op_itemType_arry.Direction = ParameterDirection.Input;
			op_itemType_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_itemType_arry.Value = itemType_arry;

			//销售项目单价数组
			OracleParameter op_price_arry = new OracleParameter("in_price_arry", OracleDbType.Decimal);
			op_price_arry.Direction = ParameterDirection.Input;
			op_price_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_price_arry.Value = price_arry;

			//销售项目数量数组
			OracleParameter op_nums_arry = new OracleParameter("in_nums_arry", OracleDbType.Decimal);
			op_nums_arry.Direction = ParameterDirection.Input;
			op_nums_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
			op_nums_arry.Value = nums_arry;

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_TempSalesSettle",
				new OracleParameter[] { op_cuname, op_settleId,op_itemId_arry,op_itemType_arry,op_price_arry,op_nums_arry,op_handler });
		}

		/// <summary>
		/// 火化证明打印日志
		/// </summary>
		/// <param name="ac001"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static int FireCertLog(string ac001, string handler)
		{
			//逝者编号
			OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;
			//结算流水号
			OracleParameter op_fc100 = new OracleParameter("ic_fc100", OracleDbType.Varchar2,10);
			op_fc100.Direction = ParameterDirection.Input;
			op_fc100.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_FireCertLog", new OracleParameter[] {op_ac001,op_fc100});
		}

		/// <summary>
		/// 生成火化序号
		/// </summary>
		/// <param name="hhl_type"></param>
		/// <returns></returns>
		public static int GenFireOrder(string hhl_type)
		{
			OracleParameter op_hhl = new OracleParameter("ic_hhl", OracleDbType.Varchar2, 3);
			op_hhl.Direction = ParameterDirection.Input;
			op_hhl.Value = hhl_type;

			Object re = SqlAssist.ExecuteFunction("pkg_business.fun_GenFireOrder", new OracleParameter[] { op_hhl });
			return int.Parse(re.ToString());
		}

		public static string Get_PassbyName(string ac001)
		{
			return SqlAssist.ExecuteScalar("select p_name from v_all_passby where p_id='" + ac001 + "'").ToString();
		}

	}
}
