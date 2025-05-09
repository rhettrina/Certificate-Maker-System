namespace Certificate_Maker_System.Resources
{
    partial class Dashboard
    {
        private System.ComponentModel.IContainer components = null;

        // Top panel: date label + Add Student button
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Button btnAddStudent;

        // Panel: Overall counts
        private System.Windows.Forms.Panel panelOverall;
        private System.Windows.Forms.Label labelOverallTitle;
        private System.Windows.Forms.Label labelTotalCount;
        private System.Windows.Forms.Label labelMaleTitle;
        private System.Windows.Forms.Label labelMaleCount;
        private System.Windows.Forms.Label labelFemaleTitle;
        private System.Windows.Forms.Label labelFemaleCount;

        // Panel: Grade counts
        private System.Windows.Forms.Panel panelGrades;
        private System.Windows.Forms.Label labelGradesTitle;
        private System.Windows.Forms.Label labelG11Title;
        private System.Windows.Forms.Label labelG12Title;
        private System.Windows.Forms.Label labelCountG11;
        private System.Windows.Forms.Label labelCountG12;

        // Panel: Track counts
        private System.Windows.Forms.Panel panelTracks;
        private System.Windows.Forms.Label labelTrackTitle;
        private System.Windows.Forms.Label labelSTEMTitle;
        private System.Windows.Forms.Label labelSTEMCount;
        private System.Windows.Forms.Label labelGASTitle;
        private System.Windows.Forms.Label labelGASCount;
        private System.Windows.Forms.Label labelHETitle;
        private System.Windows.Forms.Label labelHECount;
        private System.Windows.Forms.Label labelAgriTitle;
        private System.Windows.Forms.Label labelAgriCount;

        // Panel: Enrollment status (Enrolled, Graduated, etc.)
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label labelStatusTitle;
        private System.Windows.Forms.Label labelEnrolledTitle;
        private System.Windows.Forms.Label labelGraduatedTitle;
        private System.Windows.Forms.Label labelEnrolledCount;
        private System.Windows.Forms.Label labelGraduatedCount;

        /// <summary> 
        /// Disposes resources.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelDate = new System.Windows.Forms.Label();
            this.btnAddStudent = new System.Windows.Forms.Button();
            this.panelOverall = new System.Windows.Forms.Panel();
            this.labelOverallTitle = new System.Windows.Forms.Label();
            this.labelTotalCount = new System.Windows.Forms.Label();
            this.labelMaleTitle = new System.Windows.Forms.Label();
            this.labelMaleCount = new System.Windows.Forms.Label();
            this.labelFemaleTitle = new System.Windows.Forms.Label();
            this.labelFemaleCount = new System.Windows.Forms.Label();
            this.panelGrades = new System.Windows.Forms.Panel();
            this.labelGradesTitle = new System.Windows.Forms.Label();
            this.labelG11Title = new System.Windows.Forms.Label();
            this.labelCountG11 = new System.Windows.Forms.Label();
            this.labelG12Title = new System.Windows.Forms.Label();
            this.labelCountG12 = new System.Windows.Forms.Label();
            this.panelTracks = new System.Windows.Forms.Panel();
            this.labelTrackTitle = new System.Windows.Forms.Label();
            this.labelSTEMTitle = new System.Windows.Forms.Label();
            this.labelSTEMCount = new System.Windows.Forms.Label();
            this.labelGASTitle = new System.Windows.Forms.Label();
            this.labelGASCount = new System.Windows.Forms.Label();
            this.labelHETitle = new System.Windows.Forms.Label();
            this.labelHECount = new System.Windows.Forms.Label();
            this.labelAgriTitle = new System.Windows.Forms.Label();
            this.labelAgriCount = new System.Windows.Forms.Label();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.labelStatusTitle = new System.Windows.Forms.Label();
            this.labelEnrolledTitle = new System.Windows.Forms.Label();
            this.labelEnrolledCount = new System.Windows.Forms.Label();
            this.labelGraduatedTitle = new System.Windows.Forms.Label();
            this.labelGraduatedCount = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelOverall.SuspendLayout();
            this.panelGrades.SuspendLayout();
            this.panelTracks.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelTop.Controls.Add(this.labelDate);
            this.panelTop.Controls.Add(this.btnAddStudent);
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(724, 60);
            this.panelTop.TabIndex = 0;
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.labelDate.Location = new System.Drawing.Point(20, 20);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(181, 20);
            this.labelDate.TabIndex = 0;
            this.labelDate.Text = "Friday, September 01";
            // 
            // btnAddStudent
            // 
            this.btnAddStudent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddStudent.Location = new System.Drawing.Point(560, 10);
            this.btnAddStudent.Name = "btnAddStudent";
            this.btnAddStudent.Size = new System.Drawing.Size(150, 40);
            this.btnAddStudent.TabIndex = 1;
            this.btnAddStudent.Text = "Add New Student";
            this.btnAddStudent.UseVisualStyleBackColor = true;
            this.btnAddStudent.Click += new System.EventHandler(this.btnAddStudent_Click);
            // 
            // panelOverall
            // 
            this.panelOverall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(133)))));
            this.panelOverall.Controls.Add(this.labelOverallTitle);
            this.panelOverall.Controls.Add(this.labelTotalCount);
            this.panelOverall.Controls.Add(this.labelMaleTitle);
            this.panelOverall.Controls.Add(this.labelMaleCount);
            this.panelOverall.Controls.Add(this.labelFemaleTitle);
            this.panelOverall.Controls.Add(this.labelFemaleCount);
            this.panelOverall.Location = new System.Drawing.Point(16, 60);
            this.panelOverall.Name = "panelOverall";
            this.panelOverall.Size = new System.Drawing.Size(694, 140);
            this.panelOverall.TabIndex = 1;
            // 
            // labelOverallTitle
            // 
            this.labelOverallTitle.AutoSize = true;
            this.labelOverallTitle.Font = new System.Drawing.Font("Microsoft Tai Le", 16F, System.Drawing.FontStyle.Bold);
            this.labelOverallTitle.ForeColor = System.Drawing.Color.White;
            this.labelOverallTitle.Location = new System.Drawing.Point(310, 10);
            this.labelOverallTitle.Name = "labelOverallTitle";
            this.labelOverallTitle.Size = new System.Drawing.Size(87, 29);
            this.labelOverallTitle.TabIndex = 0;
            this.labelOverallTitle.Text = "Overall";
            // 
            // labelTotalCount
            // 
            this.labelTotalCount.AutoSize = true;
            this.labelTotalCount.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Bold);
            this.labelTotalCount.ForeColor = System.Drawing.Color.White;
            this.labelTotalCount.Location = new System.Drawing.Point(315, 50);
            this.labelTotalCount.Name = "labelTotalCount";
            this.labelTotalCount.Size = new System.Drawing.Size(86, 27);
            this.labelTotalCount.TabIndex = 1;
            this.labelTotalCount.Text = "Total: 0";
            // 
            // labelMaleTitle
            // 
            this.labelMaleTitle.AutoSize = true;
            this.labelMaleTitle.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold);
            this.labelMaleTitle.ForeColor = System.Drawing.Color.White;
            this.labelMaleTitle.Location = new System.Drawing.Point(200, 90);
            this.labelMaleTitle.Name = "labelMaleTitle";
            this.labelMaleTitle.Size = new System.Drawing.Size(52, 21);
            this.labelMaleTitle.TabIndex = 2;
            this.labelMaleTitle.Text = "Male:";
            // 
            // labelMaleCount
            // 
            this.labelMaleCount.AutoSize = true;
            this.labelMaleCount.Font = new System.Drawing.Font("Microsoft Tai Le", 14F, System.Drawing.FontStyle.Bold);
            this.labelMaleCount.ForeColor = System.Drawing.Color.White;
            this.labelMaleCount.Location = new System.Drawing.Point(250, 88);
            this.labelMaleCount.Name = "labelMaleCount";
            this.labelMaleCount.Size = new System.Drawing.Size(21, 23);
            this.labelMaleCount.TabIndex = 3;
            this.labelMaleCount.Text = "0";
            // 
            // labelFemaleTitle
            // 
            this.labelFemaleTitle.AutoSize = true;
            this.labelFemaleTitle.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold);
            this.labelFemaleTitle.ForeColor = System.Drawing.Color.White;
            this.labelFemaleTitle.Location = new System.Drawing.Point(400, 90);
            this.labelFemaleTitle.Name = "labelFemaleTitle";
            this.labelFemaleTitle.Size = new System.Drawing.Size(69, 21);
            this.labelFemaleTitle.TabIndex = 4;
            this.labelFemaleTitle.Text = "Female:";
            // 
            // labelFemaleCount
            // 
            this.labelFemaleCount.AutoSize = true;
            this.labelFemaleCount.Font = new System.Drawing.Font("Microsoft Tai Le", 14F, System.Drawing.FontStyle.Bold);
            this.labelFemaleCount.ForeColor = System.Drawing.Color.White;
            this.labelFemaleCount.Location = new System.Drawing.Point(480, 88);
            this.labelFemaleCount.Name = "labelFemaleCount";
            this.labelFemaleCount.Size = new System.Drawing.Size(21, 23);
            this.labelFemaleCount.TabIndex = 5;
            this.labelFemaleCount.Text = "0";
            // 
            // panelGrades
            // 
            this.panelGrades.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelGrades.Controls.Add(this.labelGradesTitle);
            this.panelGrades.Controls.Add(this.labelG11Title);
            this.panelGrades.Controls.Add(this.labelCountG11);
            this.panelGrades.Controls.Add(this.labelG12Title);
            this.panelGrades.Controls.Add(this.labelCountG12);
            this.panelGrades.Location = new System.Drawing.Point(0, 200);
            this.panelGrades.Name = "panelGrades";
            this.panelGrades.Size = new System.Drawing.Size(724, 120);
            this.panelGrades.TabIndex = 2;
            // 
            // labelGradesTitle
            // 
            this.labelGradesTitle.AutoSize = true;
            this.labelGradesTitle.Font = new System.Drawing.Font("Microsoft Tai Le", 14F, System.Drawing.FontStyle.Bold);
            this.labelGradesTitle.Location = new System.Drawing.Point(300, 10);
            this.labelGradesTitle.Name = "labelGradesTitle";
            this.labelGradesTitle.Size = new System.Drawing.Size(72, 23);
            this.labelGradesTitle.TabIndex = 0;
            this.labelGradesTitle.Text = "Grades";
            // 
            // labelG11Title
            // 
            this.labelG11Title.AutoSize = true;
            this.labelG11Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.labelG11Title.Location = new System.Drawing.Point(220, 50);
            this.labelG11Title.Name = "labelG11Title";
            this.labelG11Title.Size = new System.Drawing.Size(89, 20);
            this.labelG11Title.TabIndex = 1;
            this.labelG11Title.Text = "Grade 11:";
            // 
            // labelCountG11
            // 
            this.labelCountG11.AutoSize = true;
            this.labelCountG11.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold);
            this.labelCountG11.Location = new System.Drawing.Point(310, 50);
            this.labelCountG11.Name = "labelCountG11";
            this.labelCountG11.Size = new System.Drawing.Size(19, 21);
            this.labelCountG11.TabIndex = 2;
            this.labelCountG11.Text = "0";
            // 
            // labelG12Title
            // 
            this.labelG12Title.AutoSize = true;
            this.labelG12Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.labelG12Title.Location = new System.Drawing.Point(220, 80);
            this.labelG12Title.Name = "labelG12Title";
            this.labelG12Title.Size = new System.Drawing.Size(89, 20);
            this.labelG12Title.TabIndex = 3;
            this.labelG12Title.Text = "Grade 12:";
            // 
            // labelCountG12
            // 
            this.labelCountG12.AutoSize = true;
            this.labelCountG12.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold);
            this.labelCountG12.Location = new System.Drawing.Point(310, 80);
            this.labelCountG12.Name = "labelCountG12";
            this.labelCountG12.Size = new System.Drawing.Size(19, 21);
            this.labelCountG12.TabIndex = 4;
            this.labelCountG12.Text = "0";
            // 
            // panelTracks
            // 
            this.panelTracks.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelTracks.Controls.Add(this.labelTrackTitle);
            this.panelTracks.Controls.Add(this.labelSTEMTitle);
            this.panelTracks.Controls.Add(this.labelSTEMCount);
            this.panelTracks.Controls.Add(this.labelGASTitle);
            this.panelTracks.Controls.Add(this.labelGASCount);
            this.panelTracks.Controls.Add(this.labelHETitle);
            this.panelTracks.Controls.Add(this.labelHECount);
            this.panelTracks.Controls.Add(this.labelAgriTitle);
            this.panelTracks.Controls.Add(this.labelAgriCount);
            this.panelTracks.Location = new System.Drawing.Point(0, 320);
            this.panelTracks.Name = "panelTracks";
            this.panelTracks.Size = new System.Drawing.Size(724, 150);
            this.panelTracks.TabIndex = 3;
            // 
            // labelTrackTitle
            // 
            this.labelTrackTitle.AutoSize = true;
            this.labelTrackTitle.Font = new System.Drawing.Font("Microsoft Tai Le", 14F, System.Drawing.FontStyle.Bold);
            this.labelTrackTitle.Location = new System.Drawing.Point(300, 10);
            this.labelTrackTitle.Name = "labelTrackTitle";
            this.labelTrackTitle.Size = new System.Drawing.Size(67, 23);
            this.labelTrackTitle.TabIndex = 0;
            this.labelTrackTitle.Text = "Tracks";
            // 
            // labelSTEMTitle
            // 
            this.labelSTEMTitle.AutoSize = true;
            this.labelSTEMTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.labelSTEMTitle.Location = new System.Drawing.Point(150, 50);
            this.labelSTEMTitle.Name = "labelSTEMTitle";
            this.labelSTEMTitle.Size = new System.Drawing.Size(59, 18);
            this.labelSTEMTitle.TabIndex = 1;
            this.labelSTEMTitle.Text = "STEM:";
            // 
            // labelSTEMCount
            // 
            this.labelSTEMCount.AutoSize = true;
            this.labelSTEMCount.Font = new System.Drawing.Font("Microsoft Tai Le", 11F, System.Drawing.FontStyle.Bold);
            this.labelSTEMCount.Location = new System.Drawing.Point(210, 50);
            this.labelSTEMCount.Name = "labelSTEMCount";
            this.labelSTEMCount.Size = new System.Drawing.Size(18, 19);
            this.labelSTEMCount.TabIndex = 2;
            this.labelSTEMCount.Text = "0";
            // 
            // labelGASTitle
            // 
            this.labelGASTitle.AutoSize = true;
            this.labelGASTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.labelGASTitle.Location = new System.Drawing.Point(380, 50);
            this.labelGASTitle.Name = "labelGASTitle";
            this.labelGASTitle.Size = new System.Drawing.Size(47, 18);
            this.labelGASTitle.TabIndex = 3;
            this.labelGASTitle.Text = "GAS:";
            // 
            // labelGASCount
            // 
            this.labelGASCount.AutoSize = true;
            this.labelGASCount.Font = new System.Drawing.Font("Microsoft Tai Le", 11F, System.Drawing.FontStyle.Bold);
            this.labelGASCount.Location = new System.Drawing.Point(430, 50);
            this.labelGASCount.Name = "labelGASCount";
            this.labelGASCount.Size = new System.Drawing.Size(18, 19);
            this.labelGASCount.TabIndex = 4;
            this.labelGASCount.Text = "0";
            // 
            // labelHETitle
            // 
            this.labelHETitle.AutoSize = true;
            this.labelHETitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.labelHETitle.Location = new System.Drawing.Point(150, 90);
            this.labelHETitle.Name = "labelHETitle";
            this.labelHETitle.Size = new System.Drawing.Size(46, 18);
            this.labelHETitle.TabIndex = 5;
            this.labelHETitle.Text = "H.E.:";
            // 
            // labelHECount
            // 
            this.labelHECount.AutoSize = true;
            this.labelHECount.Font = new System.Drawing.Font("Microsoft Tai Le", 11F, System.Drawing.FontStyle.Bold);
            this.labelHECount.Location = new System.Drawing.Point(210, 90);
            this.labelHECount.Name = "labelHECount";
            this.labelHECount.Size = new System.Drawing.Size(18, 19);
            this.labelHECount.TabIndex = 6;
            this.labelHECount.Text = "0";
            // 
            // labelAgriTitle
            // 
            this.labelAgriTitle.AutoSize = true;
            this.labelAgriTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.labelAgriTitle.Location = new System.Drawing.Point(380, 90);
            this.labelAgriTitle.Name = "labelAgriTitle";
            this.labelAgriTitle.Size = new System.Drawing.Size(52, 18);
            this.labelAgriTitle.TabIndex = 7;
            this.labelAgriTitle.Text = "AGRI:";
            // 
            // labelAgriCount
            // 
            this.labelAgriCount.AutoSize = true;
            this.labelAgriCount.Font = new System.Drawing.Font("Microsoft Tai Le", 11F, System.Drawing.FontStyle.Bold);
            this.labelAgriCount.Location = new System.Drawing.Point(430, 90);
            this.labelAgriCount.Name = "labelAgriCount";
            this.labelAgriCount.Size = new System.Drawing.Size(18, 19);
            this.labelAgriCount.TabIndex = 8;
            this.labelAgriCount.Text = "0";
            // 
            // panelStatus
            // 
            this.panelStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelStatus.Controls.Add(this.labelStatusTitle);
            this.panelStatus.Controls.Add(this.labelEnrolledTitle);
            this.panelStatus.Controls.Add(this.labelEnrolledCount);
            this.panelStatus.Controls.Add(this.labelGraduatedTitle);
            this.panelStatus.Controls.Add(this.labelGraduatedCount);
            this.panelStatus.Location = new System.Drawing.Point(0, 470);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(724, 182);
            this.panelStatus.TabIndex = 4;
            // 
            // labelStatusTitle
            // 
            this.labelStatusTitle.AutoSize = true;
            this.labelStatusTitle.Font = new System.Drawing.Font("Microsoft Tai Le", 14F, System.Drawing.FontStyle.Bold);
            this.labelStatusTitle.Location = new System.Drawing.Point(300, 10);
            this.labelStatusTitle.Name = "labelStatusTitle";
            this.labelStatusTitle.Size = new System.Drawing.Size(168, 23);
            this.labelStatusTitle.TabIndex = 0;
            this.labelStatusTitle.Text = "Enrollment Status";
            // 
            // labelEnrolledTitle
            // 
            this.labelEnrolledTitle.AutoSize = true;
            this.labelEnrolledTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.labelEnrolledTitle.Location = new System.Drawing.Point(220, 60);
            this.labelEnrolledTitle.Name = "labelEnrolledTitle";
            this.labelEnrolledTitle.Size = new System.Drawing.Size(80, 20);
            this.labelEnrolledTitle.TabIndex = 1;
            this.labelEnrolledTitle.Text = "Enrolled:";
            // 
            // labelEnrolledCount
            // 
            this.labelEnrolledCount.AutoSize = true;
            this.labelEnrolledCount.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold);
            this.labelEnrolledCount.Location = new System.Drawing.Point(320, 60);
            this.labelEnrolledCount.Name = "labelEnrolledCount";
            this.labelEnrolledCount.Size = new System.Drawing.Size(19, 21);
            this.labelEnrolledCount.TabIndex = 2;
            this.labelEnrolledCount.Text = "0";
            // 
            // labelGraduatedTitle
            // 
            this.labelGraduatedTitle.AutoSize = true;
            this.labelGraduatedTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.labelGraduatedTitle.Location = new System.Drawing.Point(220, 100);
            this.labelGraduatedTitle.Name = "labelGraduatedTitle";
            this.labelGraduatedTitle.Size = new System.Drawing.Size(100, 20);
            this.labelGraduatedTitle.TabIndex = 3;
            this.labelGraduatedTitle.Text = "Graduated:";
            // 
            // labelGraduatedCount
            // 
            this.labelGraduatedCount.AutoSize = true;
            this.labelGraduatedCount.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold);
            this.labelGraduatedCount.Location = new System.Drawing.Point(320, 100);
            this.labelGraduatedCount.Name = "labelGraduatedCount";
            this.labelGraduatedCount.Size = new System.Drawing.Size(19, 21);
            this.labelGraduatedCount.TabIndex = 4;
            this.labelGraduatedCount.Text = "0";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelOverall);
            this.Controls.Add(this.panelGrades);
            this.Controls.Add(this.panelTracks);
            this.Controls.Add(this.panelStatus);
            this.Name = "Dashboard";
            this.Size = new System.Drawing.Size(724, 652);
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelOverall.ResumeLayout(false);
            this.panelOverall.PerformLayout();
            this.panelGrades.ResumeLayout(false);
            this.panelGrades.PerformLayout();
            this.panelTracks.ResumeLayout(false);
            this.panelTracks.PerformLayout();
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
