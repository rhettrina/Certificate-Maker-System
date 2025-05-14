using DocumentFormat.OpenXml.Packaging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Certificate_Maker_System.Resources
{
    public partial class CertificateGenerator : UserControl
    {
        private const string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=;";
        public string OutputPath { get; set; }

        public string selectedTemplate;
        private MySqlConnection connection;

        // Map friendly names to template files if needed (optional)
        private Dictionary<string, string> templatePaths = new Dictionary<string, string>();

        // A WebBrowser to display the HTML preview
        private WebBrowser webPreview;

        // Debounce timer to wait briefly before regenerating preview
        private Timer previewTimer;

        public CertificateGenerator()
        {
            InitializeComponent();
            connection = new MySqlConnection(connectionString);

            // If you have physical templates, map them here; otherwise, skip
            templatePaths["Certificate of Enrolment"] = "enrolment.docx";
            templatePaths["Good Moral"] = "good moral.docx";
            templatePaths["Graduate"] = "graduate.docx";

            // Initialize the HTML preview control
            InitializePreviewControl();
            InitializeDatabase();

            // Set up a timer to trigger preview generation after typing stops
            previewTimer = new Timer
            {
                Interval = 800 // milliseconds
            };
            previewTimer.Tick += (s, e) =>
            {
                previewTimer.Stop();
                UpdatePreview();
            };
        }

        private void lrn_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow control keys (like Backspace) and digits 0-9
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void lrn_Leave(object sender, EventArgs e)
        {
            // On leaving, confirm exactly 11 digits were entered
            if (lrn.Text.Length != 11)
            {
                MessageBox.Show("LRN must be exactly 11 digits.",
                                "LRN Validation Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                lrn.Focus();
            }
        }

        // At the top of the CertificateGenerator class, add:
        private void LoadAppSettings()
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
                            OutputPath = result.ToString();
                        }
                        else
                        {
                            // Default path if not set in database
                            OutputPath = Path.Combine(Environment.GetFolderPath(
                                Environment.SpecialFolder.MyDocuments), "Generated_Certificates");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading application settings: " + ex.Message,
                    "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Default path if database fails
                OutputPath = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments), "Generated_Certificates");
            }
        }

        private void LoadSectionsFromDatabase()
        {
            try
            {
                gradebox.Items.Clear();

                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT section_name FROM sections ORDER BY section_name";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                gradebox.Items.Add(reader["section_name"].ToString());
                            }
                        }
                    }
                }

                // Select first item if available
                if (gradebox.Items.Count > 0)
                    gradebox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sections: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTracksFromDatabase()
        {
            try
            {
                trackbox.Items.Clear();

                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT track_name FROM tracks ORDER BY track_name";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                trackbox.Items.Add(reader["track_name"].ToString());
                            }
                        }
                    }
                }

                // Select first item if available
                if (trackbox.Items.Count > 0)
                    trackbox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tracks: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CertificateGenerator_Load(object sender, EventArgs e)
        {
            // Initialize dropdowns, default text, etc.
            try
            {
                types.Items.Clear();
                types.Items.Add("Certificate of Enrolment");
                types.Items.Add("Good Moral");
                types.Items.Add("Graduate");

                // Load settings from database
                LoadAppSettings();

                // Load sections and tracks from database
                LoadSectionsFromDatabase();
                LoadTracksFromDatabase();

                semesterBox.SelectedIndex = 0;
                startYearCombo.SelectedIndex = 0;
                endYearCombo.SelectedIndex = 1;
                issueDatePicker.Value = DateTime.Today;

                registrarBox.Text = "FLORABEL A. PLACIDO";
                principalBox.Text = "MARLO FIEL P. SULTAN, Ed. D.";

                // Hook text-change events for real-time preview
                lrn.TextChanged += (s, ev) => TriggerPreview();
                namebox.TextChanged += (s, ev) => TriggerPreview();
                gradebox.SelectedIndexChanged += (s, ev) => TriggerPreview();
                trackbox.SelectedIndexChanged += (s, ev) => TriggerPreview();
                semesterBox.SelectedIndexChanged += (s, ev) => TriggerPreview();
                startYearCombo.SelectedIndexChanged += (s, ev) => TriggerPreview();
                endYearCombo.SelectedIndexChanged += (s, ev) => TriggerPreview();
                purposeBox.TextChanged += (s, ev) => TriggerPreview();
                issueDatePicker.ValueChanged += (s, ev) => TriggerPreview();
                registrarBox.TextChanged += (s, ev) => TriggerPreview();
                principalBox.TextChanged += (s, ev) => TriggerPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on load: " + ex.Message);
            }
        }

        // Simple method to set up the WebBrowser inside panelPreview
        private void InitializePreviewControl()
        {
            webPreview = new WebBrowser
            {
                Dock = DockStyle.Fill,
                ScriptErrorsSuppressed = true,
                AllowWebBrowserDrop = false
            };
            panelPreview.Controls.Clear();
            panelPreview.Controls.Add(webPreview);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedTemplate = types.SelectedItem?.ToString() ?? "";

            if (selectedTemplate == "Certificate of Enrolment")
            {
                // Toggle UI controls
                labelRegistrar.Visible = true;
                registrarBox.Visible = true;
                labelPrincipal.Visible = false;
                principalBox.Visible = false;

                labelSemester.Visible = true;
                semesterBox.Visible = true;

                labelPurpose.Visible = true;
                purposeBox.Visible = true;
            }
            else if (selectedTemplate == "Good Moral")
            {
                labelRegistrar.Visible = false;
                registrarBox.Visible = false;
                labelPrincipal.Visible = true;
                principalBox.Visible = true;

                labelSemester.Visible = false;
                semesterBox.Visible = false;

                labelPurpose.Visible = false;
                purposeBox.Visible = false;
            }
            else if (selectedTemplate == "Graduate")
            {
                labelRegistrar.Visible = false;
                registrarBox.Visible = false;
                labelPrincipal.Visible = true;
                principalBox.Visible = true;

                labelSemester.Visible = false;
                semesterBox.Visible = false;

                labelPurpose.Visible = true;
                purposeBox.Visible = true;
            }

            TriggerPreview();
        }

        // Button for generating the final "certificate" (here we just show a message)
        // You can adapt this to actually produce a file if desired.
        // Add this method to generate the actual document
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lrn.Text) ||
        string.IsNullOrWhiteSpace(selectedTemplate) ||
        string.IsNullOrWhiteSpace(namebox.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Determine template path based on selected certificate type
                string templatePath = Path.Combine(Application.StartupPath, "Templates",
                    templatePaths[selectedTemplate]);

                if (!File.Exists(templatePath))
                {
                    MessageBox.Show($"Template file not found: {templatePaths[selectedTemplate]}",
                        "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Create output directory if it doesn't exist
                // Use the path from database settings instead of hardcoded path
                string outputDir = OutputPath;
                Directory.CreateDirectory(outputDir);

                // Rest of your existing code remains the same...
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string safeName = new string(namebox.Text.Where(c => !Path.GetInvalidFileNameChars().Contains(c)).ToArray());
                string outputDoc = Path.Combine(outputDir, $"{safeName}_{timestamp}.docx");

                // Continue with file generation as before...
                File.Copy(templatePath, outputDoc);

                // 5. Determine gender prefix based on name or other logic
                string genderPrefix = DetermineGenderPrefix(namebox.Text);

                // 6. School year combined value
                string schoolYear = $"{startYearCombo.Text}-{endYearCombo.Text}";

                // 7. Issue date components
                DateTime issueDateValue = issueDatePicker.Value;

                // 8. Get track, grade section, etc.
                string track = trackbox.Text.Trim();
                string gradeSection = gradebox.Text.Trim();
                string semester = semesterBox.Visible ? semesterBox.Text : "";
                string purpose = purposeBox.Text.Trim();
                string registrar = registrarBox.Text.Trim();
                string principal = principalBox.Text.Trim();

                // 9. Apply the replacement logic from your code
                using (WordprocessingDocument doc = WordprocessingDocument.Open(outputDoc, true))
                {
                    var replacements = new List<(string placeholder, string value)>
            {
                ("##STUDENT_NAME##", namebox.Text),
                ("##LRN##", lrn.Text),
                ("##GRADE_SECTION##", gradeSection ?? ""),
                ("##TRACK##", track ?? ""),
                ("##SEMESTER##", semester ?? ""),
                ("##SCHOOL_YEAR##", schoolYear ?? ""),
                ("##PURPOSE##", purpose ?? ""),
                ("##ISSUE_DAY##", issueDateValue.ToString("dd")),
                ("##ISSUE_MONTH##", issueDateValue.ToString("MMMM")),
                ("##ISSUE_YEAR##", issueDateValue.ToString("yyyy")),
                ("##REGISTRAR_NAME##", registrar ?? ""),
                ("##PRINCIPAL##", principal ?? ""),
                ("##GENDER_PREFIX##", genderPrefix)
            };

                    // Handle special case for "he/she"
                    string heOrShe = genderPrefix == "Mr." ? "He" : "She";
                    replacements.Add(("##HE_SHE##", heOrShe));
                    replacements.Add(("##HE/SHE##", heOrShe));
                    replacements.Add(("HE/SHE", heOrShe));

                    // Add possessive variants
                    string hisOrHer = genderPrefix == "Mr." ? "his" : "her";
                    replacements.Add(("##HIS_HER##", hisOrHer));
                    replacements.Add(("##HIS/HER##", hisOrHer));

                    var body = doc.MainDocumentPart.Document.Body;

                    foreach (var paragraph in body.Descendants<DocumentFormat.OpenXml.Wordprocessing.Paragraph>())
                    {
                        foreach (var text in paragraph.Descendants<DocumentFormat.OpenXml.Wordprocessing.Text>())
                        {
                            string currentText = text.Text;
                            bool textChanged = false;

                            foreach (var (placeholder, value) in replacements)
                            {
                                if (currentText.Contains(placeholder))
                                {
                                    currentText = currentText.Replace(placeholder, value ?? "");
                                    textChanged = true;
                                }
                            }

                            if (textChanged)
                            {
                                text.Text = currentText;
                            }
                        }
                    }

                    doc.MainDocumentPart.Document.Save();
                }

                // 10. Show success message and open the file
                MessageBox.Show($"Certificate generated successfully!\nSaved to: {outputDoc}", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Optionally open the document
                System.Diagnostics.Process.Start(outputDoc);

                // 11. Save to history if needed
                SaveCertificateHistory(lrn.Text, selectedTemplate);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating certificate: {ex.Message}",
                    "Generation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Add this method to help with initial database setup
        public void InitializeDatabase()
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Create tables if they don't exist (just in case)
                    string createAppSettingsTable = @"
                CREATE TABLE IF NOT EXISTS app_settings (
                    id INT PRIMARY KEY,
                    output_path VARCHAR(255)
                );";

                    string createSectionsTable = @"
                CREATE TABLE IF NOT EXISTS sections (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    section_name VARCHAR(255) NOT NULL UNIQUE
                );";

                    string createTracksTable = @"
                CREATE TABLE IF NOT EXISTS tracks (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    track_name VARCHAR(255) NOT NULL UNIQUE
                );";

                    // Execute the create table statements
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

                    // Insert default output path if not exists
                    string checkSettings = "SELECT COUNT(*) FROM app_settings WHERE id = 1";
                    int settingsCount = 0;

                    using (var cmd = new MySqlCommand(checkSettings, conn))
                    {
                        settingsCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    if (settingsCount == 0)
                    {
                        string defaultPath = Path.Combine(Environment.GetFolderPath(
                            Environment.SpecialFolder.MyDocuments), "Generated_Certificates");

                        string insertSettings = "INSERT INTO app_settings (id, output_path) VALUES (1, @path)";

                        using (var cmd = new MySqlCommand(insertSettings, conn))
                        {
                            cmd.Parameters.AddWithValue("@path", defaultPath);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Add sample sections if table is empty
                    string checkSections = "SELECT COUNT(*) FROM sections";
                    int sectionsCount = 0;

                    using (var cmd = new MySqlCommand(checkSections, conn))
                    {
                        sectionsCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    if (sectionsCount == 0)
                    {
                        string[] defaultSections = {
                    "12 - A", "12 - B", "12 - C",
                    "11 - A", "11 - B", "11 - C"
                };

                        foreach (string section in defaultSections)
                        {
                            string insertSection = "INSERT INTO sections (section_name) VALUES (@name)";

                            using (var cmd = new MySqlCommand(insertSection, conn))
                            {
                                cmd.Parameters.AddWithValue("@name", section);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Add sample tracks if table is empty
                    string checkTracks = "SELECT COUNT(*) FROM tracks";
                    int tracksCount = 0;

                    using (var cmd = new MySqlCommand(checkTracks, conn))
                    {
                        tracksCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    if (tracksCount == 0)
                    {
                        string[] defaultTracks = {
                    "ABM", "HUMSS", "STEM", "TVL-ICT", "TVL-HE", "SPORTS"
                };

                        foreach (string track in defaultTracks)
                        {
                            string insertTrack = "INSERT INTO tracks (track_name) VALUES (@name)";

                            using (var cmd = new MySqlCommand(insertTrack, conn))
                            {
                                cmd.Parameters.AddWithValue("@name", track);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing database: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method to determine gender prefix (you can implement your own logic)
        private string DetermineGenderPrefix(string name)
        {
            // This is a simplified example - in a real application,
            // you might want to store the gender in your database
            // or have a more sophisticated mechanism
            return "Mr."; // Default value
        }

        // Demonstration of saving to a hypothetical certificate_history table
        private void SaveCertificateHistory(string lrnNo, string certificateType)
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO certificate_history
                          (lrn_no, student_name, certificate_type, generated_date)
                        VALUES
                          (@lrnNo, @studentName, @certType, @generatedDate);";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@lrnNo", lrnNo);
                        cmd.Parameters.AddWithValue("@studentName", namebox.Text.Trim());
                        cmd.Parameters.AddWithValue("@certType", certificateType);
                        cmd.Parameters.AddWithValue("@generatedDate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // Swallow or rethrow as needed
                throw;
            }
        }

        // Search logic (LRN or partial name)
        private void searchbtn(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchbox.Text) ||
                searchbox.Text == "Search LRN or Student Name")
            {
                MessageBox.Show("Please enter an LRN or student name to search.", "Search",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string searchValue = searchbox.Text.Trim();

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT s.lrnNo,
                               CONCAT(s.firstName, ' ',
                                      IFNULL(s.middleName, ''),
                                      ' ',
                                      s.lastName) AS fullName,
                               sa.grade,
                               sa.section,
                               sa.track
                        FROM students s
                        JOIN student_academic sa ON s.lrnNo = sa.lrnNo
                        WHERE s.lrnNo LIKE @search
                           OR CONCAT(s.firstName, ' ', s.middleName, ' ', s.lastName) LIKE @search
                        LIMIT 1;";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%");
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lrn.Text = reader["lrnNo"].ToString();
                                namebox.Text = reader["fullName"].ToString().Replace("  ", " ");
                                gradebox.Text = $"{reader["grade"]} - {reader["section"]}";
                                trackbox.Text = reader["track"].ToString();
                            }
                            else
                            {
                                MessageBox.Show(
                                    "No data found for the provided search criteria.",
                                    "Search",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching: " + ex.Message,
                    "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchenter(object sender, EventArgs e)
        {
            if (searchbox.Text == "Search LRN or Student Name")
            {
                searchbox.Text = "";
                searchbox.ForeColor = Color.Black;
            }
        }

        private void searchleave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchbox.Text))
            {
                searchbox.Text = "Search LRN or Student Name";
                searchbox.ForeColor = Color.Gray;
            }
        }

        private void buttonBatchGenerate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Batch generation functionality is planned for a future update.",
                "Coming Soon", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --------------- PREVIEW WITH HTML (no external library) ---------------

        /// <summary>
        /// Called to schedule a preview update after the user stops typing.
        /// </summary>
        private void TriggerPreview()
        {
            previewTimer.Stop();
            previewTimer.Start();
        }

        /// <summary>
        /// Called by the timer to rebuild the preview HTML and load in the WebBrowser.
        /// </summary>
        private void UpdatePreview()
        {
            if (string.IsNullOrWhiteSpace(selectedTemplate))
                return;

            string html = GeneratePreviewHtml();
            webPreview.DocumentText = html;
        }

        /// <summary>
        /// Generates a small HTML snippet to show in the WebBrowser preview,
        /// based on selected template and field inputs.
        /// </summary>
        /// <summary>
        /// Generates an HTML preview that closely mimics the actual Word document templates.
        /// </summary>
        /// <summary>
        /// Generates an HTML preview that closely mimics the actual Word document templates,
        /// including a school header with logo.
        /// </summary>
        private string GeneratePreviewHtml()
        {
            // Determine gender prefix (simplified example)
            string genderPrefix = DetermineGenderPrefix(namebox.Text);
            string heOrShe = genderPrefix == "Mr." ? "He" : "She";
            string hisOrHer = genderPrefix == "Mr." ? "his" : "her";

            // Basic data (same as your replacement list)
            string studentName = namebox.Text.Trim();
            string studentLRN = lrn.Text.Trim();
            string gradeSection = gradebox.Text.Trim();
            string track = trackbox.Text.Trim();
            string semester = semesterBox.Visible ? semesterBox.Text : "";
            string schoolYear = $"{startYearCombo.Text}-{endYearCombo.Text}";
            string purpose = purposeBox.Text.Trim();

            // Date components
            DateTime issueDate = issueDatePicker.Value;
            string issueDay = issueDate.ToString("dd");
            string issueMonth = issueDate.ToString("MMMM");
            string issueYear = issueDate.ToString("yyyy");

            // Signatories
            string registrar = registrarBox.Text.Trim();
            string principal = principalBox.Text.Trim();

            // Use a simple relative path for the logo - it will be relative to the HTML file
            string logoPath = "@Resources\\jpnhs_logo.png";

            // Base HTML with styles
            string html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>Certificate Preview</title>
    <style>
        body {{
            font-family: 'Times New Roman', serif;
            margin: 0;
            padding: 20px;
            background-color: #f5f5f5;
        }}
        .preview-container {{
            width: 100%;
            max-width: 800px;
            margin: 0 auto;
            background-color: white;
            padding: 40px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            min-height: 800px;
        }}
        .header {{
            text-align: center;
            margin-bottom: 30px;
            border-bottom: 2px solid #000;
            padding-bottom: 15px;
        }}
        .header-logo {{
            max-width: 80px;
            height: auto;
            margin-bottom: 10px;
        }}
        .header-text {{
            margin: 0;
            padding: 0;
        }}
        .school-name {{
            font-size: 18px;
            font-weight: bold;
            margin: 0;
            text-transform: uppercase;
        }}
        .school-address {{
            font-size: 12px;
            margin: 5px 0;
        }}
        h1 {{
            text-align: center;
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 30px;
            margin-top: 30px;
            letter-spacing: 3px;
        }}
        p {{
            line-height: 1.6;
            font-size: 14px;
            text-align: justify;
            margin-bottom: 10px;
        }}
        .signature {{
            margin-top: 60px;
            text-align: center;
        }}
        .signatory-name {{
            font-weight: bold;
            text-transform: uppercase;
            margin-bottom: 0;
        }}
        .signatory-title {{
            font-style: italic;
            margin-top: 0;
        }}
        .indent {{
            text-indent: 40px;
        }}
        .seal-notice {{
            margin-top: 80px;
            font-style: italic;
            font-size: 12px;
            position: relative;
            left: 40px;
        }}
    </style>
</head>
<body>
    <div class='preview-container'>
        <div class='header'>
";

            // Add logo with a simple relative path
            html += $@"<img src='{logoPath}' class='header-logo' alt='School Logo' onerror='this.style.display=""none""'><br>";

            // Add school header text
            html += @"
            <div class='doc-header'>
                <p>Republic of the Philippines</p>
                <p>Department of Education</p>
                <p>Region V – Bicol</p>
                <p>SCHOOLS DIVISION OFFICE OF CAMARINES NORTE</p>
                <p>JOSE PANGANIBAN NATIONAL HIGH SCHOOL</p>
                <p>Jose Panganiban, Camarines Norte</p>
            </div>
";

            // Different content based on template type
            if (selectedTemplate == "Certificate of Enrolment")
            {
                // Based on enrolment.docx template - Exact match to document
                html += $@"
        <h1>CERTIFICATE OF ENROLMENT</h1>

        <p>To Whom It May Concern:</p>

        <p class='indent'>This is to certify that {studentName} with Learners Reference Number {studentLRN} is enrolled as Grade {gradeSection} {track} in this school, this {semester} SY {schoolYear}.</p>

        <p class='indent'>Issued this {issueDay} day of {issueMonth} {issueYear} upon request of the party concerned for {purpose}.</p>

        <div class='signature'>
            <p class='signatory-name'>{registrar}</p>
            <p class='signatory-title'>Registrar I</p>
        </div>

        <p class='seal-notice'>Not Valid without<br>JPNHS Official Seal</p>
";
            }
            else if (selectedTemplate == "Good Moral")
            {
                // Based on good moral.docx template - Exact match to document
                html += $@"
        <h1>CERTIFICATE OF GOOD MORAL CHARACTER</h1>

        <p>To Whom It May Concern:</p>

        <p class='indent'>This is to certify that {studentName} with LRN {studentLRN} is a graduate of {track} in this school under the K to 12 Curriculum, this SY {schoolYear}.</p>

        <p class='indent'>{heOrShe} is known to be a student with good moral character and has no record of misdemeanor.</p>

        <p class='indent'>Issued this {issueDay} day of {issueMonth} {issueYear} upon request of the party concerned for whatever legal purpose/s this may serve.</p>

        <div class='signature'>
            <p class='signatory-name'>{principal}</p>
            <p class='signatory-title'>School Principal III</p>
        </div>

        <p class='seal-notice'>Not valid without JPNHS<br>Official Seal</p>
";
            }
            else if (selectedTemplate == "Graduate")
            {
                // Based on graduate.docx template - Exact match to document
                html += $@"
        <h1>C  E  R  T  I  F  I  C  A  T  I  O  N</h1>

        <p>To Whom It May Concern:</p>

        <p class='indent'>This is to certify that {studentName} with LRN {studentLRN} graduated Senior High School in this institution, during the School Year {schoolYear} under the K to 12 Curriculum.</p>

        <p class='indent'>This further certifies that {studentName} completed the course {track}</p>

        <p class='indent'>Issued this {issueDay} day of {issueMonth}, {issueYear} upon the request of the party concerned for whatever legal purpose this may serve.</p>

        <div class='signature'>
            <p class='signatory-name'>{principal}</p>
            <p class='signatory-title'>School Principal III</p>
        </div>

        <p class='seal-notice'>Not Valid without<br>JPNHS Official Seal</p>
";
            }
            else
            {
                // Default placeholder if no template selected
                html += "<p style='text-align:center; color:#777; margin-top:100px;'>Please select a certificate type</p>";
            }

            html += @"
    </div>
</body>
</html>";

            return html;
        }
    }
}