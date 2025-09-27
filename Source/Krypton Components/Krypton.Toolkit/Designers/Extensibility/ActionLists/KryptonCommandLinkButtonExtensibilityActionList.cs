// *****************************************************************************
// BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit)
//  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
//  © Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
// *****************************************************************************

namespace Krypton.Toolkit;

/// <summary>
/// Action list for the KryptonCommandLinkButton control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonCommandLinkButtonExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonCommandLinkButton? _commandLinkButton;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCommandLinkButtonExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonCommandLinkButtonExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _commandLinkButton = (KryptonCommandLinkButton?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the command link button style.
    /// </summary>
    public ButtonStyle ButtonStyle
    {
        get => _commandLinkButton?.ButtonStyle ?? ButtonStyle.Standalone;
        set => SetPropertyValue(_commandLinkButton!, nameof(ButtonStyle), ButtonStyle, value, v => _commandLinkButton!.ButtonStyle = v);
    }

    /// <summary>
    /// Gets and sets the command link button command.
    /// </summary>
    public IKryptonCommand? KryptonCommand
    {
        get => _commandLinkButton?.KryptonCommand;
        set => SetPropertyValue(_commandLinkButton!, nameof(KryptonCommand), KryptonCommand, value, v => _commandLinkButton!.KryptonCommand = v);
    }

    /// <summary>
    /// Gets and sets whether the command link button is enabled.
    /// </summary>
    public bool Enabled
    {
        get => _commandLinkButton?.Enabled ?? true;
        set => SetPropertyValue(_commandLinkButton!, nameof(Enabled), Enabled, value, v => _commandLinkButton!.Enabled = v);
    }

    /// <summary>
    /// Gets and sets whether the command link button is visible.
    /// </summary>
    public bool Visible
    {
        get => _commandLinkButton?.Visible ?? true;
        set => SetPropertyValue(_commandLinkButton!, nameof(Visible), Visible, value, v => _commandLinkButton!.Visible = v);
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
        if (_commandLinkButton != null)
        {
            // Add the list of CommandLinkButton specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(ButtonStyle), @"Button Style", @"Appearance", @"Button style"));
            actions.Add(new DesignerActionHeaderItem(@"Data"));
            actions.Add(new DesignerActionPropertyItem(nameof(KryptonCommand), @"Krypton Command", @"Data", @"Associated command"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Enabled), @"Enabled", @"Behavior", @"Button enabled"));
            actions.Add(new DesignerActionPropertyItem(nameof(Visible), @"Visible", @"Behavior", @"Button visible"));
        }

        return actions;
    }
    #endregion
}
