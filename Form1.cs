using System;
using System.Drawing;
using System.Windows.Forms;

namespace snow
{
    public partial class Form1 : Form
    {
        private const int CountSnowFlakes = 150;
        private const int TimerInterval = 50;

        private SnowFlake[] snowflakes;
        private Bitmap snowflakeImage;
        private System.Windows.Forms.Timer timer;

        private static  Random rand = new Random();

        private struct SnowFlake
        {
            public int X;
            public int Y;
            public int Speed;
            public float Scale;
        }

        public Form1()
        {
            InitializeComponent();

            snowflakeImage = Properties.Resources.snowflakeImg;
            snowflakes = new SnowFlake[CountSnowFlakes];

            //таймер
            timer = new System.Windows.Forms.Timer();
            timer.Interval = TimerInterval;
            timer.Tick += Timer_Tick;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            InitializeSnowflakes();
            timer.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            {
                this.Close();
            }
        }

        private void InitializeSnowflakes()
        {
            for (int i = 0; i < snowflakes.Length; i++)
            {
                ResetSnowFlake(i);
            }
        }

        private void ResetSnowFlake(int index)
        {
            //размер снежинки
            int randomSize = rand.Next(15, 51);

            int speed;
            if (randomSize <= 30)
            {
                speed = 2;
            }
            else
            {
                speed = 4;
            }
            float scale = (float)randomSize / snowflakeImage.Width;

            snowflakes[index].X = rand.Next(ClientRectangle.Width);
            snowflakes[index].Y = rand.Next(-200, 0); // появляется выше экрана
            snowflakes[index].Speed = speed;
            snowflakes[index].Scale = scale;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < snowflakes.Length; i++)
            {
                snowflakes[i].Y += snowflakes[i].Speed;

                if (snowflakes[i].Y > ClientRectangle.Height)
                {
                    ResetSnowFlake(i);
                }
            }

            Form1_Paint(this, new PaintEventArgs(CreateGraphics(), ClientRectangle));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var buffer = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            using (var g = Graphics.FromImage(buffer))
            {
                g.DrawImage(Properties.Resources.backgroundImg, ClientRectangle);
                foreach (var flake in snowflakes)
                {
                    if (snowflakeImage != null)
                    {
                        int drawWidth = (int)(snowflakeImage.Width * flake.Scale);
                        int drawHeight = (int)(snowflakeImage.Height * flake.Scale);
                        g.DrawImage(snowflakeImage, flake.X, flake.Y, drawWidth, drawHeight);
                    }
                }
            }
            e.Graphics.DrawImage(buffer, ClientRectangle);
        }
    }
}
