using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Using engine
using OpenGL_GameEngine.BeEngine2D;
using OpenGL_GameEngine.BeEngine2D.GameLoop;
using OpenGL_GameEngine.BeEngine2D.Rendering.Display;
using static OpenGL_GameEngine.BeEngine2D.GL;
using GLFW;
using System.Numerics;
using Keys = GLFW.Keys;

namespace OpenGL_GameEngine.BeEngine2D
{
    class DemoGame : BeEngine2D
    {
        public DemoGame() : base(new Vector2(800, 600), "BeEngine2D - Demo") { }

        float PlayerSpeed = 1f;

        protected override void Initialize()
        {

        }

        protected override void KeyInput(Window Window, Keys Key, int ScanCode, InputState State, ModifierKeys Mods)
        {

        }

        protected override void Update()
        {
            if (Glfw.GetKey(DisplayManager.Window, Keys.A) == InputState.Press)
            {
                CameraPosition = new Vector2(CameraPosition.X - PlayerSpeed, CameraPosition.Y);
            }

            if (Glfw.GetKey(DisplayManager.Window, Keys.D) == InputState.Press)
            {
                CameraPosition = new Vector2(CameraPosition.X + PlayerSpeed, CameraPosition.Y);
            }

            //Log.PrintInfo(1 / GameTime.DeltaTime);

            if (Glfw.GetKey(DisplayManager.Window, Keys.W) == InputState.Press)
            {
                CameraPosition = new Vector2(CameraPosition.X, CameraPosition.Y - PlayerSpeed);
            }

            if (Glfw.GetKey(DisplayManager.Window, Keys.S) == InputState.Press)
            {
                CameraPosition = new Vector2(CameraPosition.X, CameraPosition.Y + PlayerSpeed);
            }
        }
    }
}
