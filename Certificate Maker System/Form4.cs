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
using Certificate_Maker_System.Resources;
using MySqlConnector;
using Org.BouncyCastle.Tls;

namespace Certificate_Maker_System
{
    public partial class Form4 : Form
    {
        private const string ConnectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=;";
        public string inputPassword;
        public string inputUsername;
        public string getusername;

        public Form4()
        {
            InitializeComponent();
            usernamebox.TabStop = false;
            passwordbox.TabStop = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            inputUsername = usernamebox.Text;
            inputPassword = passwordbox.Text;
        }

        private void close(object sender, EventArgs e)
        {
            this.Close();
        }

        public void loginbtn(object sender, EventArgs e)
        {
            inputUsername = usernamebox.Text;
            inputPassword = passwordbox.Text;

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    string selectQuery =
      "SELECT up.* " +
      "FROM `users` u " +
      "JOIN `user_profiles` up ON u.`userId` = up.`userId` " +
      "WHERE u.`username` = @username AND u.`password` = @password";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@username", inputUsername);
                        command.Parameters.AddWithValue("@password", inputPassword);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            if (dataTable.Rows.Count > 0)
                            {
                                // Verification succeeded, open Form2
                                this.Hide();
                                Form2 form2 = new Form2(inputUsername);
                                UserManage userManage = new UserManage(inputUsername);
                                form2.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Invalid username or password. Please try again.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // Verification succeeded, open Form2
            //this.Hide();
            //Form2 form2 = new Form2(inputUsername);
            //UserManage userManage = new UserManage(inputUsername);
            //form2.ShowDialog();
        }

        public string GetUsername()
        {
            return inputUsername;
        }

        private void userfucos(object sender, EventArgs e)
        {
            if (usernamebox.Text == "Username")
            {
                // Clear the text when the TextBox receives focus
                usernamebox.Text = "";
                usernamebox.ForeColor = Color.Black;
            }
        }

        private void userleave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usernamebox.Text))
            {
                // If it's empty, set the default text back
                usernamebox.Text = "Username";
                // Set the text color back to gray
                usernamebox.ForeColor = Color.Gray;
            }
        }

        private void passfucos(object sender, EventArgs e)
        {
            if (passwordbox.Text == "Password")
            {
                // Clear the text when the TextBox receives focus
                passwordbox.Text = "";
                passwordbox.ForeColor = Color.Black;
            }
            passwordbox.PasswordChar = '*';
        }

        private void passleave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(passwordbox.Text))
            {
                // If it's empty, set the default text back
                passwordbox.Text = "Password";
                // Set the text color back to gray
                passwordbox.ForeColor = Color.Gray;
                passwordbox.PasswordChar = '\0';
            }
        }

        private void passwordbox_TextChanged(object sender, EventArgs e)
        {
        }

        // Add to Form4 class
        public int GetUserId()
        {
            // Assuming you have the username stored in a class variable
            string username = GetUsername();

            int userId = 0;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT userId FROM users WHERE username = @username";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out userId))
                        {
                            return userId;
                        }
                    }
                }
                catch (Exception)
                {
                    // Handle exception
                }
            }
            return userId; // Returns 0 if not found
        }
    }
}