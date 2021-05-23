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
using OpenGL_GameEngine.BeEngine2D.Rendering.Textures;

namespace OpenGL_GameEngine.BeEngine2D
{
    public abstract class BeEngine2D
    {
        private string WindowTitle;

        public static bool SaveLog = true;

        public static List<Block> AllBlocks = new List<Block>();
        public static List<Entity> AllEntities = new List<Entity>();
        public static List<Polygon> AllPolygons = new List<Polygon>();
        public static List<Light> AllLights = new List<Light>();
        
        // Shader vars
        uint VAO;
        uint VBO;
        Shader Shader;

        // Viewport
        Camera2D MainCamera;
        Vector2 PreviousWindowSize;

        // World
        List<float> ListVerticies = new List<float>();

        // Frame rate
        Timer FPSRefresh = new Timer();
        public bool ShowFPS;

        public enum CollisionType
        {
            None, BlockAll, Overlap
        }

        public BeEngine2D(Vector2 ScreenSize, string WindowTitle)
        {
            if (SaveLog) SetUpLog();

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

                Update();

                Glfw.PollEvents();

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
                                    layout (location = 2) in vec2 aTexCoords;

                                    out vec4 vertexColor;
                                    out vec2 fTexCoords;

                                    uniform mat4 projection;
                                    uniform mat4 model;
                                    
                                    void main()
                                    {
                                        vertexColor = vec4(aColor.rgb, 1.0);
                                        fTexCoords = aTexCoords;
                                        gl_Position = projection * model * vec4(aPosition.xy, 0, 1.0);
                                    }";

            string FragmentShader = @"#version 330 core

                                        out vec4 FragColor;

                                        in vec4 vertexColor;
                                        in vec2 fTexCoords;
                                        
                                        uniform sampler2D TEX_SAMPLER;

                                        void main()
                                        {
                                            FragColor = texture(TEX_SAMPLER, fTexCoords);
                                        }";

            //FragColor = vertexColor;

            Shader = new Shader(VertexShader, FragmentShader);
            Shader.Load();

            VAO = glGenVertexArray();
            VBO = glGenBuffer();

            PreviousWindowSize = DisplayManager.WindowSize;

            #region On load old code
            /*
            glBindVertexArray(VAO);

            glBindBuffer(GL_ARRAY_BUFFER, VBO);

            
            float[] Vertices =
            {
                -1f, -1f, 1f, 0f, 0f, // top left
                -0f, -1f, 0f, 1f, 0f,// top right
                -1f, -0f, 0f, 0f, 1f, // bottom left
                
                1f, 1f, 0f, 1f, 0f,// top right
                1f, 0f, 0f, 1f, 1f, // bottom right
                0f, 0f, 0f, 0f, 1f, // bottom left

                1f, 1f, 1f, 1f, 1f,
                1f, 0.5f, 1f, 1f, 1f,
                0.5f, 1f, 1f, 1f, 1f,
            };
            */
            /*
            new Block(new Vector2(60, 60), new Vector2(100, 100), Color.White, CollisionType.None);

            new Block(new Vector2(60, 60), new Vector2(100, 30), Color.Red, CollisionType.None);

            List<float> ListVerticies = new List<float>();

            foreach (Block block in AllBlocks)
            {
                // Bottom left
                ListVerticies.Add((float)DisplayManager.ConvertPixelsToGL_X(block.Position.X));
                ListVerticies.Add((float)DisplayManager.ConvertPixelsToGL_Y(block.Position.Y + block.Scale.Y));
                ListVerticies.AddRange( new List<float> { block.Color.R, block.Color.G, block.Color.B } );

                // Top left
                ListVerticies.Add((float)DisplayManager.ConvertPixelsToGL_X(block.Position.X));
                ListVerticies.Add((float)DisplayManager.ConvertPixelsToGL_Y(block.Position.Y));
                ListVerticies.AddRange(new List<float> { block.Color.R, block.Color.G, block.Color.B });

                // Top right
                ListVerticies.Add((float)DisplayManager.ConvertPixelsToGL_X(block.Position.X + block.Scale.X));
                ListVerticies.Add((float)DisplayManager.ConvertPixelsToGL_Y(block.Position.Y));
                ListVerticies.AddRange(new List<float> { block.Color.R, block.Color.G, block.Color.B });

                // Bottom left
                ListVerticies.Add((float)DisplayManager.ConvertPixelsToGL_X(block.Position.X));
                ListVerticies.Add((float)DisplayManager.ConvertPixelsToGL_Y(block.Position.Y + block.Scale.Y));
                ListVerticies.AddRange(new List<float> { block.Color.R, block.Color.G, block.Color.B });

                // Top right
                ListVerticies.Add((float)DisplayManager.ConvertPixelsToGL_X(block.Position.X + block.Scale.X));
                ListVerticies.Add((float)DisplayManager.ConvertPixelsToGL_Y(block.Position.Y));
                ListVerticies.AddRange(new List<float> { block.Color.R, block.Color.G, block.Color.B });

                // Bottom right
                ListVerticies.Add((float)DisplayManager.ConvertPixelsToGL_X(block.Position.X + block.Scale.X));
                ListVerticies.Add((float)DisplayManager.ConvertPixelsToGL_Y(block.Position.Y + block.Scale.Y));
                ListVerticies.AddRange(new List<float> { block.Color.R, block.Color.G, block.Color.B });
            }

            float[] Vertices = ListVerticies.ToArray();

            fixed (float* v = &Vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * Vertices.Length, v, GL_STATIC_DRAW);
            }

            glVertexAttribPointer(0, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)0);
            glEnableVertexAttribArray(0);

            glVertexAttribPointer(1, 3, GL_FLOAT, false, 5 * sizeof(float), (void*)(2 * sizeof(float)));
            glEnableVertexAttribArray(1);

            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);*/
            #endregion

            MainCamera = new Camera2D(DisplayManager.WindowSize / 2f, 1f);

            FPSRefresh.Interval = 2000;
            FPSRefresh.Tick += FPSRefresh_Tick;
            FPSRefresh.Start();
        }

        private void FPSRefresh_Tick(object sender, EventArgs e)
        {
            if (ShowFPS) Log.PrintInfo("FPS: " + (int)(1 / GameTime.DeltaTime));
        }

        protected unsafe void Render()
        {
            glClearColor(0, 0, 0, 0);
            glClear(GL_COLOR_BUFFER_BIT);

            // Camera movement
            Vector2 Scale = new Vector2(DisplayManager.WindowSize.X / 2f, DisplayManager.WindowSize.Y / 2f);
            float Rotation = /*(float)Math.Sin(GameTime.TotalElapsedSeconds) * (float)Math.PI * 2f*/
            0;

            Matrix4x4 MatrixTranslation = Matrix4x4.CreateTranslation(CameraPosition.X, CameraPosition.Y, 0);
            Matrix4x4 MatrixScale = Matrix4x4.CreateScale(Scale.X, Scale.Y, 1);
            Matrix4x4 MatrixRotation = Matrix4x4.CreateRotationZ(Rotation);

            Shader.SetMatrix4x4("model", MatrixScale * MatrixRotation * MatrixTranslation);

            Shader.Use();
            Shader.SetMatrix4x4("projection", MainCamera.GetProjectionMatrix());
            /*
            // Change camera position
            if (CameraPosition.X != DisplayManager.WindowSize.X / 2 && CameraPosition.Y != DisplayManager.WindowSize.Y / 2)
            {
                CameraPosition = new Vector2(DisplayManager.WindowSize.X / 2, DisplayManager.WindowSize.Y / 2);
            }*/

            Glfw.GetWindowSize(DisplayManager.Window, out int Width, out int Height);

            // Set viewport size
            if ((int)DisplayManager.WindowSize.X != Width || (int)DisplayManager.WindowSize.Y != Height)
            {
                PreviousWindowSize = DisplayManager.WindowSize;

                glViewport(0, 0, Width, Height);
                DisplayManager.WindowSize = new Vector2(Width, Height);

                //CameraPosition = new Vector2(CameraPosition.X - PreviousWindowSize.X / 2 + DisplayManager.WindowSize.X / 2, CameraPosition.Y - PreviousWindowSize.Y / 2 + DisplayManager.WindowSize.Y / 2);
            }

            MainCamera.FocusedObjectTag = CameraFocusedObjectTag;

            // Drawing
            glBindVertexArray(VAO);
            glBindBuffer(GL_ARRAY_BUFFER, VBO);

            ListVerticies.Clear();
            
            foreach (Block block in AllBlocks)
            {
                // Bottom left
                ListVerticies.Add((float)DisplayManager.NormalizePixels_X(block.Position.X));
                ListVerticies.Add((float)DisplayManager.NormalizePixels_Y(block.Position.Y + block.Scale.Y));
                ListVerticies.AddRange(new List<float> { block.Color.R / 255f, block.Color.G / 255f, block.Color.B / 255f });
                ListVerticies.AddRange(new List<float> { 0, 0 });

                // Top left
                ListVerticies.Add((float)DisplayManager.NormalizePixels_X(block.Position.X));
                ListVerticies.Add((float)DisplayManager.NormalizePixels_Y(block.Position.Y));
                ListVerticies.AddRange(new List<float> { block.Color.R / 255f, block.Color.G / 255f, block.Color.B / 255f });
                ListVerticies.AddRange(new List<float> { 0, 1 });

                // Top right
                ListVerticies.Add((float)DisplayManager.NormalizePixels_X(block.Position.X + block.Scale.X));
                ListVerticies.Add((float)DisplayManager.NormalizePixels_Y(block.Position.Y));
                ListVerticies.AddRange(new List<float> { block.Color.R / 255f, block.Color.G / 255f, block.Color.B / 255f });
                ListVerticies.AddRange(new List<float> { 1, 1 });

                // Bottom left
                ListVerticies.Add((float)DisplayManager.NormalizePixels_X(block.Position.X));
                ListVerticies.Add((float)DisplayManager.NormalizePixels_Y(block.Position.Y + block.Scale.Y));
                ListVerticies.AddRange(new List<float> { block.Color.R / 255f, block.Color.G / 255f, block.Color.B / 255f });
                ListVerticies.AddRange(new List<float> { 0, 0 });

                // Top right
                ListVerticies.Add((float)DisplayManager.NormalizePixels_X(block.Position.X + block.Scale.X));
                ListVerticies.Add((float)DisplayManager.NormalizePixels_Y(block.Position.Y));
                ListVerticies.AddRange(new List<float> { block.Color.R / 255f, block.Color.G / 255f, block.Color.B / 255f });
                ListVerticies.AddRange(new List<float> { 1, 1 });

                // Bottom right
                ListVerticies.Add((float)DisplayManager.NormalizePixels_X(block.Position.X + block.Scale.X));
                ListVerticies.Add((float)DisplayManager.NormalizePixels_Y(block.Position.Y + block.Scale.Y));
                ListVerticies.AddRange(new List<float> { block.Color.R / 255f, block.Color.G / 255f, block.Color.B / 255f });
                ListVerticies.AddRange(new List<float> { 1, 0 });

            }

            foreach (Entity entity in AllEntities)
            {
                Texture texture = new Texture(entity.ImageURL);

                if (entity.ImageURL != null)
                {
                    glActiveTexture(GL_TEXTURE0);
                    Shader.UploadTexture("TEX_SAMPLER", 0);
                    texture.Bind();
                }

                // Bottom left
                ListVerticies.Add((float)DisplayManager.NormalizePixels_X(entity.Position.X));
                ListVerticies.Add((float)DisplayManager.NormalizePixels_Y(entity.Position.Y + entity.Scale.Y));
                ListVerticies.AddRange(new List<float> { entity.Color.R / 255f, entity.Color.G / 255f, entity.Color.B / 255f });
                ListVerticies.AddRange(new List<float> { 0, 0 });

                // Top left
                ListVerticies.Add((float)DisplayManager.NormalizePixels_X(entity.Position.X));
                ListVerticies.Add((float)DisplayManager.NormalizePixels_Y(entity.Position.Y));
                ListVerticies.AddRange(new List<float> { entity.Color.R / 255f, entity.Color.G / 255f, entity.Color.B / 255f });
                ListVerticies.AddRange(new List<float> { 0, 1 });

                // Top right
                ListVerticies.Add((float)DisplayManager.NormalizePixels_X(entity.Position.X + entity.Scale.X));
                ListVerticies.Add((float)DisplayManager.NormalizePixels_Y(entity.Position.Y));
                ListVerticies.AddRange(new List<float> { entity.Color.R / 255f, entity.Color.G / 255f, entity.Color.B / 255f });
                ListVerticies.AddRange(new List<float> { 1, 1 });

                // Bottom left
                ListVerticies.Add((float)DisplayManager.NormalizePixels_X(entity.Position.X));
                ListVerticies.Add((float)DisplayManager.NormalizePixels_Y(entity.Position.Y + entity.Scale.Y));
                ListVerticies.AddRange(new List<float> { entity.Color.R / 255f, entity.Color.G / 255f, entity.Color.B / 255f });
                ListVerticies.AddRange(new List<float> { 0, 0 });

                // Top right
                ListVerticies.Add((float)DisplayManager.NormalizePixels_X(entity.Position.X + entity.Scale.X));
                ListVerticies.Add((float)DisplayManager.NormalizePixels_Y(entity.Position.Y));
                ListVerticies.AddRange(new List<float> { entity.Color.R / 255f, entity.Color.G / 255f, entity.Color.B / 255f });
                ListVerticies.AddRange(new List<float> { 1, 1 });

                // Bottom right
                ListVerticies.Add((float)DisplayManager.NormalizePixels_X(entity.Position.X + entity.Scale.X));
                ListVerticies.Add((float)DisplayManager.NormalizePixels_Y(entity.Position.Y + entity.Scale.Y));
                ListVerticies.AddRange(new List<float> { entity.Color.R / 255f, entity.Color.G / 255f, entity.Color.B / 255f });
                ListVerticies.AddRange(new List<float> { 1, 0 });

                /*
                if (entity.ImageURL != null)
                {
                    texture.UnBind();
                }*/

                // Update camera position
                if (entity.Tag != null && entity.Tag == MainCamera.FocusedObjectTag)
                {
                    // Copy from when window size changed
                    CameraPosition = new Vector2(entity.Position.X * -1 - entity.Scale.X / 2 + InitialWindowWidth - (InitialWindowWidth / 2 - DisplayManager.WindowSize.X / 2), entity.Position.Y * -1 - entity.Scale.Y / 2 + InitialWindowHeight - (InitialWindowHeight / 2 - DisplayManager.WindowSize.Y / 2));
                }
            }

            float[] Vertices = ListVerticies.ToArray();

            fixed (float* v = &Vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * Vertices.Length, v, GL_STATIC_DRAW);
            }

            glVertexAttribPointer(0, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)0);
            glEnableVertexAttribArray(0);

            glVertexAttribPointer(1, 3, GL_FLOAT, false, 5 * sizeof(float), (void*)(2 * sizeof(float)));
            glEnableVertexAttribArray(1);
            
            glVertexAttribPointer(2, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)(4 * sizeof(float)));
            glEnableVertexAttribArray(2);
            
            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);

            glBindVertexArray(VAO);
            glDrawArrays(GL_TRIANGLES, 0, ListVerticies.Count / 7); // :7 for textured and :5 for color only
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

        public Vector2 GetNormalizedMousePosition()
        {
            Glfw.GetCursorPosition(DisplayManager.Window, out double X, out double Y);

            return new Vector2((float)X - (int)CameraPosition.X + DisplayManager.WindowSize.X / 2 + (InitialWindowWidth - DisplayManager.WindowSize.X) / 2, (float)Y - (int)CameraPosition.Y + DisplayManager.WindowSize.Y / 2 + (InitialWindowHeight - DisplayManager.WindowSize.Y) / 2);
        }

        public bool KeyPressed(Keys Key)
        {
            if (Glfw.GetKey(DisplayManager.Window, Key) == InputState.Press)
            {
                return true;
            }

            return false;
        }

        public bool KeyReleased(Keys Key)
        {
            if (Glfw.GetKey(DisplayManager.Window, Key) == InputState.Release)
            {
                return true;
            }

            return false;
        }

        public bool KeyRepeated(Keys Key)
        {
            if (Glfw.GetKey(DisplayManager.Window, Key) == InputState.Repeat)
            {
                return true;
            }

            return false;
        }

        public bool MouseButtonPressed(MouseButton Key)
        {
            if (Glfw.GetMouseButton(DisplayManager.Window, Key) == InputState.Press)
            {
                return true;
            }

            return false;
        }

        public bool MouseButtonReleased(MouseButton Key)
        {
            if (Glfw.GetMouseButton(DisplayManager.Window, Key) == InputState.Release)
            {
                return true;
            }

            return false;
        }

        public bool MouseButtonRepeated(MouseButton Key)
        {
            if (Glfw.GetMouseButton(DisplayManager.Window, Key) == InputState.Repeat)
            {
                return true;
            }

            return false;
        }

        protected abstract void Initialize();
        protected abstract void Update();

        public Vector2 CameraPosition { get; set; }
        public string CameraFocusedObjectTag { get; set; }
        public Vector2 CameraScale { get; set; }
        public int FPS { get; set; }
    }
}