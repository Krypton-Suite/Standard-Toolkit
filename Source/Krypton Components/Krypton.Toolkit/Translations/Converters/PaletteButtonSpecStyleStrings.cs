#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable InconsistentNaming
#pragma warning disable VSSpell001
namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="PaletteButtonSpecStyleConverter"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class PaletteButtonSpecStyleStrings : GlobalId
{
    #region Static Fields

    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_CLOSE = @"Close";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_CONTEXT = @"Context";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_NEXT = @"Next";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PREVIOUS = @"Previous";
    internal const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_GENERIC = @"Generic";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_LEFT = @"Arrow Left";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_RIGHT = @"Arrow Right";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_UP = @"Arrow Up";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_DOWN = @"Arrow Down";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_DROP_DOWN = @"drop-down";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PIN_VERTICAL = @"Pin Vertical";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PIN_HORIZONTAL = @"Pin Horizontal";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_CLOSE = @"Form Close";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_MAX = @"Form Max";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_MIN = @"Form Min";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_RESTORE = @"Form Restore";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_HELP = @"Form Help";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PENDANT_CLOSE = @"Pendant Close";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PENDANT_MIN = @"Pendant Min";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PENDANT_RESTORE = @"Pendant Restore";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_WORKSPACE_MAXIMIZE = @"Workspace Maximize";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_WORKSPACE_RESTORE = @"Workspace Restore";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_RIBBON_MINIMIZE = @"Ribbon Minimize";
    private const string DEFAULT_PALETTE_BUTTON_SPEC_STYLE_RIBBON_EXPAND = @"Ribbon Expand";

    #endregion

    #region Identity

    public PaletteButtonSpecStyleStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => Close.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_CLOSE) &&
                             Context.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_CONTEXT) &&
                             Next.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_NEXT) &&
                             Previous.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PREVIOUS) &&
                             Generic.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_GENERIC) &&
                             ArrowLeft.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_LEFT) &&
                             ArrowRight.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_RIGHT) &&
                             ArrowUp.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_UP) &&
                             ArrowDown.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_DOWN) &&
                             DropDown.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_DROP_DOWN) &&
                             PinVertical.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PIN_VERTICAL) &&
                             PinHorizontal.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PIN_HORIZONTAL) &&
                             FormClose.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_CLOSE) &&
                             FormMaximise.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_MAX) &&
                             FormMinimise.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_MIN) &&
                             FormRestore.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_RESTORE) &&
                             FormHelp.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_HELP) &&
                             PendantClose.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PENDANT_CLOSE) &&
                             PendantMinimise.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PENDANT_MIN) &&
                             PendantRestore.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PENDANT_RESTORE) &&
                             WorkspaceMaximise.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_WORKSPACE_MAXIMIZE) &&
                             WorkspaceRestore.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_WORKSPACE_RESTORE) &&
                             RibbonMinimise.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_RIBBON_MINIMIZE) &&
                             RibbonExpand.Equals(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_RIBBON_EXPAND);

    public void Reset()
    {
        Close = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_CLOSE;

        Context = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_CONTEXT;

        Next = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_NEXT;

        Previous = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PREVIOUS;

        Generic = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_GENERIC;

        ArrowLeft = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_LEFT;

        ArrowRight = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_RIGHT;

        ArrowUp = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_UP;

        ArrowDown = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_DOWN;

        DropDown = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_DROP_DOWN;

        PinVertical = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PIN_VERTICAL;

        PinHorizontal = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PIN_HORIZONTAL;

        FormClose = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_CLOSE;

        FormMaximise = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_MAX;

        FormMinimise = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_MIN;

        FormRestore = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_RESTORE;

        FormHelp = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_HELP;

        PendantClose = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PENDANT_CLOSE;

        PendantMinimise = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PENDANT_MIN;

        PendantRestore = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PENDANT_RESTORE;

        WorkspaceMaximise = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_WORKSPACE_MAXIMIZE;

        WorkspaceRestore = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_WORKSPACE_RESTORE;

        RibbonMinimise = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_RIBBON_MINIMIZE;

        RibbonExpand = DEFAULT_PALETTE_BUTTON_SPEC_STYLE_RIBBON_EXPAND;
    }

    /// <summary>Gets or sets the close palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The close palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_CLOSE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Close { get; set; }

    /// <summary>Gets or sets the context palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The context palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_CONTEXT)]
    [RefreshProperties(RefreshProperties.All)]
    public string Context { get; set; }

    /// <summary>Gets or sets the next palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The next palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_NEXT)]
    [RefreshProperties(RefreshProperties.All)]
    public string Next { get; set; }

    /// <summary>Gets or sets the previous palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The previous palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PREVIOUS)]
    [RefreshProperties(RefreshProperties.All)]
    public string Previous { get; set; }

    /// <summary>Gets or sets the generic palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The generic palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_GENERIC)]
    [RefreshProperties(RefreshProperties.All)]
    public string Generic { get; set; }

    /// <summary>Gets or sets the arrow left palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The arrow left palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_LEFT)]
    [RefreshProperties(RefreshProperties.All)]
    public string ArrowLeft { get; set; }

    /// <summary>Gets or sets the arrow right palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The arrow right palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_RIGHT)]
    [RefreshProperties(RefreshProperties.All)]
    public string ArrowRight { get; set; }

    /// <summary>Gets or sets the arrow up palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The arrow up palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_UP)]
    [RefreshProperties(RefreshProperties.All)]
    public string ArrowUp { get; set; }

    /// <summary>Gets or sets the arrow down palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The arrow down palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_ARROW_DOWN)]
    [RefreshProperties(RefreshProperties.All)]
    public string ArrowDown { get; set; }

    /// <summary>Gets or sets the drop-down palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The drop-down palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_DROP_DOWN)]
    [RefreshProperties(RefreshProperties.All)]
    public string DropDown { get; set; }

    /// <summary>Gets or sets the pin vertical palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The pin vertical palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PIN_VERTICAL)]
    [RefreshProperties(RefreshProperties.All)]
    public string PinVertical { get; set; }

    /// <summary>Gets or sets the pin horizontal palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The pin horizontal palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PIN_HORIZONTAL)]
    [RefreshProperties(RefreshProperties.All)]
    public string PinHorizontal { get; set; }

    /// <summary>Gets or sets the form close palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The form close palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_CLOSE)]
    [RefreshProperties(RefreshProperties.All)]
    public string FormClose { get; set; }

    /// <summary>Gets or sets the form maximise palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The form maximise palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_MAX)]
    [RefreshProperties(RefreshProperties.All)]
    public string FormMaximise { get; set; }

    /// <summary>Gets or sets the form minimise palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The form minimise palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_MIN)]
    [RefreshProperties(RefreshProperties.All)]
    public string FormMinimise { get; set; }

    /// <summary>Gets or sets the form restore palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The form restore palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_RESTORE)]
    [RefreshProperties(RefreshProperties.All)]
    public string FormRestore { get; set; }

    /// <summary>Gets or sets the form help palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The form help palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_FORM_HELP)]
    [RefreshProperties(RefreshProperties.All)]
    public string FormHelp { get; set; }

    /// <summary>Gets or sets the pendant close palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The pendant close palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PENDANT_CLOSE)]
    [RefreshProperties(RefreshProperties.All)]
    public string PendantClose { get; set; }

    /// <summary>Gets or sets the pendant minimise palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The pendant minimise palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PENDANT_MIN)]
    [RefreshProperties(RefreshProperties.All)]
    public string PendantMinimise { get; set; }

    /// <summary>Gets or sets the pendant restore palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The pendant restore palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_PENDANT_RESTORE)]
    [RefreshProperties(RefreshProperties.All)]
    public string PendantRestore { get; set; }

    /// <summary>Gets or sets the workspace maximise palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The workspace maximise palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_WORKSPACE_MAXIMIZE)]
    [RefreshProperties(RefreshProperties.All)]
    public string WorkspaceMaximise { get; set; }

    /// <summary>Gets or sets the workspace restore palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The workspace restore palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_WORKSPACE_RESTORE)]
    [RefreshProperties(RefreshProperties.All)]
    public string WorkspaceRestore { get; set; }

    /// <summary>Gets or sets the ribbon minimise palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The ribbon minimise palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_RIBBON_MINIMIZE)]
    [RefreshProperties(RefreshProperties.All)]
    public string RibbonMinimise { get; set; }

    /// <summary>Gets or sets the ribbon expand palette button spec style string.</summary>
    [Category(@"Visuals")]
    [Description(@"The ribbon expand palette button spec style.")]
    [DefaultValue(DEFAULT_PALETTE_BUTTON_SPEC_STYLE_RIBBON_EXPAND)]
    [RefreshProperties(RefreshProperties.All)]
    public string RibbonExpand { get; set; }

    #endregion
}