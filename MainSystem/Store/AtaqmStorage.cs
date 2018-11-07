using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Card;
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
    public partial class AtaqmStorage : Form
    {
        MySqlConnection dbconnection;
        MainForm storeMainForm =null;
        bool loaded = false;
        bool flag = false;
        DataGridViewRow row1;
        public static SetRecord setRecord = null;
        public static SetUpdate setUpdate = null;
        public AtaqmStorage(MainForm StoreMainForm)
        {
            try
            {
                InitializeComponent();
                this.storeMainForm = StoreMainForm;
                dbconnection = new MySqlConnection(connection.connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void Ataqm_Load(object sender, EventArgs e)
        {
            try
            {
                loadDataToBox();
                DisplayAtaqm();
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void comBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    ComboBox comBox = (ComboBox)sender;

                    switch (comBox.Name)
                    {
                        case "comFactory":
                            txtFactory.Text = comFactory.SelectedValue.ToString();
                            DisplayAtaqm();
                            break;
                        case "comType":
                            txtType.Text = comType.SelectedValue.ToString();
                            DisplayAtaqm();
                            break;
                        case "comGroup":
                            txtGroup.Text = comGroup.SelectedValue.ToString();
                            DisplayAtaqm();
                            break;
                        case "comStore":
                            txtStoreID.Text = comStore.SelectedValue.ToString();
                            DisplayAtaqm();
                            break;
                        case "comSets":
                            txtSetsID.Text = comSets.SelectedValue.ToString();
                            DisplayAtaqm();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                            case "txtFactory":
                                query = "select Factory_Name from sets inner join factory on factory.Factory_ID=sets.Factory_ID where sets.Factory_ID=" + txtFactory.Text ;
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comFactory.Text = Name;
                                    DisplayAtaqm();
                                    txtGroup.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtType":
                                query = "select Type_Name from sets inner join type on type.Type_ID=sets.Type_ID where sets.Type_ID=" + txtType.Text ;
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comType.Text = Name;
                                    DisplayAtaqm();
                                    txtFactory.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtGroup":
                                query = "select Group_Name from sets inner join groupo on groupo.Group_ID=sets.Group_ID where sets.Group_ID=" + txtGroup.Text;
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comGroup.Text = Name;
                                    DisplayAtaqm();
                                    txtSetsID.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtStoreID":
                                query = "select Store_Name from store where Store_ID=" + txtStoreID.Text ;
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comStore.Text = Name;
                                    txtType.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtSetsID":
                                query = "select Set_Name from sets where Set_ID=" + txtSetsID.Text ;
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comSets.Text = Name;                                     
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
                DisplayAtaqm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnNewChooes_Click(object sender, EventArgs e)
        {
            try
            {
                comStore.Text = "";
                comType.Text = "";
                comFactory.Text = "";
                comGroup.Text = "";
                comSets.Text = "";

                txtStoreID.Text = "";
                txtType.Text = "";
                txtFactory.Text = "";
                txtGroup.Text = "";
                txtSetsID.Text = "";

                DisplayAtaqm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnTagame3_Click(object sender, EventArgs e)
        {
            try
            {
                storeMainForm.bindTagame3SetForm(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        private void btnFak_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                storeMainForm.bindFakSetForm(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            dbconnection.Close();
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                storeMainForm.bindReportSetForm(dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // row1 = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];

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
                DataRowView row1 = (DataRowView)(((GridView)dataGridView1.MainView).GetRow(((GridView)dataGridView1.MainView).GetSelectedRows()[0]));
                string query = "SELECT data.Data_ID,data.Code,product.Product_Name,type.Type_Name,factory.Factory_Name,groupo.Group_Name,color.Color_Name,size.Size_Value,sort.Sort_Value,data.Classification,data.Description,data.Carton from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID where  data.Code in(select set_details.Code from set_details where set_details.Set_ID=" + row1[0].ToString() + ") group by data.Code";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, dbconnection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        //function
        public void DisplayAtaqm()
        {
            try
            {
                dbconnection.Close();
                dbconnection.Open();
                loaded = false;
                string q1, q2, q3, q4,q5;
                if (txtType.Text == "")
                {
                    q1 = "select Type_ID from sets";
                }
                else
                {
                    q1 = txtType.Text;
                }
                if (txtFactory.Text == "")
                {
                    q2 = "select Factory_ID from sets";
                }
                else
                {
                    q2 = txtFactory.Text;
                }
                if (txtSetsID.Text == "")
                {
                    q3 = "select Set_ID from sets";
                }
                else
                {
                    q3 = txtSetsID.Text;
                }
                if (txtGroup.Text == "")
                {
                    q4 = "select Group_ID from sets";
                }
                else
                {
                    q4 = txtGroup.Text;
                }
                if (txtStoreID.Text == "")
                {
                    q5 = "select Store_ID from store";
                }
                else
                {
                    q5 = txtStoreID.Text;
                }
                string query = "SELECT storage.Set_ID as 'كود الطقم',Set_Name as 'اسم الطقم',Type_Name as 'النوع',Factory_Name as 'المصنع',Group_Name as 'المجموعة',SUM(storage.Total_Meters) as 'الكمية',Photo as 'صورة' from sets left Join set_photo on sets.Set_ID=set_photo.Set_ID INNER JOIN type ON type.Type_ID = sets.Type_ID  INNER JOIN factory ON sets.Factory_ID = factory.Factory_ID INNER JOIN groupo ON sets.Group_ID = groupo.Group_ID  inner join storage on storage.Set_ID=sets.Set_ID where sets.Type_ID IN(" + q1 + ") and  sets.Factory_ID  IN(" + q2 + ") and storage.Set_ID IN("+q3+") and sets.Group_ID IN (" + q4 + ")  GROUP by storage.Set_ID";

                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Close();
                    dbconnection.Close();
                    dbconnection.Open();
                    MySqlDataAdapter adapterSets = new MySqlDataAdapter(query, dbconnection);
                    query = "SELECT storage.Set_ID as 'كود الطقم',data.Code as 'الكود',set_details.Quantity as 'الكمية',product.Product_Name as 'الصنف',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',sort.Sort_Value as 'الفرز',data.Classification as 'التصنيف',data.Description as 'الوصف',data.Carton as 'الكرتنة',data_photo.Photo as 'صورة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID INNER JOIN set_details on data.Data_ID=set_details.Data_ID INNER JOIN sets on sets.Set_ID=set_details.Set_ID left join data_photo on data_photo.Data_ID=data.Data_ID inner join storage on storage.Set_ID=sets.Set_ID    order by data.Code";
                    MySqlDataAdapter AdapterProducts = new MySqlDataAdapter(query, dbconnection);
                    DataSet dataSet11 = new DataSet();

                    //Create DataTable objects for representing database's tables 
                    adapterSets.Fill(dataSet11, "Sets");
                    AdapterProducts.Fill(dataSet11, "Products");

                    //Set up a master-detail relationship between the DataTables 
                    DataColumn keyColumn = dataSet11.Tables["Sets"].Columns["كود الطقم"];
                    DataColumn foreignKeyColumn = dataSet11.Tables["Products"].Columns["كود الطقم"];
                    dataSet11.Relations.Add("بنود الطقم", keyColumn, foreignKeyColumn);

                    //Bind the grid control to the data source 
                    dataGridView1.DataSource = dataSet11.Tables["Sets"];
                }
                else
                {
                    dataGridView1.DataSource = null;
                }
                loaded = true;               
            }
            catch (Exception ex)
            {

                dataGridView1.DataSource = null;
            }
            dbconnection.Close();
        
    }

        public void deleteSet(int id)
        {
            String query = "delete from sets where Set_ID="+id;
            MySqlCommand com = new MySqlCommand(query,dbconnection);
            com.ExecuteNonQuery();
            query = "delete from set_details where Set_ID=" + id;
            com = new MySqlCommand(query, dbconnection);
            com.ExecuteNonQuery();
        }
        public void loadDataToBox()
        {
            string query = "select distinct Factory_Name,sets.Factory_ID from sets inner join Factory on sets.Factory_ID=Factory.Factory_ID";
            MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comFactory.DataSource = dt;
            comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
            comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
            comFactory.Text = "";
            txtFactory.Text = "";

            query = "select Set_Name,Set_ID from sets ";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comSets.DataSource = dt;
            comSets.DisplayMember = dt.Columns["Set_Name"].ToString();
            comSets.ValueMember = dt.Columns["Set_ID"].ToString();
            comSets.Text = "";
            txtSetsID.Text = "";

            query = "select distinct Type_Name,type.Type_ID from sets inner join type on sets.Type_ID=type.Type_ID";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comType.DataSource = dt;
            comType.DisplayMember = dt.Columns["Type_Name"].ToString();
            comType.ValueMember = dt.Columns["Type_ID"].ToString();
            comType.Text = "";
            txtType.Text = "";

            query = "select distinct Group_Name,sets.Group_ID from sets inner join groupo on sets.Group_ID=groupo.Group_ID";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comGroup.DataSource = dt;
            comGroup.DisplayMember = dt.Columns["Group_Name"].ToString();
            comGroup.ValueMember = dt.Columns["Group_ID"].ToString();
            comGroup.Text = "";
            txtGroup.Text = "";

            query = "select * from store ";
            da = new MySqlDataAdapter(query, dbconnection);
            dt = new DataTable();
            da.Fill(dt);
            comStore.DataSource = dt;
            comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
            comStore.ValueMember = dt.Columns["Store_ID"].ToString();
            comStore.Text = "";
            txtStoreID.Text = "";
        }

        
    }
}
