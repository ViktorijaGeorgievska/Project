using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Box
    {
        public Point BoxPosition { get; set; }

        public Box(int x, int y)
        {
            BoxPosition = new Point(x, y);
        }

        public void Move(int dx, int dy)
        {
            BoxPosition = new Point(BoxPosition.X + dx, BoxPosition.Y + dy);
        }
    }
}
