
using BoxPusher;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public class Game
    {
        public GameBoard Board { get; set; }
        public Player Player { get; set; }
        public Box Box { get; set; }
        public Goal Goal { get; set; }

        private List<Level> Levels;
        public int CurrentLevel { get; set; }
        public int StepCount { get; set; } = 0;

        public Game()
        {
            LoadLevels();
            LoadLevel(0);
        }

        private void LoadLevels()
        {
            Levels = new List<Level>();

            // Ниво 1
            Levels.Add(new Level(
                new int[,] {
             {1,1,1,1,1,1,1,1,1,1},
             {1,0,0,0,1,0,0,0,2,1},
             {1,0,1,0,1,0,1,0,0,1},
             {1,0,1,0,0,0,1,1,0,1},
             {1,0,1,1,1,0,0,0,0,1},
             {1,0,0,0,1,1,1,1,0,1},
             {1,1,1,0,0,0,0,1,0,1},
             {1,0,0,0,1,0,0,1,0,1},
             {1,3,0,0,0,0,0,0,0,1},
             {1,1,1,1,1,1,1,1,1,1},
                },
                new Point(1, 1),  // Почетна позиција на играчот
                new Point(5, 7),  // Почетна позиција на кутијата
                new Point(1, 8)   // Цел
            ));

            // Ниво 2
            Levels.Add(new Level(
                new int[,] {
             {1,1,1,1,1,1,1,1,1,1},
             {1,0,0,0,0,0,0,0,0,1},
             {1,0,1,1,1,1,1,0,0,1},
             {1,0,0,0,0,0,1,1,0,1},
             {1,1,1,1,1,2,0,0,0,1},
             {1,0,0,0,1,0,1,1,0,1},
             {1,0,1,0,0,0,0,1,0,1},
             {1,0,1,0,1,0,0,0,0,1},
             {1,3,0,0,0,0,0,0,0,1},
             {1,1,1,1,1,1,1,1,1,1},
                },
                new Point(7, 2),
                new Point(5, 4),
                new Point(3, 7)
            ));

            // Ниво 3 
            Levels.Add(new Level(
                new int[,] {
             {1,1,1,1,1,1,1,1,1,1},
             {1,2,0,1,0,0,0,0,0,1},
             {1,0,0,1,0,1,1,1,0,1},
             {1,0,1,1,0,0,0,1,0,1},
             {1,0,1,0,0,1,0,1,0,1},
             {1,0,1,0,1,1,0,1,0,1},
             {1,0,0,0,0,0,0,1,3,1},
             {1,1,1,1,1,1,0,0,0,1},
             {1,0,0,0,0,0,0,1,0,1},
             {1,1,1,1,1,1,1,1,1,1},
                },
                new Point(1, 1),  // Почетна позиција на играчот
                new Point(8, 2),  // Почетна позиција на кутијата
                new Point(8, 6)   // Цел
            ));
        }
        private void LoadLevel(int index)
        {
            if (index >= Levels.Count)
            {
                MessageBox.Show("🎉 Честитки! Успешно ги заврши сите нивоа!");
                Application.Exit();
                return;
            }

            CurrentLevel = index;
            StepCount = 0;
            Level level = Levels[index];

            Board = new GameBoard(level.Map);
            Player = new Player(level.PlayerStart.X, level.PlayerStart.Y);
            Box = new Box(level.BoxStart.X, level.BoxStart.Y);
            Goal = new Goal(level.Goal.X, level.Goal.Y);
        }

        public async void MovePlayer(int dx, int dy)
        {
            int nextX = Player.PlayerPosition.X + dx;
            int nextY = Player.PlayerPosition.Y + dy;

            if (!Board.IsInside(nextX, nextY) || Board.IsWall(nextX, nextY))
                return;

            if (Box.BoxPosition.X == nextX && Box.BoxPosition.Y == nextY)
            {
                int boxNextX = Box.BoxPosition.X + dx;
                int boxNextY = Box.BoxPosition.Y + dy;

                if (!Board.IsInside(boxNextX, boxNextY) || Board.IsWall(boxNextX, boxNextY))
                    return;

                Box.Move(dx, dy);
            }

            Player.Move(dx, dy);

            StepCount++;

            if (Box.BoxPosition == Goal.GoalPosition)
            {
                await Task.Delay(100);
                if (MessageBox.Show($"🎉 Успешно ја стави кутијата на целта! Чекори: {StepCount}", "Ново ниво", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    CurrentLevel++;
                    LoadLevel(CurrentLevel);
                    return;
                }
            }

            if (IsBoxStuck())
            {
                await Task.Delay(100);
                if (MessageBox.Show($"❌ Кутијата е заглавена! Чекори: {StepCount}", "Game Over", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    LoadLevel(CurrentLevel);
                }
            }
        }

        private bool IsBoxStuck()
        {
            int x = Box.BoxPosition.X;
            int y = Box.BoxPosition.Y;

            bool wallLeft = !Board.IsInside(x - 1, y) || Board.IsWall(x - 1, y);
            bool wallRight = !Board.IsInside(x + 1, y) || Board.IsWall(x + 1, y);
            bool wallUp = !Board.IsInside(x, y - 1) || Board.IsWall(x, y - 1);
            bool wallDown = !Board.IsInside(x, y + 1) || Board.IsWall(x, y + 1);

            bool topLeftCorner = wallUp && wallLeft;
            bool topRightCorner = wallUp && wallRight;
            bool bottomLeftCorner = wallDown && wallLeft;
            bool bottomRightCorner = wallDown && wallRight;

            return topLeftCorner || topRightCorner || bottomLeftCorner || bottomRightCorner;
        }
    }
}
