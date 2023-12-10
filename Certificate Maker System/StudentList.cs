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

        string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=root;";

        public StudentList()
        {
            InitializeComponent();
            Task.Run(() => PopulateDataGridViewAsync());
           
        }

        public async Task PopulateDataGridViewAsync()
        {


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

        private void deletebtn(object sender, EventArgs e)
        {
            if (studentTable.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = studentTable.SelectedRows[0];

                // Get the lrnNo value from the selected row (assuming it's in the first column)
                string lrnNo = selectedRow.Cells["lrnNo"].Value.ToString();

                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        // Confirm deletion with the user
                        DialogResult result = MessageBox.Show($"Are you sure you want to delete the record with LRN No: {lrnNo}?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            // Delete the record based on the lrnNo
                            string deleteQuery = "DELETE FROM studentlist WHERE lrnNo = @LrnNo";
                            using (MySqlConnector.MySqlCommand cmd = new MySqlConnector.MySqlCommand(deleteQuery, connection))
                            {
                                cmd.Parameters.AddWithValue("@LrnNo", lrnNo);
                                cmd.ExecuteNonQuery();
                            }

                            // Inform the user about successful deletion
                            MessageBox.Show("Record deleted successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshDataGridView();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting record: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void RefreshDataGridView()
        {
            try
            {
                // Assuming you have a method that fetches data and returns a DataTable
                DataTable dataTable = FetchDataFromDatabase();

                // Set the DataTable as the DataSource for the DataGridView
                studentTable.DataSource = null; // Clear the existing data source
                studentTable.DataSource = dataTable; // Set the new data source
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
                    string selectQuery = "SELECT * FROM studentlist";
                    using (MySqlConnector.MySqlDataAdapter adapter = new MySqlConnector.MySqlDataAdapter(selectQuery, connection))
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



        private void editbtn(object sender, EventArgs e)
        {
            
        }
    }
}
