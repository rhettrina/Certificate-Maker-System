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

namespace Certificate_Maker_System
{

    public partial class AddStudent : Form
    {
        private const string connectionString = "Server=localhost;Database=certificatemaker;User ID=root;Password=root;";
        string gender;
        public AddStudent()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void AddStudent_Load(object sender, EventArgs e)
        {
            gradebox.Items.Add("Grade 11");
            gradebox.Items.Add("Grade 12");
            sectionbox.Items.Add("Sampaguita");
            sectionbox.Items.Add("Adelfa");
            sectionbox.Items.Add("Bougainvillea");
            trackbox.Items.Add("GAS");
            trackbox.Items.Add("STEM");
            trackbox.Items.Add("AGRICULTURE");
            trackbox.Items.Add("HOME ECONOMICS");

        }

        private void cancelbtn(object sender, EventArgs e)
        {
            this.Close();
        }

        private void savebtn(object sender, EventArgs e)
        {
           
        }

        private void ClearTextBoxes()
        {
            // Clear all textboxes on the form
            foreach (Control control in Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Clear();
                }
            }
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void grade_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void lrnbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers (0-9) and control keys like backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            gender = "male";
            labelgender.Text = gender; 
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gender = "female";
            labelgender.Text = gender;
        }
    }
}
