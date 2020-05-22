using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;

namespace KeyToProcess
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);        

        static void Main(string[] args)
        {
            string processName = "notepad";
            IntPtr keyToSend = (IntPtr)(Keys.F1);
            SendKeyTo(processName,keyToSend);
        }

        static void SendKeyTo(string processName, IntPtr keyToPress)
        {
            const uint wMsg_KEY_DOWN = 0x0100;
            const uint wMsg_KEY_UP = 0x0101;

            Process process = Process.GetProcessesByName(processName).FirstOrDefault();
            if(process != null){
                IntPtr hWnd = process.MainWindowHandle;
                PostMessage(hWnd, wMsg_KEY_DOWN,keyToPress, IntPtr.Zero);
                PostMessage(hWnd, wMsg_KEY_UP, keyToPress, IntPtr.Zero);
            }
            else
            {
                MessageBox.Show("The process name did not found any process.");
            }            
        }
    }
}

