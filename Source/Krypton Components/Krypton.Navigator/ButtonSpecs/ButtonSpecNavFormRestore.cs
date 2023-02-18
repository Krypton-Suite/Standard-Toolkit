﻿#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Navigator
{
    public class ButtonSpecNavFormRestore : ButtonSpecNavFixed
    {
        public ButtonSpecNavFormRestore(KryptonNavigator navigator) : base(navigator, PaletteButtonSpecStyle.FormRestore)
        {
        }

        public override bool GetVisible(PaletteBase? palette)
        {
            throw new NotImplementedException();
        }

        public override ButtonCheckState GetChecked(PaletteBase? palette)
        {
            throw new NotImplementedException();
        }

        public override ButtonEnabled GetEnabled(PaletteBase? palette)
        {
            throw new NotImplementedException();
        }
    }
}