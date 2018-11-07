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
    public partial class customerBills : Form
    {
        MySqlConnection dbconnection;
        MySqlConnection dbconnection1;
        private string Customer_Type;
        private bool loaded = false;

        public customerBills()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection1 = new MySqlConnection(connection.connectionString);
            labelClient.Visible = false;
            labelEng.Visible = false;//label of مهندس/مقاول
            comClient.Visible = false;
            comEngCon.Visible = false;
            txtCustomerID.Visible = false;
            txtClientID.Visible = false;

        }
        //check type of customer if engineer,client or contract 
        private void radiotype_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            Customer_Type = radio.Text;
            comClient.Text = "";
            txtClientID.Text = "";
            comEngCon.Text = "";
            txtCustomerID.Text = "";
            loaded = false; //this is flag to prevent action of SelectedValueChanged event until datasource fill combobox
            try
            {
                if (Customer_Type == "عميل")
                {
                    labelEng.Visible = false;
                    comEngCon.Visible = false;
                    txtCustomerID.Visible = false;
                    labelClient.Visible = true;
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
                    labelEng.Visible = true;
                    comEngCon.Visible = true;
                    txtCustomerID.Visible = true;
                    labelClient.Visible = false;
                    comClient.Visible = false;
                    txtClientID.Visible = false;

                    string query = "select * from customer where Customer_Type='" + Customer_Type + "'";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comEngCon.DataSource = dt;
                    comEngCon.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comEngCon.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comEngCon.Text = "";
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //when select customer(مهندس,مقاول)display in comCustomer the all clients of th customer 
        private void comEngCon_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    loaded = false;
                    txtCustomerID.Text = comEngCon.SelectedValue.ToString();
                    labelClient.Visible = true;
                    comClient.Visible = true;
                    txtClientID.Visible = true;

                    string query = "select * from customer where Customer_ID in(select Client_ID from custmer_client where Customer_ID=" + comEngCon.SelectedValue + ")";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comClient.DataSource = dt;
                    comClient.DisplayMember = dt.Columns["Customer_Name"].ToString();
                    comClient.ValueMember = dt.Columns["Customer_ID"].ToString();
                    comClient.Text = "";
                    txtClientID.Text = "";
                    loaded = true;

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
                                query = "select Customer_Name from customer where Customer_ID=" + txtCustomerID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comEngCon.Text = Name;
                                    comEngCon.SelectedValue = txtCustomerID.Text;

                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtClientID":
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
                dbconnection.Open();
                dbconnection1.Open();
                dataGridView1.Rows.Clear();
             
                displayBill();
                double totalBill=0, TotalReturn = 0,rest=0;

                foreach (DataGridViewRow row1 in dataGridView1.Rows)
                {
                    totalBill += Convert.ToDouble(row1.Cells[0].Value);
                    TotalReturn += Convert.ToDouble(row1.Cells[1].Value);
                    rest = totalBill - TotalReturn;
                }

                labTotalBillCost.Text = totalBill.ToString();
                labTotalReturnCost.Text = TotalReturn.ToString();
                labRest.Text = rest.ToString();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
            dbconnection1.Close();
        }

        //function
        // display Customer bills
        public void displayBill()
        {
        
            string query = "";
            if (txtClientID.Text != "" && txtCustomerID.Text != "")
            {
                query = "select sum(TotalCostAD),sum(Total_CostAD),customer_bill.Customer_ID,customer_bill.Client_ID from customer_bill,customer_return_bill where customer_bill.Client_ID='" + txtClientID.Text + "' and customer_bill.Customer_ID='" + txtCustomerID.Text + "' group by customer_bill.Client_ID,customer_bill.Customer_ID";
            }
            else if (txtClientID.Text == "" && txtCustomerID.Text != "")
            {
                query = "select sum(TotalCostAD),sum(Total_CostAD),customer_bill.Customer_ID,customer_bill.Client_ID from customer_bill,customer_return_bill where  customer_bill.Customer_ID='" + txtCustomerID.Text + "' group by customer_bill.Customer_ID";
            }
            else if (txtClientID.Text != "" && txtCustomerID.Text == "")
            {
                query = "select sum(TotalCostAD),sum(Total_CostAD),customer_bill.Customer_ID,customer_bill.Client_ID from customer_bill,customer_return_bill where customer_bill.Client_ID='" + txtClientID.Text + "' and customer_bill.Customer_ID is null group by customer_bill.Client_ID";
            }
            else
            {
                query = "select sum(TotalCostAD),sum(Total_CostAD),customer_bill.Customer_ID,customer_bill.Client_ID from customer_bill,customer_return_bill  group by customer_bill.Client_ID";
            }
            MySqlCommand com = new MySqlCommand(query, dbconnection1);
            MySqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = dr["sum(Total_CostAD)"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = dr["sum(TotalCostAD)"].ToString();
                if (dr["Customer_ID"].ToString() != "")
                {
                    String q = "select Customer_Name from customer where Customer_ID=" + dr["Customer_ID"].ToString();
                    MySqlCommand com1 = new MySqlCommand(q, dbconnection);
                    string Customer_Name = com1.ExecuteScalar().ToString();
                    dataGridView1.Rows[n].Cells[2].Value = Customer_Name;
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "";
                }
                if (dr["Client_ID"].ToString() != "")
                {
                    String q = "select Customer_Name from customer where Customer_ID=" + dr["Client_ID"].ToString();
                    MySqlCommand com1 = new MySqlCommand(q, dbconnection);
                    string Customer_Name = com1.ExecuteScalar().ToString();
                    dataGridView1.Rows[n].Cells[3].Value = Customer_Name;
                }
                else
                {
                    dataGridView1.Rows[n].Cells[3].Value = "";
                }

            }
            dr.Close();
        }
        
    }
}
