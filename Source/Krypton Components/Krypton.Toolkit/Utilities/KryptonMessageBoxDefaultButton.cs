#region BSD License
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

namespace Krypton.Toolkit;

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
    Button4 = 768,
    /// <summary>The accelerator button.</summary>
    Button5 = 1024
}