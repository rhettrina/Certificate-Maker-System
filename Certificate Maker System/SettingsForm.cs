using Certificate_Maker_System.Resources;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Certificate_Maker_System
{
    public partial class SettingsForm : Form
    {
        private const string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=;";
        private CertificateGenerator generator;

        public SettingsForm(CertificateGenerator gen)
        {
            InitializeComponent();
            generator = gen;
        }

        // On form load, pull data from DB: Output Path, Sections, Tracks
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Ensure tables exist first
                EnsureDatabaseTablesExist();

                // Load all data from database
                LoadOutputPathFromDB();
                LoadSectionsFromDB();
                LoadTracksFromDB();

                // Show a status message to confirm data was loaded
                this.Text = "Settings - Data loaded from database";
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error connecting to database: " + ex.Message,
                    "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnsureDatabaseTablesExist()
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Create app_settings table if it doesn't exist
                    string createAppSettingsTable = @"
                        CREATE TABLE IF NOT EXISTS app_settings (
                          id INT PRIMARY KEY,
                          output_path VARCHAR(255)
                        );";

                    // Create sections table if it doesn't exist
                    string createSectionsTable = @"
                        CREATE TABLE IF NOT EXISTS sections (
                          id INT AUTO_INCREMENT PRIMARY KEY,
                          section_name VARCHAR(255) NOT NULL UNIQUE
                        );";

                    // Create tracks table if it doesn't exist
                    string createTracksTable = @"
                        CREATE TABLE IF NOT EXISTS tracks (
                          id INT AUTO_INCREMENT PRIMARY KEY,
                          track_name VARCHAR(255) NOT NULL UNIQUE
                        );";

                    using (var cmd = new MySqlCommand(createAppSettingsTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new MySqlCommand(createSectionsTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new MySqlCommand(createTracksTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Failed to create database tables: " + ex.Message);
            }
        }

        // ------------- OUTPUT PATH -----------------
        private void LoadOutputPathFromDB()
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT output_path FROM app_settings WHERE id = 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            // Display in text box
                            txtOutputPath.Text = result.ToString();
                            // Also store in generator for reference
                            generator.OutputPath = result.ToString();
                        }
                        else
                        {
                            txtOutputPath.Text = "";
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error loading output path: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtOutputPath.Text = fbd.SelectedPath;
                    SaveOutputPathToDB(fbd.SelectedPath); // Save immediately when selected
                    MessageBox.Show("Output path updated.", "Path Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Save to DB so it's persistent
        private void SaveOutputPathToDB(string path)
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    // Check if row exists:
                    string checkQuery = "SELECT COUNT(*) FROM app_settings WHERE id = 1";
                    using (var cmdCheck = new MySqlCommand(checkQuery, conn))
                    {
                        long count = Convert.ToInt64(cmdCheck.ExecuteScalar());

                        if (count == 0)
                        {
                            string ins = "INSERT INTO app_settings (id, output_path) VALUES (1, @path)";
                            using (var cmdIns = new MySqlCommand(ins, conn))
                            {
                                cmdIns.Parameters.AddWithValue("@path", path);
                                cmdIns.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string upd = "UPDATE app_settings SET output_path = @path WHERE id = 1";
                            using (var cmdUpd = new MySqlCommand(upd, conn))
                            {
                                cmdUpd.Parameters.AddWithValue("@path", path);
                                cmdUpd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                // Update generator's path
                generator.OutputPath = path;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error saving output path: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ------------- SECTIONS / TRACKS -----------------

        private void LoadSectionsFromDB()
        {
            try
            {
                comboSection.Items.Clear();
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT section_name FROM sections ORDER BY section_name";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                comboSection.Items.Add(rdr["section_name"].ToString());
                            }
                        }
                    }
                }

                // Show count of loaded sections
                lblSection.Text = $"Section: ({comboSection.Items.Count} loaded)";
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error loading sections: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTracksFromDB()
        {
            try
            {
                comboTrack.Items.Clear();
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT track_name FROM tracks ORDER BY track_name";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                comboTrack.Items.Add(rdr["track_name"].ToString());
                            }
                        }
                    }
                }

                // Show count of loaded tracks
                lblTrack.Text = $"Track: ({comboTrack.Items.Count} loaded)";
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error loading tracks: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddSection_Click(object sender, EventArgs e)
        {
            string newSec = comboSection.Text.Trim();
            if (string.IsNullOrWhiteSpace(newSec))
            {
                MessageBox.Show("Please enter a section name.",
                    "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Insert into DB if not existing
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    // Check duplicates
                    string check = "SELECT COUNT(*) FROM sections WHERE section_name = @sn";
                    long count;

                    using (var cmdCheck = new MySqlCommand(check, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@sn", newSec);
                        count = Convert.ToInt64(cmdCheck.ExecuteScalar());
                    }

                    if (count == 0)
                    {
                        string ins = "INSERT INTO sections (section_name) VALUES (@sn)";
                        using (var cmdIns = new MySqlCommand(ins, conn))
                        {
                            cmdIns.Parameters.AddWithValue("@sn", newSec);
                            cmdIns.ExecuteNonQuery();
                        }
                        MessageBox.Show($"Section '{newSec}' added successfully.",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        comboSection.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("That section already exists.",
                            "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                // Refresh the list from database to ensure we show the latest data
                LoadSectionsFromDB();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error adding section: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteSection_Click(object sender, EventArgs e)
        {
            if (comboSection.SelectedItem == null)
            {
                MessageBox.Show("Please select a section to delete.",
                    "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string toRemove = comboSection.SelectedItem.ToString();
            DialogResult dr = MessageBox.Show($"Are you sure you want to remove the section '{toRemove}'?",
                "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    using (var conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string del = "DELETE FROM sections WHERE section_name = @sn";
                        using (var cmdDel = new MySqlCommand(del, conn))
                        {
                            cmdDel.Parameters.AddWithValue("@sn", toRemove);
                            int rowsAffected = cmdDel.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show($"Section '{toRemove}' deleted successfully.",
                                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }

                    // Refresh the list from database
                    LoadSectionsFromDB();
                    comboSection.Text = "";
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error deleting section: " + ex.Message,
                        "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAddTrack_Click(object sender, EventArgs e)
        {
            string newTrack = comboTrack.Text.Trim();
            if (string.IsNullOrWhiteSpace(newTrack))
            {
                MessageBox.Show("Please enter a track name.",
                    "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string check = "SELECT COUNT(*) FROM tracks WHERE track_name = @tn";
                    long count;

                    using (var cmdCheck = new MySqlCommand(check, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@tn", newTrack);
                        count = Convert.ToInt64(cmdCheck.ExecuteScalar());
                    }

                    if (count == 0)
                    {
                        string ins = "INSERT INTO tracks (track_name) VALUES (@tn)";
                        using (var cmdIns = new MySqlCommand(ins, conn))
                        {
                            cmdIns.Parameters.AddWithValue("@tn", newTrack);
                            cmdIns.ExecuteNonQuery();
                        }
                        MessageBox.Show($"Track '{newTrack}' added successfully.",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        comboTrack.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("That track already exists.",
                            "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                // Refresh the list from database
                LoadTracksFromDB();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error adding track: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteTrack_Click(object sender, EventArgs e)
        {
            if (comboTrack.SelectedItem == null)
            {
                MessageBox.Show("Please select a track to delete.",
                    "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string toRemove = comboTrack.SelectedItem.ToString();
            DialogResult dr = MessageBox.Show($"Are you sure you want to remove track '{toRemove}'?",
                "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    using (var conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string del = "DELETE FROM tracks WHERE track_name = @tn";
                        using (var cmdDel = new MySqlCommand(del, conn))
                        {
                            cmdDel.Parameters.AddWithValue("@tn", toRemove);
                            int rowsAffected = cmdDel.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show($"Track '{toRemove}' deleted successfully.",
                                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }

                    // Refresh the list from database
                    LoadTracksFromDB();
                    comboTrack.Text = "";
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error deleting track: " + ex.Message,
                        "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ------------- CLOSE (SAVE CHANGES) -----------------
        private void btnClose_Click(object sender, EventArgs e)
        {
            // Save output path if it's been changed
            if (!string.IsNullOrWhiteSpace(txtOutputPath.Text))
            {
                SaveOutputPathToDB(txtOutputPath.Text);
            }

            MessageBox.Show("All settings have been saved.",
                "Settings Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}