using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class BillDetails_Confirm : Form
    {
        MySqlConnection dbconnection, connectionReader, connectionReader1, connectionReader2;
        
        //bool flag = false;
        DataRow row1;
        int[] addedRecordIDs;
        int recordCount = 0;
        int[] addedPackages;
        int PackageCount = 0;
        int[] addedSets;
        int SetCount = 0;
        int OfferID = -1;
        double quantityBU = -1;
        double totalCostForPackages = 0;
        //double totalCostForSets = 0;
        bool Added = false;
        string code="";
        int Dash_Bill_ID=0;
        string delegate_Name="";
        string customer_Name="";
        string customer_ID="";
        string engName = "";
        string engID="";
        string type="";
        string value="";
        string delegate_ID="";
        DateTime date;
        string Branch_ID="";
        string Branch_Name="";
        string RecivedType="";
        //bool loaded = false;

        public BillDetails_Confirm(string RecivedType, string BranchID, string BranchName, DataTable dt, string billNo, string Customer_Name, string Customer_ID, string Eng, string Type, string Delegate_Name, string Delegate_ID, string EngID, string Value, DateTime Date)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            connectionReader  = new MySqlConnection(connection.connectionString);
            connectionReader1 = new MySqlConnection(connection.connectionString);
            connectionReader2 = new MySqlConnection(connection.connectionString);
            addedRecordIDs = new int[50];
            addedPackages = new int[50];
            addedSets = new int[50];
            Branch_ID = BranchID;
            Branch_Name = BranchName;
            txtCustomerName.Text = Customer_Name;
            txtType.Text = Type;
            txtBillNo.Text = billNo;
            delegate_Name = Delegate_Name;
            customer_ID = Customer_ID;
            engName = Eng;
            customer_Name = Customer_Name;
            engID = EngID;
            type = Type;
            value = Value;
            delegate_ID = Delegate_ID;
            this.RecivedType = RecivedType;
            date =  new DateTime(Date.Year, Date.Month, Date.Day);

            dt.Columns["colالتسلسل"].ColumnName = "التسلسل";
            dt.Columns["colالكود"].ColumnName = "الكود";
            dt.Columns["colالاسم"].ColumnName = "الاسم";
            dt.Columns["colاللون"].ColumnName = "اللون";
            dt.Columns["colالمقاس"].ColumnName = "المقاس";
            dt.Columns["colالفرز"].ColumnName = "الفرز";
            dt.Columns["colالكمية"].ColumnName = "الكمية";
            dt.Columns["colالسعر"].ColumnName = "السعر";
            dt.Columns["colالاجمالى"].ColumnName = "الاجمالى";
            dt.Columns["colالخصم"].ColumnName = "الخصم";
            dt.Columns["colالكرتنة"].ColumnName = "الكرتنة";

            gridControl1.DataSource = dt;
            
            //dataGridView1.Columns["Package_ID"].Visible = false;
            //dataGridView1.Columns["DashID"].Visible = false;
            //dbconnection.Open();

            //string query = "select sum((Sell_Price*Quantity))from dash where Bill_Number=" + txtBillNo.Text + " and Delegate_Name='" + Delegate_Name + "'";

            //MySqlCommand com = new MySqlCommand(query, dbconnection);
            //if (com.ExecuteScalar() != null)
            //{
            //    labTotalBillPriceAD.Text = com.ExecuteScalar().ToString();
            //}
            //dbconnection.Close();
        }

        private void txtTotalMeters_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTotalMeters.Text != "" && row1 != null)
                {
                    double total = 0;
                    if (double.TryParse(txtTotalMeters.Text, out total))
                    {
                        double newPrice = double.Parse(labSellPrice.Text) / Convert.ToDouble(row1["الكمية"].ToString());
                        labPriceAD.Text = (newPrice * total).ToString();
                    }
                    else
                    {
                        MessageBox.Show("enter number please");
                        txtTotalMeters.Text = "";
                    }
                }
                else
                {
                    txtTotalMeters.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddToBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (cartonNoCheck())
                {
                    if (!Added && row1 != null)
                    {
                        if (row1["التسلسل"].ToString() != "")
                        {
                            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                            {
                                int[] ros = gridView1.GetSelectedRows();
                                DataRow row2 = gridView1.GetDataRow(ros[i]);
                                if (row2["الكود"].ToString() != "")
                                {
                                    //row.Selected = false;
                                }
                            }
                            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                            {
                                int[] ros = gridView1.GetSelectedRows();
                                DataRow row2 = gridView1.GetDataRow(ros[i]);

                                if (row2["التسلسل"].ToString() != "" && row2["الاسم"].ToString().Split(')')[0].Split('(')[1] == "عرض")
                                {
                                    #region عرض
                                    if (Convert.ToInt16(row2["التسلسل"].ToString()) == OfferID)
                                    {
                                        addedPackages[PackageCount] = OfferID;
                                        PackageCount++;
                                        //row2.DefaultCellStyle.BackColor = Color.Silver;
                                        totalCostForPackages += Convert.ToDouble(labPriceAD.Text);
                                        gridView2.AddNewRow();
                                        if (gridView2.IsLastVisibleRow)
                                        {
                                            gridView2.FocusedRowHandle = gridView2.RowCount - 1;
                                        }
                                        int n = gridView2.RowCount - 1;
                                        foreach (GridColumn item in gridView2.Columns)
                                        {
                                            if (item.Name.Split('l')[1].ToString() == "الكمية")
                                            {
                                                if (quantityBU != -1)
                                                {
                                                    gridView2.SetRowCellValue(n, "الكمية", (Convert.ToDouble(gridView1.GetRowCellValue(ros[i], "الكمية")) / quantityBU * Convert.ToDouble(txtTotalMeters.Text)).ToString());
                                                }
                                            }
                                            else
                                            {
                                                //gridView2.SetRowCellValue(n, "التسلسل", "3");
                                                gridView2.SetRowCellValue(n, item.Name.Split('l')[1].ToString(), gridView1.GetRowCellValue(ros[i], item.Name.Split('l')[1].ToString()).ToString());
                                            }
                                        }

                                    }
                                    #endregion
                                }
                                else if (row2["التسلسل"].ToString() != "" && row2["الاسم"].ToString().Split(')')[0].Split('(')[1] == "طقم")
                                {

                                    #region طقم
                                    if (Convert.ToInt16(row2["التسلسل"].ToString()) == OfferID)
                                    {
                                        addedSets[SetCount] = OfferID;
                                        SetCount++;
                                        //row.DefaultCellStyle.BackColor = Color.Silver;
                                        //// totalCostForSets += Convert.ToDouble(labPriceAD.Text);
                                        gridView2.AddNewRow();
                                        if (gridView2.IsLastVisibleRow)
                                        {
                                            gridView2.FocusedRowHandle = gridView2.RowCount - 1;
                                        }
                                        int n = gridView2.RowCount - 1;
                                        double q = -1;
                                        foreach (GridColumn item in gridView2.Columns)
                                        {
                                            if (item.Name == "الكمية")
                                            {
                                                dbconnection.Open();
                                                string query = "select Quantity from dash where dash.Code=" + OfferID + " and Bill_Number=" + txtBillNo.Text;
                                                MySqlCommand com = new MySqlCommand(query, dbconnection);
                                                double DashQuanityt = Convert.ToDouble(com.ExecuteScalar());
                                                dbconnection.Close();
                                                gridView2.SetRowCellValue(n, "الكمية", (Convert.ToDouble(gridView1.GetRowCellValue(i, "الكمية")) / DashQuanityt * Convert.ToDouble(txtTotalMeters.Text)).ToString());
                                                q = Convert.ToDouble(gridView1.GetRowCellValue(i,"الكمية")) / DashQuanityt * Convert.ToDouble(txtTotalMeters.Text);
                                            }
                                            else if (item.Name == "السعر")
                                            {
                                                gridView2.SetRowCellValue(n, item.Name, q * Convert.ToDouble(gridView1.GetRowCellValue(i, "السعر")));

                                            }
                                            //else if (item.HeaderCell.Value.ToString() == "سعر البيع")
                                            //{
                                            //    gridView2.Rows[n].Cells[item.Index].Value = q * Convert.ToDouble(gridView1.Rows[row.Index].Cells["سعر البيع"].Value);
                                            //}
                                            else
                                            {
                                                gridView2.SetRowCellValue(n, item.Name, gridView1.GetRowCellValue(i, item.Name));
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }
                            //CalculateTotalBill(totalCostForPackages);
                            row1 = null;
                            clear();
                        }
                        else
                        {
                            //addedRecordIDs[recordCount] = Convert.ToInt16(gridView1.Rows[gridView1.SelectedCells[0].RowIndex].Cells["DashID"].Value);
                            //gridView1.Rows[gridView1.SelectedCells[0].RowIndex].DefaultCellStyle.BackColor = Color.Silver;
                            recordCount++;
                            gridView2.AddNewRow();
                            int n = gridView2.GetRowHandle(gridView2.DataRowCount);

                            gridView2.SetRowCellValue(n, "الكمية", txtTotalMeters.Text);
                            foreach (GridColumn item in gridView1.Columns)
                            {
                                if (item.Name == "السعر")
                                {
                                    //gridView2.SetRowCellValue(n, item.Name, (Convert.ToDouble(txtTotalMeters.Text) * Convert.ToDouble(gridView1.Rows[row1.Index].Cells["السعر"].Value)));

                                }
                                //else if (item.HeaderCell.Value.ToString() == "سعر البيع")
                                //{
                                //    gridView2.Rows[n].Cells[item.Index].Value = (Convert.ToDouble(txtTotalMeters.Text) * Convert.ToDouble(gridView1.GetRowCellValue(n, "سعر البيع").ToString()));
                                //}
                                else
                                {
                                    //gridView2.SetRowCellValue(n, item.Name, gridView1.GetRowCellValue([row1.Index], item.Name));
                                }

                            }

                            //CalculateTotalBill(totalCostForPackages);
                            row1 = null;
                            clear();

                        }
                    }
                    else
                    {
                        MessageBox.Show("you must select row or this recorded already added");
                        dbconnection.Close();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("enter correct value");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void btnDeleteGrid_Click(object sender, EventArgs e)
        {
            try
            {

                DataGridViewRow row2=new DataGridViewRow(); //= gridView2.Rows[gridView2.SelectedCells[0].RowIndex];

                CalculateTotalBill(totalCostForPackages);
                if (row2.Cells["Package_ID"].Value.ToString() != "")
                {
                    if (row2.Cells["Package_ID"].Value.ToString() != "" && row2.Cells["ProductType"].Value.ToString() == "عرض")
                    {
                        #region عرض
                        for (int i = 0; i < addedPackages.Length; i++)
                        {
                            if (addedPackages[i] == Convert.ToInt16(row2.Cells["Package_ID"].Value))
                            {

                                //foreach (DataGridViewRow item in dataGridView1.Rows)
                                //{
                                //    if (item.Cells["Package_ID"].Value.ToString() != "" && row2.Cells["ProductType"].Value.ToString() == "عرض")
                                //    {
                                //        if (Convert.ToInt16(item.Cells["Package_ID"].Value) == Convert.ToInt16(row2.Cells["Package_ID"].Value))
                                //        {
                                //            item.DefaultCellStyle.BackColor = Color.White;

                                //        }
                                //    }
                                //}
                                DataGridViewRow[] x = new DataGridViewRow[5];
                                //foreach (DataGridViewRow item1 in dataGridView2.Rows)
                                //{
                                //    if (item1.Cells["Package_ID"].Value.ToString() != "" && row2.Cells["ProductType"].Value.ToString() == "عرض")
                                //    {
                                //        if (item1.Cells["Package_ID"].Value == row2.Cells["Package_ID"].Value)
                                //        {
                                //            x[i] = item1;
                                //            addedPackages[i] = 0;
                                //            i++;
                                //        }
                                //    }
                                //}
                                //foreach (DataGridViewRow item in x)
                                //{
                                //    if (item != null)
                                //    {
                                //        dataGridView2.Rows.Remove(item);
                                //    }
                                //}
                                return;

                            }
                        }
                        #endregion
                    }
                    else if (row2.Cells["Package_ID"].Value.ToString() != "" && row2.Cells["ProductType"].Value.ToString() == "طقم")
                    {
                        #region طقم
                        for (int i = 0; i < addedSets.Length; i++)
                        {
                            if (addedSets[i] == Convert.ToInt16(row2.Cells["Package_ID"].Value))
                            {
                                //foreach (DataGridViewRow item in dataGridView1.Rows)
                                //{
                                //    if (item.Cells["Package_ID"].Value.ToString() != "" && item.Cells["ProductType"].Value.ToString() == "طقم")
                                //    {
                                //        if (Convert.ToInt16(item.Cells["Package_ID"].Value) == Convert.ToInt16(row2.Cells["Package_ID"].Value))
                                //        {
                                //            item.DefaultCellStyle.BackColor = Color.White;

                                //        }
                                //    }
                                //}
                                //DataGridViewRow[] x = new DataGridViewRow[10];
                                //foreach (DataGridViewRow item1 in dataGridView2.Rows)
                                //{
                                //    if (item1.Cells["Package_ID"].Value.ToString() != "" && item1.Cells["ProductType"].Value.ToString() == "طقم")
                                //    {
                                //        if (Convert.ToInt16(item1.Cells["Package_ID"].Value) == Convert.ToInt16(row2.Cells["Package_ID"].Value))
                                //        {
                                //            x[i] = item1;
                                //            addedSets[i] = 0;
                                //            i++;
                                //        }
                                //    }
                                //}
                                //foreach (DataGridViewRow item in x)
                                //{
                                //    if (item != null)
                                //    {
                                //        dataGridView2.Rows.Remove(item);
                                //    }
                                //}
                                //return;

                            }
                        }
                        #endregion
                    }
                }

                DataGridViewRow deletedRow = new DataGridViewRow();
                if (row2.Cells["DashID"].Value.ToString() != "")
                {
                    for (int i = 0; i < addedRecordIDs.Length; i++)
                    {

                        if (addedRecordIDs[i] == Convert.ToInt16(row2.Cells["DashID"].Value))
                        {

                            //foreach (DataGridViewRow item in gridView2.Rows)
                            //{
                            //    if (item.Cells["DashID"].Value.ToString() != "")
                            //    {
                            //        if (Convert.ToInt16(row2.Cells["DashID"].Value) == Convert.ToInt16(item.Cells["DashID"].Value))
                            //        {
                            //            foreach (DataGridViewRow item1 in gridView1.Rows)
                            //            {
                            //                if (item1.Cells["DashID"].Value.ToString() != "")
                            //                {
                            //                    if (Convert.ToInt16(row2.Cells["DashID"].Value) == Convert.ToInt16(item1.Cells["DashID"].Value))
                            //                        item1.DefaultCellStyle.BackColor = Color.White;
                            //                }
                            //            }
                            //            gridView2.Rows.Remove(item);
                            //            addedRecordIDs[i] = addedRecordIDs[recordCount--];
                            //            return;
                            //        }
                            //    }
                            //}
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();

                List<int> pakagesIDs = new List<int>();
                if (txtBillNo.Text != "" && customer_ID != "" && delegate_Name != "" && customer_Name != "")
                {
                    string query = "select Branch_BillNumber from customer_bill where Branch_ID=" + Branch_ID + " order by Branch_BillNumber  Desc limit 1";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    int Branch_BillNumber = Convert.ToInt16(com.ExecuteScalar().ToString()) + 1;

                    query = "insert into customer_bill (RecivedType,Branch_BillNumber,Client_ID,Customer_ID,Delegate_ID,Total_CostBD,Total_CostAD,Bill_Date,Type_Buy,Initial_Value,Paid_Status,Branch_ID,Branch_Name) values (@RecivedType,@Branch_BillNumber,@Client_ID,@Customer_ID,@Delegate_ID,@Total_CostBD,@Total_CostAD,@Bill_Date,@Type_Buy,@Initial_Value,0,@Branch_ID,@Branch_Name)";
                    com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@Branch_BillNumber", MySqlDbType.Int16);
                    com.Parameters["@Branch_BillNumber"].Value = Branch_BillNumber;
                    com.Parameters.Add("@Client_ID", MySqlDbType.Int16);
                    com.Parameters["@Client_ID"].Value = customer_ID;
                    if (engID != "")
                    {
                        com.Parameters.Add("@Customer_ID", MySqlDbType.Int16);
                        com.Parameters["@Customer_ID"].Value = engID;
                    }
                    else
                    {
                        com.Parameters.Add("@Customer_ID", MySqlDbType.Int16);
                        com.Parameters["@Customer_ID"].Value = null;
                    }

                    if (type == "كاش")
                    {
                        com.Parameters.Add("@Initial_Value", MySqlDbType.Decimal);
                        com.Parameters["@Initial_Value"].Value = 0;

                        com.Parameters.Add("@Type_Buy", MySqlDbType.VarChar);
                        com.Parameters["@Type_Buy"].Value = "كاش";
                    }
                    else if (type == "آجل")
                    {
                        com.Parameters.Add("@Initial_Value", MySqlDbType.Decimal);
                        com.Parameters["@Initial_Value"].Value = Convert.ToDecimal(value);

                        com.Parameters.Add("@Type_Buy", MySqlDbType.VarChar);
                        com.Parameters["@Type_Buy"].Value = "آجل";
                    }
                    com.Parameters.Add("@Delegate_ID", MySqlDbType.Int16);
                    com.Parameters["@Delegate_ID"].Value = delegate_ID;
                    com.Parameters.Add("@Total_CostBD", MySqlDbType.Double);
                    com.Parameters["@Total_CostBD"].Value = labFinialTotalBillPriceBD.Text;
                    com.Parameters.Add("@Total_CostAD", MySqlDbType.Double);
                    com.Parameters["@Total_CostAD"].Value = labFinialTotalBillPriceAD.Text;
                    com.Parameters.Add("@Bill_Date", MySqlDbType.DateTime);
                    com.Parameters["@Bill_Date"].Value = date.ToString("yyyy-MM-dd");
                    com.Parameters.Add("@Branch_ID", MySqlDbType.Int16);
                    com.Parameters["@Branch_ID"].Value = Convert.ToInt16(Branch_ID);
                    com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar);
                    com.Parameters["@Branch_Name"].Value = Branch_Name;
                    com.Parameters.Add("@RecivedType", MySqlDbType.VarChar);
                    com.Parameters["@RecivedType"].Value = RecivedType;
                    com.ExecuteNonQuery();

                    query = "select Dash_Bill_ID from customer_bill order by Dash_Bill_ID desc limit 1";
                    com = new MySqlCommand(query, dbconnection);

                    if (com.ExecuteScalar() != null)
                    {
                        Dash_Bill_ID = (int)com.ExecuteScalar();


                        //foreach (DataGridViewRow row in gridView2.Rows)
                        //{

                        //    query = "insert into product_bill (Set_ID,Code,Price,Discount,Price_Discount,Dash_Bill_ID,Quantity) values (@Set_ID,@Code,@Price,@Discount,@Price_Discount,@Dash_Bill_ID,@Quantity)";
                        //    com = new MySqlCommand(query, dbconnection);
                        //    bool flag = false;
                        //    for (int i = 0; i < pakagesIDs.Count; i++)
                        //    {
                        //        if (row.Cells["Package_ID"].Value.ToString() != "")
                        //        {
                        //            if (pakagesIDs[i] == Convert.ToInt16(row.Cells["Package_ID"].Value))
                        //            {
                        //                flag = true;
                        //            }
                        //        }
                        //    }
                        //    if (!flag)
                        //    {
                        //        if (row.Cells["Package_ID"].Value.ToString() == ""|| row.Cells["ProductType"].Value.ToString() == "طقم")
                        //        {
                        //            com.Parameters.Add("@Code", MySqlDbType.VarChar);
                        //            com.Parameters["@Code"].Value = row.Cells["ProductCode"].Value;
                        //            com.Parameters.Add("@Price_Discount", MySqlDbType.Decimal);
                        //            com.Parameters["@Price_Discount"].Value = Convert.ToDouble(row.Cells["ProductSellPrice"].Value);
                        //            com.Parameters.Add("@Price", MySqlDbType.Decimal);
                        //            com.Parameters["@Price"].Value = Convert.ToDouble(row.Cells["ProductPrice"].Value);
                        //            com.Parameters.Add("@Discount", MySqlDbType.Decimal);
                        //            com.Parameters["@Discount"].Value = Convert.ToDouble(row.Cells["ProductDiscount"].Value);
                        //            com.Parameters.Add("@Quantity", MySqlDbType.Decimal);
                        //            com.Parameters["@Quantity"].Value = Convert.ToDouble(row.Cells["ProductQuantity"].Value);
                        //            if (row.Cells["ProductType"].Value.ToString() == "طقم")
                        //            {
                        //                com.Parameters.Add("@Set_ID", MySqlDbType.Int16);
                        //                com.Parameters["@Set_ID"].Value = row.Cells["Package_ID"].Value;
                        //            }
                        //            else
                        //            {
                        //                com.Parameters.Add("@Set_ID", MySqlDbType.Int16);
                        //                com.Parameters["@Set_ID"].Value = null;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            com.Parameters.Add("@Set_ID", MySqlDbType.Int16);
                        //            com.Parameters["@Set_ID"].Value = null;
                        //            com.Parameters.Add("@Code", MySqlDbType.VarChar);
                        //            com.Parameters["@Code"].Value = row.Cells["Package_ID"].Value.ToString();
                        //            com.Parameters.Add("@Price", MySqlDbType.Decimal);
                        //            com.Parameters["@Price"].Value = null;
                        //            com.Parameters.Add("@Discount", MySqlDbType.Decimal);
                        //            com.Parameters["@Discount"].Value = null;
                        //            int pakageId = Convert.ToInt16(row.Cells["Package_ID"].Value.ToString());
                        //            string q = "select Price from offer where Offer_ID=" + pakageId;
                        //            MySqlCommand com1 = new MySqlCommand(q, dbconnection);
                        //            double packagePrice = Convert.ToDouble(com1.ExecuteScalar());
                        //            q = "select Quantity from offer_details where Offer_ID=" + pakageId + " and Code=" + row.Cells["ProductCode"].Value.ToString();
                        //            com1 = new MySqlCommand(q, dbconnection);
                        //            double packageQuantity = Convert.ToDouble(row.Cells["ProductQuantity"].Value) / Convert.ToDouble(com1.ExecuteScalar());
                        //            com.Parameters.Add("@Price_Discount", MySqlDbType.Decimal);
                        //            com.Parameters["@Price_Discount"].Value = packagePrice * packageQuantity;
                        //            com.Parameters.Add("@Quantity", MySqlDbType.Decimal);
                        //            com.Parameters["@Quantity"].Value = Convert.ToDouble(packageQuantity);

                        //            string query1 = "select Quantity from offer_details where Offer_ID=" + Convert.ToInt16(row.Cells["Package_ID"].Value);
                        //            MySqlCommand com2 = new MySqlCommand(query1, dbconnection);
                        //            double quantity = Convert.ToDouble(com2.ExecuteScalar());
                        //            double OrderQuantity = Convert.ToDouble(row.Cells["ProductQuantity"].Value) / quantity;

                        //            query1 = "select Offer_Quantity from offer where Offer_ID=" + Convert.ToInt16(row.Cells["Package_ID"].Value);
                        //            com2 = new MySqlCommand(query1, dbconnection);
                        //            double offerQuantity = Convert.ToDouble(com2.ExecuteScalar());
                        //            offerQuantity -= OrderQuantity;

                        //            query1 = "update offer set Offer_Quantity=" + offerQuantity + " where Offer_ID=" + Convert.ToInt16(row.Cells["Package_ID"].Value);
                        //            com2 = new MySqlCommand(query1, dbconnection);
                        //            com2.ExecuteNonQuery();

                        //            pakagesIDs.Add(Convert.ToInt16(row.Cells["Package_ID"].Value));


                        //        }

                        //        com.Parameters.Add("@Dash_Bill_ID", MySqlDbType.Int16);
                        //        com.Parameters["@Dash_Bill_ID"].Value = Dash_Bill_ID;



                        //        com.ExecuteNonQuery();
                        //    }

                        //}
                    }
                    MessageBox.Show("Done");

                    double initialValue = 0;

                    if (txtType.Text == "آجل")
                    {
                        initialValue = Convert.ToDouble(value);
                        DecreaseProductQuantity(Dash_Bill_ID);
                    }
                    else
                    {
                        initialValue = Convert.ToDouble(labFinialTotalBillPriceAD.Text);
                    }

                    DataTable dt = new DataTable();
                    for (int i = 0; i < gridView2.Columns.Count - 2; i++)
                    {
                        dt.Columns.Add(gridView2.Columns[i].Name.ToString());
                    }
                    //for (int i = 0; i < gridView2.Rows.Count; i++)
                    //{
                    //    DataRow dr = dt.NewRow();
                    //    for (int j = 0; j < gridView2.Columns.Count - 2; j++)
                    //    {
                    //        dr[gridView2.Columns[j].Name.ToString()] = gridView2.Rows[i].Cells[j].Value;
                    //    }

                    //    dt.Rows.Add(dr);
                    //}

                    if (engID != "")
                    {
                        //DetailsConfirm_CrystalReport f2 = new DetailsConfirm_CrystalReport(dt, Dash_Bill_ID, delegate_Name, engID, engName, type, initialValue, labFinialTotalBillPriceBD.Text, labFinialTotalBillPriceAD.Text);
                        //f2.Show();
                        //this.Hide();
                    }
                    else if (customer_ID != "")
                    {
                        //DetailsConfirm_CrystalReport f2 = new DetailsConfirm_CrystalReport(dt, Dash_Bill_ID, delegate_Name, customer_ID, customer_Name, type, initialValue, labFinialTotalBillPriceBD.Text, labFinialTotalBillPriceAD.Text);
                        //f2.Show();
                        //this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("initial value must be least than or equal to bill value");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        
        //functions
        //clear details of selected row from grid view1
        public void clear()
        {
            txtTotalMeters.Text = labPriceAD.Text = labSellPrice.Text = "";
            code = "";
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                //if (loaded)
                //{
                row1 = gridView1.GetDataRow(gridView1.GetRowHandle(e.FocusedRowHandle));
                int EmpBranchId = UserControl.UserBranch(dbconnection);
                Added = false;
                if (row1["الكود"].ToString() != "")
                {
                    code = row1["الكود"].ToString();

                    labSellPrice.Text = row1["السعر"].ToString();
                    txtTotalMeters.Text = row1["الكمية"].ToString();

                    for (int i = 0; i < addedRecordIDs.Length; i++)
                    {
                        if (addedRecordIDs[i] == Convert.ToInt16(row1["التسلسل"].ToString()))
                        {
                            Added = true;
                            break;
                        }
                    }
                }
                else if (row1["التسلسل"].ToString() != "" && row1["الاسم"].ToString().Split(')')[0].Split('(')[1] == "عرض")
                {
                    #region عرض
                    dbconnection.Open();

                    OfferID = Convert.ToInt16(row1["التسلسل"].ToString());

                    //foreach (DataGridViewRow row in gridView1.Rows)
                    //{
                    //    if (row.Cells["Package_ID"].Value.ToString() != "")
                    //    {
                    //        if (Convert.ToInt16(row.Cells["Package_ID"].Value) == OfferID)
                    //            row.Selected = true;
                    //    }
                    //}

                    string query = "select * from offer inner join offer_details on offer.Offer_ID=offer_details.Offer_ID where offer.Offer_ID=" + OfferID;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        labSellPrice.Text = dr["Price"].ToString();
                        //txtTotalMeters.Text = (Convert.ToDouble(row1["الكمية"].ToString()) / Convert.ToDouble(dr["Quantity"].ToString())).ToString();
                        txtTotalMeters.Text = Convert.ToDouble(row1["الكمية"].ToString()).ToString();
                        quantityBU = Convert.ToDouble(txtTotalMeters.Text);

                    }
                    dr.Close();

                    for (int i = 0; i < addedPackages.Length; i++)
                    {
                        if (addedPackages[i] == Convert.ToInt16(row1["التسلسل"].ToString()))
                        {
                            Added = true;
                            break;
                        }
                    }
                    #endregion
                }
                else if (row1["التسلسل"].ToString() != "" && row1["الاسم"].ToString().Split(')')[0].Split('(')[1] == "طقم")
                {
                    #region طقم
                    dbconnection.Open();

                    OfferID = Convert.ToInt16(row1["التسلسل"]);

                    //foreach (DataGridViewRow row in gridView1.Rows)
                    //{
                    //    if (row.Cells["Package_ID"].Value.ToString() != "")
                    //    {
                    //        if (Convert.ToInt16(row.Cells["Package_ID"].Value) == OfferID)
                    //            row.Selected = true;
                    //    }
                    //}

                    //string query = "select sum(Sell_Price*Quantity) as 'Price' from sets inner join set_details on sets.Set_ID=set_details.Set_ID inner join price on price.Code=set_details.Code where sets.Set_ID=" + OfferID;
                    //MySqlCommand com = new MySqlCommand(query, dbconnection);
                    labSellPrice.Text = row1["السعر"].ToString(); //com.ExecuteScalar().ToString();

                    //query = "select Quantity from dash where dash.Code=" + OfferID + " and Bill_Number=" + txtBillNo.Text + " and Branch_ID=" + EmpBranchId;
                    //com = new MySqlCommand(query, dbconnection);
                    txtTotalMeters.Text = row1["الكمية"].ToString(); //com.ExecuteScalar().ToString();

                    for (int i = 0; i < addedSets.Length; i++)
                    {
                        if (addedSets[i] == Convert.ToInt16(row1["التسلسل"].ToString()))
                        {
                            Added = true;
                            break;
                        }
                    }
                    #endregion
                }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void BillDetails_Confirm_Load(object sender, EventArgs e)
        {
            try
            {
                gridView1.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                gridView1.Columns[1].Width = 200;
                for (int i = 2; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].Width = 150;
                }

                MySqlDataAdapter adapter = new MySqlDataAdapter("select distinct dash.Dash_ID as 'التسلسل', dash_details.Code as 'الكود',product.Product_Name as 'الاسم',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',dash_details.Quantity as 'الكمية',sellprice.Price as 'السعر',(sellprice.Price * dash_details.Quantity) as 'الاجمالى',(sellprice.Sell_Discount * dash_details.Quantity) as 'الخصم',data.Carton as 'الكرتنة' from dash INNER JOIN dash_details ON dash_details.Dash_ID = dash.Dash_ID inner join data on data.Code=dash_details.Code inner join product on product.Product_ID=data.Product_ID INNER JOIN color ON color.Color_ID = data.Color_ID INNER JOIN size ON size.Size_ID = data.Size_ID INNER JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN sellprice ON sellprice.Code = dash_details.Code where  dash.Bill_Number=0 and dash.Branch_ID=0", dbconnection);
                DataSet sourceDataSet = new DataSet();
                adapter.Fill(sourceDataSet);
                gridControl2.DataSource = sourceDataSet.Tables[0];

                gridView2.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                gridView2.Columns[1].Width = 200;
                for (int i = 2; i < gridView2.Columns.Count; i++)
                {
                    gridView2.Columns[i].Width = 150;
                }

                //loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //string query = "select Store_Name as 'اسم المخزن',Total_Meters as 'الكمية' from storage where Code=" + row1["Package_ID"].ToString() + " group by Code ,Store_ID";
                //MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                if (gridView1.SelectedRowsCount > 0)
                {
                    StoresDetails sd = new StoresDetails(gridView1.GetRowCellDisplayText(gridView1.GetSelectedRows()[0], "الكود"));
                    sd.ShowDialog();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //CalculateTotalBill
        public void CalculateTotalBill( double totalCostForPackages)
        {
            double BD = 0, AD = 0;
            //foreach (DataGridViewRow row in gridView2.Rows)
            //{
            //    if (row.Cells["ProductPrice"].Value.ToString() != "")
            //    {
            //        BD += Convert.ToDouble(row.Cells["ProductPrice"].Value);// * Convert.ToDouble(row.Cells["ProductQuantity"].Value);
            //        AD += Convert.ToDouble(row.Cells["ProductSellPrice"].Value);// * Convert.ToDouble(row.Cells["ProductQuantity"].Value);
            //    }

            //}
            AD += totalCostForPackages;
            BD += totalCostForPackages;
            labFinialTotalBillPriceBD.Visible = true;
            labFinialTotalBillPriceAD.Visible = true;
            labFinialTotalBillPriceBD.Text = BD.ToString();
            labFinialTotalBillPriceAD.Text = AD.ToString();
        }
        //calculate no of carton for No of meters selected
        public bool cartonNoCheck()
        {
            try
            {
                dbconnection.Open();
                //MessageBox.Show(row1["الاسم"].ToString().Split(')')[0].Split('(')[1]);
                if (row1["الاسم"].ToString().Split(')')[0].Split('(')[1] != "عرض" && row1["الاسم"].ToString().Split(')')[0].Split('(')[1] != "طقم")
                {
                    string query = "select Carton from data where Code='" + code + "'";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        double Carton = Convert.ToDouble(com.ExecuteScalar());
                        double totalMeters = double.Parse(txtTotalMeters.Text);
                        if (totalMeters % Carton == 0)
                        {
                            MessageBox.Show("you need " + totalMeters / Carton + " carton");
                            dbconnection.Close();
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("you need " + totalMeters / Carton + " carton and " + totalMeters % Carton + " meter");
                            Added = false;
                            dbconnection.Close();
                            return false;
                        }
                    }
                }
                else if (row1["التسلسل"].ToString() != "" && row1["الاسم"].ToString().Split(')')[0].Split('(')[1] == "عرض")
                {
                    string query = "select Offer_Quantity from offer where Offer_ID=" + Convert.ToInt16(row1["التسلسل"].ToString());
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    double quantity = Convert.ToDouble(com.ExecuteScalar());
                    double OrderQuantity = Convert.ToDouble(txtTotalMeters.Text);
                    if (OrderQuantity <= quantity)
                    {
                        dbconnection.Close();
                        return true;
                    }
                    else
                    {
                        dbconnection.Close();
                        return false;
                    }
                }
                else if (row1["التسلسل"].ToString() != "" && row1["الاسم"].ToString().Split(')')[0].Split('(')[1] == "طقم")
                {
                    string query = "select sum(Total_Meters) from storage where Code=" + Convert.ToInt16(row1["التسلسل"].ToString())+" group by Code";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    double quantity = Convert.ToDouble(com.ExecuteScalar());
                    double OrderQuantity = Convert.ToDouble(txtTotalMeters.Text);
                    if (OrderQuantity <= quantity)
                    {
                        dbconnection.Close();
                        return true;
                    }
                    else
                    {
                        dbconnection.Close();
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("you must select row first");
                    dbconnection.Close();
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            return false;
        }

        public void DecreaseProductQuantity( int ID)
        {
            try
            {

                connectionReader.Open();
                connectionReader1.Open();
                connectionReader2.Open();
                string q;
                int id;
                bool flag = false;
                double storageQ, productQ;


                string query = "select RecivedType from customer_bill where Dash_Bill_ID=" + ID;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                string store = com.ExecuteScalar().ToString();
                if (store != "العميل")
                {
                    query = "select Code,Quantity from product_bill where Dash_Bill_ID=" + ID;
                    com = new MySqlCommand(query, connectionReader);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["Code"].ToString().Length < 20)
                        {
                            query = "select Code,Quantity,Set_ID from offer_details where Offer_ID=" + Convert.ToInt16(dr["Code"].ToString());
                            com = new MySqlCommand(query, connectionReader1);
                            MySqlDataReader dr1 = com.ExecuteReader();

                            while (dr1.Read())
                            {
                                query = "select sum(Total_Meters) from storage where Code='" + dr1["Code"].ToString() + "' and Store_Name='" + store + "'";
                                com = new MySqlCommand(query, connectionReader2);
                                double quantityInStore = Convert.ToDouble(com.ExecuteScalar());
                                productQ = Convert.ToDouble(dr1["Quantity"]) * Convert.ToDouble(dr["Quantity"]);
                                if (quantityInStore >= productQ)
                                {
                                    query = "select Storage_ID,Total_Meters from storage where Code='" + dr1["Code"].ToString() + "' and Store_Name='" + store + "'";
                                    com = new MySqlCommand(query, connectionReader2);
                                    MySqlDataReader dr2 = com.ExecuteReader();
                                    while (dr2.Read())
                                    {

                                        storageQ = Convert.ToDouble(dr2["Total_Meters"]);

                                        if (storageQ > productQ)
                                        {
                                            id = Convert.ToInt16(dr2["Storage_ID"]);
                                            q = "update storage set Total_Meters=" + (storageQ - productQ) + " where Storage_ID=" + id;
                                            MySqlCommand comm = new MySqlCommand(q, dbconnection);
                                            comm.ExecuteNonQuery();
                                            productQ -= storageQ;
                                            flag = true;
                                            break;
                                        }
                                        else
                                        {
                                            id = Convert.ToInt16(dr2["Storage_ID"]);
                                            q = "update storage set Total_Meters=" + 0 + " where Storage_ID=" + id;
                                            MySqlCommand comm = new MySqlCommand(q, dbconnection);
                                            comm.ExecuteNonQuery();
                                            productQ -= storageQ;
                                        }
                                    }
                                    dr2.Close();

                                    if (!flag)
                                    {
                                        MessageBox.Show(dr["Code"].ToString() + "not valid in store");
                                    }
                                    flag = false;
                                }
                                else
                                {
                                    MessageBox.Show(dr["Code"].ToString() + "not valid in store");
                                }
                            }
                            dr1.Close();
                        }
                        else if (dr["Code"].ToString().Length == 20)
                        {
                            if (dr["Set_ID"].ToString() == "")
                            {
                                #region بند
                                query = "select sum(Total_Meters) from storage where Code='" + dr["Code"].ToString() + "' and Store_Name='" + store + "'";
                                com = new MySqlCommand(query, connectionReader2);
                                double quantityInStore = Convert.ToDouble(com.ExecuteScalar());
                                productQ = Convert.ToDouble(dr["Quantity"]);
                                if (quantityInStore >= productQ)
                                {
                                    query = "select Storage_ID,Total_Meters from storage where Code='" + dr["Code"].ToString() + "' and Store_Name='" + store + "'";
                                    com = new MySqlCommand(query, connectionReader2);
                                    MySqlDataReader dr2 = com.ExecuteReader();
                                    while (dr2.Read())
                                    {

                                        storageQ = Convert.ToDouble(dr2["Total_Meters"]);

                                        if (storageQ > productQ)
                                        {
                                            id = Convert.ToInt16(dr2["Storage_ID"]);
                                            q = "update storage set Total_Meters=" + (storageQ - productQ) + " where Storage_ID=" + id;
                                            MySqlCommand comm = new MySqlCommand(q, dbconnection);
                                            comm.ExecuteNonQuery();
                                            productQ -= storageQ;
                                            flag = true;
                                            break;
                                        }
                                        else
                                        {
                                            id = Convert.ToInt16(dr2["Storage_ID"]);
                                            q = "update storage set Total_Meters=" + 0 + " where Storage_ID=" + id;
                                            MySqlCommand comm = new MySqlCommand(q, dbconnection);
                                            comm.ExecuteNonQuery();
                                            productQ -= storageQ;
                                        }
                                    }
                                    dr2.Close();

                                    if (!flag)
                                    {
                                        MessageBox.Show(dr["Code"].ToString() + "not valid in store");
                                    }
                                    flag = false;
                                }
                                else
                                {
                                    MessageBox.Show(dr["Code"].ToString() + "not valid in store");
                                }
                                #endregion
                            }
                            else
                            {
                                #region طقم
                                query = "select Quantity from set_details where Set_ID=" + dr["Code"].ToString()+ " order by SetDetails_ID limit 1";
                                com = new MySqlCommand(query, dbconnection);
                                double itemQuantity=Convert.ToDouble(com.ExecuteScalar());

                                query = "select sum(Total_Meters) from storage where Code='" + dr["Set_ID"].ToString() + "' and Store_Name='" + store + "'";
                                com = new MySqlCommand(query, connectionReader2);
                                double quantityInStore = Convert.ToDouble(com.ExecuteScalar());
                                productQ = Convert.ToDouble(dr["Quantity"])/ itemQuantity;
                                if (quantityInStore >= productQ)
                                {
                                    query = "select Storage_ID,Total_Meters from storage where Code='" + dr["Set_ID"].ToString() + "' and Store_Name='" + store + "'";
                                    com = new MySqlCommand(query, connectionReader2);
                                    MySqlDataReader dr2 = com.ExecuteReader();
                                    while (dr2.Read())
                                    {

                                        storageQ = Convert.ToDouble(dr2["Total_Meters"]);

                                        if (storageQ > productQ)
                                        {
                                            id = Convert.ToInt16(dr2["Storage_ID"]);
                                            q = "update storage set Total_Meters=" + (storageQ - productQ) + " where Storage_ID=" + id;
                                            MySqlCommand comm = new MySqlCommand(q, dbconnection);
                                            comm.ExecuteNonQuery();
                                            productQ -= storageQ;
                                            flag = true;
                                            break;
                                        }
                                        else
                                        {
                                            id = Convert.ToInt16(dr2["Storage_ID"]);
                                            q = "update storage set Total_Meters=" + 0 + " where Storage_ID=" + id;
                                            MySqlCommand comm = new MySqlCommand(q, dbconnection);
                                            comm.ExecuteNonQuery();
                                            productQ -= storageQ;
                                        }
                                    }
                                    dr2.Close();

                                    if (!flag)
                                    {
                                        MessageBox.Show(dr["Set_ID"].ToString() + "not valid in store");
                                    }
                                    flag = false;
                                }
                                else
                                {
                                    MessageBox.Show(dr["Set_ID"].ToString() + "not valid in store");
                                }
                                #endregion
                            }
                        }
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connectionReader2.Close();
            connectionReader1.Close();
            connectionReader.Close();

        }

    }
}
