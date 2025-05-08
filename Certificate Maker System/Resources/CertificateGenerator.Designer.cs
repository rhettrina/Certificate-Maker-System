namespace Certificate_Maker_System.Resources
{
    partial class CertificateGenerator
    {
        private System.ComponentModel.IContainer components = null;

        // Original / Basic fields
        private System.Windows.Forms.ComboBox types;
        private System.Windows.Forms.TextBox lrn;
        private System.Windows.Forms.ComboBox trackbox;
        private System.Windows.Forms.ComboBox gradebox;
        private System.Windows.Forms.TextBox namebox;
        private System.Windows.Forms.TextBox searchbox;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonGenerate;

        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Label labelLRN;
        private System.Windows.Forms.Label labelTrack;
        private System.Windows.Forms.Label labelGradeSec;
        private System.Windows.Forms.Label labelName;

        // Advanced fields
        private System.Windows.Forms.Label labelSemester;
        private System.Windows.Forms.ComboBox semesterBox;

        // School year split
        private System.Windows.Forms.Label labelSchoolYearStart;
        private System.Windows.Forms.Label labelSchoolYearEnd;
        private System.Windows.Forms.ComboBox startYearCombo;
        private System.Windows.Forms.ComboBox endYearCombo;

        private System.Windows.Forms.Label labelPurpose;
        private System.Windows.Forms.TextBox purposeBox;
        private System.Windows.Forms.Label labelIssueDate;
        private System.Windows.Forms.DateTimePicker issueDatePicker;

        // Registrar/Principal
        private System.Windows.Forms.Label labelRegistrar;
        private System.Windows.Forms.TextBox registrarBox;
        private System.Windows.Forms.Label labelPrincipal;
        private System.Windows.Forms.TextBox principalBox;

        // Left preview panel
        private System.Windows.Forms.Panel panelPreview;

        // New batch generate button
        private System.Windows.Forms.Button buttonBatchGenerate;

        /// <summary> 
        /// Clean up resources.
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

        private void InitializeComponent()
        {
            this.panelPreview = new System.Windows.Forms.Panel();
            this.searchbox = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.labelType = new System.Windows.Forms.Label();
            this.types = new System.Windows.Forms.ComboBox();
            this.labelLRN = new System.Windows.Forms.Label();
            this.lrn = new System.Windows.Forms.TextBox();
            this.labelGradeSec = new System.Windows.Forms.Label();
            this.gradebox = new System.Windows.Forms.ComboBox();
            this.labelName = new System.Windows.Forms.Label();
            this.namebox = new System.Windows.Forms.TextBox();
            this.labelTrack = new System.Windows.Forms.Label();
            this.trackbox = new System.Windows.Forms.ComboBox();
            this.labelSemester = new System.Windows.Forms.Label();
            this.semesterBox = new System.Windows.Forms.ComboBox();
            this.labelSchoolYearStart = new System.Windows.Forms.Label();
            this.startYearCombo = new System.Windows.Forms.ComboBox();
            this.labelSchoolYearEnd = new System.Windows.Forms.Label();
            this.endYearCombo = new System.Windows.Forms.ComboBox();
            this.labelPurpose = new System.Windows.Forms.Label();
            this.purposeBox = new System.Windows.Forms.TextBox();
            this.labelIssueDate = new System.Windows.Forms.Label();
            this.issueDatePicker = new System.Windows.Forms.DateTimePicker();
            this.labelRegistrar = new System.Windows.Forms.Label();
            this.registrarBox = new System.Windows.Forms.TextBox();
            this.labelPrincipal = new System.Windows.Forms.Label();
            this.principalBox = new System.Windows.Forms.TextBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.buttonBatchGenerate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelPreview
            // 
            this.panelPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreview.Location = new System.Drawing.Point(20, 20);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(400, 500);
            this.panelPreview.TabIndex = 0;
            // 
            // searchbox
            // 
            this.searchbox.BackColor = System.Drawing.Color.LightGray;
            this.searchbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.searchbox.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.searchbox.Location = new System.Drawing.Point(445, 20);
            this.searchbox.Name = "searchbox";
            this.searchbox.Size = new System.Drawing.Size(180, 26);
            this.searchbox.TabIndex = 0;
            this.searchbox.Text = "Search LRN or Student Name";
            this.searchbox.Enter += new System.EventHandler(this.searchenter);
            this.searchbox.Leave += new System.EventHandler(this.searchleave);
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(133)))));
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonSearch.ForeColor = System.Drawing.Color.White;
            this.buttonSearch.Location = new System.Drawing.Point(630, 20);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(70, 26);
            this.buttonSearch.TabIndex = 1;
            this.buttonSearch.Text = "SEARCH";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.searchbtn);
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Bold);
            this.labelType.Location = new System.Drawing.Point(445, 60);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(114, 18);
            this.labelType.TabIndex = 2;
            this.labelType.Text = "Certificate Type";
            // 
            // types
            // 
            this.types.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.types.Font = new System.Drawing.Font("Microsoft Tai Le", 10F);
            this.types.FormattingEnabled = true;
            this.types.Location = new System.Drawing.Point(445, 80);
            this.types.Name = "types";
            this.types.Size = new System.Drawing.Size(255, 24);
            this.types.TabIndex = 3;
            this.types.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // labelLRN
            // 
            this.labelLRN.AutoSize = true;
            this.labelLRN.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Bold);
            this.labelLRN.Location = new System.Drawing.Point(445, 115);
            this.labelLRN.Name = "labelLRN";
            this.labelLRN.Size = new System.Drawing.Size(35, 18);
            this.labelLRN.TabIndex = 4;
            this.labelLRN.Text = "LRN";
            // 
            // lrn
            // 
            this.lrn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lrn.Location = new System.Drawing.Point(445, 135);
            this.lrn.Name = "lrn";
            this.lrn.Size = new System.Drawing.Size(255, 23);
            this.lrn.TabIndex = 5;
            // 
            // labelGradeSec
            // 
            this.labelGradeSec.AutoSize = true;
            this.labelGradeSec.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Bold);
            this.labelGradeSec.Location = new System.Drawing.Point(445, 170);
            this.labelGradeSec.Name = "labelGradeSec";
            this.labelGradeSec.Size = new System.Drawing.Size(104, 18);
            this.labelGradeSec.TabIndex = 6;
            this.labelGradeSec.Text = "Grade/Section";
            // 
            // gradebox
            // 
            this.gradebox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.gradebox.FormattingEnabled = true;
            this.gradebox.Location = new System.Drawing.Point(445, 190);
            this.gradebox.Name = "gradebox";
            this.gradebox.Size = new System.Drawing.Size(255, 24);
            this.gradebox.TabIndex = 7;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Bold);
            this.labelName.Location = new System.Drawing.Point(445, 225);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(103, 18);
            this.labelName.TabIndex = 8;
            this.labelName.Text = "Student Name";
            // 
            // namebox
            // 
            this.namebox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.namebox.Location = new System.Drawing.Point(445, 245);
            this.namebox.Name = "namebox";
            this.namebox.Size = new System.Drawing.Size(255, 23);
            this.namebox.TabIndex = 9;
            // 
            // labelTrack
            // 
            this.labelTrack.AutoSize = true;
            this.labelTrack.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Bold);
            this.labelTrack.Location = new System.Drawing.Point(445, 280);
            this.labelTrack.Name = "labelTrack";
            this.labelTrack.Size = new System.Drawing.Size(45, 18);
            this.labelTrack.TabIndex = 10;
            this.labelTrack.Text = "Track";
            // 
            // trackbox
            // 
            this.trackbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.trackbox.FormattingEnabled = true;
            this.trackbox.Location = new System.Drawing.Point(445, 300);
            this.trackbox.Name = "trackbox";
            this.trackbox.Size = new System.Drawing.Size(255, 24);
            this.trackbox.TabIndex = 11;
            // 
            // labelSemester
            // 
            this.labelSemester.AutoSize = true;
            this.labelSemester.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelSemester.Location = new System.Drawing.Point(445, 333);
            this.labelSemester.Name = "labelSemester";
            this.labelSemester.Size = new System.Drawing.Size(63, 16);
            this.labelSemester.TabIndex = 13;
            this.labelSemester.Text = "Semester";
            // 
            // semesterBox
            // 
            this.semesterBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.semesterBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.semesterBox.Items.AddRange(new object[] {
            "1st Semester",
            "2nd Semester"});
            this.semesterBox.Location = new System.Drawing.Point(445, 353);
            this.semesterBox.Name = "semesterBox";
            this.semesterBox.Size = new System.Drawing.Size(255, 24);
            this.semesterBox.TabIndex = 14;
            // 
            // labelSchoolYearStart
            // 
            this.labelSchoolYearStart.AutoSize = true;
            this.labelSchoolYearStart.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelSchoolYearStart.Location = new System.Drawing.Point(445, 390);
            this.labelSchoolYearStart.Name = "labelSchoolYearStart";
            this.labelSchoolYearStart.Size = new System.Drawing.Size(122, 16);
            this.labelSchoolYearStart.TabIndex = 15;
            this.labelSchoolYearStart.Text = "School Year (Start)";
            // 
            // startYearCombo
            // 
            this.startYearCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.startYearCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.startYearCombo.FormattingEnabled = true;
            this.startYearCombo.Items.AddRange(new object[] {
            "2021",
            "2022",
            "2023",
            "2024",
            "2025"});
            this.startYearCombo.Location = new System.Drawing.Point(445, 410);
            this.startYearCombo.Name = "startYearCombo";
            this.startYearCombo.Size = new System.Drawing.Size(110, 24);
            this.startYearCombo.TabIndex = 16;
            // 
            // labelSchoolYearEnd
            // 
            this.labelSchoolYearEnd.AutoSize = true;
            this.labelSchoolYearEnd.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelSchoolYearEnd.Location = new System.Drawing.Point(590, 390);
            this.labelSchoolYearEnd.Name = "labelSchoolYearEnd";
            this.labelSchoolYearEnd.Size = new System.Drawing.Size(116, 16);
            this.labelSchoolYearEnd.TabIndex = 17;
            this.labelSchoolYearEnd.Text = "School Year (End)";
            // 
            // endYearCombo
            // 
            this.endYearCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.endYearCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.endYearCombo.FormattingEnabled = true;
            this.endYearCombo.Items.AddRange(new object[] {
            "2021",
            "2022",
            "2023",
            "2024",
            "2025"});
            this.endYearCombo.Location = new System.Drawing.Point(590, 410);
            this.endYearCombo.Name = "endYearCombo";
            this.endYearCombo.Size = new System.Drawing.Size(110, 24);
            this.endYearCombo.TabIndex = 18;
            // 
            // labelPurpose
            // 
            this.labelPurpose.AutoSize = true;
            this.labelPurpose.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelPurpose.Location = new System.Drawing.Point(445, 445);
            this.labelPurpose.Name = "labelPurpose";
            this.labelPurpose.Size = new System.Drawing.Size(57, 16);
            this.labelPurpose.TabIndex = 19;
            this.labelPurpose.Text = "Purpose";
            // 
            // purposeBox
            // 
            this.purposeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.purposeBox.Location = new System.Drawing.Point(445, 465);
            this.purposeBox.Multiline = true;
            this.purposeBox.Name = "purposeBox";
            this.purposeBox.Size = new System.Drawing.Size(255, 50);
            this.purposeBox.TabIndex = 20;
            // 
            // labelIssueDate
            // 
            this.labelIssueDate.AutoSize = true;
            this.labelIssueDate.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelIssueDate.Location = new System.Drawing.Point(445, 530);
            this.labelIssueDate.Name = "labelIssueDate";
            this.labelIssueDate.Size = new System.Drawing.Size(71, 16);
            this.labelIssueDate.TabIndex = 21;
            this.labelIssueDate.Text = "Issue Date";
            // 
            // issueDatePicker
            // 
            this.issueDatePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.issueDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.issueDatePicker.Location = new System.Drawing.Point(525, 525);
            this.issueDatePicker.Name = "issueDatePicker";
            this.issueDatePicker.Size = new System.Drawing.Size(85, 21);
            this.issueDatePicker.TabIndex = 22;
            // 
            // labelRegistrar
            // 
            this.labelRegistrar.AutoSize = true;
            this.labelRegistrar.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelRegistrar.Location = new System.Drawing.Point(445, 560);
            this.labelRegistrar.Name = "labelRegistrar";
            this.labelRegistrar.Size = new System.Drawing.Size(62, 16);
            this.labelRegistrar.TabIndex = 23;
            this.labelRegistrar.Text = "Registrar";
            // 
            // registrarBox
            // 
            this.registrarBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.registrarBox.Location = new System.Drawing.Point(445, 580);
            this.registrarBox.Name = "registrarBox";
            this.registrarBox.Size = new System.Drawing.Size(255, 23);
            this.registrarBox.TabIndex = 24;
            // 
            // labelPrincipal
            // 
            this.labelPrincipal.AutoSize = true;
            this.labelPrincipal.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelPrincipal.Location = new System.Drawing.Point(445, 560);
            this.labelPrincipal.Name = "labelPrincipal";
            this.labelPrincipal.Size = new System.Drawing.Size(61, 16);
            this.labelPrincipal.TabIndex = 25;
            this.labelPrincipal.Text = "Principal";
            this.labelPrincipal.Visible = false;
            // 
            // principalBox
            // 
            this.principalBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.principalBox.Location = new System.Drawing.Point(445, 580);
            this.principalBox.Name = "principalBox";
            this.principalBox.Size = new System.Drawing.Size(255, 23);
            this.principalBox.TabIndex = 26;
            this.principalBox.Visible = false;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(133)))));
            this.buttonGenerate.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Bold);
            this.buttonGenerate.ForeColor = System.Drawing.Color.White;
            this.buttonGenerate.Location = new System.Drawing.Point(20, 536);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(400, 40);
            this.buttonGenerate.TabIndex = 27;
            this.buttonGenerate.Text = "Generate Certificate";
            this.buttonGenerate.UseVisualStyleBackColor = false;
            this.buttonGenerate.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonBatchGenerate
            // 
            this.buttonBatchGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.buttonBatchGenerate.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Bold);
            this.buttonBatchGenerate.ForeColor = System.Drawing.Color.White;
            this.buttonBatchGenerate.Location = new System.Drawing.Point(20, 586);
            this.buttonBatchGenerate.Name = "buttonBatchGenerate";
            this.buttonBatchGenerate.Size = new System.Drawing.Size(400, 40);
            this.buttonBatchGenerate.TabIndex = 28;
            this.buttonBatchGenerate.Text = "Batch Generate (Future)";
            this.buttonBatchGenerate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonBatchGenerate.UseVisualStyleBackColor = false;
            this.buttonBatchGenerate.Visible = false;
            this.buttonBatchGenerate.Click += new System.EventHandler(this.buttonBatchGenerate_Click);
            // 
            // CertificateGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.panelPreview);
            this.Controls.Add(this.searchbox);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.types);
            this.Controls.Add(this.labelLRN);
            this.Controls.Add(this.lrn);
            this.Controls.Add(this.labelGradeSec);
            this.Controls.Add(this.gradebox);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.namebox);
            this.Controls.Add(this.labelTrack);
            this.Controls.Add(this.trackbox);
            this.Controls.Add(this.labelSemester);
            this.Controls.Add(this.semesterBox);
            this.Controls.Add(this.labelSchoolYearStart);
            this.Controls.Add(this.startYearCombo);
            this.Controls.Add(this.labelSchoolYearEnd);
            this.Controls.Add(this.endYearCombo);
            this.Controls.Add(this.labelPurpose);
            this.Controls.Add(this.purposeBox);
            this.Controls.Add(this.labelIssueDate);
            this.Controls.Add(this.issueDatePicker);
            this.Controls.Add(this.labelRegistrar);
            this.Controls.Add(this.registrarBox);
            this.Controls.Add(this.labelPrincipal);
            this.Controls.Add(this.principalBox);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.buttonBatchGenerate);
            this.Name = "CertificateGenerator";
            this.Size = new System.Drawing.Size(724, 652);
            this.Load += new System.EventHandler(this.CertificateGenerator_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
