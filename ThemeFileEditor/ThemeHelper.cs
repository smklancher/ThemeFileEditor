using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeFileEditor
{
    public static class ThemeHelper
    {
        private static Dictionary<string, string> Mapping;

        /// <summary>
        /// Returns a dictionary of key=nameof(SystemColors.*) and value the equivolent property name used in the theme file,
        /// where it may be a litteral string, or null means there is not a known mapping
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> ThemeSystemMapping()
        {
            if (Mapping == null)
            {   //http://www.temblast.com/dbplot/color5.htm
                var x = new Dictionary<string, string>();
                x.Add(nameof(SystemColors.ActiveBorder), "ActiveBorder"); //same
                x.Add(nameof(SystemColors.ActiveCaption), "ActiveTitle");
                x.Add(nameof(SystemColors.ActiveCaptionText), "TitleText");
                x.Add(nameof(SystemColors.AppWorkspace), "AppWorkspace"); //same
                x.Add(nameof(SystemColors.ButtonFace), "ButtonFace"); //same
                x.Add(nameof(SystemColors.ButtonHighlight), "ButtonHilight");
                x.Add(nameof(SystemColors.ButtonShadow), "ButtonShadow"); //same
                x.Add(nameof(SystemColors.Control), null);
                x.Add(nameof(SystemColors.ControlDark), null);                      // ButtonDkShadow?
                x.Add(nameof(SystemColors.ControlDarkDark), null);
                x.Add(nameof(SystemColors.ControlLight), null);                     // ButtonAlternateFace
                x.Add(nameof(SystemColors.ControlLightLight), null);
                x.Add(nameof(SystemColors.ControlText), null);
                x.Add(nameof(SystemColors.Desktop), "Background");
                x.Add(nameof(SystemColors.GradientActiveCaption), "GradientActiveTitle");
                x.Add(nameof(SystemColors.GradientInactiveCaption), "GradientInactiveTitle");
                x.Add(nameof(SystemColors.GrayText), "GrayText"); //same
                x.Add(nameof(SystemColors.Highlight), "Hilight");
                x.Add(nameof(SystemColors.HighlightText), "HilightText");
                x.Add(nameof(SystemColors.HotTrack), null);                         // HotTrackingColor?
                x.Add(nameof(SystemColors.InactiveBorder), "InactiveBorder"); //same
                x.Add(nameof(SystemColors.InactiveCaption), "InactiveTitle");
                x.Add(nameof(SystemColors.InactiveCaptionText), "InactiveTitleText");
                x.Add(nameof(SystemColors.Info), "InfoWindow");
                x.Add(nameof(SystemColors.InfoText), "InfoText"); //same
                x.Add(nameof(SystemColors.Menu), "Menu"); //same
                x.Add(nameof(SystemColors.MenuBar), null);                          // same?
                x.Add(nameof(SystemColors.MenuHighlight), null);                    // MenuHilight?
                x.Add(nameof(SystemColors.MenuText), "MenuText"); //same
                x.Add(nameof(SystemColors.ScrollBar), "ScrollBar"); //same
                x.Add(nameof(SystemColors.Window), "Window"); //same
                x.Add(nameof(SystemColors.WindowFrame), "WindowFrame"); //same
                x.Add(nameof(SystemColors.WindowText), "WindowText"); //same
                Mapping = x;
            }
            return Mapping;
        }

        public static List<string> SystemColorPropertyNames
        {
            get
            {
                return ThemeSystemMapping().Keys.ToList<string>();
            }

        }

        public static string ThemeNameFromSystemName(string systemColor)
        {
            ThemeSystemMapping().TryGetValue(systemColor, out string val);
            if (String.IsNullOrEmpty(val))
            {
                return String.Empty;
            }
            else
            {
                return val;
            }
        }

        public static string SystemNameFromThemeName(string themeColor)
        {
            var result = ThemeSystemMapping()
                .GroupBy(x => x.Value, x => x.Key)
                .ToDictionary(g => g.Key, g => g.FirstOrDefault());

            result.TryGetValue(themeColor, out string val);
            if (String.IsNullOrEmpty(val))
            {
                return String.Empty;
            }
            else
            {
                return val;
            }
        }

        public static Color SystemColorFromSystemName(string systemColor)
        {
            var t = typeof(SystemColors);

            var prop=t.GetProperty(systemColor);

            if (prop!=null)
            {
                return (Color)prop.GetValue(null);
            }
            // TODO: check for color from HKEY_CURRENT_USER\Control Panel\Colors
            // Applies to HotTrackingColor, GradientActiveTitle, GradientInactiveTitle, MenuHilight, MenuBar

            Trace.WriteLine($"Invalid property for system color name {systemColor}");
            return Color.White;
        }

        public static string ColorToRgbSpaced(Color c)
        {
            return $"{c.R.ToString()} {c.G.ToString()} {c.B.ToString()}";
        }

        public static Color RgbSpacedToColor(string c)
        {
            string[] colors = c.Split(' ');
            if (colors.Length != 3)
            {
                throw new FormatException($"RbgSpacedColor should be ### ### ###. Was: {c}");
            }

            Color temp = Color.White;

            try
            {
                temp = Color.FromArgb(Int32.Parse(colors[0]), Int32.Parse(colors[1]), Int32.Parse(colors[2]));
            }
            catch (Exception ex)
            {

                throw new FormatException($"RbgSpacedColor should be ### ### ###. Was: {c}", ex);
            }
            return temp;
        }
    }
}
