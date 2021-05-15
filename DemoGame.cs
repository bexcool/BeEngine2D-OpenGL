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
using System.Drawing;

namespace OpenGL_GameEngine.BeEngine2D
{
    class DemoGame : BeEngine2D
    {
        public DemoGame() : base(new Vector2(800, 600), "BeEngine2D - Demo") { }

        Entity Player = new Entity(new Vector2(375, 275), new Vector2(50, 50), Color.Green, CollisionType.BlockAll, 10f);

        protected override void Initialize()
        {

        }

        protected override void Update()
        {
            //Log.PrintWarning(GameTime.CalculateSpeed(PlayerSpeed));

            //Nastavit, že kamera bude locknutá na player pos

            if (Glfw.GetKey(DisplayManager.Window, Keys.D) == InputState.Press)
            {
                CameraPosition = new Vector2(CameraPosition.X - GameTime.CalculateSpeed(Player.MoveSpeed), CameraPosition.Y);
                Player.Position = new Vector2(Player.Position.X + GameTime.CalculateSpeed(Player.MoveSpeed), Player.Position.Y);
                
                if (Player.IsColliding())
                {
                    Player.Position = new Vector2(Player.Position.X - GameTime.CalculateSpeed(Player.MoveSpeed), Player.Position.Y);
                    CameraPosition = new Vector2(CameraPosition.X + GameTime.CalculateSpeed(Player.MoveSpeed), CameraPosition.Y);
                }
            }

            if (Glfw.GetKey(DisplayManager.Window, Keys.A) == InputState.Press)
            {
                CameraPosition = new Vector2(CameraPosition.X + GameTime.CalculateSpeed(Player.MoveSpeed), CameraPosition.Y);
                Player.Position = new Vector2(Player.Position.X - GameTime.CalculateSpeed(Player.MoveSpeed), Player.Position.Y);

                if (Player.IsColliding())
                {
                    CameraPosition = new Vector2(CameraPosition.X - GameTime.CalculateSpeed(Player.MoveSpeed), CameraPosition.Y);
                    Player.Position = new Vector2(Player.Position.X + GameTime.CalculateSpeed(Player.MoveSpeed), Player.Position.Y);
                }
            }

            //Log.PrintInfo(1 / GameTime.DeltaTime);

            if (Glfw.GetKey(DisplayManager.Window, Keys.S) == InputState.Press)
            {
                CameraPosition = new Vector2(CameraPosition.X, CameraPosition.Y - GameTime.CalculateSpeed(Player.MoveSpeed));
                Player.Position = new Vector2(Player.Position.X, Player.Position.Y + GameTime.CalculateSpeed(Player.MoveSpeed));

                if (Player.IsColliding())
                {
                    CameraPosition = new Vector2(CameraPosition.X, CameraPosition.Y + GameTime.CalculateSpeed(Player.MoveSpeed));
                    Player.Position = new Vector2(Player.Position.X, Player.Position.Y - GameTime.CalculateSpeed(Player.MoveSpeed));
                }
            }

            if (Glfw.GetKey(DisplayManager.Window, Keys.W) == InputState.Press)
            {
                CameraPosition = new Vector2(CameraPosition.X, CameraPosition.Y + GameTime.CalculateSpeed(Player.MoveSpeed));
                Player.Position = new Vector2(Player.Position.X, Player.Position.Y - GameTime.CalculateSpeed(Player.MoveSpeed));

                if (Player.IsColliding())
                {
                    CameraPosition = new Vector2(CameraPosition.X, CameraPosition.Y - GameTime.CalculateSpeed(Player.MoveSpeed));
                    Player.Position = new Vector2(Player.Position.X, Player.Position.Y + GameTime.CalculateSpeed(Player.MoveSpeed));
                }
            }

            //Log.PrintError(Player.IsColliding());
        }
    }
}
