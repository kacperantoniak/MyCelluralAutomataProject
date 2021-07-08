using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Drawing;
using FreeImageAPI;
using GLFW;
using System.IO;
using static CelluralAutomata.OpenGL.GL;
using System.Runtime.InteropServices;
using CelluralAutomata.MouseEvents;

namespace CelluralAutomata.Rendering.Display
{
    class DisplayManager
    {
        public static Window Window{ get; set; }
        public static Vector2 window_size{ get; set; }
        //public static GLFW.Image[] images = new GLFW.Image[1];

        public static unsafe void CreateWindow(int width, int height, string title)
        {
            window_size = new Vector2(width, height);

            //images[0].Pixels = 

            //initialize
            Glfw.Init();
            //i'm using opengl3.3 so major version is 3
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.Focused, true);
            Glfw.WindowHint(Hint.Resizable, false);

            Window = Glfw.CreateWindow(width, height, title, Monitor.None, Window.None);

            //check if window was created
            if(Window == Window.None)
            {
                return;
            }

            // get the size of the monitor
            Rectangle workarea = Glfw.PrimaryMonitor.WorkArea;
            //calculate width and height of created window and set it so the window opens in the middle of the screen
            int x = (workarea.Width - width) /2;
            int y = (workarea.Height - height) /2;
            Glfw.SetWindowPosition(Window, x, y);

            Glfw.MakeContextCurrent(Window);
            //gets an adress of opengl and imports it
            Import(Glfw.GetProcAddress);

            glViewport(0, 0, width, height);
            //VSync off = 0, on = 1
            Glfw.SwapInterval(0);

            Glfw.SetCursorPositionCallback(Window, MouseCallbacks.cursorPositionCallback);
            Glfw.SetMouseButtonCallback(Window, MouseCallbacks.mouseButtonCallback);
        }

        public static void KillWindow()
        {
            Glfw.Terminate();
        }
    }
}