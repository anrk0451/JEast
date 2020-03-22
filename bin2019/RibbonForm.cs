using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraSplashScreen;
using white.windows;
using JEast.Domain;
using DevExpress.XtraTab;
using JEast.Dao;
using JEast.windows;
using JEast.BaseObject;
using JEast.Misc;
using System.Threading;
using DevExpress.XtraTab.ViewInfo;
using JEast.DataSet;
using System.Runtime.InteropServices;
using System.Diagnostics;
using JEast.Action;
using TaxCardX;
using System.Configuration;

namespace JEast
{
	public partial class RibbonForm : DevExpress.XtraBars.Ribbon.RibbonForm
	{
		[DllImport("user32.dll", EntryPoint = "FindWindow")]
		private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
		[DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
		private static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

		//打印服务进程
		Process printprocess = new Process();

		public GoldTaxClass goldTax { get; set; }

		public Dictionary<string, Object> swapdata { get; set; }

		//追踪已经打开的Tab页
		private Dictionary<string, Bo01> businessTab = new Dictionary<string, Bo01>();
		private Dictionary<string, XtraTabPage> openedTabPage = new Dictionary<string, XtraTabPage>();

		public RibbonForm()
		{
            SplashScreenManager.ShowForm(typeof(SplashScreen1));
            Thread.Sleep(2000);
            SplashScreenManager.CloseForm();
            InitializeComponent();

			///启动打印服务进程			 
			//MessageBox.Show(System.Windows.Forms.Application.StartupPath);
			printprocess.StartInfo.FileName =  "pbnative.exe";

			//printprocess.StartInfo.UseShellExecute = false;
			//printprocess.StartInfo.CreateNoWindow = true;

			printprocess.Start();

			swapdata = new Dictionary<string, object>();
		}

		private void RibbonForm_Load(object sender, EventArgs e)
		{
            //// 读取业务对象
            Bo01_dao bo01_dao = new Bo01_dao();
			List<Bo01> bo01_rows = bo01_dao.GetList(it => it.bo004 == "x");
			businessTab = bo01_rows.ToDictionary(key => key.bo001, value => value);

			Login login = new Login();
			if (login.ShowDialog() == DialogResult.OK)  //登录成功处理..........
			{
				barStaticItem2.Caption = Envior.cur_userName;
				bs_version.Caption = AppInfo.AppVersion;
			}
			login.Dispose();

			//连接打印服务
			this.ConnectPrtServ();

			//连接金税卡服务 ///
			if(ConfigurationManager.OpenExeConfiguration(System.Windows.Forms.Application.ExecutablePath).AppSettings.Settings["TAX_CARD"].Value.ToString() == "1")
			{
				goldTax = new GoldTaxClass();
				goldTax.CertPassWord = "12345678";
				goldTax.OpenCard();
				if (goldTax.RetCode == 1011)
				{
					MessageBox.Show("打开金税卡成功!", "提示");
					Envior.TAX_READY = true;
				}

				else
				{
					MessageBox.Show("打开金税卡失败!" + goldTax.RetCode.ToString() + goldTax.RetMsg, "提示");
					Envior.TAX_READY = false;
				}
			}

		}

		/// <summary>
		/// 连接打印服务
		/// </summary>
		private void ConnectPrtServ()
		{
			IntPtr hwnd = FindWindow(null, "prtserv");
			if (hwnd != IntPtr.Zero)
			{
				Envior.prtservHandle = hwnd;
				int prtConnId = int.Parse(SqlAssist.ExecuteScalar("select seq_prtserv.nextval from dual", null).ToString());

				////建立连接
				PrtServAction.Connect(prtConnId, hwnd.ToInt32(), this.Handle.ToInt32());
				Envior.prtConnId = prtConnId;

				////给打印服务窗口发消息 建立连接
				SendMessage(hwnd, 0x2710, 0, prtConnId);
			}
			else
			{
				MessageBox.Show("没有找到打印服务进程,不能打印!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}




		/// <summary>
		/// 打开业务对象(如果没有则创建)
		/// </summary>
		public void openBusinessObject(string bo001)
		{
			openBusinessObject(bo001, null);
		}

		/// <summary>
		/// 打开业务对象(如果没有则创建)
		/// </summary>
		public void openBusinessObject(string bo001, object parm)
		{
			if (openedTabPage.ContainsKey(bo001))
			{
				xtraTabControl1.SelectedTabPage = openedTabPage[bo001];
				if (parm != null)
				{
					foreach (Control control in openedTabPage[bo001].Controls)
					{
						if (control is BaseBusiness)
						{
							((BaseBusiness)control).swapdata["parm"] = parm;
							((BaseBusiness)control).Business_Init();
							return;
						}
					}
				}
			}
			else //如果尚未打开，则new
			{
				XtraTabPage newPage = new XtraTabPage();
				newPage.Text = businessTab[bo001].bo003;
				newPage.Tag = bo001;
                newPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;


				BaseBusiness bo = (BaseBusiness)Activator.CreateInstance(Type.GetType("JEast.BusinessObject." + bo001));

				Envior.mform = this;

				bo.Dock = DockStyle.Fill;
				bo.Parent = newPage;
				bo.swapdata.Add("parm", parm);

				newPage.Controls.Add(bo);

				xtraTabControl1.TabPages.Add(newPage);
				xtraTabControl1.SelectedTabPage = newPage;

				bo.Business_Init();

				////////登记已打开 Tabpage ////////
				openedTabPage.Add(bo001, newPage);

			}
		}

		/// <summary>
		/// 数据项维护
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.openBusinessObject("DataDict");
		}

        private void BarButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.openBusinessObject("ServiceProduct");
        }

        private void BarButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.openBusinessObject("Roles");
        }

        private void XtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;

            XtraTabPage curPage = (XtraTabPage)arg.Page;
            ///////// 清除页面追踪 ////////
            openedTabPage.Remove(curPage.Tag.ToString());

            curPage.Dispose();
        }

        private void BarButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {

            Frm_ac01 frm_ac01 = new Frm_ac01();
            frm_ac01.swapdata["action"] = "add";

            Ac01_ds ac01_ds = new Ac01_ds();
            frm_ac01.swapdata["dataset"] = ac01_ds;

            frm_ac01.ShowDialog();
            frm_ac01.Dispose();            
        }
 

        private void BarButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.openBusinessObject("Combo");
        }

        private void BarButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.openBusinessObject("RegStru");
        }

		/// <summary>
		/// 税务基础信息设置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void BarButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
        {
			//this.openBusinessObject("InvoiceItems");
			Frm_taxInfo frm_taxInfo = new Frm_taxInfo();
			frm_taxInfo.ShowDialog();
        }

        private void BarButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.openBusinessObject("FireCheckinBrow");
        }

		private void BarButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.openBusinessObject("BusinessHandleBrow");
		}

		private void BarButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.openBusinessObject("TempSales");
		}

		private void BarButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.openBusinessObject("Register_brow");
		}

		/// <summary>
		/// 操作员管理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem30_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.openBusinessObject("Operator");
		}

		/// <summary>
		/// 角色管理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem31_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.openBusinessObject("Roles");
		}


		/// <summary>
		/// 主窗口关闭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RibbonForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			SqlAssist.DisConnect();

			//关闭关联的打印进程
			if (!printprocess.HasExited) printprocess.Kill();

			//关闭金税卡
			if (Envior.TAX_READY)
			{
				goldTax.CloseCard();
			}
		}

		/// <summary>
		/// 寄存室数据
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.openBusinessObject("RegisterRoomData");
		}

		/// <summary>
		/// 每日收费明细
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem34_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.openBusinessObject("FinanceDaySearch");
		}

		/// <summary>
		/// 重新连接税务金卡
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem33_ItemClick(object sender, ItemClickEventArgs e)
		{
			PrtServAction.ReconnectTaxCard(this.Handle.ToInt32());
		}

		private void BarButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
		{
			////////  显示登录窗口  //////////
			Login login = new Login();
			if (login.ShowDialog(this) == DialogResult.OK)
			{
				/////////////////////  成功登陆后处理   ///////////////////
				barStaticItem2.Caption = Envior.cur_userName;
			}
		}

		private void BarButtonItem29_ItemClick(object sender, ItemClickEventArgs e)
		{
		
		}

		/// <summary>
		/// 类别登记
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem35_ItemClick(object sender, ItemClickEventArgs e)
		{
			openBusinessObject("Report_ClassStat");
		}

		/// <summary>
		/// 升级文件上传
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
		{
			Frm_upgrade frm_1 = new Frm_upgrade();
			frm_1.ShowDialog();
		}

		private void BarButtonItem28_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.openBusinessObject("Report_RegisterOut");
		}

		/// <summary>
		/// 出灵数据查询
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.openBusinessObject("Report_Checkout");
		}

		/// <summary>
		/// 欠费数据查询
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem27_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.openBusinessObject("Report_Debt");
		}

		/// <summary>
		/// 收款作废查询
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BarButtonItem38_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.openBusinessObject("FinanceRoll_Report");
		}

		//修改密码
		private void BarButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
		{
			Frm_ModifyPwd frm_modify_pwd = new Frm_ModifyPwd();
			frm_modify_pwd.ShowDialog();
			//frm_modify_pwd.Dispose();
		}

		private void BarButtonItem36_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.openBusinessObject("Report_ItemStat");
		}

		/// <summary>
		/// 收款员收款统计
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem37_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.openBusinessObject("Report_CasherStat");
		}
	}
}