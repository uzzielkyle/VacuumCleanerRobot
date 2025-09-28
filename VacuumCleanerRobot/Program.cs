using System;

namespace VacuumCleanerRobot
{
    public class Map
    {
        private enum CellType { Empty, Dirt, Obstacle, Cleaned };
        private readonly CellType[,] _grid;
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

        public bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < this.Width && y >= 0 && y < this.Height;
        }

        public bool IsDirt(int x, int y)
        {
            return this._grid[x, y] == CellType.Dirt;
        }

        public bool IsObstacle(int x, int y)
        {
            return this._grid[x, y] == CellType.Obstacle;
        }

        public void AddDirt(int x, int y)
        {
            this._grid[x, y] = CellType.Dirt;
        }

        public void AddObstacle(int x, int y)
        {
            this._grid[x, y] = CellType.Obstacle;
        }

        public void Clean(int x, int y)
        {
            if (IsDirt(x, y))
            {
                this._grid[x, y] = CellType.Cleaned;
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

    public class Robot(Map map)
    {
        private readonly Map _map = map;
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        public int SpeedSettings { get; private set; } = 150;

        public void AdjustSpeedSettings(int newSpeedSettings)
        {
            if (newSpeedSettings <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(newSpeedSettings),
                    "Speed settings must be greater than zero."
                );
            }

            this.SpeedSettings = newSpeedSettings;
        }

        public bool Move(int newX, int newY)
        {
            if (this._map.IsObstacle(newX, newY) || !this._map.IsInBounds(newX, newY))
                return false;

            // set new location
            this.X = newX;
            this.Y = newY;

            // display the map with the robot's new location
            this._map.Display(this.X, this.Y);
            return true;
        }

        public void CleanCurrentSpot()
        {
            if (!this._map.IsDirt(this.X, this.Y)) return;

            this._map.Clean(this.X, this.Y);
            this._map.Display(this.X, this.Y);
        }

        public void StartCleaning()
        {
            Console.WriteLine("Start cleaning the room");
            // flag that determines the direction
            int direction = 1;
            for (int x = 0; x < this._map.Width; x++)
            {
                int startY = (direction == 1) ? 0 : this._map.Height - 1;
                int endY = (direction == 1) ? this._map.Height : -1;

                for (int y = startY; y != endY; y += direction)
                {
                    if (!Move(x, y))
                    {
                        // obstacle detected
                    }
                    CleanCurrentSpot();
                    Thread.Sleep(150);
                }
                direction *= -1; // reverse direction for the next column
            }

            Console.WriteLine("Done cleaning the room.");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Map myMap = new(9, 9);
            myMap.AddDirt(2, 3);
            myMap.AddDirt(6, 2);
            myMap.AddObstacle(5, 0);

            Robot myRobot = new(myMap);
            myRobot.AdjustSpeedSettings(200);
            myRobot.StartCleaning();
        }
    }
}
