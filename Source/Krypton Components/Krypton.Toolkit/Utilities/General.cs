#region BSD 3-Clause License
// *****************************************************************************
// BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit)
// by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
//  Version 6.0.0  
// *****************************************************************************
#endregion

namespace Krypton.Toolkit
{
    /// <summary>Specifies constants defining the default button on a <seealso cref="KryptonMessageBox"/>.</summary>
    public enum KryptonMessageBoxDefaultButton
    {
        /// <summary>The first button on the message box is the default button.</summary>
        Button1 = 0,
        /// <summary>The second button on the message box is the default button.</summary>
        Button2 = 256,
        /// <summary>The third button on the message box is the default button.</summary>
        Button3 = 512,
        /// <summary>Specifies that the Help button on the message box should be the default button.</summary>
        Button4 = 768
    }
}