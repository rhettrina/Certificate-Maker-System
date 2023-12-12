using MySql.Data.MySqlClient;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Certificate_Maker_System
{

    public partial class AddStudent : Form
    {
        private const string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=root;";
        private MySqlConnection connection;
        string labelgender;
        string gender;
        public AddStudent()
        {
            InitializeComponent();
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
            if (IsAllFieldsFilled())
            {
                try
                {
                    
                    int lrnNo = Convert.ToInt32(lrnbox.Text);
                    string lastName = lastnamebox.Text;
                    string firstName = firstnamebox.Text;
                    string middleName = middlebox.Text;
                    string birthDate = birthdaybox.Text;
                    string grade = gradebox.Text;
                    string section = sectionbox.Text;
                    string gender = labelgender;
                    string address = addressbox.Text;
                    string track = trackbox.Text;

                    // Use a parameterized query to prevent SQL injection
                    string query = "INSERT INTO studentlist (lrnNo, lastName, firstName, middleName, birthDate, grade, section, gender, address, track) " +
                                   "VALUES (@lrnNo, @lastName, @firstName, @middleName, @birthDate, @grade, @section, @gender, @address, @track) " +
                                   "ON DUPLICATE KEY UPDATE " +
                                   "lastName = @lastName, firstName = @firstName, middleName = @middleName, birthDate = @birthDate, " +
                                   "grade = @grade, section = @section, gender = @gender, address = @address, track = @track";

                    using (connection = new MySqlConnection(connectionString))
                    {
                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            connection.Open();

                            // Add parameters with null checks
                            cmd.Parameters.Add(new MySqlParameter("@lrnNo", MySqlDbType.Int32)).Value = (object)lrnNo ?? DBNull.Value;
                            cmd.Parameters.Add(new MySqlParameter("@lastName", MySqlDbType.VarChar)).Value = (object)lastName ?? DBNull.Value;
                            cmd.Parameters.Add(new MySqlParameter("@firstName", MySqlDbType.VarChar)).Value = (object)firstName ?? DBNull.Value;
                            cmd.Parameters.Add(new MySqlParameter("@middleName", MySqlDbType.VarChar)).Value = (object)middleName ?? DBNull.Value;
                            cmd.Parameters.Add(new MySqlParameter("@birthDate", MySqlDbType.VarChar)).Value = string.IsNullOrEmpty(birthDate) ? DBNull.Value : (object)birthDate;
                            cmd.Parameters.Add(new MySqlParameter("@grade", MySqlDbType.VarChar)).Value = string.IsNullOrEmpty(grade) ? DBNull.Value : (object)grade;
                            cmd.Parameters.Add(new MySqlParameter("@section", MySqlDbType.VarChar)).Value = string.IsNullOrEmpty(section) ? DBNull.Value : (object)section;
                            cmd.Parameters.Add(new MySqlParameter("@gender", MySqlDbType.VarChar)).Value = string.IsNullOrEmpty(gender) ? DBNull.Value : (object)gender;
                            cmd.Parameters.Add(new MySqlParameter("@address", MySqlDbType.VarChar)).Value = string.IsNullOrEmpty(address) ? DBNull.Value : (object)address;
                            cmd.Parameters.Add(new MySqlParameter("@track", MySqlDbType.VarChar)).Value = string.IsNullOrEmpty(track) ? DBNull.Value : (object)track;

                            // Execute the query
                            int rowsAffected = cmd.ExecuteNonQuery();

                            // Check if any rows were affected
                            if (rowsAffected > 0)
                            {
                                string actionMessage = (rowsAffected == 1) ? "Record added" : "Record updated";
                                MessageBox.Show($"{actionMessage} for {firstName} {lastName} successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Clear all fields
                                lrnbox.Clear();
                                lastnamebox.Clear();
                                firstnamebox.Clear();
                                middlebox.Clear();
                                addressbox.Clear();
                                gradebox.SelectedIndex = -1;
                                sectionbox.SelectedIndex = -1;
                                trackbox.SelectedIndex = -1;
                                StudentList studentList = new StudentList();
                                studentList.RefreshDataGridView();
                            }
                            else
                            {
                                MessageBox.Show("No changes made to the record.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception (e.g., display an error message or log the error)
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
                   !string.IsNullOrEmpty(middlebox.Text) &&
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
    }
}
