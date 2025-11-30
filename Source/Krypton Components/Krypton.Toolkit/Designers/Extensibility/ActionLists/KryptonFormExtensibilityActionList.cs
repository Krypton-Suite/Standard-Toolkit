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
/// Action list for the KryptonForm control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonFormExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonForm? _form;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonFormExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonFormExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _form = (KryptonForm?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the palette to be applied.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _form?.PaletteMode ?? PaletteMode.Global;
        set => SetPropertyValue(_form!, nameof(PaletteMode), PaletteMode, value, v => _form!.PaletteMode = v);
    }

    /// <summary>
    /// Gets and sets the form border style.
    /// </summary>
    public FormBorderStyle FormBorderStyle
    {
        get => _form?.FormBorderStyle ?? FormBorderStyle.Sizable;
        set => SetPropertyValue(_form!, nameof(FormBorderStyle), FormBorderStyle, value, v => _form!.FormBorderStyle = v);
    }

    /// <summary>
    /// Gets and sets the window state.
    /// </summary>
    public FormWindowState WindowState
    {
        get => _form?.WindowState ?? FormWindowState.Normal;
        set => SetPropertyValue(_form!, nameof(WindowState), WindowState, value, v => _form!.WindowState = v);
    }

    /// <summary>
    /// Gets and sets the start position.
    /// </summary>
    public FormStartPosition StartPosition
    {
        get => _form?.StartPosition ?? FormStartPosition.WindowsDefaultLocation;
        set => SetPropertyValue(_form!, nameof(StartPosition), StartPosition, value, v => _form!.StartPosition = v);
    }

    /// <summary>
    /// Gets and sets whether the form is maximizable.
    /// </summary>
    public bool MaximizeBox
    {
        get => _form?.MaximizeBox ?? true;
        set => SetPropertyValue(_form!, nameof(MaximizeBox), MaximizeBox, value, v => _form!.MaximizeBox = v);
    }

    /// <summary>
    /// Gets and sets whether the form is minimizable.
    /// </summary>
    public bool MinimizeBox
    {
        get => _form?.MinimizeBox ?? true;
        set => SetPropertyValue(_form!, nameof(MinimizeBox), MinimizeBox, value, v => _form!.MinimizeBox = v);
    }

    /// <summary>
    /// Gets and sets whether the form shows in taskbar.
    /// </summary>
    public bool ShowInTaskbar
    {
        get => _form?.ShowInTaskbar ?? true;
        set => SetPropertyValue(_form!, nameof(ShowInTaskbar), ShowInTaskbar, value, v => _form!.ShowInTaskbar = v);
    }

    /// <summary>
    /// Gets and sets whether the form is topmost.
    /// </summary>
    public bool TopMost
    {
        get => _form?.TopMost ?? false;
        set => SetPropertyValue(_form!, nameof(TopMost), TopMost, value, v => _form!.TopMost = v);
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
        if (_form != null)
        {
            // Add the list of Form specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(FormBorderStyle), @"Border Style", @"Appearance", @"Form border style"));
            actions.Add(new DesignerActionPropertyItem(nameof(WindowState), @"Window State", @"Appearance", @"Initial window state"));
            actions.Add(new DesignerActionPropertyItem(nameof(StartPosition), @"Start Position", @"Appearance", @"Form start position"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(MaximizeBox), @"Maximize Box", @"Behavior", @"Show maximize button"));
            actions.Add(new DesignerActionPropertyItem(nameof(MinimizeBox), @"Minimize Box", @"Behavior", @"Show minimize button"));
            actions.Add(new DesignerActionPropertyItem(nameof(ShowInTaskbar), @"Show In Taskbar", @"Behavior", @"Show in taskbar"));
            actions.Add(new DesignerActionPropertyItem(nameof(TopMost), @"Top Most", @"Behavior", @"Stay on top"));
        }

        return actions;
    }
    #endregion
}
