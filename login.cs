using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeManagement
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(usernameTb.Text == string.Empty || passwordTb.Text == string.Empty)
            {
                MessageBox.Show("Enter username and password");
            }
            else if(usernameTb.Text =="Admin" && passwordTb.Text == "Admin" )
            {
                items obj = new items();
                obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong username or password");
                usernameTb.Text = "";
                passwordTb.Text = "";
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            orders obj = new orders();
            obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void usernameTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
