using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyGriffin
{
    class GamePlay
    {
        //Setting window size
        public static void WindowsSize(int fieldWidth, int windowHeight, int windowWidth)
        {
            Console.BufferHeight = Console.WindowHeight = windowHeight;
            Console.BufferWidth = Console.WindowWidth = windowWidth;
        }

        // Method for printing a string on the console.
        public static void PrintStringOnPosition(int x, int y, string c, ConsoleColor color = ConsoleColor.Gray)
        {

            Console.SetCursorPosition(x, y); // Coordinates of cursor position.
            Console.ForegroundColor = color;
            Console.Write(c);

        }

        //Method for returning random value
        public static int ReturnRandomValue(int minRange, int maxRange)
        {
            int randomVal = 0;
            Random randomGenerator = new Random(); // Gives a random number in given range /
            randomVal = randomGenerator.Next(minRange, maxRange);
            return randomVal;
        }

        //Method for printing information about score and remaining lives
        public static void PrintInfo(long score, int lives, long topScore, string userNameBestScore, string userName)
        {
            PrintStringOnPosition(2, 13, "Player: " + userName);
            PrintStringOnPosition(2, 14, "Score: " + score);
            PrintStringOnPosition(2, 15, "Lives: " + lives);
            PrintStringOnPosition(2, 16, "Top score: " + topScore + " by: " + userNameBestScore);
            PrintStringOnPosition(7, 20, "FLAPPY GRIFFIN", ConsoleColor.Magenta);
        }
    }
}
