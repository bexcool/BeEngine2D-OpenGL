using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public void SetMatrix4x4 (string UniformName, Matrix4x4 Matrix)
        {
            int Location = glGetUniformLocation(ProgramID, UniformName);
            glUniformMatrix4fv(Location, 1, false, GetMatrix4x4Values(Matrix));
        }

        private float[] GetMatrix4x4Values(Matrix4x4 m)
        {
            return new float[]
            {
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
            };
        }
    }
}
