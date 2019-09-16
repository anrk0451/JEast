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
using JEast.Misc;
using SqlSugar;
using JEast.Action;

namespace JEast.windows
{
    public partial class Frm_ac01 : MyDialog
    {
        Ac01_ds ac01_ds = null;
        Ac01_dao ac01_dao = new Ac01_dao();
        Ac01 ac01 = null;

        string action = string.Empty;
        string AC001 = string.Empty;

        BaseBusiness businessObject = null;

        public Frm_ac01()
        {
            InitializeComponent();
        }

        private void Frm_ac01_Load(object sender, EventArgs e)
        {
            ac01_ds = this.swapdata["dataset"] as Ac01_ds;
            action = this.swapdata["action"].ToString();

            if(this.swapdata.ContainsKey("businessObject"))
                businessObject = this.swapdata["businessObject"] as BaseBusiness;

            lookUp_ac005.Properties.DataSource = ac01_ds.St01_reason;
            lookUp_ac005.Properties.ValueMember = "ST003";
            lookUp_ac005.Properties.DisplayMember = "ST003";
            ac01_ds.St01_reason.Sort = "SORTID ASC";

            lookUp_ac052.Properties.DataSource = ac01_ds.St01_relation;
            lookUp_ac052.Properties.ValueMember = "ST003";
            lookUp_ac052.Properties.DisplayMember = "ST003";
            ac01_ds.St01_relation.Sort = "SORTID ASC";


            lookUp_ac060.Properties.DataSource = ac01_ds.St01_driver;
            lookUp_ac060.Properties.ValueMember = "ST001";
            lookUp_ac060.Properties.DisplayMember = "ST003";
            ac01_ds.St01_driver.Sort = "SORTID ASC";

            lookUp_ac007.Properties.DataSource = ac01_ds.St01_district;
            lookUp_ac007.Properties.ValueMember = "ST001";
            lookUp_ac007.Properties.DisplayMember = "ST003";
            ac01_ds.St01_district.Sort = "SORTID ASC";

			lookup_ash.Properties.DataSource = ac01_ds.ct01_ASH_HANDLE;
			lookup_ash.Properties.ValueMember = "CT004";
			lookup_ash.Properties.DisplayMember = "CT003";

			lookup_ac070.Properties.DataSource = ac01_ds.ct01_HHL_TYPE;
			lookup_ac070.Properties.ValueMember = "CT004";
			lookup_ac070.Properties.DisplayMember = "CT003";


			if (string.Equals(action, "edit"))
            {
                this.Text = "进灵登记修改";
                AC001 = this.swapdata["AC001"].ToString();

                ac01 = ac01_dao.GetSingle(s => s.ac001== AC001);
                if(ac01 == null)
                {
                    b_ok.Enabled = false;
                    MessageBox.Show("查找数据失败！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }

                txtEdit_ac003.EditValue = ac01.ac003;
                rg_ac002.EditValue = ac01.ac002;
                txtEdit_ac004.EditValue = ac01.ac004;
                txtedit_ac014.EditValue = ac01.ac014;
                txtEdit_ac009.EditValue = ac01.ac009;
                dateEdit_ac010.EditValue = ac01.ac010;
                lookUp_ac005.EditValue = ac01.ac005;
                lookUp_ac060.EditValue = ac01.ac060;
                lookUp_ac007.EditValue = ac01.ac007;
                txtEdit_ac008.EditValue = ac01.ac008;
                txtEdit_ac050.EditValue = ac01.ac050;
                lookUp_ac052.EditValue = ac01.ac052;
                dateEdit_ac020.EditValue = ac01.ac020;
                txtEdit_ac051.EditValue = ac01.ac051;
                txtEdit_ac055.EditValue = ac01.ac055;
                mem_ac099.EditValue = ac01.ac099;
                dateEdit_ac020.Enabled = false;

				lookup_ash.EditValue = ac01.ac006;   //骨灰处理方式
				lookup_ac070.EditValue = ac01.ac070;   //火化炉
				lookup_ac070.ReadOnly = true;          //火化炉 编辑时不能再修改!!!

				if(ac01.ac080 != null)
					label_forder.Text = ac01.ac080.ToString();   //火化序号
            }
            else
            {
                ac01 = new Ac01();
                dateEdit_ac020.EditValue = System.DateTime.Now;
            }



        }

        /// <summary>
        /// 身份证号校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txtedit_ac014_Validating(object sender, CancelEventArgs e)
        {
            string s_idcard = txtedit_ac014.Text.Trim();
            if (string.IsNullOrWhiteSpace(s_idcard)) return;

            if (s_idcard.Length != 15 && s_idcard.Length != 18)
            {
                txtedit_ac014.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                txtedit_ac014.ErrorText = "身份证号位数错误!";
                e.Cancel = true;
            }
            else if (s_idcard.Length == 15)
            {
                if (!Tools.CheckIDCard15(s_idcard))
                {
                    txtedit_ac014.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    txtedit_ac014.ErrorText = "身份证号错误!";
                    e.Cancel = true;
                }
            }
            else if (s_idcard.Length == 18)
            {
                if (!Tools.CheckIDCard18(s_idcard))
                {
                    txtedit_ac014.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    txtedit_ac014.ErrorText = "身份证号错误!";
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 年龄校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtEdit_ac004_Validating(object sender, CancelEventArgs e)
        {
            string s_ac004 = txtEdit_ac004.Text.Trim();
            if (string.IsNullOrWhiteSpace(s_ac004)) return;

            int i;
            if (int.TryParse(s_ac004, out i))
            {
                if (i < 0)
                {
                    txtEdit_ac004.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    txtEdit_ac004.ErrorText = "年龄不能小于0!";
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 死亡时间校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateEdit_ac010_Validating(object sender, CancelEventArgs e)
        {
            if (dateEdit_ac010.EditValue == null) return;
            if (DateTime.Compare((DateTime)dateEdit_ac010.EditValue, System.DateTime.Now) > 0)
            {
                dateEdit_ac010.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                dateEdit_ac010.ErrorText = "死亡时间不能大于系统当前时间!";
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 到达时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateEdit_ac020_Validating(object sender, CancelEventArgs e)
        {
            if (dateEdit_ac020.EditValue == null) return;
            if (DateTime.Compare((DateTime)dateEdit_ac020.EditValue, System.DateTime.Now) > 0)
            {
                dateEdit_ac020.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                dateEdit_ac020.ErrorText = "到达时间不能大于系统当前时间!";
                e.Cancel = true;
            }
        }

        private void B_ok_Click(object sender, EventArgs e)
        {
            if (!SaveCheck()) return;  //数据合法性校验!!!

            if (action.Equals("add"))
            {
                ac01.ac001 = Tools.GetEntityPK("AC01");
            }
            

            ac01.ac002 = rg_ac002.EditValue.ToString();     //性别
            ac01.ac003 = txtEdit_ac003.Text;                //逝者姓名
            ac01.ac004 = int.Parse(txtEdit_ac004.Text);     //年龄
            ac01.ac005 = lookUp_ac005.EditValue.ToString(); //死亡原因
            ac01.ac014 = txtedit_ac014.Text;                //身份证号
            ac01.ac007 = lookUp_ac007.EditValue.ToString(); //籍贯-所属区县
            ac01.ac008 = txtEdit_ac008.Text;                //籍贯-详细地址
			ac01.ac006 = lookup_ash.EditValue.ToString(); //骨灰处理方式
			ac01.ac070 = lookup_ac070.EditValue.ToString();

            if (dateEdit_ac010.EditValue != null)
                ac01.ac010 = DateTime.Parse(dateEdit_ac010.EditValue.ToString());  //死亡时间


            ac01.ac009 = txtEdit_ac009.Text;                //接灵地址

            ac01.ac020 = DateTime.Parse(dateEdit_ac020.EditValue.ToString());      //到达中心时间

            ac01.ac050 = txtEdit_ac050.Text;                //联系人
            ac01.ac051 = txtEdit_ac051.Text;                //联系电话

            if (!(lookUp_ac052.EditValue == null || lookUp_ac052.EditValue is System.DBNull))
            {
                ac01.ac052 = lookUp_ac052.EditValue.ToString(); //与逝者关系
            }

            ac01.ac055 = txtEdit_ac055.Text;                //联系地址

            if (lookUp_ac060.EditValue != null)
                ac01.ac060 = lookUp_ac060.EditValue.ToString(); //灵车司机

            ac01.ac100 = Envior.cur_userId;                 //经办人
            ac01.ac200 = DateTime.Now;                      //经办日期
            ac01.ac110 = Envior.cur_userId;                 //最后经办人
            ac01.ac220 = DateTime.Now;                      //最后经办日期
            ac01.ac099 = mem_ac099.Text;                    //备注
            ac01.status = "1";                              //当前状态 

			if (action.Equals("add"))
			{
				if (lookup_ash.EditValue.ToString() != "2" /*骨灰寄存:放弃*/)
				{
					ac01.ac080 = FireAction.GenFireOrder(lookup_ac070.EditValue.ToString());
					label_forder.Text = ac01.ac080.ToString();
				}
			}
			

            try
            {
				string s_tip = "保存成功!";
                if (action.Equals("add"))
                {
                    ac01_dao.Insert(ac01);
					if (ac01.ac080 != null) s_tip = s_tip + "\r\n" + "火化序号:" + ac01.ac080.ToString();
					
                }
                else
                {
                    ac01_dao.Update(ac01);
                }

				MessageBox.Show(s_tip, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

				if (action.Equals("add"))
				{
					if (MessageBox.Show("现在打印【火化登记单】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						PrtServAction.Print_CheckinNotice(ac01.ac001,this.Handle.ToInt32());
					}
				}

                if(businessObject != null)
                    businessObject.swapdata["AC001"] = ac01.ac001;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("保存数据失败!\n" + ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
   
        }

        /// <summary>
        /// 保存前检查
        /// </summary>
        /// <returns></returns>
        private bool SaveCheck()
        {
            //逝者姓名
            if (string.IsNullOrWhiteSpace(txtEdit_ac003.Text.Trim()))
            {
                txtEdit_ac003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                txtEdit_ac003.ErrorText = "逝者姓名必须输入!";
                txtEdit_ac003.Focus();
                return false;
            }
            //年龄
            if (string.IsNullOrWhiteSpace(txtEdit_ac004.Text.Trim()))
            {
                txtEdit_ac004.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                txtEdit_ac004.ErrorText = "年龄必须输入!";
                txtEdit_ac004.Focus();
                return false;
            }
            //死亡原因
            if (lookUp_ac005.EditValue == null)
            {
                lookUp_ac005.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                lookUp_ac005.ErrorText = "死亡原因必须输入!";
                lookUp_ac005.Focus();
                return false;
            }
            //逝者户籍
            if (lookUp_ac007.EditValue == null)
            {
                lookUp_ac007.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                lookUp_ac007.ErrorText = "逝者户籍必须输入!";
                lookUp_ac007.Focus();
                return false;
            }
            //联系人
            if (string.IsNullOrWhiteSpace(txtEdit_ac050.Text))
            {
                txtEdit_ac050.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                txtEdit_ac050.ErrorText = "联系人必须输入!";
                txtEdit_ac050.Focus();
                return false;
            }
            //与逝者关系
            if (string.IsNullOrWhiteSpace(lookUp_ac052.EditValue.ToString()))
            {
                lookUp_ac052.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                lookUp_ac052.ErrorText = "与逝者关系必须输入!";
                lookUp_ac052.Focus();
                return false;
            }
            //联系电话
            if (string.IsNullOrWhiteSpace(txtEdit_ac051.Text))
            {
                txtEdit_ac051.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                txtEdit_ac051.ErrorText = "联系人必须输入!";
                txtEdit_ac051.Focus();
                return false;
            }

			//骨灰处理
			if (string.IsNullOrEmpty(lookup_ash.EditValue.ToString()))
			{
				lookup_ash.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				lookup_ash.ErrorText = "骨灰处理方式必须输入!";
				lookup_ash.Focus();
				return false;
			}

			if (lookup_ash.EditValue.ToString() != "2" /*骨灰寄存:放弃*/ && lookup_ac070.EditValue == null)
			{
				lookup_ac070.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				lookup_ac070.ErrorText = "请选择火化炉!";
				lookup_ac070.Focus();
				return false;
			}

			return true;
        }


    }
}