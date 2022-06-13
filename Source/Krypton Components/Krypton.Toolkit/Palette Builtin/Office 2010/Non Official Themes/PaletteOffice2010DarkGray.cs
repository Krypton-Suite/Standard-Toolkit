using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Toolkit
{
    public class PaletteOffice2010DarkGray : PaletteOffice2010Base
    {
        public PaletteOffice2010DarkGray(Color[] schemeColors, ImageList checkBoxList, ImageList galleryButtonList, Image[] radioButtonArray, Color[] trackBarColors) : base(schemeColors, checkBoxList, galleryButtonList, radioButtonArray, trackBarColors)
        {
        }

        public override Image GetContextMenuSubMenuImage()
        {
            throw new NotImplementedException();
        }
    }
}
