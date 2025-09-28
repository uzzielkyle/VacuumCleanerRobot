using System;

public class Program
{
    public class Map
    {
        private enum CellType { Empty, Dirt, Obstacle, Cleaned };
        private CellType[,] _grid;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Map(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            _grid = new CellType[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _grid[x, y] = CellType.Empty;
                }
            }
        }
        public void Display(int robotX, int robotY)
        {
            Console.Clear();
            Console.WriteLine("Vacuum cleaner robot simulation");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Legends: #=Obstacles, D=Dirt, .=Empty, R=Robot, C=Cleaned");

            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    if (x == robotX && y == robotY)
                    {
                        Console.Write("R ");
                    }
                    else
                    {
                        switch (this._grid[x, y])
                        {
                            case CellType.Empty: Console.Write(". "); break;
                            case CellType.Dirt: Console.Write("D "); break;
                            case CellType.Obstacle: Console.Write("# "); break;
                            case CellType.Cleaned: Console.Write("C "); break;
                        }
                    }
                }
                Console.WriteLine(); 
            }
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("--------------- Vacuum Cleaner Robot Program ------------------");
        Map myMap = new Map(5, 5);

        // Console.WriteLine($"Grid width: {myMap.Width}");
        // Console.WriteLine($"Grid height: {myMap.Height}");
        myMap.Display(1, 2);
    }
}