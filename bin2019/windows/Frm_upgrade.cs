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
using JEast.Misc;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using DevExpress.XtraSplashScreen;

namespace JEast.windows
{
	public partial class Frm_upgrade : MyDialog
	{
		public Frm_upgrade()
		{
			InitializeComponent();
		}

		private void Frm_upgrade_Load(object sender, EventArgs e)
		{

		}

		private void B_exit_Click(object sender, EventArgs e)
		{
			this.Dispose();
		}

		private void ButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			openFileDialog1.Filter = "Zip files (*.zip)|*.zip";
			openFileDialog1.RestoreDirectory = true;
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				buttonEdit1.EditValue = openFileDialog1.FileName;
				string shortname = openFileDialog1.SafeFileName;
				textEdit1.Text = shortname.Substring(0, shortname.Length - 4);
			}
		}

		private void B_ok_Click(object sender, EventArgs e)
		{
			if (textEdit1.EditValue == null || textEdit1.EditValue is System.DBNull)
			{
				MessageBox.Show("请输入版本号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				textEdit1.Focus();
				return;
			}
			//else if (string.Compare(textEdit1.Text, AppInfo.AppVersion) < 0)
			//{
			//	MessageBox.Show("新版本号必须大于现有版本号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//	return;
			//}
			string s_version = textEdit1.EditValue.ToString();
			if (buttonEdit1.EditValue == null || buttonEdit1.EditValue is System.DBNull)
			{
				MessageBox.Show("请选择要升级的文件!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				buttonEdit1.Focus();
				return;
			}
			string fname = buttonEdit1.EditValue.ToString();
			if (!File.Exists(fname))
			{
				MessageBox.Show("文件不存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				buttonEdit1.Focus();
				return;
			}

			string sql = "insert into fv01(verid,ufile) values(:ver,:f)";

			OracleCommand cmd = new OracleCommand(sql, SqlAssist.conn);
			FileStream fs = File.OpenRead(fname);

			SplashScreenManager.ShowDefaultWaitForm("请等待", "上传中....");

			byte[] b = new byte[fs.Length];
			fs.Read(b, 0, b.Length);
			fs.Close();

			cmd.Parameters.Add("ver", OracleDbType.Varchar2, 20).Value = s_version;
			cmd.Parameters.Add("f", OracleDbType.Blob, b.Length).Value = b;

			cmd.ExecuteNonQuery();

			SplashScreenManager.CloseDefaultWaitForm();

			MessageBox.Show("上传成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			this.Dispose();
		}
	}
}