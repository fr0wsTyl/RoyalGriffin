using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace FlappyGriffin
{
    class GamePlay
    {
        /*
         * An array to hold the game setting values we are going 
         * to read from the config file at the WindowsSize().
         */
        static int[] GameSettings = new int[5];
        //Variable that will store the with of the game field.
        public static int playFieldWidth;
        //Variable that will store the speed of the game.
        public static int speed;
        //Variable that will store the amount of lives in the game.
        public static int lives;
        //Variable that will store the current score during playing.
        public static long score = 0;
        //Variable to hold the current user name.
        public static string userName;
        //The user name with the best score.
        public static string userNameBestScore;
        //Variables that are related to the score.
        private static string[] ScoresInfo;
        public static string topScoreString;
        public static long topScore;

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
            playFieldWidth = GameSettings[0];
            Console.BufferHeight = Console.WindowHeight = GameSettings[1];
            Console.BufferWidth = Console.WindowWidth = GameSettings[2];
            speed = GameSettings[3];
            lives = GameSettings[4];
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
        /*
         * A method that reads the top score of the player and returns
         * both the name and the score as a string array.
         */
        public static string[] ReadScore()
        {
            //string[] ScoreInfo = new string[2];
            StreamReader ScoreReader = null;
            //We try to open the TopScores file
            try
            {
                ScoreReader = new StreamReader(FlappyGriffin.TOP_SCORES_FILE);
            }
            catch (Exception)
            {
                /*
                 * If there is no such file we create a new one and add
                 * some default value.
                 */
                StreamWriter ScoreWriter = File.CreateText(FlappyGriffin.TOP_SCORES_FILE);
                ScoreWriter.WriteLine("Unknown, 0");
                ScoreWriter.Close();
                ScoreReader = new StreamReader(FlappyGriffin.TOP_SCORES_FILE);
            }
            string[] ScoreInfo = ScoreReader.ReadLine().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            ScoreReader.Close();
            return ScoreInfo;
        }
        public static void WriteScore(string ValueToWrite)
        {
            using (StreamWriter writer = new StreamWriter("..//..//Scores//TopScore.txt"))
            {
                writer.WriteLine(ValueToWrite);
            }
        }
        public static void SetPlayerName()
        {
            Console.WriteLine("Enter your name: ");
            userName = Console.ReadLine();
        }
        public static void InitializeScores()
        {
            ScoresInfo = GamePlay.ReadScore();
            userNameBestScore = ScoresInfo[0];
            topScoreString = ScoresInfo[1];
            //We have an exception in case of a invalid value 
            //written into the file.
            try
            {
                topScore = long.Parse(GamePlay.topScoreString);
            }
            catch (Exception)
            {
                topScore = 0;
            }

        }
        //Add sounds
        public static void PlayMusic(string whichSound)
        {
            SoundPlayer player = new SoundPlayer();
            switch (whichSound)
            {
                case "start":
                    player = new SoundPlayer(@"..\..\Music\mk1-00368.wav");
                    break;
                case "crash":
                    player = new SoundPlayer(@"..\..\Music\car crashing - sound effect_mp3cut.foxcom.su_.wav");
                    break;
                case "up":
                    player = new SoundPlayer(@"..\..\Music\Aii.wav");
                    break;
                case "aplause":
                    player = new SoundPlayer(@"..\..\Music\mk1-00320.wav");
                    break;
            }
            player.PlaySync();
        }
    }
}
