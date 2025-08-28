using BoxPusher;
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
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
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
            this.Controls.Add(lblInfo);

            int formWidth = game.Board.Cols * tileSize + 20;  // +16 за граници
            int formHeight = game.Board.Rows * tileSize + 70; // +60 за Label и padding

            this.ClientSize = new Size(formWidth, formHeight);

            InitializeBoardVisual();
            UpdateInfoLabel();
            UpdateBoardVisual();
            this.Invalidate();

            this.KeyDown += Form1_KeyDown;
        }

        private void UpdateInfoLabel()
        {
            lblInfo.Text = $"Ниво: {game.CurrentLevel + 1}  Чекори: {game.StepCount}";
        }

        private PictureBox[,] pictureBoxes;
        private int tileSize = 50; 

        private void InitializeBoardVisual()
        {
            int rows = game.Board.Rows;
            int cols = game.Board.Cols;

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
                        pb.Image = WindowsFormsApp1.Properties.Resources.wall;
                    }
                    else if (game.Player.PlayerPosition == new Point(x, y))
                    {
                        switch (game.Player.CurrentDirection)
                        {
                            case Direction.Up:
                                pb.Image = WindowsFormsApp1.Properties.Resources.soko_up;
                                break;
                            case Direction.Down:
                                pb.Image = WindowsFormsApp1.Properties.Resources.soko_down;
                                break;
                            case Direction.Left:
                                pb.Image = WindowsFormsApp1.Properties.Resources.soko_left;
                                break;
                            case Direction.Right:
                                pb.Image = WindowsFormsApp1.Properties.Resources.soko_right;
                                break;
                        }
                    }
                    else if (game.Box.BoxPosition == new Point(x, y))
                    {
                        pb.Image = WindowsFormsApp1.Properties.Resources.package;
                    }
                    else if (game.Goal.GoalPosition == new Point(x, y))
                    {
                        pb.Image = WindowsFormsApp1.Properties.Resources.package_goal;
                    }
                    else
                    {
                        pb.Image = WindowsFormsApp1.Properties.Resources.floor;
                    }
                }
            }
            UpdateInfoLabel();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    game.MovePlayer(0, -1);
                    game.Player.CurrentDirection = Direction.Up;
                    break;
                case Keys.Down:
                    game.MovePlayer(0, 1);
                    game.Player.CurrentDirection = Direction.Down;
                    break;
                case Keys.Left:
                    game.MovePlayer(-1, 0);
                    game.Player.CurrentDirection = Direction.Left;
                    break;
                case Keys.Right:
                    game.MovePlayer(1, 0);
                    game.Player.CurrentDirection = Direction.Right;
                    break;
            }
            UpdateBoardVisual();
            this.Invalidate();
        }
    }
}
