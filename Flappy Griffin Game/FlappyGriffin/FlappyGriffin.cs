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
            StreamReader ScoreReader = null;
            // Exception 1
            try
            {
                ScoreReader = new StreamReader(FlappyGriffin.TOP_SCORES_FILE);
            }
            catch (Exception)
            {
                // no such file
                StreamWriter ScoreWriter = File.CreateText(FlappyGriffin.TOP_SCORES_FILE);
                ScoreWriter.WriteLine("0");
                ScoreWriter.WriteLine("Unknown");
                ScoreWriter.Close();
                ScoreReader = new StreamReader(FlappyGriffin.TOP_SCORES_FILE);
            }
            string topScoreString = ScoreReader.ReadLine();
            string userNameBestScore = ScoreReader.ReadLine();
            ScoreReader.Close();
            long topScore = long.Parse(topScoreString);
            // Exception 2
            try
            {
                topScore = long.Parse(topScoreString);
            }
            catch (Exception)
            {
                topScore = 0;
            }

            int speed = 400;

            //Initializating window size
            GamePlay.WindowsSize(CONFIG_FILE);

            //Initializating griffin
            string griffinHead = "G";
            int griffinX = 10;
            int griffinY = GamePlay.playFieldWidth / 2;
            Objects griffin = Objects.CreateGriffin(griffinHead, griffinX, griffinY);

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
                            Console.Beep(100, 500);                                      // sound when rock is hitted.
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
                            speed = 400;
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
                        if (speed < 300 && speed > 250)
                        {
                            speed -= 2;
                        }
                        else if (speed < 250 && speed > 200)
                        {
                            speed -= 1;
                        }
                        else if (speed < 200 && speed > 0)
                        {
                            speed -= 0;
                        }
                        else
                        {
                            speed -= 8;
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
                    Console.Beep(100, 500);
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
                Thread.Sleep(speed); // Slows down the program so we can see what happens on the screen. We can change the speed. 
            }
        }
    }
}
