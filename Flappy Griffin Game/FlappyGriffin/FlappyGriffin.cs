﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;


class testGriffin
{

    struct Object // Creating object with paramaters for coordinates, symbol and color.
    {
        public int x;
        public int y;
        public ConsoleColor color;
        public string c;
    }
    struct Obstacle
    {
        public int x;
        public int y1;
        public int y2;
        public int y3;
        public int y4;
        public int y5;
        public int y6;
        public string c;

    }

    // Method for printing a string on the console.
    static void PrintStringOnPosition(int x, int y, string c, ConsoleColor color = ConsoleColor.Gray)
    {

        Console.SetCursorPosition(x, y); // Coordinates of cursor position.
        Console.ForegroundColor = color;
        Console.Write(c);

    }
    static void PrintObstacle(int x, int y1, int y2, int y3, int y4, int y5, int y6, string c,
        ConsoleColor color = ConsoleColor.Green)
    // Creating method for printing the obstacles.
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



    static void Main()
    {
        // Setting window size and removing scrollbars.

        int playFieldWidth = SettingWindowsSize();

        // Creating the Griffin.
        Object griffin = new Object();
        griffin.c = "G";                  // Griffin symbol.
        griffin.color = ConsoleColor.White;  // color.
        griffin.x = 10;                      // Starting coordinates of the griffin.   
        griffin.y = playFieldWidth / 2;

        Random randomGenerator = new Random();   // Gives a random number in given range
        List<Obstacle> Obstacles = new List<Obstacle>();
        int lives = 5;
        long score = 0;

        while (true)
        {
            bool obstacleHitted = false;
            int chance = randomGenerator.Next(0, 3);
            // Random number generates different obstacle.
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
                nextObstacle.c = "#";
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
                nextObstacle.c = "#";
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
                nextObstacle.c = "#";
                Obstacles.Add(nextObstacle);
            }
            // Checking if there is any key pressed.
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
                // Moving the griffin up by pressing space.
                if (keyPressed.Key == ConsoleKey.Spacebar)
                {
                    if (griffin.y - 1 >= 1)
                    {
                        griffin.y -= 1;
                    }
                }

            }
            else //Added case in which if the player is not pressing the key the bird falls down by itself
            {
                if (griffin.y + 1 <= playFieldWidth)
                {
                    if (griffin.y + 2 == playFieldWidth || griffin.y + 1 == playFieldWidth)
                    {

                        Obstacles.Clear();
                        // If we hit an obstacle we start again from starting position.
                        // switching symbol when rock is hitted.
                        PrintStringOnPosition(griffin.x, griffin.y, "GAME OVER", ConsoleColor.Red);
                        Console.Beep(100, 500);                                      // sound when rock is hitted.
                        PrintStringOnPosition(griffin.x - 5, griffin.y - 5, "Press any key to restart the game",
                            ConsoleColor.White);
                        Console.ReadKey();
                        griffin.x = 10;
                        griffin.y = playFieldWidth / 2;
                        lives--;
                        if (lives < 1)                         // Ends the game when you reach 0 lives from falling down.
                        {
                            return;
                        }

                    }
                    else
                    {
                        griffin.y += 2;
                    }

                }
            }
            // Creating new list of moving objects and filling it.
            List<Obstacle> newObstacles = new List<Obstacle>();
            for (int i = 0; i < Obstacles.Count; i++)
            {
                // the same objects but on their new positions.

                Obstacle oldObstacle = Obstacles[i];
                Obstacle newObstacle = new Obstacle();
                newObstacle.x = oldObstacle.x - 1;
                newObstacle.y1 = oldObstacle.y1;
                newObstacle.y2 = oldObstacle.y2;
                newObstacle.y3 = oldObstacle.y3;
                newObstacle.y4 = oldObstacle.y4;
                newObstacle.y5 = oldObstacle.y5;
                newObstacle.y6 = oldObstacle.y6;
                newObstacle.c = oldObstacle.c;
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
                    if (lives == 0)
                    {
                        return;        // If we lose our last life the game is over.
                    }
                    else
                    {
                        lives--;       // If we still have lives we lose one life and the game continues.
                        score = 0;
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
                PrintStringOnPosition(griffin.x, griffin.y, "X", ConsoleColor.Red);
                // sound when rock is hitted.
                Console.Beep(100, 500);
            }

            else   // Printing the griffin.
            {
                PrintStringOnPosition(griffin.x, griffin.y, griffin.c.ToString(), griffin.color);
            }
            foreach (Obstacle obstacle in Obstacles) // Printing the obstacles.
            {
                PrintObstacle(obstacle.x, obstacle.y1, obstacle.y2, obstacle.y3, obstacle.y4, obstacle.y5, obstacle.y6,
                    obstacle.c);
            }

            for (int i = 0; i < Console.WindowWidth; i++) // Drawing the play field borders.
            {
                PrintStringOnPosition(i, playFieldWidth, "-", ConsoleColor.Blue);
                PrintStringOnPosition(i, 0, "-", ConsoleColor.Blue);
            }
            // Printing info.
            PrintStringOnPosition(2, 12, "Score: " + score);
            PrintStringOnPosition(2, 13, "Lives " + lives);
            PrintStringOnPosition(5, 16, "FLAPPY GRIFFIN", ConsoleColor.Magenta);

            Thread.Sleep(300); // Slows down the program so we can see what happens on the screen. We can change the speed. 
        }
    }

    private static int SettingWindowsSize(int fieldwidth, int height, int width)
    {
        int playFieldWidth = 11;
        Console.BufferHeight = Console.WindowHeight = height;
        Console.BufferWidth = Console.WindowWidth = width;
        return playFieldWidth;
    }
}