using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyGriffin
{
    class GamePlay
    {
        public static int playFieldWidth;
        //Setting window size
        public static void WindowsSize(string ConfigFile)
        {
            StreamReader ConfigReader = null;
            //We try to open and read the config file
            try
            {
                ConfigReader = new StreamReader(ConfigFile);
            }
            catch (Exception)
            {
                /*
                 * If there isn't such a file we create one and fill
                 * it with default values.
                 */
                StreamWriter ConfigWriter = File.CreateText(ConfigFile);
                ConfigWriter.WriteLine("playFieldWidth, 12");
                ConfigWriter.WriteLine("height, 25");
                ConfigWriter.WriteLine("width, 30");
                ConfigWriter.Close();
                ConfigReader = new StreamReader(ConfigFile);
            }
            /*
             * We read the lines in the file and split them.
             * By doing so we can easly get the values we need
             * as they are stored at the second index of the string array.
             */
            string[] ConfigString = ConfigReader.ReadLine().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            playFieldWidth = Int32.Parse(ConfigString[1]); ;

            ConfigString = ConfigReader.ReadLine().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Console.BufferHeight = Console.WindowHeight = Int32.Parse(ConfigString[1]);

            ConfigString = ConfigReader.ReadLine().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Console.BufferWidth = Console.WindowWidth = Int32.Parse(ConfigString[1]);
            
            ConfigReader.Close();
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
