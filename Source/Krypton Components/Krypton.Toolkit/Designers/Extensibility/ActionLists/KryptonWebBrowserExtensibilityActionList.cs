#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Action list for the KryptonWebBrowser control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonWebBrowserExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonWebBrowser? _webBrowser;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonWebBrowserExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonWebBrowserExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _webBrowser = (KryptonWebBrowser?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the context menu strip.
    /// </summary>
    public ContextMenuStrip? ContextMenuStrip
    {
        get => _webBrowser?.ContextMenuStrip;
        set => SetPropertyValue(_webBrowser!, nameof(ContextMenuStrip), ContextMenuStrip, value, v => _webBrowser!.ContextMenuStrip = v);
    }
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_webBrowser != null)
        {
            // Add the list of WebBrowser specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(ContextMenuStrip), @"Context Menu Strip", @"Appearance", @"Context menu strip"));
        }

        return actions;
    }
    #endregion
}
