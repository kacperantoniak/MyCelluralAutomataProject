using System;
using CelluralAutomata;
using GLFW;
//using System.Drawing;
using static CelluralAutomata.OpenGL.GL;

namespace CelluralAutomata.MouseEvents
{
    class MouseCallbacks
    {
        public static unsafe void cursorPositionCallback(GLFW.Window window, double xPos, double yPos)
        {
            /*double normalizedX;
            double normalizedY;

            int width, height;
            Glfw.GetWindowSize(window, out width, out height);

            normalizedX = -1.0 + 2.0 * (double)xPos / width;
            normalizedY = -(1.0 - 2.0 * (double)yPos / height);

            //Console.WriteLine(normalizedX+":"+normalizedY);*/
            RenderGroundObject.xPostition = xPos;
            RenderGroundObject.yPostition = yPos;
        }

        public static void mouseButtonCallback(Window window, MouseButton button, InputState state, ModifierKeys modifiers)
        {
            if(button == GLFW.MouseButton.Left && state == GLFW.InputState.Press)
            {
                RenderGroundObject.start = true;
                Console.WriteLine(RenderGroundObject.start);
            }
        }
    }
}