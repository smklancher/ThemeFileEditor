﻿using IniParser.Model;
using IniParser.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeFileEditor
{
    class DefaultThemeData
    {
        public const string DEFAULT = @"; Copyright © Microsoft Corp.

[Theme]
; High Contrast White - IDS_THEME_DISPLAYNAME_HCWHITE
;DisplayName=NewTheme
;ThemeId={317DB6C3-AD2E-4713-8FF5-297D1036EDA7}

; Computer - SHIDI_SERVER
;[CLSID\{20D04FE0-3AEA-1069-A2D8-08002B30309D}\DefaultIcon]
;DefaultValue=%SystemRoot%\System32\imageres.dll,-109

; UsersFiles - SHIDI_USERFILES
;[CLSID\{59031A47-3F72-44A7-89C5-5595FE6B30EE}\DefaultIcon]
;DefaultValue=%SystemRoot%\System32\imageres.dll,-123

; Network - SHIDI_MYNETWORK
;[CLSID\{F02C1A0D-BE21-4350-88B0-7367FC96EF3C}\DefaultIcon]
;DefaultValue=%SystemRoot%\System32\imageres.dll,-25

; Recycle Bin - SHIDI_RECYCLERFULL SHIDI_RECYCLER
;[CLSID\{645FF040-5081-101B-9F08-00AA002F954E}\DefaultIcon]
;Full=%SystemRoot%\System32\imageres.dll,-54
;Empty=%SystemRoot%\System32\imageres.dll,-55

[Control Panel\Colors]
ActiveTitle=47 52 63
Background=125 151 193
ButtonFace=59 66 80
ButtonText=151 153 159
GrayText=151 153 159
Hilight=66 131 222
HilightText=125 151 193
HotTrackingColor=66 131 222
InactiveTitle=65 69 80
InactiveTitleText=151 153 159
TitleText=151 153 159
Window=127 141 163
WindowText=0 0 0
;WindowText=151 153 159
Scrollbar=47 52 63
Menu=83 92 112
WindowFrame=59 66 80
MenuText=151 153 159
ActiveBorder=59 66 80
InactiveBorder=47 52 63
AppWorkspace=47 52 63
ButtonShadow=47 52 63
ButtonHilight=47 52 63
ButtonDkShadow=47 52 63
ButtonLight=59 66 80
InfoText=151 153 159
InfoWindow=47 52 63
ButtonAlternateFace=47 52 63
GradientActiveTitle=59 66 80
GradientInactiveTitle=47 52 63
MenuHilight=66 131 222
MenuBar=59 66 80

[Control Panel\Desktop]
TileWallpaper=0
WallpaperStyle=10

[VisualStyles]
Path=%SystemRoot%\Resources\Themes\Aero\AeroLite.msstyles
ColorStyle=NormalColor
Size=NormalSize
HighContrast=4
; ColorizationColor=0X3B4250
AutoColorization=1
VisualStyleVersion=10


[MasterThemeSelector]
MTSM=RJSPBS

[Sounds]
; IDS_SCHEME_DEFAULT
SchemeName=@mmres.dll,-800";

        public static IniData DefaultData()
        {
            var parser = new IniDataParser();

            return parser.Parse(DEFAULT);
        }
    }
}
