using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Certificate_Maker_System
{
    public partial class History : UserControl
    {
        private const string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=;";

        public History()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Clear history button
            DialogResult result = MessageBox.Show(
                "Are you sure you want to clear the entire certificate history?",
                "Confirm Clear History",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                ClearHistory();
            }
        }

        private void ClearHistory()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "TRUNCATE TABLE certificate_history";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Refresh the data grid
                    LoadHistoryData();
                    MessageBox.Show("Certificate history has been cleared.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error clearing history: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell click if needed
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Handle label click if needed
        }

        private void History_Load(object sender, EventArgs e)
        {
            // Set up the DataGridView columns
            SetupDataGridView();

            // Load data from the database
            LoadHistoryData();
        }

        private void SetupDataGridView()
        {
            // Clear existing columns
            dataGridView1.Columns.Clear();

            // Add columns
            dataGridView1.Columns.Add("history_id", "ID");
            dataGridView1.Columns.Add("student_name", "Student Name");
            dataGridView1.Columns.Add("lrn_no", "LRN Number");
            dataGridView1.Columns.Add("certificate_type", "Certificate Type");
            dataGridView1.Columns.Add("generated_date", "Generated Date");

            // Format columns
            dataGridView1.Columns["history_id"].Width = 50;
            dataGridView1.Columns["student_name"].Width = 180;
            dataGridView1.Columns["lrn_no"].Width = 100;
            dataGridView1.Columns["certificate_type"].Width = 150;
            dataGridView1.Columns["generated_date"].Width = 150;

            // Set other properties
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        private void LoadHistoryData()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT ch.history_id, 
                                    CONCAT(s.firstName, ' ', s.middleName, ' ', s.lastName) AS student_name,
                                    ch.lrn_no, 
                                    ch.certificate_type, 
                                    ch.generated_date
                                    FROM certificate_history ch
                                    JOIN students s ON ch.lrn_no = s.lrnNo
                                    ORDER BY ch.generated_date DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Clear existing rows
                            dataGridView1.Rows.Clear();

                            // Add data to the grid
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add(
                                    reader["history_id"],
                                    reader["student_name"],
                                    reader["lrn_no"],
                                    reader["certificate_type"],
                                    Convert.ToDateTime(reader["generated_date"]).ToString("yyyy-MM-dd HH:mm:ss")
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading history data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
