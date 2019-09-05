using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ThemeFileEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            //Application.VisualStyleState = VisualStyleState.NonClientAreaEnabled;
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ThemeEditor());
        }
    }
}
