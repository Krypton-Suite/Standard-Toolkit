using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Toolkit
{
    public abstract class PaletteVisualStudio2015DarkMode : PaletteVisualStudioBase
    {
        protected PaletteVisualStudio2015DarkMode(Color[] schemeColours, ImageList checkBoxList, ImageList galleryButtonList, Image[] radioButtonArray, Color[] trackBarColours) : base(schemeColours, checkBoxList, galleryButtonList, radioButtonArray, trackBarColours)
        {
        }
    }
}
