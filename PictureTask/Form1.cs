using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PictureTask
{
    public partial class Form1 : Form
    {
        string folderName = "Pictures";
        string fileName = "Readme.txt";
        bool firstMessage = true;
        bool createFolder = true;
        

        public Form1()
        {
            InitializeComponent();
            btnClearPicture.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pathImage = Path.Combine(Directory.GetCurrentDirectory() + @"\..\..\", folderName);
            if (createFolder)
            {
                Directory.CreateDirectory(pathImage);
                File.Create(Path.GetFullPath(pathImage) + $@"\{fileName}");
                createFolder = false;
            }

            using (var ofd = new OpenFileDialog())
            {
                if (firstMessage)
                {
                    MessageBox.Show($"Created new folder: {folderName}\n\nAdd images to this folder\n\nLocalization folder: \n{Path.GetFullPath(pathImage)}", "Add Picture", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    firstMessage = false;
                }

                ofd.InitialDirectory = Path.GetFullPath(pathImage);
                ofd.RestoreDirectory = true;
                ofd.Filter = "JPG only (*.jpg|*.jpg";
                MessageBox.Show("Please select image from the folder.", "Select Picture",MessageBoxButtons.OK);

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string selectedImage = ofd.FileName;
                    pcbPicture.ImageLocation = selectedImage;
                    pcbPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                    btnClearPicture.Visible = true;
                }
            }

        }

        private void btnClearPicture_Click(object sender, EventArgs e)
        {
            pcbPicture.Image = null;
            btnClearPicture.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
