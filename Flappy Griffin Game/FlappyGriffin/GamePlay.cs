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
        /*
         * An array to hold the game setting values we are going 
         * to read from the config file at the WindowsSize().
         */
        static int[] GameSettings = new int[4];
        //Variable that will store the with of the game field.
        public static int playFieldWidth;
        //Variable that will store the speed of the game.
        public static int speed;

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
                ConfigWriter.WriteLine("speed, 400");
                ConfigWriter.Close();
                ConfigReader = new StreamReader(ConfigFile);
            }
            /*
             * We read the lines in the file and split them.
             * By doing so we can easly get the values we need
             * as they are stored at the second index of the string array.
             */
            string[] ConfigString;
            for (int i = 0; i < GameSettings.Length; i++)
            {
                ConfigString = ConfigReader.ReadLine().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                GameSettings[i] = Int32.Parse(ConfigString[1]);
            }
            /*
             * We use the values we got from the Config file to initialize the game field.
             * We are using mediator values playFieldWidth and speed as they are more 
             * intuitive to use rather than addressing the GameSettings array.
             */
            playFieldWidth =  GameSettings[0];
            Console.BufferHeight = Console.WindowHeight = GameSettings[1];
            Console.BufferWidth = Console.WindowWidth = GameSettings[2];
            speed = GameSettings[3];
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
