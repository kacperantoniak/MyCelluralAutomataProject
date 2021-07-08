using System.Diagnostics;
using static CelluralAutomata.OpenGL.GL;

namespace CelluralAutomata.Rendering.Shaders
{
    class Shader
    {
        string vertexCode;
        string fragmentCode;
        public uint ProgramID { get; set; }

        public Shader(string vertexCode, string fragmentCode)
        {
            this.vertexCode = vertexCode;
            this.fragmentCode = fragmentCode;
        }

        public void Load()
        {
            uint vs, fs; //uint is Unsigned INTeger

            vs = glCreateShader(GL_VERTEX_SHADER);
            glShaderSource(vs , vertexCode);
            glCompileShader(vs);

            //check if it succeded
            int[] status = glGetShaderiv(vs, GL_COMPILE_STATUS, 1);
            if(status[0] == 0)
            {
                string error = glGetShaderInfoLog(vs);
                Debug.WriteLine("Error compiling vertex shader: "+error);
            }

            fs = glCreateShader(GL_FRAGMENT_SHADER);
            glShaderSource(fs , fragmentCode);
            glCompileShader(fs);

            status = glGetShaderiv(vs, GL_COMPILE_STATUS, 1);
            if(status[0] == 0)
            {
                string error = glGetShaderInfoLog(fs);
                Debug.WriteLine("Error compiling fragment shader: "+error);
            }

            ProgramID = glCreateProgram();
            glAttachShader(ProgramID, vs);
            glAttachShader(ProgramID, fs);

            glLinkProgram(ProgramID);

            //delete shaders (free data)
            glDetachShader(ProgramID, vs);
            glDetachShader(ProgramID, fs);
            glDeleteShader(vs);
            glDeleteShader(fs);
        }

        public void Use()
        {
            glUseProgram(ProgramID);
        }
    }
}