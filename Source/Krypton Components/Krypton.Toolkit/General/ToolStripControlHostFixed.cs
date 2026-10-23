#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2022 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary></summary>
[Category(@"code")]
[ToolboxItem(false)]
public class ToolStripControlHostFixed : ToolStripControlHost
{
    #region Identity

    /// <summary>Initializes a new instance of the <see cref="ToolStripControlHostFixed" /> class.</summary>
    /// <param name="childControl">The child control.</param>
    public ToolStripControlHostFixed(Control childControl) : base(childControl)
    {

    }

    /// <summary>Initializes a new instance of the <see cref="ToolStripControlHostFixed" /> class.</summary>
    public ToolStripControlHostFixed() : base(new Control())
    {

    }

    #endregion
}