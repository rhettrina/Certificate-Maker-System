using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Certificate_Maker_System
{
    public partial class AddEditUser : Form
    {
        private const string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=root;";
        public AddEditUser()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void savebtn(object sender, EventArgs e)
        {
                try
                {
                    string username = userbox.Text;
                    string password = passbox.Text; // Note: Implement proper password hashing
                    string firstName = firstnamebox.Text;
                    string middleName = middlebox.Text;
                    string lastName = lastnamebox.Text;
                    string gender = genderbox.Text;
                    string position = positionbox.Text;
                    string email = emailbox.Text;
                    string birthday = birthdaybox.Text; // Note: Consider using a DateTimePicker control for birthday

                    // Use a parameterized query to prevent SQL injection
                    string query = "INSERT INTO `user` (username, password, firstName, middleName, lastName, gender, position, email, birthday) " +
                                   "VALUES (@username, @password, @firstName, @middleName, @lastName, @gender, @position, @email, @birthday)";

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            connection.Open();

                            // Add parameters with null checks
                            cmd.Parameters.Add(new MySqlParameter("@username", MySqlDbType.VarChar, 50)).Value = string.IsNullOrEmpty(username) ? DBNull.Value : (object)username;
                            cmd.Parameters.Add(new MySqlParameter("@password", MySqlDbType.VarChar, 50)).Value = string.IsNullOrEmpty(password) ? DBNull.Value : (object)password;
                            cmd.Parameters.Add(new MySqlParameter("@firstName", MySqlDbType.VarChar, 50)).Value = string.IsNullOrEmpty(firstName) ? DBNull.Value : (object)firstName;
                            cmd.Parameters.Add(new MySqlParameter("@middleName", MySqlDbType.VarChar, 50)).Value = string.IsNullOrEmpty(middleName) ? DBNull.Value : (object)middleName;
                            cmd.Parameters.Add(new MySqlParameter("@lastName", MySqlDbType.VarChar, 50)).Value = string.IsNullOrEmpty(lastName) ? DBNull.Value : (object)lastName;
                            cmd.Parameters.Add(new MySqlParameter("@gender", MySqlDbType.VarChar, 50)).Value = string.IsNullOrEmpty(gender) ? DBNull.Value : (object)gender;
                            cmd.Parameters.Add(new MySqlParameter("@position", MySqlDbType.VarChar, 50)).Value = string.IsNullOrEmpty(position) ? DBNull.Value : (object)position;
                            cmd.Parameters.Add(new MySqlParameter("@email", MySqlDbType.VarChar, 50)).Value = string.IsNullOrEmpty(email) ? DBNull.Value : (object)email;
                            cmd.Parameters.Add(new MySqlParameter("@birthday", MySqlDbType.VarChar, 50)).Value = string.IsNullOrEmpty(birthday) ? DBNull.Value : (object)birthday;

                            // Execute the query
                            int rowsAffected = cmd.ExecuteNonQuery();

                            // Check if any rows were affected
                            if (rowsAffected > 0)
                            {
                                string actionMessage = (rowsAffected == 1) ? "Record added" : "Record updated";
                                MessageBox.Show($"{actionMessage} successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Clear all fields
                                userbox.Clear();
                                passbox.Clear();
                                firstnamebox.Clear();
                                middlebox.Clear();
                                lastnamebox.Clear();
                                genderbox.SelectedIndex = -1;
                                positionbox.SelectedIndex = -1;
                                emailbox.Clear();
                            }
                            else
                            {
                                MessageBox.Show("No changes made to the record.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception (e.g., display an error message or log the error)
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            

        }

        private void AddEditUser_Load(object sender, EventArgs e)
        {
            positionbox.Items.Add("Registrar");
            positionbox.Items.Add("Admin");
            genderbox.Items.Add("Male");
            genderbox.Items.Add("Female");
        }
    }
}
