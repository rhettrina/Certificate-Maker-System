namespace Certificate_Maker_System
{
    partial class AddStudent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddStudent));
            this.labelforform = new System.Windows.Forms.Label();
            this.lrnbox = new System.Windows.Forms.TextBox();
            this.middlebox = new System.Windows.Forms.TextBox();
            this.lastnamebox = new System.Windows.Forms.TextBox();
            this.maleradio = new System.Windows.Forms.RadioButton();
            this.femaleradio = new System.Windows.Forms.RadioButton();
            this.gradebox = new System.Windows.Forms.ComboBox();
            this.sectionbox = new System.Windows.Forms.ComboBox();
            this.trackbox = new System.Windows.Forms.ComboBox();
            this.firstnamebox = new System.Windows.Forms.TextBox();
            this.addressbox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.birthdaybox = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button8 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelforform
            // 
            this.labelforform.AutoSize = true;
            this.labelforform.Font = new System.Drawing.Font("Microsoft Tai Le", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelforform.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelforform.Location = new System.Drawing.Point(12, 15);
            this.labelforform.Name = "labelforform";
            this.labelforform.Size = new System.Drawing.Size(223, 31);
            this.labelforform.TabIndex = 1;
            this.labelforform.Text = "Add Student Form";
            this.labelforform.Click += new System.EventHandler(this.label2_Click);
            // 
            // lrnbox
            // 
            this.lrnbox.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lrnbox.Location = new System.Drawing.Point(265, 59);
            this.lrnbox.Name = "lrnbox";
            this.lrnbox.Size = new System.Drawing.Size(223, 32);
            this.lrnbox.TabIndex = 3;
            this.lrnbox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            this.lrnbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lrnbox_KeyPress);
            // 
            // middlebox
            // 
            this.middlebox.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.middlebox.Location = new System.Drawing.Point(18, 113);
            this.middlebox.Name = "middlebox";
            this.middlebox.Size = new System.Drawing.Size(223, 32);
            this.middlebox.TabIndex = 4;
            // 
            // lastnamebox
            // 
            this.lastnamebox.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastnamebox.Location = new System.Drawing.Point(18, 173);
            this.lastnamebox.Name = "lastnamebox";
            this.lastnamebox.Size = new System.Drawing.Size(223, 32);
            this.lastnamebox.TabIndex = 6;
            // 
            // maleradio
            // 
            this.maleradio.AutoSize = true;
            this.maleradio.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maleradio.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.maleradio.Location = new System.Drawing.Point(55, 23);
            this.maleradio.Name = "maleradio";
            this.maleradio.Size = new System.Drawing.Size(62, 25);
            this.maleradio.TabIndex = 8;
            this.maleradio.TabStop = true;
            this.maleradio.Text = "Male";
            this.maleradio.UseVisualStyleBackColor = true;
            this.maleradio.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // femaleradio
            // 
            this.femaleradio.AutoSize = true;
            this.femaleradio.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.femaleradio.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.femaleradio.Location = new System.Drawing.Point(130, 23);
            this.femaleradio.Name = "femaleradio";
            this.femaleradio.Size = new System.Drawing.Size(78, 25);
            this.femaleradio.TabIndex = 9;
            this.femaleradio.TabStop = true;
            this.femaleradio.Text = "Female";
            this.femaleradio.UseVisualStyleBackColor = true;
            this.femaleradio.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // gradebox
            // 
            this.gradebox.AutoCompleteCustomSource.AddRange(new string[] {
            "Grade 11",
            "Grade 12"});
            this.gradebox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gradebox.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gradebox.FormattingEnabled = true;
            this.gradebox.Location = new System.Drawing.Point(18, 243);
            this.gradebox.Name = "gradebox";
            this.gradebox.Size = new System.Drawing.Size(223, 31);
            this.gradebox.TabIndex = 10;
            this.gradebox.SelectedIndexChanged += new System.EventHandler(this.grade_SelectedIndexChanged);
            // 
            // sectionbox
            // 
            this.sectionbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sectionbox.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sectionbox.FormattingEnabled = true;
            this.sectionbox.Location = new System.Drawing.Point(18, 314);
            this.sectionbox.Name = "sectionbox";
            this.sectionbox.Size = new System.Drawing.Size(223, 31);
            this.sectionbox.TabIndex = 11;
            // 
            // trackbox
            // 
            this.trackbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.trackbox.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackbox.FormattingEnabled = true;
            this.trackbox.Location = new System.Drawing.Point(18, 383);
            this.trackbox.Name = "trackbox";
            this.trackbox.Size = new System.Drawing.Size(223, 31);
            this.trackbox.TabIndex = 13;
            this.trackbox.SelectedIndexChanged += new System.EventHandler(this.trackbox_SelectedIndexChanged);
            // 
            // firstnamebox
            // 
            this.firstnamebox.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstnamebox.Location = new System.Drawing.Point(18, 59);
            this.firstnamebox.Name = "firstnamebox";
            this.firstnamebox.Size = new System.Drawing.Size(223, 32);
            this.firstnamebox.TabIndex = 2;
            // 
            // addressbox
            // 
            this.addressbox.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addressbox.Location = new System.Drawing.Point(271, 314);
            this.addressbox.Multiline = true;
            this.addressbox.Name = "addressbox";
            this.addressbox.Size = new System.Drawing.Size(217, 100);
            this.addressbox.TabIndex = 14;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(133)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.Location = new System.Drawing.Point(301, 472);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(187, 36);
            this.button2.TabIndex = 16;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.savebtn);
            // 
            // birthdaybox
            // 
            this.birthdaybox.CalendarFont = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.birthdaybox.CustomFormat = "dd-MM-yyyy";
            this.birthdaybox.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.birthdaybox.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.birthdaybox.Location = new System.Drawing.Point(265, 113);
            this.birthdaybox.Name = "birthdaybox";
            this.birthdaybox.Size = new System.Drawing.Size(223, 32);
            this.birthdaybox.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(268, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 16);
            this.label4.TabIndex = 19;
            this.label4.Text = "LRN";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(22, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "First Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(22, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 16);
            this.label5.TabIndex = 20;
            this.label5.Text = "Middle Name (optional)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(22, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 21;
            this.label6.Text = "Last Name";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(268, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 16);
            this.label7.TabIndex = 22;
            this.label7.Text = "BirthDate";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Location = new System.Drawing.Point(22, 277);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 16);
            this.label8.TabIndex = 23;
            this.label8.Text = "Grade";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Location = new System.Drawing.Point(22, 348);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 16);
            this.label9.TabIndex = 24;
            this.label9.Text = "Section";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label10.Location = new System.Drawing.Point(278, 417);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 16);
            this.label10.TabIndex = 25;
            this.label10.Text = "Address";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label11.Location = new System.Drawing.Point(22, 417);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 16);
            this.label11.TabIndex = 26;
            this.label11.Text = "Track or Strand";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Controls.Add(this.maleradio);
            this.groupBox1.Controls.Add(this.femaleradio);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(271, 214);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 60);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gender";
            // 
            // button8
            // 
            this.button8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button8.BackgroundImage")));
            this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Location = new System.Drawing.Point(484, 2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(20, 20);
            this.button8.TabIndex = 28;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.close);
            // 
            // AddStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(506, 520);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.birthdaybox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.addressbox);
            this.Controls.Add(this.trackbox);
            this.Controls.Add(this.sectionbox);
            this.Controls.Add(this.gradebox);
            this.Controls.Add(this.lastnamebox);
            this.Controls.Add(this.middlebox);
            this.Controls.Add(this.lrnbox);
            this.Controls.Add(this.firstnamebox);
            this.Controls.Add(this.labelforform);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddStudent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddStudent";
            this.Load += new System.EventHandler(this.AddStudent_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labelforform;
        public System.Windows.Forms.TextBox lrnbox;
        public System.Windows.Forms.TextBox middlebox;
        public System.Windows.Forms.TextBox lastnamebox;
        public System.Windows.Forms.RadioButton maleradio;
        public System.Windows.Forms.RadioButton femaleradio;
        public System.Windows.Forms.ComboBox gradebox;
        public System.Windows.Forms.ComboBox sectionbox;
        public System.Windows.Forms.ComboBox trackbox;
        public System.Windows.Forms.TextBox firstnamebox;
        public System.Windows.Forms.TextBox addressbox;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.DateTimePicker birthdaybox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button8;
    }
}