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

namespace PatelFinalProject
{
    public partial class game : Form
    {
        //Declares ractangle variable
        Rectangle gBackground;
        //Declares ractangle variable
        Rectangle playerRect;

        //declares list rectangle
        List<Rectangle> bulletRect;
        //declares list rectangle
        List<Rectangle> enemyRectList;

        //sets image saved into the debug folder
        Image galaxy = Image.FromFile(Application.StartupPath + @"\galaxy.jpg");
        //sets image from the debug folder
        Image ship = Image.FromFile(Application.StartupPath + @"\whiteship.png");
        //sets image from the debug folder
        Image enemy = Image.FromFile(Application.StartupPath + @"\alien.png");

        //craetes timer
        Timer refresh = new Timer();

        //declares variable
        AudioFilePlayer shootSound;

        //creates private font collection
        PrivateFontCollection myFont;
        //names the font variable
        Font coolfont;

        //declares the variable x
        int x = 0;
        //declares the variable y
        int y = 0;
        //declares the variable dy
        int dy = 0;
        //declares the variable ey
        int ey = 1;
        //declares the variable eHealth
        List<int> eHealth;
        //declares the variable score
        int score = 0;
        //declares the variable highscore
        int highscore = 0;
        //declares the variable enemyHealth
        int enemyHealth = 1;

        public game()
        {
            InitializeComponent();
        }

        private void game_Load(object sender, EventArgs e)
        {
            //makes the image smoother
            this.DoubleBuffered = true;

            //sets form location
            this.Location = new Point(0, 0);
            //sets the maximum size o the form
            this.MaximumSize = this.MinimumSize;
            //sets the minimum size o the form
            this.MinimumSize = new Size(1000, 800);
            //makes user unable to minimize
            this.MinimizeBox = false;
            //makes user unable to maximize
            this.MaximizeBox = false;

            //-------------------------------------------------------------------------

            //gives the coordinates and size to rectangle
            gBackground = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);

            //gives the rectamgle coordinates and size
            playerRect = new Rectangle(this.ClientSize.Width / 2 - playerRect.Width / 2, this.ClientSize.Height / 2 - playerRect.Height / 2, 100, 100);

            //----------------------------------------------------------------------

            //creates the variable
            myFont = new PrivateFontCollection();
            //gets the font from the debug folder
            myFont.AddFontFile(Application.StartupPath + @"\coolfont.ttf");
            //creates font size
            coolfont = new Font(myFont.Families[0], 20);

            //-------------------------------------------------------------

            //constructs the List 
            bulletRect = new List<Rectangle>();
            //constructs the List 
            enemyRectList = new List<Rectangle>();
            //constructs the List
            eHealth = new List<int>();

            //-----------------------------------------------------------------

            //creates method for paint
            this.Paint += Game_Paint;

            //creates method for if the key is pressed
            this.KeyDown += Game_KeyDown;
            //creates method for if the key is released
            this.KeyUp += Game_KeyUp;

            //creates the interval for the refresh timer
            refresh.Tick += Refresh_Tick;
            //sets the interval fro the timer
            refresh.Interval = 5;
            //starts the timer
            refresh.Start();

            //----------------------------------------------------------------------

            //checks to see if the file exists
            if (File.Exists(Application.StartupPath + @"\highscore.txt"))
            {
                //reads what is written in the file
                StreamReader hScore = new StreamReader(Application.StartupPath + @"\highscore.txt");

                //converts the number from the file into an integer and uses that as variable for highscore
                highscore = Convert.ToInt32(hScore.ReadLine());

                //closes the file
                hScore.Close();
            }

            //variable sets as audioplaye
            shootSound = new AudioFilePlayer();
            //gets sound from file
            shootSound.setAudioFile(Application.StartupPath + @"\shoot.mp3");
        }

        //----------------------------------------------------------------------


        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            //if up arrow or down arrow key is pressed it will run
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                //resets location
                y = 0;
            }

            //if right arrow or left arrow key is pressed it will run
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
            {
                //resets location
                x = 0;
            }

            //if right arrow or left arrow key is pressed it will run
            if (e.KeyCode == Keys.Space)
            {
                //resets location
                dy = -5;
            }
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                //moves the rectangle up 5 pixels
                dy = -5;

                //adds a rectangle from the list
                bulletRect.Add(new Rectangle(playerRect.Left + playerRect.Width / 2, playerRect.Top, 5, 5));

                //plays sound
                shootSound.play();
            }

            //if the up arrow is pressed then this will run
            if (e.KeyCode == Keys.Up)
            {
                //moves the rectangle up 7 pixels
                y = -7;
            }

            //if the down arrow is pressed then this will run
            if (e.KeyCode == Keys.Down)
            {
                //moves the rectangle down 7 pixels
                y = 7;
            }

            //if the right arrow is pressed then this will run
            if (e.KeyCode == Keys.Right)
            {
                //moves the rectangle right 7 pixels
                x = 7;
            }

            //if the left arrow is pressed then this will run
            if (e.KeyCode == Keys.Left)
            {
                //moves the rectangle left 7 pixels
                x = -7;
            }

            //bottom code moves bullet---------------------------------------------

            //if the up arrow is pressed then this will run

        }

        private void Refresh_Tick(object sender, EventArgs e)
        {
            //refreshes the screen
            this.Invalidate();

            //if the rectangle is going up then it will run
            if (y < 0 && playerRect.Top > 0)
            {
                //changes the location
                playerRect.Y += y;
            }
            //if the rectangle is going up then it will run
            if (y > 0 && playerRect.Bottom < this.ClientSize.Height)
            {
                //changes the location
                playerRect.Y += y;
            }

            //if the rectangle is going sideways then it will run
            if (x < 0 && playerRect.Left > 0)
            {
                //changes the location
                playerRect.X += x;
            }
            //if the rectangle is going sideways then it will run
            if (x > 0 && playerRect.Right < this.ClientSize.Width)
            {
                //changes the location
                playerRect.X += x;
            }

            //differentiates between the player and bullet----------------------------

            //for loop runs to check the list
            for (int i = 0; i < bulletRect.Count; i++)
            {
                //changes the location
                bulletRect[i] = new Rectangle(bulletRect[i].X, bulletRect[i].Y + dy, bulletRect[i].Width, bulletRect[i].Height);

                //removes bullet when it reaches the top of the screen
                if (bulletRect[i].Top <= this.Top)
                {
                    //removes from the list
                    bulletRect.RemoveAt(i);
                    --i;
                }
            }

            //-----------------------------------------------------------------------------

            //for loop runs to check the list
            for (int i = 0; i < enemyRectList.Count; i++)
            {
                //changes the location
                enemyRectList[i] = new Rectangle(enemyRectList[i].X, enemyRectList[i].Y + ey, enemyRectList[i].Width, enemyRectList[i].Height);
            }

            //runs to check if any of the enemies hit the player
            for (int i = 0; i < enemyRectList.Count; ++i)
            {
                if (enemyRectList[i].IntersectsWith(playerRect))
                {
                    //stops timer
                    refresh.Stop();

                    //shows game over
                    MessageBox.Show("Game Over");

                    //closes the form
                    Application.Exit();

                    //runs method
                    hiscore();

                    //breaks the loop
                    break;
                }
            }

            //for loop runs to check the list for if the enemy hits the bullet
            for (int i = 0; i < enemyRectList.Count; i++)
            {
                //runs to check to see if it hits the enemy
                for (int j = 0; j < bulletRect.Count; j++)
                {


                    //if bullet hits enemy this will run
                    if (bulletRect[j].IntersectsWith(enemyRectList[i]))
                    {
                        //removes from the list
                        bulletRect.RemoveAt(j);

                        //minuses 1 each time it runs
                        eHealth[i]--;

                        //
                        if (eHealth[i] <= 0)
                        {
                            //removes from the list
                            enemyRectList.RemoveAt(i);

                            //
                            eHealth.RemoveAt(i);

                            //increases the score by 10 everytime an enemy is killed
                            score += 10;

                            //takes out enemy if it is hit
                            --i;
                            //breaks the loop
                            break;
                        }


                    }

                }
            }

            //if enemies are gone then this will run
            if (enemyRectList.Count == 0)
            {
                //runs method
                spawnEnemies();
            }


            //
            for (int i = 0; i < enemyRectList.Count; i++)
            {
                if (enemyRectList[i].Bottom >= this.ClientSize.Height)
                {
                    //stops timer
                    refresh.Stop();

                    //shows game over
                    MessageBox.Show("Game Over!");

                    //closes the form
                    Application.Exit();

                    //runs method
                    hiscore();

                    //breaks the loop
                    break;
                }
            }



        }

        private void hiscore()
        {
            //stops timer
            refresh.Stop();

            //rewrites the file that is in the debug folder
            StreamWriter hSwriter = new StreamWriter(Application.StartupPath + @"\highscore.txt", false);

            //if the score is higher than the highscore than this will run
            if (score > highscore)
            {
                //rewrites the highscore into the score
                hSwriter.WriteLine(score);
            }
            //else
            else
            {
                //sets the highscore to what it originally was
                hSwriter.WriteLine(highscore);
            }

            //closes the file
            hSwriter.Close();
        }


        private void spawnEnemies()
        {
            //fro loop runs to spawn 30 enemies
            for (int i = 0; i < 30; i++)
            {

                //if i equals to 0 than this will run
                if (i == 0)
                {
                    //adds an enemy to the list
                    enemyRectList.Add(new Rectangle(40, 40, 40, 40));

                    //sets enemyhealth to the variable
                    eHealth.Add(enemyHealth);

                    //adds another anemy
                    i++;
                }

                //if i is modulus divided by 15 and equals to 0 than this will run
                if (i % 15 == 0)
                {
                    //adds more enemies to the list but in another row
                    enemyRectList.Add(new Rectangle(enemyRectList[i - 15].X, enemyRectList[i - 15].Bottom + 20, 40, 40));

                    //sets enemyhealth to the variable
                    eHealth.Add(enemyHealth);
                }

                //else
                else
                {
                    //adds enemies to the list but beside other enemies
                    enemyRectList.Add(new Rectangle(enemyRectList[enemyRectList.Count - 1].Right + 20, enemyRectList[enemyRectList.Count - 1].Y, 40, 40));

                    //sets enemyhealth to the variable
                    eHealth.Add(enemyHealth);
                }



            }

            //adds 1 to enemyhealth
            enemyHealth++;

        }
        private void Game_Paint(object sender, PaintEventArgs e)
        {
            //makes the rectangle into the image saved into the gBackground variable
            e.Graphics.DrawImage(galaxy, gBackground);

            //makes the rectangle into the image saved into the ship variable
            e.Graphics.DrawImage(ship, playerRect);

            //allows to write on form
            e.Graphics.DrawString("Score: " + score, coolfont, Brushes.White, new Rectangle(0, 0, 150, 150));
            //allows to write on form
            e.Graphics.DrawString("HighScore: " + highscore, coolfont, Brushes.White, new Rectangle(200, 0, 200, 200));

            //runs to check the list
            for (int i = 0; i < bulletRect.Count; ++i)
            {
                //fills the bullet with a red color
                e.Graphics.FillRectangle(Brushes.Red, bulletRect[i]);
            }

            //runs to check the list
            for (int i = 0; i < enemyRectList.Count; ++i)
            {
                //fills the bullet with a red color
                e.Graphics.DrawImage(enemy, enemyRectList[i]);
            }
        }
    }
}
