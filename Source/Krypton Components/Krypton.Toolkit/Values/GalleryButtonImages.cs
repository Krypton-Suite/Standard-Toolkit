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

namespace Krypton.Toolkit;

/// <summary>
/// Storage for gallery button state specific images.
/// </summary>
public class GalleryButtonImages : Storage
{
    #region Instance Fields
    private Image? _common;
    private Image? _disabled;
    private Image? _normal;
    private Image? _tracking;
    private Image? _pressed;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the GalleryButtonImages class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public GalleryButtonImages(NeedPaintHandler needPaint) 
    {
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Create the storage
        _common = null;
        _disabled = null;
        _normal = null;
        _tracking = null;
        _pressed = null;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (_common == null) &&
                                      (_disabled == null) &&
                                      (_normal == null) &&
                                      (_tracking == null) &&
                                      (_pressed == null);

    #endregion

    #region Common
    /// <summary>
    /// Gets and sets the common image that other gallery button images inherit from.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Common image that other gallery button images inherit from.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? Common
    {
        get => _common;

        set
        {
            if (_common != value)
            {
                _common = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the Common property to its default value.
    /// </summary>
    public void ResetCommon() => Common = null;
    #endregion

    #region Disabled
    /// <summary>
    /// Gets and sets the image for use when the gallery button is disabled.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Image for use when the gallery button is disabled.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? Disabled
    {
        get => _disabled;

        set
        {
            if (_disabled != value)
            {
                _disabled = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the Disabled property to its default value.
    /// </summary>
    public void ResetDisabled() => Disabled = null;
    #endregion

    #region Normal
    /// <summary>
    /// Gets and sets the image for use when the gallery button is normal.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Image for use when the gallery button is normal.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? Normal
    {
        get => _normal;

        set
        {
            if (_normal != value)
            {
                _normal = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the Normal property to its default value.
    /// </summary>
    public void ResetNormal() => Normal = null;
    #endregion

    #region Tracking
    /// <summary>
    /// Gets and sets the image for use when the gallery button is hot tracking.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Image for use when the gallery button is hot tracking.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? Tracking
    {
        get => _tracking;

        set
        {
            if (_tracking != value)
            {
                _tracking = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the Tracking property to its default value.
    /// </summary>
    public void ResetTracking() => Tracking = null;
    #endregion

    #region Pressed
    /// <summary>
    /// Gets and sets the image for use when the gallery button is pressed.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Image for use when the gallery button is pressed.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? Pressed
    {
        get => _pressed;

        set
        {
            if (_pressed != value)
            {
                _pressed = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the Pressed property to its default value.
    /// </summary>
    public void ResetPressed() => Pressed = null;
    #endregion
}