using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDBI
{
   public class Util
    {
        //----------------------------------------------------------------------------------
        public static void pause()
        {
            Console.WriteLine("pausing...");
            Console.ReadKey();
        }//void pause()
        public static void pause(String msg)
        {
            Console.WriteLine(msg);
            Console.WriteLine("pausing...");
            Console.ReadKey();
        }//void pause()
    }
}
