using MySql.Data.MySqlClient;
using System;
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

                            // Insert or update into user_profiles table (formerly user_info)
                            string userProfileQuery = isUpdate
                                ? "UPDATE user_profiles SET firstName = @firstName, middleName = @middleName, lastName = @lastName, " +
                                  "gender = @gender, position = @position, email = @email, birthday = @birthday WHERE userId = @userId"
                                : "INSERT INTO user_profiles (firstName, middleName, lastName, gender, position, email, birthday) " +
                                  "VALUES (@firstName, @middleName, @lastName, @gender, @position, @email, @birthday)";

                            using (MySqlCommand cmdUserProfile = new MySqlCommand(userProfileQuery, connection, transaction))
                            {
                                if (isUpdate)
                                {
                                    cmdUserProfile.Parameters.AddWithValue("@userId", userId);
                                }

                                cmdUserProfile.Parameters.AddWithValue("@firstName", string.IsNullOrEmpty(firstName) ? DBNull.Value : (object)firstName);
                                cmdUserProfile.Parameters.AddWithValue("@middleName", string.IsNullOrEmpty(middleName) ? DBNull.Value : (object)middleName);
                                cmdUserProfile.Parameters.AddWithValue("@lastName", string.IsNullOrEmpty(lastName) ? DBNull.Value : (object)lastName);
                                cmdUserProfile.Parameters.AddWithValue("@gender", string.IsNullOrEmpty(gender) ? DBNull.Value : (object)gender);
                                cmdUserProfile.Parameters.AddWithValue("@position", string.IsNullOrEmpty(position) ? DBNull.Value : (object)position);
                                cmdUserProfile.Parameters.AddWithValue("@email", string.IsNullOrEmpty(email) ? DBNull.Value : (object)email);
                                cmdUserProfile.Parameters.AddWithValue("@birthday", string.IsNullOrEmpty(birthday) ? DBNull.Value : (object)birthday);

                                cmdUserProfile.ExecuteNonQuery();
                            }

                            // If it's an update, update the users table only if the password is provided
                            if (isUpdate && !string.IsNullOrEmpty(password))
                            {
                                string usersQuery = "UPDATE users SET username = @username, password = @password WHERE userId = @userId";

                                using (MySqlCommand cmdUsers = new MySqlCommand(usersQuery, connection, transaction))
                                {
                                    cmdUsers.Parameters.AddWithValue("@userId", userId);
                                    cmdUsers.Parameters.AddWithValue("@username", string.IsNullOrEmpty(username) ? DBNull.Value : (object)username);
                                    cmdUsers.Parameters.AddWithValue("@password", string.IsNullOrEmpty(password) ? DBNull.Value : (object)password);

                                    cmdUsers.ExecuteNonQuery();
                                }
                            }
                            else if (!isUpdate)
                            {
                                // Retrieve the automatically generated userId after the insert
                                using (MySqlCommand cmdGetUserId = new MySqlCommand("SELECT LAST_INSERT_ID()", connection, transaction))
                                {
                                    userId = cmdGetUserId.ExecuteScalar().ToString();
                                }

                                // Insert into users table (formerly user_auth)
                                string usersQuery = "INSERT INTO users (userId, username, password) " +
                                                   "VALUES (@userId, @username, @password)";

                                using (MySqlCommand cmdUsers = new MySqlCommand(usersQuery, connection, transaction))
                                {
                                    cmdUsers.Parameters.AddWithValue("@userId", userId);
                                    cmdUsers.Parameters.AddWithValue("@username", string.IsNullOrEmpty(username) ? DBNull.Value : (object)username);
                                    cmdUsers.Parameters.AddWithValue("@password", string.IsNullOrEmpty(password) ? DBNull.Value : (object)password);

                                    cmdUsers.ExecuteNonQuery();
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