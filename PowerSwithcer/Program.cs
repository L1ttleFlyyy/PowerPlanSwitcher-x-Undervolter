using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerSwitcher
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Show the system tray icon.
            using (ProcessIcon pi = new ProcessIcon())
            {
                pi.Disposed += (sender,e)=> {
                    Application.Exit();
                };
                SystemEvents.PowerModeChanged += pi.OnPowerChanged;
                Application.Run();
            }
        }
    }
}
