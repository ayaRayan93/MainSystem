﻿using MySql.Data.MySqlClient;
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
    public partial class CustomerReturnBill : Form
    {
        MySqlConnection dbconnection , connectionReader, connectionReader2;
        private string Customer_Type;
        private bool loaded = false;
        private bool flag = false;
        bool comBranchLoaded = false;
        DataGridViewRow row1;
        private int[] addedRecordIDs;
        int recordCount = 0;
        //List<DataGridViewRow> myRows;
        List<int> listOfRow2In;
        int EmpBranchId = 0;
        //int customerBillId = 0;
        MainForm salesMainForm;

        public CustomerReturnBill(MainForm SalesMainForm)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            connectionReader = new MySqlConnection(connection.connectionString);
            connectionReader2 = new MySqlConnection(connection.connectionString);
            addedRecordIDs = new int[100];
            listOfRow2In = new List<int>();
            //myRows = new List<DataGridViewRow>();
            labClient.Visible = false;
            labCustomer.Visible = false;//label of مهندس/مقاول
            comClient.Visible = false;
            comCustomer.Visible = false;
            txtCustomerID.Visible = false;
            txtClientID.Visible = false;

            labBillNumber.Visible = false;
            comBillNumber.Visible = false;

            salesMainForm = SalesMainForm;
        }

        private void CustomerReturnBill_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from branch";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comBranch.Text = "";
                txtBranchID.Text = "";
                comBranchLoaded = true;
                EmpBranchId = UserControl.UserBranch(dbconnection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //check type of customer if engineer,client or contract 
        private void radiotype_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            Customer_Type = radio.Text;
            groupBox2.Visible = true;
            labBillNumber.Visible = false;
            comBillNumber.Visible = false;
            loaded = false;
            try
            {
                if (Customer_Type == "عميل")
                {
                    labCustomer.Visible = false;
                    comCustomer.Visible = false;
                    txtCustomerID.Visible = false;
                    labClient.Visible = true;
                    comClient.Visible = true;
                    txtClientID.Visible = true;

                    string query = "select * from customer where Customer_Type='" + Customer_Type + "'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comClient.DataSource = dt;
                    comClient.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comClient.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comClient.Text = "";

                    txtClientID.Text = "";
                }
                else
                {
                    labCustomer.Visible = true;
                    comCustomer.Visible = true;
                    txtCustomerID.Visible = true;
                    labClient.Visible = false;
                    comClient.Visible = false;
                    txtClientID.Visible = false;


                    string query = "select * from customer where Customer_Type='" + Customer_Type + "'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comCustomer.DataSource = dt;
                    comCustomer.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comCustomer.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comCustomer.Text = "";
                    txtCustomerID.Text = "";


                }
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void comClient_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    txtClientID.Text = comClient.SelectedValue.ToString();
                    if (txtBranchID.Text != "")
                    {
                        DisplayBillNumber(Convert.ToInt16(comCustomer.SelectedValue), Convert.ToInt16(comClient.SelectedValue));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //when select customer(مهندس,مقاول)display in comCustomer the all clients of th customer 
        private void comCustomer_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                txtCustomerID.Text = comCustomer.SelectedValue.ToString();
                if (txtBranchID.Text != "")
                {
                    DisplayBillNumber(Convert.ToInt16(comCustomer.SelectedValue), Convert.ToInt16(comClient.SelectedValue));
                }

                labClient.Visible = true;
                comClient.Visible = true;
                txtClientID.Visible = true;

                try
                {
                    loaded = false;
                    string query = "select * from customer where Customer_ID in(select Client_ID from custmer_client where Customer_ID=" + comCustomer.SelectedValue + ")";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comClient.DataSource = dt;
                    comClient.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comClient.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comClient.Text = "";
                    comClient.SelectedValue = 0;
                    txtClientID.Text = "";
                    loaded = true;
                    //DisplayBillNumber(Convert.ToInt16(comCustomer.SelectedValue), Convert.ToInt16(comClient.SelectedValue));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
                            case "txtCustomerID":
                                query = "select Customer_Name from customer where Customer_ID=" + txtClientID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comClient.Text = Name;
                                    comClient.SelectedValue = txtClientID.Text;

                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtEngConID":
                                query = "select Customer_Name from customer where Customer_ID=" + txtCustomerID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comCustomer.Text = Name;
                                    comCustomer.SelectedValue = txtCustomerID.Text;
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
            }
        }

        private void comBillNumber_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (flag && comBillNumber.Text != "")
                {
                    int billNum = Convert.ToInt16(comBillNumber.Text);
                    DataTable dtAll = new DataTable();
                    string query = "select data.Data_ID, data.Code as 'الكود',product_bill.Type as 'الفئة',product_bill.Quantity as 'الكمية',product_bill.Price as 'السعر',product_bill.Discount as 'نسبة الخصم',product_bill.PriceAD as 'السعر بعد الخصم' , product.Product_Name as 'الصنف', type.Type_Name as 'النوع', factory.Factory_Name as 'المصنع' ,groupo.Group_Name as 'المجموعة' ,color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Description as 'الوصف',product_bill.Returned as 'تم الاسترجاع',product_bill.CustomerBill_ID  from product_bill inner join data on data.Data_ID=product_bill.Data_ID LEFT JOIN color ON color.Color_ID = data.Color_ID LEFT JOIN size ON size.Size_ID = data.Size_ID LEFT JOIN sort ON sort.Sort_ID = data.Sort_ID INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID  where product_bill.CustomerBill_ID=" + comBillNumber.SelectedValue + " and product_bill.Type='بند'  and (product_bill.Returned='لا' or product_bill.Returned='جزء')";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dtProduct = new DataTable();
                    da.Fill(dtProduct);

                    query = "select sets.Set_ID as 'Data_ID',product_bill.Type as 'الفئة', product_bill.Quantity as 'الكمية',product_bill.Price as 'السعر',product_bill.Discount as 'نسبة الخصم',product_bill.PriceAD as 'السعر بعد الخصم' , sets.Set_Name as 'الصنف', type.Type_Name as 'النوع', factory.Factory_Name as 'المصنع', groupo.Group_Name as 'المجموعة', sets.Description as 'الوصف',product_bill.Returned as 'تم الاسترجاع',product_bill.CustomerBill_ID from product_bill inner join sets on sets.Set_ID=product_bill.Data_ID INNER JOIN type ON type.Type_ID = sets.Type_ID INNER JOIN factory ON sets.Factory_ID = factory.Factory_ID INNER JOIN groupo ON sets.Group_ID = groupo.Group_ID  where product_bill.CustomerBill_ID=" + comBillNumber.SelectedValue + " and product_bill.Type='طقم' and (product_bill.Returned='لا' or product_bill.Returned='جزء')";
                    da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dtSet = new DataTable();
                    da.Fill(dtSet);
                    
                    dtAll = dtProduct.Copy();
                    dtAll.Merge(dtSet);

                    dataGridView1.DataSource = dtAll;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns["CustomerBill_ID"].Visible = false;
                    //dataGridView2.Rows.Clear();

                    txtCode.Text = "";
                    txtPriceAD.Text = "";
                    txtTotalMeter.Text = "";
                    labTotalAD.Text = "";
                    labReturnedQuantity.Text = "";
                    labBillTotalCostAD.Text = "";
                    labBillDate.Text = "";

                    dbconnection.Open();
                    query = "select * from customer_bill where customer_bill.Branch_BillNumber=" + comBillNumber.Text + " and customer_bill.Branch_ID=" + txtBranchID.Text;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //customerBillId = Convert.ToInt16(dr["CustomerBill_ID"].ToString());
                            labBillTotalCostAD.Text = dr["Total_CostAD"].ToString();
                            labBillDate.Text = Convert.ToDateTime(dr["Bill_Date"].ToString()).ToShortDateString();

                            if (!listBoxControlCustomerBill.Items.Contains(comBillNumber.SelectedValue.ToString()))
                            {
                                listBoxControlBills.Items.Add(comBillNumber.Text + ":" + comBranch.Text);
                                listBoxControlCustomerBill.Items.Add(comBillNumber.SelectedValue.ToString());
                            }
                        }
                        dr.Close();
                    }
                    else
                    {
                        //customerBillId = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                row1 = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];

                dbconnection.Open();
                string query = "SELECT sum(customer_return_bill_details.TotalMeter) FROM customer_return_bill_details where customer_return_bill_details.CustomerBill_ID=" + row1.Cells["CustomerBill_ID"].Value.ToString() + " and customer_return_bill_details.Data_ID=" + row1.Cells["Data_ID"].Value.ToString() + " and customer_return_bill_details.Type='" + row1.Cells["الفئة"].Value.ToString() + "'";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                if (com.ExecuteScalar() != null)
                {
                    labReturnedQuantity.Text = com.ExecuteScalar().ToString();
                }
                else
                {
                    labReturnedQuantity.Text = "0";
                }

                txtCode.Text = row1.Cells["الكود"].Value.ToString();
                if (labReturnedQuantity.Text == "")
                {
                    txtTotalMeter.Text = row1.Cells["الكمية"].Value.ToString();
                }
                else
                {
                    txtTotalMeter.Text = (Convert.ToDouble(row1.Cells["الكمية"].Value.ToString()) - Convert.ToDouble(labReturnedQuantity.Text)).ToString();
                }
                txtPriceAD.Text = row1.Cells["السعر بعد الخصم"].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtTotalMeter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double quantity, priceAD;
                if (txtTotalMeter.Text != "" && txtPriceAD.Text != "")
                {
                    if (double.TryParse(txtTotalMeter.Text, out quantity) & double.TryParse(txtPriceAD.Text, out priceAD))
                    {
                        labTotalAD.Text = (priceAD * quantity).ToString();
                    }
                    else
                    {
                        MessageBox.Show("enter correct value");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddToReturnBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsAdded(row1) && row1 != null && txtCode.Text != "")
                {
                    double totalMeter = 0;
                    if (!double.TryParse(txtTotalMeter.Text, out totalMeter))
                    {
                        MessageBox.Show("اجمالى عدد الوحدات يجب ان يكون رقم");
                        return;
                    }

                    double returnedQuantity = 0;
                    if (labReturnedQuantity.Text != "")
                    {
                        returnedQuantity = Convert.ToDouble(labReturnedQuantity.Text);
                    }

                    if ((totalMeter + returnedQuantity) > Convert.ToDouble(row1.Cells["الكمية"].Value))
                    {
                        MessageBox.Show("الكمية لا تكفى");
                        return;
                    }
                    //myRows.Add(row1);
                    addedRecordIDs[recordCount] = dataGridView1.SelectedCells[0].RowIndex + 1;
                    dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DefaultCellStyle.BackColor = Color.Silver;
                    recordCount++;
                    int n = dataGridView2.Rows.Add();
                    dataGridView2.Rows[n].Cells["Data_ID"].Value = row1.Cells["Data_ID"].Value;
                    dataGridView2.Rows[n].Cells["Code"].Value = txtCode.Text;
                    dataGridView2.Rows[n].Cells["Type"].Value = row1.Cells["الفئة"].Value;
                    dataGridView2.Rows[n].Cells["Quantity"].Value = txtTotalMeter.Text;
                    dataGridView2.Rows[n].Cells["priceAD"].Value = txtPriceAD.Text;
                    dataGridView2.Rows[n].Cells["totalAD"].Value = labTotalAD.Text;
                    dataGridView2.Rows[n].Cells["Discount"].Value = row1.Cells["نسبة الخصم"].Value;
                    dataGridView2.Rows[n].Cells["Type_Name"].Value = row1.Cells["النوع"].Value;
                    dataGridView2.Rows[n].Cells["Factory_Name"].Value = row1.Cells["المصنع"].Value;
                    dataGridView2.Rows[n].Cells["Group_Name"].Value = row1.Cells["المجموعة"].Value;
                    dataGridView2.Rows[n].Cells["Product_Name"].Value = row1.Cells["الصنف"].Value;
                    dataGridView2.Rows[n].Cells["Colour"].Value = row1.Cells["اللون"].Value;
                    dataGridView2.Rows[n].Cells["Size"].Value = row1.Cells["المقاس"].Value;
                    dataGridView2.Rows[n].Cells["Sort"].Value = row1.Cells["الفرز"].Value;
                    dataGridView2.Rows[n].Cells["Description"].Value = row1.Cells["الوصف"].Value;
                    if ((totalMeter + returnedQuantity) == Convert.ToDouble(row1.Cells["الكمية"].Value))
                    {
                        dataGridView2.Rows[n].Cells["Returned"].Value = "نعم";
                    }
                    else if ((totalMeter + returnedQuantity) < Convert.ToDouble(row1.Cells["الكمية"].Value))
                    {
                        dataGridView2.Rows[n].Cells["Returned"].Value = "جزء";
                    }

                    dataGridView2.Rows[n].Cells["CustomerBill_ID"].Value = row1.Cells["CustomerBill_ID"].Value;

                    listOfRow2In.Add(n);
                    double totalAD = 0;
                    foreach (DataGridViewRow item in dataGridView2.Rows)
                    {
                        totalAD += Convert.ToDouble(item.Cells["totalAD"].Value);
                    }
                    labTotalReturnBillAD.Text = totalAD.ToString();
                }
                else
                {
                    MessageBox.Show("برجاء اختيار عنصر والتاكد انه لم يتم اضافتة من قبل");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCreateReturnBill_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (dataGridView2.Rows.Count > 0 && (comClient.Text != "" || comCustomer.Text != ""))
                {
                    string query = "select Branch_BillNumber from customer_return_bill where Branch_ID=" + txtBranchID.Text+ " order by CustomerReturnBill_ID desc limit 1";
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    int Branch_BillNumber = 1;
                    if (com.ExecuteScalar() != null)
                    {
                        Branch_BillNumber = Convert.ToInt16(com.ExecuteScalar());
                        Branch_BillNumber++;
                    }
                    //Type_Buy
                    query = "insert into customer_return_bill (Branch_BillNumber,Branch_ID,Customer_ID,Client_ID,Date,TotalCostAD,ReturnInfo) values (@Branch_BillNumber,@Branch_ID,@Customer_ID,@Client_ID,@Date,@TotalCostAD,@ReturnInfo)";
                    com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@Branch_BillNumber", MySqlDbType.Int16);
                    com.Parameters["@Branch_BillNumber"].Value = Branch_BillNumber;
                    com.Parameters.Add("@Branch_ID", MySqlDbType.Int16);
                    com.Parameters["@Branch_ID"].Value = EmpBranchId;
                    //com.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16);
                    //com.Parameters["@CustomerBill_ID"].Value = customerBillId;
                    if (comCustomer.Text != "")
                    {
                        com.Parameters.Add("@Customer_ID", MySqlDbType.Int16);
                        com.Parameters["@Customer_ID"].Value = Convert.ToInt16(comCustomer.SelectedValue.ToString());
                    }
                    else
                    {
                        com.Parameters.Add("@Customer_ID", MySqlDbType.Int16);
                        com.Parameters["@Customer_ID"].Value = null;
                    }
                    if (comClient.Text != "")
                    {
                        com.Parameters.Add("@Client_ID", MySqlDbType.Int16);
                        com.Parameters["@Client_ID"].Value = Convert.ToInt16(comClient.SelectedValue.ToString());
                    }
                    else
                    {
                        com.Parameters.Add("@Client_ID", MySqlDbType.Int16);
                        com.Parameters["@Client_ID"].Value = null;
                    }
                    /*query = "select customer_bill.Type_Buy from customer_bill where customer_bill.CustomerBill_ID=" + customerBillId;
                    MySqlCommand com1 = new MySqlCommand(query, dbconnection);
                    MySqlDataReader dr1 = com1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            com.Parameters.Add("@Type_Buy", MySqlDbType.VarChar);
                            com.Parameters["@Type_Buy"].Value = dr1["Type_Buy"].ToString();
                        }
                        dr1.Close();
                    }
                    else
                    {
                        com.Parameters.Add("@Type_Buy", MySqlDbType.VarChar);
                        com.Parameters["@Type_Buy"].Value = null;
                    }*/
                    com.Parameters.Add("@Date", MySqlDbType.Date);
                    com.Parameters["@Date"].Value = DateTime.Now.Date;
                    com.Parameters.Add("@ReturnInfo", MySqlDbType.VarChar);
                    com.Parameters["@ReturnInfo"].Value = txtInfo.Text;
                    com.Parameters.Add("@TotalCostAD", MySqlDbType.Decimal);
                    com.Parameters["@TotalCostAD"].Value = Convert.ToDouble(labTotalReturnBillAD.Text);
                    com.ExecuteNonQuery();

                    query = "select CustomerReturnBill_ID from customer_return_bill order by CustomerReturnBill_ID desc limit 1";
                    com = new MySqlCommand(query, dbconnection);
                    int id = Convert.ToInt16(com.ExecuteScalar());

                    query = "insert into customer_return_bill_details (CustomerReturnBill_ID,Data_ID,Type,TotalMeter,PriceAD,TotalAD,SellDiscount,CustomerBill_ID)values (@CustomerReturnBill_ID,@Data_ID,@Type,@TotalMeter,@PriceAD,@TotalAD,@SellDiscount,@CustomerBill_ID)";
                    com = new MySqlCommand(query, dbconnection);
                    com.Parameters.Add("@CustomerReturnBill_ID", MySqlDbType.Int16);
                    com.Parameters.Add("@Data_ID", MySqlDbType.Int16);
                    com.Parameters.Add("@Type", MySqlDbType.VarChar);
                    com.Parameters.Add("@TotalMeter", MySqlDbType.Decimal);
                    com.Parameters.Add("@PriceAD", MySqlDbType.Decimal);
                    com.Parameters.Add("@TotalAD", MySqlDbType.Decimal);
                    com.Parameters.Add("@SellDiscount", MySqlDbType.Decimal);
                    com.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16);
                    foreach (DataGridViewRow row2 in dataGridView2.Rows)
                    {
                        if (row2.Cells[0].Value != null)
                        {
                            com.Parameters["@CustomerReturnBill_ID"].Value = id;
                            com.Parameters["@Data_ID"].Value = Convert.ToInt16(row2.Cells[0].Value);
                            com.Parameters["@Type"].Value = row2.Cells["Type"].Value;
                            com.Parameters["@TotalMeter"].Value = Convert.ToDouble(row2.Cells["Quantity"].Value);
                            com.Parameters["@PriceAD"].Value = Convert.ToDouble(row2.Cells["priceAD"].Value);
                            com.Parameters["@TotalAD"].Value = Convert.ToDouble(row2.Cells["totalAD"].Value);
                            com.Parameters["@SellDiscount"].Value = Convert.ToDouble(row2.Cells["Discount"].Value);
                            com.Parameters["@CustomerBill_ID"].Value = Convert.ToInt16(row2.Cells["CustomerBill_ID"].Value);
                            com.ExecuteNonQuery();
                            
                            string queryf = "update product_bill set Returned='" + row2.Cells["Returned"].Value + "' where CustomerBill_ID=" + row2.Cells["CustomerBill_ID"].Value + " and Data_ID=" + Convert.ToInt16(row2.Cells[0].Value) + " and Type='" + row2.Cells["Type"].Value + "'";
                            MySqlCommand c = new MySqlCommand(queryf, dbconnection);
                            c.ExecuteNonQuery();
                        }
                    }

                    for (int i = 0; i < listBoxControlCustomerBill.Items.Count; i++)
                    {
                        string queryf2 = "insert into customerbill_return (CustomerReturnBill_ID,CustomerBill_ID)values (@CustomerReturnBill_ID,@CustomerBill_ID)";
                        MySqlCommand c2 = new MySqlCommand(queryf2, dbconnection);
                        c2.Parameters.Add("@CustomerReturnBill_ID", MySqlDbType.Int16);
                        c2.Parameters["@CustomerReturnBill_ID"].Value = id;
                        c2.Parameters.Add("@CustomerBill_ID", MySqlDbType.Int16);
                        c2.Parameters["@CustomerBill_ID"].Value = Convert.ToInt16(listBoxControlCustomerBill.Items[i].ToString());
                        c2.ExecuteNonQuery();
                    }

                    IncreaseProductQuantity(id);
                    clrearAll();
                    clear(tableLayoutPanel1);
                    //returnBillReport.DisplayBillNumber();
                    //returnBillReport.DisplayBills();
                }
                else
                {
                    MessageBox.Show("return bill is empty insert row");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    int dgv2Index = dataGridView2.SelectedCells[0].RowIndex;
                    for (int i = 0; i < listOfRow2In.Count; i++)
                    {
                        if (listOfRow2In[i] == dgv2Index)
                        {
                            //myRows.RemoveAt(dgv2Index);
                            dataGridView2.Rows.Remove(dataGridView2.Rows[dgv2Index]);
                            dataGridView1.Rows[addedRecordIDs[i] - 1].DefaultCellStyle.BackColor = Color.White;
                            addedRecordIDs[i] = 0;
                            listOfRow2In.Remove(dgv2Index);
                            recordCount--;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void comBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (comBranchLoaded)
                {
                    txtBranchID.Text = comBranch.SelectedValue.ToString();
                    if (txtCustomerID.Text != "")
                    {
                        DisplayBillNumber(Convert.ToInt16(comCustomer.SelectedValue), Convert.ToInt16(comClient.SelectedValue));
                    }
                    else
                    {
                        DisplayBillNumber(0, Convert.ToInt16(comClient.SelectedValue));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBranchID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dbconnection.Close();
                    string query = "select Branch_Name from branch where Branch_ID=" + txtBranchID.Text;
                    MySqlCommand comand = new MySqlCommand(query, dbconnection);
                    dbconnection.Open();
                    string Branch_Name = comand.ExecuteScalar().ToString();
                    dbconnection.Close();
                    comBranch.Text = Branch_Name;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        

        //function
        //display bill number for selected customer/client
        public void DisplayBillNumber(int customerID, int clientID)
        {
            if (txtBranchID.Text != "")
            {
                labBillNumber.Visible = true;
                comBillNumber.Visible = true;
                string strQuery = "";
                try
                {
                    dbconnection.Open();

                    if (clientID > 0)
                    {

                        strQuery = " and Client_ID = " + clientID + "";
                    }
                    if (customerID > 0)
                    {

                        strQuery += " and Customer_ID = " + customerID + "";
                    }
                    comBillNumber.DataSource = null;
                    flag = false;
                    string query = "";

                    if (strQuery != "")
                    {
                        query = "select Branch_BillNumber,CustomerBill_ID from customer_bill where Branch_ID=" + txtBranchID.Text + strQuery;
                    }
                    else
                    {
                        query = "select Branch_BillNumber,CustomerBill_ID from customer_bill where Branch_ID=" + txtBranchID.Text;
                    }
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comBillNumber.DataSource = dt;
                    comBillNumber.DisplayMember = dt.Columns["Branch_BillNumber"].ToString();
                    comBillNumber.ValueMember= dt.Columns["CustomerBill_ID"].ToString();
                    comBillNumber.Text = "";

                    flag = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
        }

        //clear all fields
        private void clrearAll()
        {
            try
            {
                radClient.Checked = false;
                radCon.Checked = false;
                radEng.Checked = false;
                comBranch.Text = "";
                txtBranchID.Text = "";

                listBoxControlBills.Items.Clear();
                listBoxControlCustomerBill.Items.Clear();

                labBillDate.Text = labBillTotalCostAD.Text = labTotalReturnBillAD.Text = labTotalAD.Text = labReturnedQuantity.Text = "";

                dataGridView1.DataSource = null;
                dataGridView2.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void clear(Control tlp)
        {
            foreach (Control co in tlp.Controls)
            {
                if (co is Panel || co is TableLayoutPanel)
                {
                    foreach (Control item in co.Controls)
                    {
                        if (item is System.Windows.Forms.ComboBox)
                        {
                            item.Text = "";
                        }
                        else if (item is TextBox)
                        {
                            item.Text = "";
                        }
                    }
                }
            }
        }

        private void btnNewChooes_Click(object sender, EventArgs e)
        {
            try
            {
                listBoxControlBills.Items.Clear();
                listBoxControlCustomerBill.Items.Clear();
                dataGridView1.DataSource = null;
                dataGridView2.Rows.Clear();
                comBillNumber.Text = "";
                txtCode.Text = "";
                txtPriceAD.Text = "";
                txtTotalMeter.Text = "";
                labTotalAD.Text = "";
                labReturnedQuantity.Text = "";
                labBillTotalCostAD.Text = "";
                labBillDate.Text = "";
                labTotalReturnBillAD.Text = "";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        bool IsAdded(DataGridViewRow row1)
        {
            foreach (DataGridViewRow item in dataGridView2.Rows)
            {
                if ((row1.Cells["Data_ID"].Value.ToString() == item.Cells["Data_ID"].Value.ToString()) && (row1.Cells["الفئة"].Value.ToString() == item.Cells["Type"].Value.ToString()) && (row1.Cells["CustomerBill_ID"].Value.ToString() == item.Cells["CustomerBill_ID"].Value.ToString()))
                    return true;
            }
            return false;
        }
        
        //return quantity to store
        public void IncreaseProductQuantity(int billNumber)
        {
            connectionReader.Open();
            connectionReader2.Open();
            string q;
            int id;
            bool flag = false;
            double storageQ, productQ;
            string query = "select Data_ID,Type,TotalMeter from customer_return_bill_details where CustomerReturnBill_ID=" + billNumber;
            MySqlCommand com = new MySqlCommand(query, connectionReader);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                #region بند
                if (dr["Type"].ToString() == "بند")
                {
                    string query2 = "select Storage_ID,Total_Meters from storage where Data_ID=" + dr["Data_ID"].ToString() + " and Type='" + dr["Type"].ToString() + "'";
                    MySqlCommand com2 = new MySqlCommand(query2, connectionReader2);
                    MySqlDataReader dr2 = com2.ExecuteReader();
                    while (dr2.Read())
                    {

                        storageQ = Convert.ToDouble(dr2["Total_Meters"]);
                        productQ = Convert.ToDouble(dr["TotalMeter"]);

                        storageQ += productQ;
                        id = Convert.ToInt16(dr2["Storage_ID"]);
                        q = "update storage set Total_Meters=" + storageQ + " where Storage_ID=" + id;
                        MySqlCommand comm = new MySqlCommand(q, dbconnection);
                        comm.ExecuteNonQuery();
                        flag = true;
                        break;

                    }
                    dr2.Close();
                }
                #endregion

                #region طقم
                if (dr["Type"].ToString() == "طقم")
                {
                    string query2 = "select Storage_ID,Total_Meters from storage where Set_ID=" + dr["Data_ID"].ToString() + " and Type='" + dr["Type"].ToString() + "'";
                    MySqlCommand com2 = new MySqlCommand(query2, connectionReader2);
                    MySqlDataReader dr2 = com2.ExecuteReader();
                    while (dr2.Read())
                    {

                        storageQ = Convert.ToDouble(dr2["Total_Meters"]);
                        productQ = Convert.ToDouble(dr["TotalMeter"]);

                        storageQ += productQ;
                        id = Convert.ToInt16(dr2["Storage_ID"]);
                        q = "update storage set Total_Meters=" + storageQ + " where Storage_ID=" + id;
                        MySqlCommand comm = new MySqlCommand(q, dbconnection);
                        comm.ExecuteNonQuery();
                        flag = true;
                        break;

                    }
                    dr2.Close();
                }
                #endregion

                #region StorageTaxes
                string query3 = "select StorageTaxesID,Total_Meters from storage_taxes where Data_ID=" + dr["Data_ID"].ToString() + " and Type='" + dr["Type"].ToString() + "'";
                MySqlCommand com3 = new MySqlCommand(query3, connectionReader2);
                MySqlDataReader dr3 = com3.ExecuteReader();
                while (dr3.Read())
                {

                    storageQ = Convert.ToDouble(dr3["Total_Meters"]);
                    productQ = Convert.ToDouble(dr["TotalMeter"]);

                    storageQ += productQ;
                    id = Convert.ToInt16(dr3["StorageTaxesID"]);
                    q = "update storage_taxes set Total_Meters=" + storageQ + " where StorageTaxesID=" + id;
                    MySqlCommand comm = new MySqlCommand(q, dbconnection);
                    comm.ExecuteNonQuery();
                    flag = true;
                    break;

                }
                dr3.Close(); 
                #endregion

                if (!flag)
                {
                    MessageBox.Show(dr["Data_ID"].ToString() + "not valid in store");
                }
                flag = false;
            }
            dr.Close();
            
            connectionReader2.Close();
            connectionReader.Close();
        }
    }
}
