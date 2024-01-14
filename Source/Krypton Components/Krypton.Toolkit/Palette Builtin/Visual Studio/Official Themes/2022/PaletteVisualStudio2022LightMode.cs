#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2024. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    public abstract class PaletteVisualStudio2022LightMode : PaletteVisualStudioBase
    {
        protected PaletteVisualStudio2022LightMode(Color[] schemeColours, ImageList checkBoxList, ImageList galleryButtonList, Image?[] radioButtonArray, Color[] trackBarColours) 
            : base(schemeColours, checkBoxList, galleryButtonList, radioButtonArray, trackBarColours)
        {
            ThemeName = nameof(PaletteVisualStudio2022LightMode);
        }
    }
}
