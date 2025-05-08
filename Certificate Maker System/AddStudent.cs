using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Certificate_Maker_System
{
    public partial class AddStudent : Form
    {
        private const string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=;";
        private MySqlConnection connection;
        private string labelgender;
        private string gender;
        private StudentList parentStudentList;

        public AddStudent(StudentList parentList)
        {
            InitializeComponent();
            this.parentStudentList = parentList;
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void AddStudent_Load(object sender, EventArgs e)
        {
            gradebox.Items.Add("Grade 11");
            gradebox.Items.Add("Grade 12");
            sectionbox.Items.Add("Sampaguita");
            sectionbox.Items.Add("Adelfa");
            sectionbox.Items.Add("Bougainvillea");
            trackbox.Items.Add("GAS");
            trackbox.Items.Add("STEM");
            trackbox.Items.Add("AGRICULTURE");
            trackbox.Items.Add("HOME ECONOMICS");
        }

        private void cancelbtn(object sender, EventArgs e)
        {
            this.Close();
        }

        private void savebtn(object sender, EventArgs e)
        {
            // 1) Ensure LRN has exactly 11 digits:
            if (lrnbox.Text.Length != 11)
            {
                MessageBox.Show("LRN must be exactly 11 digits. Please correct it to continue.",
                                "Invalid LRN",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return; // Stop execution if it's not exactly 11 digits
            }
            if (IsAllFieldsFilled())
            {
                try
                {
                    long lrnNo = Convert.ToInt64(lrnbox.Text);
                    string lastName = lastnamebox.Text;
                    string firstName = firstnamebox.Text;
                    string middleName = middlebox.Text;
                    // In the savebtn method (around line 68)
                    string birthDate = birthdaybox.Value.ToString("yyyy-MM-dd");

                    string grade = gradebox.Text;
                    string section = sectionbox.Text;
                    string gender = labelgender;
                    string address = addressbox.Text;
                    string track = trackbox.Text;

                    string query = "INSERT INTO students (lrnNo, lastName, firstName, middleName, birthDate, gender, address) " +
                                   "VALUES (@lrnNo, @lastName, @firstName, @middleName, @birthDate, @gender, @address) " +
                                   "ON DUPLICATE KEY UPDATE " +
                                   "lastName = @lastName, firstName = @firstName, middleName = @middleName, birthDate = @birthDate, " +
                                   "gender = @gender, address = @address";

                    // Updated query for student_academic (formerly student_details)
                    string queryAcademic = "INSERT INTO student_academic (lrnNo, grade, section, track) " +
                                          "VALUES (@lrnNo, @grade, @section, @track) " +
                                          "ON DUPLICATE KEY UPDATE " +
                                          "grade = @grade, section = @section, track = @track";

                    string successMessage = "";

                    using (connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        using (MySqlTransaction transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                using (MySqlCommand cmd = new MySqlCommand(query, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@lrnNo", lrnNo);
                                    cmd.Parameters.AddWithValue("@lastName", lastName);
                                    cmd.Parameters.AddWithValue("@firstName", firstName);
                                    cmd.Parameters.AddWithValue(
     "@middleName",
     string.IsNullOrEmpty(middleName) ? (object)DBNull.Value : middleName
 );
                                    // In AddStudent.cs, savebtn method
                                    cmd.Parameters.AddWithValue("@birthDate", DateTime.Parse(birthDate));

                                    cmd.Parameters.AddWithValue("@gender", string.IsNullOrEmpty(gender) ? (object)DBNull.Value : gender);
                                    cmd.Parameters.AddWithValue("@address", string.IsNullOrEmpty(address) ? (object)DBNull.Value : address);

                                    int rowsAffected = cmd.ExecuteNonQuery();

                                    // Handle the result as needed
                                    if (rowsAffected > 0)
                                    {
                                        successMessage += "Student information saved successfully!\n";
                                    }
                                }

                                // Updated command to use the student_academic table
                                using (MySqlCommand cmdAcademic = new MySqlCommand(queryAcademic, connection, transaction))
                                {
                                    cmdAcademic.Parameters.AddWithValue("@lrnNo", lrnNo);
                                    cmdAcademic.Parameters.AddWithValue("@grade", string.IsNullOrEmpty(grade) ? (object)DBNull.Value : grade);
                                    cmdAcademic.Parameters.AddWithValue("@section", string.IsNullOrEmpty(section) ? (object)DBNull.Value : section);
                                    cmdAcademic.Parameters.AddWithValue("@track", string.IsNullOrEmpty(track) ? (object)DBNull.Value : track);

                                    int rowsAffectedAcademic = cmdAcademic.ExecuteNonQuery();

                                    // Handle the result as needed
                                    if (rowsAffectedAcademic > 0)
                                    {
                                        successMessage += "Student academic details saved successfully!";
                                    }
                                }

                                // Commit the transaction only once after both queries have been executed
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        // Display combined success message if applicable
                        if (!string.IsNullOrEmpty(successMessage))
                        {
                            MessageBox.Show(successMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            parentStudentList.RefreshDataGridView();
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill in all required fields.");
            }
        }

        private bool IsAllFieldsFilled()
        {
            return !string.IsNullOrEmpty(lrnbox.Text) &&
                   !string.IsNullOrEmpty(lastnamebox.Text) &&
                   !string.IsNullOrEmpty(firstnamebox.Text) &&
                   DateTime.TryParse(birthdaybox.Text, out _) &&
                   !string.IsNullOrEmpty(gradebox.Text) &&
                   !string.IsNullOrEmpty(sectionbox.Text) &&
                   !string.IsNullOrEmpty(labelgender) &&
                   !string.IsNullOrEmpty(addressbox.Text) &&
                   !string.IsNullOrEmpty(trackbox.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void grade_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label11_Click(object sender, EventArgs e)
        {
        }

        private void lrnbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers (0-9) and control keys like backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            gender = "male";
            labelgender = gender;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gender = "female";
            labelgender = gender;
        }

        private void labelgender_Click(object sender, EventArgs e)
        {
        }

        private void trackbox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void close(object sender, EventArgs e)
        {
            this.Close();
            StudentList studentList = new StudentList();
            studentList.RefreshDataGridView();
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }
    }
}