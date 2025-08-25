using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Form1 : Form
    {
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private Game game;
        private const int CellSize = 50;
        private Label lblInfo;

        public Form1()
        {
            InitializeComponent();

            this.Text = "Box Pusher";
            this.Width = 400;
            this.Height = 300;
            this.DoubleBuffered = true;

            game = new Game();

            lblInfo = new Label();
            lblInfo.Font = new Font("Arial", 12, FontStyle.Bold);
            lblInfo.AutoSize = true;
            lblInfo.Top = tileSize * game.Board.Rows + 10;
            lblInfo.Left = 10;
            lblInfo.Text = $"Ниво: {game.CurrentLevel + 1}  Чекори: {game.StepCount}";
            this.Controls.Add(lblInfo);

            int formWidth = game.Board.Cols * tileSize + 20; // +16 за граници
            int formHeight = game.Board.Rows * tileSize + 70; // +60 за Label и padding

            this.ClientSize = new Size(formWidth, formHeight);

            InitializeBoardVisual();   // <--- ДОДАДИ ОВА
            UpdateInfoLabel();
            UpdateBoardVisual();
            this.Invalidate();

            this.KeyDown += Form1_KeyDown;
            this.Paint += Form1_Paint;
        }

        private void UpdateInfoLabel()
        {
            lblInfo.Text = $"Ниво: {game.CurrentLevel + 1}  Чекори: {game.StepCount}";
        }

        private PictureBox[,] pictureBoxes;
        private int tileSize = 50;  // големина на секое поле (px)

        private void InitializeBoardVisual()
        {
            int rows = game.Board.Rows; // број на редови во таблата
            int cols = game.Board.Cols; // број на колони

            pictureBoxes = new PictureBox[rows, cols];

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    PictureBox pb = new PictureBox();
                    pb.Width = tileSize;
                    pb.Height = tileSize;
                    pb.Left = x * tileSize;
                    pb.Top = y * tileSize;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.BorderStyle = BorderStyle.FixedSingle;

                    this.Controls.Add(pb);
                    pictureBoxes[y, x] = pb;
                }
            }
        }
        private void UpdateBoardVisual()
        {
            for (int y = 0; y < game.Board.Rows; y++)
            {
                for (int x = 0; x < game.Board.Cols; x++)
                {
                    PictureBox pb = pictureBoxes[y, x];
                    if (game.Board.IsWall(x, y))
                    {
                        pb.Image = Properties.Resources.wall_jpg; // слика за ѕид
                    }
                    else if (game.Player.PlayerPosition == new Point(x, y))
                    {
                        pb.Image = Properties.Resources.soko_down_jpg;  // слика за човече
                    }
                    else if (game.Box.BoxPosition == new Point(x, y))
                    {
                        pb.Image = Properties.Resources.package_jpg;  // слика за кутија
                    }
                    //else if (game.Goal.Position == new Point(x, y) && game.Box.Position == new Point(x, y))
                    //{
                    //    pb.Image = Properties.Resources.package_goal; // треба да додадеш слика
                    //}
                    else if (game.Goal.GoalPosition == new Point(x, y))
                    {
                        pb.Image = Properties.Resources.package_goal_jpg;  // слика за цел
                    }
                    else
                    {
                        pb.Image = Properties.Resources.floor_jpg;  // празно поле
                    }
                }
            }
            lblInfo.Text = $"Ниво: {game.CurrentLevel + 1}  Чекори: {game.StepCount}";
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up: game.MovePlayer(0, -1); break;
                case Keys.Down: game.MovePlayer(0, 1); break;
                case Keys.Left: game.MovePlayer(-1, 0); break;
                case Keys.Right: game.MovePlayer(1, 0); break;
            }
            UpdateBoardVisual();
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            for (int y = 0; y < game.Board.Map.GetLength(0); y++)
            {
                for (int x = 0; x < game.Board.Map.GetLength(1); x++)
                {
                    Rectangle cell = new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize);

                    if (game.Board.Map[y, x] == 1)
                        g.FillRectangle(Brushes.Gray, cell);
                    else if (game.Board.Map[y, x] == 2)
                        g.FillRectangle(Brushes.Yellow, cell);
                    else
                        g.FillRectangle(Brushes.White, cell);

                    g.DrawRectangle(Pens.Black, cell);
                }
            }

            // Кутија
            g.FillRectangle(Brushes.Brown, new Rectangle(game.Box.BoxPosition.X * CellSize, game.Box.BoxPosition.Y * CellSize, CellSize, CellSize));

            // Играч
            g.FillEllipse(Brushes.Blue, new Rectangle(game.Player.PlayerPosition.X * CellSize + 10, game.Player.PlayerPosition.Y * CellSize + 10, 30, 30));

        }
    }
}
