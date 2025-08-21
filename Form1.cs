using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeaboyTimesheet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            inputDate.Text = DateTime.Now.ToString("MM/dd/yy");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

            //Try convert
            decimal Hours;
            try
            {
                Hours = Convert.ToDecimal(inputHours.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oopsiepoopsie! You sweem to hab nawt entewed a numbew fow hours. Pwease entew a vawid numbew in decimaw fowmat. uwu", "Oopsie!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Program.minion.AddNewEntry(inputClientName.Text, Convert.ToDecimal(inputHours.Text), DateTime.Now);
            inputClientName.Text = "";
            inputHours.Text = "";

            SystemSounds.Beep.Play();
        }

        private void createNewDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.minion.FreshLog();
            SystemSounds.Beep.Play();
        }

        private void openDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string DocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string DefaultLogFolderPath = Path.Combine(DocumentsPath, "Teaboy Timesheet");

            Process.Start("explorer.exe", DefaultLogFolderPath);
        }

        private void contactSupportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please contact Aresima for software support.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Question);

        }

        private void programInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Code by Aresima. \nMatcha images made by https://bsky.app/profile/xo-ziel.bsky.social", "Info", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        
    }
}
