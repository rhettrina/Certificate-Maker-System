using Certificate_Maker_System.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.WinForms.Helpers.GraphicsHelper;

namespace Certificate_Maker_System
{
    public partial class UserManage : UserControl
    {

        private Form2 form2;
        private ManageButton manageButton;
        public UserManage()
        {
            InitializeComponent();
            firstnameuser.Enabled = false;
            middleuser.Enabled = false;
            lastuser.Enabled = false;
            genderuser.Enabled = false;
            positionuser.Enabled = false;
            emailuser.Enabled = false;
            birthdayuser.Enabled = false;
            usernameuser.Enabled = false;
            passworduser.Enabled = false;
            form2 = new Form2("");
            manageButton = new ManageButton();
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UserManage_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
                    }

        private void button4_Click(object sender, EventArgs e)
        {
            // Get the position value
            string userPosition = positionuser.Text.Trim();

            // Check if the position is 'admin'
            if (userPosition.ToLower() == "admin")
            {
                Form2 form2 = this.ParentForm as Form2;

                Panel panelcontainer = form2.panelContainer;
                
                ManageButton manageButton   = new ManageButton();
                panelcontainer.Controls.Clear();
                panelcontainer.Controls.Add(manageButton);
            }
            else
            {
                // Show a message that only admins are allowed
                MessageBox.Show("Only administrators are allowed to perform this action.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }





        private void changeuser(object sender, EventArgs e)
        {

        }
    }
}
