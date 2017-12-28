using System;
using System.Collections.Generic;

namespace core_snake_game_demo
{
    class Pos
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Program
    {
        static char[][] grid = new char[20][];
        static int width = 50;
        static int height = 20;

        static List<Pos> worm = new List<Pos>();

        static void Main(string[] args)
        {
            //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var currentForegroundColor = Console.ForegroundColor;

            //Console.WriteLine("Hello World!");
            InitFrame();
            DrawFrame();
            InitWorm();
            DrawWorm();

            // reset foreground color
            Console.ForegroundColor = currentForegroundColor;

            // reset cursor
            Console.SetCursorPosition(0, height + 1);
            Console.CursorVisible = true;
        }

        /* Utility Methods */
        static void InitFrame()
        {
            Console.CursorVisible = false;

            for (int i = 0; i < height; i++)
                grid[i] = new char[width];

            grid[0][0] = '╔';
            grid[0][width - 1] = '╗';
            grid[height - 1][0] = '╚';
            grid[height - 1][width - 1] = '╝';

            // fill left and right borders
            for (int i = 1; i < height - 1; i++)
            {
                grid[i][0] = '║';
                grid[i][width - 1] = '║';
            }

            for (int i = 1; i < width - 1; i++)
            {
                grid[0][i] = '═';
                grid[height - 1][i] = '═';
            }

            for (int y = 1; y < height - 1; y++)
                for (int x = 1; x < width - 1; x++)
                    grid[y][x] = ' ';

        }

        static void DrawFrame()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(grid[y][x].ToString());
                }

            //Console.WriteLine(); // create new line

        }

        static void InitWorm()
        {
            worm.Add(new Pos() { X = 21, Y = 9 });
            worm.Add(new Pos() { X = 22, Y = 9 });
            worm.Add(new Pos() { X = 23, Y = 9 });
            worm.Add(new Pos() { X = 24, Y = 9 });
            worm.Add(new Pos() { X = 25, Y = 9 });
            foreach (Pos pos in worm)
            {
                grid[pos.Y][pos.X] = 'o';
            }

        }

        static void DrawWorm()
        {
            int count = 0;
            Console.ForegroundColor = ConsoleColor.Yellow;

            foreach (Pos wormpart in worm)
            {
                Console.SetCursorPosition(wormpart.X, wormpart.Y);
                count++;
                if (count < 5)
                    Console.Write('o');
                else
                    Console.Write('ö');
            }
        }
    }


}
