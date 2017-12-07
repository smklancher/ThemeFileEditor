using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;

namespace ThemeFileEditor
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/bb773190(VS.85).aspx
    /// </summary>
    class ThemeFile
    {

        public string FileName { get; }
        public KeyDataCollection Colors { get => data[@"Control Panel\Colors"];  }


        private IniData data;


        public ThemeFile(string file)
        {
            FileName = file;
            var parser = new FileIniDataParser();
            data = parser.ReadFile(file);
        }

        public void Save()
        {
            var parser = new FileIniDataParser();
            parser.WriteFile(FileName, data);

            
        }

    }
}
