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
        private const string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=;";
        public string userId;
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

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            ManageButton manage = new ManageButton();

                            bool isUpdate = !string.IsNullOrEmpty(userId);

                            // Insert or update into user_info table
                            string userInfoQuery = isUpdate
                                ? "UPDATE user_info SET firstName = @firstName, middleName = @middleName, lastName = @lastName, " +
                                  "gender = @gender, position = @position, email = @email, birthday = @birthday WHERE userId = @userId"
                                : "INSERT INTO user_info (firstName, middleName, lastName, gender, position, email, birthday) " +
                                  "VALUES (@firstName, @middleName, @lastName, @gender, @position, @email, @birthday)";

                            using (MySqlCommand cmdUserInfo = new MySqlCommand(userInfoQuery, connection, transaction))
                            {
                                if (isUpdate)
                                {
                                    cmdUserInfo.Parameters.AddWithValue("@userId", userId);
                                }

                                cmdUserInfo.Parameters.AddWithValue("@firstName", string.IsNullOrEmpty(firstName) ? DBNull.Value : (object)firstName);
                                cmdUserInfo.Parameters.AddWithValue("@middleName", string.IsNullOrEmpty(middleName) ? DBNull.Value : (object)middleName);
                                cmdUserInfo.Parameters.AddWithValue("@lastName", string.IsNullOrEmpty(lastName) ? DBNull.Value : (object)lastName);
                                cmdUserInfo.Parameters.AddWithValue("@gender", string.IsNullOrEmpty(gender) ? DBNull.Value : (object)gender);
                                cmdUserInfo.Parameters.AddWithValue("@position", string.IsNullOrEmpty(position) ? DBNull.Value : (object)position);
                                cmdUserInfo.Parameters.AddWithValue("@email", string.IsNullOrEmpty(email) ? DBNull.Value : (object)email);
                                cmdUserInfo.Parameters.AddWithValue("@birthday", string.IsNullOrEmpty(birthday) ? DBNull.Value : (object)birthday);

                                cmdUserInfo.ExecuteNonQuery();
                            }

                            // If it's an update, update the user_auth table only if the password is provided
                            if (isUpdate && !string.IsNullOrEmpty(password))
                            {
                                string userAuthQuery = "UPDATE user_auth SET username = @username, password = @password WHERE userId = @userId";

                                using (MySqlCommand cmdUserAuth = new MySqlCommand(userAuthQuery, connection, transaction))
                                {
                                    cmdUserAuth.Parameters.AddWithValue("@userId", userId);
                                    cmdUserAuth.Parameters.AddWithValue("@username", string.IsNullOrEmpty(username) ? DBNull.Value : (object)username);
                                    cmdUserAuth.Parameters.AddWithValue("@password", string.IsNullOrEmpty(password) ? DBNull.Value : (object)password);

                                    cmdUserAuth.ExecuteNonQuery();
                                }
                            }
                            else if (!isUpdate)
                            {
                                // Retrieve the automatically generated userId after the insert
                                using (MySqlCommand cmdGetUserId = new MySqlCommand("SELECT LAST_INSERT_ID()", connection, transaction))
                                {
                                    userId = cmdGetUserId.ExecuteScalar().ToString();
                                }

                                // Insert into user_auth table
                                string userAuthQuery = "INSERT INTO user_auth (userId, username, password) " +
                                                        "VALUES (@userId, @username, @password)";

                                using (MySqlCommand cmdUserAuth = new MySqlCommand(userAuthQuery, connection, transaction))
                                {
                                    cmdUserAuth.Parameters.AddWithValue("@userId", userId);
                                    cmdUserAuth.Parameters.AddWithValue("@username", string.IsNullOrEmpty(username) ? DBNull.Value : (object)username);
                                    cmdUserAuth.Parameters.AddWithValue("@password", string.IsNullOrEmpty(password) ? DBNull.Value : (object)password);

                                    cmdUserAuth.ExecuteNonQuery();
                                }
                            }

                            // Commit the transaction if everything is successful
                            transaction.Commit();

                            MessageBox.Show(isUpdate ? "User updated successfully!" : "User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                        catch (Exception ex)
                        {
                            // Rollback the transaction if an error occurs
                            transaction.Rollback();
                            throw; // Re-throw the exception after rollback
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions at a higher level if needed
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
