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
using OpenGL_GameEngine.BeEngine2D.Utilities;
using GLFW;
using System.Numerics;
using Keys = GLFW.Keys;
using System.Drawing;

namespace OpenGL_GameEngine.BeEngine2D
{
    class DemoGame : BeEngine2D
    {
        public DemoGame() : base(new Vector2(800, 600), "BeEngine2D - Demo") { }

        Entity Player = new Entity(new Vector2(325, 275), new Vector2(50, 50), Color.Lime, CollisionType.BlockAll, 15f, "player");

        List<Entity> Bullets = new List<Entity>();
        List<Vector2> BulletsDirections = new List<Vector2>();

        //Entity Player = new Entity(new Vector2(-25, -25), new Vector2(50, 50), @"F:\Obrázky\block.png", CollisionType.BlockAll, 15f);

        // Game map (you can make own map creator, but I have used this array map)
        private string[,] Map =
        {
            {"a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", },
            {"a", "b", "a", "b", "b", "b", "b", "b", "b", "b", "a", },
            {"a", "b", "a", "b", "b", "b", "b", "b", "b", "b", "a", },
            {"a", "b", "a", "b", "b", "b", "b", "b", "b", "b", "a", },
            {"a", "b", "b", "b", "b", "b", "b", "b", "b", "b", "a", },
            {"a", "b", "b", "b", "b", "b", "a", "b", "b", "b", "a", },
            {"a", "b", "b", "b", "b", "b", "a", "b", "b", "b", "a", },
            {"a", "b", "b", "b", "b", "b", "a", "b", "b", "b", "a", },
            {"a", "a", "a", "a", "a", "a", "a", "b", "b", "b", "a", },
        };

        protected override void Initialize()
        {
            ShowFPS = true;
            SaveLog = false;

            CameraFocusedObjectTag = "player";

            new Block(new Vector2(0, 0), new Vector2(100, 10), @"F:\Obrázky\block.png", CollisionType.Overlap);

            new Block(new Vector2(0, 0), new Vector2(100, 100), Color.White, CollisionType.BlockAll);

            for (int i = 1; i <= 20; i++)
            {
                new Block(TraceLine.ShootLineTrace(new Vector2(0, 0), i * 18, 500), new Vector2(100, 100), Color.Green, CollisionType.None);
            }

            new Polygon(new Vector2[] { new Vector2(-40, 0), new Vector2(-200, -20), new Vector2(-180, 100), new Vector2(-100, 150) }, Color.Green);

            new Polygon(new Vector2[] { new Vector2(-40, 0), new Vector2(-200, -50), new Vector2(-200, -51) }, Color.Red);

            new Block(new Vector2(600, 60), new Vector2(100, 30), Color.Khaki, CollisionType.BlockAll);

            new Light(new Vector2(800, 100), 100, 10);

            new SoundPlayer(new Vector2(300, 400), @"F:\HUDBA\Rady - Broke.mp3", 0.2f , 400, true);
            new Block(new Vector2(295, 395), new Vector2(10, 10), Color.Black, CollisionType.Overlap);

            new SoundPlayer(new Vector2(-100, 400), @"F:\HUDBA\Ment - 128.mp3", 0.3f, 400, true);
            new Block(new Vector2(-105, 395), new Vector2(10, 10), Color.Black, CollisionType.Overlap);

            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == "a") new Block(new Vector2(j * 30, i * 30), new Vector2(30, 30), Color.White, CollisionType.BlockAll, "wall");
                    //if (Map[i, j] == "b") new Block(new Vector2(j * 30, i * 30), new Vector2(30, 30), Color.Black, CollisionType.None);
                }
            }
        }

        protected override void Update()
        {
            Vector2 MousePos = GetNormalizedMousePosition();

            if (KeyPressed(Keys.D))
            {
                //CameraPosition = new Vector2(CameraPosition.X - GameTime.CalculateSpeed(Player.MoveSpeed), CameraPosition.Y);
                Player.Position = new Vector2(Player.Position.X + GameTime.CalculateSpeed(Player.MoveSpeed), Player.Position.Y);
                
                if (Player.IsColliding())
                {
                    Player.Position = new Vector2(Player.Position.X - GameTime.CalculateSpeed(Player.MoveSpeed), Player.Position.Y);
                }
            }

            if (KeyPressed(Keys.A))
            {
                Player.Position = new Vector2(Player.Position.X - GameTime.CalculateSpeed(Player.MoveSpeed), Player.Position.Y);

                if (Player.IsColliding())
                {
                    Player.Position = new Vector2(Player.Position.X + GameTime.CalculateSpeed(Player.MoveSpeed), Player.Position.Y);
                }
            }

            if (KeyPressed(Keys.S))
            {
                Player.Position = new Vector2(Player.Position.X, Player.Position.Y + GameTime.CalculateSpeed(Player.MoveSpeed));

                if (Player.IsColliding())
                {
                    Player.Position = new Vector2(Player.Position.X, Player.Position.Y - GameTime.CalculateSpeed(Player.MoveSpeed));
                }
            }

            if (KeyPressed(Keys.W))
            {
                Player.Position = new Vector2(Player.Position.X, Player.Position.Y - GameTime.CalculateSpeed(Player.MoveSpeed));

                if (Player.IsColliding())
                {
                    Player.Position = new Vector2(Player.Position.X, Player.Position.Y + GameTime.CalculateSpeed(Player.MoveSpeed));
                }
            }

            if (KeyPressed(Keys.LeftShift))
            {
                Player.MoveSpeed = 25f;
            }

            if (KeyReleased(Keys.LeftShift))
            {
                Player.MoveSpeed = 15f;
            }
            
            if (MouseButtonPressed(MouseButton.Left))
            {
                if (GetBlockByPosition(MousePos.X, MousePos.Y) != null)
                {
                    if (GetBlockByPosition(MousePos.X, MousePos.Y).Tag == "destroyable") GetBlockByPosition(MousePos.X, MousePos.Y).DestroySelf();
                }

                /*
                if (GetEntityByPosition(MousePos.X, MousePos.Y) != null)
                {
                    GetEntityByPosition(MousePos.X, MousePos.Y).DestroySelf();
                }
                
                Bullets.Add(new Entity(new Vector2(Player.Position.X + Player.Scale.X / 2, Player.Position.Y - 10 - Player.Scale.Y / 2), new Vector2(10, 10), CollisionType.Overlap, 35));

                float Distance = (float)Math.Sqrt(Math.Pow((Player.Position.X + Player.Scale.X / 2) - GetNormalizedMousePosition().X, 2) + Math.Pow((Player.Position.Y - Player.Scale.Y / 2) - GetNormalizedMousePosition().Y, 2));

                Vector2 Direction = new Vector2((Player.Position.X + Player.Scale.X / 2) / Distance, (Player.Position.Y - Player.Scale.Y / 2) / Distance);

                Log.PrintError(Distance);
                Log.PrintError(Direction);

                BulletsDirections.Add(Direction);*/
            }

            if (MouseButtonPressed(MouseButton.Right))
            {
                if (GetBlockByPosition(MousePos.X, MousePos.Y) == null)
                {
                    Block block = new Block(new Vector2(MousePos.X - 10, MousePos.Y - 10), new Vector2(20, 20), CollisionType.BlockAll, "destroyable");
                    Log.PrintWarning(block.Position);
                }
            }

            for (int i = 0; i < Bullets.Count; i++)
            {
                Entity bullet = Bullets[i];

                bullet.Position = new Vector2(bullet.Position.X + BulletsDirections[i].X, bullet.Position.Y + BulletsDirections[i].Y);

                if (bullet.IsCollidingByTag("wall"))
                {
                    bullet.DestroySelf();
                    Bullets.Remove(bullet);
                }
            }
        }
    }
}
