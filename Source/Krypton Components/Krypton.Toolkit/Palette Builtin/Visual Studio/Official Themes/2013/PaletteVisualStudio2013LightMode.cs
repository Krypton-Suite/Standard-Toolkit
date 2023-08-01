using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Toolkit
{
    public abstract class PaletteVisualStudio2013LightMode : PaletteVisualStudioBase
    {
        protected PaletteVisualStudio2013LightMode(Color[] schemeColours, ImageList checkBoxList, ImageList galleryButtonList, Image[] radioButtonArray, Color[] trackBarColours) : base(schemeColours, checkBoxList, galleryButtonList, radioButtonArray, trackBarColours)
        {
        }
    }
}
