using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Windows.Forms;

namespace Certificate_Maker_System
{
    public partial class BulkAddStudents : Form
    {
        // Connection string to your MySQL database
        private const string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=;";

        // Reference to StudentList to refresh the grid after import
        private StudentList parentStudentList;

        public BulkAddStudents(StudentList parent)
        {
            InitializeComponent();
            parentStudentList = parent;
        }

        /// <summary>
        /// Browse CSV button - opens a file dialog to pick a .csv file,
        /// then loads its lines into the multiline text box.
        /// </summary>
        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.Title = "Select CSV File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Read all lines from the CSV file
                        string[] allLines = File.ReadAllLines(openFileDialog.FileName);
                        // Join them and display in the textbox:
                        txtBulk.Text = string.Join(Environment.NewLine, allLines);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error opening file: {ex.Message}",
                            "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Import Students button - parses the CSV lines in txtBulk
        /// and does a bulk insert/update into MySQL.
        /// </summary>
        private void btnImport_Click(object sender, EventArgs e)
        {
            // Get each nonempty line from the textbox
            string[] lines = txtBulk.Text
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length == 0)
            {
                MessageBox.Show("No data found. Please paste or load a CSV file first.");
                return;
            }

            int totalProcessed = 0;
            int lineNumber = 0;

            // Use a database transaction for all inserts/updates
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (string line in lines)
                        {
                            lineNumber++;
                            // Example layout (10 fields):
                            // LRN,FirstName,MiddleName,LastName,BirthDate,Gender,Address,Grade,Section,Track
                            string[] parts = line.Split(',');

                            if (parts.Length < 10)
                            {
                                // If a row is missing fields, skip or throw an error:
                                // throw new Exception($"Line {lineNumber} has incomplete fields.");
                                continue;
                            }

                            // Trim each field
                            string lrn = parts[0].Trim();
                            string firstName = parts[1].Trim();
                            string middle = parts[2].Trim();
                            string lastName = parts[3].Trim();
                            string bDateStr = parts[4].Trim();
                            string gender = parts[5].Trim();
                            string address = parts[6].Trim();
                            string grade = parts[7].Trim();
                            string section = parts[8].Trim();
                            string track = parts[9].Trim();

                            // Validate LRN - must be 11 digits in many PH contexts
                            if (lrn.Length != 11)
                            {
                                // Skip or throw
                                // throw new Exception($"Line {lineNumber}: LRN must be 11 digits.");
                                continue;
                            }

                            // Parse birthdate
                            if (!DateTime.TryParse(bDateStr, out DateTime birthDate))
                            {
                                // throw new Exception($"Line {lineNumber}: Invalid birthdate '{bDateStr}'.");
                                continue;
                            }

                            // Insert or update in 'students'
                            string queryStudents = @"
INSERT INTO students
    (lrnNo, lastName, firstName, middleName, birthDate, gender, address)
VALUES
    (@lrnNo, @lastName, @firstName, @middleName, @birthDate, @gender, @address)
ON DUPLICATE KEY UPDATE
    lastName   = @lastName,
    firstName  = @firstName,
    middleName = @middleName,
    birthDate  = @birthDate,
    gender     = @gender,
    address    = @address;";

                            using (MySqlCommand cmd = new MySqlCommand(queryStudents, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@lrnNo", lrn);
                                cmd.Parameters.AddWithValue("@lastName", lastName);
                                cmd.Parameters.AddWithValue("@firstName", firstName);
                                cmd.Parameters.AddWithValue("@middleName", middle);
                                cmd.Parameters.AddWithValue("@birthDate", birthDate);
                                cmd.Parameters.AddWithValue("@gender", gender);
                                cmd.Parameters.AddWithValue("@address", address);
                                cmd.ExecuteNonQuery();
                            }

                            // Insert or update in 'student_academic'
                            string queryAcademic = @"
INSERT INTO student_academic
    (lrnNo, grade, section, track)
VALUES
    (@lrnNo, @grade, @section, @track)
ON DUPLICATE KEY UPDATE
    grade   = @grade,
    section = @section,
    track   = @track;";

                            using (MySqlCommand cmdAcad = new MySqlCommand(queryAcademic, conn, transaction))
                            {
                                cmdAcad.Parameters.AddWithValue("@lrnNo", lrn);
                                cmdAcad.Parameters.AddWithValue("@grade", grade);
                                cmdAcad.Parameters.AddWithValue("@section", section);
                                cmdAcad.Parameters.AddWithValue("@track", track);
                                cmdAcad.ExecuteNonQuery();
                            }

                            totalProcessed++;
                        }

                        // If we got here, everything is good; commit
                        transaction.Commit();
                        MessageBox.Show(
                            $"Bulk import successful!\nLines processed: {lines.Length}\nImported or updated: {totalProcessed}",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    catch (Exception ex)
                    {
                        // Rollback on error
                        transaction.Rollback();
                        MessageBox.Show(
                            $"Error during import (line {lineNumber}): {ex.Message}",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }

            // Refresh the main StudentList’s DataGridView
            parentStudentList.RefreshDataGridView();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}