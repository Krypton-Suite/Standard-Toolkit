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

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBeInternal
// ReSharper disable MemberCanBePrivate.Global

namespace Krypton.Toolkit;

/// <summary>
/// Button specification that can be assigned as any button type.
/// </summary>
public class ButtonSpecAny : ButtonSpec
{
    #region Instance Fields
    private bool _visible;
    private ButtonEnabled _enabled;
    private ButtonCheckState _checked;
    private ButtonEnabled _wasEnabled;
    private ButtonCheckState _wasChecked;
    private bool _showDrop;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the AnyButtonSpec class.
    /// </summary>
    public ButtonSpecAny()
    {
        _visible = true;
        _enabled = ButtonEnabled.Container;
        _checked = ButtonCheckState.NotCheckButton;
    }

    /// <summary>
    /// Make a clone of this object.
    /// </summary>
    /// <returns>New instance.</returns>
    public override object Clone()
    {
        var clone = (ButtonSpecAny)base.Clone();
        clone.Visible = Visible;
        clone.Enabled = Enabled;
        clone.Checked = Checked;
        clone.Type = Type;
        return clone;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => base.IsDefault &&
                                      Visible &&
                                      (Enabled == ButtonEnabled.Container) &&
                                      (Checked == ButtonCheckState.NotCheckButton);

    #endregion

    #region Visible
    /// <summary>
    /// Gets and sets if the button should be shown.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"Should the button be shown.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(true)]
    public bool Visible
    {
        get => _visible;

        set
        {
            if (_visible != value)
            {
                _visible = value;
                OnButtonSpecPropertyChanged(nameof(Visible));
            }
        }
    }

    /// <summary>
    /// Resets the Visible property to its default value.
    /// </summary>
    public void ResetVisible() => Visible = true;
    #endregion

    #region Enabled
    /// <summary>
    /// Gets and sets the button enabled state.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"Defines the button enabled state.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(ButtonEnabled.Container)]
    public ButtonEnabled Enabled
    {
        get => _enabled;

        set
        {
            if (_enabled != value)
            {
                _enabled = value;
                OnButtonSpecPropertyChanged(nameof(Enabled));
            }
        }
    }

    private bool ShouldSerializeEnabled() => Enabled != ButtonEnabled.Container;

    /// <summary>
    /// Resets the Enabled property to its default value.
    /// </summary>
    private void ResetEnabled() => Enabled = ButtonEnabled.Container;

    #endregion

    #region Checked
    /// <summary>
    /// Gets and sets if the button is checked or capable of being checked.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"Defines if the button is checked or capable of being checked.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(ButtonCheckState.NotCheckButton)]
    public ButtonCheckState Checked
    {
        get => _checked;

        set
        {
            if (_checked != value)
            {
                _checked = value;
                OnButtonSpecPropertyChanged(nameof(Checked));
            }
        }
    }

    private bool ShouldSerializeChecked() => Checked != ButtonCheckState.NotCheckButton;

    /// <summary>
    /// Resets the Checked property to its default value.
    /// </summary>
    public void ResetChecked() => Checked = ButtonCheckState.NotCheckButton;

    #endregion

    #region ShowDrop
    /// <summary>
    /// Gets and sets if the button is checked or capable of being checked.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"Displays a drop-down arrow on the button.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(false)]
    public bool ShowDrop
    {
        get => _showDrop;

        set
        {
            if (_showDrop != value)
            {
                _showDrop = value;
                OnButtonSpecPropertyChanged(nameof(ShowDrop));
            }
        }
    }

    private bool ShouldSerializeShowDrop() => !ShowDrop;

    /// <summary>
    /// Resets the Checked property to its default value.
    /// </summary>
    public void ResetShowDrop() => ShowDrop = false;

    #endregion

    #region KryptonCommand
    /// <summary>
    /// Gets and sets the associated KryptonCommand.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Command associated with the button.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(null)]
    public override KryptonCommand? KryptonCommand
    {
        get => base.KryptonCommand;

        set
        {
            if (base.KryptonCommand != value)
            {
                if (base.KryptonCommand == null)
                {
                    _wasEnabled = Enabled;
                    _wasChecked = Checked;
                }

                base.KryptonCommand = value;

                if (base.KryptonCommand == null)
                {
                    Enabled = _wasEnabled;
                    Checked = _wasChecked;
                }
            }
        }
    }
    #endregion

    #region Type
    /// <summary>
    /// Gets and sets the button type.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"Defines the type of button specification.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(PaletteButtonSpecStyle.Generic)]
    public PaletteButtonSpecStyle Type
    {
        get => ProtectedType;

        set
        {
            if (ProtectedType != value)
            {
                ProtectedType = value;
                OnButtonSpecPropertyChanged(nameof(Type));
            }
        }
    }

    private bool ShouldSerializeType() => Type != PaletteButtonSpecStyle.Generic;

    private void ResetType() => Type = PaletteButtonSpecStyle.Generic;

    #endregion

    #region CopyFrom
    /// <summary>
    /// Value copy form the provided source to ourself.
    /// </summary>
    /// <param name="source">Source instance.</param>
    public void CopyFrom(ButtonSpecAny source)
    {
        // Copy class specific values
        Visible = source.Visible;
        Enabled = source.Enabled;
        Checked = source.Checked;

        // Let base class copy the base values
        base.CopyFrom(source);
    }
    #endregion

    #region IButtonSpecValues
    /// <summary>
    /// Gets the button visible value.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button visibility.</returns>
    public override bool GetVisible(PaletteBase palette) => Visible;

    /// <summary>
    /// Gets the button enabled state.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button enabled state.</returns>
    public override ButtonEnabled GetEnabled(PaletteBase palette) => Enabled;

    /// <summary>
    /// Gets the button checked state.
    /// </summary>
    /// <param name="palette">Palette to use for inheriting values.</param>
    /// <returns>Button checked state.</returns>
    public override ButtonCheckState GetChecked(PaletteBase? palette) => Checked;

    #endregion

    #region Protected
    /// <summary>
    /// Raises the ButtonSpecPropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of the appearance property that has changed.</param>
    protected override void OnButtonSpecPropertyChanged(string propertyName)
    {
        base.OnButtonSpecPropertyChanged(propertyName);

        if (KryptonCommand != null)
        {
            switch (propertyName)
            {
                case nameof(KryptonCommand):
                    if (Checked != ButtonCheckState.NotCheckButton)
                    {
                        Checked = KryptonCommand.Checked ? ButtonCheckState.Checked : ButtonCheckState.Unchecked;
                    }

                    Enabled = KryptonCommand.Enabled ? ButtonEnabled.True : ButtonEnabled.False;
                    break;
                case nameof(Checked):
                    KryptonCommand.Checked = Checked == ButtonCheckState.Checked;
                    break;
            }
        }
    }

    /// <summary>
    /// Handles a change in the property of an attached command.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A PropertyChangedEventArgs that contains the event data.</param>
    protected override void OnCommandPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        base.OnCommandPropertyChanged(sender, e);

        switch (e.PropertyName)
        {
            case nameof(Checked):
                Checked = KryptonCommand?.Checked == true ? ButtonCheckState.Checked : ButtonCheckState.Unchecked;
                break;
            case nameof(Enabled):
                Enabled = KryptonCommand?.Enabled == true ? ButtonEnabled.True : ButtonEnabled.False;
                break;
        }
    }

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected override void OnClick(EventArgs e)
    {
        // Only if associated view is enabled to we perform an action
        if (GetViewEnabled())
        {
            // If a checked style button
            if (Checked != ButtonCheckState.NotCheckButton)
            {
                // Then invert the checked state
                Checked = Checked == ButtonCheckState.Unchecked
                    ? ButtonCheckState.Checked
                    : ButtonCheckState.Unchecked;
            }
        }

        base.OnClick(e);
    }
    #endregion
}