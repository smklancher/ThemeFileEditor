using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;

namespace ThemeFileEditor
{
    /// <summary>
    /// https://stackoverflow.com/a/23205242/221018
    /// </summary>
    class ApplyTheme
    {
        /// Handles to Win 32 API
        [DllImport("dwmapi.dll")]
        public static extern IntPtr DwmIsCompositionEnabled(out bool pfEnabled);
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string sClassName, string sAppName);
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /// Windows Constants
        private const uint WM_CLOSE = 0x10;

        /// <summary>
        /// "The only real difference between Aero and Basic theme is Composition=0 in the [VisualStyles] in Basic (line omitted in Aero)"
        /// </summary>
        /// <returns></returns>
        public Boolean IsCompositionEnabled()
        {
            DwmIsCompositionEnabled(out bool aeroEnabled);
            return aeroEnabled;
        }

        private String StartProcessAndWait(string filename, string arguments, int seconds, ref Boolean bExited)
        {
            String msg = String.Empty;
            Process p = new Process();
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p.StartInfo.FileName = filename;
            p.StartInfo.Arguments = arguments;
            p.Start();

            bExited = false;
            int counter = 0;
            /// give it "seconds" seconds to run
            while (!bExited && counter < seconds)
            {
                bExited = p.HasExited;
                counter++;
                System.Threading.Thread.Sleep(1000);
            }//while
            if (counter == seconds)
            {
                msg = "Program did not close in expected time.";
            }//if

            return msg;
        }

        /// <summary>
        /// This doesn't appear to work anymore. (at least in Windows 10 1709)
        /// </summary>
        /// <param name="themePath"></param>
        /// <returns></returns>
        public Boolean SwitchTheme(string themePath)
        {
            try
            {
                //String themePath = System.Environment.GetFolderPath(Environment.SpecialFolder.Windows) + @"\Resources\Ease of Access Themes\basic.theme";
                /// Set the theme
                Boolean bExited = false;
                /// essentially runs the command line:  rundll32.exe %SystemRoot%\system32\shell32.dll,Control_RunDLL %SystemRoot%\system32\desk.cpl desk,@Themes /Action:OpenTheme /file:"%WINDIR%\Resources\Ease of Access Themes\classic.theme"
                String ThemeOutput = this.StartProcessAndWait("rundll32.exe", System.Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\shell32.dll,Control_RunDLL " + System.Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\desk.cpl desk,@Themes /Action:OpenTheme /file:\"" + themePath + "\"", 30, ref bExited);

                Console.WriteLine(ThemeOutput);

                /// Wait for the theme to be set
                System.Threading.Thread.Sleep(1000);

                /// Close the Theme UI Window
                IntPtr hWndTheming = FindWindow("CabinetWClass", null);
                SendMessage(hWndTheming, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }//try
            catch (Exception ex)
            {
                Console.WriteLine("An exception occured while setting the theme: " + ex.Message);

                return false;
            }//catch
            return true;
        }

        public Boolean SwitchToClassicTheme()
        {
            return SwitchTheme(System.Environment.GetFolderPath(Environment.SpecialFolder.Windows) + @"\Resources\Ease of Access Themes\basic.theme");
        }

        public Boolean SwitchToAeroTheme()
        {
            return SwitchTheme(System.Environment.GetFolderPath(Environment.SpecialFolder.Windows) + @"\Resources\Themes\aero.theme");
        }

        public string GetTheme()
        {
            string RegistryKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes";
            string theme;
            theme = (string)Registry.GetValue(RegistryKey, "CurrentTheme", string.Empty);
            theme = theme.Split('\\').Last().Split('.').First().ToString();
            return theme;
        }

        // end of object Theming
    }
    
}