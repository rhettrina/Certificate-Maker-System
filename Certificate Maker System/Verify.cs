using System;
using System.Windows.Forms;

namespace Certificate_Maker_System
{
    public partial class Verify : Form
    {
        public Verify()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void changeUserControl(UserControl userControl)
        {
            Form2 form2 = new Form2("");
            userControl.Dock = DockStyle.Fill;
            form2.panelContainer.Controls.Add(userControl);
            form2.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void cancel(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okbtn(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();

            string trytry = "try";

            if (confirmation.Text == trytry)
            {
                UserManage userManage = new UserManage("");
                changeUserControl(userManage);
                this.Close();
                MessageBox.Show("Correct");
            }
            else
            {
                MessageBox.Show("Wrong Password");
            }
        }
    }
}