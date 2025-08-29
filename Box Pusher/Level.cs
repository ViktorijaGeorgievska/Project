using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Level
    {
        public int[,] Map { get; set; }
        public Point PlayerStart { get; set; }
        public Point BoxStart { get; set; }
        public Point Goal { get; set; }

        public Level(int[,] map, Point playerStart, Point boxStart, Point goal)
        {
            Map = map;
            PlayerStart = playerStart;
            BoxStart = boxStart;
            Goal = goal;
        }
    }
}