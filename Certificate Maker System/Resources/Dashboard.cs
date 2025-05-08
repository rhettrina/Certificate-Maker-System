using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Certificate_Maker_System.Resources
{
    public partial class Dashboard : UserControl
    {
        private const string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=;";

        public Dashboard()
        {
            InitializeComponent();
            DateTime currentDate = DateTime.Now;

            // Format the date as "dddd, MMMM dd" (day of the week, full month name, day of the month)
            string formattedDate = currentDate.ToString("dddd, MMMM dd");

            // Set the formatted date to the label
            datenow.Text = formattedDate;
            UpdateGenderCounts();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            // Handle any additional load logic here if needed
        }

        private void UpdateGenderCounts()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Count males
                    string maleCountQuery = "SELECT COUNT(*) FROM students WHERE gender = 'male' COLLATE utf8mb4_general_ci";
                    using (MySqlCommand maleCommand = new MySqlCommand(maleCountQuery, connection))
                    {
                        int maleCount = Convert.ToInt32(maleCommand.ExecuteScalar());

                        // Count females
                        string femaleCountQuery = "SELECT COUNT(*) FROM students WHERE gender = 'female' COLLATE utf8mb4_general_ci";
                        using (MySqlCommand femaleCommand = new MySqlCommand(femaleCountQuery, connection))
                        {
                            int femaleCount = Convert.ToInt32(femaleCommand.ExecuteScalar());

                            int totalCount = maleCount + femaleCount;
                            totalno.Text = totalCount.ToString();
                            maleno.Text = maleCount.ToString();
                            femaleno.Text = femaleCount.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., display an error message)
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Optional paint handling
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            // Optional paint handling
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Optional event handling
        }

        private void label5_Click(object sender, EventArgs e)
        {
            // Optional event handling
        }

        private void label7_Click(object sender, EventArgs e)
        {
            // Optional event handling
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            // Optional event handling for other calendars if needed
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optional event handling for combo box
        }

        // This is the click event for the "Add Student" button
        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            // Instantiate and show the AddStudent form
            AddStudent addStudentForm = new AddStudent(null);
            addStudentForm.Show();
        }
    }
}