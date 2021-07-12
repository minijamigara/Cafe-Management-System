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
    public partial class items : Form
    {
        // Creating instance of SqlConnection  
        public string conString = "Data Source=LAPTOP-D2MA3PAN\\SQLEXPRESS;Initial Catalog=CafeDB;Integrated Security=True";
        public items()
        {
            InitializeComponent();
            displayItem();
        }
        private void label8_Click(object sender, EventArgs e)
        {
            login obj = new login();
            obj.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(catTxt.Text == string.Empty)
            {
                MessageBox.Show("Enter the category");
            }
            else
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();// open the database connection  
               if(con.State==System.Data.ConnectionState.Open)
                {
                    try
                    {
                        string q = "Insert INTO categoryTbl(catName) VALUES ('" + catTxt.Text.ToString() + "')";
                        SqlCommand cmd = new SqlCommand(q, con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Category Added"); // showing messagebox for confirmation message for user  
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Cannot add category", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    finally
                    {
                        con.Close();// Close the connection 
                        catTxt.Text = "";
                        fillCategory();
                    }
/*                    string q = "Insert INTO categoryTbl(catName) VALUES ('" + catTxt.Text.ToString() + "')";
                    SqlCommand cmd = new SqlCommand(q,con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Added"); // showing messagebox for confirmation message for user  
                    con.Close();// Close the connection 
                    catTxt.Text = "";
                    fillCategory();*/
                }
                
            }
 
        }
        private void reset()
        {
            itNameTxt.Text = "";
            catCb.SelectedIndex = -1; //To clear the selected item
            //catCb.Items.Clear(); clear all combobox items
            itQtyTxt.Text = "";
            itPriceTxt.Text = "";
        }

        private void fillCategory()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();// open the database connection  
            string q = "Select catName from CategoryTbl";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            adapter.Fill(tbl);

            foreach(DataRow dr in tbl.Rows)
            {
                catCb.Items.Add(dr["catName"].ToString());
            }

            con.Close();
        }
        private void reset_btn_Click(object sender, EventArgs e)
        {
            reset();
        }
        private void timeDisplay()
        {
            /*            if (DateTime.Now.ToString("tt") == "AM")
                        {
                            time_lbl.Text = "Good Morning";
                        }
                        else if(DateTime.Now.ToString("tt") == "PM")
                        {
                            time_lbl.Text = "Good Evening";
                        }*/

            try
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
            catch (System.Exception ex)
            {
                throw new System.Exception("Couldn't get the date");
            }

        }
        private void items_Load(object sender, EventArgs e)
        {
            fillCategory();
            displayItem();
            timeDisplay();
        }
        private void displayItem()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            //SqlCommand cmd;
            SqlDataAdapter adpt;
            DataTable dt;
            adpt = new SqlDataAdapter("select * from ItemTbl", con);
            dt = new DataTable();
            adpt.Fill(dt);
            itemDGV.DataSource = dt;
            con.Close();
        }
        private void Add_btn_Click(object sender, EventArgs e)
        {
            if (catCb.SelectedIndex == -1 || itNameTxt.Text == "" || itPriceTxt.Text == "" || itQtyTxt.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();// open the database connection  
                if (con.State == System.Data.ConnectionState.Open)
                {

                    string q = "Insert INTO ItemTbl(itName,itcat,itPrice,itQty) VALUES ('" + itNameTxt.Text.ToString() + "','"+catCb.SelectedIndex.ToString()+"','"+itPriceTxt.Text.ToString()+"','"+itQtyTxt.Text.ToString()+"')";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Added"); // showing messagebox for confirmation message for user  
                    con.Close();// Close the connection 
                    reset();
                    displayItem();
                    
                }

            }
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select item to delete");
            }
            else
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();// open the database connection  
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = "delete from ItemTbl where Id = " + key + "";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Deleted"); // showing messagebox for confirmation message for user  
                    con.Close();// Close the connection 
                    reset();
                    displayItem();

                }

            }
        }
        private void edit_btn_Click(object sender, EventArgs e)
        {
            if (catCb.SelectedIndex == -1 ||  itNameTxt.Text == "" || itPriceTxt.Text == "" || itQtyTxt.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();// open the database connection  
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = "update ItemTbl set itName='"+itNameTxt.Text+"',itcat='"+catCb.SelectedIndex+"',itPrice='"+itPriceTxt.Text+"',itQty='"+itQtyTxt.Text+"' where Id= '"+key+"'";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Edited"); // showing messagebox for confirmation message for user  
                    con.Close();// Close the connection 
                    reset();
                    displayItem();

                }

            }
        }
        public int key;
        private void itemDGV_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            itemDGV.CurrentRow.Selected = true;
            itNameTxt.Text = itemDGV.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            catCb.SelectedValue = itemDGV.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            itPriceTxt.Text = itemDGV.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
            itQtyTxt.Text = itemDGV.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
            if(itNameTxt.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(itemDGV.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
            }
            
        }

        private void itemDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void itQtyTxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
