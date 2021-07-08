using System;
using GLFW;
using static CelluralAutomata.OpenGL.GL;

namespace CelluralAutomata.MouseEvents
{
    class MouseCallbacks
    {
        public static unsafe void cursorPositionCallback(GLFW.Window window, double xPos, double yPos)
        {
            //Console.WriteLine(xPos+":"+yPos);
        }

        public static void mouseButtonCallback(Window window, MouseButton button, InputState state, ModifierKeys modifiers)
        {
            if(button == GLFW.MouseButton.Left && state == GLFW.InputState.Press)
            {
                Console.WriteLine("CLICK!@#!@#@!#!@");
            }
        }
    }
}