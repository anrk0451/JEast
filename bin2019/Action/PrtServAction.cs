using JEast.Domain;
using JEast.Misc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JEast.Action
{
	class PrtServAction
	{
		[DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
		private static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

		/// <summary>
		/// 返回税务下一张票号!
		/// </summary>
		/// <returns></returns>
		public static string GetTaxInvoiceNextNum()
		{
			if (!Envior.TAX_READY)
				return "0";
			else
			{
				Envior.mform.goldTax.GetInfo();
				return Envior.mform.goldTax.InfoNumber.ToString();
			}

		}


		/// <summary>
		/// 打印 火化、临时性销售发票
		/// </summary>
		/// <param name="settleId"></param>
		/// <param name="invClient"></param>
		public static void Print_Invoice(string settleId, InvoiceInfo invClient)
		{
			string s_tax_name = string.Empty;
			string s_nextNum = GetTaxInvoiceNextNum();
			MessageBox.Show("下一张发票号:" + s_nextNum ,"",MessageBoxButtons.OK,MessageBoxIcon.Information);


			OracleDataReader reader = SqlAssist.ExecuteReader("select * from v_sa01 where sa010 ='" + settleId + "'");
			OracleDataReader itemTaxInfo = null;
			int i_counter = 0;
			 
			///1.设置发票整体信息
			Envior.mform.goldTax.InvInfoInit();   //发票信息初始化
			Envior.mform.goldTax.InfoKind = 2;    //发票类型 - 普通发票
			Envior.mform.goldTax.InfoSellerBankAccount = MiscAction.GetTaxSellerBank();      //销方开户行及账号
			Envior.mform.goldTax.InfoSellerAddressPhone = MiscAction.GetTaxSellerAddress();  //销方地址电话
			Envior.mform.goldTax.InfoInvoicer = Envior.cur_invoicer;                         //开票人
			Envior.mform.goldTax.InfoCashier = Envior.cur_invoicer;                          //收款人
			Envior.mform.goldTax.InfoChecker = Envior.cur_checker;							 //复核人
 
			Envior.mform.goldTax.InfoClientName = invClient.InfoClientName;                  //客户名称
			Envior.mform.goldTax.InfoClientTaxCode = invClient.InfoClientTaxCode;            //购方税号
			Envior.mform.goldTax.InfoClientBankAccount = invClient.infoclientbankaccount;    //购方银行账号
			Envior.mform.goldTax.InfoClientAddressPhone = invClient.infoclientaddressphone;  //购方地址电话

			Envior.mform.goldTax.InfoTaxRate = 17;                                            //税率
			Envior.mform.goldTax.InfoBillNumber = settleId;                                  //销售单据编号

			Envior.mform.goldTax.ClearInvList();
			while (reader.Read())
			{
				Envior.mform.goldTax.InvListInit();

				if (reader["SA002"].ToString() == "08" || reader["SA003"].ToString().Contains("寄存费"))
				{
					s_tax_name = GetRegLevel_TaxName(Convert.ToDecimal(reader["PRICE"]));
					Envior.mform.goldTax.ListGoodsName = s_tax_name;
					Envior.mform.goldTax.InfoTaxRate = 0;
					Envior.mform.goldTax.ListNumber = Convert.ToDouble(reader["NUMS"]);
					Envior.mform.goldTax.ListPrice = Convert.ToDouble(reader["PRICE"]);

					Envior.mform.goldTax.ListPriceKind = 1;       //含税价标志，单价和金额的种类， 0 为不含税价，1 为含税价 
					Envior.mform.goldTax.AddInvList();
				}
				else
				{
					itemTaxInfo = SqlAssist.ExecuteReader("select * from v_allItem where item_id ='" + reader["SA004"].ToString() + "'");
					if (itemTaxInfo.HasRows)
					{
						while (itemTaxInfo.Read())
						{
							if (string.IsNullOrEmpty(itemTaxInfo["TX003"].ToString()))
							{
								MessageBox.Show(reader["SA003"] + "未设置对应的税务项目!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return;
							}
							else
							{
								s_tax_name = itemTaxInfo["TX003"].ToString();
							}
							Envior.mform.goldTax.ListGoodsName = s_tax_name;
							Envior.mform.goldTax.InfoTaxRate = Convert.ToSByte(itemTaxInfo["TX005"]);
							Envior.mform.goldTax.ListNumber = Convert.ToDouble(reader["NUMS"]);
							Envior.mform.goldTax.ListPrice = Convert.ToDouble(reader["PRICE"]);

							Envior.mform.goldTax.ListPriceKind = 1;       //含税价标志，单价和金额的种类， 0 为不含税价，1 为含税价 
							Envior.mform.goldTax.AddInvList();
						}
					}

				}
				 i_counter++;
			}

			if(i_counter > 7 /*AppInfo.TAXITEMCOUNT*/)
			{
				Envior.mform.goldTax.InfoListName = "商品服务项目清单" + settleId;   //设置后才会打印清单
			}

			//开具发票
			Envior.mform.goldTax.Invoice();

			string s_invoice_code = string.Empty;
			int i_invoice_num = 0;
			if(Envior.mform.goldTax.RetCode == 4011)
			{
				s_invoice_code = Envior.mform.goldTax.InfoTypeCode;
				i_invoice_num = Envior.mform.goldTax.InfoNumber;
				if(Tax_Log(settleId, s_invoice_code, i_invoice_num.ToString()) == 1)
				{
					MessageBox.Show("开票成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				/////////////////////  打印发票  ///////////////////////////
				Envior.mform.goldTax.InfoKind = 2;                              //发票类型	
				Envior.mform.goldTax.InfoTypeCode = s_invoice_code;             //发票代码
				Envior.mform.goldTax.InfoNumber = i_invoice_num;                //发票号
				Envior.mform.goldTax.InfoShowPrtDlg = 1;                        //是否显示确认对话框
				Envior.mform.goldTax.GoodsListFlag = 0;                         //打印发票
				Envior.mform.goldTax.PrintInv();

				if(i_counter > 7 /*AppInfo.TAXITEMCOUNT*/ )
				{
				    if(MessageBox.Show("是否打印发票清单?","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
					{
						Envior.mform.goldTax.InfoKind = 2;                      //发票类型	
						Envior.mform.goldTax.InfoTypeCode = s_invoice_code;     //发票代码
						Envior.mform.goldTax.InfoNumber = i_invoice_num;        //发票号
						Envior.mform.goldTax.InfoShowPrtDlg = 1;                 //是否显示确认对话框
						Envior.mform.goldTax.GoodsListFlag = 1;                  //打印清单
						Envior.mform.goldTax.PrintInv();
					}
				}				
			}
			else
			{
				MessageBox.Show("错误代码:" + Envior.mform.goldTax.RetCode + "\n" + "错误信息:" + Envior.mform.goldTax.RetMsg,"开票失败",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
  
		}




		public static int Connect(int connId, int prtservHandle, int clientHandle)
		{
			OracleCommand cmd = new OracleCommand("pkg_business.prc_ConnectPrtServ", SqlAssist.conn);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			OracleTransaction trans = null;

			//连接Id
			OracleParameter op_connId = new OracleParameter("in_connectId", OracleDbType.Int32, 10);
			op_connId.Direction = ParameterDirection.Input;
			op_connId.Value = connId;
			//打印服务窗口Handle
			OracleParameter op_servHandle = new OracleParameter("in_servHandle", OracleDbType.Int32);
			op_servHandle.Direction = ParameterDirection.Input;
			op_servHandle.Value = prtservHandle;
			//打印客户端窗口
			OracleParameter op_clientHandle = new OracleParameter("in_clientHandle", OracleDbType.Int32);
			op_clientHandle.Direction = ParameterDirection.Input;
			op_clientHandle.Value = clientHandle;

			OracleParameter appcode = new OracleParameter("on_appcode", OracleDbType.Int16);
			appcode.Direction = ParameterDirection.Output;
			OracleParameter apperror = new OracleParameter("oc_error", OracleDbType.Varchar2, 100);
			apperror.Direction = ParameterDirection.Output;

			try
			{
				trans = SqlAssist.conn.BeginTransaction();
				cmd.Parameters.AddRange(new OracleParameter[] { op_connId, op_servHandle, op_clientHandle, appcode, apperror });
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
		/// 发送打印命令
		/// </summary>
		/// <param name="connId"></param>
		/// <param name="clientHandle"></param>
		/// <param name="commandNum"></param>
		/// <param name="commandString"></param>
		/// <param name="para_arry"></param>  不定参数 但实际运行只有2或8个参数（对应Oracle 过程重载）
		/// <returns></returns>
		public static int SendPrtCommand(int connId, int clientHandle, int commandNum, string commandString, params string[] para_arry)
		{
			OracleCommand cmd = new OracleCommand("pkg_business.prc_SendPrtCommand", SqlAssist.conn);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			OracleTransaction trans = null;

			//连接Id
			OracleParameter op_connId = new OracleParameter("in_connectId", OracleDbType.Int32, 10);
			op_connId.Direction = ParameterDirection.Input;
			op_connId.Value = connId;
			//打印客户端窗口
			OracleParameter op_clientHandle = new OracleParameter("in_clientHandle", OracleDbType.Int32);
			op_clientHandle.Direction = ParameterDirection.Input;
			op_clientHandle.Value = clientHandle;
			//打印命令编号
			OracleParameter op_commandNum = new OracleParameter("in_commandNum", OracleDbType.Int32);
			op_commandNum.Direction = ParameterDirection.Input;
			op_commandNum.Value = commandNum;
			//打印命令字符串
			OracleParameter op_commandString = new OracleParameter("ic_commandString", OracleDbType.Varchar2, 50);
			op_commandString.Direction = ParameterDirection.Input;
			op_commandString.Value = commandString;

			List<OracleParameter> para_list = new List<OracleParameter>();
			OracleParameter op_1 = null;

			for (int i = 0; i < para_arry.Length; i++)
			{
				op_1 = new OracleParameter("ic_para" + (i + 1).ToString(), OracleDbType.Varchar2, 200);
				op_1.Direction = ParameterDirection.Input;
				op_1.Value = para_arry[i];
				para_list.Add(op_1);
			}

			////参数1
			//OracleParameter op_para1 = new OracleParameter("ic_para1", OracleDbType.Varchar2, 50);
			//op_para1.Direction = ParameterDirection.Input;
			//op_para1.Value = para1;
			////参数2
			//OracleParameter op_para2 = new OracleParameter("ic_para2", OracleDbType.Varchar2, 50);
			//op_para2.Direction = ParameterDirection.Input;
			//op_para2.Value = para2;

			////参数3 - 客户名称
			//OracleParameter op_para3 = new OracleParameter("ic_para3", OracleDbType.Varchar2, 200);
			//op_para3.Direction = ParameterDirection.Input;
			//op_para3.Value = para3;

			////参数4 - 客户税号
			//OracleParameter op_para4 = new OracleParameter("ic_para4", OracleDbType.Varchar2, 50);
			//op_para4.Direction = ParameterDirection.Input;
			//op_para4.Value = para4;

			////参数5 - 客户 开户行及账号
			//OracleParameter op_para5 = new OracleParameter("ic_para5", OracleDbType.Varchar2, 200);
			//op_para5.Direction = ParameterDirection.Input;
			//op_para5.Value = para5;

			////参数6 - 客户 地址及电话
			//OracleParameter op_para6 = new OracleParameter("ic_para6", OracleDbType.Varchar2, 200);
			//op_para6.Direction = ParameterDirection.Input;
			//op_para6.Value = para6;

			////参数7 - 收款人
			//OracleParameter op_para7 = new OracleParameter("ic_para7", OracleDbType.Varchar2, 50);
			//op_para7.Direction = ParameterDirection.Input;
			//op_para7.Value = para7;

			////参数8 - 审核人
			//OracleParameter op_para8 = new OracleParameter("ic_para8", OracleDbType.Varchar2, 50);
			//op_para8.Direction = ParameterDirection.Input;
			//op_para8.Value = para8;

			OracleParameter appcode = new OracleParameter("on_appcode", OracleDbType.Int16);
			appcode.Direction = ParameterDirection.Output;
			OracleParameter apperror = new OracleParameter("oc_error", OracleDbType.Varchar2, 100);
			apperror.Direction = ParameterDirection.Output;

			try
			{
				trans = SqlAssist.conn.BeginTransaction();

				para_list.InsertRange(0, new OracleParameter[] { op_connId, op_clientHandle, op_commandNum, op_commandString});
				para_list.AddRange(new OracleParameter[] { appcode, apperror });


				cmd.Parameters.AddRange(para_list.ToArray());
				cmd.ExecuteNonQuery();

				if (int.Parse(appcode.Value.ToString()) < 0)
				{
					trans.Rollback();
					MessageBox.Show(apperror.Value.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return -1;
				}
				trans.Commit();

				SendMessage(Envior.prtservHandle, 0x2710, commandNum, 0);

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
		/// 生成新的命令编号
		/// </summary>
		/// <returns></returns>
		public static int GenNewCommandNum()
		{
			int result = int.Parse(SqlAssist.ExecuteScalar("select seq_prtserv.nextVal from dual", null).ToString());
			return result;
		}

		/// <summary>
		/// 返回打印服务响应
		/// </summary>
		/// <param name="commandNum"></param>
		/// <returns></returns>
		public static string GetResponseText(int commandNum)
		{
			OracleCommand cmd = new OracleCommand("pkg_business.fun_getResponseText ", SqlAssist.conn);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;

			OracleParameter returnValue = new OracleParameter("result", OracleDbType.Varchar2, 100);
			returnValue.Direction = ParameterDirection.ReturnValue;

			OracleParameter op_commandNum = new OracleParameter("in_commandNum", OracleDbType.Int32);
			op_commandNum.Direction = ParameterDirection.Input;
			op_commandNum.Value = commandNum;

			try
			{
				cmd.Parameters.Add(returnValue);
				cmd.Parameters.Add(op_commandNum);
				cmd.ExecuteNonQuery();
			}
			finally
			{
				cmd.Dispose();
			}

			return returnValue.Value.ToString();
		}

		 

		/// <summary>
		/// 打印火化结算发票
		/// </summary>
		/// <param name="settleId"></param>
		public static void Print_Fireinvoice(string settleId,InvoiceInfo invClient,Int32 whandle )
		{
			int commandNum = GenNewCommandNum();
			SendPrtCommand( Envior.prtConnId,
							whandle,
							commandNum,
							"Fire_invoice",
							settleId,
							Envior.cur_userId,
							invClient.InfoClientName,
							invClient.InfoClientTaxCode,
							invClient.infoclientbankaccount,
							invClient.infoclientaddressphone,
							invClient.infocashier,
							invClient.infochecker
				);
		}

		/// <summary>
		/// 打印火化证明
		/// </summary>
		/// <param name="ac001"></param>
		public static void Print_HHZM(string ac001,int whandle)
		{
			int commandNum = GenNewCommandNum();
			int result = SendPrtCommand(Envior.prtConnId,
										whandle,
										commandNum,
										"Fire_HHZM",
										ac001,
										null
			);
			if (result > 0)
			{  //记录火化证明打印日志
				FireAction.FireCertLog(ac001, Envior.cur_userId);
			}
		}

		/// <summary>
		/// 打印寄存发票
		/// </summary>
		/// <param name="settleId"></param>
		/// <param name="whandle"></param>
		public static void Print_RegisterInvoice(string settleId, InvoiceInfo invClient, int whandle)
		{
			int commandNum = PrtServAction.GenNewCommandNum();
			SendPrtCommand( Envior.prtConnId,
							whandle,
							commandNum,
							"Register_invoice",
							settleId,
							Envior.cur_userId,
							invClient.InfoClientName,
							invClient.InfoClientTaxCode,
							invClient.infoclientbankaccount,
							invClient.infoclientaddressphone,
							invClient.infocashier,
							invClient.infochecker

			);
		}

		/// <summary>
		/// 打印骨灰寄存证 (初次)
		/// </summary>
		/// <param name="rc001"></param>
		/// <param name="settleId"></param>
		public static void PrtRegisterCert(string rc001,string settleId,int whandle)
		{
			int commandNum = GenNewCommandNum();
			SendPrtCommand(Envior.prtConnId,
						   whandle,
						   commandNum,
						   "Register_Cert_First",
						   rc001,
						   settleId
		     );
		}

		/// <summary>
		/// 打印寄存标签
		/// </summary>
		/// <param name="rc001"></param>
		/// <param name="whandle"></param>
		public static void PrtRegisterLabel(string rc001,int whandle)
		{
			int commandNum = GenNewCommandNum();
			SendPrtCommand(Envior.prtConnId,
						   whandle,
						   commandNum,
						   "Register_Label",
						   rc001,
						   null
			 );
		}

		/// <summary>
		/// 打印寄存 续存记录
		/// </summary>
		/// <param name="settleId"></param>
		/// <param name="whandle"></param>
		public static void PrtRegisterPayRecord(string settleId,int whandle)
		{
			int commandNum = GenNewCommandNum();
			SendPrtCommand(Envior.prtConnId,
						   whandle,
						   commandNum,
						   "Register_Payrecord",
						   settleId,
						   null
			 );
		}

		/// <summary>
		/// 补打寄存证
		/// </summary>
		/// <param name="rc001"></param>
		/// <param name="whandle"></param>
		public static void PrtRegisterCertBD(string rc001,int whandle)
		{
			int commandNum = GenNewCommandNum();
			SendPrtCommand(Envior.prtConnId,
						   whandle,
						   commandNum,
						   "Register_Cert_BD",
						   rc001,
						   null
			 );
		}


		/// <summary>
		/// 补打 缴费记录
		/// </summary>
		/// <param name="settleId"></param>
		/// <param name="whandle"></param>
		public static void PrtPayrecord_BD(string settleId, int whandle)
		{
			int commandNum = PrtServAction.GenNewCommandNum();
			SendPrtCommand(Envior.prtConnId,
						   whandle,
						   commandNum,
						   "Register_Payrecord_BD",
						   settleId,
						   null
			 );
		}

		/// <summary>
		/// 打印 迁出通知单
		/// </summary>
		/// <param name="rc001"></param>
		/// <param name="whandle"></param>
		public static void PrtRegisterOutNotice(string rc001, int whandle)
		{
			int commandNum = PrtServAction.GenNewCommandNum();
			SendPrtCommand(Envior.prtConnId,
						   whandle,
						   commandNum,
						   "Register_Out_Notice",
						   rc001,
						   null
			 );
		}	


		/// <summary>
		/// 税务发票作废
		/// </summary>
		/// <param name="fa001"></param>
		public static string InvoiceRemoved(string fa001,int whandle)
		{
			//int commandNum = PrtServAction.GenNewCommandNum();
			//SendPrtCommand(Envior.prtConnId,
			//			   whandle,
			//			   commandNum,
			//			   "FinanceRemove",
			//			   fa001,
			//			   null
			// );
			Envior.mform.goldTax.InfoKind = 2;
			Envior.mform.goldTax.InfoTypeCode = GetTaxCode(fa001);
			MessageBox.Show(GetTaxNum(fa001),"票号");
			Envior.mform.goldTax.InfoNumber = Convert.ToInt32(GetTaxNum(fa001));
			Envior.mform.goldTax.CancelInv();
			return Envior.mform.goldTax.RetCode.ToString();
		}

		/// <summary>
		/// 根据结算流水号获取 税务发票 代码
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static string GetTaxCode(string fa001)
		{
			OracleParameter op_fa001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;
			return SqlAssist.ExecuteScalar("select pkg_business.fun_GetTaxCode(:ic_fa001) from dual", new OracleParameter[] { op_fa001 }).ToString();
		}

		/// <summary>
		/// 根据结算流水号获取 税务发票 号码
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static string GetTaxNum(string fa001)
		{
			OracleParameter op_fa001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;
			return SqlAssist.ExecuteScalar("select pkg_business.fun_GetTaxNum(:ic_fa001) from dual", new OracleParameter[] { op_fa001 }).ToString();
		}

		/// <summary>
		/// 打印发票
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="whandle"></param>
		public static void PrintInvoice(string fa001, int whandle)
		{
			int commandNum = PrtServAction.GenNewCommandNum();
			SendPrtCommand(Envior.prtConnId,
						   whandle,
						   commandNum,
						   "Print_Invoice",
						   fa001,
						   null
			 );
		}

		/// <summary>
		/// 打印火化登记通知单
		/// </summary>
		/// <param name="ac001"></param>
		/// <param name="whandle"></param>
		public static void Print_CheckinNotice(string ac001, int whandle)
		{
			int commandNum = PrtServAction.GenNewCommandNum();
			SendPrtCommand(Envior.prtConnId,
						   whandle,
						   commandNum,
						   "Fire_Checkin_Notice",
						   ac001,
						   null
			 );
		}

		/// <summary>
		/// 打印付货单
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="whandle"></param>
		public static void Print_FHD(string fa001,int whandle)
		{
			int commandNum = PrtServAction.GenNewCommandNum();
			SendPrtCommand(Envior.prtConnId,
						   whandle,
						   commandNum,
						   "Fire_FHD",
						   fa001,
						   null
			 );
		}


		/// <summary>
		/// 重新连接税务金卡
		/// </summary>
		/// <param name="whandle"></param>
		public static void ReconnectTaxCard(int whandle)
		{
			int commandNum = PrtServAction.GenNewCommandNum();
			SendPrtCommand(Envior.prtConnId,
						   whandle,
						   commandNum,
						   "Reconnect_Card",
						   null,
						   null
			 );
		}

		/// <summary>
		/// 税务发票打印日志
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="inv_code"></param>
		/// <param name="inv_num"></param>
		/// <returns></returns>
		public static int Tax_Log(string fa001,string inv_code,string inv_num)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			//发票代码
			OracleParameter op_inv_code = new OracleParameter("ic_inv_code", OracleDbType.Varchar2, 50);
			op_inv_code.Direction = ParameterDirection.Input;
			op_inv_code.Value = inv_code;

			//发票号码
			OracleParameter op_inv_num = new OracleParameter("ic_inv_num", OracleDbType.Varchar2, 50);
			op_inv_num.Direction = ParameterDirection.Input;
			op_inv_num.Value = inv_num;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_Tax_log", new OracleParameter[] { op_fa001, op_inv_code,op_inv_num });
		}


		/// <summary>
		/// 根据寄存费单价 返回寄存费税务映射价格
		/// </summary>
		/// <param name="price"></param>
		/// <returns></returns>
		private static string GetRegLevel_TaxName(decimal price)
		{
			if (price > 500)
				return "高档寄存费";
			else if (price <= 500 && price > 100)
				return "中档寄存费";
			else
				return "普通寄存费";
		}

		/// <summary>
		/// 打印财务类别统计
		/// </summary>
		/// <param name="s_begin"></param>
		/// <param name="s_end"></param>
		/// <param name="whandle"></param>
		public static void Print_Report_ClassStat(string s_begin,string s_end,int whandle)
		{
			int commandNum = PrtServAction.GenNewCommandNum();
			SendPrtCommand(Envior.prtConnId,
						   whandle,
						   commandNum,
						   "Report_ClassStat",
						   s_begin,
						   s_end
			 );
		}


		/// <summary>
		/// 打印财务单项统计
		/// </summary>
		/// <param name="s_begin"></param>
		/// <param name="s_end"></param>
		/// <param name="whandle"></param>
		public static void Print_Report_ItemStat(string s_begin, string s_end, int whandle)
		{
			int commandNum = PrtServAction.GenNewCommandNum();
			SendPrtCommand(Envior.prtConnId,
						   whandle,
						   commandNum,
						   "Report_ItemStat",
						   s_begin,
						   s_end
			 );
		}
	}
}
