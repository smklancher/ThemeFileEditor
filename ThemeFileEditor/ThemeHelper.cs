using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeFileEditor
{
    class ThemeHelper
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
            {
                var x = new Dictionary<string, string>();
                x.Add(nameof(SystemColors.ActiveBorder), "ActiveBorder"); //same
                x.Add(nameof(SystemColors.ActiveCaption), "ActiveTitle");
                x.Add(nameof(SystemColors.ActiveCaptionText), "TitleText");
                x.Add(nameof(SystemColors.AppWorkspace), "AppWorkspace"); //same
                x.Add(nameof(SystemColors.ButtonFace), "ButtonFace"); //same
                x.Add(nameof(SystemColors.ButtonHighlight), "ButtonHilight");
                x.Add(nameof(SystemColors.ButtonShadow), "ButtonShadow"); //same
                x.Add(nameof(SystemColors.Control), null);
                x.Add(nameof(SystemColors.ControlDark), null);
                x.Add(nameof(SystemColors.ControlDarkDark), null);
                x.Add(nameof(SystemColors.ControlLight), null);
                x.Add(nameof(SystemColors.ControlLightLight), null);
                x.Add(nameof(SystemColors.ControlText), null);
                x.Add(nameof(SystemColors.Desktop), "Background");
                x.Add(nameof(SystemColors.GradientActiveCaption), "GradientActiveTitle");
                x.Add(nameof(SystemColors.GradientInactiveCaption), "GradientInactiveTitle");
                x.Add(nameof(SystemColors.GrayText), "GrayText"); //same
                x.Add(nameof(SystemColors.Highlight), "Hilight");
                x.Add(nameof(SystemColors.HighlightText), "HilightText");
                x.Add(nameof(SystemColors.HotTrack), null);
                x.Add(nameof(SystemColors.InactiveBorder), "InactiveBorder"); //same
                x.Add(nameof(SystemColors.InactiveCaption), "InactiveTitle");
                x.Add(nameof(SystemColors.InactiveCaptionText), "InactiveTitleText");
                x.Add(nameof(SystemColors.Info), "InfoWindow");
                x.Add(nameof(SystemColors.InfoText), "InfoText"); //same
                x.Add(nameof(SystemColors.Menu), "Menu"); //same
                x.Add(nameof(SystemColors.MenuBar), null);
                x.Add(nameof(SystemColors.MenuHighlight), null);
                x.Add(nameof(SystemColors.MenuText), "MenuText"); //same
                x.Add(nameof(SystemColors.ScrollBar), "ScrollBar"); //same
                x.Add(nameof(SystemColors.Window), "Window"); //same
                x.Add(nameof(SystemColors.WindowFrame), "WindowFrame"); //same
                x.Add(nameof(SystemColors.WindowText), "WindowText"); //same
                Mapping = x;
            }
            return Mapping;
        }

        public static string ThemeColorFromSystemColor(string systemColor)
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

        public static string SystemColorFromThemeColor(string themeColor)
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
    }
}
