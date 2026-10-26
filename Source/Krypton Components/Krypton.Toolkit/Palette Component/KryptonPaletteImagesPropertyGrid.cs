#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for palette property grid toolbar images.
/// </summary>
public class KryptonPaletteImagesPropertyGrid : Storage
{
    #region Instance Fields
    private PaletteRedirect? _redirect;
    private Image? _alphabetical;
    private Image? _categorized;
    private Image? _propertyPages;
    private Image? _events;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonPaletteImagesPropertyGrid"/> class.
    /// </summary>
    /// <param name="redirect">Redirector to inherit values from.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteImagesPropertyGrid(PaletteRedirect? redirect,
        NeedPaintHandler needPaint)
    {
        _redirect = redirect;
        NeedPaint = needPaint;
        _alphabetical = null;
        _categorized = null;
        _propertyPages = null;
        _events = null;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (_alphabetical == null) &&
                                      (_categorized == null) &&
                                      (_propertyPages == null) &&
                                      (_events == null);
    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        _alphabetical = _redirect?.GetPropertyGridImage(PropertyGridToolImage.Alphabetical);
        _categorized = _redirect?.GetPropertyGridImage(PropertyGridToolImage.Categorized);
        _propertyPages = _redirect?.GetPropertyGridImage(PropertyGridToolImage.PropertyPages);
        _events = _redirect?.GetPropertyGridImage(PropertyGridToolImage.Events);
    }
    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect? redirect) =>
        _redirect = redirect;
    #endregion

    #region Alphabetical
    /// <summary>
    /// Gets and sets the alphabetical (A-Z) sort toolbar image.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Image for the alphabetical sort toolbar button.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    [Editor(KryptonWinFormsDesignerSdk.ImageEditor, typeof(UITypeEditor))]
    public Image? Alphabetical
    {
        get => _alphabetical;

        set
        {
            if (_alphabetical != value)
            {
                _alphabetical = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the Alphabetical property to its default value.
    /// </summary>
    public void ResetAlphabetical() => Alphabetical = null;
    #endregion

    #region Categorized
    /// <summary>
    /// Gets and sets the categorized sort toolbar image.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Image for the categorized sort toolbar button.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    [Editor(KryptonWinFormsDesignerSdk.ImageEditor, typeof(UITypeEditor))]
    public Image? Categorized
    {
        get => _categorized;

        set
        {
            if (_categorized != value)
            {
                _categorized = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the Categorized property to its default value.
    /// </summary>
    public void ResetCategorized() => Categorized = null;
    #endregion

    #region PropertyPages
    /// <summary>
    /// Gets and sets the property pages toolbar image.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Image for the property pages toolbar button.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    [Editor(KryptonWinFormsDesignerSdk.ImageEditor, typeof(UITypeEditor))]
    public Image? PropertyPages
    {
        get => _propertyPages;

        set
        {
            if (_propertyPages != value)
            {
                _propertyPages = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the PropertyPages property to its default value.
    /// </summary>
    public void ResetPropertyPages() => PropertyPages = null;
    #endregion

    #region Events
    /// <summary>
    /// Gets and sets the events (lightning bolt) toolbar image.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Image for the events toolbar button.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    [Editor(KryptonWinFormsDesignerSdk.ImageEditor, typeof(UITypeEditor))]
    public Image? Events
    {
        get => _events;

        set
        {
            if (_events != value)
            {
                _events = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the Events property to its default value.
    /// </summary>
    public void ResetEvents() => Events = null;
    #endregion
}
