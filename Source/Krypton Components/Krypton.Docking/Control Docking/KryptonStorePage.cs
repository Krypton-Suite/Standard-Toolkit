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
/// Acts as a placeholder for a KryptonPage so that it can be restored to this location at a later time.
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