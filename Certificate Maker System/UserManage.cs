using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Certificate_Maker_System
{
    public partial class UserManage : UserControl
    {
        private Form2 form2;
        private ManageButton manageButton;
        private int clickCount = 0;
        private int clickCount1 = 0;
        private string receive;

        public UserManage(string getuser)
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
            receive = getuser;
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

                ManageButton manageButton = new ManageButton();
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
            clickCount++;
            if (clickCount % 2 == 1)
            {
                usernameuser.Enabled = true;
            }
            else
            {
                usernameuser.Enabled = false;

                UpdateUsername(usernameuser.Text);
            }
        }

        private void passbtn_Click(object sender, EventArgs e)
        {
            clickCount1++;

            if (clickCount1 % 2 == 1)
            {
                passworduser.Enabled = true;
                passworduser.PasswordChar = '\0';
            }
            else
            {
                passworduser.Enabled = false;
                passworduser.PasswordChar = '*';
            }
        }

        private void UpdateUsername(string newUsername)
        {
            string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Updated query to use the users table instead of user_auth
                string query = "UPDATE users SET username = @NewUsername WHERE userId = @UserId";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewUsername", newUsername);
                    command.Parameters.AddWithValue("@UserId", receive); // Replace with the actual user ID

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Username updated successfully!");
        }
    }
}