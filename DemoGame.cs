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
            ShowFPS = true;
            new Block(new Vector2(200, 200), new Vector2(100, 10), Color.Aqua, CollisionType.Overlap);

            new Block(new Vector2(0, 0), new Vector2(100, 100), Color.White, CollisionType.BlockAll);

            new Block(new Vector2(60, 60), new Vector2(100, 30), Color.Red, CollisionType.BlockAll);
        }

        protected override void Update()
        {
            if (KeyPressed(Keys.D))
            {
                CameraPosition = new Vector2(CameraPosition.X - GameTime.CalculateSpeed(Player.MoveSpeed), CameraPosition.Y);
                Player.Position = new Vector2(Player.Position.X + GameTime.CalculateSpeed(Player.MoveSpeed), Player.Position.Y);
                
                if (Player.IsColliding())
                {
                    Player.Position = new Vector2(Player.Position.X - GameTime.CalculateSpeed(Player.MoveSpeed), Player.Position.Y);
                    CameraPosition = new Vector2(CameraPosition.X + GameTime.CalculateSpeed(Player.MoveSpeed), CameraPosition.Y);
                }
            }

            if (KeyPressed(Keys.A))
            {
                CameraPosition = new Vector2(CameraPosition.X + GameTime.CalculateSpeed(Player.MoveSpeed), CameraPosition.Y);
                Player.Position = new Vector2(Player.Position.X - GameTime.CalculateSpeed(Player.MoveSpeed), Player.Position.Y);

                if (Player.IsColliding())
                {
                    CameraPosition = new Vector2(CameraPosition.X - GameTime.CalculateSpeed(Player.MoveSpeed), CameraPosition.Y);
                    Player.Position = new Vector2(Player.Position.X + GameTime.CalculateSpeed(Player.MoveSpeed), Player.Position.Y);
                }
            }

            if (KeyPressed(Keys.S))
            {
                CameraPosition = new Vector2(CameraPosition.X, CameraPosition.Y - GameTime.CalculateSpeed(Player.MoveSpeed));
                Player.Position = new Vector2(Player.Position.X, Player.Position.Y + GameTime.CalculateSpeed(Player.MoveSpeed));

                if (Player.IsColliding())
                {
                    CameraPosition = new Vector2(CameraPosition.X, CameraPosition.Y + GameTime.CalculateSpeed(Player.MoveSpeed));
                    Player.Position = new Vector2(Player.Position.X, Player.Position.Y - GameTime.CalculateSpeed(Player.MoveSpeed));
                }
            }

            if (KeyPressed(Keys.W))
            {
                CameraPosition = new Vector2(CameraPosition.X, CameraPosition.Y + GameTime.CalculateSpeed(Player.MoveSpeed));
                Player.Position = new Vector2(Player.Position.X, Player.Position.Y - GameTime.CalculateSpeed(Player.MoveSpeed));

                if (Player.IsColliding())
                {
                    CameraPosition = new Vector2(CameraPosition.X, CameraPosition.Y - GameTime.CalculateSpeed(Player.MoveSpeed));
                    Player.Position = new Vector2(Player.Position.X, Player.Position.Y + GameTime.CalculateSpeed(Player.MoveSpeed));
                }
            }

            if (MouseButtonPressed(MouseButton.Left))
            {
                Vector2 MousePos = GetNormalizedMousePosition();

                if (GetBlockByPosition(MousePos.X, MousePos.Y) != null)
                {
                    GetBlockByPosition(MousePos.X, MousePos.Y).DestroySelf();
                }

                /*
                if (GetEntityByPosition(MousePos.X, MousePos.Y) != null)
                {
                    GetEntityByPosition(MousePos.X, MousePos.Y).DestroySelf();
                }*/
            }

            if (MouseButtonPressed(MouseButton.Right))
            {
                Vector2 MousePos = GetNormalizedMousePosition();

                if (GetBlockByPosition(MousePos.X, MousePos.Y) == null)
                {
                    new Block(new Vector2(MousePos.X - 10, MousePos.Y - 10), new Vector2(20, 20), CollisionType.BlockAll);
                }
            }
        }
    }
}
