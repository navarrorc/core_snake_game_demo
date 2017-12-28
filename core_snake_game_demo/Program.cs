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
        static bool gameover = false;
        static int worm_x = 25;
        static int worm_y = 9;
        enum Direction { UP = 1, DOWN, LEFT, RIGHT }
        static Direction current = Direction.RIGHT;

        static int lengthtimer = 0;
        static int lengthtime = 8;
        static int wormlength = 5;
        static int target_x;
        static int target_y;
        static int score = 0;

        static List<Pos> worm = new List<Pos>();

        static void Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var currentForegroundColor = Console.ForegroundColor;

            //Console.WriteLine("Hello World!");
            InitFrame();
            DrawFrame();
            InitWorm();
            DrawWorm();
            SetTarget();
            InitScore();

            while (!gameover)
            {
                DrawWormHead();
                if (TargetTaken())
                {
                    SetTarget();
                    UpdateScore();
                }
                Pause();
                ReadKeys();
                DrawWormBodyOnHeadPosition();
                MoveWormHead();

                if (isGameOver())
                    gameover = true;

                IncreaseWormLength();
                DeleteWormTail();
            }

            DrawWormHead();
            Console.SetCursorPosition(0, 20);
            Console.WriteLine("Game Over");



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

        static void DrawWormHead()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(worm_x, worm_y);
            Console.Write('ö');
        }

        static void DrawWormBodyOnHeadPosition()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(worm_x, worm_y);
            Console.Write('o');
        }

        static void MoveWormHead()
        {
            grid[worm_y][worm_x] = 'o';

            switch (current)
            {
                case Direction.UP:
                    worm_y--;
                    break;
                case Direction.DOWN:
                    worm_y++;
                    break;
                case Direction.LEFT:
                    worm_x--;
                    break;
                case Direction.RIGHT:
                    worm_x++;
                    break;
                default:
                    break;
            }

            worm.Add(new Pos() { X = worm_x, Y = worm_y });
        }

        static bool isGameOver()
        {
            bool value = false;

            if (grid[worm_y][worm_x] != ' ')
                value = true;

            return value;
        }

        static bool TargetTaken()
        {
            return worm_x == target_x && worm_y == target_y;
        }

        static void SetTarget()
        {
            Random rnd = new Random();
            int x = 0;
            int y = 0;

            while (grid[y][x] != ' ')
            {
                x = rnd.Next(1, width - 1);
                y = rnd.Next(1, height - 1);
            }

            target_x = x;
            target_y = y;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(x, y);
            Console.Write('☼');
        }

        static void InitScore()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(51, 0);
            Console.Write("Score: 0");
        }

        static void UpdateScore()
        {
            score++;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(59, 0);
            Console.Write(score);
            //PlayScoreSound();
        }

        static void DeleteWormTail()
        {
            Console.SetCursorPosition(worm[0].X, worm[0].Y);
            Console.Write(' ');
            if (worm.Count != wormlength)
            {
                grid[worm[0].Y][worm[0].X] = ' ';
                worm.RemoveAt(0);
            }
        }

        static void IncreaseWormLength()
        {
            lengthtimer++;

            if (lengthtimer == lengthtime)
            {
                lengthtimer = 0;
                wormlength++;
            }
        }

        static void Pause()
        {
            System.Threading.Thread.Sleep(100);
        }

        static void ReadKeys()
        {
            ConsoleKeyInfo s;

            if (Console.KeyAvailable)
            {
                s = Console.ReadKey(true);

                switch (s.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (current != Direction.DOWN)
                        {
                            current = Direction.UP;
                            //PlayMoveSound();
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (current != Direction.UP)
                        {
                            current = Direction.DOWN;
                            //PlayMoveSound();
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (current != Direction.RIGHT)
                        {
                            current = Direction.LEFT;
                            //PlayMoveSound();
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (current != Direction.LEFT)
                        {
                            current = Direction.RIGHT;
                            //PlayMoveSound();
                        }
                        break;

                    default:
                        break;
                }
            }
        }
    }


}
