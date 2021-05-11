using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenGL_GameEngine.BeEngine2D.GL;

namespace OpenGL_GameEngine.BeEngine2D.Rendering.Shaders
{
    class Shader
    {
        string VertexCode;
        string FragmentCode;

        public uint ProgramID { get; set; }

        public Shader(string VertexCode, string FragmentCode)
        {
            this.VertexCode = VertexCode;
            this.FragmentCode = FragmentCode;
        }

        public void Load()
        {
            Log.PrintInfo("Loading shaders...");

            uint VS, FS;

            VS = glCreateShader(GL_VERTEX_SHADER);
            glShaderSource(VS, VertexCode);
            glCompileShader(VS);

            int[] status = glGetShaderiv(VS, GL_COMPILE_STATUS, 1);

            if (status[0] == 0)
            {
                Log.PrintError("Error compiling Vertex Shader: " + glGetShaderInfoLog(VS));
            }

            FS = glCreateShader(GL_FRAGMENT_SHADER);
            glShaderSource(FS, FragmentCode);
            glCompileShader(FS);

            status = glGetShaderiv(FS, GL_COMPILE_STATUS, 1);

            if (status[0] == 0)
            {
                Log.PrintError("Error compiling Vertex Shader: " + glGetShaderInfoLog(FS));
            }

            ProgramID = glCreateProgram();
            glAttachShader(ProgramID, VS);
            glAttachShader(ProgramID, FS);

            glLinkProgram(ProgramID);

            // Delete shaders

            glDetachShader(ProgramID, VS);
            glDetachShader(ProgramID, FS);
            glDeleteShader(VS);
            glDeleteShader(FS);
        }

        public void Use()
        {
            glUseProgram(ProgramID);
        }
    }
}
