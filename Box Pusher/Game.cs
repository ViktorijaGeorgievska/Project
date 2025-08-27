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
                 new Point(1, 1),  // Player start
                 new Point(5, 7),  // Box start
                 new Point(1, 8)   // Goal
             ));
        
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
                 new Point(7, 2),  // Player start
                 new Point(5, 4),  // Box start
                 new Point(3, 7)   // Goal
             ));
        
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
             new Point(1, 1),  // Player start
             new Point(8, 2),  // Box start (истата како 2 во мапата)
             new Point(8, 6)   // Goal (позиција на 3 во мапата)
             ));
        }
        private void LoadLevel(int index)
        {
            if (index >= Levels.Count)
            {
                MessageBox.Show("🎉 Честитки! Ги заврши сите нивоа!");
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

        public void MovePlayer(int dx, int dy)
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

            // Проверка за успешен крај - кутијата треба да е ТОЧНО на целното поле
            if (Box.BoxPosition == Goal.GoalPosition)
            {
                MessageBox.Show($"🎉 Успешно ја стави кутијата на целта! Чекори: {StepCount}");
                CurrentLevel++;
                LoadLevel(CurrentLevel);
                return;
            }

            // Ако кутијата не е на целта, проверуваме дали е заглавена
            if (IsBoxStuck())
            {
                MessageBox.Show($"❌ Кутијата е заглавена, играта завршува неуспешно! Чекори: {StepCount}");
                LoadLevel(CurrentLevel);  // Рестартирај го сегашното ниво
            }
        }

        //private bool IsBoxStuck()
        //{
        //    int x = Box.Position.X;
        //    int y = Box.Position.Y;

        //    // Проверуваме 4 страни околу кутијата
        //    bool wallLeft = !Board.IsInside(x - 1, y) || Board.IsWall(x - 1, y);
        //    bool wallRight = !Board.IsInside(x + 1, y) || Board.IsWall(x + 1, y);
        //    bool wallUp = !Board.IsInside(x, y - 1) || Board.IsWall(x, y - 1);
        //    bool wallDown = !Board.IsInside(x, y + 1) || Board.IsWall(x, y + 1);

        //    // Ако има ѕид/пречка лево и горе (или било кои две спротивни страни)
        //    bool stuckHorizontally = wallLeft && wallRight;
        //    bool stuckVertically = wallUp && wallDown;

        //    // Или ѕид лево и горе, или лево и долу, или десно и горе, или десно и долу:
        //    bool stuckInCorner =
        //        (wallLeft && wallUp) ||
        //        (wallLeft && wallDown) ||
        //        (wallRight && wallUp) ||
        //        (wallRight && wallDown);

        //    // Ако кутијата е заглавена во корнер или помеѓу две спротивни страни
        //    if (stuckHorizontally || stuckVertically || stuckInCorner)
        //        return true;

        //    return false;
        //}

        private bool IsBoxStuck()
        {
            int x = Box.BoxPosition.X;
            int y = Box.BoxPosition.Y;

            // Проверуваме дали има ѕид од две соседни страни (агол)
            bool wallLeft = !Board.IsInside(x - 1, y) || Board.IsWall(x - 1, y);
            bool wallRight = !Board.IsInside(x + 1, y) || Board.IsWall(x + 1, y);
            bool wallUp = !Board.IsInside(x, y - 1) || Board.IsWall(x, y - 1);
            bool wallDown = !Board.IsInside(x, y + 1) || Board.IsWall(x, y + 1);

            // Вистински ќошеви (перпендикуларно заглавување)
            bool topLeftCorner = wallUp && wallLeft;
            bool topRightCorner = wallUp && wallRight;
            bool bottomLeftCorner = wallDown && wallLeft;
            bool bottomRightCorner = wallDown && wallRight;

            return topLeftCorner || topRightCorner || bottomLeftCorner || bottomRightCorner;
        }
    }
}
