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
using Word = Microsoft.Office.Interop.Word;

namespace Certificate_Maker_System.Resources
{

    public partial class CertificateGenerator : UserControl
    {
        private const string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=;";
        public string lrnInput;
        public string gnsInput;
        public string satInput;
        public string nameInput;
        public string corTemp;
        public string goodMoral;
        public string ranking;
        public string codTemp;
        private object folder;
        public string selectedTemplate;

        public CertificateGenerator()
        {
            InitializeComponent();
            searchbox.TabStop = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (types.SelectedItem != null)
            {
                selectedTemplate = this.types.GetItemText(this.types.SelectedItem);

                switch (selectedTemplate)
                {
                    case "Certificate of Enrolment":
                        LoadTemplate1();
                        break;

                    case "Good Moral":
                        LoadTemplate2();
                        break;

                    case "Ranking":
                        LoadTemplate3();
                        break;

                    default:
                        // Handle default case or do nothing
                        break;
                }
            }
        }

        private void LoadTemplate1()
        {
            corTemp = "CERTIFICATE OF ENROLMENT\r\n\r\n" +
                      "To Whom It May Concern:\r\n" +
                      "This is to certify that " + namebox.Text + " with Learners Reference Number " + lrn.Text + "" +
                      " is enrolled as Grade " + gradebox.Text + " in this school, this SEMESTER SY SCHOOL YEAR.\r\n" +
                      "Issued this DAY day of MONTH YEAR upon request of the party concerned for PURPOSE.\r\n\r\n" +
                      "FLORABEL A. PLACIDO\r\nRegistrar I\r\n\r\n\r\n\r\n\r\n  " +
                      "Not Valid without\r\n" +
                      "JPNHS Official Seal";
        }

        private void LoadTemplate2()
        {
            goodMoral = "CERTIFICATE OF GOOD MORAL CHARACTER\r\n\r\n" +
                "To Whom It May Concern:\r\n" +
                "This is to certify that " + namebox.Text + " with LRN " + lrn.Text + " is a graduate of " + trackbox.Text + " in this school under the K to 12 Curriculum, this SY SCHOOL YEAR.\r\n        \r\n" +
                "HE/SHE is known to be a student with good moral character and has no record of misdemeanor.\r\n\r\n" +
                "Issued this DAY day of MONTH YEAR upon request of the party concerned for whatever legal purpose/s this may serve.\r\n\r\n\r\n\r\n\r\n" +
                "MARLO FIEL P. SULTAN, Ed. D.\r\nSchool Principal III\r\n\r\n\r\n\r\n\r\n" +
                "Not valid without JPNHS \r\n        " +
                "Official Seal";
        }

        private void LoadTemplate3()
        {
            // Get the current date
            string currentDate = DateTime.Now.ToString("dd MMMM, yyyy");

            ranking = "C E R T I F I C A T I O N\r\n\r\n" +
                "To Whom It May Concern:\r\n\r\n\t" +
                "This is to certify that " + namebox.Text + " with LRN " + lrn.Text + " ranked 1st out of 744 learners in this institution during the School Year 2022-2023. \r\n\r\n\t" +
                "Also, Mr. Carlo Mario Panambo ranked 1st out of 388 learners in Academic Track. He obtained a general weighted average of 98% and awarded with Highest Honors.\r\n\r\n\t" +
                "Issued this " + currentDate + " upon the request of the party concerned as reference for Ateneo De Naga University Scholarship.\r\n\r\n\r\n\r\n" +
                "MARO FIEL P. SULTAN\r\n" +
                "School Principal III\r\n\r\n\r\n" +
                "BY AUTHORITY OF THE PRINCIPAL\r\n\r\n\r\n" +
                "EUNICE M. LONIZO\r\n" +
                "HT III, English / OIC – Asst. School Principal II\r\n\r\n\r\n  " +
                "Not Valid without\r\nJPNHS Official Seal";
        }




        private void button1_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(selectedTemplate))
            {
                switch (selectedTemplate)
                {
                    case "Certificate of Enrolment":
                        Console.WriteLine(corTemp);
                        Word.Application word = new Word.Application();
                        word.Visible = true;
                        word.WindowState = Word.WdWindowState.wdWindowStateNormal;
                        Word.Document doc = word.Documents.Add();
                        Word.Paragraph paragraph;
                        paragraph = doc.Paragraphs.Add();
                        paragraph.Range.Text = corTemp;
                        //doc.SaveAs2(@E:\New folder\mydoc.doc);//
                        //doc.Close();//
                        //word.Quit();
                       


                        break;

                    case "Good Moral":
                        Console.WriteLine(goodMoral);
                        Word.Application words = new Word.Application();
                        words.Visible = true;
                        words.WindowState = Word.WdWindowState.wdWindowStateNormal;
                        Word.Document docs = words.Documents.Add();
                        Word.Paragraph paragraphs;
                        paragraphs = docs.Paragraphs.Add();
                        paragraphs.Range.Text = goodMoral;
                        //doc.SaveAs2(@E:\New folder\mydoc.doc);//
                        //doc.Close();//
                        //words.Quit();


                        break;

                    case "Ranking":
                        Console.WriteLine(ranking);
                        Word.Application wordss = new Word.Application();
                        wordss.Visible = true;
                        wordss.WindowState = Word.WdWindowState.wdWindowStateNormal;
                        Word.Document docss = wordss.Documents.Add();
                        Word.Paragraph paragraphss;
                        paragraphss = docss.Paragraphs.Add();
                        paragraphss.Range.Text = ranking;
                        //doc.SaveAs2(@E:\New folder\mydoc.doc);//
                        //doc.Close();//
                        //wordss.Quit();



                        break;

                    default:
                        Console.WriteLine("No template loaded.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("No template loaded.");
            }
        }



        private void label2_Click(object sender, EventArgs e)
        {
            // Implementation for label2_Click
        }


        private void lrnBox(object sender, EventArgs e)
        {
            lrnInput = lrn.Text;
            LoadSelectedTemplate(); // Trigger template loading when lrnBox is updated
        }

        private void gradeAndSection(object sender, EventArgs e)
        {
            gnsInput = gradebox.Text;
            LoadSelectedTemplate(); // Trigger template loading when gradeAndSectionBox is updated
        }

        private void strandAndTrack(object sender, EventArgs e)
        {
            satInput = trackbox.Text;
            LoadSelectedTemplate(); // Trigger template loading when strandAndSectionBox is updated
        }

        private void name(object sender, EventArgs e)
        {
            nameInput = namebox.Text;
            LoadSelectedTemplate(); // Trigger template loading when nameBox is updated
        }

        private void LoadSelectedTemplate()
        {
            // Update the content based on the selected template
            switch (selectedTemplate)
            {
                case "Certificate of Enrolment":
                    LoadTemplate1();
                    break;

                case "Good Moral":
                    LoadTemplate2();
                    break;

                case "Ranking":
                    LoadTemplate3();
                    break;

                // Add more cases as needed

                default:
                    // Handle default case or do nothing
                    break;
            }

            // Set the appropriate template content to the corresponding variables
            corTemp = selectedTemplate == "Certificate of Enrolment" ? corTemp : null;
            goodMoral = selectedTemplate == "Good Moral" ? goodMoral : null;
            ranking = selectedTemplate == "Ranking" ? ranking : null;
        }



        private void CertificateGenerator_Load(object sender, EventArgs e)
        {
            types.Items.Add("Certificate of Enrolment");
            types.Items.Add("Good Moral");
            types.Items.Add("Ranking");
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
        }

    }
}
