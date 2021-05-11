using System;
using System.IO;

namespace OpenGL_GameEngine.BeEngine2D
{
    class Log
    {
        public static void PrintInfo(object Value)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" [INFO] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Value + "\n");

            try
            {
                File.AppendAllText(@"log.txt", DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond + " [INFO] " + Value + "\n");
            }
            catch (Exception ex)
            {
                PrintError("Can't write to log file - " + ex);
            }
        }

        public static void PrintWarning(object Value)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" [WARNING] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Value + "\n");

            try
            {
                File.AppendAllText(@"log.txt", DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond + " [WARNING] " + Value + "\n");
            }
            catch (Exception ex)
            {
                PrintError("Can't write to log file - " + ex);
            }
        }

        public static void PrintError(object Value)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" [ERROR] " + Value + "\n");
            Console.ForegroundColor = ConsoleColor.White;

            try
            {
                File.AppendAllText(@"log.txt", DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond + " [ERROR] " + Value + "\n");
            }
            catch (Exception ex)
            {
                PrintError("Can't write to log file - " + ex);
            }
        }
    }
}
