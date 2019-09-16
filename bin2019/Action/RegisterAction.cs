using JEast.Misc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEast.Action
{
	class RegisterAction
	{
		//获取号位状态
		public static string GetBitStatus(string regionId, string bitDesc)
		{
			OracleParameter op_regionId = new OracleParameter("ic_regionId", OracleDbType.Varchar2, 10);
			op_regionId.Direction = ParameterDirection.Input;
			op_regionId.Value = regionId;

			OracleParameter op_bitDesc = new OracleParameter("ic_bitdesc", OracleDbType.Varchar2, 50);
			op_bitDesc.Direction = ParameterDirection.Input;
			op_bitDesc.Value = bitDesc;

			Object re = SqlAssist.ExecuteScalar("select pkg_business.fun_getBitStatus(:ic_regionId,:ic_bitdesc) from dual", new OracleParameter[] { op_regionId, op_bitDesc });
			return re.ToString();
		}

		//获取层定价
		public static decimal GetLayerPrice(string regionId, int layer)
		{
			OracleParameter op_regionId = new OracleParameter("ic_regionId", OracleDbType.Varchar2, 10);
			op_regionId.Direction = ParameterDirection.Input;
			op_regionId.Value = regionId;

			OracleParameter op_layerNum = new OracleParameter("ic_layerNum", OracleDbType.Int32);
			op_layerNum.Direction = ParameterDirection.Input;
			op_layerNum.Value = layer;

			Object re = SqlAssist.ExecuteScalar("select pkg_business.fun_getLayerPrice(:ic_regionId,:ic_layerNum) from dual", new OracleParameter[] { op_regionId, op_layerNum });
			return (decimal)re;
		}

		/// <summary>
		/// 根据寄存排 和 号位描述 返回号位编号
		/// </summary>
		/// <param name="regionId"></param>
		/// <param name="bitDesc"></param>
		/// <returns></returns>
		public static string GetBitId(string regionId, string bitDesc)
		{
			OracleParameter op_regionId = new OracleParameter("ic_regionId", OracleDbType.Varchar2, 10);
			op_regionId.Direction = ParameterDirection.Input;
			op_regionId.Value = regionId;

			OracleParameter op_bitDesc = new OracleParameter("ic_bitdesc", OracleDbType.Varchar2, 50);
			op_bitDesc.Direction = ParameterDirection.Input;
			op_bitDesc.Value = bitDesc;

			Object re = SqlAssist.ExecuteScalar("select pkg_business.fun_getBitId(:ic_regionId,:ic_bitdesc) from dual", new OracleParameter[] { op_regionId, op_bitDesc });
			return re.ToString();
		}

		/// <summary>
		/// 返回寄存号位定价
		/// </summary>
		/// <param name="regionId"></param>
		/// <param name="bitDesc"></param>
		/// <returns></returns>
		public static decimal GetBitPrice(string regionId, string bitDesc)
		{
			OracleParameter op_regionId = new OracleParameter("ic_regionId", OracleDbType.Varchar2, 10);
			op_regionId.Direction = ParameterDirection.Input;
			op_regionId.Value = regionId;

			OracleParameter op_bitDesc = new OracleParameter("ic_bitdesc", OracleDbType.Varchar2, 50);
			op_bitDesc.Direction = ParameterDirection.Input;
			op_bitDesc.Value = bitDesc;

			Object re = SqlAssist.ExecuteScalar("select pkg_business.fun_getBitPrice(:ic_regionId,:ic_bitdesc) from dual", new OracleParameter[] { op_regionId, op_bitDesc });
			return decimal.Parse(re.ToString());
		}

		/// <summary>
		/// 返回寄存号位定价
		/// </summary>
		/// <param name="bitId"></param>
		/// <returns></returns>
		public static decimal GetBitPrice(string bitId)
		{
			OracleParameter op_bitId = new OracleParameter("ic_bitId", OracleDbType.Varchar2, 10);
			op_bitId.Direction = ParameterDirection.Input;
			op_bitId.Value = bitId;

			Object re = SqlAssist.ExecuteScalar("select pkg_business.fun_getBitPrice(:ic_bitId) from dual", new OracleParameter[] { op_bitId });
			return decimal.Parse(re.ToString());
		}




		public static string GetBitFullName(string regionId, string bitDesc)
		{
			OracleParameter op_regionId = new OracleParameter("ic_regionId", OracleDbType.Varchar2, 10);
			op_regionId.Direction = ParameterDirection.Input;
			op_regionId.Value = regionId;

			OracleParameter op_bitDesc = new OracleParameter("ic_bitdesc", OracleDbType.Varchar2, 50);
			op_bitDesc.Direction = ParameterDirection.Input;
			op_bitDesc.Value = bitDesc;

			Object re = SqlAssist.ExecuteScalar("select pkg_business.fun_getBitFullName(:ic_regionId,:ic_bitdesc) from dual", new OracleParameter[] { op_regionId, op_bitDesc });
			return re.ToString();
		}

		/// <summary>
		/// 生成 寄存证号
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string GenRegisterNo(string type)
		{
			OracleParameter op_type = new OracleParameter("ic_type", OracleDbType.Varchar2, 3);
			op_type.Direction = ParameterDirection.Input;
			op_type.Value = type;

			Object re = SqlAssist.ExecuteFunction("pkg_business.GenRegisterNo", new OracleParameter[] { op_type });
			return re.ToString();
		}

		public static int RegisterEnroll(string rc001, string rc109, string fa001, string rc002, string rc202, string rc003, string rc303, int rc004, int rc404,
			string rc014, string rc050, string rc051, string rc052, string rc055, string rc099, string rc130, decimal price, DateTime rc140, DateTime rc150,
			decimal nums, string source, string handler
			)
		{
			return RegisterEnroll(rc001, rc109, fa001, rc002, rc202, rc003, rc303, rc004, rc404, rc014, rc050, rc051, rc052, rc055, rc099, rc130, price, rc140, rc150, nums, source,
				null, null, null, handler);
		}

		//寄存办理
		public static int RegisterEnroll(string rc001, string rc109, string fa001, string rc002, string rc202, string rc003, string rc303, int rc004, int rc404,
			string rc014, string rc050, string rc051, string rc052, string rc055, string rc099, string rc130, decimal price, DateTime rc140, DateTime rc150,
			decimal nums, string source, string[] itemId_arry, decimal[] itemPrice_arry, int[] itemNums_arry, string handler
			)
		{
			//逝者编号
			OracleParameter op_rc001 = new OracleParameter("ic_rc001", OracleDbType.Varchar2, 10);
			op_rc001.Direction = ParameterDirection.Input;
			op_rc001.Value = rc001;
			//寄存证号
			OracleParameter op_rc109 = new OracleParameter("ic_rc109", OracleDbType.Varchar2, 20);
			op_rc109.Direction = ParameterDirection.Input;
			op_rc109.Value = rc109;
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;
			//性别
			OracleParameter op_rc002 = new OracleParameter("ic_rc002", OracleDbType.Varchar2, 3);
			op_rc002.Direction = ParameterDirection.Input;
			op_rc002.Value = rc002;
			//性别2
			OracleParameter op_rc202 = new OracleParameter("ic_rc202", OracleDbType.Varchar2, 3);
			op_rc202.Direction = ParameterDirection.Input;
			op_rc202.Value = rc202;
			//逝者姓名
			OracleParameter op_rc003 = new OracleParameter("ic_rc003", OracleDbType.Varchar2, 20);
			op_rc003.Direction = ParameterDirection.Input;
			op_rc003.Value = rc003;
			//逝者姓名2
			OracleParameter op_rc303 = new OracleParameter("ic_rc303", OracleDbType.Varchar2, 20);
			op_rc303.Direction = ParameterDirection.Input;
			op_rc303.Value = rc303;
			//年龄
			OracleParameter op_rc004 = new OracleParameter("ic_rc004", OracleDbType.Int32);
			op_rc004.Direction = ParameterDirection.Input;
			op_rc004.Value = rc004;
			//年龄2
			OracleParameter op_rc404 = new OracleParameter("ic_rc404", OracleDbType.Int32);
			op_rc404.Direction = ParameterDirection.Input;
			op_rc404.Value = rc404;
			//身份证号
			OracleParameter op_rc014 = new OracleParameter("ic_rc014", OracleDbType.Varchar2, 18);
			op_rc014.Direction = ParameterDirection.Input;
			op_rc014.Value = rc014;
			//联系人
			OracleParameter op_rc050 = new OracleParameter("ic_rc050", OracleDbType.Varchar2, 50);
			op_rc050.Direction = ParameterDirection.Input;
			op_rc050.Value = rc050;
			//联系电话
			OracleParameter op_rc051 = new OracleParameter("ic_rc051", OracleDbType.Varchar2, 50);
			op_rc051.Direction = ParameterDirection.Input;
			op_rc051.Value = rc051;
			//与逝者关系
			OracleParameter op_rc052 = new OracleParameter("ic_rc052", OracleDbType.Varchar2, 10);
			op_rc052.Direction = ParameterDirection.Input;
			op_rc052.Value = rc052;
			//联系地址
			OracleParameter op_rc055 = new OracleParameter("ic_rc055", OracleDbType.Varchar2, 80);
			op_rc055.Direction = ParameterDirection.Input;
			op_rc055.Value = rc055;
			//备注
			OracleParameter op_rc099 = new OracleParameter("ic_rc099", OracleDbType.Varchar2, 200);
			op_rc099.Direction = ParameterDirection.Input;
			op_rc099.Value = rc099;
			//寄存号位编号
			OracleParameter op_rc130 = new OracleParameter("ic_rc130", OracleDbType.Varchar2, 10);
			op_rc130.Direction = ParameterDirection.Input;
			op_rc130.Value = rc130;
			//寄存号位价格
			OracleParameter op_price = new OracleParameter("in_price", OracleDbType.Decimal);
			op_price.Direction = ParameterDirection.Input;
			op_price.Value = price;
			//寄存日期
			OracleParameter op_rc140 = new OracleParameter("id_rc140", OracleDbType.Date);
			op_rc140.Direction = ParameterDirection.Input;
			op_rc140.Value = rc140;
			//寄存到期日期
			OracleParameter op_rc150 = new OracleParameter("id_rc150", OracleDbType.Date);
			op_rc150.Direction = ParameterDirection.Input;
			op_rc150.Value = rc150;
			//缴费年限
			OracleParameter op_nums = new OracleParameter("in_nums", OracleDbType.Decimal);
			op_nums.Direction = ParameterDirection.Input;
			op_nums.Value = nums;
			//寄存来源
			OracleParameter op_source = new OracleParameter("ic_source", OracleDbType.Varchar2, 3);
			op_source.Direction = ParameterDirection.Input;
			op_source.Value = source;

			OracleParameter op_itemId_arry = null;
			OracleParameter op_price_arry = null;
			OracleParameter op_nums_arry = null;

			if (itemId_arry != null)
			{
				//项目编号数组
				op_itemId_arry = new OracleParameter("ic_itemId_arry", OracleDbType.Varchar2);
				op_itemId_arry.Direction = ParameterDirection.Input;
				op_itemId_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
				op_itemId_arry.Size = itemId_arry.Count();
				op_itemId_arry.Value = itemId_arry;

				//单价数组
				op_price_arry = new OracleParameter("in_itemPrice_arry", OracleDbType.Decimal);
				op_price_arry.Direction = ParameterDirection.Input;
				op_price_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
				op_price_arry.Size = itemPrice_arry.Count();
				op_price_arry.Value = itemPrice_arry;


				//数量数组
				op_nums_arry = new OracleParameter("in_itemNums_arry", OracleDbType.Int32);
				op_nums_arry.Direction = ParameterDirection.Input;
				op_nums_arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
				op_nums_arry.Size = itemNums_arry.Count();
				op_nums_arry.Value = itemNums_arry;
			}

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			if (itemId_arry == null)
				return SqlAssist.ExecuteProcedure("pkg_business.prc_Register", new OracleParameter[]
				{op_rc001 , op_rc109, op_fa001, op_rc002, op_rc202, op_rc003, op_rc303, op_rc004, op_rc404,
				 op_rc014, op_rc050, op_rc051,op_rc052, op_rc055,op_rc099, op_rc130, op_price,op_rc140,
				 op_rc150, op_nums,op_source,op_handler});
			else
				return SqlAssist.ExecuteProcedure("pkg_business.prc_Register", new OracleParameter[]
				{op_rc001 , op_rc109, op_fa001, op_rc002, op_rc202, op_rc003, op_rc303, op_rc004, op_rc404,
				 op_rc014, op_rc050, op_rc051,op_rc052, op_rc055,op_rc099, op_rc130, op_price,op_rc140,
				 op_rc150, op_nums,op_source,op_itemId_arry,op_price_arry,op_nums_arry, op_handler});
		}

		/// <summary>
		/// 返回逝者 寄存位置
		/// </summary>
		/// <param name="ac001"></param>
		/// <returns></returns>
		public static string GetRegPathName(string ac001)
		{
			OracleParameter op_ac001 = new OracleParameter("ic_rc001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;

			Object re = SqlAssist.ExecuteFunction("pkg_business.fun_getRegPathName", new OracleParameter[] { op_ac001 });
			return re.ToString();
		}

		public static int RegisterEdit(string rc001, string rc002, string rc202, string rc003, string rc303, int rc004, int rc404,
			string rc014, string rc050, string rc051, string rc052, string rc055, string rc099)
		{

			//逝者编号
			OracleParameter op_rc001 = new OracleParameter("ic_rc001", OracleDbType.Varchar2, 10);
			op_rc001.Direction = ParameterDirection.Input;
			op_rc001.Value = rc001;
			//性别
			OracleParameter op_rc002 = new OracleParameter("ic_rc002", OracleDbType.Varchar2, 3);
			op_rc002.Direction = ParameterDirection.Input;
			op_rc002.Value = rc002;
			//性别2
			OracleParameter op_rc202 = new OracleParameter("ic_rc202", OracleDbType.Varchar2, 3);
			op_rc202.Direction = ParameterDirection.Input;
			op_rc202.Value = rc202;
			//逝者姓名
			OracleParameter op_rc003 = new OracleParameter("ic_rc003", OracleDbType.Varchar2, 20);
			op_rc003.Direction = ParameterDirection.Input;
			op_rc003.Value = rc003;
			//逝者姓名2
			OracleParameter op_rc303 = new OracleParameter("ic_rc303", OracleDbType.Varchar2, 20);
			op_rc303.Direction = ParameterDirection.Input;
			op_rc303.Value = rc303;
			//年龄
			OracleParameter op_rc004 = new OracleParameter("ic_rc004", OracleDbType.Int32);
			op_rc004.Direction = ParameterDirection.Input;
			op_rc004.Value = rc004;
			//年龄2
			OracleParameter op_rc404 = new OracleParameter("ic_rc404", OracleDbType.Int32);
			op_rc404.Direction = ParameterDirection.Input;
			op_rc404.Value = rc404;
			//身份证号
			OracleParameter op_rc014 = new OracleParameter("ic_rc014", OracleDbType.Varchar2, 18);
			op_rc014.Direction = ParameterDirection.Input;
			op_rc014.Value = rc014;
			//联系人
			OracleParameter op_rc050 = new OracleParameter("ic_rc050", OracleDbType.Varchar2, 50);
			op_rc050.Direction = ParameterDirection.Input;
			op_rc050.Value = rc050;
			//联系电话
			OracleParameter op_rc051 = new OracleParameter("ic_rc051", OracleDbType.Varchar2, 50);
			op_rc051.Direction = ParameterDirection.Input;
			op_rc051.Value = rc051;
			//与逝者关系
			OracleParameter op_rc052 = new OracleParameter("ic_rc052", OracleDbType.Varchar2, 10);
			op_rc052.Direction = ParameterDirection.Input;
			op_rc052.Value = rc052;
			//联系地址
			OracleParameter op_rc055 = new OracleParameter("ic_rc055", OracleDbType.Varchar2, 80);
			op_rc055.Direction = ParameterDirection.Input;
			op_rc055.Value = rc055;
			//备注
			OracleParameter op_rc099 = new OracleParameter("ic_rc099", OracleDbType.Varchar2, 200);
			op_rc099.Direction = ParameterDirection.Input;
			op_rc099.Value = rc099;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_RegisterEdit", new OracleParameter[]
				{op_rc001 , op_rc002, op_rc202, op_rc003, op_rc303, op_rc004, op_rc404,
				   op_rc014, op_rc050, op_rc051,op_rc052, op_rc055,op_rc099});

		}


		/// <summary>
		/// 寄存位置变更
		/// </summary>
		/// <param name="rc001"></param>
		/// <param name="bitId_old"></param>
		/// <param name="bitId_new"></param>
		/// <param name="rt003"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static int RegisterMove(string rc001, string bitId_old, string bitId_new, string rt003, string handler)
		{
			//逝者编号
			OracleParameter op_rc001 = new OracleParameter("ic_rc001", OracleDbType.Varchar2, 10);
			op_rc001.Direction = ParameterDirection.Input;
			op_rc001.Value = rc001;

			//原号位
			OracleParameter op_rc130_b = new OracleParameter("ic_rc130_b", OracleDbType.Varchar2, 10);
			op_rc130_b.Direction = ParameterDirection.Input;
			op_rc130_b.Value = bitId_old;

			//新号位
			OracleParameter op_rc130_a = new OracleParameter("ic_rc130_a", OracleDbType.Varchar2, 10);
			op_rc130_a.Direction = ParameterDirection.Input;
			op_rc130_a.Value = bitId_new;

			//变更原因
			OracleParameter op_rt003 = new OracleParameter("ic_rt003", OracleDbType.Varchar2, 100);
			op_rt003.Direction = ParameterDirection.Input;
			op_rt003.Value = rt003;

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_RegisterMove", new OracleParameter[]
				{op_rc001 , op_rc130_b,op_rc130_a,op_rt003,op_handler });

		}

		/// <summary>
		/// 续交寄存费
		/// </summary>
		/// <param name="rc001"></param>
		/// <param name="fa001"></param>
		/// <param name="price"></param>
		/// <param name="nums"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static int RegisterPay(string rc001, string fa001, decimal price, decimal nums, string handler)
		{
			//逝者编号
			OracleParameter op_rc001 = new OracleParameter("ic_rc001", OracleDbType.Varchar2, 10);
			op_rc001.Direction = ParameterDirection.Input;
			op_rc001.Value = rc001;

			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			//单价
			OracleParameter op_price = new OracleParameter("in_price", OracleDbType.Decimal);
			op_price.Direction = ParameterDirection.Input;
			op_price.Value = price;

			//缴费年限
			OracleParameter op_nums = new OracleParameter("in_nums", OracleDbType.Decimal);
			op_nums.Direction = ParameterDirection.Input;
			op_nums.Value = nums;

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_RegisterPay", new OracleParameter[]
				{op_rc001 ,op_fa001,op_price,op_nums,op_handler });
		}

		/// <summary>
		/// 计算 迁出差异天数
		/// </summary>
		/// <param name="rc001"></param>
		/// <returns></returns>
		public static int CalcOutDiffDays(string rc001)
		{
			OracleParameter op_rc001 = new OracleParameter("ic_rc001", OracleDbType.Varchar2, 10);
			op_rc001.Direction = ParameterDirection.Input;
			op_rc001.Value = rc001;

			Object re = SqlAssist.ExecuteFunction("pkg_business.fun_CalcOutDiffDays", new OracleParameter[] { op_rc001 });
			return Convert.ToInt32(re.ToString());
		}


		/// <summary>
		/// 寄存迁出办理
		/// </summary>
		/// <param name="rc001"></param>
		/// <param name="oc003"></param>
		/// <param name="oc004"></param>
		/// <param name="oc005"></param>
		/// <param name="oc030"></param>
		/// <param name="fa001"></param>
		/// <param name="price"></param>
		/// <param name="nums"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static int RegisterOut(string rc001, string oc003, string oc004, string oc005, int oc030, string fa001, decimal price, decimal nums, string handler)
		{
			//逝者编号
			OracleParameter op_rc001 = new OracleParameter("ic_rc001", OracleDbType.Varchar2, 10);
			op_rc001.Direction = ParameterDirection.Input;
			op_rc001.Value = rc001;

			//迁出人
			OracleParameter op_oc003 = new OracleParameter("ic_oc003", OracleDbType.Varchar2, 50);
			op_oc003.Direction = ParameterDirection.Input;
			op_oc003.Value = oc003;

			//迁出人身份证号
			OracleParameter op_oc004 = new OracleParameter("ic_oc004", OracleDbType.Varchar2, 20);
			op_oc004.Direction = ParameterDirection.Input;
			op_oc004.Value = oc004;

			//迁出原因
			OracleParameter op_oc005 = new OracleParameter("ic_oc005", OracleDbType.Varchar2, 100);
			op_oc005.Direction = ParameterDirection.Input;
			op_oc005.Value = oc005;

			//差异天数
			OracleParameter op_oc030 = new OracleParameter("ic_oc030", OracleDbType.Int32);
			op_oc030.Direction = ParameterDirection.Input;
			op_oc030.Value = oc030;

			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			//单价
			OracleParameter op_price = new OracleParameter("in_price", OracleDbType.Decimal);
			op_price.Direction = ParameterDirection.Input;
			op_price.Value = price;

			//缴费年限
			OracleParameter op_nums = new OracleParameter("in_nums", OracleDbType.Decimal);
			op_nums.Direction = ParameterDirection.Input;
			op_nums.Value = nums;

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_RegisterOut", new OracleParameter[]
				{op_rc001,op_oc003,op_oc004,op_oc005,op_oc030,op_fa001,op_price,op_nums,op_handler });
		}

		/// <summary>
		/// 返回寄存结构共有号位数
		/// </summary>
		/// <param name="rg001"></param>
		/// <returns></returns>
		public static int GetRgAllBits(string rg001)
		{
			OracleParameter op_rg001 = new OracleParameter("ic_rg001", OracleDbType.Varchar2, 10);
			op_rg001.Direction = ParameterDirection.Input;
			op_rg001.Value = rg001;

			Object re = SqlAssist.ExecuteScalar("select pkg_Report.fun_GetRgAllBits(:ic_rg001) from dual", new OracleParameter[] { op_rg001 });
			return Convert.ToInt32(re);
		}

		/// <summary>
		/// 返回寄存结构空闲号位数
		/// </summary>
		/// <param name="rg001"></param>
		/// <returns></returns>
		public static int GetRgFreeBits(string rg001)
		{
			OracleParameter op_rg001 = new OracleParameter("ic_rg001", OracleDbType.Varchar2, 10);
			op_rg001.Direction = ParameterDirection.Input;
			op_rg001.Value = rg001;

			Object re = SqlAssist.ExecuteScalar("select pkg_Report.fun_GetRgFreeBits(:ic_rg001) from dual", new OracleParameter[] { op_rg001 });
			return Convert.ToInt32(re);
		}

		/// <summary>
		/// 返回寄存结构占用号位数
		/// </summary>
		/// <param name="rg001"></param>
		/// <returns></returns>
		public static int GetRgUsedBits(string rg001)
		{
			OracleParameter op_rg001 = new OracleParameter("ic_rg001", OracleDbType.Varchar2, 10);
			op_rg001.Direction = ParameterDirection.Input;
			op_rg001.Value = rg001;

			Object re = SqlAssist.ExecuteScalar("select pkg_Report.fun_GetRgUsedBits(:ic_rg001) from dual", new OracleParameter[] { op_rg001 });
			return Convert.ToInt32(re);
		}

		/// <summary>
		/// 返回寄存结构欠费号位数
		/// </summary>
		/// <param name="rg001"></param>
		/// <returns></returns>
		public static int GetRgDebtBits(string rg001)
		{
			OracleParameter op_rg001 = new OracleParameter("ic_rg001", OracleDbType.Varchar2, 10);
			op_rg001.Direction = ParameterDirection.Input;
			op_rg001.Value = rg001;

			Object re = SqlAssist.ExecuteScalar("select pkg_Report.fun_GetRgDebtBits(:ic_rg001) from dual", new OracleParameter[] { op_rg001 });
			return Convert.ToInt32(re);
		}

		/// <summary>
		/// 获取缴费次数
		/// </summary>
		/// <param name="rc001"></param>
		/// <returns></returns>
		public static int GetRegPayRecordNum(string rc001)
		{
			OracleParameter op_rc001 = new OracleParameter("ic_rg001", OracleDbType.Varchar2, 10);
			op_rc001.Direction = ParameterDirection.Input;
			op_rc001.Value = rc001;

			Object re = SqlAssist.ExecuteScalar("select pkg_business.fun_GetRegPayRecordNum(:ic_rc001) from dual", new OracleParameter[] { op_rc001 });
			return Convert.ToInt32(re);
		}

		public static int AdjustRegisterDate(string rc001, DateTime rc020, DateTime rc022)
		{
			//逝者编号
			OracleParameter op_rc001 = new OracleParameter("ic_rc001", OracleDbType.Varchar2, 10);
			op_rc001.Direction = ParameterDirection.Input;
			op_rc001.Value = rc001;
			//开始时间
			OracleParameter op_begin = new OracleParameter("id_begin", OracleDbType.Date);
			op_begin.Direction = ParameterDirection.Input;
			op_begin.Value = rc020;
			//结束日期
			OracleParameter op_end = new OracleParameter("id_end", OracleDbType.Date);
			op_end.Direction = ParameterDirection.Input;
			op_end.Value = rc022;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_AdjustRegisterDate", new OracleParameter[]
				{op_rc001,op_begin,op_end});
		}
	}
}
