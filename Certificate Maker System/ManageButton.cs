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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Certificate_Maker_System
{
    public partial class ManageButton : UserControl
    {
        string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=;";
        private MySqlConnection connection;
        public string getuserid;
        public ManageButton()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            LoadUserData();
        }

        private void adduserbtn(object sender, EventArgs e)
        {
            AddEditUser addEditUser = new AddEditUser();
            addEditUser.Show();
        }

        private void InitializeDatabaseConnection()
        {
            // Initialize the MySqlConnection object with your connection string
            connection = new MySqlConnection(connectionString);
        }

        private void LoadUserData()
        {
            try
            {
                // Open the database connection
                connection.Open();

                // Query to select all rows from the user_info table
                string query = @"SELECT
                            LPAD(ui.userId, 4, '0') AS userId,
                            ui.firstName,
                            ui.middleName,
                            ui.lastName,
                            ui.gender,
                            ui.position,
                            ui.email,
                            ui.birthday,
                            ua.username,
                            ua.password
                        FROM
                            user_info ui
                        LEFT JOIN
                            user_auth ua ON ui.userId = ua.userId";

                // Create a MySqlCommand object
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Create a DataTable to store the data
                DataTable dataTable = new DataTable();

                // Create a MySqlDataAdapter to fill the DataTable with data
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                // Fill the DataTable
                adapter.Fill(dataTable);

                // Set the DataTable as the DataSource for the DataGridView
                managetable.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the database connection
                connection.Close();
            }
        }


        private void ManageButton_Load(object sender, EventArgs e)
        {

        }

        private void deletebtn(object sender, EventArgs e)
        {
            // Ensure that a row is selected in the DataGridView
            if (managetable.SelectedRows.Count > 0)
            {
                try
                {
                    // Get the userId of the selected row
                    int userId = Convert.ToInt32(managetable.SelectedRows[0].Cells["userId"].Value);

                    // Open the database connection
                    connection.Open();

                    // Begin a transaction to ensure both DELETE statements succeed or fail together
                    using (MySqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Delete from user_auth table
                            string deleteAuthQuery = "DELETE FROM user_auth WHERE userId = @userId";
                            MySqlCommand deleteAuthCmd = new MySqlCommand(deleteAuthQuery, connection, transaction);
                            deleteAuthCmd.Parameters.AddWithValue("@userId", userId);
                            deleteAuthCmd.ExecuteNonQuery();

                            // Delete from user_info table
                            string deleteInfoQuery = "DELETE FROM user_info WHERE userId = @userId";
                            MySqlCommand deleteInfoCmd = new MySqlCommand(deleteInfoQuery, connection, transaction);
                            deleteInfoCmd.Parameters.AddWithValue("@userId", userId);
                            deleteInfoCmd.ExecuteNonQuery();

                            // Commit the transaction if both DELETE statements succeed
                            transaction.Commit();

                            MessageBox.Show("User deleted successfully.");
                        }
                        catch (Exception ex)
                        {
                            // Rollback the transaction if there's an error
                            transaction.Rollback();
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    // Close the database connection
                    connection.Close();

                    // Refresh the DataGridView after deletion
                    LoadUserData();
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }


        private void editbtn(object sender, EventArgs e)
        {
            AddEditUser addEditUser = new AddEditUser();
            addEditUser.labelforform.Text = "Edit User";

            if (managetable.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = managetable.SelectedRows[0];

                // Extract data from the selected row
                string user = selectedRow.Cells["userId"].Value?.ToString();
                string lastName = selectedRow.Cells["firstName"].Value?.ToString();
                string firstName = selectedRow.Cells["middleName"].Value?.ToString();
                string middleName = selectedRow.Cells["lastName"].Value?.ToString();
                string date = selectedRow.Cells["gender"].Value?.ToString();
                string grade = selectedRow.Cells["position"].Value?.ToString();
                string section = selectedRow.Cells["email"].Value?.ToString();
                string gender = selectedRow.Cells["birthday"].Value?.ToString();
                string track = selectedRow.Cells["username"].Value?.ToString();
                string address = selectedRow.Cells["password"].Value?.ToString();

                // Show the AddStudent form
                addEditUser.Show();

                // Set the values in the AddStudent form
                addEditUser.userId = user;
                addEditUser.firstnamebox.Text = lastName;
                addEditUser.middlebox.Text = firstName;
                addEditUser.lastnamebox.Text = middleName;
                addEditUser.genderbox.Text = date;
                addEditUser.emailbox.Text = section;
                addEditUser.positionbox.Text = grade;
                addEditUser.birthdaybox.Text = gender;
                addEditUser.userbox.Text = track;
                addEditUser.passbox.Text = address;
            }
            else
            {
                MessageBox.Show("Please select a row in the table.");
            }
        }
    }
}
