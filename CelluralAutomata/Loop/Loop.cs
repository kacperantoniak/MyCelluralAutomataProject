using CelluralAutomata.Rendering.Display;
using GLFW;
using System;

namespace CelluralAutomata.Loop
{
    abstract class RenderLoop
    {
        protected int InitialWindowWidth{ get; set; }
        protected int InitialWindowHeight{ get; set; }
        protected string InitialWindowTitle{ get; set; }

        public RenderLoop(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle)
        {
            this.InitialWindowWidth = initialWindowWidth;
            this.InitialWindowHeight = initialWindowHeight;
            this.InitialWindowTitle = initialWindowTitle;
        }

        public void Run()
        {
            Initialize();

            DisplayManager.CreateWindow(InitialWindowWidth, InitialWindowHeight, InitialWindowTitle);

            LoadContent();

            while(!Glfw.WindowShouldClose(DisplayManager.Window))
            {
                //calculate how much time passed from frame x to frame x+1
                Time.DeltaTime = (float)Glfw.Time - Time.TotalElapsedSeconds;
                Time.TotalElapsedSeconds = (float)Glfw.Time;

                Update();

                //chcek if the window is still responding
                Glfw.PollEvents();

                Render();
            }
            DisplayManager.KillWindow();
        }

        protected abstract void Initialize();
        protected abstract void LoadContent();
        
        protected abstract void Update();
        protected abstract void Render();
    }
}