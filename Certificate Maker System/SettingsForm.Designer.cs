// SettingsForm.Designer.cs
namespace Certificate_Maker_System
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Button btnBrowsePath;
        private System.Windows.Forms.Button btnAddSection;
        private System.Windows.Forms.Button btnRemoveSection;
        private System.Windows.Forms.Button btnAddTrack;
        private System.Windows.Forms.Button btnRemoveTrack;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox comboSection;
        private System.Windows.Forms.ComboBox comboTrack;
        private System.Windows.Forms.Label lblOutputPath;
        private System.Windows.Forms.Label lblSection;
        private System.Windows.Forms.Label lblTrack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {// Add this at the end of the InitializeComponent() method, just before the ResumeLayout call
            this.Load += new System.EventHandler(this.SettingsForm_Load);

            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.btnBrowsePath = new System.Windows.Forms.Button();
            this.btnAddSection = new System.Windows.Forms.Button();
            this.btnRemoveSection = new System.Windows.Forms.Button();
            this.btnAddTrack = new System.Windows.Forms.Button();
            this.btnRemoveTrack = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.comboSection = new System.Windows.Forms.ComboBox();
            this.comboTrack = new System.Windows.Forms.ComboBox();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.lblSection = new System.Windows.Forms.Label();
            this.lblTrack = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(108, 12);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(310, 20);
            this.txtOutputPath.TabIndex = 0;
            // 
            // btnBrowsePath
            // 
            this.btnBrowsePath.Location = new System.Drawing.Point(424, 10);
            this.btnBrowsePath.Name = "btnBrowsePath";
            this.btnBrowsePath.Size = new System.Drawing.Size(75, 23);
            this.btnBrowsePath.TabIndex = 1;
            this.btnBrowsePath.Text = "Browse...";
            this.btnBrowsePath.UseVisualStyleBackColor = true;
            this.btnBrowsePath.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnAddSection
            // 
            this.btnAddSection.Location = new System.Drawing.Point(424, 49);
            this.btnAddSection.Name = "btnAddSection";
            this.btnAddSection.Size = new System.Drawing.Size(75, 23);
            this.btnAddSection.TabIndex = 2;
            this.btnAddSection.Text = "Add";
            this.btnAddSection.UseVisualStyleBackColor = true;
            this.btnAddSection.Click += new System.EventHandler(this.btnAddSection_Click);
            // 
            // btnRemoveSection
            // 
            this.btnRemoveSection.Location = new System.Drawing.Point(505, 49);
            this.btnRemoveSection.Name = "btnRemoveSection";
            this.btnRemoveSection.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveSection.TabIndex = 3;
            this.btnRemoveSection.Text = "Remove";
            this.btnRemoveSection.UseVisualStyleBackColor = true;
            this.btnRemoveSection.Click += new System.EventHandler(this.btnDeleteSection_Click);
            // 
            // btnAddTrack
            // 
            this.btnAddTrack.Location = new System.Drawing.Point(424, 85);
            this.btnAddTrack.Name = "btnAddTrack";
            this.btnAddTrack.Size = new System.Drawing.Size(75, 23);
            this.btnAddTrack.TabIndex = 4;
            this.btnAddTrack.Text = "Add";
            this.btnAddTrack.UseVisualStyleBackColor = true;
            this.btnAddTrack.Click += new System.EventHandler(this.btnAddTrack_Click);
            // 
            // btnRemoveTrack
            // 
            this.btnRemoveTrack.Location = new System.Drawing.Point(505, 85);
            this.btnRemoveTrack.Name = "btnRemoveTrack";
            this.btnRemoveTrack.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveTrack.TabIndex = 5;
            this.btnRemoveTrack.Text = "Remove";
            this.btnRemoveTrack.UseVisualStyleBackColor = true;
            this.btnRemoveTrack.Click += new System.EventHandler(this.btnDeleteTrack_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(505, 131);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Done";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // comboSection
            // 
            this.comboSection.FormattingEnabled = true;
            this.comboSection.Location = new System.Drawing.Point(108, 50);
            this.comboSection.Name = "comboSection";
            this.comboSection.Size = new System.Drawing.Size(310, 21);
            this.comboSection.TabIndex = 7;
            // 
            // comboTrack
            // 
            this.comboTrack.FormattingEnabled = true;
            this.comboTrack.Location = new System.Drawing.Point(108, 87);
            this.comboTrack.Name = "comboTrack";
            this.comboTrack.Size = new System.Drawing.Size(310, 21);
            this.comboTrack.TabIndex = 8;
            // 
            // lblOutputPath
            // 
            this.lblOutputPath.AutoSize = true;
            this.lblOutputPath.Location = new System.Drawing.Point(12, 15);
            this.lblOutputPath.Name = "lblOutputPath";
            this.lblOutputPath.Size = new System.Drawing.Size(68, 13);
            this.lblOutputPath.TabIndex = 9;
            this.lblOutputPath.Text = "Output Path:";
            // 
            // lblSection
            // 
            this.lblSection.AutoSize = true;
            this.lblSection.Location = new System.Drawing.Point(12, 53);
            this.lblSection.Name = "lblSection";
            this.lblSection.Size = new System.Drawing.Size(46, 13);
            this.lblSection.TabIndex = 10;
            this.lblSection.Text = "Section:";
            // 
            // lblTrack
            // 
            this.lblTrack.AutoSize = true;
            this.lblTrack.Location = new System.Drawing.Point(12, 90);
            this.lblTrack.Name = "lblTrack";
            this.lblTrack.Size = new System.Drawing.Size(38, 13);
            this.lblTrack.TabIndex = 11;
            this.lblTrack.Text = "Track:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 166);
            this.Controls.Add(this.lblTrack);
            this.Controls.Add(this.lblSection);
            this.Controls.Add(this.lblOutputPath);
            this.Controls.Add(this.comboTrack);
            this.Controls.Add(this.comboSection);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRemoveTrack);
            this.Controls.Add(this.btnAddTrack);
            this.Controls.Add(this.btnRemoveSection);
            this.Controls.Add(this.btnAddSection);
            this.Controls.Add(this.btnBrowsePath);
            this.Controls.Add(this.txtOutputPath);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
