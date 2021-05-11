using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;
using Exception = System.Exception;
using System.Numerics;

// Using engine
using GLFW;
using OpenGL_GameEngine.BeEngine2D.Rendering.Display;
using OpenGL_GameEngine.BeEngine2D.GameLoop;
using static OpenGL_GameEngine.BeEngine2D.GL;
using OpenGL_GameEngine.BeEngine2D.Rendering.Shaders;
using OpenGL_GameEngine.BeEngine2D.Rendering.Cameras;

namespace OpenGL_GameEngine.BeEngine2D
{
    public abstract class BeEngine2D
    {
        private string WindowTitle;

        public static List<Block> AllBlocks = new List<Block>();
        public static List<Entity> AllEntities = new List<Entity>();
        public static List<Polygon> AllPolygons = new List<Polygon>();
        public static List<Light> AllLights = new List<Light>();

        // Shader vars
        uint VAO;
        uint VBO;
        Shader Shader;

        // Camera
        Camera2D MainCamera;

        public enum CollisionType
        {
            None, BlockAll, Overlap
        }

        public BeEngine2D(Vector2 ScreenSize, string WindowTitle)
        {
            SetUpLog();

            Log.PrintInfo("*********************************************");
            Log.PrintInfo("Preparing BeEngine2D...");

            CameraPosition = new Vector2(400, 300);

            // Window values
            Log.PrintInfo("Assigning engine values...");
            InitialWindowWidth = (int)ScreenSize.X;
            InitialWindowHeight = (int)ScreenSize.Y;
            InitialWindowTitle = WindowTitle;

            Log.PrintInfo("Running BeEngine2D...");
            Run();
        }

        protected int InitialWindowWidth { get; set; }
        protected int InitialWindowHeight { get; set; }
        protected string InitialWindowTitle { get; set; }

        public void Run()
        {
            Log.PrintInfo("Initializing BeEngine2D...");
            Initialize();

            Log.PrintInfo("Creating window...");
            DisplayManager.CreateWindow(InitialWindowWidth, InitialWindowHeight, InitialWindowTitle);

            Log.PrintInfo("Loading content...");
            LoadEngineContent();

            Log.PrintInfo("Starting game loop...");
            while (!Glfw.WindowShouldClose(DisplayManager.Window))
            {
                GameTime.DeltaTime = (float)Glfw.Time - GameTime.TotalElapsedSeconds;
                GameTime.TotalElapsedSeconds = (float)Glfw.Time;

                Glfw.PollEvents();

                Update();

                Render();
            }

            DisplayManager.CloseWindow();
        }

        protected unsafe void LoadEngineContent()
        {
            // Creating shaders
            string VertexShader = @"#version 330 core
                                    layout (location = 0) in vec2 aPosition;
                                    layout (location = 1) in vec3 aColor;
                                    out vec4 vertexColor;

                                    uniform mat4 projection;
                                    uniform mat4 model;
                                    
                                    void main()
                                    {
                                        vertexColor = vec4(aColor.rgb, 1.0);
                                        gl_Position = projection * model * vec4(aPosition.xy, 0, 1.0);
                                    }";

            string FragmentShader = @"#version 330 core
                                        out vec4 FragColor;
                                        in vec4 vertexColor;
                                        
                                        void main()
                                        {
                                            FragColor = vertexColor;
                                        }";

            Shader = new Shader(VertexShader, FragmentShader);
            Shader.Load();

            VAO = glGenVertexArray();
            VBO = glGenBuffer();

            glBindVertexArray(VAO);

            glBindBuffer(GL_ARRAY_BUFFER, VBO);

            float[] Vertices =
            {
                -0.5f, 0.5f, 1f, 0f, 0f, // top left
                0.5f, 0.5f, 0f, 1f, 0f,// top right
                -0.5f, -0.5f, 0f, 0f, 1f, // bottom left

                0.5f, 0.5f, 0f, 1f, 0f,// top right
                0.5f, -0.5f, 0f, 1f, 1f, // bottom right
                -0.5f, -0.5f, 0f, 0f, 1f, // bottom left
            };

            fixed (float* v = &Vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * Vertices.Length, v, GL_STATIC_DRAW);
            }

            glVertexAttribPointer(0, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)0);
            glEnableVertexAttribArray(0);

            glVertexAttribPointer(1, 3, GL_FLOAT, false, 5 * sizeof(float), (void*)(2 * sizeof(float)));
            glEnableVertexAttribArray(1);

            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);

            MainCamera = new Camera2D(DisplayManager.WindowSize / 2f, 2.5f);

            // Setup key input
            Glfw.SetKeyCallback(DisplayManager.Window, KeyInput);
        }

        protected void Render()
        {
            glClearColor(0, 0, 0, 0);
            glClear(GL_COLOR_BUFFER_BIT);

            // Camera movement
            Vector2 Scale = new Vector2(150, 100);
            float Rotation = /*(float)Math.Sin(GameTime.TotalElapsedSeconds) * (float)Math.PI * 2f*/0;

            Matrix4x4 MatrixTranslation = Matrix4x4.CreateTranslation(CameraPosition.X, CameraPosition.Y, 0);
            Matrix4x4 MatrixScale = Matrix4x4.CreateScale(Scale.X, Scale.Y, 1);
            Matrix4x4 MatrixRotation = Matrix4x4.CreateRotationZ(Rotation);

            Shader.SetMatrix4x4("model", MatrixScale * MatrixRotation * MatrixTranslation);

            Shader.Use();
            Shader.SetMatrix4x4("projection", MainCamera.GetProjectionMatrix());

            glBindVertexArray(VAO);
            glDrawArrays(GL_TRIANGLES, 0, 6);
            glBindVertexArray(0);

            Glfw.SwapBuffers(DisplayManager.Window);
        }

        private void SetUpLog()
        {
            Log.PrintInfo("Setting up log...");
            try
            {
                if (File.Exists(@"log.txt"))
                {
                    File.Delete(@"log.txt");
                    var log = File.Create(@"log.txt");
                    log.Close();
                }
                else
                {
                    File.Create(@"log.txt");
                }

                Log.PrintInfo("Log file has been created.");
            }
            catch (Exception ex)
            {
                Log.PrintError("Can't create log file - " + ex);
            }
        }

        public static void RegisterBlock(Block block)
        {
            AllBlocks.Add(block);
            Log.PrintInfo("Registered block with ID \"" + block.ObjectID + "\"");
        }

        public static void RegisterEntity(Entity entity)
        {
            AllEntities.Add(entity);
            Log.PrintInfo("Registered entity with ID \"" + entity.ObjectID + "\"");
        }

        public static void RegisterPolygon(Polygon polygon)
        {
            AllPolygons.Add(polygon);
            Log.PrintInfo("Registered polygon with ID \"" + polygon.ObjectID + "\"");
        }

        public static void RegisterLight(Light light)
        {
            AllLights.Add(light);
            Log.PrintInfo("Registered polygon with ID \"" + light.ObjectID + "\"");
        }

        public static Block[] GetBlocksByTag(string tag)
        {
            List<Block> blocks = new List<Block>();

            foreach (Block block in AllBlocks)
            {
                if (block.Tag == tag) blocks.Add(block);
            }

            return blocks.ToArray();
        }

        public static Entity[] GetEntitiesByTag(string tag)
        {
            List<Entity> entities = new List<Entity>();

            foreach (Entity entity in AllEntities)
            {
                if (entity.Tag == tag) entities.Add(entity);
            }

            return entities.ToArray();
        }

        public static Block GetBlockByPosition(float x, float y)
        {
            foreach (Block block in AllBlocks)
            {
                if (x > block.Position.X && x < block.Position.X + block.Scale.X &&
                        y < block.Position.Y + block.Scale.Y && y > block.Position.Y) return block;
            }

            return null;
        }

        public static Entity GetEntityByPosition(float x, float y)
        {
            foreach (Entity entity in AllEntities)
            {
                if (x > entity.Position.X && x < entity.Position.X + entity.Scale.X &&
                        y < entity.Position.Y + entity.Scale.Y && y > entity.Position.Y) return entity;
            }

            return null;
        }
        protected abstract void Initialize();
        protected abstract void Update();
        protected abstract void KeyInput(Window window, Keys key, int scanCode, InputState state, ModifierKeys mods);

        public Vector2 CameraPosition { get; set; }
        public Vector2 CameraScale { get; set; }
        public int FPS { get; set; }
    }
}