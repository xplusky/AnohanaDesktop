using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AnohanaDesktop
{
    internal class User32
    {

        public const uint SE_SHUTDOWN_PRIVILEGE = 0x13;



        [DllImport("user32.dll")]

        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);



        [DllImport("user32.dll")]

        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);



        [DllImport("user32.dll")]

        public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx,

            int cy, uint uFlags);

    }

}
