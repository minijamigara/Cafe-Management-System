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
    public partial class splashScrean : Form
    {
        public splashScrean()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private int startPos = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startPos += 1;
            myProgress.Value = startPos;
            percentage_lbl.Text = startPos + "%";
            if(myProgress.Value == 100)
            {
                myProgress.Value = 0;
                timer1.Stop();
                login obj = new login();
                obj.Show();
                this.Hide();
            }
        }

        private void splashScrean_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
