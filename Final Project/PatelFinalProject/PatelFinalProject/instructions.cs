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
using AudioPlayer;

namespace PatelFinalProject
{
    public partial class instructions : Form
    {
        //Declares ractangle variable
        Rectangle iBackground;

        //sets image saved into the debug folder
        Image space = Image.FromFile(Application.StartupPath + @"\space.jpg");

        //creates private font collection
        PrivateFontCollection myFont;
        //names the font variable
        Font coolfont;

        //craetes timer
        Timer refresh = new Timer();

        public instructions()
        {
            InitializeComponent();
        }

        private void instructions_Load(object sender, EventArgs e)
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

            //gives the coordinates and size to rectangle
            iBackground = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);

            //creates the variable
            myFont = new PrivateFontCollection();
            //gets the font from the debug folder
            myFont.AddFontFile(Application.StartupPath + @"\coolfont.ttf");
            //creates font size
            coolfont = new Font(myFont.Families[0], 50);

            //creates method for paint
            this.Paint += Instructions_Paint;

            //creates the interval for the refresh timer
            refresh.Tick += Refresh_Tick;
            //sets the interval fro the timer
            refresh.Interval = 1000 / 60;
            //starts the timer
            refresh.Start();
        }

        private void Refresh_Tick(object sender, EventArgs e)
        {
            //refreshes the screen
            this.Invalidate();
        }

        private void Instructions_Paint(object sender, PaintEventArgs e)
        {
            //makes the rectangle into the image saved into the iBackground variable
            e.Graphics.DrawImage(space, iBackground);

            //allows to write on form
            e.Graphics.DrawString("\n   Instructions\n- Use arrow keys to control players movement\n- Press spacebar to shoot enemies\n- If enemies intersect with player then you lose\n- you can move wherever you want\n- each wave of enemies are stronger\n- If enemies reach bottom of screen than you lose\n- Player has to get highscore\n   HAVE FUN!", coolfont, Brushes.White, new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height));
        }

    }
}
