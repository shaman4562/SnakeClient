using System;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.Threading;

namespace Snake
{
    public partial class Form1 : Form
    {
        private readonly string urlGameboard = "http://safeboard.northeurope.cloudapp.azure.com/api/Player/gameboard";
        private Gameboard gameboard;
        private static string CurrentDirection = "Up";

        public Form1()
        {
            Request();

            InitializeComponent();

            new Settings();

            // Set gameboard size
            int SizeX = gameboard.GameBoardSize.Width;
            int SizeY = gameboard.GameBoardSize.Height;
            pbCanvas.Image = (Image)new Bitmap(pbCanvas.Width, pbCanvas.Height);
            Graphics g = Graphics.FromImage(pbCanvas.Image);
            Pen p = new Pen(Color.Gray);
            for (int i = 0; i < SizeX - 1; i++)
            {
                g.DrawLine(p, new Point((pbCanvas.Width / SizeX * (i + 1)), 0), new Point((pbCanvas.Width / SizeX * (i + 1)), pbCanvas.Height));               
            }
            for (int i = 0; i < SizeY - 1; i++)
            {
                g.DrawLine(p, new Point(0, (pbCanvas.Height / SizeY * (i + 1))), new Point(pbCanvas.Width, (pbCanvas.Height / SizeY * (i + 1))));
            }

            gameTimer.Interval = gameboard.TurnTimeMilliseconds;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();


        }
            private void UpdateScreen(object sender, EventArgs e)
        {            
            Request();
            pbCanvas.Invalidate();            
        }

        private void PbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            Brush snakeColour = Brushes.Black;
            
                //Draw snake                
                for (int i = 0; i < gameboard.Players.Length; i++)
                {                   

                if (gameboard.Players[i].Name == "Shaman")
                    {
                        snakeColour = Brushes.Blue;
                    }
                    else
                    {
                        snakeColour = Brushes.Black;
                    }

                if (gameboard.Players[i].IsSpawnProtected == true)
                {
                    if (gameboard.Players[i].Name != "Shaman")
                        snakeColour = Brushes.Yellow;

                }

                if (gameboard.Players[i].Snake != null)
                    {
                        for (int j = 0; j < gameboard.Players[i].Snake.Length; j++)
                        {
                            canvas.FillEllipse(snakeColour,
                            new Rectangle(gameboard.Players[i].Snake[j].X * Settings.Width,
                                          gameboard.Players[i].Snake[j].Y * Settings.Height,
                                          Settings.Width, Settings.Height));
                        }
                    }
                }

                //Draw Food
                for (int j = 0; j < gameboard.MaxFood; j++)
                    {
                        canvas.FillEllipse(Brushes.Red,
                        new Rectangle(gameboard.Food[j].X * Settings.Width,
                             gameboard.Food[j].Y * Settings.Height, Settings.Width, Settings.Height));
                    }

                //Draw Walls
                for (int j = 0; j < gameboard.Walls.Length; j++)
                {
                    for (int i = 0; i < gameboard.Walls[j].Width; i++)
                    {
                        for (int q = 0; q < gameboard.Walls[j].Height; q++)
                            canvas.FillEllipse(Brushes.Green,
                        new Rectangle((gameboard.Walls[j].X + i) * Settings.Width,
                         (gameboard.Walls[j].Y + q) * Settings.Height, Settings.Width, Settings.Height));
                    }
                }                        
        }

        private void Request()
        {
            using (var webClient = new WebClient())
            {            
                string response = webClient.DownloadString(urlGameboard);
                gameboard = JsonConvert.DeserializeObject<Gameboard>(response);                
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            CurrentDirection = e.KeyCode.ToString();
            Input.PostRequestAsync(e.KeyCode.ToString());
            Request();
        }        
    }
}
