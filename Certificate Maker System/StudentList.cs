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

        string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=;";

        public StudentList()
        {
            InitializeComponent();
            Task.Run(() => PopulateDataGridViewAsync());
           
        }

        public async Task PopulateDataGridViewAsync()
        {


            // SQL query to retrieve data
            string query = "SELECT s.lrnNo, s.lastName, s.firstName, s.middleName, s.birthDate, s.gender, s.address, sd.grade, sd.section, sd.track " +
                               "FROM students s " +
                               "LEFT JOIN student_details sd ON s.lrnNo = sd.lrnNo";

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
                            using (MySqlConnector.MySqlTransaction transaction = connection.BeginTransaction())
                            {
                                try
                                {
                                    // Delete record from student_details table
                                    string deleteDetailsQuery = "DELETE FROM student_details WHERE lrnNo = @LrnNo";

                                    using (MySqlConnector.MySqlCommand cmdDetails = new MySqlConnector.MySqlCommand(deleteDetailsQuery, connection, transaction))
                                    {
                                        cmdDetails.Parameters.AddWithValue("@LrnNo", lrnNo);
                                        cmdDetails.ExecuteNonQuery();
                                    }

                                    // Then, delete the record from students table
                                    string deleteQuery = "DELETE FROM students WHERE lrnNo = @LrnNo";

                                    using (MySqlConnector.MySqlCommand cmd = new MySqlConnector.MySqlCommand(deleteQuery, connection, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@LrnNo", lrnNo);
                                        cmd.ExecuteNonQuery();
                                    }

                                    // Commit the transaction if everything is successful
                                    transaction.Commit();

                                    // Inform the user about successful deletion
                                    MessageBox.Show("Record deleted successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    RefreshDataGridView();
                                }
                                catch (Exception ex)
                                {
                                    // Rollback the transaction if an error occurs
                                    transaction.Rollback();
                                    MessageBox.Show($"Error deleting record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string selectQuery = "SELECT s.lrnNo, s.lastName, s.firstName, s.middleName, s.birthDate, s.gender, s.address, sd.grade, sd.section, sd.track " +
                      "FROM students s " +
                      "INNER JOIN student_details sd ON s.lrnNo = sd.lrnNo";

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
            AddStudent addStudent = new AddStudent();

            if (studentTable.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = studentTable.SelectedRows[0];

                // Extract data from the selected row
                string lrnNo = selectedRow.Cells["lrnNo"].Value?.ToString();
                string lastName = selectedRow.Cells["lastName"].Value?.ToString();
                string firstName = selectedRow.Cells["firstName"].Value?.ToString();
                string middleName = selectedRow.Cells["middleName"].Value?.ToString();
                string date = selectedRow.Cells["birthDate"].Value?.ToString();
                string grade = selectedRow.Cells["grade"].Value?.ToString();
                string section = selectedRow.Cells["section"].Value?.ToString();
                string gender = selectedRow.Cells["gender"].Value?.ToString();
                string track = selectedRow.Cells["track"].Value?.ToString();
                string address = selectedRow.Cells["address"].Value?.ToString();

                // Show the AddStudent form
                addStudent.Show();

                // Set the values in the AddStudent form
                addStudent.lrnbox.Text = lrnNo;
                addStudent.lastnamebox.Text = lastName;
                addStudent.firstnamebox.Text = firstName;
                addStudent.middlebox.Text = middleName;
                addStudent.addressbox.Text = address;
                addStudent.gradebox.Text = grade;
                addStudent.sectionbox.Text = section;
                addStudent.trackbox.Text = track;
                addStudent.labelforform.Text = "Edit Student Form";
                addStudent.birthdaybox.Text = date;

                // Check the radio button based on gender
                addStudent.maleradio.Checked = string.Equals(gender, "male", StringComparison.OrdinalIgnoreCase);
                addStudent.femaleradio.Checked = string.Equals(gender, "female", StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                MessageBox.Show("Please select a row in the table.");
            }

        }

        private void StudentList_Load(object sender, EventArgs e)
        {

        }
    }
}
