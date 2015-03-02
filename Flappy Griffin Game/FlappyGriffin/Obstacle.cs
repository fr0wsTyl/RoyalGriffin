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
