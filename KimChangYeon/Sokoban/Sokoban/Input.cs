using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class Input
    {
        public static ConsoleKey key;
        public static ConsoleKey InputKey()
        {
          key = Console.ReadKey().Key;

          return key;
        }
        

    }
}
