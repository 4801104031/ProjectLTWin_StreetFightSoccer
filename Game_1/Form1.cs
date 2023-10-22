using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace Game_1
{
    public partial class Form1 : Form
    {
        Image player;
        Image background;
        Image drum;
        Image fireball;

        int playerX =  0;
        int playerY = 300;

        int drumX = 450;
        int drumY = 350;

        int drumMoveTime = 0;
        int actionStrength = 0;
        int endFrame = 0;
        int backgroundPosition = 0;
        int totalFrame = 0;
        int bg_number = 0;

        float num;

        bool goLeft, goRight;
        bool directionPressed;
        bool playingAction;
        bool shotFireBall;

        List<string> background_images = new List<string>();

        public Form1()
        {
            InitializeComponent();
            SetUpForm();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && !directionPressed)
            {
                MovePlayerAnimation("left");
            }
            if(e.KeyCode==Keys.Right && !directionPressed)
            {
                MovePlayerAnimation("right");
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)// thay doi background 
        {
            if(e.KeyCode==Keys.X)
            {
                if (bg_number < background_images.Count - 1)
                {
                    bg_number++;
                }
                else
                {
                    bg_number = 0;
                }
                background = Image.FromFile(background_images[bg_number]);
                backgroundPosition = 0;
                drumMoveTime = 0;
                drumX = 300;
            }
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
            {
                goLeft = false;
                goRight = false;
                directionPressed= false;
                ResetPlayer();
            }
        }

        private void FormPaintEvent(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(background, new Point(backgroundPosition, 0));
            e.Graphics.DrawImage(player, new Point(playerX, playerY));
            e.Graphics.DrawImage(drum,new Point(drumX, drumY));
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            this.Invalidate();
            ImageAnimator.UpdateFrames();
        }
        private void SetUpForm()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint| ControlStyles.OptimizedDoubleBuffer|
                ControlStyles.UserPaint,true);
            background_images = Directory.GetFiles("background", "*.jpg").ToList();
            background = Image.FromFile(background_images[bg_number]);
            player = Image.FromFile("standing.gif");
            drum = Image.FromFile("drum.png");
            SetUpAnimation();
        }
        private void SetUpAnimation()
        {
            ImageAnimator.Animate(player,this.OnFrameChangedHandler);
            FrameDimension dimentions = new FrameDimension(player.FrameDimensionsList[0]);
            totalFrame = player.GetFrameCount(dimentions);
            endFrame = totalFrame - 3;
        }

        private void OnFrameChangedHandler(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void MovePlayerandBackground()
        {
            if (goLeft)
            {
                if (playerX > 0)
                {
                    playerX -= 6;
                }
                if (backgroundPosition < 0 && playerX < 100)
                {
                    backgroundPosition += 5;
                    drumX += 5;
                }
            }
            if (goRight)
            {
                if (playerX + player.Width < this.ClientSize.Width)
                {
                    playerX += 6;
                }
                if (backgroundPosition + background.Width > this.ClientSize.Width + 5 && playerX > 650)
                {
                    backgroundPosition -= 5;
                    drumX -= 5;
                }
            
        }
    }

        private void MovePlayerAnimation(string direction) 
        {
            if(direction == "left")
            {
                goLeft = true;
                player = Image.FromFile("backwards.gif");
            }
            if(direction == "right")
            {
                goRight = true;
                player = Image.FromFile("forwards.gif");
            }
            directionPressed = true;
            playingAction = false;
            SetUpAnimation();
        }

        private void ResetPlayer()
        {
            player = Image.FromFile("standing.gif");
            SetUpAnimation();
            num = 0;
            playingAction = false;
        }

        private void SetPlayerAction(string animation , int strength)
        {

        }

        private void ShootFireball()
        {

        }

        private void CheckPunchHit()
        {

        }

        private void CheckFireballHit()
        {

        }
        
        private bool DetectCollision()
        {
            return true;
        }
    }
}
