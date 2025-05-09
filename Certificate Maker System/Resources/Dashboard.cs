using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Certificate_Maker_System.Resources
{
    public partial class Dashboard : UserControl
    {
        private const string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=;";

        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            // Show today's date
            labelDate.Text = DateTime.Now.ToString("dddd, MMMM dd");

            // Load stats from the database
            CountGender();
            CountGrades();
            CountTracks();
            CountEnrollmentStatus();
        }

        /// <summary>
        /// Button event to open AddStudent form.
        /// </summary>
        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            AddStudent addStudentForm = new AddStudent(null);
            addStudentForm.Show();
        }

        /// <summary>
        /// Counts total students, plus male/female, from "students" table.
        /// </summary>
        private void CountGender()
        {
            int maleCount = 0;
            int femaleCount = 0;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Count Males
                    string sqlMale = "SELECT COUNT(*) FROM students WHERE gender = 'male' COLLATE utf8mb4_general_ci";
                    using (MySqlCommand cmdMale = new MySqlCommand(sqlMale, connection))
                    {
                        maleCount = Convert.ToInt32(cmdMale.ExecuteScalar());
                    }

                    // Count Females
                    string sqlFemale = "SELECT COUNT(*) FROM students WHERE gender = 'female' COLLATE utf8mb4_general_ci";
                    using (MySqlCommand cmdFemale = new MySqlCommand(sqlFemale, connection))
                    {
                        femaleCount = Convert.ToInt32(cmdFemale.ExecuteScalar());
                    }
                }

                int total = maleCount + femaleCount;
                labelTotalCount.Text = $"Total: {total}";
                labelMaleCount.Text = maleCount.ToString();
                labelFemaleCount.Text = femaleCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error counting students by gender: " + ex.Message);
            }
        }

        /// <summary>
        /// Counts how many are Grade 11 and Grade 12 in "student_academic".
        /// </summary>
        private void CountGrades()
        {
            int grade11Count = 0;
            int grade12Count = 0;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Grade 11
                    string sqlG11 = "SELECT COUNT(*) FROM student_academic WHERE grade = 'Grade 11'";
                    using (MySqlCommand cmdG11 = new MySqlCommand(sqlG11, connection))
                    {
                        grade11Count = Convert.ToInt32(cmdG11.ExecuteScalar());
                    }

                    // Grade 12
                    string sqlG12 = "SELECT COUNT(*) FROM student_academic WHERE grade = 'Grade 12'";
                    using (MySqlCommand cmdG12 = new MySqlCommand(sqlG12, connection))
                    {
                        grade12Count = Convert.ToInt32(cmdG12.ExecuteScalar());
                    }
                }

                labelCountG11.Text = grade11Count.ToString();
                labelCountG12.Text = grade12Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error counting grade 11/12: " + ex.Message);
            }
        }

        /// <summary>
        /// Counts how many students are in each track (STEM, GAS, HOME ECONOMICS, AGRICULTURE).
        /// </summary>
        private void CountTracks()
        {
            int stemCount = 0;
            int gasCount = 0;
            int heCount = 0;
            int agriCount = 0;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // STEM
                    string sqlSTEM = "SELECT COUNT(*) FROM student_academic WHERE track = 'STEM'";
                    using (MySqlCommand cmdSTEM = new MySqlCommand(sqlSTEM, connection))
                    {
                        stemCount = Convert.ToInt32(cmdSTEM.ExecuteScalar());
                    }

                    // GAS
                    string sqlGAS = "SELECT COUNT(*) FROM student_academic WHERE track = 'GAS'";
                    using (MySqlCommand cmdGAS = new MySqlCommand(sqlGAS, connection))
                    {
                        gasCount = Convert.ToInt32(cmdGAS.ExecuteScalar());
                    }

                    // HOME ECONOMICS
                    string sqlHE = "SELECT COUNT(*) FROM student_academic WHERE track = 'HOME ECONOMICS'";
                    using (MySqlCommand cmdHE = new MySqlCommand(sqlHE, connection))
                    {
                        heCount = Convert.ToInt32(cmdHE.ExecuteScalar());
                    }

                    // AGRICULTURE
                    string sqlAgri = "SELECT COUNT(*) FROM student_academic WHERE track = 'AGRICULTURE'";
                    using (MySqlCommand cmdAgri = new MySqlCommand(sqlAgri, connection))
                    {
                        agriCount = Convert.ToInt32(cmdAgri.ExecuteScalar());
                    }
                }

                labelSTEMCount.Text = stemCount.ToString();
                labelGASCount.Text = gasCount.ToString();
                labelHECount.Text = heCount.ToString();
                labelAgriCount.Text = agriCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error counting tracks: " + ex.Message);
            }
        }

        /// <summary>
        /// Counts how many are Enrolled vs. Graduated in "student_academic".
        /// </summary>
        private void CountEnrollmentStatus()
        {
            int enrolledCount = 0;
            int graduatedCount = 0;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Enrolled
                    string sqlEnrolled = "SELECT COUNT(*) FROM student_academic WHERE status = 'Enrolled'";
                    using (MySqlCommand cmdEnrolled = new MySqlCommand(sqlEnrolled, connection))
                    {
                        enrolledCount = Convert.ToInt32(cmdEnrolled.ExecuteScalar());
                    }

                    // Graduated
                    string sqlGraduated = "SELECT COUNT(*) FROM student_academic WHERE status = 'Graduated'";
                    using (MySqlCommand cmdGraduated = new MySqlCommand(sqlGraduated, connection))
                    {
                        graduatedCount = Convert.ToInt32(cmdGraduated.ExecuteScalar());
                    }
                }

                labelEnrolledCount.Text = enrolledCount.ToString();
                labelGraduatedCount.Text = graduatedCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error counting enrollment status: " + ex.Message);
            }
        }
    }
}
