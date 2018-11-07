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
    public partial class Form1 : Form
    {
        MySqlConnection dbconnection;
        public Form1()
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            dbconnection.Open();
            string query = "select * from store";
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            MySqlDataReader dr = com.ExecuteReader();

            DataTable dt = new DataTable();
            DataColumn c = new DataColumn("كود");
            DataColumn c1 = new DataColumn("المخزن");
            DataColumn c2 = new DataColumn("العنوان");
            DataColumn c3 = new DataColumn("التلفون");
            dt.TableName = "المخازن";

            dt.Columns.Add(c);
            dt.Columns.Add(c1);
            dt.Columns.Add(c2);
            dt.Columns.Add(c3);

            while (dr.Read())
            {
                dt.Rows.Add(dr[0],dr[1],dr[2],dr[3]);
                
            }
            dr.Close();
            layoutView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
    }
}
