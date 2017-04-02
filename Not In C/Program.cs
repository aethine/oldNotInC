using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Not_In_C
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "NIC Interpreter 5.0";
                Console.WriteLine("Enter path to read");
                Interpreter.Run(Console.ReadLine());
            } catch(Exception e)
            {
                Console.WriteLine("An exception occurred! Details:\n\n{0}\n\nPlease contact the creator at twitter.com/aethine with a paste or screenshot of this message.", e);
                while(true) Console.ReadKey(true);
            }
        }
    }
}
