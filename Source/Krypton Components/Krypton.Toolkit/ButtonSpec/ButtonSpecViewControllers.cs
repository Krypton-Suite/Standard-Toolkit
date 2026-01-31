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
/// Stores a triple of controller references.
/// </summary>
public class ButtonSpecViewControllers
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecViewControllers class.
    /// </summary>
    /// <param name="mouseController">Mouse controller.</param>
    /// <param name="sourceController">Source controller.</param>
    /// <param name="keyController">Key controller.</param>
    public ButtonSpecViewControllers([DisallowNull] IMouseController mouseController,
        [DisallowNull] ISourceController sourceController,
        [DisallowNull] IKeyController keyController)
    {
        Debug.Assert(mouseController != null);
        Debug.Assert(sourceController != null);
        Debug.Assert(keyController != null);

        MouseController = mouseController;
        SourceController = sourceController;
        KeyController = keyController;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the mouse controller reference.
    /// </summary>
    public IMouseController? MouseController { get; }

    /// <summary>
    /// Gets the mouse controller reference.
    /// </summary>
    public ISourceController? SourceController { get; }

    /// <summary>
    /// Gets the mouse controller reference.
    /// </summary>
    public IKeyController? KeyController { get; }

    #endregion
}