using System;
using System.Drawing;
using System.Windows.Forms;

namespace snow
{
    public partial class SnowfallForm  : Form
    {
        private const int CountSnowFlakes = 150;
        private const int TimerInterval = 50;

        private SnowFlake[] snowflakes;
        private Bitmap snowflakeImage;
        private Bitmap background;
        private System.Windows.Forms.Timer timer;
        private static  Random rand = new Random();

        private Bitmap buffer;
        private Graphics bufferGraphics;

        private const int MinSpeed = 3;
        private const int MaxSpeed = 6;

        private const int MinRandSize = 20;
        private const int MaxRandSize = 51;
        private const int MediumRandSize = 35;

        private const int LessPositionY = -100;
        private const int MorePositionY = 0;

        private struct SnowFlake
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Speed { get; set; }
            public float Scale { get; set; }
        }

        public SnowfallForm()
        {
            InitializeComponent();

            // для устранение утечки ОЗУ
            snowflakeImage = new Bitmap(Properties.Resources.snowflakeImg);
            background = new Bitmap(Properties.Resources.backgroundImg);

            snowflakes = new SnowFlake[CountSnowFlakes];

            //таймер
            timer = new System.Windows.Forms.Timer();
            timer.Interval = TimerInterval;
            timer.Tick += Timer_Tick;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeSnowflakes();
            buffer = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            bufferGraphics = Graphics.FromImage(buffer);

            timer.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            {
                Close();
            }
        }

        private void InitializeSnowflakes()
        {
            for (var i = 0; i < snowflakes.Length; i++)
            {
                ResetSnowFlake(i);
            }
        }

        private void ResetSnowFlake(int index)
        {
            //размер снежинки
            var randomSize = rand.Next(MinRandSize, MaxRandSize);

            var speed = randomSize <= MediumRandSize ? MinSpeed : MaxSpeed;

            var scale = (float)randomSize / snowflakeImage.Width;

            snowflakes[index].X = rand.Next(ClientRectangle.Width);
            snowflakes[index].Y = rand.Next(LessPositionY, MorePositionY); // появляется выше экрана
            snowflakes[index].Speed = speed;
            snowflakes[index].Scale = scale;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            for (var i = 0; i < snowflakes.Length; i++)
            {
                snowflakes[i].Y += snowflakes[i].Speed;

                if (snowflakes[i].Y > ClientRectangle.Height)
                {
                    ResetSnowFlake(i);
                }
            }
            bufferGraphics.DrawImage(background, 0, 0, ClientRectangle.Width, ClientRectangle.Height);


            foreach (var flake in snowflakes)
            {
                if (snowflakeImage != null)
                {
                    var drawWidth = (int)(snowflakeImage.Width * flake.Scale);
                    var drawHeight = (int)(snowflakeImage.Height * flake.Scale);
                    bufferGraphics.DrawImage(snowflakeImage, flake.X, flake.Y, drawWidth, drawHeight);
                }
            }

            using (var g = CreateGraphics())
            {
                g.DrawImage(buffer, 0, 0);
            }
        }
    }
}
