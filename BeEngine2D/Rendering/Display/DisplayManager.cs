using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using GLFW;
using static OpenGL_GameEngine.BeEngine2D.GL;
using System.Drawing;

namespace OpenGL_GameEngine.BeEngine2D.Rendering.Display
{
    class DisplayManager
    {
        public static Window Window { get; set; }
        public static Vector2 WindowSize { get; set; }

        public static void CreateWindow(int Width, int Height, string Title)
        {
            WindowSize = new Vector2(Width, Height);

            Glfw.Init();
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);

            Glfw.WindowHint(Hint.Focused, true);
            Glfw.WindowHint(Hint.Resizable, false);

            Window = Glfw.CreateWindow(Width, Height, Title, Monitor.None, Window.None);

            if (Window == Window.None)
            {
                return;
            }

            Rectangle Screen = Glfw.PrimaryMonitor.WorkArea;
            int X_WindowPos = (Screen.Width - Width) / 2;
            int Y_WindowPos = (Screen.Height - Height) / 2;

            Glfw.SetWindowPosition(Window, X_WindowPos, Y_WindowPos);

            Glfw.MakeContextCurrent(Window);
            Import(Glfw.GetProcAddress);

            glViewport(0, 0, Width, Height);
            Glfw.SwapInterval(0); // 0 VSync is off, 1 VSync is on 
        }

        public static void CloseWindow()
        {
            Glfw.Terminate();
        }

        public static void SetWindowsSize(int Width, int Height)
        {
            
        }

        public static Vector2 ConvertPixelsToGL (double PosX, double PosY)
        {
            return new Vector2((float)(1 - (WindowSize.X - PosX) / WindowSize.X * 2), (float)(1 - (WindowSize.Y - PosY) / WindowSize.Y * 2));
        }

        public static double ConvertPixelsToGL_X (double PosX)
        {
            return (float)(1 - (WindowSize.X - PosX) / WindowSize.X * 2);
        }

        public static double ConvertPixelsToGL_Y (double PosY)
        {
            return (float)(1 - (WindowSize.Y - PosY) / WindowSize.Y * 2);
        }
    }
}
