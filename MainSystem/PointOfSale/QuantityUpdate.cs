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
    public partial class QuantityUpdate : DevExpress.XtraEditors.XtraForm
    {
        DataRowView selRow;
        MySqlConnection conn;
        XtraTabControl MainTabControlPointSale;
        int cartons = 0;

        public QuantityUpdate(DataRowView Selrow)
        {
            InitializeComponent();
            conn = new MySqlConnection(connection.connectionString);
            selRow = Selrow;
            MainTabControlPointSale = MainForm.tabControlPointSale;
        }

        private void QuantityUpdate_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from store";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();

                txtQuantity.Text = selRow["الكمية"].ToString();
                comStore.SelectedValue = selRow["Store_ID"].ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtQuantity.Text != "" && comStore.Text != "")
                {
                    double quantity = 0;
                    if (double.TryParse(txtQuantity.Text, out quantity))
                    {
                        conn.Open();
                        if (!checkQuantityInStore(quantity))
                        {
                            MessageBox.Show("لا يوجد كمية كافية من العنصر فى المخزن");
                            conn.Close();
                            return;
                        }

                        cartons = 0;
                        if (selRow["الكود"].ToString().Length >= 20)
                        {
                            string q = "select Carton from data where Code='" + selRow["الكود"].ToString() + "'";
                            MySqlCommand c = new MySqlCommand(q, conn);
                            if (c.ExecuteScalar() != null)
                            {
                                if (Convert.ToDouble(c.ExecuteScalar().ToString()) > 0)
                                {
                                    if (cartonNumCheck())
                                    { }
                                    else
                                    {
                                        conn.Close();
                                        return;
                                    }
                                }
                            }
                        }

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
                        Label textLabel = new Label() { Left = 340, Top = 20, Text = "ما هو سبب التعديل؟" };
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
                                /*string query = "select sum(storage.Total_Meters) from storage where storage.Code='" + selRow["الكود"].ToString() + "' group by storage.Code";
                                MySqlCommand comand = new MySqlCommand(query, conn);
                                double TotalMeters = Convert.ToDouble(comand.ExecuteScalar());*/

                                //quantity <= TotalMeters

                                string query = "update dash_details set Quantity=" + quantity + ",Cartons=" + cartons + ", Store_ID=" + comStore.SelectedValue.ToString() + " , Store_Name='" + comStore.Text + "' where DashDetails_ID=" + Convert.ToInt16(selRow[0].ToString());
                                MySqlCommand comand = new MySqlCommand(query, conn);
                                comand.ExecuteNonQuery();

                                UserControl.ItemRecord("dash_details", "تعديل", Convert.ToInt16(selRow[0].ToString()), DateTime.Now, textBox.Text, conn);

                                conn.Close();
                                MainForm.ProductsDetailsReport.loadFunc();
                                this.Close();
                                
                            }
                            else
                            {
                                MessageBox.Show("يجب كتابة السبب");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("الكمية يجب ان تكون عدد");
                    }
                }
                else
                {
                    MessageBox.Show("يجب ادخال البيانات كاملة");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        //functions
        public bool cartonNumCheck()
        {
            try
            {
                string query = "select Carton from data where Code='" + selRow["الكود"].ToString() + "'";
                MySqlCommand com = new MySqlCommand(query, conn);
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

        public bool checkQuantityInStore(double totalMeter)
        {
            if (selRow["النوع"].ToString() == "بند")
            {
                string query = "SELECT sum(storage.Total_Meters) as 'الكمية' FROM storage inner join data on storage.Data_ID=data.Data_ID where storage.Store_ID=" + comStore.SelectedValue.ToString() + " and data.Code='" + selRow["الكود"].ToString() + "' group by storage.Data_ID,storage.Store_ID";
                MySqlCommand com = new MySqlCommand(query, conn);
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
            else if (selRow["النوع"].ToString() == "طقم")
            {
                string query = "SELECT sum(storage.Total_Meters) as 'الكمية' FROM storage where storage.Store_ID=" + comStore.SelectedValue.ToString() + " and storage.Set_ID=" + selRow["الكود"].ToString() + " group by storage.Set_ID,storage.Store_ID";
                MySqlCommand com = new MySqlCommand(query, conn);
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
            else if (selRow["النوع"].ToString() == "عرض")
            {
                string query = "SELECT sum(storage.Total_Meters) as 'الكمية' FROM storage where storage.Store_ID=" + comStore.SelectedValue.ToString() + " and storage.Offer_ID=" + selRow["الكود"].ToString() + " group by storage.Offer_ID,storage.Store_ID";
                MySqlCommand com = new MySqlCommand(query, conn);
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
            return false;
        }
    }
}