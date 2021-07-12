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
    public partial class orders : Form
    {
        // Creating instance of SqlConnection  
        public string conString = "Data Source=LAPTOP-D2MA3PAN\\SQLEXPRESS;Initial Catalog=CafeDB;Integrated Security=True";
        public orders()
        {
            InitializeComponent();
            displayItem();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            login obj = new login();
            obj.Show();
            this.Hide();
        }
        // function to filter the items by category 
        private void filterByCategory()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            //SqlCommand cmd;
            SqlDataAdapter adpt;
            DataTable dt;
            adpt = new SqlDataAdapter("Select * from ItemTbl where itcat = '" + catCb.SelectedIndex.ToString() + "'", con); //sql statement to filter items by the combobox index
            dt = new DataTable();
            adpt.Fill(dt);
            itemDGV.DataSource = dt;
            con.Close();
        }

        //function to add the categories to the combobox - display categories in the combobox
        private void fillCategory()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();// open the database connection  
            string q = "Select catName from CategoryTbl"; //-get data from the category table
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            adapter.Fill(tbl);

            foreach (DataRow dr in tbl.Rows)
            {
                catCb.Items.Add(dr["catName"].ToString());
            }

            con.Close();
        }

        //function to display all the items in the cafe 
        private void displayItem()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            //SqlCommand cmd;
            SqlDataAdapter adpt;
            DataTable dt;
            adpt = new SqlDataAdapter("select * from ItemTbl", con); //- get data from the item table
            dt = new DataTable();
            adpt.Fill(dt);
            itemDGV.DataSource = dt;
            con.Close();
        }

        //function to get the current time and display the greeting message on the lable
        private void timeDisplay()
        {
            if (DateTime.Now.ToString("tt") == "AM")
            {
                time_lbl.Text = "Good Morning";
            }
            else if (DateTime.Now.ToString("tt") == "PM")
            {
                time_lbl.Text = "Good Evening";
            }
        }

        private void orders_Load(object sender, EventArgs e)
        {
            displayItem();
            fillCategory();
            timeDisplay();
        }

        private void catCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filterByCategory();
        }

        private void refresh_btn_Click(object sender, EventArgs e)
        {
            displayItem();
            catCb.SelectedIndex = -1; //To clear the selected item
        }

        //function to add the bill details to the DB
        private void addBill()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();// open the database connection  
            if (con.State == System.Data.ConnectionState.Open)
            {
                if (GrdTotal == 0)
                {
                    MessageBox.Show("Please select an item"); // showing messagebox for confirmation message for user  
                }
                else
                {
                    string q = "Insert INTO OrderTbl(ordDate,ordAmount) VALUES ('" + DateTime.Today.Date + "','" + GrdTotal + "')"; //-insert bill data to order table
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Added"); // showing messagebox for confirmation message for user  
                    con.Close();// Close the connection
                    printPreviewDialog1.Show();
                }
                 
            }
        }

        private void print_btn_Click(object sender, EventArgs e)
        {
            addBill();
            //printPreviewDialog1.Show();
            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Pixel Cafe",new Font("Arial",22),Brushes.BlueViolet,335,35);
            e.Graphics.DrawString("Your Bill", new Font("Arial",14), Brushes.BlueViolet, 350, 60);

            Bitmap bm = new Bitmap(bill_DGV.Width,bill_DGV.Height);
            bill_DGV.DrawToBitmap(bm, new Rectangle(0, 0, bill_DGV.Width, bill_DGV.Height));
            e.Graphics.DrawImage(bm, 0, 90);
            e.Graphics.DrawString("Total amount" + GrdTotal.ToString(),new Font("Arial",15), Brushes.Crimson, 325, 580);
            e.Graphics.DrawString("**********THANK YOU VISIT AGAIN***********", new Font("Arial", 15), Brushes.Crimson, 130, 600);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            viewOrders obj = new viewOrders();
            obj.Show();
            this.Hide();
        }

        //function to update items
        private void updateItem()
        {
            int newQty = stock - Convert.ToInt32(qty_txt.Text);
            SqlConnection con = new SqlConnection(conString);
            con.Open();// open the database connection  
            if (con.State == System.Data.ConnectionState.Open)
            {
                string q = "update ItemTbl set itQty='"+newQty+"' where Id='"+key+"'"; //-update data in the item table
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Edited"); // showing messagebox for confirmation message for user  
                con.Close();// Close the connection 
                displayItem();

            }
        }

        private void addBill_btn_Click(object sender, EventArgs e)
        {
            if(key == 0)
            {
                MessageBox.Show("Please select an item");
            }
            else if(Convert.ToInt32(qty_txt.Text ) > stock)
            {
                MessageBox.Show("out of stock");
            }
            else
            {
                int rnum = bill_DGV.Rows.Add();
                int total = Convert.ToInt32(qty_txt.Text) * price;
                i = i + 1;
                bill_DGV.Rows[rnum].Cells["Column1"].Value = i ;
                bill_DGV.Rows[rnum].Cells["Column2"].Value = productName;
                bill_DGV.Rows[rnum].Cells["Column3"].Value = price;
                bill_DGV.Rows[rnum].Cells["Column4"].Value = qty_txt.Text;
                bill_DGV.Rows[rnum].Cells["Column5"].Value = total;
                GrdTotal = GrdTotal + total;
                total_lbl.Text = "Rs" + Convert.ToString(GrdTotal);
                updateItem();
                qty_txt.Text = "";
                key = 0;
            }
        }
        //Mouse events
        private string productName;
        private int i = 0, price, qty, GrdTotal = 0;

        private void itemDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bill_DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private int key = 0,stock;
        private void itemDGV_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            itemDGV.CurrentRow.Selected = true;
            productName = itemDGV.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();

            if (productName == "")
            {
                key = 0;
                stock = 0;
            }
            else
            {
                key = Convert.ToInt32(itemDGV.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
                stock = Convert.ToInt32(itemDGV.Rows[e.RowIndex].Cells[4].FormattedValue.ToString()); //assign the updated value to the quantity column
                price = Convert.ToInt32(itemDGV.Rows[e.RowIndex].Cells[3].FormattedValue.ToString());
            }
        }
    }
}
