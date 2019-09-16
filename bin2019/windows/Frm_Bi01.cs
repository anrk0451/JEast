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

namespace JEast.windows
{
    public partial class Frm_Bi01 : MyDialog
    {
        private string regionId;
        private DataTable bi01;
        private string bi003;
        private string bi001;

        public Frm_Bi01()
        {
            InitializeComponent();
        }

        private void Frm_Bi01_Load(object sender, EventArgs e)
        {
            regionId = this.swapdata["regionId"].ToString();
            bi01 = this.swapdata["table"] as DataTable;
            bi003 = this.swapdata["bi003"].ToString();
            bi001 = this.swapdata["bi001"].ToString();

            //获取号位记录
            var result = bi01.AsEnumerable().Where<DataRow>(c => c["RG001"].ToString() == regionId && c["BI003"].ToString() == bi003);
            if (result.Count<DataRow>() > 0)
            {
                te_bi003.Text = result.First()["BI003"].ToString();
                te_price.Text = result.First()["BI009"].ToString();
                string status = result.First()["STATUS"].ToString();
                if (status == "1")
                    radioButton3.Enabled = false;
                else if (status == "0")
                {
                    radioButton3.Checked = true;
                    te_price.Enabled = false;
                    radioButton3.Text = "使有效";
                }
                te_price.Focus();
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                te_price.Enabled = true;
                te_bi003.Enabled = false;
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                te_price.Enabled = false;
                te_bi003.Enabled = true;
            }
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                te_price.Enabled = false;
                te_bi003.Enabled = false;
            }   
        }

        private void Te_price_Validating(object sender, CancelEventArgs e)
        {
            if (decimal.Parse(te_price.Text) < 0)
            {
                te_price.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_price.ErrorText = "价格不能小于0";
                e.Cancel = true;
            }
        }

        private void Te_bi003_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(te_bi003.Text))
            {
                te_bi003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_bi003.ErrorText = "号位描述不能为空!";
                e.Cancel = true;
            }
            else
            {
                var r = bi01.AsEnumerable().Where<DataRow>
                    (c => c["RG001"].ToString() == regionId && c["BI003"].ToString() == te_bi003.Text && c["BI001"].ToString() != bi001);
                if (r.Count<DataRow>() > 0)
                {
                    te_bi003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    te_bi003.ErrorText = "号位描述重复!";
                    e.Cancel = true;
                }
            }
        }

        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            var row = bi01.AsEnumerable().Where<DataRow>(c => c["BI001"].ToString() == bi001);
            if (row.Count<DataRow>() == 0) return;

            if (radioButton1.Checked)           //修改价格
            {
                decimal price = decimal.Parse(te_price.Text);
                row.First()["BI009"] = price;
                row.First()["BI007"] = "1";
            }
            else if (radioButton2.Checked)      //修改号位描述
            {
                string bi003 = te_bi003.Text;
                row.First()["BI003"] = bi003;
            }
            else if (radioButton3.Checked)      //修改号位状态
            {
                if (radioButton3.Text == "使有效")
                {
					row.First()["BI003"] = row.First()["BI003"].ToString().Substring(1);
					row.First()["STATUS"] = "9";
                }
                else
                {
					row.First()["BI003"] = "#" + row.First()["BI002"].ToString().PadLeft(4, '0');
					row.First()["STATUS"] = "0";
                }
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SimpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}