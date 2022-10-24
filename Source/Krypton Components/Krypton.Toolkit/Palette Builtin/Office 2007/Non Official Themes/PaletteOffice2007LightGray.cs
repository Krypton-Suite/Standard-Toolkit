﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    public class PaletteOffice2007LightGray : PaletteOffice2007Base
    {
        public PaletteOffice2007LightGray(Color[] schemeColors, ImageList checkBoxList, ImageList galleryButtonList, Image[] radioButtonArray, Color[] trackBarColors) : base(schemeColors, checkBoxList, galleryButtonList, radioButtonArray, trackBarColors)
        {
        }

        public override Image GetContextMenuSubMenuImage()
        {
            throw new NotImplementedException();
        }
    }
}
