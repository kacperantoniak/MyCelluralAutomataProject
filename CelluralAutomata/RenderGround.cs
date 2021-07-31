using CelluralAutomata.Loop;
using CelluralAutomata.Rendering.Display;
using CelluralAutomata.Rendering.Shaders;
using CelluralAutomata.MouseEvents;
using GLFW;
using static CelluralAutomata.OpenGL.GL;

using System;

namespace CelluralAutomata
{
    class RenderGroundObject : RenderLoop
    {
        public static bool start;
        uint vao;
        uint vbo;
        Shader shader;
        public static double xPostition;
        public static double yPostition;
        public static float[] vertices =
        {
            //first 2 variables describe position (x,y) the rest is rgb
            -0.01f, 0.01f, 0f, 1f, 0f, // top left 0
            0.01f, 0.01f, 0f, 1f, 0f,// top right 5
            -0.01f, -0.01f, 0f, 1f, 0f, // bottom left 10

            0.01f, 0.01f, 0f, 1f, 0f,// top right[2] 15
            0.01f, -0.01f, 0f, 1f, 0f, // bottom right 20
            -0.01f, -0.01f, 0f, 1f, 0f, // bottom left[2] 25
        };

        public RenderGroundObject(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle)
        {
        }

        protected override void Initialize()
        {
        }

        public static void GiveVerticies()
        {
            Glfw.SetCursorPositionCallback(DisplayManager.Window, MouseCallbacks.cursorPositionCallback);

            double normalizedX;
            double normalizedY;

            int width, height;
            Glfw.GetWindowSize(DisplayManager.Window, out width, out height);

            normalizedX = -1.0 + 2.0 * (double)xPostition / width;
            normalizedY = -(1.0 - 2.0 * (double)yPostition / height);

            float value = 0.02f;

            //top left
            vertices[0] = (float)normalizedX - value;
            vertices[1] = (float)normalizedY + value;

            //top right
            vertices[5] = (float)normalizedX + value;
            vertices[6] = (float)normalizedY + value;

            //bottom left
            vertices[10] = (float)normalizedX - value;
            vertices[11] = (float)normalizedY - value;

            //top right[2]
            vertices[15] = (float)normalizedX + value;
            vertices[16] = (float)normalizedY + value;

            //bottom right
            vertices[20] = (float)normalizedX + value;
            vertices[21] = (float)normalizedY - value;

            //bottom left[2]
            vertices[10] = (float)normalizedX - value;
            vertices[11] = (float)normalizedY - value;
        }

        protected unsafe override void LoadContent()
        {
            //if(start)
            //{
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

                GiveVerticies();

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

                //start = false;
            //}
            /*else
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

                GiveVerticies();

                float[] nullvertices =
                {
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                };

                fixed(float* v = &nullvertices[0])
                {
                    glBufferData(GL_ARRAY_BUFFER, sizeof(float) * nullvertices.Length, v, GL_STATIC_DRAW);
                }

                glVertexAttribPointer(0, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)0);
                glEnableVertexAttribArray(0);

                glVertexAttribPointer(1, 3, GL_FLOAT, false, 5 * sizeof(float), (void*)(2 * sizeof(float)));
                glEnableVertexAttribArray(1);

                glBindBuffer(GL_ARRAY_BUFFER, 0);
                glBindVertexArray(0);
            }*/
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