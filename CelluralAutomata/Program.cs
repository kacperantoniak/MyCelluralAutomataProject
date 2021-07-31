using System;
using GLFW;
using CelluralAutomata.Loop;
using static CelluralAutomata.OpenGL.GL; // a ref to GL.cs
using System.Runtime.InteropServices;
using System.Diagnostics;
// HEY, TO USE GLFW.GETPRIMARYMONITOR() YOU HAVE TO SET THE FUNCTION TO "EXTERN" so you can go fullscreen
namespace CelluralAutomata
{
    public class Program
    {
        //makes the console disappear while the program is running
        //[DllImport("user32.dll")]
        //static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void Main(string[] args)
        {
            //part 2 of disappearing cmd
            //IntPtr h = Process.GetCurrentProcess().MainWindowHandle;
            //ShowWindow(h, 0);

            RenderLoop render = new RenderGroundObject(1300, 900 ,"works!");
            render.Run();
            /*RenderLoop test = new Test(1300, 900 ,"works!");
            test.Run();*/
        }
    }
}