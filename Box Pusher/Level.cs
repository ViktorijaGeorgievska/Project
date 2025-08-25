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
        public int[,] Map { get; }
        public Point PlayerStart { get; }
        public Point BoxStart { get; }
        public Point Goal { get; }

        public Level(int[,] map, Point playerStart, Point boxStart, Point goal)
        {
            Map = map;
            PlayerStart = playerStart;
            BoxStart = boxStart;
            Goal = goal;
        }
    }
}