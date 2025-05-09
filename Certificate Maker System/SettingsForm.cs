using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using Certificate_Maker_System.Resources;

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
            LoadOutputPathFromDB();
            LoadSectionsFromDB();
            LoadTracksFromDB();
        }

        // ------------- OUTPUT PATH -----------------
        private void LoadOutputPathFromDB()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT setting_value FROM app_settings WHERE setting_key='OUTPUT_PATH' LIMIT 1";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null)
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

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtOutputPath.Text = fbd.SelectedPath;
                }
            }
        }

        // Save to DB so it’s persistent
        private void SaveOutputPathToDB(string path)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // check if row exists:
                string checkQuery = "SELECT COUNT(*) FROM app_settings WHERE setting_key='OUTPUT_PATH'";
                long count = (long)new MySqlCommand(checkQuery, conn).ExecuteScalar();

                if (count == 0)
                {
                    string ins = "INSERT INTO app_settings (setting_key, setting_value) VALUES ('OUTPUT_PATH', @val)";
                    using (var cmdIns = new MySqlCommand(ins, conn))
                    {
                        cmdIns.Parameters.AddWithValue("@val", path);
                        cmdIns.ExecuteNonQuery();
                    }
                }
                else
                {
                    string upd = "UPDATE app_settings SET setting_value=@val WHERE setting_key='OUTPUT_PATH'";
                    using (var cmdUpd = new MySqlCommand(upd, conn))
                    {
                        cmdUpd.Parameters.AddWithValue("@val", path);
                        cmdUpd.ExecuteNonQuery();
                    }
                }
            }
        }

        // ------------- SECTIONS / TRACKS -----------------

        private void LoadSectionsFromDB()
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
        }

        private void LoadTracksFromDB()
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
        }

        private void btnAddSection_Click(object sender, EventArgs e)
        {
            string newSec = comboSection.Text.Trim();
            if (string.IsNullOrWhiteSpace(newSec)) return;

            // Insert into DB if not existing
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // Check duplicates
                string check = "SELECT COUNT(*) FROM sections WHERE section_name=@sn";
                long count = 0;
                using (var cmdCheck = new MySqlCommand(check, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@sn", newSec);
                    count = (long)cmdCheck.ExecuteScalar();
                }
                if (count == 0)
                {
                    string ins = "INSERT INTO sections (section_name) VALUES (@sn)";
                    using (var cmdIns = new MySqlCommand(ins, conn))
                    {
                        cmdIns.Parameters.AddWithValue("@sn", newSec);
                        cmdIns.ExecuteNonQuery();
                    }
                    MessageBox.Show("Section added: " + newSec);
                }
                else
                {
                    MessageBox.Show("That section already exists.");
                }
            }
            LoadSectionsFromDB();
        }

        private void btnDeleteSection_Click(object sender, EventArgs e)
        {
            if (comboSection.SelectedItem == null) return;
            string toRemove = comboSection.SelectedItem.ToString();
            DialogResult dr = MessageBox.Show("Are you sure to remove section '" + toRemove + "'?",
                "Delete Confirmation", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string del = "DELETE FROM sections WHERE section_name=@sn LIMIT 1";
                    using (var cmdDel = new MySqlCommand(del, conn))
                    {
                        cmdDel.Parameters.AddWithValue("@sn", toRemove);
                        cmdDel.ExecuteNonQuery();
                    }
                }
                LoadSectionsFromDB();
            }
        }

        private void btnAddTrack_Click(object sender, EventArgs e)
        {
            string newTrack = comboTrack.Text.Trim();
            if (string.IsNullOrWhiteSpace(newTrack)) return;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string check = "SELECT COUNT(*) FROM tracks WHERE track_name=@tn";
                long count = 0;
                using (var cmdCheck = new MySqlCommand(check, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@tn", newTrack);
                    count = (long)cmdCheck.ExecuteScalar();
                }
                if (count == 0)
                {
                    string ins = "INSERT INTO tracks (track_name) VALUES (@tn)";
                    using (var cmdIns = new MySqlCommand(ins, conn))
                    {
                        cmdIns.Parameters.AddWithValue("@tn", newTrack);
                        cmdIns.ExecuteNonQuery();
                    }
                    MessageBox.Show("Track added: " + newTrack);
                }
                else
                {
                    MessageBox.Show("That track already exists.");
                }
            }
            LoadTracksFromDB();
        }

        private void btnDeleteTrack_Click(object sender, EventArgs e)
        {
            if (comboTrack.SelectedItem == null) return;
            string toRemove = comboTrack.SelectedItem.ToString();
            DialogResult dr = MessageBox.Show("Are you sure to remove track/strand '" + toRemove + "'?",
                "Delete Confirmation", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string del = "DELETE FROM tracks WHERE track_name=@tn LIMIT 1";
                    using (var cmdDel = new MySqlCommand(del, conn))
                    {
                        cmdDel.Parameters.AddWithValue("@tn", toRemove);
                        cmdDel.ExecuteNonQuery();
                    }
                }
                LoadTracksFromDB();
            }
        }

        // ------------- CLOSE (SAVE CHANGES) -----------------
        // For example, we explicitly save the output path DB on “Done”:
        private void btnClose_Click(object sender, EventArgs e)
        {
            // If the user typed a path, store in generator & DB
            if (!string.IsNullOrWhiteSpace(txtOutputPath.Text))
            {
                generator.OutputPath = txtOutputPath.Text;
                SaveOutputPathToDB(txtOutputPath.Text);
            }
            Close();
        }
    }
}