using Certificate_Maker_System.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Certificate_Maker_System
{
   
    public partial class Form2 : Form
    {

        private List<Button> reset = new List<Button>();
    
        public Form2()
        {
            InitializeComponent();
            Dashboard dashboard = new Dashboard();
            changeUserControl(dashboard);
        }

        private void ResetButtonColors()
        {
            foreach (Button button in reset)
            {
                button.ForeColor = Color.White;
            }
        }

        private void changeUserControl (UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(userControl);
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        } 

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            UserManage userManage = new UserManage();
            ResetButtonColors();
            ((Button)sender).ForeColor = Color.RoyalBlue;
            reset.Add((Button)sender);
            changeUserControl(userManage);
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void dashboardbtn(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            ResetButtonColors();
            ((Button)sender).ForeColor = Color.RoyalBlue;
            reset.Add((Button)sender);
            changeUserControl(dashboard);

        }

        private void certificatebtn(object sender, EventArgs e)
        {
            CertificateGenerator certificateGenerator = new CertificateGenerator();
            ResetButtonColors();
            ((Button)sender).ForeColor = Color.RoyalBlue;
            reset.Add((Button)sender);
            changeUserControl(certificateGenerator);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void close(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimize(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {

        }

        private void logoutbtn(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                Form4 form4 = new Form4();
                form4.Show();
            }
        }

        private void studentbtn(object sender, EventArgs e)
        {
            StudentList studentList = new StudentList();
            ResetButtonColors();
            ((Button)sender).ForeColor = Color.RoyalBlue;
            reset.Add((Button)sender);
            changeUserControl(studentList);
        }
    }
}
