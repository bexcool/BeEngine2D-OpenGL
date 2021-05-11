using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_GameEngine.BeEngine2D
{
    class Log
    {
        public static void PrintInfo(string text)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" [INFO] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text + "\n");

            try
            {
                File.AppendAllText(@"log.txt", DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond + " [INFO] " + text + "\n");
            }
            catch (Exception ex)
            {
                PrintError("Can't write to log file - " + ex);
            }
        }

        public static void PrintWarning(string text)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" [WARNING] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text + "\n");

            try
            {
                File.AppendAllText(@"log.txt", DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond + " [WARNING] " + text + "\n");
            }
            catch (Exception ex)
            {
                PrintError("Can't write to log file - " + ex);
            }
        }

        public static void PrintError(string text)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" [ERROR] " + text + "\n");
            Console.ForegroundColor = ConsoleColor.White;

            try
            {
                File.AppendAllText(@"log.txt", DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond + " [ERROR] " + text + "\n");
            }
            catch (Exception ex)
            {
                PrintError("Can't write to log file - " + ex);
            }
        }
    }
}
