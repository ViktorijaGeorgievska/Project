using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class GameBoard
    {
        public int[,] Map { get; set; }
        public int Rows => Map.GetLength(0);  
        public int Cols => Map.GetLength(1);  

        public GameBoard(int[,] map)
        {
            Map = map;
        }

        public bool IsWall(int x, int y)
        {
            return Map[y, x] == 1;
        }

        public bool IsInside(int x, int y)
        {
            return y >= 0 && y < Map.GetLength(0) && x >= 0 && x < Map.GetLength(1);
        }
    }
}