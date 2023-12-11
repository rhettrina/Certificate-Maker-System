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
                    using (MySqlCommand cmdCheck = connection.CreateCommand())
                    {
                        cmdCheck.CommandText = "SELECT COUNT(*) FROM studentlist WHERE lrnNo = @LrnNo";
                        cmdCheck.Parameters.Add("@LrnNo", MySqlDbType.VarChar).Value = (object)lrnbox.Text ?? DBNull.Value;

                        int existingRecords = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        using (MySqlCommand cmd = connection.CreateCommand())
                        {
                            if (existingRecords > 0)
                            {
                                // Update the existing record
                                cmd.CommandText = "UPDATE studentlist SET lastName = @LastName, " +
                                                  "firstName = @FirstName, middleName = @MiddleName, " +
                                                  "birthDate = @BirthDate, grade = @Grade, " +
                                                  "section = @Section, gender = @Gender, " +
                                                  "address = @Address, track = @Track " +
                                                  "WHERE lrnNo = @LrnNo";
                            }
                            else
                            {
                                // Insert a new record
                                cmd.CommandText = "INSERT INTO studentlist (lrnNo, lastName, firstName, middleName, " +
                                                  "birthDate, grade, section, gender, address, track) " +
                                                  "VALUES (@LrnNo, @LastName, @FirstName, @MiddleName, " +
                                                  "@BirthDate, @Grade, @Section, @Gender, @Address, @Track)";
                            }

                            // Clear existing parameters before adding new ones
                            cmd.Parameters.Clear();

                            // Add parameters to prevent SQL injection
                            cmd.Parameters.Add("@LrnNo", MySqlDbType.VarChar).Value = (object)lrnbox.Text ?? DBNull.Value;
                            cmd.Parameters.Add("@LastName", MySqlDbType.VarChar).Value = (object)lastnamebox.Text ?? DBNull.Value;
                            cmd.Parameters.Add("@FirstName", MySqlDbType.VarChar).Value = (object)firstnamebox.Text ?? DBNull.Value;
                            cmd.Parameters.Add("@MiddleName", MySqlDbType.VarChar).Value = (object)middlebox.Text ?? DBNull.Value;

                            // Convert the string to a DateTime value
                            if (DateTime.TryParse(birthdaybox.Text, out DateTime birthDate))
                            {
                                cmd.Parameters.Add("@BirthDate", MySqlDbType.Date).Value = birthDate;
                            }
                            else
                            {
                                // Handle the case where the DateTime conversion fails
                                MessageBox.Show("Invalid birth date format.");
                                return;
                            }

                            cmd.Parameters.Add("@Grade", MySqlDbType.VarChar).Value = (object)gradebox.Text ?? DBNull.Value;
                            cmd.Parameters.Add("@Section", MySqlDbType.VarChar).Value = (object)sectionbox.Text ?? DBNull.Value;
                            cmd.Parameters.Add("@Gender", MySqlDbType.VarChar).Value = (object)labelgender ?? DBNull.Value;
                            cmd.Parameters.Add("@Address", MySqlDbType.VarChar).Value = (object)addressbox.Text ?? DBNull.Value;
                            cmd.Parameters.Add("@Track", MySqlDbType.VarChar).Value = (object)trackbox.Text ?? DBNull.Value;

                            cmd.ExecuteNonQuery();

                            MessageBox.Show(existingRecords > 0 ? "Data updated successfully!" : "Data inserted successfully!");

                            // Clear the form
                            lrnbox.Clear();
                            lastnamebox.Clear();
                            firstnamebox.Clear();
                            middlebox.Clear();
                            addressbox.Clear();
                            gradebox.SelectedIndex = -1;
                            sectionbox.SelectedIndex = -1;
                            trackbox.SelectedIndex = -1;

                            // Refresh the DataGridView
                            StudentList studentList = new StudentList();
                            studentList.RefreshDataGridView();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error processing data: {ex.Message}");
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
