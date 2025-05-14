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
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace Certificate_Maker_System.Resources
{
    public partial class Dashboard : UserControl
    {
        private const string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=;";

        public Dashboard()
        {
            InitializeComponent();
            DateTime currentDate = DateTime.Now;

            // Format the date as "dddd, MMMM dd" (day of the week, full month name, day of the month)
            string formattedDate = currentDate.ToString("dddd, MMMM dd");

            // Set the formatted date to the TextBox
            UpdateGenderCounts();
            // Set the date (e.g., "Wednesday, May 14, 2025")
            datetext.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy");

            // Set up a timer to update timetext every second
            Timer timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;
            timer.Start();
            stubyprog_Click(null, null);
        }

        private void UpdateGenderCounts()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Count males
                    string maleCountQuery = "SELECT COUNT(*) FROM students WHERE gender = 'male' COLLATE utf8mb4_general_ci";
                    using (MySqlCommand maleCommand = new MySqlCommand(maleCountQuery, connection))
                    {
                        int maleCount = Convert.ToInt32(maleCommand.ExecuteScalar());

                        // Count females
                        string femaleCountQuery = "SELECT COUNT(*) FROM students WHERE gender = 'female' COLLATE utf8mb4_general_ci";
                        using (MySqlCommand femaleCommand = new MySqlCommand(femaleCountQuery, connection))
                        {
                            int femaleCount = Convert.ToInt32(femaleCommand.ExecuteScalar());

                            int totalCount = maleCount + femaleCount;
                            totalno.Text = totalCount.ToString();
                            // Update TextBoxes with counts
                            maleno.Text = maleCount.ToString();
                            femaleno.Text = femaleCount.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., display an error message)
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the time in HH:MM:SS AM/PM format
            timetext.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Create a new AddStudent form instance
            AddStudent addStudentForm = new AddStudent(null);
            // If you need to pass a StudentList instance, replace null with the actual parent
            addStudentForm.ShowDialog();
            // This will open the AddStudent Form as a dialog.
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Create a new Form to host the CertificateGenerator user control
            Form certificateForm = new Form();
            certificateForm.Text = "Generate Certificate";
            certificateForm.Size = new Size(724, 652); // Adjust as needed

            // Center the form on the screen
            certificateForm.StartPosition = FormStartPosition.CenterScreen;

            // Create the user control
            CertificateGenerator generator = new CertificateGenerator();
            generator.Dock = DockStyle.Fill;

            // Add the user control to the form
            certificateForm.Controls.Add(generator);

            // Show as a modal dialog
            certificateForm.ShowDialog();
        }

        private void stubyprog_Click(object sender, EventArgs e)
        {
            // Clear any existing controls in panel1
            panel1.Controls.Clear();

            // Create a new Chart control
            Chart trackChart = new Chart
            {
                Dock = DockStyle.Fill
            };

            // Create and add a ChartArea
            ChartArea chartArea = new ChartArea("ChartArea1");
            trackChart.ChartAreas.Add(chartArea);

            // Create a Series to display the count of students per track
            Series series = new Series("Students by Track")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String
            };

            // Notice we exclude rows where track is 'Grade 11' or 'Grade 12'
            string query = @"
        SELECT track, COUNT(*) AS total_students
        FROM student_academic
        WHERE track NOT IN ('Grade 11', 'Grade 12')
        GROUP BY track;
    ";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string trackName = reader["track"].ToString();
                                int count = Convert.ToInt32(reader["total_students"]);
                                series.Points.AddXY(trackName, count);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data: " + ex.Message);
            }

            // Add the series to the chart
            trackChart.Series.Add(series);

            // Show data labels on each bar
            series.IsValueShownAsLabel = true;

            // Set axis titles
            chartArea.AxisX.Title = "Track";
            chartArea.AxisY.Title = "Number of Students";

            // Add the chart to panel1
            panel1.Controls.Add(trackChart);
        }

        private void stuacadstat_Click(object sender, EventArgs e)
        {
            // Clear any existing controls in panel1
            panel1.Controls.Clear();

            // Create a new Chart control
            Chart chart = new Chart
            {
                Dock = DockStyle.Fill
            };

            // Create and add a ChartArea
            ChartArea chartArea = new ChartArea("ChartArea1");
            chart.ChartAreas.Add(chartArea);

            // Create a Series for displaying the academic status counts
            Series series = new Series("Students by Status")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String
            };

            // Query to count students by status (Enrolled vs. Graduated)
            string query = @"
        SELECT status, COUNT(*) AS total_students
        FROM student_academic
        GROUP BY status;
    ";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // status = "Enrolled" or "Graduated"
                                string statusName = reader["status"].ToString();
                                int count = Convert.ToInt32(reader["total_students"]);

                                // Add data points to the series
                                series.Points.AddXY(statusName, count);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data: " + ex.Message);
            }

            // Add the series to the chart
            chart.Series.Add(series);

            // Show data labels on each bar
            series.IsValueShownAsLabel = true;

            // Set axis titles for clarity
            chartArea.AxisX.Title = "Academic Status";
            chartArea.AxisY.Title = "Number of Students";

            // Add the chart to panel1
            panel1.Controls.Add(chart);
        }

        private void stusection_Click(object sender, EventArgs e)
        {
            // Clear any existing controls in panel1
            panel1.Controls.Clear();

            // Create a new Chart control
            Chart sectionChart = new Chart
            {
                Dock = DockStyle.Fill
            };

            // Create and add a ChartArea
            ChartArea chartArea = new ChartArea("ChartArea1");
            sectionChart.ChartAreas.Add(chartArea);

            // Create a Series for displaying the count of students per section
            Series series = new Series("Students by Section")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String
            };

            // Query to count how many students per section
            string query = @"
        SELECT section, COUNT(*) AS total_students
        FROM student_academic
        GROUP BY section";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string sectionName = reader["section"].ToString();
                                int count = Convert.ToInt32(reader["total_students"]);
                                series.Points.AddXY(sectionName, count);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data: " + ex.Message);
            }

            // Show data labels inside each bar
            sectionChart.Series.Add(series);
            series.IsValueShownAsLabel = true;
            series["LabelStyle"] = "Center";  // Attempt to position label in the center
                                              // For contrast, set label color white (if bars are a dark color)
            series.LabelForeColor = Color.White;

            // Configure axis titles for clarity
            chartArea.AxisX.Title = "Section";
            chartArea.AxisY.Title = "Number of Students";

            // Add the chart to panel1
            panel1.Controls.Add(sectionChart);
        }

        private void certrends_Click(object sender, EventArgs e)
        {
            // Clear any existing controls in panel1
            panel1.Controls.Clear();

            // Create a new Chart control
            Chart trendsChart = new Chart
            {
                Dock = DockStyle.Fill
            };

            // Create a ChartArea
            ChartArea chartArea = new ChartArea("ChartArea1");
            trendsChart.ChartAreas.Add(chartArea);

            // Create a Series for displaying how many certificates were generated per day
            Series series = new Series("Daily Certificates Generated")
            {
                ChartType = SeriesChartType.Line,     // Show a line chart for daily trends
                XValueType = ChartValueType.String,   // We'll plot each day as a string label (YYYY-MM-DD)
                BorderWidth = 3                      // Make the line thicker
            };

            // Day-by-day grouping of certificates in certificate_history
            // "generated_date" is a DATETIME column
            string query = @"
        SELECT DATE_FORMAT(generated_date, '%Y-%m-%d') AS day_label,
               COUNT(*) AS total_certificates
        FROM certificate_history
        GROUP BY DATE_FORMAT(generated_date, '%Y-%m-%d')
        ORDER BY day_label;
    ";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string dayLabel = reader["day_label"].ToString();     // e.g. "2025-05-14"
                                int count = Convert.ToInt32(reader["total_certificates"]);

                                // Add data points: X = dayLabel, Y = count
                                series.Points.AddXY(dayLabel, count);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving certificate trends: " + ex.Message);
            }

            // Add the series to the chart
            trendsChart.Series.Add(series);

            // Show numeric labels on each data point
            series.IsValueShownAsLabel = true;
            series.LabelForeColor = Color.Black;

            // Configure axis titles
            chartArea.AxisX.Title = "Day (YYYY-MM-DD)";
            chartArea.AxisY.Title = "Certificates Generated";

            // Finally, add the chart to panel1
            panel1.Controls.Add(trendsChart);
        }
    }
}