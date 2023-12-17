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
    public partial class ManageButton : UserControl
    {
        string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=;";
        public ManageButton()
        {
            InitializeComponent();
            Task.Run(() => PopulateUserInfoDataGridViewAsync());
        }

        private void adduserbtn(object sender, EventArgs e)
        {
            AddEditUser addEditUser = new AddEditUser();
            addEditUser.Show();
        }


        public async Task PopulateUserInfoDataGridViewAsync()
        {
            // SQL query to retrieve data from the user_info table
            string query = "SELECT userId, firstName, middleName, lastName, gender, position, email, birthday FROM user_info";

            // Create a connection and a data adapter
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, connection);

                // Create a DataTable to store the data
                DataTable dataTable = new DataTable();

                try
                {
                    // Open the connection asynchronously
                    await connection.OpenAsync();

                    // Fill the DataTable with the data from the database
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Close the connection
                    connection.Close();
                }

                // Bind the DataTable to the DataGridView (replace studentTable with your DataGridView name)
                managetable.Invoke((MethodInvoker)(() => managetable.DataSource = dataTable));
            }
        }


        public void RefreshDataGridView()
        {
            try
            {
                // Fetch the data from the database
                DataTable dataTable = FetchDataFromDatabase();

                // Set the DataTable as the DataSource for the DataGridView
                managetable.Invoke((MethodInvoker)(() => managetable.DataSource = dataTable));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing DataGridView: {ex.Message}");
            }
        }

        // Example method to fetch data from the database
        private DataTable FetchDataFromDatabase()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch the data from your table (adjust the query based on your table structure)
                    string selectQuery = "SELECT userId, firstName, middleName, lastName, gender, position, email, birthday FROM user_info";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, connection))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching data: {ex.Message}");
            }

            return dataTable;
        }


    }
}
