using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using JEast.BaseObject;
using JEast.DataSet;
using JEast.Dao;
using JEast.Domain;
using SqlSugar;
using JEast.Misc;
using Oracle.ManagedDataAccess.Client;

namespace JEast.windows
{
    public partial class frm_Operator : MyDialog
    {
        Ro01_ds ro01_ds = new Ro01_ds();
        Uc01 uc01 = null;
        Uc01_dao uc01_dao = new Uc01_dao();

        string s_uc001 = string.Empty;     //操作员编号


        public frm_Operator()
        {
            InitializeComponent();
            ro01_ds.ro01Adapter.Fill(ro01_ds.Ro01);    
        }

        private void Frm_Operator_Load(object sender, EventArgs e)
        {
            clbx_roles.DataSource = ro01_ds.Ro01;
            clbx_roles.DisplayMember = "RO003";
            clbx_roles.ValueMember = "RO001";
            ro01_ds.ro01Adapter.Fill(ro01_ds.Ro01);

            if (this.swapdata["action"].ToString() == "add")
            {
                this.Text = "新建用户";
                uc01 = new Uc01();
            }
            else if (this.swapdata["action"].ToString() == "edit")
            {
                this.Text = "编辑用户";
                s_uc001 = this.swapdata["uc001"].ToString();

                uc01 = uc01_dao.GetSingle(s => s.uc001 == s_uc001);
 
                txtedit_uc002.Text = uc01.uc002;
                txtedit_uc003.Text = uc01.uc003;

				checkEdit1.EditValue = uc01.uc005;
 
				txtedit_uc009.Text = uc01.uc009;
				txtedit_uc010.Text = uc01.uc010;

                txtedit_pwd.ReadOnly = true;
                txtedit_pwd2.ReadOnly = true;

                Ur_Mapper_dao ur_Mapper_dao = new Ur_Mapper_dao();
                List<Ur_Mapper> mapper = ur_Mapper_dao.GetList(s => s.uc001 == s_uc001);
                if(mapper.Count > 0)
                {
                    for (int i = 0; i < clbx_roles.ItemCount; i++)
                    {
                        string ro001 = clbx_roles.GetItemValue(i).ToString();
                        if (mapper.FindIndex(x => x.ro001.Equals(ro001)) >= 0)
                        {
                            clbx_roles.SetItemChecked(i, true);
                        }
                    }
                }
            }
        }

        private void Sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Sb_ok_Click(object sender, EventArgs e)
        {
            //数据校验
            string s_uc002 = txtedit_uc002.Text;
            string s_uc003 = txtedit_uc003.Text;
            string s_uc004 = txtedit_pwd.Text;
            string s_uc004_2 = txtedit_pwd2.Text;

            if (String.IsNullOrEmpty(s_uc002))
            {
                txtedit_uc002.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                txtedit_uc002.ErrorText = "用户登录代码必须输入!";
                txtedit_uc002.Focus();
                return;
            }

            if (String.IsNullOrEmpty(s_uc003))
            {
                txtedit_uc003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                txtedit_uc003.ErrorText = "用户姓名必须输入!";
                txtedit_uc003.Focus();
                return;
            }

            if (this.swapdata["action"].ToString() == "add")
            {
                if (String.IsNullOrEmpty(s_uc004))
                {
                    txtedit_pwd.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    txtedit_pwd.ErrorText = "密码必须输入!";
                    txtedit_pwd.Focus();
                    return;
                }
                else if (!String.Equals(s_uc004, s_uc004_2))
                {
                    txtedit_pwd2.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    txtedit_pwd2.ErrorText = "密码不一致!";
                    txtedit_pwd2.Focus();
                    return;
                }
            }

			//允许开税务发票
			if (checkEdit1.Checked)
			{
				if (txtedit_uc009.EditValue == null)
				{
					txtedit_uc009.ErrorText = "请输入映射开票人姓名!";
					txtedit_uc009.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
					return;
				}
				if (txtedit_uc010.EditValue == null)
				{
					txtedit_uc010.ErrorText = "请输入复核人!";
					txtedit_uc010.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
					return;
				}
			}

			uc01.uc005 = checkEdit1.EditValue.ToString();
 
 
            /////// 保存过程 ///////
            uc01.uc002 = txtedit_uc002.Text;
            uc01.uc003 = txtedit_uc003.Text;

            List<string> ro001_list = new List<string>();
            foreach (DataRowView item in clbx_roles.CheckedItems)
            {
                ro001_list.Add(item["ro001"].ToString());
            }

            if (this.swapdata["action"].ToString() == "add")
            {
                uc01.uc001 = Tools.GetEntityPK("UC01");
                uc01.uc004 = Tools.EncryptWithMD5(s_uc004);
                if (CreateOperator(uc01, ro001_list.ToArray()) > 0)
                {
                    MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                if (UpdateOperator(uc01, ro001_list.ToArray()) > 0)
                {
                    MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }     
        }

        /// <summary>
        /// 创建新用户
        /// </summary>
        /// <returns></returns>
        private int CreateOperator(Uc01 uc01,string[] rolesarry)
        {
            //用户编号
            OracleParameter op_uc001 = new OracleParameter("ic_uc001", OracleDbType.Varchar2, 10);
            op_uc001.Direction = ParameterDirection.Input;
            op_uc001.Value = uc01.uc001;

            //用户代码
            OracleParameter op_uc002 = new OracleParameter("ic_uc002", OracleDbType.Varchar2, 50);
            op_uc002.Direction = ParameterDirection.Input;
            op_uc002.Value = uc01.uc002;

            //用户姓名
            OracleParameter op_uc003 = new OracleParameter("ic_uc003", OracleDbType.Varchar2, 50);
            op_uc003.Direction = ParameterDirection.Input;
            op_uc003.Value = uc01.uc003;
            
            //用户密码
            OracleParameter op_uc004 = new OracleParameter("ic_uc004", OracleDbType.Varchar2, 50);
            op_uc004.Direction = ParameterDirection.Input;
            op_uc004.Value = uc01.uc004;

			//是否允许开票
			OracleParameter op_uc005 = new OracleParameter("ic_uc005", OracleDbType.Varchar2, 3);
			op_uc005.Direction = ParameterDirection.Input;
			op_uc005.Value = uc01.uc005;

			//映射开票人
			OracleParameter op_uc009 = new OracleParameter("ic_uc009", OracleDbType.Varchar2, 50);
			op_uc009.Direction = ParameterDirection.Input;
			op_uc009.Value = uc01.uc009;

			//复核人
			OracleParameter op_uc010 = new OracleParameter("ic_uc010", OracleDbType.Varchar2, 50);
			op_uc010.Direction = ParameterDirection.Input;
			op_uc010.Value = uc01.uc010;

			//用户角色数组
			OracleParameter op_roles_arry = new OracleParameter("ic_roles", OracleDbType.Varchar2,500);
            op_roles_arry.Direction = ParameterDirection.Input;
            op_roles_arry.Value = string.Join("|", rolesarry );
 

            return SqlAssist.ExecuteProcedure("pkg_business.prc_CreateOperator", new OracleParameter[] { op_uc001,op_uc002,op_uc003,op_uc004, op_uc005,op_uc009,op_uc010, op_roles_arry });
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        private int UpdateOperator(Uc01 uc01, string[] rolesarry)
        {
            //用户编号
            OracleParameter op_uc001 = new OracleParameter("ic_uc001", OracleDbType.Varchar2, 10);
            op_uc001.Direction = ParameterDirection.Input;
            op_uc001.Value = uc01.uc001;

            //用户代码
            OracleParameter op_uc002 = new OracleParameter("ic_uc002", OracleDbType.Varchar2, 50);
            op_uc002.Direction = ParameterDirection.Input;
            op_uc002.Value = uc01.uc002;

            //用户姓名
            OracleParameter op_uc003 = new OracleParameter("ic_uc003", OracleDbType.Varchar2, 50);
            op_uc003.Direction = ParameterDirection.Input;
            op_uc003.Value = uc01.uc003;

			//是否允许开票
			OracleParameter op_uc005 = new OracleParameter("ic_uc005", OracleDbType.Varchar2, 3);
			op_uc005.Direction = ParameterDirection.Input;
			op_uc005.Value = uc01.uc005;

			//映射开票人
			OracleParameter op_uc009 = new OracleParameter("ic_uc009", OracleDbType.Varchar2, 20);
			op_uc009.Direction = ParameterDirection.Input;
			op_uc009.Value = uc01.uc009;

			//复核人
			OracleParameter op_uc010 = new OracleParameter("ic_uc010", OracleDbType.Varchar2, 50);
			op_uc010.Direction = ParameterDirection.Input;
			op_uc010.Value = uc01.uc010;

			//用户角色数组
			OracleParameter op_roles_arry = new OracleParameter("ic_roles", OracleDbType.Varchar2, 500);
            op_roles_arry.Direction = ParameterDirection.Input;
            op_roles_arry.Value = string.Join("|", rolesarry);


            return SqlAssist.ExecuteProcedure("pkg_business.prc_UpdateOperator", 
				new OracleParameter[] { op_uc001, op_uc002, op_uc003, op_uc005,op_uc009, op_uc010,op_roles_arry });
        }

		private void CheckEdit1_CheckedChanged(object sender, EventArgs e)
		{
			if (checkEdit1.Checked)
			{
				txtedit_uc009.EditValue = txtedit_uc003.EditValue;
				txtedit_uc009.Enabled = true;

				txtedit_uc010.EditValue = txtedit_uc003.EditValue;
				txtedit_uc010.Enabled = true;

			}
			else
			{
				txtedit_uc009.EditValue = "";
				txtedit_uc009.Enabled = false;

				txtedit_uc010.EditValue = "";
				txtedit_uc010.Enabled = false;
			}
				
		}
	}
}