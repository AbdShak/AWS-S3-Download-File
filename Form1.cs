using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace S3DownloadFile
{
    public partial class Form1 : Form
    {
        AmazonS3Util amazonS3Util = new AmazonS3Util();

        public Form1()
        {
            InitializeComponent();
        }

        private async void Downloadbutton_Click(object sender, EventArgs e)
        {
            // Get Current Software Version
            string curentVersion = Assembly.GetEntryAssembly().GetName().Version.Major.ToString() + "." + Assembly.GetEntryAssembly().GetName().Version.Minor.ToString();

            // Create the folder in AppData if it doesn't exist
            string downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + ConfigurationManager.OpenExeConfiguration(this.GetType().Assembly.Location).AppSettings.Settings["MainFolder"].Value;

            if (!Directory.Exists(downloadPath))
            {
                Directory.CreateDirectory(downloadPath);
            }

            // Get the latest Version number from s3 latest installer file name
            amazonS3Util.GetFoldersNamesFromS3();
            string s3Version = amazonS3Util.GetLatestSetupFileVersion();

            // Compare S3 Version with the current version
            if (s3Version.CompareTo(curentVersion) > 0)
            {
                try
                {
                    // Download the new version
                    await amazonS3Util.DownloadTheNewSetupFile(s3Version);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
    }
}
