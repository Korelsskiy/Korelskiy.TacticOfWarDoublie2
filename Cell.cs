using Korelskiy.TacticOfWarDoublie2;
using System;
using System.Collections.Generic;

namespace Korelskiy.TacticOfWar
{
    class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Id { get; set; }

        public bool GoUp { get; set; }

        public bool GoDown { get; set; }
        public bool GoRight { get; set; }
        public bool GoLeft { get; set; }

        public Terrain Terrain { get; set; }
        public Spawn Spawn { get; set; }

        public static List<Cell> Cells = new List<Cell>()
        {
           new Cell(1, 7, 3, false, true, false, false, Terrain.Field, Spawn.ThirdReich),

           new Cell(2, 6, 1, false, true, true, false, Terrain.Field, Spawn.No),
           new Cell(3, 6, 2, false, false, true, true, Terrain.Field, Spawn.No),
           new Cell(4, 6, 3, true, true, true, true, Terrain.Field, Spawn.No),
           new Cell(5, 6, 4, false, false, true, true, Terrain.Field, Spawn.No),
           new Cell(6, 6, 5, false, true, false, true, Terrain.Field, Spawn.No),

           new Cell(7, 5, 1, true, true, false, false, Terrain.Rise, Spawn.No),
           new Cell(8, 5, 3, true, true, false, false, Terrain.Field, Spawn.No),
           new Cell(9, 5, 5, true, true, false, false, Terrain.Forest, Spawn.No),

           new Cell(10, 4, 1, true, true, true, false, Terrain.Top, Spawn.No),
           new Cell(11, 4, 2, false, false, true, true, Terrain.Rise, Spawn.No),
           new Cell(12, 4, 3, true, true, true, true, Terrain.Farm, Spawn.No),
           new Cell(13, 4, 4, false, false, true, true, Terrain.Swamp, Spawn.No),
           new Cell(14, 4, 5, true, true, false, true, Terrain.Forest, Spawn.No),

           new Cell(15, 3, 1, true, true, false, false, Terrain.Rise, Spawn.No),
           new Cell(16, 3, 3, true, true, false, false, Terrain.Field, Spawn.No),
           new Cell(17, 3, 5, true, true, false, false, Terrain.Forest, Spawn.No),

           new Cell(18, 2, 1, true, false, true, false, Terrain.Field, Spawn.No),
           new Cell(19, 2, 2, false, false, true, true, Terrain.Field, Spawn.No),
           new Cell(20, 2, 3, true, true, true, true, Terrain.Field, Spawn.No),
           new Cell(21, 2, 4, false, false, true, true, Terrain.Field, Spawn.No),
           new Cell(22, 2, 5, true, false, false, true, Terrain.Field, Spawn.No),

           new Cell(23, 1, 3, true, false, false, false, Terrain.Field, Spawn.USSR),
         };





        public Cell() { }
        public Cell(int id, int x, int y, bool right, bool left, bool up, bool down, Terrain terrain, Spawn spawn)
        {
            Id = id;

            X = x;
            Y = y;

            GoRight = right;
            GoLeft = left;
            GoUp = up;
            GoDown = down;

            Terrain = terrain;
            Spawn = spawn;

        }

        public static void DrawMap()
        {
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("5.   |_||_||_||_||_|");
            Console.WriteLine("4.   |_|   |_|   |_|");
            Console.WriteLine("3.|_||_||_||_||_||_||_|");
            Console.WriteLine("2.   |_|   |_|   |_|");
            Console.WriteLine("1.   |_||_||_||_||_|");
            Console.WriteLine("   1. 2. 3. 4. 5. 6. 7.");
            Console.WriteLine("-------------------------------------------------------");
        }
        public static string GetTerrain(Cell cell)
        {
            string ter = "";
            if (cell.Terrain == Terrain.Farm)
                ter = "это хутор";
            if (cell.Terrain == Terrain.Field)
                ter = "здесь равнинная местность";
            if (cell.Terrain == Terrain.Forest)
                ter = "здесь лесистая местность";
            if (cell.Terrain == Terrain.Rise)
                ter = "- Подъем на высоту";
            if (cell.Terrain == Terrain.Swamp)
                ter = "здесь заболоченная местность";
            if (cell.Terrain == Terrain.Top)
                ter = "- высота";

            return ter;
        }

    }


}

