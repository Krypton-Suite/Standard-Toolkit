#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class KryptonTaskDialogElementEmpty : KryptonTaskDialogElementBase
{
    public KryptonTaskDialogElementEmpty(KryptonTaskDialogDefaults taskDialogDefaults) 
        : base(taskDialogDefaults)
    {
        Panel.Width = Defaults.ClientWidth;
        Panel.Height = 100;
    }
}