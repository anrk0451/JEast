using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEast.Domain
{
	/// <summary>
	/// 发票客户信息
	/// </summary>
	class InvoiceInfo
	{
		public string InfoClientName { get; set; }			//购方名称
		public string InfoClientTaxCode { get; set; }		//购方税号

		public string infoclientbankaccount { get; set; }    //购方银行及帐号
		public string infoclientaddressphone { get; set; }   //购方地址及电话

		public string infocashier { get; set; }				 //收款人
		public string infochecker { get; set; }				 //复核人
	}
}
