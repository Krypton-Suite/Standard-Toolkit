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

/// <summary>
/// Event arguments for a FloatspaceAdding/FloatspaceRemoved event.
/// </summary>
public class FloatspaceEventArgs : EventArgs
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the FloatspaceEventArgs class.
    /// </summary>
    /// <param name="floatspace">Reference to new floatspace control instance.</param>
    /// <param name="element">Reference to docking floatspace element that is managing the floatspace control.</param>
    public FloatspaceEventArgs(KryptonFloatspace? floatspace,
        KryptonDockingFloatspace element)
    {
        FloatspaceControl = floatspace;
        FloatspaceElement = element;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets a reference to the KryptonFloatspace control..
    /// </summary>
    public KryptonFloatspace? FloatspaceControl { get; }

    /// <summary>
    /// Gets a reference to the KryptonDockingFloatspace that is managing the space control.
    /// </summary>
    public KryptonDockingFloatspace FloatspaceElement { get; }

    #endregion
}