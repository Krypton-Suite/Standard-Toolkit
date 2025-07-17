#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Ribbon;

/// <summary>
/// Represents a ribbon group theme combo box.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonRibbonGroupThemeComboBox), "ToolboxBitmaps.KryptonRibbonGroupComboBox.bmp")]
[Designer(typeof(KryptonRibbonGroupThemeComboBoxDesigner))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultEvent("SelectedTextChanged")]
[DefaultProperty(nameof(Text))]
public class KryptonRibbonGroupThemeComboBox : KryptonRibbonGroupComboBox, IKryptonThemeSelectorBase
{
    // TODO: grouped Ribbon controls do expose designers, needs a closer look

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

    /// <summary>Initializes a new instance of the <see cref="KryptonRibbonGroupThemeComboBox" /> class.</summary>
    public KryptonRibbonGroupThemeComboBox()
    {
        _manager = new KryptonManager();
        DropDownStyle = ComboBoxStyle.DropDownList;

        Items.Clear();
        Items.AddRange(CommonHelperThemeSelectors.GetThemesArray());

        // Sets the intial palette from either global or DefaultPalette property
        SelectedIndex = CommonHelperThemeSelectors.GetInitialSelectedIndex(DefaultPalette, _manager, Items);

        // React to theme changes from outside this control.
        KryptonManager.GlobalPaletteChanged += KryptonManagerGlobalPaletteChanged;
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
    /// This method will run when the KryptonManager.GlobalPaletteChanged event is fired.<br/>
    /// It will synchronize the SelectedIndex with the newly assigned Global Palette.
    /// </summary>
    /// <param name="sender">Object that initiated the call.</param>
    /// <param name="e">Eventargs object data (not used).</param>
    private void KryptonManagerGlobalPaletteChanged(object? sender, EventArgs e)
    {
        /*
         * Only executes when fully initialized.
         * OnHandleCreated could not be used here since this control derives from Component.
         */

        if (ComboBox is not null)
        {
            SelectedIndex = CommonHelperThemeSelectors.KryptonManagerGlobalPaletteChanged(_isLocalUpdate, ref _isExternalUpdate, SelectedIndex, Items);
        }
    }

    #endregion

    #region Protected Overrides
    /// <inheritdoc />
    protected override void OnSelectedIndexChanged(EventArgs e)
    {
        if (!CommonHelperThemeSelectors.OnSelectedIndexChanged(ref _isLocalUpdate, _isExternalUpdate, ref _defaultPalette, Text, _manager, _kryptonCustomPalette))
        {
            //theme change went wrong, make the active theme the selected theme in the list.
            SelectedIndex = CommonHelperThemeSelectors.GetPaletteIndex(Items, _manager.GlobalPaletteMode);
        }

        base.OnSelectedIndexChanged(e);
    }
    #endregion

    #region Removed Designer

    /// <summary>Gets and sets the text associated with the control.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    /// <summary>Gets or sets the format specifier characters that indicate how a value is to be Displayed.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string FormatString 
    {
        get => base.FormatString;
        set => base.FormatString = value;
    }

    /// <summary>Gets and sets the appearance and functionality of the KryptonComboBox.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override ComboBoxStyle DropDownStyle
    {
        get => base.DropDownStyle;
        set => base.DropDownStyle = value;
    }

    /// <summary>Gets or sets the items in the KryptonComboBox.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override ComboBox.ObjectCollection Items => base.Items;

    /// <summary>Gets or sets the StringCollection to use when the AutoCompleteSource property is set to CustomSource.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override AutoCompleteStringCollection AutoCompleteCustomSource
    {
        get => base.AutoCompleteCustomSource;
        set => base.AutoCompleteCustomSource = value;
    }

    /// <summary>Gets or sets the text completion behavior of the combobox.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override AutoCompleteMode AutoCompleteMode
    {
        get => base.AutoCompleteMode;
        set => base.AutoCompleteMode = value;
    }

    /// <summary>Gets or sets the autocomplete source, which can be one of the values from AutoCompleteSource enumeration.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override AutoCompleteSource AutoCompleteSource
    {
        get => base.AutoCompleteSource;
        set => base.AutoCompleteSource = value;
    }
    #endregion
}