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
/// Invisible placeholder left at the original tab/cell index while the real page is shown elsewhere.
/// <see cref="StoreName"/> scopes clears (<c>ClearDockedStoredPages</c>, etc.) so only the matching
/// host removes its placeholder when the page returns or is discarded.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonStorePage : KryptonPage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonStorePage class.
    /// </summary>
    /// <param name="uniqueName">UniqueName of the page this is placeholding.</param>
    /// <param name="storeName">Storage name associated with this page location.</param>
    public KryptonStorePage([DisallowNull] string uniqueName, string storeName)
    {
        Visible = false;
        UniqueName = uniqueName;
        StoreName = storeName;
    }
    #endregion

    #region Public
    /// <summary>
    /// As a placeholder this page is never visible.
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
    /// Gets the storage name associated with this page.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public string StoreName { get; }

    #endregion
}