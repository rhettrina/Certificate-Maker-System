using MySqlConnector;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Certificate_Maker_System
{
    public partial class StudentList : UserControl
    {
        private string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=;ConvertZeroDateTime=True;";

        public StudentList()
        {
            InitializeComponent();// Connect the event handler
            cbStatusFilter.SelectedIndexChanged += cbStatusFilter_SelectedIndexChanged;

            Task.Run(() => PopulateDataGridViewAsync());
        }

        public async Task PopulateDataGridViewAsync()
        {
            // Updated SQL query to use student_academic instead of student_details
            string query = "SELECT s.lrnNo, s.lastName, s.firstName, s.middleName, s.birthDate, s.gender, s.address, sa.grade, sa.section, sa.track " +
                           "FROM students s " +
                           "LEFT JOIN student_academic sa ON s.lrnNo = sa.lrnNo";

            // Check the ComboBox selection
            string selectedStatus = cbStatusFilter.SelectedItem?.ToString();
            if (selectedStatus != null && selectedStatus != "All")
            {
                // For "Enrolled" or "Graduated", apply a WHERE
                query += " WHERE sa.status = @StatusFilter";
            }

            // Create a connection and a data adapter
            using (MySqlConnection connection = new MySqlConnector.MySqlConnection(connectionString))
            {
                MySqlConnector.MySqlDataAdapter dataAdapter = new MySqlConnector.MySqlDataAdapter(query, connection);

                // Add the parameter to the adapter if we're filtering
                if (selectedStatus != null && selectedStatus != "All")
                {
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@StatusFilter", selectedStatus);
                }

                // Create a DataTable to store the data
                DataTable dataTable = new DataTable();

                try
                {
                    // Open the connection asynchronously
                    await connection.OpenAsync();

                    // Fill the DataTable with the data from the database
                    dataAdapter.Fill(dataTable);

                    // Add after filling the dataTable
                    if (dataTable.Columns.Contains("birthDate"))
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row["birthDate"] != DBNull.Value)
                            {
                                DateTime date = Convert.ToDateTime(row["birthDate"]);
                                row["birthDate"] = date.ToString("yyyy-MM-dd");
                            }
                        }
                    }
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
            AddStudent addStudent = new AddStudent(this);

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
                                    // Delete record from student_academic table (formerly student_details)
                                    string deleteAcademicQuery = "DELETE FROM student_academic WHERE lrnNo = @LrnNo";

                                    using (MySqlConnector.MySqlCommand cmdAcademic = new MySqlConnector.MySqlCommand(deleteAcademicQuery, connection, transaction))
                                    {
                                        cmdAcademic.Parameters.AddWithValue("@LrnNo", lrnNo);
                                        cmdAcademic.ExecuteNonQuery();
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

                    // Updated fetch query to use student_academic instead of student_details
                    string selectQuery = "SELECT s.lrnNo, s.lastName, s.firstName, s.middleName, s.birthDate, s.gender, s.address, sa.grade, sa.section, sa.track " +
                                         "FROM students s " +
                                         "INNER JOIN student_academic sa ON s.lrnNo = sa.lrnNo";

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
            AddStudent addStudent = new AddStudent(this);

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
            studentTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            studentTable.MultiSelect = false;
            // Example code if you added a ComboBox in the designer named cbStatusFilter:
            cbStatusFilter.Items.Clear();
            cbStatusFilter.Items.Add("Enrolled");
            cbStatusFilter.Items.Add("Graduated");
            cbStatusFilter.Items.Add("All");
            cbStatusFilter.SelectedIndex = 0; // default to "Enrolled"

            // Call your method to populate the DataGridView, filtered
            PopulateDataGridViewAsync();
        }

        private void cbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDataGridViewAsync();
        }

        private async void btnMarkAsGraduate_Click(object sender, EventArgs e)
        {
            // 1) Make sure a student row is selected
            if (studentTable.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a student to mark as Graduated.",
                    "No Student Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2) Retrieve the selected student’s LRN
            DataGridViewRow selectedRow = studentTable.SelectedRows[0];
            string lrnNo = selectedRow.Cells["lrnNo"].Value.ToString();

            // 3) Ask confirmation
            DialogResult confirm = MessageBox.Show(
                $"Are you sure you want to mark LRN {lrnNo} as Graduated?",
                "Mark as Graduate",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (confirm != DialogResult.Yes) return;

            // 4) Perform the update on student_academic
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    string updateQuery = "UPDATE student_academic SET status = 'Graduated' WHERE lrnNo = @LRN";
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@LRN", lrnNo);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                MessageBox.Show("Student successfully marked as Graduated!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 5) Refresh or re-populate the DataGridView
                await PopulateDataGridViewAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonBulkAdd_Click(object sender, EventArgs e)
        {
            // Open the new BulkAddStudents form
            BulkAddStudents bulkAddForm = new BulkAddStudents(this);
            bulkAddForm.ShowDialog();
        }
    }
}