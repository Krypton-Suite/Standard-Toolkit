#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Toolkit snap-layout menu settings for every form that inherits <see cref="KryptonForm.SnapLayouts"/>.
/// </summary>
/// <remarks>
/// Edit this object on a <see cref="KryptonManager"/> in the property grid, or use
/// <see cref="KryptonManager.SnapLayoutValues"/> from code. The menu reads the values when it opens.
/// </remarks>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class SnapLayoutValues : Storage
{
    #region Instance Fields

    private KryptonSnapLayoutMode _mode = KryptonSnapLayoutMode.System;
    private float _cornerRounding = 8f;
    private Size _cardSize = Size.Empty;
    private bool _showIcon = true;
    private bool _showPreview = true;
    private bool _showPreviewGhost;
    private float _previewOpacity = 0.45f;
    private bool _showShadow = true;
    private Color _shadowColor = Color.Empty;
    private float _shadowOpacity = 0.18f;
    private int _shadowOffset = 3;
    private int _shadowBlur;
    private bool _includeBuiltinLayouts = true;
    private bool _rememberGroups = true;
    private bool _showAssist = true;
    private readonly KryptonSnapLayoutCardCollection _customLayouts = new KryptonSnapLayoutCardCollection();

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="SnapLayoutValues"/> class.
    /// </summary>
    public SnapLayoutValues() => Reset();

    /// <summary>
    /// Resets every snap-layout setting to its default.
    /// </summary>
    public void Reset()
    {
        ResetMode();
        ResetCornerRounding();
        ResetCardSize();
        ResetShowIcon();
        ResetShowPreview();
        ResetShowPreviewGhost();
        ResetPreviewOpacity();
        ResetShowShadow();
        ResetShadowColor();
        ResetShadowOpacity();
        ResetShadowOffset();
        ResetShadowBlur();
        ResetIncludeBuiltinLayouts();
        ResetRememberGroups();
        ResetShowAssist();
        ResetCustomLayouts();
    }

    #endregion

    #region Public

    /// <summary>
    /// Occurs when <see cref="Mode"/> changes.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public event EventHandler? ModeChanged;

    /// <summary>
    /// Gets or sets which maximize-button layout menu a form uses while its own
    /// <see cref="KryptonForm.SnapLayouts"/> is <see cref="KryptonSnapLayoutMode.Inherit"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="KryptonSnapLayoutMode.System"/> (the default) keeps the Windows 11 snap menu.
    /// <see cref="KryptonSnapLayoutMode.Toolkit"/> draws the palette menu and snaps that form.
    /// <see cref="KryptonSnapLayoutMode.None"/> is maximize and restore only.
    /// <see cref="KryptonSnapLayoutMode.Inherit"/> is stored as <see cref="KryptonSnapLayoutMode.System"/>.
    /// </remarks>
    [Category(@"Visuals")]
    [Description(@"Maximize-button layout menu for forms that inherit: System (Windows 11), Toolkit (palette), or None.")]
    [DefaultValue(KryptonSnapLayoutMode.System)]
    public KryptonSnapLayoutMode Mode
    {
        get => _mode;

        set
        {
            if (value == KryptonSnapLayoutMode.Inherit)
            {
                value = KryptonSnapLayoutMode.System;
            }

            if (_mode != value)
            {
                _mode = value;
                ModeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private bool ShouldSerializeMode() => Mode != KryptonSnapLayoutMode.System;

    private void ResetMode() => Mode = KryptonSnapLayoutMode.System;

    /// <summary>
    /// Gets or sets the corner radius, in pixels, of the toolkit snap-layout menu.
    /// </summary>
    /// <remarks>Zero draws square corners. Values below zero are stored as zero. The default is 8.</remarks>
    [Category(@"Visuals")]
    [Description(@"Corner radius, in pixels, of the toolkit snap-layout menu. Zero is square.")]
    [DefaultValue(8f)]
    public float CornerRounding
    {
        get => _cornerRounding;

        set
        {
            if (value < 0f)
            {
                value = 0f;
            }

            if (Math.Abs(_cornerRounding - value) > 0.01f)
            {
                _cornerRounding = value;
            }
        }
    }

    private bool ShouldSerializeCornerRounding() => Math.Abs(CornerRounding - 8f) > 0.01f;

    private void ResetCornerRounding() => CornerRounding = 8f;

    /// <summary>
    /// Gets or sets the preferred size of one toolkit snap-layout card.
    /// </summary>
    /// <remarks>
    /// <see cref="Size.Empty"/> sizes the cards to the work area and wraps extra cards onto another row.
    /// A non-positive width or height is stored as <see cref="Size.Empty"/>.
    /// </remarks>
    [Category(@"Visuals")]
    [Description(@"Preferred size of one toolkit snap-layout card. Empty sizes and wraps the cards to the work area.")]
    [DefaultValue(typeof(Size), "0, 0")]
    public Size CardSize
    {
        get => _cardSize;

        set
        {
            if (value.Width <= 0 || value.Height <= 0)
            {
                value = Size.Empty;
            }

            if (_cardSize != value)
            {
                _cardSize = value;
            }
        }
    }

    private bool ShouldSerializeCardSize() => CardSize != Size.Empty;

    private void ResetCardSize() => CardSize = Size.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether each toolkit snap-layout zone draws an application icon.
    /// </summary>
    /// <remarks>The default is <see langword="true"/>.</remarks>
    [Category(@"Visuals")]
    [Description(@"Show this form's icon in each toolkit snap-layout zone.")]
    [DefaultValue(true)]
    public bool ShowIcon
    {
        get => _showIcon;
        set => _showIcon = value;
    }

    private bool ShouldSerializeShowIcon() => !ShowIcon;

    private void ResetShowIcon() => ShowIcon = true;

    /// <summary>
    /// Gets or sets a value indicating whether hovering a zone previews the snap rectangle.
    /// </summary>
    /// <remarks>The default is <see langword="true"/>.</remarks>
    [Category(@"Visuals")]
    [Description(@"Show a palette preview of the snap rectangle while the pointer is over a zone.")]
    [DefaultValue(true)]
    public bool ShowPreview
    {
        get => _showPreview;
        set => _showPreview = value;
    }

    private bool ShouldSerializeShowPreview() => !ShowPreview;

    private void ResetShowPreview() => ShowPreview = true;

    /// <summary>
    /// Gets or sets a value indicating whether the zone preview ghosts this form's window contents.
    /// </summary>
    /// <remarks>
    /// The default is <see langword="false"/>. When <see langword="true"/> and <see cref="ShowPreview"/> is on,
    /// the preview draws a scaled capture of the owning form in the hot zone instead of the palette fill and icon.
    /// </remarks>
    [Category(@"Visuals")]
    [Description(@"Ghost this form's window contents in the snap zone preview. Requires ShowPreview.")]
    [DefaultValue(false)]
    public bool ShowPreviewGhost
    {
        get => _showPreviewGhost;
        set => _showPreviewGhost = value;
    }

    private bool ShouldSerializeShowPreviewGhost() => ShowPreviewGhost;

    private void ResetShowPreviewGhost() => ShowPreviewGhost = false;

    /// <summary>
    /// Gets or sets the opacity of the snap-layout preview, from 0 (invisible) to 1 (opaque).
    /// </summary>
    /// <remarks>The default is 0.45. Zero hides the preview. Values outside 0..1 are stored at the nearest end.</remarks>
    [Category(@"Visuals")]
    [Description(@"Opacity of the toolkit snap-layout preview, from 0 (invisible) to 1 (opaque).")]
    [DefaultValue(0.45f)]
    public float PreviewOpacity
    {
        get => _previewOpacity;

        set
        {
            if (value < 0f)
            {
                value = 0f;
            }
            else if (value > 1f)
            {
                value = 1f;
            }

            if (Math.Abs(_previewOpacity - value) > 0.001f)
            {
                _previewOpacity = value;
            }
        }
    }

    private bool ShouldSerializePreviewOpacity() => Math.Abs(PreviewOpacity - 0.45f) > 0.001f;

    private void ResetPreviewOpacity() => PreviewOpacity = 0.45f;

    /// <summary>
    /// Gets or sets a value indicating whether the toolkit snap-layout menu draws a drop shadow.
    /// </summary>
    /// <remarks>The default is <see langword="true"/>.</remarks>
    [Category(@"Visuals")]
    [Description(@"Draw a drop shadow behind the toolkit snap-layout menu.")]
    [DefaultValue(true)]
    public bool ShowShadow
    {
        get => _showShadow;
        set => _showShadow = value;
    }

    private bool ShouldSerializeShowShadow() => !ShowShadow;

    private void ResetShowShadow() => ShowShadow = true;

    /// <summary>
    /// Gets or sets the colour of the toolkit snap-layout shadow.
    /// </summary>
    /// <remarks><see cref="Color.Empty"/> uses black. The default is empty.</remarks>
    [Category(@"Visuals")]
    [Description(@"Colour of the toolkit snap-layout shadow. Empty uses black.")]
    [KryptonDefaultColor]
    public Color ShadowColor
    {
        get => _shadowColor;
        set => _shadowColor = value;
    }

    private bool ShouldSerializeShadowColor() => ShadowColor != Color.Empty;

    private void ResetShadowColor() => ShadowColor = Color.Empty;

    /// <summary>
    /// Gets or sets the opacity of the toolkit snap-layout shadow, from 0 to 1.
    /// </summary>
    /// <remarks>The default is 0.18. Zero hides the shadow. Values outside 0..1 are stored at the nearest end.</remarks>
    [Category(@"Visuals")]
    [Description(@"Opacity of the toolkit snap-layout shadow, from 0 (invisible) to 1 (opaque).")]
    [DefaultValue(0.18f)]
    public float ShadowOpacity
    {
        get => _shadowOpacity;

        set
        {
            if (value < 0f)
            {
                value = 0f;
            }
            else if (value > 1f)
            {
                value = 1f;
            }

            if (Math.Abs(_shadowOpacity - value) > 0.001f)
            {
                _shadowOpacity = value;
            }
        }
    }

    private bool ShouldSerializeShadowOpacity() => Math.Abs(ShadowOpacity - 0.18f) > 0.001f;

    private void ResetShadowOpacity() => ShadowOpacity = 0.18f;

    /// <summary>
    /// Gets or sets how far the toolkit snap-layout shadow drops, in pixels down and to the right.
    /// </summary>
    /// <remarks>The default is 3. Values outside 0..32 are stored at the nearest end.</remarks>
    [Category(@"Visuals")]
    [Description(@"Drop of the toolkit snap-layout shadow, in pixels down and to the right.")]
    [DefaultValue(3)]
    public int ShadowOffset
    {
        get => _shadowOffset;

        set
        {
            if (value < 0)
            {
                value = 0;
            }
            else if (value > 32)
            {
                value = 32;
            }

            if (_shadowOffset != value)
            {
                _shadowOffset = value;
            }
        }
    }

    private bool ShouldSerializeShadowOffset() => ShadowOffset != 3;

    private void ResetShadowOffset() => ShadowOffset = 3;

    /// <summary>
    /// Gets or sets how far the toolkit snap-layout shadow extends past the menu, in pixels.
    /// </summary>
    /// <remarks>The default is 0, which keeps the shadow on the drop only. Values outside 0..32 are stored at the nearest end.</remarks>
    [Category(@"Visuals")]
    [Description(@"Halo of the toolkit snap-layout shadow, in pixels outside the menu. Zero keeps the drop only.")]
    [DefaultValue(0)]
    public int ShadowBlur
    {
        get => _shadowBlur;

        set
        {
            if (value < 0)
            {
                value = 0;
            }
            else if (value > 32)
            {
                value = 32;
            }

            if (_shadowBlur != value)
            {
                _shadowBlur = value;
            }
        }
    }

    private bool ShouldSerializeShadowBlur() => ShadowBlur != 0;

    private void ResetShadowBlur() => ShadowBlur = 0;

    /// <summary>
    /// Gets or sets a value indicating whether the toolkit snap menu includes the builtin cards.
    /// </summary>
    /// <remarks>
    /// The default is <see langword="true"/>. Set this to <see langword="false"/> to show only
    /// <see cref="CustomLayouts"/>. An empty custom list and <see langword="false"/> shows no menu.
    /// </remarks>
    [Category(@"Visuals")]
    [Description(@"Include the builtin snap-layout cards. False shows only CustomLayouts.")]
    [DefaultValue(true)]
    public bool IncludeBuiltinLayouts
    {
        get => _includeBuiltinLayouts;
        set => _includeBuiltinLayouts = value;
    }

    private bool ShouldSerializeIncludeBuiltinLayouts() => !IncludeBuiltinLayouts;

    private void ResetIncludeBuiltinLayouts() => IncludeBuiltinLayouts = true;

    /// <summary>
    /// Gets or sets a value indicating whether toolkit snaps remember in-process Snap Groups.
    /// </summary>
    /// <remarks>
    /// The default is <see langword="true"/>. When two or more live <see cref="KryptonForm"/> instances
    /// share a card, the menu shows a Group caption and can restore them together.
    /// </remarks>
    [Category(@"Visuals")]
    [Description(@"Remember in-process Snap Groups of peer KryptonForms and restore them from the menu.")]
    [DefaultValue(true)]
    public bool RememberGroups
    {
        get => _rememberGroups;
        set => _rememberGroups = value;
    }

    private bool ShouldSerializeRememberGroups() => !RememberGroups;

    private void ResetRememberGroups() => RememberGroups = true;

    /// <summary>
    /// Gets or sets a value indicating whether remaining zones offer peer forms after a toolkit snap.
    /// </summary>
    /// <remarks>
    /// The default is <see langword="true"/>. Only other open <see cref="KryptonForm"/> windows in this process are offered.
    /// </remarks>
    [Category(@"Visuals")]
    [Description(@"After a toolkit snap, offer peer KryptonForms for the remaining zones of the card.")]
    [DefaultValue(true)]
    public bool ShowAssist
    {
        get => _showAssist;
        set => _showAssist = value;
    }

    private bool ShouldSerializeShowAssist() => !ShowAssist;

    private void ResetShowAssist() => ShowAssist = true;

    /// <summary>
    /// Gets the custom snap-layout cards appended after the builtin cards.
    /// </summary>
    /// <remarks>
    /// Empty by default, which leaves the builtin menu unchanged.
    /// Add a card, then add zones whose edges are fractions of the work area from 0 to 1.
    /// The menu copies this list when it opens.
    /// </remarks>
    [Category(@"Visuals")]
    [Description(@"Snap-layout cards appended after the builtin cards. Each zone is a fraction of the work area.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof(KryptonSnapLayoutCardCollectionEditor), typeof(UITypeEditor))]
    [MergableProperty(false)]
    public KryptonSnapLayoutCardCollection CustomLayouts => _customLayouts;

    private bool ShouldSerializeCustomLayouts() => CustomLayouts.Count > 0;

    private void ResetCustomLayouts() => CustomLayouts.Clear();

    #endregion

    #region IsDefault

    /// <summary>
    /// Gets a value indicating if every snap-layout setting is at its default.
    /// </summary>
    [Browsable(false)]
    public override bool IsDefault => !ShouldSerializeMode()
                                      && !ShouldSerializeCornerRounding()
                                      && !ShouldSerializeCardSize()
                                      && !ShouldSerializeShowIcon()
                                      && !ShouldSerializeShowPreview()
                                      && !ShouldSerializeShowPreviewGhost()
                                      && !ShouldSerializePreviewOpacity()
                                      && !ShouldSerializeShowShadow()
                                      && !ShouldSerializeShadowColor()
                                      && !ShouldSerializeShadowOpacity()
                                      && !ShouldSerializeShadowOffset()
                                      && !ShouldSerializeShadowBlur()
                                      && !ShouldSerializeIncludeBuiltinLayouts()
                                      && !ShouldSerializeRememberGroups()
                                      && !ShouldSerializeShowAssist()
                                      && !ShouldSerializeCustomLayouts();

    #endregion
}
