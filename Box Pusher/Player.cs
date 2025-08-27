using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Project.Form1;

namespace Project
{
    public class Player
    {
        public Point PlayerPosition { get; set; }
        public Direction CurrentDirection { get; set; } = Direction.Down;

        public Player(int x, int y)
        {
            PlayerPosition = new Point(x, y);
        }

        public void Move(int dx, int dy)
        {
            PlayerPosition = new Point(PlayerPosition.X + dx, PlayerPosition.Y + dy);
        }
    }
}
