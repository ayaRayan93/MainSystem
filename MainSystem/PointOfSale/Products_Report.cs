using DevExpress.Utils;
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
    public partial class Products_Report : Form
    {
        MySqlConnection dbconnection;
        MySqlConnection dbconnection2;
        MySqlConnection dbconnection3;
        MySqlConnection dbconnection4;
        MySqlConnection dbconnection5;
        MySqlConnection dbconnection6;
        XtraTabControl MainTabControlPointSale;
        bool loaded = false;
        bool factoryFlage = false;
        bool groupFlage = false;
        bool flagProduct = false;
        bool AddedToBill = false;
        XtraTabPage xtraTabPage;

        //public static XtraTabPage MainTabPageUpdatePSDetails;
        //Panel panelUpdatePSDetails;

        public static GridControl gridcontrol;
        
        List<string> arr;
        bool billExist = false;
        bool mainBillExist = false;
        int ClintID = 0;
        MainForm main;
        DataRow loadedRow;
        string productType = "";
        DataRow row1;
        public static TipImage tipImage = null;
        int EmpBranchId = 0;
        int LoginDelegateID = -1;
        int DashBillNum = 0;
        int cartons = 0;

        string code1 = "0000";
        string code2 = "0000";
        string code3 = "0000";
        string code4 = "0000";
        string code5 = "0000";
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;
        bool flag4 = false;
        bool flag5 = false;

        public Products_Report(MainForm Min, DataRow Row1, string Type)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            dbconnection3 = new MySqlConnection(connection.connectionString);
            dbconnection4 = new MySqlConnection(connection.connectionString);
            dbconnection5 = new MySqlConnection(connection.connectionString);
            dbconnection6 = new MySqlConnection(connection.connectionString);
            //list_product = new List<Product>();
            MainTabControlPointSale = MainForm.tabControlPointSale;

            xtraTabPage = new XtraTabPage();

            gridcontrol = gridControl1;
            arr = new List<string>();

            txtClient.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtClient.AutoCompleteSource = AutoCompleteSource.CustomSource;

            comType.AutoCompleteMode = AutoCompleteMode.Suggest;
            comType.AutoCompleteSource = AutoCompleteSource.ListItems;
            comFactory.AutoCompleteMode = AutoCompleteMode.Suggest;
            comFactory.AutoCompleteSource = AutoCompleteSource.ListItems;
            comGroup.AutoCompleteMode = AutoCompleteMode.Suggest;
            comGroup.AutoCompleteSource = AutoCompleteSource.ListItems;
            comProduct.AutoCompleteMode = AutoCompleteMode.Suggest;
            comProduct.AutoCompleteSource = AutoCompleteSource.ListItems;

            comSet.AutoCompleteMode = AutoCompleteMode.Suggest;
            comSet.AutoCompleteSource = AutoCompleteSource.ListItems;
            comOffer.AutoCompleteMode = AutoCompleteMode.Suggest;
            comOffer.AutoCompleteSource = AutoCompleteSource.ListItems;

            main = Min;
            loadedRow = Row1;
            productType = Type;

            panel2.AutoScroll = false;
            panel2.VerticalScroll.Enabled = false;
            panel2.VerticalScroll.Visible = false;
            panel2.VerticalScroll.Maximum = 0;
            panel2.AutoScroll = true;
        }

        private void SearchProduct_Load(object sender, EventArgs e)
        {
            try
            {
                //LoginDelegateID = UserControl.LoginDelegate(dbconnection);
                EmpBranchId = UserControl.UserBranch(dbconnection);
                search();
                search2();
                foreach (GridColumn column in gridView1.Columns)
                    column.OptionsColumn.AllowSort = DefaultBoolean.False;
                //column.Settings.AutoFilterCondition = AutoFilterCondition.Equals;
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection6.Close();
        }
        
        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dbconnection.Open();
                    string query = "select Customer_Name from customer inner join customer_phone on customer_phone.Customer_ID=customer.Customer_ID where customer_phone.Phone='" + txtPhone.Text + "'";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        if (MessageBox.Show("هذا العميل موجود من قبل..هل انت متاكد انك تريد الاستمرار؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        {
                            txtClient.Text = "";
                            dbconnection.Close();
                            return;
                        }
                        txtClient.Text = com.ExecuteScalar().ToString();
                    }
                    else
                    {
                        txtClient.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        private void txtBillNum_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                MySqlDataReader dr2 = null;
                try
                {
                    int billnum = 0;
                    if (int.TryParse(txtBillNum.Text, out billnum))
                    {
                        string q= "select * from dash where dash.Branch_ID=" + EmpBranchId + " and dash.Bill_Number=" + billnum + " order by dash.Dash_ID desc limit 1";
                        dbconnection5.Open();
                        MySqlCommand cc = new MySqlCommand(q, dbconnection5);
                        MySqlDataReader dr4 = cc.ExecuteReader();
                        if (dr4.HasRows)
                        {
                            while (dr4.Read())
                            {
                                DashBillNum = Convert.ToInt16(dr4["Dash_ID"].ToString());
                                mainBillExist = true;
                                if (dr4["Customer_ID"].ToString() != "")
                                {
                                    dbconnection4.Open();
                                    string query = "select * from customer inner join customer_phone on customer_phone.Customer_ID=customer.Customer_ID where customer.Customer_ID=" + dr4["Customer_ID"].ToString();
                                    MySqlCommand cd = new MySqlCommand(query, dbconnection4);
                                    MySqlDataReader dr3 = cd.ExecuteReader();
                                    if (dr3.HasRows)
                                    {
                                        while (dr3.Read())
                                        {
                                            txtPhone.Enabled = false;
                                            txtClient.Enabled = false;
                                            //checkEdit1.Checked = false;
                                            //checkEdit1.Enabled = false;
                                            txtPhone.Text = dr3["Phone"].ToString();
                                            txtClient.Text = dr3["Customer_Name"].ToString();
                                            LoginDelegateID = Convert.ToInt16(dr4["Delegate_ID"].ToString());
                                            AddedToBill = true;
                                        }
                                        dr3.Close();
                                    }
                                }
                                else
                                {
                                    txtClient.Text = "";
                                    txtPhone.Text = "";
                                    txtClient.Enabled = true;
                                    txtPhone.Enabled = true;
                                    //checkEdit1.Enabled = true;
                                    //checkEdit1.Checked = false;
                                    AddedToBill = false;
                                }
                            }
                            //dr4.Close();

                            //inner join customer on customer.Customer_ID=dash.Customer_ID 
                            string qu = "select * from dash INNER JOIN dash_details ON dash_details.Dash_ID = dash.Dash_ID where dash.Branch_ID=" + EmpBranchId + " and dash.Bill_Number=" + billnum + " order by dash.Dash_ID desc limit 1";
                            dbconnection2.Open();
                            MySqlCommand com = new MySqlCommand(qu, dbconnection2);
                            dr2 = com.ExecuteReader();
                            if (dr2.HasRows)
                            {
                                billExist = true;
                                main.test(LoginDelegateID, billnum);
                            }
                            else
                            {
                                billExist = false;
                                main.test(LoginDelegateID, billnum);
                            }
                        }
                        else
                        {
                            txtClient.Text = "";
                            txtPhone.Text = "";
                            txtClient.Enabled = true;
                            txtPhone.Enabled = true;
                            //checkEdit1.Enabled = true;
                            //checkEdit1.Checked = false;
                            billExist = false;
                            mainBillExist = false;
                            AddedToBill = false;
                            LoginDelegateID = -1;
                            main.test(LoginDelegateID, 0);
                            MessageBox.Show("هذه الفاتورة غير موجودة");
                        }
                    }
                    else
                    {
                        MessageBox.Show("رقم الفاتورة يجب ان يكون عدد");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection4.Close();
                dbconnection2.Close();
                dbconnection5.Close();
            }
        }

        private void txtClient_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox t = sender as TextBox;
                if (t != null)
                {
                    //say you want to do a search when user types 3 or more chars
                    if (t.Text.Length >= 2)
                    {
                        //SuggestStrings will have the logic to return array of strings either from cache/db
                        dbconnection.Close();
                        dbconnection.Open();
                        string query = "select Customer_Name from customer where Customer_Name like '" + txtClient.Text + "%'";
                        MySqlCommand comand = new MySqlCommand(query, dbconnection);
                        MySqlDataReader dr = comand.ExecuteReader();
                        while (dr.Read())
                        {
                            arr.Add(dr["Customer_Name"].ToString());
                        }
                        dr.Close();
                        string[] strarr = new string[arr.Count];
                        arr.CopyTo(strarr);

                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        collection.AddRange(strarr);

                        txtClient.AutoCompleteCustomSource = collection;
                    }
                    xtraTabPage = getTabPage("tabPageProductsReport");
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtClient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dbconnection.Open();
                    string query = "select customer_phone.Phone from customer inner join customer_phone on customer_phone.Customer_ID=customer.Customer_ID where customer.Customer_Name='" + txtClient.Text + "' order by customer_phone.CustomerPhone_ID desc limit 1";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        txtPhone.Text = com.ExecuteScalar().ToString();
                    }
                    else
                    {
                        txtPhone.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }
        
        private void txtBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {

            TextBox txtBox = (TextBox)sender;
            
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtBox.Text != "")
                    {
                        switch (txtBox.Name)
                        {
                            case "txtSetID":
                                comSet.SelectedValue = txtSetID.Text;
                                break;
                            case "txtOfferID":
                                comOffer.SelectedValue = txtOfferID.Text;
                                break;
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                dbconnection.Close();
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string q1, q2, q3, q4, fQuery = "";
                if (comType.Text == "")
                {
                    q1 = "select Type_ID from type";
                }
                else
                {
                    q1 = comType.SelectedValue.ToString();
                }
                if (comFactory.Text == "")
                {
                    q2 = "select Factory_ID from factory";
                }
                else
                {
                    q2 = comFactory.SelectedValue.ToString();
                }
                if (comProduct.Text == "")
                {
                    q3 = "select Product_ID from product";
                }
                else
                {
                    q3 = comProduct.SelectedValue.ToString();
                }
                if (comGroup.Text == "")
                {
                    q4 = "select Group_ID from groupo";
                }
                else
                {
                    q4 = comGroup.SelectedValue.ToString();
                }

                if (comSize.Text != "")
                {
                    fQuery += " and size.Size_ID=" + comSize.SelectedValue.ToString();
                }

                if (comColor.Text != "")
                {
                    fQuery += " and color.Color_ID=" + comColor.SelectedValue.ToString();
                }
                if (comSort.Text != "")
                {
                    fQuery += " and Sort.Sort_ID=" + comSort.SelectedValue.ToString();
                }

                dbconnection.Open();
                dbconnection6.Open();
                //AND size.Factory_ID = factory.Factory_ID AND groupo.Factory_ID = factory.Factory_ID  AND product.Group_ID = groupo.Group_ID  AND factory.Type_ID = type.Type_ID AND color.Type_ID = type.Type_ID
                string query = "select data.Data_ID,data.Code as 'الكود',CONCAT('(بند) ',product.Product_Name) as 'الاسم',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Carton as 'الكرتنة',data.Description as 'الوصف',sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية',sellprice.Price_Type FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and data.Group_ID IN (" + q4 + ") and data.Data_ID=0 group by data.Data_ID";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;

                query = "SELECT data.Data_ID,data.Code as 'الكود',CONCAT('(بند) ',product.Product_Name) as 'الاسم',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Carton as 'الكرتنة',data.Description as 'الوصف',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Type_ID IN(" + q1 + ") and data.Factory_ID IN(" + q2 + ") and data.Product_ID IN (" + q3 + ") and data.Group_ID IN (" + q4 + ") " + fQuery + " group by data.Data_ID";
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    string q = "select sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sellprice.Price_Type from data INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID where data.Data_ID=" + dr["Data_ID"].ToString() + " order by sellprice.Date desc limit 1";
                    MySqlCommand comand2 = new MySqlCommand(q, dbconnection6);
                    MySqlDataReader dr2 = comand2.ExecuteReader();
                    while (dr2.Read())
                    {
                        gridView1.AddNewRow();
                        int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                        if (gridView1.IsNewItemRow(rowHandle))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["Data_ID"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[2], dr["الاسم"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[3], dr["النوع"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[4], dr["المصنع"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[5], dr["المجموعة"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اللون"], dr["اللون"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المقاس"], dr["المقاس"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الفرز"], dr["الفرز"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكرتنة"], dr["الكرتنة"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الوصف"], dr["الوصف"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"]);

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr2["السعر"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخصم"], dr2["الخصم"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], dr2["بعد الخصم"]);
                        }
                    }
                    dr2.Close();
                }
                dr.Close();
                if (gridView1.IsLastVisibleRow)
                {
                    gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection6.Close();
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            try
            {
                //"SELECT data.Data_ID,data.Code as 'الكود',product.Product_Name as 'المنتج',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as '',data.Carton as 'الكرتنة',data.Description as 'الوصف',price.Price as 'السعر',sum(storage.Total_Meters) as 'الكمية' FROM data INNER JOIN color ON color.Color_ID = data.Color_ID INNER JOIN size ON size.Size_ID = data.Size_ID INNER JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID AND size.Factory_ID = factory.Factory_ID AND groupo.Factory_ID = factory.Factory_ID INNER JOIN product ON product.Product_ID = data.Product_ID AND product.Group_ID = groupo.Group_ID INNER JOIN type ON type.Type_ID = data.Type_ID AND factory.Type_ID = type.Type_ID AND color.Type_ID = type.Type_ID INNER JOIN price ON price.Code = data.Code LEFT JOIN storage ON storage.Code = data.Code group by storage.Code", dbconnection);

                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT data.Data_ID,data.Code as 'الكود',CONCAT('(بند) ',product.Product_Name) as 'الاسم',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Carton as 'الكرتنة',data.Description as 'الوصف',sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Data_ID=0 group by data.Data_ID", dbconnection);
                DataTable dtf = new DataTable();
                adapter.Fill(dtf);
                gridControl1.DataSource = dtf;

                dbconnection.Open();
                dbconnection6.Open();
                #region بند
                string query = "SELECT data.Data_ID,data.Code as 'الكود',CONCAT('(بند) ',product.Product_Name) as 'الاسم',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Carton as 'الكرتنة',data.Description as 'الوصف',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID group by data.Data_ID";
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    string q = "select sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sellprice.Price_Type from data INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID where data.Data_ID=" + dr["Data_ID"].ToString() + " order by sellprice.Date desc limit 1";
                    MySqlCommand comand2 = new MySqlCommand(q, dbconnection6);
                    MySqlDataReader dr2 = comand2.ExecuteReader();
                    while (dr2.Read())
                    {
                        gridView1.AddNewRow();
                        int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                        if (gridView1.IsNewItemRow(rowHandle))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["Data_ID"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[2], dr["الاسم"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[3], dr["النوع"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[4], dr["المصنع"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[5], dr["المجموعة"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اللون"], dr["اللون"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المقاس"], dr["المقاس"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الفرز"], dr["الفرز"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكرتنة"], dr["الكرتنة"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الوصف"], dr["الوصف"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"]);

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr2["السعر"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخصم"], dr2["الخصم"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], dr2["بعد الخصم"]);
                        }
                    }
                    dr2.Close();
                }
                dr.Close();
                #endregion

                #region طقم
                query = "SELECT sets.Set_ID as 'الكود',CONCAT('(طقم) ',sets.Set_Name) as 'الاسم',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',sets.Description as 'الوصف' FROM sets INNER JOIN type ON type.Type_ID = sets.Type_ID INNER JOIN factory ON factory.Factory_ID = sets.Factory_ID INNER JOIN groupo ON groupo.Group_ID = sets.Group_ID";
                comand = new MySqlCommand(query, dbconnection);
                dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    dbconnection3.Open();
                    double price = 0;
                    double priceF = 0;
                    double totalQuanity = 0;
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[2], dr["الاسم"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[3], dr["النوع"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[4], dr["المصنع"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[5], dr["المجموعة"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الوصف"], dr["الوصف"]);
                        
                        query = "SELECT sum(set_details.Quantity*sellprice.Last_Price) as 'السعر',(sellprice.Sell_Discount) as 'الخصم',sum(set_details.Quantity*sellprice.Sell_Price) as 'بعد الخصم' FROM sets INNER JOIN set_details ON set_details.Set_ID = sets.Set_ID INNER JOIN sellprice ON set_details.Data_ID = sellprice.Data_ID where sets.Set_ID=" + dr["الكود"] + " group by sets.Set_ID order by sellprice.Date desc";
                        comand = new MySqlCommand(query, dbconnection3);
                        MySqlDataReader dr1 = comand.ExecuteReader();
                        while (dr1.Read())
                        {
                            price += Convert.ToDouble(dr1["السعر"].ToString());
                            priceF += Convert.ToDouble(dr1["بعد الخصم"].ToString());
                            
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخصم"], dr1["الخصم"]);
                        }
                        dr1.Close();

                        query = "SELECT sum(storage.Total_Meters) as 'الكمية' FROM sets LEFT JOIN storage ON storage.Set_ID = sets.Set_ID where sets.Set_ID=" + dr["الكود"] + " group by sets.Set_ID";
                        comand = new MySqlCommand(query, dbconnection3);
                        dr1 = comand.ExecuteReader();
                        while (dr1.Read())
                        {
                            if (dr1["الكمية"].ToString() != "")
                            {
                                totalQuanity += Convert.ToDouble(dr1["الكمية"].ToString());
                            }
                            else
                            {
                                totalQuanity += 0;
                            }
                        }
                        dr1.Close();

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], price);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], priceF);
                        if (totalQuanity != 0)
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], totalQuanity);
                        }
                        else
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], "");
                        }
                    }
                    dbconnection3.Close();
                }
                dr.Close();
                #endregion

                #region عرض
                query = "select offer.Offer_ID as 'الكود',CONCAT('(عرض) ',Offer_Name) as 'الاسم',Price as 'السعر',sum(storage.Total_Meters) as 'الكمية' from offer LEFT JOIN storage ON storage.Offer_ID = offer.Offer_ID group by storage.Offer_ID";
                comand = new MySqlCommand(query, dbconnection);
                dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[2], dr["الاسم"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr["السعر"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], dr["السعر"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"]);
                    }
                }
                dr.Close();
                #endregion

                dbconnection.Close();

                gridView1.Columns[1].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                gridView1.Columns[1].Width = 150;
                gridView1.Columns[0].Visible = false;
                for (int i = 2; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].Width = 100;
                }
                gridView1.Columns["الفرز"].Width = 50;
                gridView1.Columns["الكرتنة"].Width = 60;
                //gridView1.Columns["بعد الخصم"].Width = 130;
                //gridView1.Columns["الكمية"].Width = 120;

                if (gridView1.IsLastVisibleRow)
                {
                    gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection3.Close();
            dbconnection.Close();
            dbconnection6.Close();
        }

        private void txtCodeSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dbconnection.Open();
                    dbconnection6.Open();
                    string query = "select data.Data_ID,data.Code as 'الكود',CONCAT('(بند) ',product.Product_Name) as 'الاسم',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Carton as 'الكرتنة',data.Description as 'الوصف',sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Code='" + code1 + code2 + code3 + code4 + code5 + "' and data.Data_ID=0 group by data.Data_ID";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;

                    query = "SELECT data.Data_ID,data.Code as 'الكود',CONCAT('(بند) ',product.Product_Name) as 'الاسم',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Carton as 'الكرتنة',data.Description as 'الوصف',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Code='" + code1 + code2 + code3 + code4 + code5 + "' group by data.Data_ID";
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = comand.ExecuteReader();
                    while (dr.Read())
                    {
                        string q = "select sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sellprice.Price_Type from data INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID where data.Data_ID=" + dr["Data_ID"].ToString() + " order by sellprice.Date desc limit 1";
                        MySqlCommand comand2 = new MySqlCommand(q, dbconnection6);
                        MySqlDataReader dr2 = comand2.ExecuteReader();
                        while (dr2.Read())
                        {
                            gridView1.AddNewRow();
                            int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                            if (gridView1.IsNewItemRow(rowHandle))
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["Data_ID"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[2], dr["الاسم"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[3], dr["النوع"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[4], dr["المصنع"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[5], dr["المجموعة"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اللون"], dr["اللون"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المقاس"], dr["المقاس"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الفرز"], dr["الفرز"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكرتنة"], dr["الكرتنة"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الوصف"], dr["الوصف"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"]);

                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr2["السعر"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخصم"], dr2["الخصم"]);
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], dr2["بعد الخصم"]);
                            }
                        }
                        dr2.Close();
                    }
                    dr.Close();
                    if (gridView1.IsLastVisibleRow)
                    {
                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection6.Close();
        }

        private void comBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ComboBox comBox = (ComboBox)sender;

                switch (comBox.Name)
                {
                    case "comType":
                        if (loaded)
                        {
                            txtCodeSearch1.Text = comType.SelectedValue.ToString();
                            string query = "select * from factory inner join type_factory on factory.Factory_ID=type_factory.Factory_ID inner join type on type_factory.Type_ID=type.Type_ID where type_factory.Type_ID=" + comType.SelectedValue.ToString();
                            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            comFactory.DataSource = dt;
                            comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                            comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                            comFactory.Text = "";
                            if (comType.SelectedValue.ToString() == "1")
                            {
                                string query2 = "select * from groupo where Factory_ID=0 and Type_ID=" + comType.SelectedValue.ToString();
                                MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                comGroup.DataSource = dt2;
                                comGroup.DisplayMember = dt2.Columns["Group_Name"].ToString();
                                comGroup.ValueMember = dt2.Columns["Group_ID"].ToString();
                                comGroup.Text = "";
                            }
                            factoryFlage = true;

                            query = "select * from color where Type_ID=" + comType.SelectedValue.ToString();
                            da = new MySqlDataAdapter(query, dbconnection);
                            dt = new DataTable();
                            da.Fill(dt);
                            comColor.DataSource = dt;
                            comColor.DisplayMember = dt.Columns["Color_Name"].ToString();
                            comColor.ValueMember = dt.Columns["Color_ID"].ToString();
                            comColor.Text = "";
                            comFactory.Focus();
                        }
                        break;
                    case "comFactory":
                        if (factoryFlage)
                        {
                            txtCodeSearch2.Text = comFactory.SelectedValue.ToString();
                            if (comType.SelectedValue.ToString() != "1")
                            {
                                string query2f = "select * from groupo where Factory_ID=" + comFactory.SelectedValue.ToString();
                                MySqlDataAdapter da2f = new MySqlDataAdapter(query2f, dbconnection);
                                DataTable dt2f = new DataTable();
                                da2f.Fill(dt2f);
                                comGroup.DataSource = dt2f;
                                comGroup.DisplayMember = dt2f.Columns["Group_Name"].ToString();
                                comGroup.ValueMember = dt2f.Columns["Group_ID"].ToString();
                                comGroup.Text = "";
                            }

                            groupFlage = true;

                            string query2 = "select * from size where Factory_ID=" + comFactory.SelectedValue.ToString();
                            MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                            DataTable dt2 = new DataTable();
                            da2.Fill(dt2);
                            comSize.DataSource = dt2;
                            comSize.DisplayMember = dt2.Columns["Size_Value"].ToString();
                            comSize.ValueMember = dt2.Columns["Size_ID"].ToString();
                            comSize.Text = "";
                            comGroup.Focus();
                        }
                        break;
                    case "comGroup":
                        if (groupFlage)
                        {
                            txtCodeSearch3.Text = comGroup.SelectedValue.ToString();
                            string query3 = "select distinct  product.Product_ID  ,Product_Name  from product inner join product_factory_group on product.Product_ID=product_factory_group.Product_ID  where product.Type_ID=" + comType.SelectedValue.ToString() + " and product_factory_group.Factory_ID=" + comFactory.SelectedValue.ToString() + " and product_factory_group.Group_ID=" + comGroup.SelectedValue.ToString() + "  order by product.Product_ID";
                            MySqlDataAdapter da3 = new MySqlDataAdapter(query3, dbconnection);
                            DataTable dt3 = new DataTable();
                            da3.Fill(dt3);
                            comProduct.DataSource = dt3;
                            comProduct.DisplayMember = dt3.Columns["Product_Name"].ToString();
                            comProduct.ValueMember = dt3.Columns["Product_ID"].ToString();
                            comProduct.Text = "";


                            string query2 = "select * from size where Factory_ID=" + comFactory.SelectedValue.ToString() + " and Group_ID=" + comGroup.SelectedValue.ToString();
                            MySqlDataAdapter da2 = new MySqlDataAdapter(query2, dbconnection);
                            DataTable dt2 = new DataTable();
                            da2.Fill(dt2);
                            comSize.DataSource = dt2;
                            comSize.DisplayMember = dt2.Columns["Size_Value"].ToString();
                            comSize.ValueMember = dt2.Columns["Size_ID"].ToString();
                            comSize.Text = "";

                            comProduct.Focus();
                            flagProduct = true;
                        }
                        break;

                    case "comProduct":
                        if (flagProduct)
                        {
                            txtCodeSearch4.Text = comProduct.SelectedValue.ToString();
                            //flagProduct = false;
                            comColor.Focus();
                        }
                        break;

                    case "comColour":
                        comSize.Focus();
                        break;

                    case "comSize":
                        comSort.Focus();
                        break;

                    case "comSort":
                        {  }
                        break;
                }
            }
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            string query;
            MySqlCommand com;
            string Name;
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtBox.Text != "")
                    {
                        dbconnection.Open();
                        switch (txtBox.Name)
                        {
                            case "txtCodeSearch1":
                                query = "select Type_Name from type where Type_ID='" + txtCodeSearch1.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comType.Text = Name;
                                    txtCodeSearch2.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtCodeSearch2":
                                query = "select Factory_Name from factory where Factory_ID='" + txtCodeSearch2.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comFactory.Text = Name;
                                    txtCodeSearch3.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtCodeSearch3":
                                query = "select Group_Name from groupo where Group_ID='" + txtCodeSearch3.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comGroup.Text = Name;
                                    txtCodeSearch4.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtCodeSearch4":
                                query = "select Product_Name from product where Product_ID='" + txtCodeSearch4.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comProduct.Text = Name;
                                    txtCodeSearch5.Focus();

                                    query = "select data.Data_ID,data.Code as 'الكود',CONCAT('(بند) ',product.Product_Name) as 'الاسم',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Carton as 'الكرتنة',data.Description as 'الوصف',sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Code like '" + code1 + code2 + code3 + code4 + "%' and data.Data_ID=0 group by data.Data_ID";
                                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                                    DataTable dt = new DataTable();
                                    da.Fill(dt);
                                    gridControl1.DataSource = dt;

                                    dbconnection6.Open();
                                    query = "SELECT data.Data_ID,data.Code as 'الكود',CONCAT('(بند) ',product.Product_Name) as 'الاسم',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Carton as 'الكرتنة',data.Description as 'الوصف',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Code like '" + code1 + code2 + code3 + code4 + "%' group by data.Data_ID";
                                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                                    MySqlDataReader dr = comand.ExecuteReader();
                                    while (dr.Read())
                                    {
                                        string q = "select sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sellprice.Price_Type from data INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID where data.Data_ID=" + dr["Data_ID"].ToString() + " order by sellprice.Date desc limit 1";
                                        MySqlCommand comand2 = new MySqlCommand(q, dbconnection6);
                                        MySqlDataReader dr2 = comand2.ExecuteReader();
                                        while (dr2.Read())
                                        {
                                            gridView1.AddNewRow();
                                            int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                                            if (gridView1.IsNewItemRow(rowHandle))
                                            {
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["Data_ID"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[2], dr["الاسم"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[3], dr["النوع"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[4], dr["المصنع"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[5], dr["المجموعة"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اللون"], dr["اللون"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المقاس"], dr["المقاس"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الفرز"], dr["الفرز"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكرتنة"], dr["الكرتنة"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الوصف"], dr["الوصف"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"]);

                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr2["السعر"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخصم"], dr2["الخصم"]);
                                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], dr2["بعد الخصم"]);
                                            }
                                        }
                                        dr2.Close();
                                    }
                                    dr.Close();
                                    if (gridView1.IsLastVisibleRow)
                                    {
                                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                dbconnection.Close();
                dbconnection6.Close();
            }
        }

        private void comSet_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    comOffer.Text = "";
                    txtOfferID.Text = "";
                    txtSetID.Text = comSet.SelectedValue.ToString();
                    checkEditOffers.Checked = false;

                    checkEditSets.Checked = false;
                    dbconnection.Open();

                    string query = "SELECT sets.Set_ID as 'الكود',CONCAT('(طقم) ',sets.Set_Name) as 'الاسم',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',sets.Description as 'الوصف' FROM sets INNER JOIN type ON type.Type_ID = sets.Type_ID INNER JOIN factory ON factory.Factory_ID = sets.Factory_ID INNER JOIN groupo ON groupo.Group_ID = sets.Group_ID where sets.Set_ID=" + comSet.SelectedValue.ToString();
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = comand.ExecuteReader();
                    while (dr.Read())
                    {
                        dbconnection3.Open();
                        double price = 0;
                        double priceF = 0;
                        double totalQuanity = 0;
                        gridView1.AddNewRow();
                        int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                        if (gridView1.IsNewItemRow(rowHandle))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[2], dr["الاسم"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[3], dr["النوع"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[4], dr["المصنع"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[5], dr["المجموعة"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الوصف"], dr["الوصف"]);

                            query = "SELECT sum(set_details.Quantity*sellprice.Last_Price) as 'السعر',(set_details.Quantity*sellprice.Sell_Discount) as 'الخصم',sum(set_details.Quantity*sellprice.Sell_Price) as 'بعد الخصم' FROM sets INNER JOIN set_details ON set_details.Set_ID = sets.Set_ID INNER JOIN sellprice ON set_details.Data_ID = sellprice.Data_ID where sets.Set_ID=" + dr["الكود"] + " group by sets.Set_ID order by sellprice.Date desc";
                            comand = new MySqlCommand(query, dbconnection3);
                            MySqlDataReader dr1 = comand.ExecuteReader();
                            while (dr1.Read())
                            {
                                price += Convert.ToDouble(dr1["السعر"].ToString());
                                priceF += Convert.ToDouble(dr1["بعد الخصم"].ToString());
                                
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخصم"], dr1["الخصم"]);
                            }
                            dr1.Close();

                            query = "SELECT sum(storage.Total_Meters) as 'الكمية' FROM sets LEFT JOIN storage ON storage.Set_ID = sets.Set_ID where sets.Set_ID=" + dr["الكود"] + " group by sets.Set_ID";
                            comand = new MySqlCommand(query, dbconnection3);
                            dr1 = comand.ExecuteReader();
                            while (dr1.Read())
                            {
                                if (dr1["الكمية"].ToString() != "")
                                {
                                    totalQuanity += Convert.ToDouble(dr1["الكمية"].ToString());
                                }
                                else
                                {
                                    totalQuanity += 0;
                                }
                            }
                            dr1.Close();

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], price);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], priceF);
                            if (totalQuanity != 0)
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], totalQuanity);
                            }
                            else
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], "");
                            }
                        }
                        dbconnection3.Close();
                    }
                    dr.Close();
                    if (gridView1.IsLastVisibleRow)
                    {
                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
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

        private void comOffer_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    comSet.Text = "";
                    txtSetID.Text = "";
                    txtOfferID.Text = comOffer.SelectedValue.ToString();
                    checkEditSets.Checked = false;

                    checkEditOffers.Checked = false;
                    string query = "select offer.Offer_ID as 'الكود',CONCAT('(عرض) ',Offer_Name) as 'الاسم',Price as 'السعر',Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية' from offer LEFT JOIN storage ON storage.Offer_ID = offer.Offer_ID where offer.Offer_ID=" + comOffer.SelectedValue.ToString()+ "  group by storage.Offer_ID";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;

                    if (gridView1.IsLastVisibleRow)
                    {
                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtCodeSearch1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                if (txtCodeSearch1.Text != "")
                {
                    if (int.TryParse(txtCodeSearch1.Text, out result))
                    {
                        if (!flag1)
                        {
                            if (txtCodeSearch1.TextLength == 1)
                            {
                                code1 = "000" + txtCodeSearch1.Text;
                            }
                            else if (txtCodeSearch1.TextLength == 2)
                            {
                                code1 = "00" + txtCodeSearch1.Text;
                            }
                            else if (txtCodeSearch1.TextLength == 3)
                            {
                                code1 = "0" + txtCodeSearch1.Text;
                            }
                            else if (txtCodeSearch1.TextLength == 4)
                            {
                                code1 = txtCodeSearch1.Text;
                            }
                            else if (txtCodeSearch1.TextLength > 4)
                            {
                                txtCodeSearch1.Text = code1;
                            }
                            else if (txtCodeSearch1.TextLength == 0)
                            {
                                code1 = "0000";
                            }
                        }
                    }
                    else
                    {
                        flag1 = true;
                        if (txtCodeSearch1.TextLength == 1)
                        {
                            txtCodeSearch1.Text = "";
                        }
                        else if (txtCodeSearch1.TextLength > 1)
                        {
                            for (int i = 0; i < txtCodeSearch1.TextLength - 1; i++)
                            {
                                txtCodeSearch1.Text = txtCodeSearch1.Text[i].ToString();
                            }
                        }
                        MessageBox.Show("يمكنك ادخال ارقام فقط");
                        flag1 = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCodeSearch2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                if (txtCodeSearch2.Text != "")
                {
                    if (int.TryParse(txtCodeSearch2.Text, out result))
                    {
                        if (!flag2)
                        {
                            if (txtCodeSearch2.TextLength == 1)
                            {
                                code2 = "000" + txtCodeSearch2.Text;
                            }
                            else if (txtCodeSearch2.TextLength == 2)
                            {
                                code2 = "00" + txtCodeSearch2.Text;
                            }
                            else if (txtCodeSearch2.TextLength == 3)
                            {
                                code2 = "0" + txtCodeSearch2.Text;
                            }
                            else if (txtCodeSearch2.TextLength == 4)
                            {
                                code2 = txtCodeSearch2.Text;
                            }
                            else if (txtCodeSearch2.TextLength > 4)
                            {
                                txtCodeSearch2.Text = code2;
                            }
                            else if (txtCodeSearch2.TextLength == 0)
                            {
                                code2 = "0000";
                            }
                        }
                    }
                    else
                    {
                        flag2 = true;
                        if (txtCodeSearch2.TextLength == 1)
                        {
                            txtCodeSearch2.Text = "";
                        }
                        else if (txtCodeSearch2.TextLength > 1)
                        {
                            for (int i = 0; i < txtCodeSearch2.TextLength - 1; i++)
                            {
                                txtCodeSearch2.Text = txtCodeSearch2.Text[i].ToString();
                            }
                        }
                        MessageBox.Show("يمكنك ادخال ارقام فقط");
                        flag2 = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCodeSearch3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                if (txtCodeSearch3.Text != "")
                {
                    if (int.TryParse(txtCodeSearch3.Text, out result))
                    {
                        if (!flag3)
                        {
                            if (txtCodeSearch3.TextLength == 1)
                            {
                                code3 = "000" + txtCodeSearch3.Text;
                            }
                            else if (txtCodeSearch3.TextLength == 2)
                            {
                                code3 = "00" + txtCodeSearch3.Text;
                            }
                            else if (txtCodeSearch3.TextLength == 3)
                            {
                                code3 = "0" + txtCodeSearch3.Text;
                            }
                            else if (txtCodeSearch3.TextLength == 4)
                            {
                                code3 = txtCodeSearch3.Text;
                            }
                            else if (txtCodeSearch3.TextLength > 4)
                            {
                                txtCodeSearch3.Text = code3;
                            }
                            else if (txtCodeSearch3.TextLength == 0)
                            {
                                code3 = "0000";
                            }
                        }
                    }
                    else
                    {
                        flag3 = true;
                        if (txtCodeSearch3.TextLength == 1)
                        {
                            txtCodeSearch3.Text = "";
                        }
                        else if (txtCodeSearch3.TextLength > 1)
                        {
                            for (int i = 0; i < txtCodeSearch3.TextLength - 1; i++)
                            {
                                txtCodeSearch3.Text = txtCodeSearch3.Text[i].ToString();
                            }
                        }
                        MessageBox.Show("يمكنك ادخال ارقام فقط");
                        flag3 = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCodeSearch4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                if (txtCodeSearch4.Text != "")
                {
                    if (int.TryParse(txtCodeSearch4.Text, out result))
                    {
                        if (!flag4)
                        {
                            if (txtCodeSearch4.TextLength == 1)
                            {
                                code4 = "000" + txtCodeSearch4.Text;
                            }
                            else if (txtCodeSearch4.TextLength == 2)
                            {
                                code4 = "00" + txtCodeSearch4.Text;
                            }
                            else if (txtCodeSearch4.TextLength == 3)
                            {
                                code4 = "0" + txtCodeSearch4.Text;
                            }
                            else if (txtCodeSearch4.TextLength == 4)
                            {
                                code4 = txtCodeSearch4.Text;
                            }
                            else if (txtCodeSearch4.TextLength > 4)
                            {
                                txtCodeSearch4.Text = code4;
                            }
                            else if (txtCodeSearch4.TextLength == 0)
                            {
                                code4 = "0000";
                            }
                        }
                    }
                    else
                    {
                        flag4 = true;
                        if (txtCodeSearch4.TextLength == 1)
                        {
                            txtCodeSearch4.Text = "";
                        }
                        else if (txtCodeSearch4.TextLength > 1)
                        {
                            for (int i = 0; i < txtCodeSearch4.TextLength - 1; i++)
                            {
                                txtCodeSearch4.Text = txtCodeSearch4.Text[i].ToString();
                            }
                        }
                        MessageBox.Show("يمكنك ادخال ارقام فقط");
                        flag4 = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCodeSearch5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                if (txtCodeSearch5.Text != "")
                {
                    if (int.TryParse(txtCodeSearch5.Text, out result))
                    {
                        if (!flag5)
                        {
                            if (txtCodeSearch5.TextLength == 1)
                            {
                                code5 = "000" + txtCodeSearch5.Text;
                            }
                            else if (txtCodeSearch5.TextLength == 2)
                            {
                                code5 = "00" + txtCodeSearch5.Text;
                            }
                            else if (txtCodeSearch5.TextLength == 3)
                            {
                                code5 = "0" + txtCodeSearch5.Text;
                            }
                            else if (txtCodeSearch5.TextLength == 4)
                            {
                                code5 = txtCodeSearch5.Text;
                            }
                            else if (txtCodeSearch5.TextLength > 4)
                            {
                                txtCodeSearch5.Text = code5;
                            }
                            else if (txtCodeSearch5.TextLength == 0)
                            {
                                code5 = "0000";
                            }
                        }
                    }
                    else
                    {
                        flag5 = true;
                        if (txtCodeSearch5.TextLength == 1)
                        {
                            txtCodeSearch5.Text = "";
                        }
                        else if (txtCodeSearch5.TextLength > 1)
                        {
                            for (int i = 0; i < txtCodeSearch5.TextLength - 1; i++)
                            {
                                txtCodeSearch5.Text = txtCodeSearch5.Text[i].ToString();
                            }
                        }
                        MessageBox.Show("يمكنك ادخال ارقام فقط");
                        flag5 = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkEditSets_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkEditSets.Checked)
                {
                    comOffer.Text = "";
                    txtOfferID.Text = "";
                    checkEditOffers.Checked = false;

                    comSet.Text = "";
                    txtSetID.Text = "";
                    dbconnection.Open();

                    string query = "SELECT sets.Set_ID as 'الكود',CONCAT('(طقم) ',sets.Set_Name) as 'الاسم',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',sets.Description as 'الوصف' FROM sets INNER JOIN type ON type.Type_ID = sets.Type_ID INNER JOIN factory ON factory.Factory_ID = sets.Factory_ID INNER JOIN groupo ON groupo.Group_ID = sets.Group_ID";
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = comand.ExecuteReader();
                    while (dr.Read())
                    {
                        dbconnection3.Open();
                        double price = 0;
                        double priceF = 0;
                        double totalQuanity = 0;
                        gridView1.AddNewRow();
                        int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                        if (gridView1.IsNewItemRow(rowHandle))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[2], dr["الاسم"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[3], dr["النوع"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[4], dr["المصنع"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[5], dr["المجموعة"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الوصف"], dr["الوصف"]);

                            query = "SELECT sum(set_details.Quantity*sellprice.Last_Price) as 'السعر',(set_details.Quantity*sellprice.Sell_Discount) as 'الخصم',sum(set_details.Quantity*sellprice.Sell_Price) as 'بعد الخصم' FROM sets INNER JOIN set_details ON set_details.Set_ID = sets.Set_ID INNER JOIN sellprice ON set_details.Data_ID = sellprice.Data_ID where sets.Set_ID=" + dr["الكود"] + " group by sets.Set_ID order by sellprice.Date desc";
                            comand = new MySqlCommand(query, dbconnection3);
                            MySqlDataReader dr1 = comand.ExecuteReader();
                            while (dr1.Read())
                            {
                                price += Convert.ToDouble(dr1["السعر"].ToString());
                                priceF += Convert.ToDouble(dr1["بعد الخصم"].ToString());
                                
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخصم"], dr1["الخصم"]);
                            }
                            dr1.Close();

                            query = "SELECT sum(storage.Total_Meters) as 'الكمية' FROM sets LEFT JOIN storage ON storage.Set_ID = sets.Set_ID where sets.Set_ID=" + dr["الكود"] + " group by sets.Set_ID";
                            comand = new MySqlCommand(query, dbconnection3);
                            dr1 = comand.ExecuteReader();
                            while (dr1.Read())
                            {
                                if (dr1["الكمية"].ToString() != "")
                                {
                                    totalQuanity += Convert.ToDouble(dr1["الكمية"].ToString());
                                }
                                else
                                {
                                    totalQuanity += 0;
                                }
                            }
                            dr1.Close();

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], price);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], priceF);
                            if (totalQuanity != 0)
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], totalQuanity);
                            }
                            else
                            {
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], "");
                            }
                        }
                        dbconnection3.Close();
                    }
                    dr.Close();
                    if (gridView1.IsLastVisibleRow)
                    {
                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
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

        private void checkEditOffers_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkEditOffers.Checked)
                {
                    comSet.Text = "";
                    checkEditSets.Checked = false;

                    comOffer.Text = "";
                    txtOfferID.Text = "";
                    string query = "select offer.Offer_ID as 'الكود',CONCAT('(عرض) ',Offer_Name) as 'الاسم',Price as 'السعر',Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية' from offer LEFT JOIN storage ON storage.Offer_ID = offer.Offer_ID group by storage.Offer_ID";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;

                    if (gridView1.IsLastVisibleRow)
                    {
                        gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.RowHandle));
                //row1.Selected = true;
                if (loaded)
                {
                    if (e.Column.ToString() == "الكود")
                    {
                        if (tipImage == null)
                        {
                            if (row1["الكود"].ToString().Length >= 20)
                            {
                                tipImage = new TipImage(row1["Data_ID"].ToString(), "بند");
                            }
                            else if (row1["الاسم"].ToString().Split(')')[0].Split('(')[1] == "طقم")
                            {
                                tipImage = new TipImage(row1["الكود"].ToString(), "طقم");
                            }
                            else if (row1["الاسم"].ToString().Split(')')[0].Split('(')[1] == "عرض")
                            {
                                tipImage = new TipImage(row1["الكود"].ToString(), "عرض");
                            }
                            tipImage.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                            tipImage.Show();
                        }
                        else
                        {
                            tipImage.Close();
                            if (row1["الكود"].ToString().Length >= 20)
                            {
                                tipImage = new TipImage(row1["Data_ID"].ToString(), "بند");
                            }
                            else if (row1["الاسم"].ToString().Split(')')[0].Split('(')[1] == "طقم")
                            {
                                tipImage = new TipImage(row1["الكود"].ToString(), "طقم");
                            }
                            else if (row1["الاسم"].ToString().Split(')')[0].Split('(')[1] == "عرض")
                            {
                                tipImage = new TipImage(row1["الكود"].ToString(), "عرض");
                            }
                            tipImage.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                            tipImage.Show();
                        }
                    }

                    else if (e.Column.ToString() == "الكمية")
                    {
                        if (row1["الكود"].ToString().Length >= 20)
                        {
                            StoresDetails sd = new StoresDetails(row1["Data_ID"].ToString());
                            sd.ShowDialog();
                        }
                    }

                    else if (e.Column.ToString() == "الاسم")
                    {
                        if (row1["الكود"].ToString().Length >= 20)
                        {

                        }
                        else if (row1["الاسم"].ToString().Split(')')[0].Split('(')[1] == "طقم")
                        {
                            ItemDetails itemD = new ItemDetails(row1["الكود"].ToString(), "طقم");
                            itemD.ShowDialog();
                        }
                        else if (row1["الاسم"].ToString().Split(')')[0].Split('(')[1] == "عرض")
                        {
                            ItemDetails itemD = new ItemDetails(row1["الكود"].ToString(), "عرض");
                            itemD.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //DataRowView selRow = (DataRowView)(((GridView)gridControl1.MainView).GetRow(((GridView)gridControl1.MainView).GetSelectedRows()[0]));
                int billNum = 0;
                if (txtBillNum.Text == "")
                {
                    MessageBox.Show("يجب ادخال رقم الفاتورة");
                    return;
                }

                if (int.TryParse(txtBillNum.Text, out billNum))
                { }
                else
                {
                    MessageBox.Show("رقم الفاتورة يجب ان يكون عدد");
                    return;
                }
                
                XtraTabPage xtraTabPage = getTabPage("tabPageProductsDetailsReport");
                MainForm.tabPageProductsDetailsReport.Name = "tabPageProductsDetailsReport";
                MainForm.tabPageProductsDetailsReport.Text = "تفاصيل فاتورة";
                MainForm.panelProductsDetailsReport.Name = "panelProductsDetailsReport";
                MainForm.panelProductsDetailsReport.Dock = DockStyle.Fill;

                MainForm.ProductsDetailsReport = new ProductsDetails_Report(main, LoginDelegateID, billNum);
                MainForm.ProductsDetailsReport.Size = new Size(1109, 660);
                MainForm.ProductsDetailsReport.TopLevel = false;
                MainForm.ProductsDetailsReport.FormBorderStyle = FormBorderStyle.None;
                MainForm.ProductsDetailsReport.Dock = DockStyle.Fill;

                MainForm.panelProductsDetailsReport.Controls.Clear();
                MainForm.panelProductsDetailsReport.Controls.Add(MainForm.ProductsDetailsReport);
                MainForm.tabPageProductsDetailsReport.Controls.Add(MainForm.panelProductsDetailsReport);
                MainTabControlPointSale.TabPages.Add(MainForm.tabPageProductsDetailsReport);
                MainForm.ProductsDetailsReport.Show();
                MainTabControlPointSale.SelectedTabPage = MainForm.tabPageProductsDetailsReport;

                //XtraTabPage xtraTabPage = getTabPage("tabPageProductsDetailsReport");
                //if (xtraTabPage.ImageOptions.Image == null)
                //{
                //    //if (row1[0].ToString() != "")
                //    //{
                //    Main.tabPageProductsDetailsReport.Name = "tabPageProductsDetailsReport";
                //    Main.tabPageProductsDetailsReport.Text = "تعديل فاتورة";
                //    Main.tabPageProductsDetailsReport.ImageOptions.Image = null;
                //    Main.panelProductsDetailsReport.Name = "panelProductsDetailsReport";
                //    Main.panelProductsDetailsReport.Dock = DockStyle.Fill;

                //    Main.panelProductsDetailsReport.Controls.Clear();
                //    Main.ProductsDetailsReport = new ProductsDetails_Report(main, LoginDelegateID, billNum);
                //    Main.ProductsDetailsReport.Size = new Size(1059, 638);
                //    Main.ProductsDetailsReport.TopLevel = false;
                //    Main.ProductsDetailsReport.FormBorderStyle = FormBorderStyle.None;
                //    Main.ProductsDetailsReport.Dock = DockStyle.Fill;
                //    Main.panelProductsDetailsReport.Controls.Add(Main.ProductsDetailsReport);
                //    Main.tabPageProductsDetailsReport.Controls.Add(Main.panelProductsDetailsReport);
                //    MainTabControlPointSale.TabPages.Add(Main.tabPageProductsDetailsReport);
                //    Main.ProductsDetailsReport.Show();
                //    MainTabControlPointSale.SelectedTabPage = Main.tabPageProductsDetailsReport;
                //    //}
                //    //else
                //    //{
                //    //    MessageBox.Show("يجب ان تختار عنصر");
                //    //}
                //}
                //else
                //{
                //    MainTabControlPointSale.SelectedTabPage = Main.tabPageProductsDetailsReport;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (txtBillNum.Text != "")
                {
                    int billNo;
                    if (int.TryParse(txtBillNum.Text, out billNo))
                    {
                        if (mainBillExist)
                        {
                            if (gridView1.SelectedRowsCount > 0)
                            {
                                if (gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكمية").ToString() != "" && gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكمية").ToString() != "0")
                                {
                                    if (txtQuantity.Text != "" && comStore.Text != "")
                                    {
                                        double TotalMeters = 0;
                                        if (double.TryParse(txtQuantity.Text, out TotalMeters))
                                        {
                                            //TotalMeters <= Convert.ToDouble(gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكمية"))
                                            if (checkQuantityInStore(TotalMeters))
                                            {
                                                cartons = 0;
                                                if (gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكود").Length >= 20 && Convert.ToDouble(gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكرتنة")) > 0)
                                                {
                                                    if (cartonNumCheck())
                                                    { }
                                                    else
                                                    {
                                                        dbconnection.Close();
                                                        return;
                                                    }
                                                }
                                                string query = "";
                                                MySqlCommand com;

                                                if (!AddedToBill)
                                                {
                                                    if (txtClient.Text != "" && txtPhone.Text != "")
                                                    {
                                                        //if (checkEdit1.Checked == true)
                                                        //{
                                                            query = "select customer.Customer_ID from customer inner join customer_phone on customer_phone.Customer_ID=customer.Customer_ID where customer_phone.Phone='" + txtPhone.Text + "'";
                                                            com = new MySqlCommand(query, dbconnection);
                                                            if (com.ExecuteScalar() != null)
                                                            {
                                                                if (MessageBox.Show("هذا العميل موجود من قبل..هل انت متاكد انك تريد الاستمرار؟", "تنبية", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                                                                {
                                                                    dbconnection.Close();
                                                                    return;
                                                                }
                                                                ClintID = Convert.ToInt16(com.ExecuteScalar().ToString());
                                                            }
                                                            else
                                                            {
                                                                query = "insert into customer (Customer_Name,Customer_Start,Customer_Type) values(@Customer_Name,@Customer_Start,@Customer_Type)";
                                                                com = new MySqlCommand(query, dbconnection);
                                                                com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar, 255).Value = txtClient.Text;
                                                                //com.Parameters.Add("@Customer_Phone", MySqlDbType.VarChar, 255).Value = txtPhone.Text;
                                                                com.Parameters.Add("@Customer_Start", MySqlDbType.Date, 0).Value = DateTime.Now.Date;
                                                                com.Parameters.Add("@Customer_Type", MySqlDbType.VarChar, 255).Value = "عميل";
                                                                com.ExecuteNonQuery();

                                                                query = "select Customer_ID from customer order by Customer_ID desc limit 1";
                                                                com = new MySqlCommand(query, dbconnection);
                                                                ClintID = Convert.ToInt16(com.ExecuteScalar().ToString());

                                                                query = "insert into customer_phone (Customer_ID,Phone) values(@Customer_ID,@Phone)";
                                                                com = new MySqlCommand(query, dbconnection);
                                                                com.Parameters.Add("@Customer_ID", MySqlDbType.Int16, 11).Value = ClintID;
                                                                com.Parameters.Add("@Phone", MySqlDbType.VarChar, 255).Value = txtPhone.Text;
                                                                com.ExecuteNonQuery();

                                                                /*query = "select customer.Customer_ID from customer inner join customer_phone on customer_phone.Customer_ID=customer.Customer_ID where customer_phone.Phone='" + txtPhone.Text + "'";
                                                                com = new MySqlCommand(query, dbconnection);
                                                                ClintID = Convert.ToInt16(com.ExecuteScalar().ToString());*/
                                                            }
                                                        //}
                                                        //else
                                                        //{
                                                        //    query = "select customer.Customer_ID from customer inner join customer_phone on customer_phone.Customer_ID=customer.Customer_ID where customer_phone.Phone='" + txtPhone.Text + "'";
                                                        //    com = new MySqlCommand(query, dbconnection);
                                                        //    if (com.ExecuteScalar() != null)
                                                        //    {
                                                        //        ClintID = Convert.ToInt16(com.ExecuteScalar().ToString());
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        MessageBox.Show("هذا العميل غير موجود من قبل", "تنبية", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                        //        dbconnection.Close();
                                                        //        return;
                                                        //    }
                                                        //}

                                                        query = "update dash set Customer_ID=@Customer_ID,Customer_Name=@Customer_Name where Bill_Number=" + billNo + " and Branch_ID=" + EmpBranchId + " order by Dash_ID desc limit 1";
                                                        com = new MySqlCommand(query, dbconnection);
                                                        com.Parameters.Add("@Customer_ID", MySqlDbType.Int16).Value = ClintID;
                                                        com.Parameters.Add("@Customer_Name", MySqlDbType.VarChar).Value = txtClient.Text;
                                                        com.ExecuteNonQuery();
                                                        AddedToBill = true;
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("تاكد من تحديد العميل");
                                                        dbconnection.Close();
                                                        return;
                                                    }
                                                }

                                                #region new bill
                                                if (!billExist)
                                                {
                                                    billExist = true;

                                                    string q = "select Dash_ID from dash where Branch_ID=" + EmpBranchId + " and Bill_Number=" + txtBillNum.Text + " order by Dash_ID desc limit 1";
                                                    MySqlCommand command = new MySqlCommand(q, dbconnection);
                                                    int dashId = Convert.ToInt16(command.ExecuteScalar().ToString());


                                                    query = "insert into dash_details (Dash_ID,Type,Data_ID,Quantity,Store_ID,Store_Name,Cartons) values (@Dash_ID,@Type,@Data_ID,@Quantity,@Store_ID,@Store_Name,@Cartons)";
                                                    com = new MySqlCommand(query, dbconnection);
                                                    com.Parameters.Add("@Dash_ID", MySqlDbType.Int16).Value = dashId;
                                                    if (gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكود").Length >= 20)
                                                    {
                                                        com.Parameters.Add("@Type", MySqlDbType.VarChar).Value = "بند";
                                                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16).Value = gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "Data_ID");
                                                    }
                                                    else
                                                    {
                                                        com.Parameters.Add("@Type", MySqlDbType.VarChar).Value = gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الاسم").Split(')')[0].Split('(')[1].ToString();
                                                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16).Value = gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكود");
                                                    }
                                                    com.Parameters.Add("@Quantity", MySqlDbType.Decimal).Value = TotalMeters;
                                                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16).Value = comStore.SelectedValue.ToString();
                                                    com.Parameters.Add("@Store_Name", MySqlDbType.VarChar).Value = comStore.Text;
                                                    com.Parameters.Add("@Cartons", MySqlDbType.Int16).Value = cartons;
                                                    //com.Parameters.Add("@Error", MySqlDbType.Int16).Value = 0;
                                                    com.ExecuteNonQuery();
                                                    dbconnection.Close();
                                                    MessageBox.Show("تم");
                                                    txtQuantity.Text = "";
                                                    comStore.Text = "";
                                                    //txtBillNum.Enabled = false;
                                                    //txtPhone.Enabled = false;
                                                    //txtClient.Enabled = false;
                                                    //checkEdit1.Enabled = false;
                                                    //checkEdit1.Checked = false;
                                                    main.test(LoginDelegateID, billNo);
                                                }
                                                #endregion

                                                #region bill already exist
                                                else if (billExist)
                                                {
                                                    string qq = "SELECT dash_details.Data_ID FROM dash_details INNER JOIN dash ON dash.Dash_ID = dash_details.Dash_ID where dash.Bill_Number=" + billNo + " and dash.Branch_ID=" + EmpBranchId + " and dash_details.Data_ID=" + gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكود") + " and dash_details.Type='" + row1["الاسم"].ToString().Split(')')[0].Split('(')[1] + "' and dash_details.Store_ID=" + comStore.SelectedValue.ToString() + " order by dash.Dash_ID desc limit 1";
                                                    MySqlCommand comm = new MySqlCommand(qq, dbconnection);
                                                    if (comm.ExecuteScalar() != null)
                                                    {
                                                        MessageBox.Show("هذا البند تم اضافتة للسلة من قبل");
                                                        dbconnection.Close();
                                                        return;
                                                    }

                                                    string q = "select Dash_ID from dash where Bill_Number=" + billNo + " and Branch_ID=" + EmpBranchId + "  order by Dash_ID desc limit 1";
                                                    MySqlCommand command = new MySqlCommand(q, dbconnection);
                                                    int dashId = Convert.ToInt16(command.ExecuteScalar().ToString());

                                                    query = "insert into dash_details (Dash_ID,Type,Data_ID,Quantity,Store_ID,Store_Name,Cartons) values (@Dash_ID,@Type,@Data_ID,@Quantity,@Store_ID,@Store_Name,@Cartons)";
                                                    com = new MySqlCommand(query, dbconnection);
                                                    com.Parameters.Add("@Dash_ID", MySqlDbType.Int16).Value = dashId;
                                                    if (gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكود").Length >= 20)
                                                    {
                                                        com.Parameters.Add("@Type", MySqlDbType.VarChar).Value = "بند";
                                                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16).Value = gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "Data_ID");
                                                    }
                                                    else
                                                    {
                                                        com.Parameters.Add("@Type", MySqlDbType.VarChar).Value = gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الاسم").Split(')')[0].Split('(')[1].ToString();
                                                        com.Parameters.Add("@Data_ID", MySqlDbType.Int16).Value = gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكود");
                                                    }
                                                    com.Parameters.Add("@Quantity", MySqlDbType.Decimal).Value = TotalMeters;
                                                    com.Parameters.Add("@Store_ID", MySqlDbType.Int16).Value = comStore.SelectedValue.ToString();
                                                    com.Parameters.Add("@Store_Name", MySqlDbType.VarChar).Value = comStore.Text;
                                                    com.Parameters.Add("@Cartons", MySqlDbType.Int16).Value = cartons;
                                                    //com.Parameters.Add("@Error", MySqlDbType.Int16).Value = 0;
                                                    com.ExecuteNonQuery();
                                                    dbconnection.Close();
                                                    MessageBox.Show("تم");
                                                    txtQuantity.Text = "";
                                                    comStore.Text = "";
                                                    //txtBillNum.Enabled = false;
                                                    //txtPhone.Enabled = false;
                                                    //txtClient.Enabled = false;
                                                    //checkEdit1.Enabled = false;
                                                    //checkEdit1.Checked = false;
                                                    main.test(LoginDelegateID, billNo);
                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                MessageBox.Show("لا يوجد كمية كافية");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("الكمية يجب ان تكون عدد");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("برجاء ادخال الوصفات كاملة");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("لا يوجد اى كمية من هذا العنصر");
                                }
                            }
                            else
                            {
                                MessageBox.Show("برجاء اختيار عنصر للاضافة");
                            }
                        }
                        else
                        {
                            MessageBox.Show("هذه الفاتورة غير موجودة من قبل");
                        }
                    }
                    else
                    {
                        MessageBox.Show("رقم الفاتورة يجب ان يكون عدد");
                    }
                }
                else
                {
                    MessageBox.Show("برجاء ادخال الوصفات كاملة");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                xtraTabPage = getTabPage("tabPageProductsReport");
                if (!IsClear())
                {
                    xtraTabPage.ImageOptions.Image = Properties.Resources.unsave;
                }
                else
                {
                    xtraTabPage.ImageOptions.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions
        public void search()
        {
            dbconnection.Open();

            string query = "select * from type";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comType.DataSource = dt;
            comType.DisplayMember = dt.Columns["Type_Name"].ToString();
            comType.ValueMember = dt.Columns["Type_ID"].ToString();
            comType.Text = "";
            //txtType.Text = "";

            query = "select * from factory";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comFactory.DataSource = dt;
            comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
            comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
            comFactory.Text = "";
            //txtFactory.Text = "";

            query = "select * from groupo";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comGroup.DataSource = dt;
            comGroup.DisplayMember = dt.Columns["Group_Name"].ToString();
            comGroup.ValueMember = dt.Columns["Group_ID"].ToString();
            comGroup.Text = "";
            //txtGroup.Text = "";

            query = "select * from product";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comProduct.DataSource = dt;
            comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
            comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
            comProduct.Text = "";
            //txtProduct.Text = "";

            query = "select * from sets";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comSet.DataSource = dt;
            comSet.DisplayMember = dt.Columns["Set_Name"].ToString();
            comSet.ValueMember = dt.Columns["Set_ID"].ToString();
            comSet.Text = "";

            query = "select * from offer";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comOffer.DataSource = dt;
            comOffer.DisplayMember = dt.Columns["Offer_Name"].ToString();
            comOffer.ValueMember = dt.Columns["Offer_ID"].ToString();
            comOffer.Text = "";

            query = "select * from store";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comStore.DataSource = dt;
            comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
            comStore.ValueMember = dt.Columns["Store_ID"].ToString();
            comStore.Text = "";

            query = "select * from size";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comSize.DataSource = dt;
            comSize.DisplayMember = dt.Columns["Size_Value"].ToString();
            comSize.ValueMember = dt.Columns["Size_ID"].ToString();
            comSize.Text = "";

            query = "select * from color";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comColor.DataSource = dt;
            comColor.DisplayMember = dt.Columns["Color_Name"].ToString();
            comColor.ValueMember = dt.Columns["Color_ID"].ToString();
            comColor.Text = "";

            query = "select * from sort";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comSort.DataSource = dt;
            comSort.DisplayMember = dt.Columns["Sort_Value"].ToString();
            comSort.ValueMember = dt.Columns["Sort_ID"].ToString();
            comSort.Text = "";

            //search2();
            dbconnection.Close();
        }

        public void search2()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT data.Data_ID,data.Code as 'الكود',CONCAT('(بند) ',product.Product_Name) as 'الاسم',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Carton as 'الكرتنة',data.Description as 'الوصف',sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID  INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Data_ID=0 group by data.Data_ID", dbconnection);
            DataTable dtf = new DataTable();
            adapter.Fill(dtf);
            gridControl1.DataSource = dtf;

            dbconnection.Open();
            if (productType == "بند")
            {
                #region بند
                dbconnection6.Open();
                string query = "SELECT data.Data_ID,data.Code as 'الكود',CONCAT('(بند) ',product.Product_Name) as 'الاسم',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Carton as 'الكرتنة',data.Description as 'الوصف',sum(storage.Total_Meters) as 'الكمية' FROM data LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID INNER JOIN factory ON factory.Factory_ID = data.Factory_ID  INNER JOIN product ON product.Product_ID = data.Product_ID  INNER JOIN type ON type.Type_ID = data.Type_ID LEFT JOIN storage ON storage.Data_ID = data.Data_ID where data.Data_ID=" + loadedRow["Data_ID"].ToString() + " group by data.Data_ID";
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    string q = "select sellprice.Last_Price as 'السعر',sellprice.Sell_Discount as 'الخصم',sellprice.Sell_Price as 'بعد الخصم',sellprice.Price_Type from data INNER JOIN sellprice ON sellprice.Data_ID = data.Data_ID where data.Data_ID=" + dr["Data_ID"].ToString() + " order by sellprice.Date desc limit 1";
                    MySqlCommand comand2 = new MySqlCommand(q, dbconnection6);
                    MySqlDataReader dr2 = comand2.ExecuteReader();
                    while (dr2.Read())
                    {
                        gridView1.AddNewRow();
                        int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                        if (gridView1.IsNewItemRow(rowHandle))
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[0], dr["Data_ID"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[2], dr["الاسم"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[3], dr["النوع"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[4], dr["المصنع"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[5], dr["المجموعة"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["اللون"], dr["اللون"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["المقاس"], dr["المقاس"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الفرز"], dr["الفرز"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكرتنة"], dr["الكرتنة"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الوصف"], dr["الوصف"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"]);

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr2["السعر"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخصم"], dr2["الخصم"]);
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], dr2["بعد الخصم"]);
                        }
                    }
                    dr2.Close();
                }
                dr.Close();
                #endregion
            }
            else if (productType == "طقم")
            {
                #region طقم
                string query = "SELECT sets.Set_ID as 'الكود',CONCAT('(طقم) ',sets.Set_Name) as 'الاسم',type.Type_Name 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',sets.Description as 'الوصف' FROM sets INNER JOIN type ON type.Type_ID = sets.Type_ID INNER JOIN factory ON factory.Factory_ID = sets.Factory_ID INNER JOIN groupo ON groupo.Group_ID = sets.Group_ID where sets.Set_ID=" + loadedRow["الكود"].ToString();
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    dbconnection3.Open();
                    double price = 0;
                    double priceF = 0;
                    double totalQuanity = 0;
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[2], dr["الاسم"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[3], dr["النوع"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[4], dr["المصنع"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[5], dr["المجموعة"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الوصف"], dr["الوصف"]);

                        query = "SELECT sum(set_details.Quantity*sellprice.Last_Price) as 'السعر',(set_details.Quantity*sellprice.Sell_Discount) as 'الخصم',sum(set_details.Quantity*sellprice.Sell_Price) as 'بعد الخصم',sum(storage.Total_Meters)/COUNT(set_details.Data_ID) as 'الكمية' FROM sets INNER JOIN set_details ON set_details.Set_ID = sets.Set_ID INNER JOIN sellprice ON set_details.Data_ID = sellprice.Data_ID LEFT JOIN storage ON storage.Set_ID = sets.Set_ID where sets.Set_ID=" + dr["الكود"] + " group by set_details.Set_ID, storage.Set_ID order by sellprice.Date desc";
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
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الخصم"], dr1["الخصم"]);
                        }
                        dr1.Close();
                        
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], price);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], priceF);
                        if (totalQuanity != 0)
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], totalQuanity);
                        }
                        else
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], "");
                        }
                    }
                    dbconnection3.Close();
                }
                dr.Close();
                #endregion
            }
            else if (productType == "عرض")
            {
                #region عرض
                string query = "select offer.Offer_ID as 'الكود',CONCAT('(عرض) ',Offer_Name) as 'الاسم',Price as 'السعر',sum(storage.Total_Meters) as 'الكمية' from offer LEFT JOIN storage ON storage.Offer_ID = offer.Offer_ID where offer.Offer_ID=" + loadedRow["الكود"].ToString()+ " group by storage.Offer_ID";
                MySqlCommand comand = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = comand.ExecuteReader();
                while (dr.Read())
                {
                    gridView1.AddNewRow();
                    int rowHandle = gridView1.GetRowHandle(gridView1.DataRowCount);
                    if (gridView1.IsNewItemRow(rowHandle))
                    {
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[1], dr["الكود"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[2], dr["الاسم"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["السعر"], dr["السعر"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["بعد الخصم"], dr["السعر"]);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns["الكمية"], dr["الكمية"]);
                    }
                }
                dr.Close();
                #endregion
            }
            dbconnection.Close();
            gridView1.Columns[1].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridView1.Columns[1].Width = 150;
            gridView1.Columns[0].Visible = false;
            for (int i = 2; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = 100;
            }
            gridView1.Columns["الفرز"].Width = 50;
            gridView1.Columns["الكرتنة"].Width = 60;
            //gridView1.Columns["بعد الخصم"].Width = 130;
            //gridView1.Columns["الكمية"].Width = 120;

            if (gridView1.IsLastVisibleRow)
            {
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }
        }

        public bool checkQuantityInStore(double totalMeter)
        {
            if (row1["الاسم"].ToString().Split(')')[0].Split('(')[1] == "بند")
            {
                string query = "SELECT sum(storage.Total_Meters) as 'الكمية' FROM storage where storage.Store_ID=" + comStore.SelectedValue.ToString() + " and storage.Data_ID=" + gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "Data_ID") + " group by storage.Data_ID,storage.Store_ID";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                if (com.ExecuteScalar() != null)
                {
                    double totalquant = Convert.ToDouble(com.ExecuteScalar().ToString());
                    if (totalMeter <= totalquant)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (row1["الاسم"].ToString().Split(')')[0].Split('(')[1] == "طقم")
            {
                string query = "SELECT sum(storage.Total_Meters) as 'الكمية' FROM storage where storage.Store_ID=" + comStore.SelectedValue.ToString() + " and storage.Set_ID=" + gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكود") + " group by storage.Set_ID,storage.Store_ID";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                if (com.ExecuteScalar() != null)
                {
                    double totalquant = Convert.ToDouble(com.ExecuteScalar().ToString());
                    if (totalMeter <= totalquant)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (row1["الاسم"].ToString().Split(')')[0].Split('(')[1] == "عرض")
            {
                double totalquant = Convert.ToDouble(gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكمية"));
                if (totalMeter <= totalquant)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool cartonNumCheck()
        {
            try
            {
                string query = "select Carton from data where Code='" + gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكود") + "'";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                if (com.ExecuteScalar() != null)
                {
                    double Carton = Convert.ToDouble(com.ExecuteScalar());
                    double totalMeters = double.Parse(txtQuantity.Text);
                    if (totalMeters % Carton == 0)
                    {
                        MessageBox.Show("تحتاج " + totalMeters / Carton + " كرتونة");
                        cartons = (int)(totalMeters / Carton);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("تحتاج " + totalMeters / Carton + " كرتونة و " + totalMeters % Carton + " متر");
                        cartons = 0;
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
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

        //clear function
        public void clear()
        {
            foreach (Control co in this.panel1.Controls)
            {
                //if (co is GroupBox)
                //{
                foreach (Control item in co.Controls)
                {
                    if (item is ComboBox)
                    {
                        item.Text = "";
                    }
                    else if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
                //}
            }
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

        private void bunifuTileButtonAddSpecialOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (DashBillNum != 0 && txtClient.Text != "" && txtPhone.Text != "")
                {
                    int clientIdSO = 0;
                    int DelegateId = UserControl.LoginDelegate(dbconnection);

                    dbconnection.Open();
                    string query = "select customer.Customer_ID from customer inner join customer_phone on customer_phone.Customer_ID=customer.Customer_ID where customer_phone.Phone='" + txtPhone.Text + "'";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        clientIdSO = Convert.ToInt16(com.ExecuteScalar().ToString());
                    }
                    dbconnection.Close();

                    AddSpecialOrderScanner soForm = new AddSpecialOrderScanner(DashBillNum, EmpBranchId, DelegateId, clientIdSO);
                    soForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("تاكد من رقم الفاتورة ومن تحديد العميل");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnNewChosen_Click(object sender, EventArgs e)
        {
            clearCom();
        }

        //clear function
        public void clearCom()
        {
            foreach (Control co in this.panel3.Controls)
            {
                if (co is System.Windows.Forms.ComboBox)
                {
                    co.Text = "";
                }
                else if (co is TextBox)
                {
                    co.Text = "";
                }
            }
        }
    }
}
