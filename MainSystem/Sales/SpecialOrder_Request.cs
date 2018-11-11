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
    public partial class SpecialOrder_Request : Form
    {
        MySqlConnection dbconnection;
        bool loaded = false;
        string Customer_Type;
        DataGridViewRow row;
        private int[] addedRecordIDs;
        int recordCount = 0;
        private bool Added = false;
        int id;

        public SpecialOrder_Request()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            addedRecordIDs = new int[100];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
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
                txtType.Text = "";

                query = "select * from factory";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";
                txtFactory.Text = "";

                query = "select * from groupo";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comGroup.DataSource = dt;
                comGroup.DisplayMember = dt.Columns["Group_Name"].ToString();
                comGroup.ValueMember = dt.Columns["Group_ID"].ToString();
                comGroup.Text = "";
                txtGroup.Text = "";

                query = "select * from product";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comProduct.DataSource = dt;
                comProduct.DisplayMember = dt.Columns["Product_Name"].ToString();
                comProduct.ValueMember = dt.Columns["Product_ID"].ToString();
                comProduct.Text = "";
                txtProduct.Text = "";

                query = "select * from delegate";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comDelegate.DataSource = dt;
                comDelegate.DisplayMember = dt.Columns["Delegate_Name"].ToString();
                comDelegate.ValueMember = dt.Columns["Delegate_ID"].ToString();
                comDelegate.Text = "";

                query = "select * from supplier";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comSupplier.DataSource = dt;
                comSupplier.DisplayMember = dt.Columns["Supplier_Name"].ToString();
                comSupplier.ValueMember = dt.Columns["Supplier_ID"].ToString();
                comSupplier.Text = "";

                query = "select * from branch";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comBranch.DataSource = dt;
                comBranch.DisplayMember = dt.Columns["Branch_Name"].ToString();
                comBranch.ValueMember = dt.Columns["Branch_ID"].ToString();
                comBranch.Text = "";
                txtBranchID.Text = "";

                query = "select * from store";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
                txtStoreID.Text = "";

                groupBox2.Visible = false;
                loaded = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        //check type of customer if engineer,client or contract 
        private void radiotype_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            Customer_Type = radio.Text;
            groupBox2.Visible = true;
            loaded = false;//this is flag to prevent action of SelectedValueChanged event until datasource fill combobox
            try
            {
                if (Customer_Type == "عميل")
                {
                    label3.Visible = false;
                    comEngCon.Visible = false;
                    txtEngConID.Visible = false;
                    label9.Visible = true;
                    comCustomer.Visible = true;
                    txtCustomerID.Visible = true;

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
                else
                {
                    label3.Visible = true;
                    comEngCon.Visible = true;
                    txtEngConID.Visible = true;
                    label9.Visible = false;
                    comCustomer.Visible = false;
                    txtCustomerID.Visible = false;


                    string query = "select * from customer where Customer_Type='" + Customer_Type + "'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comEngCon.DataSource = dt;
                    comEngCon.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comEngCon.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comEngCon.Text = "";
                    txtEngConID.Text = "";


                }


                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        private void comCustomerType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    txtCustomerID.Text = comCustomer.SelectedValue.ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //when select customer(مهندس,مقاول)display in comCustomer the all clients of th customer 
        private void comEngCon_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                txtEngConID.Text = comEngCon.SelectedValue.ToString();

                label9.Visible = true;
                comCustomer.Visible = true;
                txtCustomerID.Visible = true;

                try
                {
                    string query = "select * from customer where Customer_ID in(select Client_ID from custmer_client where Customer_ID=" + comEngCon.SelectedValue + ")";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comCustomer.DataSource = dt;
                    comCustomer.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comCustomer.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comCustomer.Text = "";

                    txtCustomerID.Text = "";
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
        private void comBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ComboBox comBox = (ComboBox)sender;

                switch (comBox.Name)
                {
                    case "comType":
                        txtType.Text = comType.SelectedValue.ToString();
                        break;
                    case "comFactory":
                        txtFactory.Text = comFactory.SelectedValue.ToString();
                        break;
                    case "comGroup":
                        txtGroup.Text = comGroup.SelectedValue.ToString();
                        break;
                    case "comProduct":
                        txtProduct.Text = comProduct.SelectedValue.ToString();
                        break;
                    case "comDelegate":
                        txtDelegate.Text = comDelegate.SelectedValue.ToString();
                        break;
                    case "comSupplier":
                        txtSupplier.Text = comSupplier.SelectedValue.ToString();
                        break;
                    case "comStore":
                        txtStoreID.Text = comStore.SelectedValue.ToString();
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
                            case "txtType":
                                query = "select Type_Name from type where Type_ID='" + txtType.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comType.Text = Name;
                                    txtFactory.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtFactory":
                                query = "select Factory_Name from factory where Factory_ID='" + txtFactory.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comFactory.Text = Name;
                                    txtGroup.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtGroup":
                                query = "select Group_Name from groupo where Group_ID='" + txtGroup.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comGroup.Text = Name;
                                    txtProduct.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtProduct":
                                query = "select Product_Name from product where Product_ID='" + txtProduct.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comProduct.Text = Name;
                                    txtType.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtDelegate":
                                query = "select Delegate_Name from delegate where Delegate_ID=" + txtDelegate.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comDelegate.Text = Name;
                                    
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtCustomerID":
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
                            case "txtEngConID":
                                query = "select Customer_Name from customer where Customer_ID=" + txtEngConID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comEngCon.Text = Name;
                                    comEngCon.SelectedValue = txtEngConID.Text;
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtSupplier":
                                query = "select Supplier_Name from supplier where Supplier_ID=" + txtSupplier.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comSupplier.Text = Name;
                                    comSupplier.SelectedValue = txtSupplier.Text;
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                                
                            case "txtBranchID":
                                query = "select Branch_Name from branch where Branch_ID=" + txtBranchID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comBranch.Text = Name;
                                    comBranch.SelectedValue = txtBranchID.Text;
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtStoreID":
                                query = "select Store_Name from branch where Store_ID=" + txtBranchID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comStore.Text = Name;
                                    comStore.SelectedValue = txtStoreID.Text;
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
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string q1, q2, q3, q4;
            if (txtType.Text == "")
            {
                q1 = "select Type_ID from data";
            }
            else
            {
                q1 = txtType.Text;
            }
            if (txtFactory.Text == "")
            {
                q2 = "select Factory_ID from data";
            }
            else
            {
                q2 = txtFactory.Text;
            }
            if (txtProduct.Text == "")
            {
                q3 = "select Product_ID from data";
            }
            else
            {
                q3 = txtProduct.Text;
            }
            if (txtGroup.Text == "")
            {
                q4 = "select Group_ID from data";
            }
            else
            {
                q4 = txtGroup.Text;
            }

            string query = "select distinct data.Code as 'كود' , type.Type_Name as 'النوع', factory.Factory_Name as 'المصنع' ,groupo.Group_Name as 'المجموعة', product.Product_Name as 'المنتج' ,data.Colour as 'لون', data.Size as 'المقاس', data.Sort as 'الفرز',data.Classification as 'التصنيف', data.Description as 'الوصف'  from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID  where  data.Type_ID IN(" + q1 + ") and  data.Factory_ID  IN(" + q2 + ") and data.Group_ID IN (" + q4 + ") ";

            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];

            string value = row.Cells[0].Value.ToString();
            txtCode.Text = value;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            double x = 0;
            if (!Added && row != null && double.TryParse(txtPrice.Text,out x))
            {
                addedRecordIDs[recordCount] = dataGridView1.SelectedCells[0].RowIndex + 1;
                dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DefaultCellStyle.BackColor = Color.Silver;
                recordCount++;
                int n = dataGridView2.Rows.Add();
                foreach (DataGridViewColumn item in dataGridView1.Columns)
                {
                    dataGridView2.Rows[n].Cells[item.Index].Value = dataGridView1.Rows[row.Index].Cells[item.Index].Value.ToString();

                }
                dataGridView2.Rows[n].Cells[10].Value = txtTotalMeters.Text;
                dataGridView2.Rows[n].Cells[11].Value = x;

                //clear fields
                txtCode.Text = txtPrice.Text = txtTotalMeters.Text = "";
            }
            else
            {
                MessageBox.Show("insert all values with correct format");
                return;
            }
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            int branchId = 0;
            if ( comDelegate.Text != "" && txtEmployee.Text!=""&&comBranch.Text!=""&& int.TryParse(txtBranchID.Text,out branchId)&&comStore.Text!=""&&txtStoreID.Text!="")
            {
                dbconnection.Open();

                int branchBillNumber = 1;
                string query = "select BranchBillNumber from requests where Branch_ID="+branchId+" order by BranchBillNumber desc limit 1";
                MySqlCommand com = new MySqlCommand(query,dbconnection);
                if (com.ExecuteScalar()!= null)
                {
                    branchBillNumber = Convert.ToInt16(com.ExecuteScalar())+1;
                }
                try
                {
                    query = "insert into requests (Supplier_ID,Supplier_Name,Store_ID,Store_Name,Branch_ID,Branch_Name,BranchBillNumber,Customer_ID,Client_ID,Delegate_ID,Employee_Name,Request_Date,Recive_Date,PaidMoney)values(@Supplier_ID,@Supplier_Name,@Store_ID,@Store_Name,@Branch_ID,@Branch_Name,@BranchBillNumber,@Customer_ID,@Client_ID,@Delegate_ID,@Employee_Name,@Request_Date,@Recive_Date,@PaidMoney)";
                    com = new MySqlCommand(query,dbconnection);
                    com.Parameters.Add("@Client_ID", MySqlDbType.Int16);
                    com.Parameters["@Client_ID"].Value = comCustomer.SelectedValue;
                    com.Parameters.Add("@Customer_ID", MySqlDbType.Int16);
                    com.Parameters["@Customer_ID"].Value = comEngCon.SelectedValue;
                    com.Parameters.Add("@Delegate_ID", MySqlDbType.Int16);
                    com.Parameters["@Delegate_ID"].Value = comDelegate.SelectedValue;
                    com.Parameters.Add("@Employee_Name", MySqlDbType.VarChar);
                    com.Parameters["@Employee_Name"].Value = txtEmployee.Text;
                    com.Parameters.Add("@Request_Date", MySqlDbType.Date);
                    com.Parameters["@Request_Date"].Value = dateTimePicker1.Value.Date;             
                    com.Parameters.Add("@Recive_Date", MySqlDbType.Date);
                    com.Parameters["@Recive_Date"].Value = dateTimePicker2.Value.Date;
                    int res2;
                    if (int.TryParse(txtSupplier.Text, out res2))
                    {
                        com.Parameters.Add("@Supplier_ID", MySqlDbType.Int16);
                        com.Parameters["@Supplier_ID"].Value = res2;
                    }
                    else
                    {
                        MessageBox.Show("insert correct value please :)");
                        dbconnection.Close();
                        return;
                    }
                    com.Parameters.Add("@Supplier_Name", MySqlDbType.VarChar);
                    com.Parameters["@Supplier_Name"].Value = comSupplier.Text;
                    int res1;
                    if (int.TryParse(txtStoreID.Text, out res1))
                    {
                        com.Parameters.Add("@Store_ID", MySqlDbType.Int16);
                        com.Parameters["@Store_ID"].Value = res1;
                    }
                    else
                    {
                        MessageBox.Show("insert correct value please :)");
                        dbconnection.Close();
                        return;
                    }
                    com.Parameters.Add("@Store_Name", MySqlDbType.VarChar);
                    com.Parameters["@Store_Name"].Value = comStore.Text;
                    int res;
                    if (int.TryParse(txtBranchID.Text, out res))
                    {
                        com.Parameters.Add("@Branch_ID", MySqlDbType.Int16);
                        com.Parameters["@Branch_ID"].Value = res;
                    }
                    else
                    {
                        MessageBox.Show("insert correct value please :)");
                        dbconnection.Close();
                        return;
                    }
                    com.Parameters.Add("@Branch_Name", MySqlDbType.VarChar);
                    com.Parameters["@Branch_Name"].Value = comBranch.Text;
                    com.Parameters.Add("@BranchBillNumber", MySqlDbType.Int16);
                    com.Parameters["@BranchBillNumber"].Value = branchBillNumber;
                    double y = 0;

                    if ( double.TryParse(txtPaidMoney.Text, out y))
                    {
                       
                        com.Parameters.Add("@PaidMoney", MySqlDbType.Double);
                        com.Parameters["@PaidMoney"].Value = y;
                    }
                    else
                    {
                        MessageBox.Show("insert correct value please :)");
                        dbconnection.Close();
                        return;
                    }
                    com.ExecuteNonQuery();

                    query = "select Request_ID from requests order by Request_ID desc limit 1";
                    com = new MySqlCommand(query, dbconnection);

                    if (com.ExecuteScalar() != null)
                    {
                        id = (int)com.ExecuteScalar();


                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {

                            query = "insert into request_details (Request_ID,Code,Quantity,Price,Supplier_ID) values (@Request_ID,@Code,@Quantity,@Price,@Supplier_ID)";
                            com = new MySqlCommand(query, dbconnection);

                            com.Parameters.Add("@Request_ID", MySqlDbType.Int16);
                            com.Parameters["@Request_ID"].Value = id;
                            com.Parameters.Add("@Code", MySqlDbType.VarChar);
                            com.Parameters["@Code"].Value = row.Cells["Code"].Value;
                            com.Parameters.Add("@Quantity", MySqlDbType.Double);
                            com.Parameters["@Quantity"].Value = row.Cells["Total_Meter"].Value; 
                            com.Parameters.Add("@Supplier_ID", MySqlDbType.VarChar);
                            com.Parameters["@Supplier_ID"].Value = comSupplier.SelectedValue;
                            com.Parameters.Add("@Price", MySqlDbType.Double);
                            com.Parameters["@Price"].Value = row.Cells["Price"].Value;
                          
                            com.ExecuteNonQuery();
                            
                        }
                        MessageBox.Show("Done");
                        dbconnection.Close();

                        //لعرض التقرير
                        if (comDelegate.Text != "" && txtEmployee.Text != "" && txtPaidMoney.Text != "")
                        {
                            DataTable dt = new DataTable();
                            for (int i = 0; i < dataGridView2.Columns.Count; i++)
                            {
                                dt.Columns.Add(dataGridView2.Columns[i].Name.ToString());
                            }
                            for (int i = 0; i < dataGridView2.Rows.Count; i++)
                            {
                                DataRow dr = dt.NewRow();
                                for (int j = 0; j < dataGridView2.Columns.Count; j++)
                                {
                                    dr[dataGridView2.Columns[j].Name.ToString()] = dataGridView2.Rows[i].Cells[j].Value;
                                }

                                dt.Rows.Add(dr);
                            }

                            if (comCustomer.Text != "" && txtCustomerID.Text != "")
                            {
                                dbconnection.Open();
                                string q = "select Customer_Phone from customer where Customer_ID=" + txtCustomerID.Text;
                                MySqlCommand comand = new MySqlCommand(q, dbconnection);
                                string phone = comand.ExecuteScalar().ToString();
                                dbconnection.Close();

                                dbconnection.Open();
                                q = "select Customer_Address from customer where Customer_ID=" + txtCustomerID.Text;
                                comand = new MySqlCommand(q, dbconnection);
                                string address = comand.ExecuteScalar().ToString();
                                dbconnection.Close();

                                /*Form1_CrystalReport f = new Form1_CrystalReport(comStore.Text,comBranch.Text, branchBillNumber, comCustomer.Text, phone, address, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, comDelegate.Text, txtPaidMoney.Text, txtEmployee.Text, dt);
                                f.Show();
                                this.Hide();*/
                            }
                            else if (comEngCon.Text != "" && txtEngConID.Text != "")
                            {
                                dbconnection.Open();
                                string q = "select Customer_Phone from customer where Customer_ID=" + txtEngConID.Text;
                                MySqlCommand comand = new MySqlCommand(q, dbconnection);
                                string phone = comand.ExecuteScalar().ToString();
                                dbconnection.Close();

                                dbconnection.Open();
                                q = "select Customer_Address from customer where Customer_ID=" + txtEngConID.Text;
                                comand = new MySqlCommand(q, dbconnection);
                                string address = comand.ExecuteScalar().ToString();
                                dbconnection.Close();

                                /*Form1_CrystalReport f = new Form1_CrystalReport(comStore.Text,comBranch.Text, branchBillNumber, comEngCon.Text, phone, address, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, comDelegate.Text, txtPaidMoney.Text, txtEmployee.Text, dt);
                                f.Show();
                                this.Hide();*/
                            }
                            else
                            {
                                MessageBox.Show("fill requird fields");
                            }
                        }
                        else
                        {
                            MessageBox.Show("fill requird fields");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dbconnection.Close();
            }
            else
            {
                MessageBox.Show("fill requird fields");
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Environment.Exit(0);
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
                if (loaded)
                    txtBranchID.Text = comBranch.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
