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
/// Application-wide property grid toolbar images exposed via <see cref="KryptonManager.Images"/>.
/// </summary>
/// <remarks>
/// A <see langword="null"/> property value means “use the next fallback” (palette image, then stock WinForms).
/// Assign an image to override that button for every <see cref="KryptonPropertyGrid"/>.
/// </remarks>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class PropertyGridImageStorage : Storage
{
    #region Instance Fields
    private Image? _alphabetical;
    private Image? _categorized;
    private Image? _propertyPages;
    private Image? _events;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="PropertyGridImageStorage"/> class.
    /// </summary>
    public PropertyGridImageStorage() => Reset();

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : string.Empty;
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all images are unset (stock WinForms fallback).
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (_alphabetical == null) &&
                                      (_categorized == null) &&
                                      (_propertyPages == null) &&
                                      (_events == null);
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the alphabetical (A-Z) sort toolbar image.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Image for the alphabetical sort toolbar button. Null uses palette then stock WinForms.")]
    [DefaultValue(null)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    [Editor(KryptonWinFormsDesignerSdk.ImageEditor, typeof(UITypeEditor))]
    public Image? Alphabetical
    {
        get => _alphabetical;
        set => _alphabetical = value;
    }

    private bool ShouldSerializeAlphabetical() => _alphabetical != null;
    private void ResetAlphabetical() => _alphabetical = null;

    /// <summary>
    /// Gets and sets the categorized sort toolbar image.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Image for the categorized sort toolbar button. Null uses palette then stock WinForms.")]
    [DefaultValue(null)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    [Editor(KryptonWinFormsDesignerSdk.ImageEditor, typeof(UITypeEditor))]
    public Image? Categorized
    {
        get => _categorized;
        set => _categorized = value;
    }

    private bool ShouldSerializeCategorized() => _categorized != null;
    private void ResetCategorized() => _categorized = null;

    /// <summary>
    /// Gets and sets the property pages toolbar image.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Image for the property pages toolbar button. Null uses palette then stock WinForms.")]
    [DefaultValue(null)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    [Editor(KryptonWinFormsDesignerSdk.ImageEditor, typeof(UITypeEditor))]
    public Image? PropertyPages
    {
        get => _propertyPages;
        set => _propertyPages = value;
    }

    private bool ShouldSerializePropertyPages() => _propertyPages != null;
    private void ResetPropertyPages() => _propertyPages = null;

    /// <summary>
    /// Gets and sets the events (lightning bolt) toolbar image.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Image for the events toolbar button. Null uses palette then stock WinForms.")]
    [DefaultValue(null)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    [Editor(KryptonWinFormsDesignerSdk.ImageEditor, typeof(UITypeEditor))]
    public Image? Events
    {
        get => _events;
        set => _events = value;
    }

    private bool ShouldSerializeEvents() => _events != null;
    private void ResetEvents() => _events = null;
    #endregion

    #region Implementation
    /// <summary>
    /// Resets all images to unset so stock WinForms icons are used.
    /// </summary>
    public void Reset()
    {
        _alphabetical = null;
        _categorized = null;
        _propertyPages = null;
        _events = null;
    }

    /// <summary>
    /// Gets the manager override for the given toolbar image, or null when unset.
    /// </summary>
    /// <param name="toolImage">Which toolbar image is required.</param>
    /// <returns>The assigned image, or null to fall through.</returns>
    public Image? GetImage(PropertyGridToolImage toolImage)
    {
        switch (toolImage)
        {
            case PropertyGridToolImage.Alphabetical:
                return _alphabetical;
            case PropertyGridToolImage.Categorized:
                return _categorized;
            case PropertyGridToolImage.PropertyPages:
                return _propertyPages;
            case PropertyGridToolImage.Events:
                return _events;
            default:
                return null;
        }
    }
    #endregion
}
