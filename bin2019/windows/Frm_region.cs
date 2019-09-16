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
using JEast.BusinessObject;
using DevExpress.XtraTreeList.Nodes;
using JEast.Misc;

namespace JEast.windows
{
    public partial class Frm_region : MyDialog
    {
        RegStru rs = null;
        public Frm_region()
        {
            InitializeComponent();
        }

        private void Frm_region_Load(object sender, EventArgs e)
        {
            rs = this.swapdata["bobject"] as RegStru;
            TreeListNode parentNode = this.swapdata["pnode"] as TreeListNode;

            if (this.swapdata["action"].ToString() == "add")  //新增
            {
                combo_rg030.SelectedIndex = 0;
                combo_rg033.SelectedIndex = 0;
                txt_rg003.Text = rs.GetSuggestName(parentNode, "3");

                if (!parentNode.HasChildren)
                {
                    txt_rg010.Text = "1";
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(parentNode.LastNode.GetValue("RG011").ToString()))
                        txt_rg010.Text = "1";
                    else
                        txt_rg010.Text = (int.Parse(parentNode.LastNode.GetValue("RG011").ToString()) + 1).ToString();
                }
            }
        }

        private void SimpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            string rg003 = txt_rg003.Text;
            int rg010;    //起始号位
            //int rg011 ;    //终止号位
            int rg020;    //层数
            int rg021;    //每层号位数

            ///输入校验
            if (String.IsNullOrEmpty(rg003))
            {
                txt_rg003.Focus();
                txt_rg003.ErrorText = "请输入寄存排名字!";
                return;
            }
            if (string.IsNullOrEmpty(txt_rg010.Text))
            {
                txt_rg010.Focus();
                txt_rg010.ErrorText = "请输入起始号位!";
                return;
            }
            else
            {
                rg010 = int.Parse(txt_rg010.Text);
            }

            if (string.IsNullOrEmpty(txt_rg020.Text))
            {
                txt_rg020.Focus();
                txt_rg020.ErrorText = "请输入层数!";
                return;
            }
            else
            {
                rg020 = int.Parse(txt_rg020.Text);
            }

            if (string.IsNullOrEmpty(txt_rg021.Text))
            {
                txt_rg021.Focus();
                txt_rg021.ErrorText = "请输入每层号位数!";
                return;
            }
            else
            {
                rg021 = int.Parse(txt_rg021.Text);
            }

            //////////////////  校验结束  ///////////////////////////
            DataRow newrow = rs.GetDataset().Rg01.NewRow();
            newrow["RG001"] = Tools.GetEntityPK("RG01");
            newrow["RG002"] = "3";
            newrow["RG003"] = rg003;
            newrow["RG010"] = rg010;
            //newrow["RG011"] = rg011;
            newrow["RG020"] = rg020;
            newrow["RG021"] = rg021;
            newrow["RG030"] = combo_rg030.SelectedIndex.ToString();
            newrow["RG033"] = combo_rg033.SelectedIndex.ToString();
            newrow["RG009"] = (this.swapdata["pnode"] as TreeListNode).Id;  //父节点编号
            newrow["STATUS"] = "1";          //状态

            rs.swapdata["nodedata"] = newrow;

            DialogResult = DialogResult.OK;

            this.Close();

        }

        private void Txt_rg010_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txt_rg010.Text))
            {
                txt_rg010.ErrorText = "请输入起始号位!";
                e.Cancel = true;
                return;
            }
            if (int.Parse(txt_rg010.EditValue.ToString()) <= 0)
            {
                txt_rg010.ErrorText = "请输入大于0的数字!";
                e.Cancel = true;
            }
        }

        private void Txt_rg011_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_rg011.Text) && int.Parse(txt_rg011.Text) <= 0)
            {
                txt_rg011.ErrorText = "请输入大于0的数字!";
                e.Cancel = true;
                return;
            }
        }

        private void Txt_rg020_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txt_rg020.Text))
            {
                txt_rg020.ErrorText = "请输入层数!";
                e.Cancel = true;
                return;
            }
            if (int.Parse(txt_rg020.Text) <= 0)
            {
                txt_rg020.ErrorText = "请输入大于0的数字!";
                e.Cancel = true;
            }
        }

        private void Txt_rg021_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txt_rg021.Text))
            {
                txt_rg021.ErrorText = "请输入每层号位数!";
                e.Cancel = true;
                return;
            }
            if (int.Parse(txt_rg021.Text) <= 0)
            {
                txt_rg021.ErrorText = "请输入大于0的数字!";
                e.Cancel = true;
            }
        }
    }
}