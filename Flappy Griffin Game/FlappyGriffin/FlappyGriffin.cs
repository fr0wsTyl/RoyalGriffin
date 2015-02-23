using System;
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
        public char c;
    }
    // Method for printing a symbol on the console.
    static void PrintOnPosition(int x, int y, char c, ConsoleColor color = ConsoleColor.Gray)
    {
        Console.SetCursorPosition(x, y); // Coordinates of cursor position.
        Console.ForegroundColor = color;
        Console.Write(c);
    }
    // Another method for printing strings on given position.
    static void PrintStringOnPosition(int x, int y, string str, ConsoleColor color = ConsoleColor.White)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(str);
    }


    static void Main()
    {
        // Setting window size and removing scrollbars.
        int playFieldWidth = 10;
        Console.BufferHeight = Console.WindowHeight = 18;
        Console.BufferWidth = Console.WindowWidth = 50;

        // Creating the Griffin.
        Object griffin = new Object();
        griffin.c = 'G';                     // Griffin symbol.
        griffin.color = ConsoleColor.White;  // color.
        griffin.x = 20;                      // Starting coordinates of the griffin.   
        griffin.y = playFieldWidth / 2;

        Random randomGenerator = new Random();   // Gives a random number in given range
        List<Object> rocks = new List<Object>(); // Creating a list of moving objects on the field.
        int lives = 5;
        long score = 0;


        while (true)
        {
            bool rockHitted = false;
            bool lifeHitted = false;
            bool moneyHitted = false;
            {
                int chance = randomGenerator.Next(0, 100);
                if (chance < 1)                            // In about 1% of the time we create object "life".
                {
                    Object nextrock = new Object();
                    nextrock.color = ConsoleColor.Yellow;
                    nextrock.c = 'L';                      // Symbol.
                    nextrock.x = Console.WindowWidth;      // Coordinates: Always appears in right end
                    nextrock.y = randomGenerator.Next(1, playFieldWidth); // and random height 
                    rocks.Add(nextrock);                   // Adding the object to the list of miving objects.

                }

                else if (chance < 10)                      // In 10% of time we create "money" object.
                {
                    Object nextrock = new Object();
                    nextrock.color = ConsoleColor.Green;
                    nextrock.c = '$';                      // Symbol.
                    nextrock.x = Console.WindowWidth;      // Coordinates.
                    nextrock.y = randomGenerator.Next(1, playFieldWidth);
                    rocks.Add(nextrock);                   // Adding "money" to the list of moving objects.
                }
                else
                {                                  // In the rest of the time there are only rocks moving towards the griffin
                    Object nextrock = new Object();
                    nextrock.color = ConsoleColor.DarkGray;
                    nextrock.c = '^';
                    nextrock.x = Console.WindowWidth;
                    nextrock.y = randomGenerator.Next(1, playFieldWidth);
                    rocks.Add(nextrock);
                }
            }

            if (Console.KeyAvailable)                       // Checking if there is any key pressed.
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
                if (keyPressed.Key == ConsoleKey.UpArrow)   // Moving the griffin up.
                {
                    if (griffin.y - 1 >= 1)
                    {
                        griffin.y -= 1;
                    }

                }
                else if (keyPressed.Key == ConsoleKey.DownArrow)   // Moving the griffin down.
                {
                    if (griffin.y + 1 < playFieldWidth)
                    {
                        griffin.y += 1;
                    }
                }
            }
            List<Object> nextrocks = new List<Object>();     // Creating new list of moving objects and filling it with
            for (int i = 0; i < rocks.Count; i++)            // the same objects but on their new positions.   
            {
                Object oldrock = rocks[i];
                Object nextrock = new Object();
                nextrock.x = oldrock.x - 1;
                nextrock.y = oldrock.y;
                nextrock.c = oldrock.c;
                nextrock.color = oldrock.color;

                // Checking if the griffin hits any moving objects.
                if (nextrock.c == '^' && nextrock.x == griffin.x && nextrock.y == griffin.y)
                {
                    rockHitted = true; // If we hit a rock we lose a life.
                    if (lives < 1)
                    {
                        return;        // If we lose our last life the game is over.
                    }
                    else
                    {
                        lives--;       // If we still have lives we lose one life and the game continues.
                        score = 0;
                    }
                }
                if (nextrock.c == '$' && nextrock.x == griffin.x && nextrock.y == griffin.y)
                {
                    moneyHitted = true;   // If we hit money the score increases.
                    score += 10;
                }

                if (nextrock.c == 'L' && nextrock.x == griffin.x && nextrock.y == griffin.y)
                {
                    lifeHitted = true;     // If we hit life we get one life bonus.
                    lives++;
                }
                if (nextrock.x > 0)
                {
                    nextrocks.Add(nextrock);// When an object disappears from the screen a new one appears in starting position
                }
            }
            rocks = nextrocks;         // Current list of objects saves the new objects and their new positions.
            Console.Clear();           // Clearing the console from the old objects.
            if (rockHitted)
            {
                rocks.Clear();         // If we hit a rock we start again from starting position.
                PrintOnPosition(griffin.x, griffin.y, 'X', ConsoleColor.Red); // switching symbol when rock is hitted.
                Console.Beep(500, 1000);                                      // sound when rock is hitted.
            }
            else if (moneyHitted)
            {
                PrintOnPosition(griffin.x, griffin.y, 'G', ConsoleColor.Green);

            }
            else if (lifeHitted)
            {
                PrintOnPosition(griffin.x, griffin.y, 'G', ConsoleColor.Yellow);
                Console.Beep(1200, 200);
            }
            else
            {
                PrintOnPosition(griffin.x, griffin.y, griffin.c, griffin.color);
            }
            foreach (Object rock in rocks) // Printing the objects from the list of moving objects.
            {
                PrintOnPosition(rock.x, rock.y, rock.c, rock.color);
            }
            for (int i = 0; i < Console.WindowWidth; i++) // Drowing the play field borders.
            {
                PrintOnPosition(i, playFieldWidth, '-', ConsoleColor.Green);
                PrintOnPosition(i, 0, '-', ConsoleColor.Green);
            }
            // Printing info.
            PrintStringOnPosition(2, 12, "Score: " + score);
            PrintStringOnPosition(2, 13, "Lives " + lives);
            PrintStringOnPosition(15, 12, "FLAPPY GRIFFIN", ConsoleColor.Magenta);

            Thread.Sleep(250); // Slows down the program so we can see what happens on the screen. We can change the speed. 
        }
    }
}

