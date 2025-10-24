#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Represents the base class for all ribbon group containers.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
public abstract class KryptonRibbonGroupContainer : KryptonRibbonGroupItem,
    IRibbonGroupContainer
{
    #region Identity
    /// <summary>
    /// Initialise a new instance of the KryptonRibbonGroupContainer class.
    /// </summary>
    protected KryptonRibbonGroupContainer()
    {
    }
    #endregion

    #region Public
        
    /// <summary>
    /// Gets access to the parent group instance.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual KryptonRibbonGroup? RibbonGroup { get; set; }

    /// <summary>
    /// Gets an array of all the contained components.
    /// </summary>
    /// <returns>Array of child components.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual Component[] GetChildComponents() => Array.Empty<Component>();

    #endregion
}