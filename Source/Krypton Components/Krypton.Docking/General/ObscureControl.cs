#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Docking;

internal class ObscureControl : Control
{
    #region Protected
    /// <summary>
    /// Raises the PaintBackground event.
    /// </summary>
    /// <param name="e">An PaintEventArgs containing the event data.</param>
    protected override void OnPaintBackground(PaintEventArgs e)
    {
        // We do nothing, so the area underneath shows through
    }

    /// <summary>
    /// Raises the Paint event.
    /// </summary>
    /// <param name="e">An PaintEventArgs containing the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
        // We do nothing, so the area underneath shows through
    }
    #endregion
}