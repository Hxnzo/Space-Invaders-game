using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;
using AudioPlayer;

//Hanzalah Patel
//ICS 3U
//Final Project
//December 12 2018

namespace PatelFinalProject
{
    public partial class Form1 : Form
    {
        //Declares ractangle variable
        Rectangle mBackground;
        //Declares ractangle variable
        Rectangle title;

        //craetes timer
        Timer refresh = new Timer();

        //creates button
        Button start = new Button();
        //creates button
        Button instructions = new Button();

        //creates private font collection
        PrivateFontCollection myFont;
        //names the font variable
        Font coolfont;

        //sets image saved into the debug folder
        Image menu = Image.FromFile(Application.StartupPath + @"\menu.jpg");

        //declares variable
        AudioFilePlayer backMusic;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //makes the image smoother
            this.DoubleBuffered = true;

            //sets form location
            this.Location = new Point(0, 0);
            //sets the maximum size o the form
            this.MaximumSize = this.MinimumSize;
            //sets the minimum size o the form
            this.MinimumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            //makes user unable to minimize
            this.MinimizeBox = false;
            //makes user unable to maximize
            this.MaximizeBox = false;

            //creates the variable
            myFont = new PrivateFontCollection();
            //gets the font from the debug folder
            myFont.AddFontFile(Application.StartupPath + @"\coolfont.ttf");
            //creates font size
            coolfont = new Font(myFont.Families[0], 75);

            //gives the coordinates and size to rectangle
            mBackground = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            //gives the coordinates and size to rectangle
            title = new Rectangle(this.ClientSize.Width / 2 - 287, 0, 575, 100);

            //writes start inside the button
            start.Text = "Start";
            //write instructions inside the button
            instructions.Text = "Instructions";

            //creates method for when the start button is clicked
            start.Click += Start_Click;
            //creates method for when the start button is clicked
            instructions.Click += Instructions_Click;

            //sets location for the start button
            start.Location = new Point(this.ClientSize.Width / 2 - start.Width / 2, this.ClientSize.Height / 2 - start.Height * 3);
            //sets location for the instructions button
            instructions.Location = new Point(this.ClientSize.Width / 2 - instructions.Width / 2, this.ClientSize.Height / 2 - instructions.Height / 2);

            //sets buttons width
            start.Width = 75;
            //sets buttons height
            start.Height = 50;
            //changes button color
            start.BackColor = Color.LightGreen;

            //sets buttons width
            instructions.Width = 75;
            //sets buttons height
            instructions.Height = 50;
            //changes button color
            instructions.BackColor = Color.LightGreen;

            //creates method for paint
            this.Paint += Form1_Paint;

            //adds button onto the form
            this.Controls.Add(start);
            //adds button onto the form
            this.Controls.Add(instructions);

            //creates the interval for the refresh timer
            refresh.Tick += Refresh_Tick;
            //sets the interval fro the timer
            refresh.Interval = 1000 / 60;
            //starts the timer
            refresh.Start();

            //variable sets as audioplayer
            backMusic = new AudioFilePlayer();
            //gets sound from file
            backMusic.setAudioFile(Application.StartupPath + @"\backMusic.mp3");
            //plays sound
            backMusic.playLooping();
        }

        private void Loadform(Form frm)
        {
            //closes form
            frm.FormClosed += Frm_FormClosed;
            //hides form
            this.Hide();
            //shows form
            frm.Show();
        }

        private void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //reshows the original form
            this.Show();
        }

        private void Instructions_Click(object sender, EventArgs e)
        {
            //creates method
            Loadform(new instructions());
        }

        private void Refresh_Tick(object sender, EventArgs e)
        {
            //refreshes the screen
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //makes the rectangle into the image saved into the menu variable
            e.Graphics.DrawImage(menu, mBackground);

            //allows to write on form
            e.Graphics.DrawString("Space Invaders", coolfont, Brushes.LawnGreen, title);

        }

        private void Start_Click(object sender, EventArgs e)
        {
            //creates method 
            gameForm(new game());

            //reads the file that is in the debug folder

        }

        private void gameForm(Form frm)
        {
            //closes form
            frm.FormClosed += Frm_FormClosed1;
            //hides form
            this.Hide();
            //shows form
            frm.Show();
        }

        private void Frm_FormClosed1(object sender, FormClosedEventArgs e)
        {
            //reshows the original form
            this.Show();
        }
    }
}
