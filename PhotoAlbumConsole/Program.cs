using PhotoAlbumLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbumConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var commandInterpreter = new CommandInterpreter();

            Console.WriteLine("Welcome to the photo album browser.\nIf this is your first time using the application, please type 'help' at the prompt below.\nWritten by Joseph Hicks(c)");

            while(true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();

                if(input.ToLower().StartsWith("exit"))
                {
                    break;
                }

                var output = commandInterpreter.Evaluate(input);

                Console.WriteLine($"\n{output}\n");
            }
        }
    }
}
