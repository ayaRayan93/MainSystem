using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraTab;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class ProductsDetails_Report : Form
    {
        MySqlConnection conn;
        XtraTabControl MainTabControlPointSale;
        int DelegateBranchID = 0;

        //public static XtraTabPage MainTabPageAddPointSale;
        //Panel panelAddBank;

        //public static XtraTabPage MainTabPageUpdateDetails;
        //Panel panelUpdateBank;

        //public static XtraTabPage MainTabPagePrintingBank;
        //Panel panelPrintingBank;

        //public static Bank_Print bankPrint;

        public static GridControl gridcontrol;
        public static bool UpdateDetailsTextChangedFlag = false;
        bool PSloaded = false;

        int delegateID = -1;
        int billNum = 0;
        MainForm main;

        public ProductsDetails_Report(MainForm min, int DelegateId, int BillNum)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            main = min;
            MainTabControlPointSale = MainForm.tabControlPointSale;

            gridcontrol = gridControl1;

            delegateID = DelegateId;
            billNum = BillNum;
        }

        private void Delegate_Report_Load(object sender, EventArgs e)
        {
            try
            {
                search();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView selRow = (DataRowView)(gridView1.GetRow(gridView1.GetSelectedRows()[0]));
                // && gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "Error") == "0"
                if (selRow[0].ToString() != "")
                {
                    QuantityUpdate qu = new QuantityUpdate(selRow);
                    qu.ShowDialog();
                }
                else
                {
                    MessageBox.Show("تاكد من اختيارك");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                GridView view = gridView1 as GridView;
                delete(view);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                //MainTabPagePrintingBank.Name = "tabPagePrintingBank";
                //MainTabPagePrintingBank.Text = "طباعة البنوك";
                //panelPrintingBank.Name = "panelPrintingBank";
                //panelPrintingBank.Dock = DockStyle.Fill;
                
                //panelPrintingBank.Controls.Clear();
                //bankPrint = new Bank_Print();
                //bankPrint.Size = new Size(1059, 638);
                //bankPrint.TopLevel = false;
                //bankPrint.FormBorderStyle = FormBorderStyle.None;
                //bankPrint.Dock = DockStyle.Fill;
                //panelPrintingBank.Controls.Add(bankPrint);
                //MainTabPagePrintingBank.Controls.Add(panelPrintingBank);
                //MainTabControlBank.TabPages.Add(MainTabPagePrintingBank);
                //bankPrint.Show();
                //MainTabControlBank.SelectedTabPage = MainTabPagePrintingBank;

                //Main.loadedPrintBank = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    //GridView view = sender as GridView;
                    GridView view = gridView1 as GridView;
                    delete(view);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            try
            {
                if (e.SelectedControl != gridControl1) return;
                GridHitInfo gridhitinfo = gridView1.CalcHitInfo(e.ControlMousePosition);
                object o = gridhitinfo.HitTest.ToString();
                string text = gridhitinfo.HitTest.ToString();
                e.Info = new DevExpress.Utils.ToolTipControlInfo(o, text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        //{
        //    try
        //    {
        //        GridView View = sender as GridView;
        //        if (e.RowHandle >= 0)
        //        {
        //            string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Error"]);
        //            if (category == "1")
        //            {
        //                e.Appearance.BackColor = Color.Salmon;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //functions
        public void search()
        {
            PSloaded = false;
            DelegateBranchID = UserControl.UserBranch(conn);
            //if (delegateID == -1)
            //{
            //    delegateID = -1;
            //}

            conn.Open();
            string query = "select * from delegate where Branch_ID=" + DelegateBranchID;
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comDelegate.DataSource = dt;
            comDelegate.DisplayMember = dt.Columns["Delegate_Name"].ToString();
            comDelegate.ValueMember = dt.Columns["Delegate_ID"].ToString();
            //comDelegate.SelectedIndex = delegateID;

            comDelegate.SelectedValue = delegateID;
            txtBillNum.Text = billNum.ToString();

            query = "select * from customer";
            da = new MySqlDataAdapter(query, conn);
            dt = new DataTable();
            da.Fill(dt);
            comCustomer.DataSource = dt;
            comCustomer.DisplayMember = dt.Columns["Customer_Name"].ToString();
            comCustomer.ValueMember = dt.Columns["Customer_ID"].ToString();
            conn.Close();

            loadFunc();

            PSloaded = true;
            //GridColumn colBillNumber = gridView1.Columns["Bill_Number"];
            //gridView1.BeginSort();
            //gridView1.ClearGrouping();
            //colBillNumber.GroupIndex = 0;
            //gridView1.EndSort();
        }

        public void loadFunc()
        {
            //,dash.Customer_ID as 'العميل'
            conn.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT dash_details.DashDetails_ID,data.Code as 'الكود',dash_details.Type as 'النوع',dash_details.Quantity as 'الكمية',dash_details.Store_Name as 'المخزن',dash_details.Store_ID FROM dash INNER JOIN dash_details ON dash_details.Dash_ID = dash.Dash_ID inner join data on dash_details.Data_ID=data.Data_ID where dash.Branch_ID=" + DelegateBranchID + " and dash.Bill_Number=" + billNum + " and dash_details.Type='بند'", conn);
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet);

            adapter = new MySqlDataAdapter("SELECT dash_details.DashDetails_ID,CAST(dash_details.Data_ID as CHAR(255)) as 'الكود',dash_details.Type as 'النوع',dash_details.Quantity as 'الكمية',dash_details.Store_Name as 'المخزن',dash_details.Store_ID FROM dash INNER JOIN dash_details ON dash_details.Dash_ID = dash.Dash_ID where dash.Branch_ID=" + DelegateBranchID + " and dash.Bill_Number=" + billNum+ " and (dash_details.Type='طقم' or dash_details.Type='عرض')", conn);
            DataSet sourceDataSet2 = new DataSet();
            adapter.Fill(sourceDataSet2);

            sourceDataSet.Merge(sourceDataSet2);

            gridControl1.DataSource = sourceDataSet.Tables[0];
            //gridView1.Columns["Error"].Visible = false;
            gridView1.Columns["DashDetails_ID"].Visible = false;
            gridView1.Columns["Store_ID"].Visible = false;

            string query = "SELECT dash.Customer_ID FROM dash where dash.Branch_ID=" + DelegateBranchID + " and dash.Bill_Number=" + billNum;
            MySqlCommand com = new MySqlCommand(query, conn);
            if (com.ExecuteScalar() != null && com.ExecuteScalar().ToString() != "")
            {
                int CustomerId = Convert.ToInt16(com.ExecuteScalar().ToString());
                comCustomer.SelectedValue = CustomerId;
            }
            else
            {
                comCustomer.SelectedIndex = -1;
            }
            conn.Close();

            for (int i = 1; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = 150;
            }
        }

        void delete(GridView view)
        {
            int[] selRows = ((GridView)gridControl1.MainView).GetSelectedRows();
            // && gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "Error") == "0"
            if (selRows.Length > 0)
            {
                if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
                
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 220,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = "",
                    StartPosition = FormStartPosition.CenterScreen,
                    MaximizeBox = false,
                    MinimizeBox = false
                };
                Label textLabel = new Label() { Left = 340, Top = 20, Text = "ما هو سبب الحذف؟" };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 385, Multiline = true, Height = 80, RightToLeft = RightToLeft };
                Button confirmation = new Button() { Text = "تأكيد", Left = 200, Width = 100, Top = 140, DialogResult = DialogResult.OK };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;
                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    if (textBox.Text != "")
                    {
                        conn.Open();
                        for (int i = 0; i < selRows.Length; i++)
                        {
                            DataRowView selRow = (DataRowView)(((GridView)gridControl1.MainView).GetRow(selRows[i]));

                            string query = "delete from dash_details where DashDetails_ID=" + selRow[0].ToString();
                            MySqlCommand comand = new MySqlCommand(query, conn);
                            comand.ExecuteNonQuery();
                            main.test(delegateID, billNum);

                            UserControl.ItemRecord("dash_details", "حذف", Convert.ToInt16(selRow[0].ToString()), DateTime.Now, textBox.Text, conn);
                        }
                        conn.Close();
                        search();
                    }
                    else
                    {
                        MessageBox.Show("يجب كتابة السبب");
                    }
                }
                else
                { }
            }
            else
            {
                MessageBox.Show("تاكد من اختيارك");
            }
        }

        private void txtBillNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT dash_details.DashDetails_ID,data.Code as 'الكود',dash_details.Type as 'النوع',dash_details.Quantity as 'الكمية',dash_details.Store_Name as 'المخزن',dash_details.Store_ID FROM dash INNER JOIN dash_details ON dash_details.Dash_ID = dash.Dash_ID inner join data on dash_details.Data_ID=data.Data_ID where dash.Branch_ID=" + DelegateBranchID + " and dash.Bill_Number=" + txtBillNum.Text + " and dash_details.Type='بند'", conn);
                    DataSet sourceDataSet = new DataSet();
                    adapter.Fill(sourceDataSet);

                    adapter = new MySqlDataAdapter("SELECT dash_details.DashDetails_ID,CAST(dash_details.Data_ID as CHAR(255)) as 'الكود',dash_details.Type as 'النوع',dash_details.Quantity as 'الكمية',dash_details.Store_Name as 'المخزن',dash_details.Store_ID FROM dash INNER JOIN dash_details ON dash_details.Dash_ID = dash.Dash_ID where dash.Branch_ID=" + DelegateBranchID + " and dash.Bill_Number=" + txtBillNum.Text + " and (dash_details.Type='طقم' or dash_details.Type='عرض')", conn);
                    DataSet sourceDataSet2 = new DataSet();
                    adapter.Fill(sourceDataSet2);

                    sourceDataSet.Merge(sourceDataSet2);

                    gridControl1.DataSource = sourceDataSet.Tables[0];
                    //gridView1.Columns["Error"].Visible = false;
                    gridView1.Columns["DashDetails_ID"].Visible = false;
                    gridView1.Columns["Store_ID"].Visible = false;

                    string query = "SELECT dash.Customer_ID FROM dash where dash.Branch_ID=" + DelegateBranchID + " and dash.Bill_Number=" + billNum;
                    MySqlCommand com = new MySqlCommand(query, conn);
                    if (com.ExecuteScalar() != null && com.ExecuteScalar().ToString() != "")
                    {
                        int CustomerId = Convert.ToInt16(com.ExecuteScalar().ToString());
                        comCustomer.SelectedValue = CustomerId;
                    }
                    else
                    {
                        comCustomer.SelectedIndex = -1;
                    }
                    conn.Close();

                    for (int i = 1; i < gridView1.Columns.Count; i++)
                    {
                        gridView1.Columns[i].Width = 150;
                    }

                    main.test(delegateID, billNum);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (PSloaded)
                {
                    XtraTabPage xtraTabPage = getTabPage("tabPageProductsDetailsReport");
                    if (!IsClear())
                    {
                        xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                    }
                    else
                    {
                        xtraTabPage.ImageOptions.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < MainForm.tabControlPointSale.TabPages.Count; i++)
                if (MainForm.tabControlPointSale.TabPages[i].Name == text)
                {
                    return MainForm.tabControlPointSale.TabPages[i];
                }
            return null;
        }

        public bool IsClear()
        {
            bool flag5 = false;
            foreach (Control co in this.panel1.Controls)
            {
                foreach (Control item in co.Controls)
                {
                    if (item is ComboBox)
                    {
                        if (item.Text == "")
                            flag5 = true;
                        else
                            return false;
                    }
                    else if (item is TextBox)
                    {
                        if (item.Text == "")
                            flag5 = true;
                        else
                            return false;
                    }
                }
            }

            return flag5;
        }

        private void comDelegate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (PSloaded)
                {
                    delegateID = Convert.ToInt16(comDelegate.SelectedValue.ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBillNum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (PSloaded)
                {
                    XtraTabPage xtraTabPage = getTabPage("tabPageProductsDetailsReport");
                    xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                    if (int.TryParse(txtBillNum.Text, out billNum))
                    { }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comCustomer_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (PSloaded)
                {
                    string query = "update dash set Customer_ID=" + comCustomer.SelectedValue + " , Customer_Name='" + comCustomer.Text + "' where dash.Branch_ID=" + DelegateBranchID + " and dash.Bill_Number=" + billNum;
                    MySqlCommand com = new MySqlCommand(query, conn);
                    conn.Close();
                    conn.Open();
                    com.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }
    }
}
