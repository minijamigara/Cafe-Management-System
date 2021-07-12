using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeManagement
{
    public partial class viewOrders : Form
    {
        // Creating instance of SqlConnection  
        public string conString = "Data Source=LAPTOP-D2MA3PAN\\SQLEXPRESS;Initial Catalog=CafeDB;Integrated Security=True";
        public viewOrders()
        {
            InitializeComponent();
        }
        private void displayBill()
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd;
            SqlDataAdapter adapt;
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from OrderTbl", con);
            adapt.Fill(dt);
            itemDGV.DataSource = dt;
            con.Close();
        }
        private void viewOrders_Load(object sender, EventArgs e)
        {
            displayBill();
        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            orders obj = new orders();
            obj.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            orders obj = new orders();
            obj.Show();
            this.Close();
        }

        private void itemDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
