using CelluralAutomata.Loop;
using CelluralAutomata.Rendering.Display;
using CelluralAutomata.Rendering.Shaders;
using GLFW;
using static CelluralAutomata.OpenGL.GL;

namespace CelluralAutomata.MakeObject
{
    class Make : Loopname
    {
        uint vao;
        uint vbo;

        Shader shader;

        public Make(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle)
        {

        }

        protected override void Initialize()
        {
        }

        protected override void LoadContent()
        {
            string vertexShader = @"
                #version 330 core
                layout (location = 0) in vec2 aPosition;
                layout (location = 1) in vec4 aColor;
                out vec4 vertexColor;

                void main()
                {
                    vertexColor = vec4(aColor.xyzw);
                    gl_Position = vec4(aPosition.xy, 0, 1.0)
                }";
            string fragmentShader = @"
            #version 330 core
            in vec4 vertexColor;
            out vec4 FragColor;

            void main()
            {
                FragColor = vertexColor;
            }";

            //idk about this shader init right HERE, cuz giving input to this method is a pain in the ass
            //figure it out after mouse pos normalization method (yes, do this)
            shader = new Shader(vertexShader, fragmentShader);
            shader.Load();

            vao = glGenVertexArray();
            vbo = glGenBuffer();

            glBindVertexArray(vao);
            glBindBuffer(GL_ARRAY_BUFFER, vbo);

            
        }

        protected override void Update()
        {
        }
        protected override void Render()
        {
        }

        public static void Ground()
        {
            
        }
    }
}