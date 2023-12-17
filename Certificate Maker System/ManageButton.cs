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
                string query = "SELECT * FROM user_info";

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
    }
}
