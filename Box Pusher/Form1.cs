using System;
using System.Drawing;
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
        private Label lblInfo;
        private PictureBox[,] pictureBoxes;
        private int tileSize = 50;

        public Form1()
        {
            InitializeComponent();

            this.Text = "Box Pusher";
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Width = 400;
            this.Height = 300;
            
            game = new Game();

            lblInfo = new Label();
            lblInfo.Font = new Font("Arial", 12, FontStyle.Bold);
            lblInfo.AutoSize = true;
            lblInfo.Top = tileSize * game.Board.Rows + 10;
            lblInfo.Left = 20;
            this.Controls.Add(lblInfo);

            Panel bottomPanel = new Panel();
            bottomPanel.Width = this.ClientSize.Width;
            bottomPanel.Height = 40;
            bottomPanel.Top = tileSize * game.Board.Rows;
            bottomPanel.Left = 370;
            bottomPanel.BackColor = Color.Transparent;
            this.Controls.Add(bottomPanel);

            Button btnMenu = new Button();
            btnMenu.Text = "Мени";
            btnMenu.Font = new Font("Arial", 10, FontStyle.Bold);
            btnMenu.Width = 100;
            btnMenu.Height = 30;
            btnMenu.Left = 10;
            btnMenu.Top = 5;
            btnMenu.TabStop = false;
            btnMenu.Click += BtnMenu_Click;
            bottomPanel.Controls.Add(btnMenu);

            int formWidth = game.Board.Cols * tileSize;
            int formHeight = game.Board.Rows * tileSize + 50;
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
                        pb.Image = Properties.Resources.wall;
                    else if (game.Player.PlayerPosition.X == x && game.Player.PlayerPosition.Y == y)
                    {
                        switch (game.Player.CurrentDirection)
                        {
                            case
                            Direction.Up:
                                pb.Image = Properties.Resources.player_up;
                                break;
                            case
                            Direction.Down:
                                pb.Image = Properties.Resources.player_down;
                                break;
                            case
                            Direction.Left:
                                pb.Image = Properties.Resources.player_left;
                                break;
                            case
                            Direction.Right:
                                pb.Image = Properties.Resources.player_right;
                                break;
                        }
                    }
                    else if (game.Box.BoxPosition == new Point(x, y))
                    {
                        pb.Image = Properties.Resources.box;
                    }
                    else if (game.Goal.GoalPosition == new Point(x, y))
                    {
                        pb.Image = Properties.Resources.box_goal;
                    }
                    else
                    {
                        pb.Image = Properties.Resources.floor;
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

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Дали сакаш да се вратиш во менито?\nТековната игра нема да се зачува.",
                "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                StartForm startForm = new StartForm();
                startForm.StartPosition = FormStartPosition.CenterScreen;
                startForm.Show();
                this.Hide();
            }
            else
            {
                this.ActiveControl = null;
                this.Focus();
            }
        }
    }
}
