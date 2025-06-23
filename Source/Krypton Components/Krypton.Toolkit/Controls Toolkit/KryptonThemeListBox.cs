#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit;

/// <summary>Allows the user to change themes using a <see cref="KryptonListBox"/>.</summary>
/// <seealso cref="KryptonListBox" />
[Designer(typeof(KryptonStubDesigner))]
public class KryptonThemeListBox : KryptonListBox, IKryptonThemeSelectorBase
{
    #region Instance Fields

    /// <summary> When we change the palette, Krypton Manager will notify us that there was a change. Since we are changing it that notification can be skipped.</summary>
    private bool _isLocalUpdate = false;
    /// <summary> Suppress code execution in the SelectedIndexChanged event handler, when a theme change via the KManager has been performed.</summary>
    private bool _isExternalUpdate = false;
    /// <summary> Backing var for the DefaultPalette property.</summary>
    private PaletteMode _defaultPalette = PaletteMode.Global;
    /// <summary> Local Krypton Manager instance.</summary>
    private readonly KryptonManager _manager;
    /// <summary> User defined palette.</summary>
    private KryptonCustomPaletteBase? _kryptonCustomPalette = null;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonThemeListBox" /> class.</summary>
    public KryptonThemeListBox()
    {
        _manager = new KryptonManager();

        Items.Clear();
        Items.AddRange(CommonHelperThemeSelectors.GetThemesArray());

        // Sets the intial palette from either global or DefaultPalette property
        SelectedIndex = CommonHelperThemeSelectors.GetInitialSelectedIndex(DefaultPalette, _manager, Items);
    }

    #endregion

    #region Public

    /// <inheritdoc/>
    [Category(@"Visuals")]
    [Description(@"The custom assigned palette mode.")]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Obsolete("Deprecated and will be removed in V110. Set a global custom palette through 'ThemeManager.ApplyTheme(...)'.")]
    public KryptonCustomPaletteBase? KryptonCustomPalette 
    {
        get => _kryptonCustomPalette;
        set => _kryptonCustomPalette = value;
    }

    private void ResetKryptonCustomPalette() => _kryptonCustomPalette = null;
    private bool ShouldSerializeKryptonCustomPalette() => _kryptonCustomPalette is not null;

    /// <inheritdoc/>
    [Category(@"Visuals")]
    [Description(@"The default palette mode.")]
    [DefaultValue(PaletteMode.Global)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public PaletteMode DefaultPalette 
    {
        get => _defaultPalette;
        set => SelectedIndex = CommonHelperThemeSelectors.DefaultPaletteSetter(ref _defaultPalette, value, Items, SelectedIndex);
    }

    private void ResetDefaultPalette() => DefaultPalette = PaletteMode.Global;
    private bool ShouldSerializeDefaultPalette() => _defaultPalette != PaletteMode.Global;

    #endregion

    #region Implementation

    /// <summary>
    /// Routine that will be executed when the control is fully instantiated.
    /// </summary>
    /// <param name="e">EventArgs param. Not used in this implementation.</param>
    protected override void OnHandleCreated(EventArgs e)
    {
        // React to theme changes from outside this control.
        KryptonManager.GlobalPaletteChanged += KryptonManagerGlobalPaletteChanged;
        base.OnHandleCreated(e);
    }

    /// <summary>
    /// This method will run when the KryptonManager.GlobalPaletteChanged event is fired.<br/>
    /// It will synchronize the SelectedIndex with the newly assigned Global Palette.
    /// </summary>
    /// <param name="sender">Object that intiated the call.</param>
    /// <param name="e">Eventargs object data (not used).</param>
    private void KryptonManagerGlobalPaletteChanged(object? sender, EventArgs e)
    {
        SelectedIndex = CommonHelperThemeSelectors.KryptonManagerGlobalPaletteChanged(_isLocalUpdate, ref _isExternalUpdate, SelectedIndex, Items);
    }

    #endregion

    #region Protected Overrides

    /// <inheritdoc />
    protected override void OnSelectedIndexChanged(EventArgs e)
    {
        // The theme listbox needs a check first since SelectedItem is of type: object?
        string themeName = SelectedIndex > -1 && SelectedItem is string str && str.Length > 0
            ? str
            : string.Empty;

        if (!CommonHelperThemeSelectors.OnSelectedIndexChanged(ref _isLocalUpdate, _isExternalUpdate, ref _defaultPalette, themeName, _manager, _kryptonCustomPalette))
        {
            //theme change went wrong, make the active theme the selected theme in the list.
            SelectedIndex = CommonHelperThemeSelectors.GetPaletteIndex(Items, _manager.GlobalPaletteMode);
        }

        base.OnSelectedIndexChanged(e);
    }

    #endregion

    #region Removed Designer Visibility

    /// <summary>Gets and sets the text associated with the control.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    /// <summary>Gets or sets the format specifier characters that indicate how a value is to be Displayed.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new string FormatString 
    {
        get => base.FormatString;
        set => base.FormatString = value;
    }

    /// <summary>Gets the items of the KryptonListBox.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ListBox.ObjectCollection Items => base.Items;

    /// <summary>Gets and sets the selected index.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new int SelectedIndex 
    {
        get => base.SelectedIndex;
        set => base.SelectedIndex = value;
    }

    #endregion
}