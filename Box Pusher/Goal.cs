using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Goal
    {
        public Point GoalPosition { get; set; }

        public Goal(int x, int y)
        {
            GoalPosition = new Point(x, y);
        }
    }
}