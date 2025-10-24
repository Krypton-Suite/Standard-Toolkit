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

namespace Krypton.Toolkit;

/// <summary>
/// Details for context menu related events.
/// </summary>
public class ContextMenuArgs : CancelEventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ContextMenuArgs class.
    /// </summary>
    public ContextMenuArgs()
        : this(null, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ContextMenuArgs class.
    /// </summary>
    /// <param name="cms">Context menu strip that can be customized.</param>
    public ContextMenuArgs(ContextMenuStrip cms)
        : this(cms, null)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ContextMenuArgs class.
    /// </summary>
    /// <param name="kcm">KryptonContextMenu that can be customized.</param>
    public ContextMenuArgs(KryptonContextMenu kcm)
        : this(null, kcm)
    {
    }

    /// <summary>
    /// Initialize a new instance of the ContextMenuArgs class.
    /// </summary>
    /// <param name="cms">Context menu strip that can be customized.</param>
    /// <param name="kcm">KryptonContextMenu that can be customized.</param>
    public ContextMenuArgs(ContextMenuStrip? cms,
        KryptonContextMenu? kcm)
    {
        ContextMenuStrip = cms;
        KryptonContextMenu = kcm;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the context menu strip instance.
    /// </summary>
    public ContextMenuStrip? ContextMenuStrip { get; }

    /// <summary>
    /// Gets access to the KryptonContextMenu instance.
    /// </summary>
    public KryptonContextMenu? KryptonContextMenu { get; }

    #endregion
}