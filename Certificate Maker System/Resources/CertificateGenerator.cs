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

namespace Certificate_Maker_System.Resources
{

    public partial class CertificateGenerator : UserControl
    {
        private const string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=;";
        public CertificateGenerator()
        {
            InitializeComponent();
            searchbox.TabStop = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void CertificateGenerator_Load(object sender, EventArgs e)
        {
            types.Items.Add("Certificate of Grades");
            types.Items.Add("Certificate of Registrar");
            types.Items.Add("Certificate of Enrollment");
            types.Items.Add("Good Moral");
        }

        private void searchenter(object sender, EventArgs e)
        {
            if (searchbox.Text == "Search LRN or Student Name")
            {
                // Clear the text when the TextBox receives focus
                searchbox.Text = "";
                searchbox.ForeColor = Color.Black;
            }
        }

        private void searchleave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchbox.Text))
            {
                // If it's empty, set the default text back
                searchbox.Text = "Search LRN or Student Name";
                // Set the text color back to gray
                searchbox.ForeColor = Color.Gray;
            }
        }


        private async void searchbtn(object sender, EventArgs e)
        {

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string searchCriteria = searchbox.Text;

                    string query = "SELECT s.lrnNo, CONCAT(s.firstName, ' ', s.middleName, ' ', s.lastName) AS fullName, sd.grade, sd.section, sd.track " +
                                   "FROM students s " +
                                   "JOIN student_details sd ON s.lrnNo = sd.lrnNo " +
                                   "WHERE s.lrnNo LIKE @searchCriteria OR " +
                                   "CONCAT(s.firstName, ' ', s.middleName, ' ', s.lastName) LIKE @searchCriteria";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@searchCriteria", "%" + searchCriteria + "%");

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lrn.Text = reader["lrnNo"].ToString();
                                namebox.Text = reader["fullName"].ToString();
                                gradebox.Text = $"{reader["grade"]} - {reader["section"]}";
                                trackbox.Text = reader["track"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("No data found for the provided search criteria.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
Queryable

        }

    }
}
