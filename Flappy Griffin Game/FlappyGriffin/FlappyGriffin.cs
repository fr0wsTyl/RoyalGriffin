using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace FlappyGriffin
{
    /*
     * 
     * TeamWork Requirements
    @[UNDONE] At least 1 multi-dimensional array
    @[UNDONE] At least 3 one-dimensional arrays
    @[DONE] At least 10 methods (separating the application’s logic)
    @[DONE] At least 3 existing .NET classes (like System.Math or System.DateTime)
    @[DONE] At least 2 exception handlings
    @[DONE] At least 1 use of external text file

     */
    class FlappyGriffin
    {
        public static string TOP_SCORES_FILE = "..//..//Scores//TopScore.txt";
        public static string CONFIG_FILE = "..//..//Scores//Config.txt";
        //Main method
        static void Main()
        {
            //Setting variables
            int lives = 5;
            long score = 0;
            Console.WriteLine("Enter your name: ");
            string userName = Console.ReadLine();

            string[] ScoresInfo = GamePlay.ReadScore();
            string topScoreString = ScoresInfo[0];
            string userNameBestScore = ScoresInfo[1];

            long topScore;
            // Exception 2
            try
            {
                topScore = long.Parse(topScoreString);
            }
            catch (Exception)
            {
                topScore = 0;
            }
            /*
             * Initializating the playfield window by reading
             * settings from a config file.
             */
            GamePlay.WindowsSize(CONFIG_FILE);

            /*
             * Initializating the griffin.
             */
            string griffinHead = "G";
            int griffinX = 10;
            /*
             * Here we use a GamePlay variable that 
             * was initialized by the WindowsSize() method.
             */
            int griffinY = GamePlay.playFieldWidth / 2;
            Objects griffin = Objects.CreateGriffin(griffinHead, griffinX, griffinY);
            GamePlay.PlayMusic("start");        //Start sound


            List<Obstacle> Obstacles = new List<Obstacle>();

            while (true)
            {
                bool obstacleHitted = false;
                int chance = GamePlay.ReturnRandomValue(0, 5);
                // Random number generates different obstacle.
                Obstacle.GenerateDifferentObstacles(chance, GamePlay.playFieldWidth, Obstacles);
                // Checking if there is any key pressed.
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyPressed;
                    Console.Beep(1000,50);
                    //GamePlay.PlayMusic("up");
                    griffin = Objects.CheckGriffin(griffin, out keyPressed);
                }
                else //Added case in which if the player is not pressing the key the bird falls down by itself
                {
                    if (griffin.y + 1 <= GamePlay.playFieldWidth)
                    {
                        if (griffin.y + 1 == GamePlay.playFieldWidth || griffin.y + 1 == GamePlay.playFieldWidth)
                        {

                            Obstacles.Clear();
                            // If we hit an obstacle we start again from starting position.
                            // switching symbol when rock is hitted.
                            GamePlay.PrintStringOnPosition(griffin.x, griffin.y, "GAME OVER", ConsoleColor.Red);
                            GamePlay.PlayMusic("crash");                                     // Sound when ground is hitted.
                            GamePlay.PlayMusic("aplause");                                   // sound when game over.
                            GamePlay.PrintStringOnPosition(griffin.x - 5, griffin.y - 5, "Press any key to restart the game",
                                ConsoleColor.White);
                            Console.ReadKey();
                            griffin.x = 10;
                            griffin.y = GamePlay.playFieldWidth / 2;
                            lives--;
                            if (lives < 1)                         // Ends the game when you reach 0 lives from falling down.
                            {
                                return;
                            }

                        }
                        else
                        {
                            griffin.y += 1;
                        }

                    }
                }
                // Creating new list of moving objects and filling it.
                List<Obstacle> newObstacles = new List<Obstacle>();
                for (int i = 0; i < Obstacles.Count; i++)
                {
                    // the same objects but on their new positions.

                    Obstacle oldObstacle;
                    var newObstacle = Obstacle.ChangeObstaclePosition(Obstacles, i, out oldObstacle);
                    // Checking if the griffin hits any moving objects.

                    if (newObstacle.x == griffin.x &&
                       (newObstacle.y1 == griffin.y ||
                        newObstacle.y2 == griffin.y ||
                        newObstacle.y3 == griffin.y ||
                        newObstacle.y4 == griffin.y ||
                        newObstacle.y5 == griffin.y ||
                        newObstacle.y6 == griffin.y))
                    {
                        obstacleHitted = true; // If we hit an obstacle we lose a life.
                        if (score > topScore)
                        {
                            userNameBestScore = userName;
                            topScore = score;
                            StreamWriter writer = new StreamWriter("..//..//Scores//TopScore.txt");
                            writer.WriteLine(topScore);
                            writer.WriteLine(userName);
                            writer.Close();
                        }
                        if (lives == 0)
                        {
                            return;        // If we lose our last life the game is over.
                        }
                        else
                        {
                            lives--;       // If we still have lives we lose one life and the game continues.
                            score = 0;
                            GamePlay.speed = 400;
                        }
                    }
                    // Checking if we have succesfully passed an obstacle.
                    else if (newObstacle.x == griffin.x &&
                        newObstacle.y1 != griffin.y &&
                        newObstacle.y2 != griffin.y &&
                        newObstacle.y3 != griffin.y &&
                        newObstacle.y4 != griffin.y &&
                        newObstacle.y5 != griffin.y &&
                        newObstacle.y6 != griffin.y)
                    {
                        score++;
                        if (GamePlay.speed < 300 && GamePlay.speed > 250)
                        {
                            GamePlay.speed -= 2;
                        }
                        else if (GamePlay.speed < 250 && GamePlay.speed > 200)
                        {
                            GamePlay.speed -= 1;
                        }
                        else if (GamePlay.speed < 200 && GamePlay.speed > 0)
                        {
                            GamePlay.speed -= 0;
                        }
                        else
                        {
                            GamePlay.speed -= 8;
                        }
                    }
                    if (newObstacle.x > 0)
                    {
                        // When an object disappears from the screen a new one appears in starting position
                        newObstacles.Add(newObstacle);
                    }
                }
                // Current list of objects saves the new objects and their new positions.
                Obstacles = newObstacles;

                // Clearing the console from the old objects.
                Console.Clear();
                if (obstacleHitted)
                {
                    // If we hit an obstacle we start again from starting position.
                    Obstacles.Clear();

                    // switching symbol when rock is hitted
                    GamePlay.PrintStringOnPosition(griffin.x, griffin.y, "X", ConsoleColor.Red);
                    // sound when rock is hitted.
                    GamePlay.PlayMusic("crash");
                }
                else   // Printing the griffin.
                {
                    GamePlay.PrintStringOnPosition(griffin.x, griffin.y, griffin.c.ToString(), griffin.color);
                }
                foreach (Obstacle obstacle in Obstacles) // Printing the obstacles.
                {
                    Obstacle.PrintObstacle(obstacle.x, obstacle.y1, obstacle.y2, obstacle.y3, obstacle.y4, obstacle.y5, obstacle.y6,
                        obstacle.c);
                }

                for (int i = 0; i < Console.WindowWidth; i++) // Drawing the play field borders.
                {
                    GamePlay.PrintStringOnPosition(i, GamePlay.playFieldWidth, "-", ConsoleColor.Blue);
                    GamePlay.PrintStringOnPosition(i, 0, "-", ConsoleColor.Blue);
                }
                // Printing info.
                GamePlay.PrintInfo(score, lives, topScore, userNameBestScore, userName);
                Thread.Sleep(GamePlay.speed); // Slows down the program so we can see what happens on the screen. We can change the speed. 
            }
        }
    }
}
