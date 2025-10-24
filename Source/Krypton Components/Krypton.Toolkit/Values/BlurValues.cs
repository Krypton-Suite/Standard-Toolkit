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
/// 
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public class BlurValues : Storage
{
    #region statics
    private byte _opacity;
    private bool _blurWhenFocusLost;
    #endregion

    #region Events
#pragma warning disable 1591
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public event EventHandler? BlurWhenFocusLostChanged;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public event EventHandler? OpacityChanged;
#pragma warning restore 1591
    #endregion

    #region Identity
    /// <summary>
    /// 
    /// </summary>
    public BlurValues() => Reset();

    /// <summary>
    /// 
    /// </summary>
    public void Reset()
    {
        ResetOpacity();
        ResetBlurWhenFocusLost();
    }
    #endregion Identity

    private const byte OPACITY_DEFAULT = 80;
    /// <summary>
    /// </summary>
    [Description(@"Opacity Percentage to be applied to the blur over source form. Tuning this allows for background updates to show through.")]
    [DefaultValue(OPACITY_DEFAULT)]
    public byte Opacity
    {
        get => _opacity;
        set
        {
            if (Math.Abs(_opacity - value) > 0.001
                && value is >= 0 and <= 100
               )
            {
                _opacity = value;
                OpacityChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private bool ShouldSerializeOpacity() => Math.Abs(_opacity - OPACITY_DEFAULT) > 0.001;

    /// <summary>
    /// </summary>
    public void ResetOpacity() => _opacity = OPACITY_DEFAULT;

    /// <summary>
    /// </summary>
    [Description(@"Blur this when not Focused")]
    [DefaultValue(false)]
    public bool BlurWhenFocusLost
    {
        get => _blurWhenFocusLost;
        set
        {
            if (_blurWhenFocusLost != value)
            {
                _blurWhenFocusLost = value;
                BlurWhenFocusLostChanged?.Invoke(this, EventArgs.Empty);

            }
        }
    }

    private bool ShouldSerializeBlurWhenFocusLost() => BlurWhenFocusLost;

    /// <summary>
    /// </summary>
    public void ResetBlurWhenFocusLost() => BlurWhenFocusLost = false;

    #region Default Values
    /// <summary>
    /// 
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => !ShouldSerializeOpacity()
                                      && !ShouldSerializeBlurWhenFocusLost()
    ;

    #endregion Default Values
}