namespace Certificate_Maker_System
{
    partial class ManageButton
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Dispose resources.
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
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(ManageButton));
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle rowStyle = new System.Windows.Forms.DataGridViewCellStyle();

            this.button1 = new System.Windows.Forms.Button();
            this.managetable = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.managetable)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(0, 32, 133);
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F,
                                System.Drawing.FontStyle.Bold,
                                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(570, 603);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 34);
            this.button1.TabIndex = 7;
            this.button1.Text = "ADD USER";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.adduserbtn);
            // 
            // managetable
            // 
            this.managetable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.managetable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.managetable.Location = new System.Drawing.Point(23, 67);
            this.managetable.Name = "managetable";
            this.managetable.Size = new System.Drawing.Size(685, 513);
            this.managetable.TabIndex = 6;

            // 1) Make the table read-only
            this.managetable.ReadOnly = true;
            // 2) Hide the row header (left edge)
            this.managetable.RowHeadersVisible = false;
            // 3) Disallow users from resizing columns/rows
            this.managetable.AllowUserToResizeColumns = false;
            this.managetable.AllowUserToResizeRows = false;
            // 4) Use full-row selection mode, so a single click selects the entire row
            this.managetable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.managetable.MultiSelect = false;

            // Disable default header styling for custom theme
            this.managetable.EnableHeadersVisualStyles = false;
            this.managetable.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.managetable.ColumnHeadersHeight = 35;

            // Header styling (blue background, white text, bold font)
            headerStyle.BackColor = System.Drawing.Color.FromArgb(0, 32, 133);
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 42, 173);
            headerStyle.SelectionForeColor = System.Drawing.Color.White;
            this.managetable.ColumnHeadersDefaultCellStyle = headerStyle;

            // Row styling (white background, black text, highlight on selection)
            rowStyle.BackColor = System.Drawing.Color.White;
            rowStyle.ForeColor = System.Drawing.Color.Black;
            rowStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            rowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 42, 173);
            rowStyle.SelectionForeColor = System.Drawing.Color.White;
            this.managetable.DefaultCellStyle = rowStyle;

            // You can handle the row click here if desired
            // e.g., CellClick or CellContentClick
            // this.managetable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.managetable_CellClick);

            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(23, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(685, 2);
            this.panel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 18F,
                                System.Drawing.FontStyle.Bold,
                                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 31);
            this.label1.TabIndex = 4;
            this.label1.Text = "Manage Users";
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(639, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(30, 30);
            this.button3.TabIndex = 9;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.editbtn);
            // 
            // button2
            // 
            this.button2.BackgroundImage =
                ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(675, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 30);
            this.button2.TabIndex = 8;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.deletebtn);
            // 
            // ManageButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.managetable);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "ManageButton";
            this.Size = new System.Drawing.Size(724, 652);
            this.Load += new System.EventHandler(this.ManageButton_Load);
            ((System.ComponentModel.ISupportInitialize)(this.managetable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.DataGridView managetable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
    }
}
