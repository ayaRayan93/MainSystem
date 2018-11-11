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

namespace MainSystem.Sales.accounting
{
    public partial class CustomerTaswaya : Form
    {
        MySqlConnection dbconnection;
        MySqlConnection dbconnection1;
        MainForm saleMainForm;
        private string Customer_Type;
        private bool loaded = false;
        double safay = -1;
        int ClientID = -1;
        string ClientName = "";
        string customerID="", clientID="";
        public CustomerTaswaya()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
        public CustomerTaswaya(string customerType, string customerID, string clientID)
        {
            try
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
                this.customerID = customerID;
                this.clientID = clientID;
                this.Customer_Type = customerType;
                dbconnection.Open();
                setCustomerType();
                dbconnection.Open();
                setIDs();
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
            comClient.Text = "";
            txtClientID.Text = "";
            comEngCon.Text = "";
            txtCustomerID.Text = "";

            loaded = false; //this is flag to prevent action of SelectedValueChanged event until datasource fill combobox
            try
            {
                checkCustomerType();
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
                  //  MessageBox.Show(ex.Message);
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


        //function
        public void checkCustomerType()
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
        }
        public void setCustomerType()
        {
            if (Customer_Type == "عميل")
            {
                radClient.Checked = true;
            }
            else if (Customer_Type == "مهندس")
            {
                radEng.Checked = true;
            }
            else if (Customer_Type == "مقاول")
            {
                radCon.Checked = true;
            }
            else if (Customer_Type == "تاجر")
            {
                radDealer.Checked = true;
            }
        }

        private void btnTaswaya_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                string query = "insert into customer_taswaya (Customer_ID,Client_ID,Taswaya_Type,Money_Paid,Info,Date) values(@Customer_ID,@Client_ID,@Taswaya_Type,@Money_Paid,@Info,@Date)";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                com.Parameters.Add("@Customer_ID", MySqlDbType.Int16);
                com.Parameters["@Customer_ID"].Value =Convert.ToInt16(txtCustomerID.Text);
                com.Parameters.Add("@Client_ID", MySqlDbType.Int16);
                com.Parameters["@Client_ID"].Value =Convert.ToInt16(txtClientID.Text);
                if (radioButton1.Checked)
                {
                    com.Parameters.Add("@Taswaya_Type", MySqlDbType.VarChar);
                    com.Parameters["@Taswaya_Type"].Value = radioButton1.Text;
                }
                else
                {
                    com.Parameters.Add("@Taswaya_Type", MySqlDbType.VarChar);
                    com.Parameters["@Taswaya_Type"].Value = radioButton2.Text;
                }
                com.Parameters.Add("@Money_Paid", MySqlDbType.Decimal);
                com.Parameters["@Money_Paid"].Value =Convert.ToDouble(txtMoney.Text);
                com.Parameters.Add("@Info", MySqlDbType.VarChar);
                com.Parameters["@Info"].Value = txtInfo.Text;
                com.Parameters.Add("@Date", MySqlDbType.Date);
                com.Parameters["@Date"].Value = dateTimeFrom.Value.Date;

                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        public void setIDs()
        {
             
            if (customerID != "")
            {
                string query = "select Customer_Name from customer where Customer_ID=" + customerID + "";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                if (com.ExecuteScalar() != null)
                {
                    Name = (string)com.ExecuteScalar();
                    comEngCon.Text = Name;

                }
            }
            if (clientID != "")
            {
               string query = "select Customer_Name from customer where Customer_ID=" + clientID + "";
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                if (com.ExecuteScalar() != null)
                {
                    Name = (string)com.ExecuteScalar();
                    comClient.Text = Name;
                    txtClientID.Text = clientID;
                }
            }
        }
    }
}
