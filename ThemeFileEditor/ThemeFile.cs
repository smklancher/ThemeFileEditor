using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;
using IniParser.Parser;

namespace ThemeFileEditor
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/bb773190(VS.85).aspx
    /// </summary>
    class ThemeFile
    {

        public string FileName { get; private set; }
        private KeyDataCollection Colors { get => data[@"Control Panel\Colors"];  }

        private IniData data;

        /// <summary>
        /// Load a theme file
        /// </summary>
        /// <param name="file"></param>
        public ThemeFile(string file)
        {
            FileName = file;
            var parser = new FileIniDataParser();
            data = parser.ReadFile(file);
        }

        /// <summary>
        /// Create a theme file representing the current system colors
        /// </summary>
        public ThemeFile()
        {
            // set temp file name... or blank?

            // string parse default required sections
            data = DefaultThemeData.DefaultData();
            Colors.RemoveAllKeys();

            // then overwrite colors from system
            foreach (string name in ThemeHelper.SystemColorPropertyNames)
            {
                SetColor(name, ThemeHelper.SystemColorFromSystemName(name));

            }
        }

        public void SetColor(string name, Color c)
        {
            string tname = ThemeHelper.ThemeNameFromSystemName(name);
            if (!String.IsNullOrEmpty(tname))
            {
                Colors[tname] = ThemeHelper.ColorToRgbSpaced(c);
            }
            else
            {
                var key = Colors.First();
                if (key != null)
                {
                    key.Comments.Add($"No theme property known for system name {name}");
                }
            }
        }

        public bool TryGetColor(string name, out Color c)
        {
            string tname = ThemeHelper.ThemeNameFromSystemName(name);
            if (Colors.ContainsKey(tname))
            {
                c=ThemeHelper.RgbSpacedToColor(Colors[tname]);
                return true;
            }

            c = new Color();
            return false;
        }

        public void Save()
        {
            var parser = new FileIniDataParser();
            parser.WriteFile(FileName, data);
        }

        public void SaveAs(string NewFileName)
        {
            Trace.WriteLine($"Saving {FileName} as {NewFileName}");
            FileName = NewFileName;
            Save();
        }

        public void Apply()
        {
            string tempfile = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".theme";
            string originalFilename = FileName;
            
            // Save as temp file and execute
            SaveAs(tempfile);
            Trace.WriteLine($"Applying theme via temp file");
            var p=Process.Start(tempfile);

            //var t = new ApplyTheme();
            //t.SwitchTheme(tempfile);

            // Restore original file name
            FileName = originalFilename;
        }

        

        
    }
}
