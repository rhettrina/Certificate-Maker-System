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

        private void CertificateGenerator_Load(object sender, EventArgs e)
        {
            // Initialize dropdowns, default text, etc.
            try
            {
                types.Items.Clear();
                types.Items.Add("Certificate of Enrolment");
                types.Items.Add("Good Moral");
                types.Items.Add("Graduate");

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
                string outputDir = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments), "Generated_Certificates");
                Directory.CreateDirectory(outputDir);

                // 3. Generate unique filename for output
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string safeName = new string(namebox.Text.Where(c => !Path.GetInvalidFileNameChars().Contains(c)).ToArray());
                string outputDoc = Path.Combine(outputDir, $"{safeName}_{timestamp}.docx");

                // 4. Copy template to output location
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
        private string GeneratePreviewHtml()
        {
            // Basic data
            string studentName = namebox.Text.Trim();
            string studentLRN = lrn.Text.Trim();
            string gradeSection = gradebox.Text.Trim();
            string track = trackbox.Text.Trim();
            string sy = $"{startYearCombo.Text}-{endYearCombo.Text}";
            string dateIssued = issueDatePicker.Value.ToString("MMMM dd, yyyy");
            string purpose = purposeBox.Text.Trim();
            string sem = semesterBox.Visible ? semesterBox.Text : "";
            string signatoryName = (selectedTemplate == "Certificate of Enrolment")
                ? registrarBox.Text : principalBox.Text;
            string signatoryTitle = (selectedTemplate == "Certificate of Enrolment")
                ? "School Registrar" : "School Principal";

            // Build the HTML
            var html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8' />
    <title>Certificate Preview</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            margin: 0; padding: 0;
            background: #f2f2f2;
        }}
        .preview-container {{
            width: 100%;
            padding: 20px;
            box-sizing: border-box;
        }}
        .certificate {{
            margin: 0 auto;
            width: 600px;
            background: #fff;
            padding: 40px;
            border: 2px solid #003299;
            box-sizing: border-box;
        }}
        h1 {{
            text-align: center;
            text-transform: uppercase;
            color: #003299;
            margin-bottom: 0;
        }}
        h2 {{
            text-align: center;
            margin-top: 5px;
            font-weight: normal;
            color: #333;
        }}
        p {{
            margin: 5px 0;
            font-size: 14px;
            line-height: 1.4;
            text-align: center;
        }}
        .signatory {{
            margin-top: 60px;
            text-align: center;
        }}
        .signatory-line {{
            display: block;
            width: 250px;
            margin: 0 auto;
            border-bottom: 1px solid #000;
            margin-bottom: 5px;
        }}
        .signatory-name {{
            font-weight: bold;
            text-transform: uppercase;
        }}
        .signatory-title {{
            font-style: italic;
            font-size: 13px;
        }}
        .issued-date {{
            margin-top: 25px;
            font-style: italic;
        }}
    </style>
</head>
<body>
    <div class='preview-container'>
        <div class='certificate'>
            <h1>CERTIFICATE</h1>
            <h2>{selectedTemplate}</h2>
";
            // Template logic
            if (selectedTemplate == "Certificate of Enrolment")
            {
                html += $@"
            <p>This is to certify that {studentName}, LRN: {studentLRN}<br/>
               is officially enrolled in Grade {gradeSection}, Track: {track}<br/>
               {sem}, School Year {sy}.</p>
            ";
                if (!string.IsNullOrEmpty(purpose))
                {
                    html += $@"
            <p>This certification is issued for {purpose}.</p>
                    ";
                }
            }
            else if (selectedTemplate == "Good Moral")
            {
                html += $@"
            <p>This is to certify that {studentName}, LRN: {studentLRN}<br/>
               is a student of <strong>Good Moral Character</strong><br/>
               Grade {gradeSection}, Track: {track}, S.Y. {sy}.</p>
            ";
            }
            else if (selectedTemplate == "Graduate")
            {
                html += $@"
            <p>This is to certify that {studentName}, LRN: {studentLRN}<br/>
               has successfully completed all requirements for<br/>
               Grade {gradeSection}, Track: {track}, S.Y. {sy}.</p>
            ";
                if (!string.IsNullOrEmpty(purpose))
                {
                    html += $@"
            <p>This certification is issued for {purpose}.</p>
                    ";
                }
            }

            html += $@"
            <div class='issued-date'>Issued on {dateIssued}</div>
            <div class='signatory'>
                <span class='signatory-line'></span>
                <div class='signatory-name'>{signatoryName}</div>
                <div class='signatory-title'>{signatoryTitle}</div>
            </div>
        </div>
    </div>
</body>
</html>
";
            return html;
        }
    }
}