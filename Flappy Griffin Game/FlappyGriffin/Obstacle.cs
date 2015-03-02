using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyGriffin
{
    class Obstacle
    {
        public int x;
        public int y1;
        public int y2;
        public int y3;
        public int y4;
        public int y5;
        public int y6;
        public string c;

        //Changing obstacles positions
        public static Obstacle ChangeObstaclePosition(List<Obstacle> Obstacles, int i, out Obstacle oldObstacle)
        {
            oldObstacle = Obstacles[i];
            Obstacle newObstacle = new Obstacle();
            newObstacle.x = oldObstacle.x - 1;
            newObstacle.y1 = oldObstacle.y1;
            newObstacle.y2 = oldObstacle.y2;
            newObstacle.y3 = oldObstacle.y3;
            newObstacle.y4 = oldObstacle.y4;
            newObstacle.y5 = oldObstacle.y5;
            newObstacle.y6 = oldObstacle.y6;
            newObstacle.c = oldObstacle.c;
            return newObstacle;
        }

        // Creating method for printing the obstacles.
        public static void PrintObstacle(int x, int y1, int y2, int y3, int y4, int y5, int y6, string c,
            ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y1);
            Console.WriteLine(c);
            Console.SetCursorPosition(x, y2);
            Console.WriteLine(c);
            Console.SetCursorPosition(x, y3);
            Console.WriteLine(c);
            Console.SetCursorPosition(x, y4);
            Console.WriteLine(c);
            Console.SetCursorPosition(x, y5);
            Console.WriteLine(c);
            Console.SetCursorPosition(x, y6);
            Console.WriteLine(c);
        }

        //Generate different obstacles
        public static void GenerateDifferentObstacles(int chance, int playFieldWidth, List<Obstacle> Obstacles)
        {
            if (chance == 0)
            {
                Obstacle nextObstacle = new Obstacle();
                nextObstacle.x = Console.WindowWidth;
                nextObstacle.y1 = 1;
                nextObstacle.y2 = 2;
                nextObstacle.y3 = 3;
                nextObstacle.y4 = 4;
                nextObstacle.y5 = playFieldWidth - 2;
                nextObstacle.y6 = playFieldWidth - 1;
                nextObstacle.c = "|";
                Obstacles.Add(nextObstacle);
            }
            else if (chance == 1)
            {
                Obstacle nextObstacle = new Obstacle();
                nextObstacle.x = Console.WindowWidth;
                nextObstacle.y1 = 1;
                nextObstacle.y2 = 2;
                nextObstacle.y3 = 3;
                nextObstacle.y4 = playFieldWidth - 3;
                nextObstacle.y5 = playFieldWidth - 2;
                nextObstacle.y6 = playFieldWidth - 1;
                nextObstacle.c = "|";
                Obstacles.Add(nextObstacle);
            }
            else if (chance == 2)
            {
                Obstacle nextObstacle = new Obstacle();
                nextObstacle.x = Console.WindowWidth;
                nextObstacle.y1 = 1;
                nextObstacle.y2 = 2;
                nextObstacle.y3 = 3;
                nextObstacle.y4 = 4;
                nextObstacle.y5 = 5;
                nextObstacle.y6 = playFieldWidth - 1;
                nextObstacle.c = "|";
                Obstacles.Add(nextObstacle);
            }
        }

    }
}
