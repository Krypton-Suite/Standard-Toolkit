#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Docking;

/// <summary>
/// Stand-in page that mirrors appearance and state of a cached page in an auto-hidden group.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory("code")]
[DesignTimeVisible(false)]
public class KryptonAutoHiddenProxyPage : KryptonPage
{
    #region Identity
    /// <summary>
    /// Wraps the supplied page, copying tab text and last-visible-set state from the source.
    /// </summary>
    /// <param name="page">Page whose properties this proxy forwards; must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown when page is null.</exception>
    public KryptonAutoHiddenProxyPage(KryptonPage page)
    {

        // We are a proxy for this cached page reference
        Page = page ?? throw new ArgumentNullException(nameof(page));

        // Text property was updated by the base class constructor, so now we update the actual referenced class
        Page.Text = Text;

        // Fix for 822: https://github.com/Krypton-Suite/Standard-Toolkit/issues/822#issuecomment-2228211126

        Visible = page.Visible;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        Page.Dispose();

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// The underlying page whose properties and events this proxy forwards.
    /// </summary>
    public KryptonPage Page { get; }

    /// <summary>
    /// Tab text synchronized between this proxy and the wrapped page.
    /// </summary>
    [AllowNull]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string Text
    {
        // Check for null when initialising
        get => Page != null! ? Page.Text : base.Text;

        set
        {
            base.Text = value;
                
            // Check for null when initialising
            if (Page != null!)
            {
                Page.Text = value;
            }
        }
    }

    /// <summary>
    /// Title text forwarded to the wrapped page.
    /// </summary>
    [AllowNull]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Visible )]
    public override string TextTitle
    {
        get => Page.TextTitle;

        set => Page.TextTitle = value;
    }

    /// <summary>
    /// Description text forwarded to the wrapped page.
    /// </summary>
    [AllowNull]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
    public override string TextDescription
    {
        get => Page.TextDescription;

        set => Page.TextDescription = value;
    }

    /// <summary>
    /// Small tab image forwarded to the wrapped page.
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public override Bitmap? ImageSmall
    {
        get => Page.ImageSmall;

        set => Page.ImageSmall = value;
    }

    /// <summary>
    /// Medium tab image forwarded to the wrapped page.
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public override Bitmap? ImageMedium
    {
        get => Page.ImageMedium;

        set => Page.ImageMedium = value;
    }

    /// <summary>
    /// Large tab image forwarded to the wrapped page.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Bitmap? ImageLarge
    {
        get => Page.ImageLarge;

        set => Page.ImageLarge = value;
    }

    /// <summary>
    /// Tooltip image forwarded to the wrapped page.
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public override Bitmap? ToolTipImage
    {
        get => Page.ToolTipImage;

        set => Page.ToolTipImage = value;
    }

    /// <summary>
    /// Tooltip image transparency color forwarded to the wrapped page.
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public override Color ToolTipImageTransparentColor
    {
        get => Page.ToolTipImageTransparentColor;

        set => Page.ToolTipImageTransparentColor = value;
    }

    /// <summary>
    /// Tooltip title forwarded to the wrapped page.
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
    public override string ToolTipTitle
    {
        get => Page.ToolTipTitle;

        set => Page.ToolTipTitle = value;
    }

    /// <summary>
    /// Tooltip body text forwarded to the wrapped page.
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public override string ToolTipBody
    {
        get => Page.ToolTipBody;

        set => Page.ToolTipBody = value;
    }

    /// <summary>
    /// Tooltip label style forwarded to the wrapped page.
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public override LabelStyle ToolTipStyle
    {
        get => Page.ToolTipStyle;

        set => Page.ToolTipStyle = value;
    }

    /// <summary>
    /// Context menu shown on right-click, forwarded to the wrapped page.
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public override KryptonContextMenu? KryptonContextMenu
    {
        get => Page.KryptonContextMenu;

        set => Page.KryptonContextMenu = value;
    }

    /// <summary>
    /// Unique name forwarded to the wrapped page.
    /// </summary>
    [DisallowNull]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
    public override string UniqueName
    {
        get => Page.UniqueName;

        set => Page.UniqueName = value;
    }

    /// <summary>
    /// Returns mapped text from the wrapped page for the requested mapping.
    /// </summary>
    /// <param name="mapping">Text field to resolve.</param>
    /// <returns>Mapped text from the wrapped page.</returns>
    public override string GetTextMapping(MapKryptonPageText mapping) => Page.GetTextMapping(mapping);

    /// <summary>
    /// Returns mapped image from the wrapped page for the requested mapping.
    /// </summary>
    /// <param name="mapping">Image field to resolve.</param>
    /// <returns>Mapped image from the wrapped page, or null when none applies.</returns>
    public override Image? GetImageMapping(MapKryptonPageImage mapping) => Page.GetImageMapping(mapping);

    /// <summary>
    /// Page flags forwarded to the wrapped page.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override int Flags
    {
        get => Page.Flags;

        set => Page.Flags = value;
    }

    /// <summary>
    /// Sets the specified flags on the wrapped page.
    /// </summary>
    /// <param name="flags">Flags to enable on the wrapped page.</param>
    public override void SetFlags(KryptonPageFlags flags) => Page.SetFlags(flags);

    /// <summary>
    /// Clears the specified flags on the wrapped page.
    /// </summary>
    /// <param name="flags">Flags to disable on the wrapped page.</param>
    public override void ClearFlags(KryptonPageFlags flags) => Page.ClearFlags(flags);

    /// <summary>
    /// Reports whether all specified flags are set on the wrapped page.
    /// </summary>
    /// <param name="flags">Flags to test on the wrapped page.</param>
    /// <returns>True when every specified flag is set on the wrapped page; otherwise false.</returns>
    public override bool AreFlagsSet(KryptonPageFlags flags) => Page.AreFlagsSet(flags);

    /// <summary>
    /// Last-visible-set state forwarded to the wrapped page.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public override bool LastVisibleSet
    {
        get => Page.LastVisibleSet;

        set => Page.LastVisibleSet = value;
    }

    /// <summary>
    /// Forwards appearance property change notifications from the wrapped page.
    /// </summary>
    public override event PropertyChangedEventHandler? AppearancePropertyChanged
    {
        add => Page.AppearancePropertyChanged += value;

        remove => Page.AppearancePropertyChanged -= value;
    }
    #endregion
}