using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using JEast.BaseObject;
using DevExpress.XtraSplashScreen;
using JEast.DataSet;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using JEast.Misc;
using JEast.windows;
using JEast.Action;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace JEast.BusinessObject
{
    public partial class RegStru : BaseBusiness
    {
        private DataTable gridTable = new DataTable("grid");
        private string curRegionId = string.Empty;
        private RGDataSet rgset = new RGDataSet();


        public RegStru()
        {
            InitializeComponent();
        }

        private void RegStru_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm("请等待", "处理中....");

            rgset.rg01Adapter.Fill(rgset.Rg01);
            rgset.bi01Adapter.Fill(rgset.Bi01);
            rgset.ly01Adapter.Fill(rgset.Ly01);
            treeList1.DataSource = rgset.Rg01;

            treeList1.ExpandToLevel(2);

            try
            {
                SplashScreenManager.CloseDefaultWaitForm();
            }
            finally { }


        }

        //判断是否可编辑
        private void TreeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            TreeList currentTreeList = sender as TreeList;

            if (currentTreeList != null)
            {
                TreeListNode node = currentTreeList.FocusedNode;
                DevExpress.XtraTreeList.Columns.TreeListColumn column = currentTreeList.FocusedColumn;

                if (column.FieldName == "RG003" && node.GetValue("RG003").ToString() == "寄存结构")
                {
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
            }
        }

        /// <summary>
        /// 树节点选择变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null) return;
            if (e.Node.Level < 3)
            {
                pictureBox1.Visible = true;
                gridControl1.Visible = false;
                return;
            }
            else
            {
                pictureBox1.Visible = false;
                gridControl1.Visible = true;
                curRegionId = e.Node.GetValue("RG001").ToString();
            }
            SplashScreenManager.ShowDefaultWaitForm("请等待", "处理中....");
            DrawGrid(e.Node);
            SplashScreenManager.CloseDefaultWaitForm();
        }



        /// <summary>
        /// 绘制寄存号位
        /// </summary>
        /// <param name="regionNode"></param>
        private void DrawGrid(TreeListNode regionNode)
        {
            int rows = int.Parse(regionNode.GetValue("RG020").ToString());  //层数
            int cols = int.Parse(regionNode.GetValue("RG021").ToString());

            gridView1.BeginUpdate();

            /////////清除所有数据
            gridTable.Clear();
            gridTable.Columns.Clear();


            ////生成列
            DataColumn col = null;
            DataRow row = null;
            for (int i = 1; i <= cols; i++)
            {
                col = new DataColumn("col" + i.ToString(), typeof(string));
                col.ReadOnly = true;
                gridTable.Columns.Add(col);
            }

            DataRow[] datarow_arry = rgset.Bi01.Select("RG001='" + regionNode.GetValue("RG001").ToString() + "'", "BI005 DESC,BI008 ASC");
            int bitIndex = 0;
            for (int i = 1; i <= rows; i++)
            {
                row = gridTable.NewRow();
                for (int j = 1; j <= cols; j++)
                {
                    row.SetField(j - 1, datarow_arry[bitIndex]["BI003"]);
                    bitIndex++;
                }
                gridTable.Rows.Add(row);
            }

            gridControl1.DataSource = gridTable;
            gridView1.PopulateColumns();

            //设置列宽 
            for (int i = 1; i <= cols; i++)
            {
                gridView1.Columns[i - 1].Width = 50;
            }
            //grid标题
            TreeListNode hall_node = regionNode.ParentNode.ParentNode;
            TreeListNode room_node = regionNode.ParentNode;
            gridView1.ViewCaption = hall_node.GetDisplayText("RG003") + "-" + room_node.GetDisplayText("RG003") + "-" + regionNode.GetDisplayText("RG003");

            gridView1.EndUpdate();

        }

        /// <summary>
        /// 新建同级节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode node = treeList1.FocusedNode;
            string nodeType = node.GetValue("RG002").ToString();

            if (nodeType == "0")        //顶级节点,退出
            {
                MessageBox.Show("不能在顶级节点创建!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nodeType != "3")        //非寄存排
            {
                DataRow newrow = rgset.Rg01.NewRow();
                newrow["RG001"] = Tools.GetEntityPK("RG01");
                newrow["RG002"] = nodeType;                      //类型
                newrow["RG003"] = this.GetSuggestName(node.ParentNode, nodeType);
                newrow["RG009"] = node.ParentNode.Id;            //父节点编号 
                newrow["STATUS"] = "1";                          //状态 
                TreeListNode newNode = treeList1.AppendNode(newrow, treeList1.FocusedNode.ParentNode.Id);
                treeList1.SetFocusedNode(newNode);
                treeList1.ShowEditor();
            }
            else                        //寄存排
            {
                CreateRegion(node.ParentNode);
            }
        }

        /// <summary>
        /// 获取建议名称
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public string GetSuggestName(TreeListNode parent, string level)
        {
            if (level == "1")         //寄存楼
            {
                return "新寄存楼" + (parent.Nodes.Count + 1).ToString();
            }
            else if (level == "2")    //寄存室
            {
                return "新寄存室" + (parent.Nodes.Count + 1).ToString();
            }
            else if (level == "3")    //寄存排
            {
                return (parent.Nodes.Count + 1).ToString() + "排";
            }
            else
                return "";
        }

        /// <summary>
        /// 返回结果集
        /// </summary>
        /// <returns></returns>
        public RGDataSet GetDataset()
        {
            return rgset;
        }


        /// <summary>
        /// 创建新 寄存排
        /// </summary>
        public void CreateRegion(TreeListNode parentNode)
        {
            Frm_region hall_edit = new Frm_region();

            hall_edit.swapdata["action"] = "add";
            hall_edit.swapdata["bobject"] = this;
            hall_edit.swapdata["pnode"] = parentNode;

            if (hall_edit.ShowDialog(this.ParentForm) == DialogResult.OK)
            {
                DataRow newrow = this.swapdata["nodedata"] as DataRow;
                TreeListNode newNode = treeList1.AppendNode(newrow, parentNode.Id);

                ///创建层
                DataRow layerRow = null;
                DataRow bitRow = null;
                int startbit = int.Parse(newrow["RG010"].ToString());
                int layerIndex;
                int colIndex;
                string rg030 = newrow["RG030"].ToString();                      //起始位置
                bool flag = true;

                if (rg030 == "0" || rg030 == "2")  //左上或右上
                    layerIndex = int.Parse(newrow["RG020"].ToString());
                else
                    layerIndex = 1;


                for (int i = 1; i <= int.Parse(newrow["RG020"].ToString()); i++) //rg020层数
                {
                    layerRow = rgset.Ly01.NewRow();
                    layerRow["LY001"] = Tools.GetEntityPK("LY01");
                    layerRow["RG001"] = newNode.GetValue("RG001").ToString();    //寄存架编号
                    layerRow["LY002"] = i;
                    rgset.Ly01.Rows.Add(layerRow);

                    if ((rg030 == "0" || rg030 == "1") && flag) //左上或左下
                    {
                        colIndex = 1;
                    }
                    else if ((rg030 == "2" || rg030 == "3") && !flag)
                    {
                        colIndex = 1;
                    }
                    else
                    {
                        colIndex = int.Parse(newrow["RG021"].ToString());
                    }

                    ///创建号位
                    for (int j = 1; j <= int.Parse(newrow["RG021"].ToString()); j++)  //rg021每层号位数
                    {
                        bitRow = rgset.Bi01.NewRow();
                        bitRow["BI001"] = Tools.GetEntityPK("BI01");
                        bitRow["RG001"] = newNode.GetValue("RG001").ToString();                        //寄存架编号
                        bitRow["BI020"] = newNode.ParentNode.GetValue("RG001").ToString();             //寄存室编号
                        bitRow["BI030"] = newNode.ParentNode.ParentNode.GetValue("RG001").ToString();  //寄存楼编号

                        bitRow["BI002"] = startbit + j - 1;
                        bitRow["BI003"] = (startbit + j - 1).ToString().PadLeft(4, '0');
                        bitRow["BI005"] = layerIndex;                               //层号
                        bitRow["BI008"] = colIndex;                                 //列数.
                        bitRow["BI009"] = 0;                                        //价格
                        bitRow["BI007"] = "0";                                      //价格锁
                        bitRow["STATUS"] = "9";                                     //状态-空闲
                        rgset.Bi01.Rows.Add(bitRow);


                        //RG033 排列顺序 0-顺序 1-蛇形
                        if (newrow["RG033"].ToString() == "0")
                        {
                            if (rg030 == "0" || rg030 == "1") //左上或左下
                            {
                                if (colIndex >= int.Parse(newrow["RG021"].ToString()))
                                    colIndex = 1;
                                else
                                    colIndex++;
                            }
                            else
                            {
                                if (colIndex <= 1)
                                    colIndex = 1;
                                else
                                    colIndex--;
                            }
                        }
                        else
                        {
                            if ((rg030 == "0" || rg030 == "1") && flag) //左上或左下
                            {
                                if (colIndex >= int.Parse(newrow["RG021"].ToString()))
                                {
                                    colIndex = int.Parse(newrow["RG021"].ToString());
                                    //flag = !flag;
                                }
                                else
                                    colIndex++;
                            }
                            else if ((rg030 == "0" || rg030 == "1") && !flag)
                            {
                                if (colIndex <= 1)
                                {
                                    colIndex = 1;
                                    //flag = !flag;
                                }
                                else
                                {
                                    colIndex--;
                                }
                            }
                            else if ((rg030 == "2" || rg030 == "3") && flag)
                            {
                                if (colIndex <= 1)
                                {
                                    colIndex = 1;
                                    //flag = !flag;
                                }
                                else
                                {
                                    colIndex--;
                                }
                            }
                            else if ((rg030 == "2" || rg030 == "3") && !flag)
                            {
                                if (colIndex >= int.Parse(newrow["RG021"].ToString()))
                                {
                                    colIndex = int.Parse(newrow["RG021"].ToString());
                                    //flag = !flag;
                                }
                                else
                                {
                                    colIndex++;
                                }
                            }

                        }
                    }
                    startbit += int.Parse(newrow["RG021"].ToString());

                    if (newrow["RG033"].ToString() == "1")
                    {
                        flag = !flag;
                    }

                    if (rg030 == "0" || rg030 == "2")  //左上或右上
                        layerIndex--;
                    else
                        layerIndex++;
                }

                treeList1.SetFocusedNode(newNode);
                DrawGrid(newNode);
 
            }

        }

        private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode node = treeList1.FocusedNode;
            string nodeType = node.GetValue("RG002").ToString();

            if (nodeType == "3")        //最末级节点,退出
            {
                MessageBox.Show("不能在末级节点创建!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nodeType != "2")   //非寄存排
            {
                //this.CreateHall();
                DataRow newrow = rgset.Rg01.NewRow();
                newrow["RG001"] = Tools.GetEntityPK("RG01");

                if (nodeType == "0")
                    newrow["RG002"] = "1";           //寄存堂
                else if (nodeType == "1")
                    newrow["RG002"] = "2";           //寄存室

                newrow["RG003"] = this.GetSuggestName(node, newrow["RG002"].ToString());
                newrow["RG009"] = node.Id;  //父节点编号
                newrow["STATUS"] = "1";          //状态
                TreeListNode newNode = treeList1.AppendNode(newrow, node.Id);
                treeList1.SetFocusedNode(newNode);
                treeList1.ShowEditor();
            }
            if (nodeType == "2")   //创建寄存排
            {
                this.CreateRegion(node);
            }
        }

        private void GridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            string s_bitStatus = RegisterAction.GetBitStatus(curRegionId, e.CellValue.ToString());
            if (s_bitStatus == "9" || s_bitStatus == "n")
            {
                e.Appearance.BackColor = Color.Green;
                e.Appearance.ForeColor = Color.White;
            }
			else if(s_bitStatus == "0")
			{
				e.Appearance.BackColor = Color.White;
				e.Appearance.ForeColor = Color.White;
			}
            else
            {
                e.Appearance.BackColor = Color.Yellow;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// 绘制行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    int layerNum = gridView1.RowCount - e.RowHandle;
                    e.Info.DisplayText = (gridView1.RowCount - e.RowHandle).ToString() + "【" + RegisterAction.GetLayerPrice(curRegionId, layerNum).ToString() + "】";
                    //e.Info.DisplayText = (gridView1.RowCount - e.RowHandle).ToString(); // (e.RowHandle + 1).ToString();
                }
                else if (e.RowHandle < 0 && e.RowHandle > -1000)
                {
                    e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
                    e.Info.DisplayText = "G" + e.RowHandle.ToString();
                }
            }
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode curNode = treeList1.FocusedNode;
            if (curNode.Level == 0) return;

            DialogResult result = MessageBox.Show("确认要删除当前节点及其所有子节点?", "删除确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Cancel) return;

            ////判断是否有被占用的节点
            DataTable dt = rgset.Bi01;
            int count = 0;
            if (curNode.Level == 3)
            {
                count = dt.AsEnumerable().Count<DataRow>(c => c["RG001"].ToString() == curNode.GetValue("RG001").ToString() && c["STATUS"].ToString() == "1");
            }
            else if (curNode.Level == 2)
            {
                count = dt.AsEnumerable().Count<DataRow>(c => c["BI020"].ToString() == curNode.GetValue("RG001").ToString() && c["STATUS"].ToString() == "1");
            }
            else if (curNode.Level == 1)
            {
                count = dt.AsEnumerable().Count<DataRow>(c => c["BI030"].ToString() == curNode.GetValue("RG001").ToString() && c["STATUS"].ToString() == "1");
            }

            if (count > 0)
            {
                MessageBox.Show("存在被占用的号位,不能删除!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                curNode.SetValue("STATUS", "0");
                treeList1.SetFocusedNode(curNode.ParentNode);
            }
        }

        private void BarButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshData();
        }


        private void RefreshData()
        {
            if (rgset.Rg01.GetChanges() != null || rgset.Bi01.GetChanges() != null || rgset.Ly01.GetChanges() != null)
            {
                DialogResult dr = MessageBox.Show("刷新会丢失未保存的更新,是否继续?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Cancel) return;
            }
            SplashScreenManager.ShowDefaultWaitForm("请等待", "刷新数据....");
            treeList1.BeginUpdate();
            treeList1.ClearNodes();

            rgset.Rg01.Clear();
            rgset.Bi01.Clear();
            rgset.Ly01.Clear();

            rgset.rg01Adapter.Fill(rgset.Rg01);
            rgset.bi01Adapter.Fill(rgset.Bi01);
            rgset.ly01Adapter.Fill(rgset.Ly01);

            treeList1.ExpandToLevel(2);

            treeList1.EndUpdate();

            try
            {
                SplashScreenManager.CloseDefaultWaitForm();
            }
            catch { }
            
        }



        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            GridHitInfo _info;
            Point _pt = gridView1.GridControl.PointToClient(Control.MousePosition);
            _info = gridView1.CalcHitInfo(_pt);
            if (_info.HitTest != GridHitTest.RowCell)
                return;

            TreeListNode curNode = treeList1.FocusedNode;
            string bi003 = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.FocusedColumn).ToString();

            Frm_Bi01 frm_bi01 = new Frm_Bi01();
            frm_bi01.swapdata["bi003"] = bi003;
            frm_bi01.swapdata["table"] = rgset.Bi01;
            frm_bi01.swapdata["regionId"] = curNode.GetValue("RG001");


            var r = rgset.Bi01.AsEnumerable().Where<DataRow>(c => c["RG001"].ToString() == curNode.GetValue("RG001").ToString() && c["BI003"].ToString() == bi003);
            if (r.Count<DataRow>() > 0)
            {
                frm_bi01.swapdata["bi001"] = r.First()["BI001"].ToString();
            }
            else
            {
                MessageBox.Show("检索号位错误!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            DialogResult dr = frm_bi01.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.DrawGrid(curNode);
            }
        }

        /// <summary>
        /// 层定价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode curNode = treeList1.FocusedNode;
            if (curNode == null || curNode.Level != 3)
            {
                MessageBox.Show("请选择寄存排", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Frm_LayerPrice regionprice = new Frm_LayerPrice();

            regionprice.swapdata["table"] = rgset.Ly01;
            regionprice.swapdata["regionId"] = curNode.GetValue("RG001");

            DialogResult dr = regionprice.ShowDialog();
            if (dr == DialogResult.OK)
            {
                var rows = rgset.Ly01.AsEnumerable().Where<DataRow>(c => c["RG001"].ToString() == curNode.GetValue("RG001").ToString());
                foreach (DataRow r in rows)
                {
                    var bitrows = rgset.Bi01.AsEnumerable().Where<DataRow>
                        (c => c["RG001"].ToString() == curNode.GetValue("RG001").ToString() && c["BI005"].ToString() == r["LY002"].ToString() && c["BI007"].ToString() == "0");
                    foreach (DataRow br in bitrows)
                    {
                        br["BI009"] = r["PRICE"];
                    }
                }
                
            }
        }

        private void BarButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            treeList1.SetFocusedNode(treeList1.GetNodeByVisibleIndex(0));
            treeList1.PostEditor();
            treeList1.CloseEditor();
            try
            {
                rgset.rg01Adapter.Update(rgset.Rg01);
                rgset.ly01Adapter.Update(rgset.Ly01);
                rgset.bi01Adapter.Update(rgset.Bi01);
                 
                MessageBox.Show("保存成功!", "提示");
            }
            catch (Exception ee)
            {
                MessageBox.Show("保存数据错误!\n" + ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.RefreshData();
        }
    }
}
