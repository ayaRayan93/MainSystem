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
using MySql.Data.MySqlClient;
using DevExpress.XtraTab;

namespace MainSystem
{
    public partial class Information_Sets : DevExpress.XtraEditors.XtraForm
    {
        MySqlConnection dbconnection;
        MySqlConnection dbconnection3;
        DataRow row1;
        bool loaded = false;

        //public static Products_Report ProductsReport;

        //XtraTabPage tabPageProductsReport;
        //Panel panelProductsReport;

        MainForm main;

        public Information_Sets(MainForm Min)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection3 = new MySqlConnection(connection.connectionString);
            //tabPageProductsReport = new XtraTabPage();
            //panelProductsReport = new Panel();

            main = Min;
        }

        private void Information_Products_Load(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT sets.Set_ID as 'الكود',sets.Set_Name as 'الاسم', 'السعر', 'الخصم', 'بعد الخصم', 'الكمية',sets.Description as 'الوصف',set_photo.Photo as 'الصورة' FROM sets LEFT JOIN set_photo ON set_photo.Set_ID = sets.Set_ID where sets.Set_ID=0", dbconnection);
                DataTable dtf = new DataTable();
                adapter.Fill(dtf);
                gridControl1.DataSource = dtf;
                layoutView1.Columns[0].Visible = false;

                string query = "SELECT sets.Set_ID as 'الكود',sets.Set_Name as 'الاسم',sets.Description as 'الوصف',set_photo.Photo as 'الصورة' FROM sets LEFT JOIN set_photo ON set_photo.Set_ID = sets.Set_ID";
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    dbconnection3.Open();
                    double price = 0;
                    double priceF = 0;
                    double totalQuanity = 0;
                    layoutView1.AddNewRow();
                    int rowHandle = layoutView1.GetRowHandle(layoutView1.DataRowCount);
                    if (layoutView1.IsNewItemRow(rowHandle))
                    {
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الكود"], dr["الكود"]);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الاسم"], dr["الاسم"]);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الوصف"], dr["الوصف"]);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الصورة"], dr["الصورة"]);

                        query = "SELECT sum(set_details.Quantity*sellprice.Last_Price) as 'السعر',sum(set_details.Quantity*sellprice.Sell_Discount) as 'الخصم',sum(set_details.Quantity*sellprice.Sell_Price) as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية' FROM sets INNER JOIN set_details ON set_details.Set_ID = sets.Set_ID INNER JOIN sellprice ON set_details.Data_ID = sellprice.Data_ID LEFT JOIN storage ON storage.Set_ID = sets.Set_ID where sets.Set_ID=" + dr["الكود"] + " group by set_details.Set_ID, sets.Set_ID order by sellprice.Date desc";
                        comand = new MySqlCommand(query, dbconnection3);
                        MySqlDataReader dr1 = comand.ExecuteReader();
                        while (dr1.Read())
                        {
                            price += Convert.ToDouble(dr1["السعر"].ToString());
                            priceF += Convert.ToDouble(dr1["بعد الخصم"].ToString());
                            totalQuanity += Convert.ToDouble(dr1["الكمية"].ToString());
                            layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الخصم"], dr1["الخصم"]);
                        }
                        dr1.Close();
                        
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["السعر"], price);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["بعد الخصم"], priceF);
                        layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الكمية"], totalQuanity);
                    }
                    dbconnection3.Close();
                }
                dr.Close();

                if (layoutView1.IsLastVisibleRow)
                {
                    layoutView1.FocusedRowHandle = layoutView1.RowCount - 1;
                }

                search();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comSet_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    checkEditSets.Checked = false;
                    dbconnection.Open();

                    MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT sets.Set_ID as 'الكود',sets.Set_Name as 'الاسم', 'السعر', 'الخصم', 'بعد الخصم', 'الكمية',sets.Description as 'الوصف',set_photo.Photo as 'الصورة' FROM sets LEFT JOIN set_photo ON set_photo.Set_ID = sets.Set_ID where sets.Set_ID=0", dbconnection);
                    DataTable dtf = new DataTable();
                    adapter.Fill(dtf);
                    gridControl1.DataSource = dtf;
                    layoutView1.Columns[0].Visible = false;

                    string query = "SELECT sets.Set_ID as 'الكود',sets.Set_Name as 'الاسم',sets.Description as 'الوصف',set_photo.Photo as 'الصورة' FROM sets LEFT JOIN set_photo ON set_photo.Set_ID = sets.Set_ID where sets.Set_ID=" + comSet.SelectedValue.ToString();
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = comand.ExecuteReader();
                    while (dr.Read())
                    {
                        dbconnection3.Open();
                        double price = 0;
                        double priceF = 0;
                        double totalQuanity = 0;
                        layoutView1.AddNewRow();
                        int rowHandle = layoutView1.GetRowHandle(layoutView1.DataRowCount);
                        if (layoutView1.IsNewItemRow(rowHandle))
                        {
                            layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الكود"], dr["الكود"]);
                            layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الاسم"], dr["الاسم"]);
                            layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الوصف"], dr["الوصف"]);
                            layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الصورة"], dr["الصورة"]);

                            query = "SELECT sum(set_details.Quantity*sellprice.Last_Price) as 'السعر',(set_details.Quantity*sellprice.Sell_Discount) as 'الخصم',sum(set_details.Quantity*sellprice.Sell_Price) as 'بعد الخصم',sum(storage.Total_Meters)/COUNT(set_details.Data_ID) as 'الكمية' FROM sets INNER JOIN set_details ON set_details.Set_ID = sets.Set_ID INNER JOIN sellprice ON set_details.Data_ID = sellprice.Data_ID LEFT JOIN storage ON storage.Set_ID = sets.Set_ID where sets.Set_ID=" + dr["الكود"] + " group by set_details.Set_ID, sets.Set_ID order by sellprice.Date desc";
                            comand = new MySqlCommand(query, dbconnection3);
                            MySqlDataReader dr1 = comand.ExecuteReader();
                            while (dr1.Read())
                            {
                                price += Convert.ToDouble(dr1["السعر"].ToString());
                                priceF += Convert.ToDouble(dr1["بعد الخصم"].ToString());
                                if (dr1["الكمية"].ToString() != "")
                                {
                                    totalQuanity += Convert.ToDouble(dr1["الكمية"].ToString());
                                }
                                else
                                {
                                    totalQuanity += 0;
                                }
                                layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الخصم"], dr1["الخصم"]);
                            }
                            dr1.Close();
                            
                            layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["السعر"], price);
                            layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["بعد الخصم"], priceF);
                            layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الكمية"], totalQuanity);
                        }
                        dbconnection3.Close();
                    }
                    dr.Close();
                    if (layoutView1.IsLastVisibleRow)
                    {
                        layoutView1.FocusedRowHandle = layoutView1.RowCount - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection3.Close();
            dbconnection.Close();
        }

        private void layoutView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                row1 = layoutView1.GetDataRow(layoutView1.GetRowHandle(layoutView1.FocusedRowHandle));
                XtraTabPage xtraTabPage = getTabPage("tabPageProductsReport");
                if (xtraTabPage == null)
                {
                    MainForm.tabPageProductsReport.Name = "tabPageProductsReport";
                    MainForm.tabPageProductsReport.Text = "عرض البنود";
                    MainForm.panelProductsReport.Name = "panelProductsReport";
                    MainForm.panelProductsReport.Dock = DockStyle.Fill;

                    MainForm.ProductsReport = new Products_Report(main, row1, "طقم");
                    MainForm.ProductsReport.Size = new Size(1109, 660);
                    MainForm.ProductsReport.TopLevel = false;
                    MainForm.ProductsReport.FormBorderStyle = FormBorderStyle.None;
                    MainForm.ProductsReport.Dock = DockStyle.Fill;

                    MainForm.panelProductsReport.Controls.Clear();
                    MainForm.panelProductsReport.Controls.Add(MainForm.ProductsReport);
                    MainForm.tabPageProductsReport.Controls.Add(MainForm.panelProductsReport);
                    MainForm.tabControlPointSale.TabPages.Add(MainForm.tabPageProductsReport);
                    MainForm.ProductsReport.Show();
                    MainForm.tabControlPointSale.SelectedTabPage = MainForm.tabPageProductsReport;
                }
                else
                {
                    MainForm.ProductsReport = new Products_Report(main, row1, "طقم");
                    MainForm.ProductsReport.Size = new Size(1109, 660);
                    MainForm.ProductsReport.TopLevel = false;
                    MainForm.ProductsReport.FormBorderStyle = FormBorderStyle.None;
                    MainForm.ProductsReport.Dock = DockStyle.Fill;

                    MainForm.panelProductsReport.Controls.Clear();
                    MainForm.panelProductsReport.Controls.Add(MainForm.ProductsReport);
                    MainForm.ProductsReport.Show();
                    MainForm.tabControlPointSale.SelectedTabPage = MainForm.tabPageProductsReport;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void checkEditSets_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkEditSets.Checked)
                {
                    comSet.Text = "";
                    dbconnection.Open();

                    MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT sets.Set_ID as 'الكود',sets.Set_Name as 'الاسم', 'السعر', 'الخصم', 'بعد الخصم', 'الكمية',sets.Description as 'الوصف',set_photo.Photo as 'الصورة' FROM sets LEFT JOIN set_photo ON set_photo.Set_ID = sets.Set_ID where sets.Set_ID=0", dbconnection);
                    DataTable dtf = new DataTable();
                    adapter.Fill(dtf);
                    gridControl1.DataSource = dtf;
                    layoutView1.Columns[0].Visible = false;

                    string query = "SELECT sets.Set_ID as 'الكود',sets.Set_Name as 'الاسم',sets.Description as 'الوصف',set_photo.Photo as 'الصورة' FROM sets LEFT JOIN set_photo ON set_photo.Set_ID = sets.Set_ID";
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = comand.ExecuteReader();
                    while (dr.Read())
                    {
                        dbconnection3.Open();
                        double price = 0;
                        double priceF = 0;
                        double totalQuanity = 0;
                        layoutView1.AddNewRow();
                        int rowHandle = layoutView1.GetRowHandle(layoutView1.DataRowCount);
                        if (layoutView1.IsNewItemRow(rowHandle))
                        {
                            layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الكود"], dr["الكود"]);
                            layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الاسم"], dr["الاسم"]);
                            layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الوصف"], dr["الوصف"]);
                            layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الصورة"], dr["الصورة"]);

                            query = "SELECT sum(set_details.Quantity*sellprice.Last_Price) as 'السعر',(set_details.Quantity*sellprice.Sell_Discount) as 'الخصم',sum(set_details.Quantity*sellprice.Sell_Price) as 'بعد الخصم',sum(storage.Total_Meters)/COUNT(set_details.Data_ID) as 'الكمية' FROM sets INNER JOIN set_details ON set_details.Set_ID = sets.Set_ID INNER JOIN sellprice ON set_details.Data_ID = sellprice.Data_ID LEFT JOIN storage ON storage.Set_ID = sets.Set_ID where sets.Set_ID=" + dr["الكود"] + " group by set_details.Set_ID, sets.Set_ID order by sellprice.Date desc";
                            comand = new MySqlCommand(query, dbconnection3);
                            MySqlDataReader dr1 = comand.ExecuteReader();
                            while (dr1.Read())
                            {
                                price += Convert.ToDouble(dr1["السعر"].ToString());
                                priceF += Convert.ToDouble(dr1["بعد الخصم"].ToString());
                                if (dr1["الكمية"].ToString() != "")
                                {
                                    totalQuanity += Convert.ToDouble(dr1["الكمية"].ToString());
                                }
                                else
                                {
                                    totalQuanity += 0;
                                }
                                layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الخصم"], dr1["الخصم"]);
                            }
                            dr1.Close();
                            
                            layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["السعر"], price);
                            layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["بعد الخصم"], priceF);
                            layoutView1.SetRowCellValue(rowHandle, layoutView1.Columns["الكمية"], totalQuanity);
                        }
                        dbconnection3.Close();
                    }
                    dr.Close();
                    if (layoutView1.IsLastVisibleRow)
                    {
                        layoutView1.FocusedRowHandle = layoutView1.RowCount - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection3.Close();
            dbconnection.Close();
        }

        //functions
        public void search()
        {
            string query = "select * from sets";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comSet.DataSource = dt;
            comSet.DisplayMember = dt.Columns["Set_Name"].ToString();
            comSet.ValueMember = dt.Columns["Set_ID"].ToString();
            comSet.Text = "";
            loaded = true;
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < MainForm.tabControlPointSale.TabPages.Count; i++)
                if (MainForm.tabControlPointSale.TabPages[i].Name == text)
                {
                    return MainForm.tabControlPointSale.TabPages[i];
                }
            return null;
        }
    }
}