using MySqlConnector;
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
using MySql.Data.MySqlClient;
using MySqlConnection = MySqlConnector.MySqlConnection;

namespace Certificate_Maker_System
{
    public partial class StudentList : UserControl
    {

    
        public StudentList()
        {
            InitializeComponent();
            Task.Run(() => PopulateDataGridViewAsync());
           
        }

        private async Task PopulateDataGridViewAsync()
        {
            // Connection string for MySQL - Modify it according to your database connection
            string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=root;";

            // SQL query to retrieve data
            string query = "SELECT * FROM studentlist";

            // Create a connection and a data adapter
            using (MySqlConnection connection = new MySqlConnector.MySqlConnection(connectionString))
            {
                MySqlConnector.MySqlDataAdapter dataAdapter = new MySqlConnector.MySqlDataAdapter(query, connection);

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

                // Bind the DataTable to the DataGridView
                studentTable.Invoke((MethodInvoker)(() => studentTable.DataSource = dataTable));
            }
        }




        private void addstudentbtn(object sender, EventArgs e)
        {
            AddStudent addStudent = new AddStudent();
            addStudent.Show();
        }

        private void studentTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
