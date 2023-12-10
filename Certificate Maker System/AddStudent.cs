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
            InitializeDatabaseConnection();
        }

        private void InitializeDatabaseConnection()
        {
            // Create a MySqlConnection object
            connection = new MySqlConnection(connectionString);

            try
            {
                // Open the connection
                connection.Open();
                Console.WriteLine("Database connection opened successfully!");
            }
            catch (Exception ex)
            {
            }
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
                    using (MySqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO studentlist (lrnNo, lastName, firstName, middleName, birthDate, grade, section, gender, address, track) " +
                                          "VALUES (@LrnNo, @LastName, @FirstName, @MiddleName, @BirthDate, @Grade, @Section, @Gender, @Address, @Track)";

                        // Add parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@LrnNo", (object)lrnbox.Text ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@LastName", (object)lastnamebox.Text ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@FirstName", (object)firstnamebox.Text ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MiddleName", (object)middlebox.Text ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@BirthDate", (object)birthdaybox.Text ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Grade", (object)gradebox.Text ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Section", (object)sectionbox.Text ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Gender", (object)labelgender ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Address", (object)addressbox.Text ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Track", (object)trackbox.Text ?? DBNull.Value);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data inserted successfully!");
                        lrnbox.Clear();
                        lastnamebox.Clear();
                        firstnamebox.Clear();
                        middlebox.Clear();
                        addressbox.Clear();
                        gradebox.SelectedIndex = -1; // Set to default or -1 if no default
                        sectionbox.SelectedIndex = -1; // Set to default or -1 if no default
                        trackbox.SelectedIndex = -1;
                        StudentList studentList = new StudentList();
                        studentList.RefreshDataGridView();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error inserting data: {ex.Message}");
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
                   DateTime.TryParse(birthdaybox.Text, out _) && // Check if it's a valid DateTime
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
