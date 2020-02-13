using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
//Coded By Giorgi Tchkoidze

namespace Viewer
{
    public partial class Form1 : Form
    {
        private int position = -1; 
        private int Endposition; //number of imeges in folder
        private int interval; //interval for slideshow

        private List<string> ImageLocationList = new List<string>(); //srores images addresses
        public Form1()
        {
            InitializeComponent();
        }

        private void clearButton_Click(object sender, EventArgs e) //cleasrs image to default
        {
            // clear box
            pictureBox1.Image = null;
        }

        private void backgoundToolStripMenuItem_Click(object sender, EventArgs e) //opens menus to choose color for background
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                pictureBox1.BackColor = colorDialog1.Color;
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e) //opens image (not folder)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e) // Close app
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("* Coded by Giorgi Tchkoidze");
        }

        private void nextButton_Click(object sender, EventArgs e) //shows next picture
        {
            if (position != Endposition)
            {
                position++;
            }
            if (position < Endposition)
            {
                pictureBox1.ImageLocation = ImageLocationList.ElementAt(position); 
            }
        }

        private void startSlideButton_Click(object sender, EventArgs e) //starts slideshow
        {
            position = 0;
            try
            {
                interval = Convert.ToInt32(textBox1.Text);
                interval *= 1000;
                timer1.Interval = interval; //set timers Interval to interval 
                timer1.Start(); // start timer
            }
            catch (Exception)
            {
                MessageBox.Show("Enter appropriate time interval");
            }
        }

        private void backButton_Click(object sender, EventArgs e) //previous image
        {
            if (position > 0)
            {
                position--;
            }

            if (position>=0)
            {
                pictureBox1.ImageLocation = ImageLocationList.ElementAt(position);
            }
        }

        private void sellectButton_Click(object sender, EventArgs e) // opens menu to scoose folder
        {
            if (!(folderBrowserDialog1.ShowDialog() == DialogResult.OK))
            {
                MessageBox.Show("* Select appropriate folder");
            }
            string folderLocation = folderBrowserDialog1.SelectedPath;
            GetPicture ob1 = new GetPicture(folderLocation);
            locationTextBox.Text = folderBrowserDialog1.SelectedPath;
            foreach(var el in ob1.GetPictureList())
            {
                ImageLocationList.Add(el);
            }
            Endposition = ImageLocationList.Count; // set Endposition to number of images;
            pictureBox1.ImageLocation = ImageLocationList.ElementAt(0);
            nextButton.Enabled = true;
            backButton.Enabled = true;
            startSlideButton.Enabled = true;
            position = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void showButton_Click(object sender, EventArgs e) // takes box2 string and show images 
        {
            pictureBox1.ImageLocation = textBox2.Text;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e) //timer used for slide show
        {
            timer1.Stop();
            pictureBox1.ImageLocation = ImageLocationList.ElementAt(position);
            if (checkBox1.Checked)
            {
                timer2.Start();
                
            }
            position++;
            if (position < Endposition)
            {
                timer1.Start();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            pictureBox1.Dock = DockStyle.Left;
            timer3.Start();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Stop();
            pictureBox1.Dock = DockStyle.Right;
            timer4.Start();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4.Stop();
            pictureBox1.Dock = DockStyle.Fill;

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }


    class GetPicture
    {
        List<string> pictureList = new List<string>(); //stores pictures
        private string Location { get; set; }

        public GetPicture(string location)
        {
            Location = location;

            DirectoryInfo images = new DirectoryInfo(Location);
            FileInfo[] Files = images.GetFiles();

            foreach (FileInfo file in Files)
            {
                pictureList.Add(file.FullName);
            }
        }
        public List<string> GetPictureList() => pictureList; //return picture address list;
    }

}
