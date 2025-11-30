// *****************************************************************************
// BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit)
//  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
//  © Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
// *****************************************************************************

namespace Krypton.Toolkit;

/// <summary>
/// Action list for the KryptonCommand control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonCommandExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonCommand? _command;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCommandExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonCommandExtensibilityActionList(ComponentDesigner owner)
        : base(owner)
    {
        _command = (KryptonCommand?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the command text.
    /// </summary>
    public string Text
    {
        get => _command?.Text ?? string.Empty;
        set => SetPropertyValue(_command!, nameof(Text), Text, value, v => _command!.Text = v);
    }

    /// <summary>
    /// Gets and sets the command extra text.
    /// </summary>
    public string ExtraText
    {
        get => _command?.ExtraText ?? string.Empty;
        set => SetPropertyValue(_command!, nameof(ExtraText), ExtraText, value, v => _command!.ExtraText = v);
    }

    /// <summary>
    /// Gets and sets the command image.
    /// </summary>
    public Image? ImageLarge
    {
        get => _command?.ImageLarge;
        set => SetPropertyValue(_command!, nameof(ImageLarge), ImageLarge, value, v => _command!.ImageLarge = v);
    }

    /// <summary>
    /// Gets and sets the command small image.
    /// </summary>
    public Image? ImageSmall
    {
        get => _command?.ImageSmall;
        set => SetPropertyValue(_command!, nameof(ImageSmall), ImageSmall, value, v => _command!.ImageSmall = v);
    }

    /// <summary>
    /// Gets and sets whether the command is enabled.
    /// </summary>
    public bool Enabled
    {
        get => _command?.Enabled ?? true;
        set => SetPropertyValue(_command!, nameof(Enabled), Enabled, value, v => _command!.Enabled = v);
    }

    /// <summary>
    /// Gets and sets whether the command is checked.
    /// </summary>
    public bool Checked
    {
        get => _command?.Checked ?? false;
        set => SetPropertyValue(_command!, nameof(Checked), Checked, value, v => _command!.Checked = v);
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
        if (_command != null)
        {
            // Add the list of Command specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), @"Text", @"Appearance", @"Command text"));
            actions.Add(new DesignerActionPropertyItem(nameof(ExtraText), @"Extra Text", @"Appearance", @"Command extra text"));
            actions.Add(new DesignerActionPropertyItem(nameof(ImageLarge), @"Image Large", @"Appearance", @"Large image"));
            actions.Add(new DesignerActionPropertyItem(nameof(ImageSmall), @"Image Small", @"Appearance", @"Small image"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Enabled), @"Enabled", @"Behavior", @"Command enabled"));
            actions.Add(new DesignerActionPropertyItem(nameof(Checked), @"Checked", @"Behavior", @"Initially checked"));
        }

        return actions;
    }
    #endregion
}
