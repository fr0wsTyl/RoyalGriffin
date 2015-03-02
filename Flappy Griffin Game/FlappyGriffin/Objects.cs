using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyGriffin
{
    class Objects
    {
        // Creating object with paramaters for coordinates, symbol and color.
        public int x;
        public int y;
        public ConsoleColor color;
        public string c;

        // Creating the Griffin.
        public static Objects CreateGriffin(string griffin, int x, int y, ConsoleColor color = ConsoleColor.White)
        {
            Objects griffinObj = new Objects();
            griffinObj.c = griffin;                  // Griffin symbol.
            griffinObj.color = ConsoleColor.White;  // color.
            griffinObj.x = x;                      // Starting coordinates of the griffin.   
            griffinObj.y = y;
            return griffinObj;
        }

        //Method for checking if console key is available
        public static Objects CheckGriffin(Objects griffin, out ConsoleKeyInfo keyPressed)
        {
            keyPressed = Console.ReadKey(true);
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
            return griffin;
        }

    }
}
