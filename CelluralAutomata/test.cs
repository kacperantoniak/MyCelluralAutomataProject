using CelluralAutomata.Loop;
using CelluralAutomata.Rendering.Display;
using CelluralAutomata.Rendering.Shaders;
using GLFW;
using static CelluralAutomata.OpenGL.GL;

namespace CelluralAutomata
{
    class Test : RenderLoop
    {
        uint vao;
        uint vbo;

        Shader shader;

        public Test(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle)
        {
        }

        protected override void Initialize()
        {
        }

        protected unsafe override void LoadContent()
        {
            string vertexShader = @"#version 330 core
                                    layout (location = 0) in vec2 aPosition;
                                    layout (location = 1) in vec3 aColor;
                                    out vec4 vertexColor;

                                    void main()
                                    {
                                        vertexColor = vec4(aColor.rgb, 1.0);
                                        gl_Position = vec4(aPosition.xy, 0, 1.0);
                                    }";

            string fragmentShader = @"#version 330 core
                                    in vec4 vertexColor;
                                    out vec4 FragColor;

                                    void main() 
                                    {
                                        FragColor = vertexColor;
                                    }";

            shader = new Shader(vertexShader, fragmentShader);
            shader.Load();

            //generate VBO and VAO
            vao = glGenVertexArray();
            vbo = glGenBuffer();

            glBindVertexArray(vao);
            glBindBuffer(GL_ARRAY_BUFFER ,vbo);

            float[] vertices =
            {
                //first 2 variables describe position (x,y) the rest is rgb
                -0.01f, 0.01f, 0f, 1f, 0f, // top left
                0.01f, 0.01f, 0f, 1f, 0f,// top right
                -0.01f, -0.01f, 0f, 1f, 0f, // bottom left

                /*0.01f, 0.01f, 0f, 1f, 0f,// top right
                0.01f, -0.01f, 0f, 1f, 0f, // bottom right
                -0.01f, -0.01f, 0f, 1f, 0f, // bottom left*/
            };

            fixed(float* v = &vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, v, GL_STATIC_DRAW);
            }

            glVertexAttribPointer(0, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)0);
            glEnableVertexAttribArray(0);

            glVertexAttribPointer(1, 3, GL_FLOAT, false, 5 * sizeof(float), (void*)(2 * sizeof(float)));
            glEnableVertexAttribArray(1);

            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);
        }

        protected override void Update()
        {
        }
        protected override void Render()
        {
            glClearColor(0, 0, 0, 0);
            glClear(GL_COLOR_BUFFER_BIT);

            shader.Use();

            glBindVertexArray(vao);
            glDrawArrays(GL_TRIANGLES, 0, 6);
            glBindVertexArray(0);

            Glfw.SwapBuffers(DisplayManager.Window);
        }

    }
}