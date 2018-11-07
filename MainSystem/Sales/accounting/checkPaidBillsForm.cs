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
    public partial class checkPaidBillsForm : Form
    {
        private MySqlConnection dbconnection;
        private double recivedMoney = 0;
        int clientID = -1;
        private bool loaded=false;

        public checkPaidBillsForm()//, StoreForm2CrystalReport crystalReport)
        {
            try
            {
                InitializeComponent();
                dbconnection = new MySqlConnection(connection.connectionString);
                dbconnection.Open();
                /* and Bill_Date between '"+DateFrom+"' and '"+DateTo+ "'*/
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void checkPaidBillsForm_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select Customer_ID,Customer_Name from customer inner join Client_Rest_Money on Client_Rest_Money.Client_ID=customer.Customer_ID";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comClientName.DataSource = dt;
                comClientName.DisplayMember = dt.Columns["Customer_Name"].ToString();
                comClientName.ValueMember = dt.Columns["Customer_ID"].ToString();
                comClientName.Text = "";
                txtClientID.Text = "";
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (e.ColumnIndex == dataGridView1.Columns["PaidOrNot"].Index)
                {
                    DataGridViewRow row = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                    DataGridViewCheckBoxCell x =row.Cells["PaidOrNot"] as DataGridViewCheckBoxCell;
                  
                    
                    if (recivedMoney >= Convert.ToDouble(row.Cells["اجمالي الفاتورة"].Value))
                    {
                        if (Convert.ToBoolean(row.Cells["PaidOrNot"].EditedFormattedValue) == false)
                        {
                            recivedMoney -= Convert.ToDouble(row.Cells["اجمالي الفاتورة"].Value);
                            labRecivedMoney.Text = recivedMoney.ToString();
                            string query = "update customer_bill set Paid_Status=1 where CustomerBill_ID=" + row.Cells["رقم الفاتورة"].Value.ToString();
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                        }
                        else
                        {
                            recivedMoney += Convert.ToDouble(row.Cells["اجمالي الفاتورة"].Value);
                            labRecivedMoney.Text = recivedMoney.ToString();
                            string query = "update customer_bill set Paid_Status=0 where CustomerBill_ID=" + row.Cells["رقم الفاتورة"].Value.ToString();
                            MySqlCommand com = new MySqlCommand(query, dbconnection);
                            com.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        MessageBox.Show("قيمة الفاتورة اكبر من المبلغ المدفوع");
                        x.Value = x.FalseValue;
                        dataGridView1.EndEdit();
                    }
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
   
        private void comClientName_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    dbconnection.Open();
                    txtClientID.Text = comClientName.SelectedValue.ToString();
                    clientID = Convert.ToInt16(txtClientID.Text);
                    Display();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void txtClientID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int id;
                    if (int.TryParse(txtClientID.Text, out id))
                    {
                        string query = "select Customer_Name from customer where Customer_ID="+id;
                        dbconnection.Open();
                        MySqlCommand com = new MySqlCommand(query, dbconnection);
                       
                            comClientName.Text = com.ExecuteScalar().ToString();
                        clientID = id;
                        Display();

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
            dbconnection.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                if (clientID != -1)
                {
                    string query = "select Money from Client_Rest_Money where Client_ID=" + clientID;
                    MySqlCommand com = new MySqlCommand(query, dbconnection);
                    if (com.ExecuteScalar() != null)
                    {
                        double paidMoney = Convert.ToDouble(labRecivedMoney.Text);
                        query = "update Client_Rest_Money set Money=" + paidMoney + " where Client_ID=" + txtClientID.Text;
                    }
                    else
                    {
                        query = "insert into Client_Rest_Money (Client_ID,Client_Name,Money) values (" + clientID + ",'" + comClientName.Text + "'," + labRecivedMoney.Text + ")";
                    }
                    com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Done");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        public void Display()
        {
            string query = "select distinct CustomerBill_ID as 'رقم الفاتورة' ,customer.Customer_Name as ' العميل'"+/* ,delegate.Delegate_Name as 'المندوب' */""+", Total_CostAD as 'اجمالي الفاتورة',customer_bill.Branch_Name as 'الفرع',Bill_Date as'التاريخ'  from customer_bill inner join customer on customer_bill.Client_ID=customer.Customer_ID "+/*inner join delegate on delegate.Delegate_ID=customer_bill.Delegate_ID */""+"where Client_ID=" + clientID + " and Paid_Status=0 and Type_Buy='آجل'";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].Visible = false;
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "PaidOrNot";
            checkColumn.HeaderText = "تم الدفع";
       
            checkColumn.ReadOnly = false;
            checkColumn.FalseValue = false;
            checkColumn.TrueValue = true;
           // checkColumn.FillWeight = 10; //if the datagridview is resized (on form resize) the checkbox won't take up too much; value is relative to the other columns' fill values           
            dataGridView1.Columns.Add(checkColumn);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            checkColumn.Width = 100;
            query = "select Money from Client_Rest_Money where Client_ID="+clientID;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                double money = Convert.ToDouble(com.ExecuteScalar());
                recivedMoney += money;
            }
            labRecivedMoney.Text = recivedMoney.ToString();
        }
    }
}
