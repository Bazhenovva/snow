using System;
using System.Drawing;
using System.Windows.Forms;

namespace snow
{
    public partial class Form1 : Form
    {
        private int snowX = 100;
        private int snowY = -20;
        private int snowSpeed = 3;
        private Bitmap snowflakeImage;
        private Timer timer;

        public Form1()
        {
            InitializeComponent();
            snowflakeImage = Properties.Resources.snowflakeImg;
            timer = new Timer();
            timer.Interval = 20;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            snowY += snowSpeed;
            if (snowY > ClientRectangle.Height)
            {
                snowY = -20;
                snowX = new Random().Next(ClientRectangle.Width);
            }
            Form1_Paint(this, new PaintEventArgs(CreateGraphics(), ClientRectangle));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var buffer = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            using (var g = Graphics.FromImage(buffer))
            {
                g.DrawImage(Properties.Resources.backgroundImg, ClientRectangle);
                g.DrawImage(Properties.Resources.snowflakeImg, new Rectangle(snowX, snowY, 40, 40));
            }
            e.Graphics.DrawImage(buffer, ClientRectangle);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            timer.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }
    }
}
