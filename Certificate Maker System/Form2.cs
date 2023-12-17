using Certificate_Maker_System.Resources;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Certificate_Maker_System
{
   
    public partial class Form2 : Form
    {

        private const string ConnectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=;";
        private List<Button> reset = new List<Button>();
        private string receive;
    
        public Form2(string getuser)
        {
            InitializeComponent();
            Dashboard dashboard = new Dashboard();
            changeUserControl(dashboard);
            receive = getuser;
        }

        private void ResetButtonColors()
        {
            foreach (Button button in reset)
            {
                button.ForeColor = Color.White;
            }
        }

        public void changeUserControl (UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(userControl);
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        } 

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
                    UserManage userManage = new UserManage("");
                    ResetButtonColors();
                    ((Button)sender).ForeColor = Color.RoyalBlue;
                    reset.Add((Button)sender);
                    changeUserControl(userManage);


            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                Form4 form4 = new Form4();

                // Fetch userId based on username
                string getUserIdQuery = "SELECT userId FROM user_auth WHERE username = @username";
                using (MySqlCommand getUserIdCommand = new MySqlCommand(getUserIdQuery, connection))
                {
                    getUserIdCommand.Parameters.AddWithValue("@username", receive); // Assuming GetInputUsername() is a method in Form4
                    object result = getUserIdCommand.ExecuteScalar();

                    if (result != null)
                    {
                        int selectedUserId = Convert.ToInt32(result);

                        // Fetch user_info data
                        string userInfoQuery = "SELECT * FROM user_info WHERE userId = @userId";
                        using (MySqlCommand userInfoCommand = new MySqlCommand(userInfoQuery, connection))
                        {
                            userInfoCommand.Parameters.AddWithValue("@userId", selectedUserId);

                            using (MySqlDataReader reader = userInfoCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Populate textboxes with user_info data
                                    userManage.firstnameuser.Text = reader["firstName"].ToString();
                                    userManage.middleuser.Text = reader["middleName"].ToString();
                                    userManage.lastuser.Text = reader["lastName"].ToString();
                                    userManage.emailuser.Text = reader["email"].ToString();
                                    userManage.genderuser.Text = reader["gender"].ToString() ;
                                    userManage.positionuser.Text = reader["position"].ToString();
                                    userManage.birthdayuser.Text = reader["birthday"].ToString();
                                    string firstName = reader["firstName"].ToString();
                                    string middleName = reader["middleName"].ToString();
                                    string lastName = reader["lastName"].ToString();

                                    // Combine first name, middle name, and last name into a full name
                                    string fullName = $"{firstName} {middleName} {lastName}".Trim();

                                    // Assign the full name to the TextBox
                                    userManage.fullnamelabel.Text = fullName;
                                    userManage.positionlabel.Text = reader["position"].ToString();

                                    // Add more textboxes as needed
                                }
                            }
                        }

                        // Fetch user_auth data
                        string userAuthQuery = "SELECT * FROM user_auth WHERE userId = @userId";
                        using (MySqlCommand userAuthCommand = new MySqlCommand(userAuthQuery, connection))
                        {
                            userAuthCommand.Parameters.AddWithValue("@userId", selectedUserId);

                            using (MySqlDataReader reader = userAuthCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Populate textboxes with user_auth data
                                    userManage.usernameuser.Text = reader["username"].ToString();
                                    userManage.passworduser.Text = reader["password"].ToString();
                                    // Add more textboxes as needed
                                }
                            }
                        }
                    }
                    else
                    {
                        // Handle the case where the username is not found
                        MessageBox.Show("Username not found.");
                    }
                }           

        }
    }


        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void dashboardbtn(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            ResetButtonColors();
            ((Button)sender).ForeColor = Color.RoyalBlue;
            reset.Add((Button)sender);
            changeUserControl(dashboard);

        }

        private void certificatebtn(object sender, EventArgs e)
        {
            CertificateGenerator certificateGenerator = new CertificateGenerator("");
            ResetButtonColors();
            ((Button)sender).ForeColor = Color.RoyalBlue;
            reset.Add((Button)sender);
            changeUserControl(certificateGenerator);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void close(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimize(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {

        }

        private void logoutbtn(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                Form4 form4 = new Form4();
                form4.Show();
            }
        }

        private void studentbtn(object sender, EventArgs e)
        {
            StudentList studentList = new StudentList();
            ResetButtonColors();
            ((Button)sender).ForeColor = Color.RoyalBlue;
            reset.Add((Button)sender);
            changeUserControl(studentList);
        }

        private void historybtn(object sender, EventArgs e)
        {
            History history = new History();
            ResetButtonColors();
            ((Button)sender).ForeColor = Color.RoyalBlue;
            reset.Add((Button)sender);
            changeUserControl(history);
        }
    }
}
