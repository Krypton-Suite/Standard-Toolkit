#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Encapsulates all backstage-related properties for the ribbon.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class PaletteRibbonBackstage
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRibbonBackstage class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    internal PaletteRibbonBackstage(KryptonRibbon ribbon)
    {
        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
    }
    #endregion

    #region Public Properties

    /// <summary>Gets a value indicating whether this instance is default.</summary>
    /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
    [Browsable(false)]
    public bool IsDefault => UseBackstageForAppButton && 
                             BackstagePages.Count == 0 && 
                             SelectedBackstagePage == null &&
                             !IsVisible && NavigationWidth == 500 && 
                             !AllowNavigationResize;

    /// <summary>
    /// Gets or sets whether the backstage view is shown when the application button is clicked.
    /// </summary>
    [Category("Behavior")]
    [Description("Determines if the backstage view is shown when the application button is clicked.")]
    [DefaultValue(false)]
    public bool UseBackstageForAppButton
    {
        get => _ribbon.UseBackstageForAppButton;
        set => _ribbon.UseBackstageForAppButton = value;
    }

    /// <summary>
    /// Gets the collection of backstage pages.
    /// </summary>
    [Category("Data")]
    [Description("Collection of backstage pages available in the ribbon.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof(KryptonRibbonBackstagePageCollectionEditor), typeof(UITypeEditor))]
    public KryptonRibbonBackstagePageCollection BackstagePages => _ribbon.BackstagePages;

    /// <summary>
    /// Gets or sets the currently selected backstage page.
    /// </summary>
    [Category("Behavior")]
    [Description("The currently selected backstage page.")]
    [DefaultValue(null)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonRibbonBackstagePage? SelectedBackstagePage
    {
        get => _ribbon.SelectedBackstagePage;
        set => _ribbon.SelectedBackstagePage = value;
    }

    /// <summary>
    /// Gets whether the backstage view is currently visible.
    /// </summary>
    [Category("Appearance")]
    [Description("Indicates whether the backstage view is currently visible.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsVisible => _ribbon.BackstageVisible;

    /// <summary>
    /// Gets the width of the navigation panel in pixels.
    /// </summary>
    [Category("Layout")]
    [Description("The width of the backstage navigation panel in pixels.")]
    [DefaultValue(500)]
    public int NavigationWidth { get; set; } = 500;

    /// <summary>
    /// Gets or sets whether the backstage navigation panel can be resized by the user.
    /// </summary>
    [Category("Behavior")]
    [Description("Determines if the navigation panel can be resized by the user.")]
    [DefaultValue(false)]
    public bool AllowNavigationResize { get; set; } = false;

    #endregion

    #region Public Methods
    /// <summary>
    /// Shows the backstage view.
    /// </summary>
    public void Show() => _ribbon.ShowBackstage();

    /// <summary>
    /// Shows the backstage view with keyboard activation state.
    /// </summary>
    /// <param name="keyboardActivated">True if activated by keyboard.</param>
    public void Show(bool keyboardActivated) => _ribbon.ShowBackstage(keyboardActivated);

    /// <summary>
    /// Hides the backstage view.
    /// </summary>
    public void Hide() => _ribbon.HideBackstage();

    /// <summary>
    /// Toggles the backstage view visibility.
    /// </summary>
    public void Toggle() => _ribbon.ToggleBackstage();
    #endregion

    #region Overrides
    /// <summary>
    /// Returns a string representation of the backstage configuration.
    /// </summary>
    /// <returns>String describing the backstage state.</returns>
    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion
}
