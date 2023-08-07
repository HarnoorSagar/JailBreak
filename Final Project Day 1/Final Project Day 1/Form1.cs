/*
 * Harnoor Sagar
 * June 14, 2023
 * Final Project: Game
 * ICS3U PM
 */

using System;
using AudioPlayer;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Media;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project_Day_1
{
    public partial class Form1 : Form
    {
        //******************SETS VARIABLES**********************

        //Declares the image background and the rectangle rectBackground
        Image background;
        private Rectangle rectBackground;
        
        //Declares the image lilBro and the rectangle lilBro
        Image lilBro;
        private Rectangle rectLilBro;

        //Declares the image bigBro and the rectangle bigBro
        Image bigBro;
        private Rectangle rectBigBro;

        //declares the dx variable for lilBro
        int lilBroDx = 0;

        //declares the dx variable for bigBro
        int bigBroDx = 0;

        //declares the money collected variable
        int bagsCollected = 0;

        //creates an array of platform rectangles
        Image platforms;
        private Rectangle[] rectPlatform;

        //Creates an array of acid rectangles
        Image greenAcid;
        private Rectangle[] rectGreenAcid;

        //Creates an array for the moneybags
        Image moneyBags;
        private Rectangle[] rectMoneyBags;

        //declares the groundTop variable
        int groundTop;

        //declares the jumping bools
        bool jumpingLilBro;
        bool jumpingBigBro;

        //declares the falling bools
        bool fallingLilBro;
        bool fallingBigBro;

        //creates the y limit variables
        int yLimitLilBro = 0;
        int yLimitBigBro = 0;

        //declares the upper variables
        int upperLilBro;
        int upperBigBro;

        //declares the gravity variables
        int gravityLilBro;
        int gravityBigBro;

        //Set the onplatform bool for both characters to false
        bool OnPlatformLilBro = false;
        bool OnPlatformBigBro = false;
            
        //Creates constant variables for upper and gravity
        public const int upper = 25;
        public const int gravity = 4;

        //Creates a constant variable for the speed
        public const int speed = 7;

        //creates the constant variable for platform height
        public const int platformHeight = 30;

        //creates constant variable for the width and height of lilBro
        public const int lilBroHeight = 65;
        public const int lilBroWidth = 60;

        //creates constant variable for the width and height of bigBro
        public const int bigBroHeight = 75;
        public const int bigBroWidth = 70;

        //creates a const variable for the greenAcid height
        public const int greenAcidHeight = 20;

        //creates const variables for the money bag width and height
        public const int moneyBagsWidth = 54;
        public const int moneyBagsHeight = 59;

        //declares the audioplayer variable
        AudioFilePlayer backgroundMusic;

        //initializes the form
        public Form1()
        {
            InitializeComponent();
        }

        //Loads the form
        private void Form1_Load(object sender, EventArgs e)
        {
            //sets the form width and heights
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            //Stes the form location to the top left of the screen
            this.Location = new Point(0, 0);

            //Increase the buffer space
            this.DoubleBuffered = true;

            //locks the form size
            this.MinimumSize = this.MaximumSize = new Size(this.Width, this.Height);

            //Sets the length of the rect platform array
            rectPlatform = new Rectangle[14];

            //Sets the length of the rect green acid array
            rectGreenAcid = new Rectangle[5];

            //Sets the length of the rect money bags array
            rectMoneyBags = new Rectangle[10];

            //******************GETS IMAGES**********************

            //Gets the background image
            background = Image.FromFile(Application.StartupPath + @"\background3.jpg", true);
            
            //Gets the image for the lilBro
            lilBro = Image.FromFile(Application.StartupPath + @"\lilBro.png", true);

            //Gets the image for the bigbro
            bigBro = Image.FromFile(Application.StartupPath + @"\bigBro.png", true);

            //gets the image for the platforms
            platforms = Image.FromFile(Application.StartupPath + @"\platform1_image.png", true);

            //Gets the image for the green acid
            greenAcid = Image.FromFile(Application.StartupPath + @"\greenLiquid.png", true);

            //Gets the image for the moneybags
            moneyBags = Image.FromFile(Application.StartupPath + @"\moneyBag.png");

            //creates the audioplayer for background music
            backgroundMusic = new AudioFilePlayer();

            //gets the music
            backgroundMusic.setAudioFile(Application.StartupPath + @"\backgroundmusic.mp3");

            //plays and loops the music
            backgroundMusic.playLooping();

            //******************CREATES THE RECTANGLES**********************

            //Creates the rectangle for the background
            rectBackground = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);

            //creates a new rectangle with the following location and size
            //rectLilBro = new Rectangle(0, 144 - lilBroWidth, lilBroWidth, lilBroHeight);
            rectLilBro = new Rectangle((this.ClientSize.Width/2 - bigBroWidth) - lilBroWidth, this.ClientSize.Height - lilBroHeight, lilBroWidth, lilBroHeight);

            //creates a new rectangle for bigBro with the following location and size
            rectBigBro = new Rectangle(this.ClientSize.Width/2 - bigBroWidth, this.ClientSize.Height - bigBroHeight, bigBroWidth, bigBroHeight);

            //Creates the rectangles for the platforms
            rectPlatform[0] = new Rectangle(0, 144, 575, platformHeight);
            rectPlatform[1] = new Rectangle(777, 144, 381, platformHeight);
            rectPlatform[2] = new Rectangle(1328, 144, this.Width - 1328, platformHeight);
            rectPlatform[3] = new Rectangle(1458, 309, 334, platformHeight);
            rectPlatform[4] = new Rectangle(578, 309, 382, platformHeight);
            rectPlatform[5] = new Rectangle(0, 422, 523, platformHeight);
            rectPlatform[6] = new Rectangle(1155, 422, this.Width - 1155, platformHeight);
            rectPlatform[7] = new Rectangle(0, 579, 1709, platformHeight);
            rectPlatform[8] = new Rectangle(996, 712, this.Width - 996, platformHeight);
            rectPlatform[9] = new Rectangle(143, 847, 1389, platformHeight);

            //Creates the rectangles for the green acid
            rectGreenAcid[0] = new Rectangle(150, 139, 130, greenAcidHeight);
            rectGreenAcid[1] = new Rectangle(1580, 417, 110, greenAcidHeight);
            rectGreenAcid[2] = new Rectangle(720, 304, 80, greenAcidHeight);
            rectGreenAcid[3] = new Rectangle(809, 574, 110, greenAcidHeight);
            rectGreenAcid[4] = new Rectangle(350, 842, 130, greenAcidHeight);

            //Creates the rectangles for the money bags
            rectMoneyBags[0] = new Rectangle(50, 75, moneyBagsWidth, moneyBagsHeight);
            rectMoneyBags[1] = new Rectangle(1780, 75, moneyBagsWidth, moneyBagsHeight);
            rectMoneyBags[2] = new Rectangle(840, 245, moneyBagsWidth, moneyBagsHeight);
            rectMoneyBags[3] = new Rectangle(1540, 245, moneyBagsWidth, moneyBagsHeight);
            rectMoneyBags[4] = new Rectangle(180, 358, moneyBagsWidth, moneyBagsHeight);
            rectMoneyBags[5] = new Rectangle(1740, 358, moneyBagsWidth, moneyBagsHeight);
            rectMoneyBags[6] = new Rectangle(340, 515, moneyBagsWidth, moneyBagsHeight);
            rectMoneyBags[7] = new Rectangle(1490, 647, moneyBagsWidth, moneyBagsHeight);
            rectMoneyBags[8] = new Rectangle(243, 782, moneyBagsWidth, moneyBagsHeight);
            rectMoneyBags[9] = new Rectangle(1259, 782, moneyBagsWidth, moneyBagsHeight);
            

            //******************EVENTS AND TIMER**********************

            //Increase the buffer space
            this.DoubleBuffered = true;

            //Creates the key down event
            this.KeyDown += Form1_KeyDown;

            //Creates the Key up event
            this.KeyUp += Form1_KeyUp;

            //Creates the paint event
            this.Paint += Form1_Paint;

            //sets the ground top to the height of the form
            groundTop = this.ClientSize.Height;

            //sets the ylimit of both characters to ground top
            yLimitLilBro = groundTop;
            yLimitBigBro = groundTop;

            //makes falling false for both players
            fallingLilBro = false;
            fallingBigBro = false;

            //makes jumping false for both players
            jumpingLilBro = false;
            jumpingBigBro = false;

            //Initiates the timer tick and ticks it every 10 miliseconds
            timerTick.Start();
            timerTick.Interval = 1000 / 60;
            
        }

        //runs the key up event
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //if the key released is right or left, it changes stops moving the object vertically
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.A)
            {
                lilBroDx = 0;

            }
            //if the key released is right or left, it changes stops moving the object vertically
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
            {
                bigBroDx = 0;
            }
        }

        //runs the key down event
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //****************LIL BRO MOVEMENT********************

            //if the key pressed is A, it moves the lilBro player left by 5 pixels
            if (e.KeyCode == Keys.A)
            {
                lilBroDx = -speed;

                //Gets the image for the variable lilBro
                lilBro = Image.FromFile(Application.StartupPath + @"\lilBro.png", true);
            }
            //if the key pressed is D, it moves the lilBro player right by 5 pixels
            if (e.KeyCode == Keys.D)
            {
                lilBroDx = speed;

                //Gets the image for the variable lilBro
                lilBro = Image.FromFile(Application.StartupPath + @"\lilBro2.png", true);
            }
            //if the key pressed is W
            if (e.KeyCode == Keys.W)
            {
                //if lilBro is not jumping
                if (!jumpingLilBro && !fallingLilBro)
                {
                    //makes jumping for LilBro true
                    jumpingLilBro = true;

                    //sets the upper and gravity values of lilBro to upper and gravity consts
                    upperLilBro = upper;
                    gravityLilBro = gravity;
                }
            }

            //****************BIG BRO MOVEMENT********************

            //if the key pressed is left, it moves the bigBro player left by 5 pixels
            if (e.KeyCode == Keys.Left)
            {
                bigBroDx = -speed;

                //Gets the image for the variable bigbro
                bigBro = Image.FromFile(Application.StartupPath + @"\bigBro2.png", true);
            }
            //if the key pressed is right, it moves the bigBro player right by 5 pixels
            if (e.KeyCode == Keys.Right)
            {
                bigBroDx = speed;

                //Gets the image for the variable bigbro
                bigBro = Image.FromFile(Application.StartupPath + @"\bigBro.png", true);
            }
            //if the key pressed is up
            if (e.KeyCode == Keys.Up)
            {
                //if bigBro is not jumping
                if (!jumpingBigBro && !fallingBigBro)
                {
                    //makes jumping for bigBro true
                    jumpingBigBro = true;

                    //sets the upper and gravity values for bigBro to upper and gravity const values
                    upperBigBro = upper;
                    gravityBigBro = gravity;
                }
            }
        }

        //runs the paint event
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //inserts the image in the background rectangle
            e.Graphics.DrawImage(background, rectBackground);

            //inserts the images for the platforms
            for (int i = 0; i < rectPlatform.Length; i++)
            {
                e.Graphics.DrawImage(platforms, rectPlatform[i]);
            }

            //inserts the images for the green acid
            for(int i = 0; i < rectGreenAcid.Length; i++)
            {
                e.Graphics.DrawImage(greenAcid, rectGreenAcid[i]);
            }

            //inserts the images for the money bags
            for (int i = 0; i < rectMoneyBags.Length; i++)
            {
                e.Graphics.DrawImage(moneyBags, rectMoneyBags[i]);
            }

            //inserts the image in the enemy rectangle
            e.Graphics.DrawImage(bigBro, rectBigBro);

            //inserts the image in the rectangle
            e.Graphics.DrawImage(lilBro, rectLilBro);
        }


        //runs the timer tick event
        private void timerTick_Tick(object sender, EventArgs e)
        {
            //Deems the form invalid and refreshes the screen
            this.Invalidate();

            //Runs the AcidIntersect method
            AcidIntersect();

            //Runs the MoneyCollected method
            MoneyCollected();

            //*********************CHARACTER PLATFORM HIT TESTS***********************************

            //**LILBRO PLATFORM HIT TESTS**
            //runs the loop if lilBro is touching the platforms
            for (int i = 0; i < rectPlatform.Length; i++)
            {
                //if lilBro intersects with a platform
                if (rectLilBro.IntersectsWith(rectPlatform[i]))
                {
                    
                    //if lilBro's top is touching the bottom of a platform or past it by the speed pixels
                    if (rectLilBro.Top <= rectPlatform[i].Bottom && rectLilBro.Top >= rectPlatform[i].Bottom - upper)
                    {
                        //Sets upper of lilbro to 0
                        upperLilBro = 0;

                        //Teleports lilbro to the bottom of the platforms
                        rectLilBro.Y = rectPlatform[i].Bottom + 1;

                        //breaks out of the loop
                        break;
                    }
                    //if lilBro's bottom is touching the top of a platform or past it by the speed pixels
                    else if (rectLilBro.Bottom >= rectPlatform[i].Top + speed)
                    {
                        //Makes on platform for lilbro true
                        OnPlatformLilBro = true;

                        //Teleports lilbro to the top of the platforms
                        rectLilBro.Y = rectPlatform[i].Top - rectLilBro.Height - 1;

                        //breaks out of the loop
                        break;
                    }
                    //else, runs if the right of lilbro is touching or past the left side of a platform and lilBrodx is positive
                    else if (rectLilBro.Right >= rectPlatform[i].Left && rectLilBro.Right <= rectPlatform[i].Left + speed && lilBroDx > 0)
                    {
                        //makes lilbroDx  0
                        lilBroDx = 0;

                        //breaks out of the loop
                        break;
                    }
                    //else, runs if the left of lilbro is touching or past the right side of a platform and lilBrodx is negative
                    else if (rectLilBro.Left <= rectPlatform[i].Right && rectLilBro.Left >= rectPlatform[i].Right - speed && lilBroDx < 0)
                    {
                        //makes lilbroDx 0
                        lilBroDx = 0;

                        //breaks out of the loop
                        break;
                    }

                }
                //otheriwse...
                else
                {
                    //makes onplatformLilBro to false
                    OnPlatformLilBro = false;
                }

                //**BIGBRO PLATFORM HIT TESTS**

                //if bigBro intersects with a platform
                if (rectBigBro.IntersectsWith(rectPlatform[i]))
                {

                    //if bigBro's top is touching the bottom of a platform or past it by the speed pixels
                    if (rectBigBro.Top <= rectPlatform[i].Bottom && rectBigBro.Top >= rectPlatform[i].Bottom - upper)
                    {
                        //Sets upper of bigBro to 0
                        upperBigBro = 0;

                        //Teleports bigBro to the bottom of the platforms
                        rectBigBro.Y = rectPlatform[i].Bottom + 1;

                        //breaks out of the loop
                        break;
                    }
                    //if bigBro's bottom is touching the top of a platform or past it by the speed pixels
                    else if (rectBigBro.Bottom >= rectPlatform[i].Top + speed)
                    {
                        //Makes on platform for lilbro true
                        OnPlatformBigBro = true;

                        //Teleports lilbro to the top of the platforms
                        rectBigBro.Y = rectPlatform[i].Top - rectBigBro.Height - 1;

                        //breaks out of the loop
                        break;
                    }
                    //else, runs if the right of bigBro is touching or past the left side of a platform and bigBrodx is positive
                    else if (rectBigBro.Right >= rectPlatform[i].Left && rectBigBro.Right <= rectPlatform[i].Left + speed && bigBroDx > 0)
                    {
                        //makes bigBroDx  0
                        bigBroDx = 0;

                        //breaks out of the loop
                        break;
                    }
                    //else, runs if the left of bigBro is touching or past the right side of a platform and bigBroDx is negative
                    else if (rectBigBro.Left <= rectPlatform[i].Right && rectBigBro.Left >= rectPlatform[i].Right - speed && bigBroDx < 0)
                    {
                        //makes lilbroDx 0
                        bigBroDx = 0;

                        //breaks out of the loop
                        break;
                    }

                }
                //otheriwse...
                else
                {
                    //makes onPlatformBigBro to false
                    OnPlatformBigBro = false;
                }
            }

            //**LILBRO YLIMIT STUFF**
            //runs a for loop to check through the platforms array
            for (int i = 0; i < rectPlatform.Length; i++)
            {
                //runs if lilBro is touching the left, right or top of the platform
                if (rectLilBro.Right >= rectPlatform[i].Left && rectLilBro.Left <= rectPlatform[i].Right && rectLilBro.Bottom <= rectPlatform[i].Top)
                {
                    //changes the y limit to the platform tops
                    yLimitLilBro = rectPlatform[i].Top;

                    //breaks out of the statement
                    break;
                }
                //otherwise....
                else
                {
                    //changes the y limit to the top of the ground
                    yLimitLilBro = groundTop;
                }
            }

            //**BIGBRO YLIMIT STUFF**
            //runs a for loop to check through the platforms array
            for (int i = 0; i < rectPlatform.Length; i++)
            {
                //runs if bigBro is touching the left, right or top of the platform
                if (rectBigBro.Right >= rectPlatform[i].Left && rectBigBro.Left <= rectPlatform[i].Right && rectBigBro.Bottom <= rectPlatform[i].Top)
                {
                    //changes the y limit to the platform tops
                    yLimitBigBro = rectPlatform[i].Top;

                    //breaks out of the statement
                    break;
                }
                //otherwise....
                else
                {
                    //changes the y limit to the top of the ground
                    yLimitBigBro = groundTop;
                }
            }

            //************************JUMPING AND FALLING (LILBRO)******************************

            //runs if upper lil bro is greater than 0
            if (upperLilBro > 0)
            {
                //makes jumping true
                jumpingLilBro = true;

            }

            //if the bottom of lil bro is less than the y limit
            if (rectLilBro.Bottom < yLimitLilBro)
            {
                //makes falling lil bro true
                fallingLilBro = true;
            }
            
            //If lilBro is falling
            if (fallingLilBro)
            {
                //moves lilbro by the gravity value
                rectLilBro.Y += gravityLilBro;
            }

            //if lilBro is jumping
            if (jumpingLilBro)
            {
                //if the bottom of lilBro rect is less than or equal to the ground top
                if (rectLilBro.Bottom <= yLimitLilBro)
                {
                    //makes jumping true
                    jumpingLilBro = true;

                    //subtracts lilBro's y value by the upper
                    rectLilBro.Y -= upperLilBro;
                   
                    //subtracts the upperlilbro value by 1
                    upperLilBro--;

                    //if the bottom of lilBro rect is greater than or equal to the ground top
                    if (rectLilBro.Bottom >= groundTop || OnPlatformLilBro)
                    {
                        //makes jumping false
                        jumpingLilBro = false;

                        //if lilBro is not on the platform
                        if (!OnPlatformLilBro)
                        {
                            //changes the y value of lilBro to set it exactly on top of the ground
                            rectLilBro.Y = groundTop - rectLilBro.Height;
                        }

                        //sets the value of upper and gravity of lilbro to 0
                        upperLilBro = 0;
                        gravityLilBro = 0;
                    }

                }
            }

            //if lilbro is falling
            if (fallingLilBro)
            {
                //Adds to lilbro's y value by the gravity
                rectLilBro.Y += gravityLilBro;
            }

            //If the bottom of lilBro is greater than or equal to the y limit
            if (rectLilBro.Bottom >= yLimitLilBro)
            {
                //makes jumping lilbro false
                jumpingLilBro = false;

                //makes falling lilBro false
                fallingLilBro = false;

                //teleports lilbro rect to top of platform
                rectLilBro.Y = yLimitLilBro - lilBroHeight;
            }

            //************************JUMPING AND FALLING (BIGBRO)******************************

            //runs if upper Big bro is greater than 0
            if (upperBigBro > 0)
            {
                //makes jumping true
                jumpingBigBro = true;

            }

            //if the bottom of Big bro is less than the y limit
            if (rectBigBro.Bottom < yLimitBigBro)
            {
                //makes falling Big bro true
                fallingBigBro = true;
            }

            //If BigBro is falling
            if (fallingBigBro)
            {
                //moves Bigbro by the gravity value
                rectBigBro.Y += gravityBigBro;
            }

            //if BigBro is jumping
            if (jumpingBigBro)
            {
                //if the bottom of BigBro rect is less than or equal to the ground top
                if (rectBigBro.Bottom <= yLimitBigBro)
                {
                    //makes jumping true
                    jumpingBigBro = true;

                    //subtracts BigBro's y value by the upper
                    rectBigBro.Y -= upperBigBro;

                    //subtracts the upperBigbro value by 1
                    upperBigBro--;

                    //if the bottom of BigBro rect is greater than or equal to the ground top
                    if (rectBigBro.Bottom >= groundTop || OnPlatformBigBro)
                    {
                        //makes jumping false
                        jumpingBigBro = false;

                        //if BigBro is not on the platform
                        if (!OnPlatformBigBro)
                        {
                            //changes the y value of BigBro to set it exactly on top of the ground
                            rectBigBro.Y = groundTop - rectBigBro.Height;
                        }

                        //sets the value of upper and gravity of Bigbro to 0
                        upperBigBro = 0;
                        gravityBigBro = 0;
                    }

                }
            }

            //if Bigbro is falling
            if (fallingBigBro)
            {
                //Adds to Bigbro's y value by the gravity
                rectBigBro.Y += gravityBigBro;
            }

            //If the bottom of BigBro is greater than or equal to the y limit
            if (rectBigBro.Bottom >= yLimitBigBro)
            {
                //makes jumping BigBro false
                jumpingBigBro = false;

                //makes falling BigBro false
                fallingBigBro = false;

                //teleports BigBro rect to top of platform
                rectBigBro.Y = yLimitBigBro - bigBroHeight;
            }

            //***********************************LIL BRO BORDER LIMIT************************************

            //runs if the lilBro player is touching left border, and the dx is negative, then dx changes to 0
            if (rectLilBro.Left <= 0 && lilBroDx < 0)
            {
                lilBroDx = 0;
            }
            //runs if the lilBro player is touching right border, and the dx is positive, then dx changes to 0
            if (rectLilBro.Right >= this.ClientSize.Width && lilBroDx > 0)
            {
                lilBroDx = 0;
            }

            //**************************************BIG BRO BORDER LIMIT********************************

            //runs if the lilBro player is touching left border, and the dx is negative, then dx changes to 0
            if (rectBigBro.Left <= 0 && bigBroDx < 0)
            {
                bigBroDx = 0;
            }
            //runs if the lilBro player is touching right border, and the dx is positive, then dx changes to 0
            if (rectBigBro.Right >= this.ClientSize.Width && bigBroDx > 0)
            {
                bigBroDx = 0;
            }

            //**********************MOVEMENT***********************

            //Moves the lilBro player by the dx 
            rectLilBro.X += lilBroDx;

            //Moves the bigBro player by the dx value
            rectBigBro.X += bigBroDx;
        }

        //creates the method acid intersect
        private void AcidIntersect()
        {
            //runs a for loop to the length of th egreen acid array
            for (int i = 0; i < rectGreenAcid.Length; i++)
            {
                //if lilbro player intersects with the green acid
                if (rectLilBro.IntersectsWith(rectGreenAcid[i]))
                {
                    //teleports lilbro back to starting position
                    rectLilBro.X = (this.ClientSize.Width / 2 - bigBroWidth) - lilBroWidth;
                    rectLilBro.Y = this.ClientSize.Height - lilBroHeight;
                }
                //if bigBro player intersects with the green acid
                if (rectBigBro.IntersectsWith(rectGreenAcid[i]))
                {
                    //teleports bigbro back to starting position
                    rectBigBro.X = this.ClientSize.Width / 2 - bigBroWidth;
                    rectBigBro.Y = this.ClientSize.Height - bigBroHeight;
                }
            }
        }

        //creates the moneycollected method
        private void MoneyCollected()
        {
            //runs a for loop to the length of the moneybags array
            for (int i = 0; i < rectMoneyBags.Length; i++)
            {
                //if lilbro or bigbro intersects with a money bag
                if (rectLilBro.IntersectsWith(rectMoneyBags[i]) || rectBigBro.IntersectsWith(rectMoneyBags[i]))
                {
                    //adds to the bagscollected variable
                    bagsCollected++;

                    //removes the money bag from the screen
                    rectMoneyBags[i].X = this.ClientSize.Width + moneyBagsWidth * 2;
                }
            }
        }
    }
}
