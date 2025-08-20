namespace TeaboyTimesheet
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.inputClientName = new System.Windows.Forms.TextBox();
            this.inputHours = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.inputDate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.createNewDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(60)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(532, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewDocumentToolStripMenuItem,
            this.openDocumentToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Client Name";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // inputClientName
            // 
            this.inputClientName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(108)))));
            this.inputClientName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputClientName.ForeColor = System.Drawing.SystemColors.InfoText;
            this.inputClientName.Location = new System.Drawing.Point(15, 52);
            this.inputClientName.Name = "inputClientName";
            this.inputClientName.Size = new System.Drawing.Size(170, 20);
            this.inputClientName.TabIndex = 2;
            // 
            // inputHours
            // 
            this.inputHours.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(122)))), ((int)(((byte)(108)))));
            this.inputHours.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputHours.ForeColor = System.Drawing.SystemColors.InfoText;
            this.inputHours.Location = new System.Drawing.Point(206, 52);
            this.inputHours.Name = "inputHours";
            this.inputHours.Size = new System.Drawing.Size(170, 20);
            this.inputHours.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(203, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hours";
            // 
            // inputDate
            // 
            this.inputDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(92)))), ((int)(((byte)(78)))));
            this.inputDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputDate.ForeColor = System.Drawing.SystemColors.InfoText;
            this.inputDate.Location = new System.Drawing.Point(395, 52);
            this.inputDate.Name = "inputDate";
            this.inputDate.ReadOnly = true;
            this.inputDate.Size = new System.Drawing.Size(114, 20);
            this.inputDate.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(392, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Current Date";
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(143)))), ((int)(((byte)(135)))));
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonAdd.Location = new System.Drawing.Point(221, 90);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(89, 57);
            this.buttonAdd.TabIndex = 7;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = false;
            // 
            // createNewDocumentToolStripMenuItem
            // 
            this.createNewDocumentToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(60)))));
            this.createNewDocumentToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.createNewDocumentToolStripMenuItem.Name = "createNewDocumentToolStripMenuItem";
            this.createNewDocumentToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.createNewDocumentToolStripMenuItem.Text = "Create New Document";
            // 
            // openDocumentToolStripMenuItem
            // 
            this.openDocumentToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(60)))));
            this.openDocumentToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.openDocumentToolStripMenuItem.Name = "openDocumentToolStripMenuItem";
            this.openDocumentToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.openDocumentToolStripMenuItem.Text = "Open Document";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(532, 159);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.inputDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.inputHours);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.inputClientName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Teaboy Timesheet";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputClientName;
        private System.Windows.Forms.TextBox inputHours;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox inputDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ToolStripMenuItem createNewDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDocumentToolStripMenuItem;
    }
}

