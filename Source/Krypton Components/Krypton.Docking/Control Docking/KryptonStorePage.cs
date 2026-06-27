#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Docking;

/// <summary>
/// Invisible placeholder that preserves a page unique name and storage location until the page is restored.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonStorePage : KryptonPage
{
    #region Identity
    /// <summary>
    /// Creates an invisible placeholder for the given page identity and docking storage location.
    /// </summary>
    /// <param name="uniqueName">Unique name of the page being stored.</param>
    /// <param name="storeName">Storage location label used when serializing this placeholder.</param>
    public KryptonStorePage([DisallowNull] string uniqueName, string storeName)
    {
        Visible = false;
        UniqueName = uniqueName;
        StoreName = storeName;
    }
    #endregion

    #region Public
    /// <summary>
    /// Always false for placeholders; assignments are ignored.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override bool LastVisibleSet
    {
        get => false;
        set { }
    }

    /// <summary>
    /// Storage location label that identifies where this placeholder belongs in the docking layout.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public string StoreName { get; }

    #endregion
}