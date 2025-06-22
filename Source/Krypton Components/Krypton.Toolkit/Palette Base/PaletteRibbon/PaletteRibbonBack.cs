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
/// Storage for ribbon background values.
/// </summary>
public class PaletteRibbonBack : Storage,
    IPaletteRibbonBack
{
    #region Instance Fields
    private IPaletteRibbonBack _inheritBack;
    private Color _backColor1;
    private Color _backColor2;
    private Color _backColor3;
    private Color _backColor4;
    private Color _backColor5;
    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the PaletteRibbonBack class.
    /// </summary>
    /// <param name="inheritBack">Source for inheriting background values.</param>
    /// <param name="needPaint">Delegate for notifying changes in value.</param>
    public PaletteRibbonBack([DisallowNull] IPaletteRibbonBack inheritBack,
        NeedPaintHandler needPaint) 
    {
        Debug.Assert(inheritBack != null);

        // Remember inheritance
        _inheritBack = inheritBack ?? throw new ArgumentNullException(nameof(inheritBack));

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Define default values
        _backColor1 = GlobalStaticValues.EMPTY_COLOR;
        _backColor2 = GlobalStaticValues.EMPTY_COLOR;
        _backColor3 = GlobalStaticValues.EMPTY_COLOR;
        _backColor4 = GlobalStaticValues.EMPTY_COLOR;
        _backColor5 = GlobalStaticValues.EMPTY_COLOR;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (BackColor1 == GlobalStaticValues.EMPTY_COLOR) &&
                                      (BackColor2 == GlobalStaticValues.EMPTY_COLOR) &&
                                      (BackColor3 == GlobalStaticValues.EMPTY_COLOR) &&
                                      (BackColor4 == GlobalStaticValues.EMPTY_COLOR) &&
                                      (BackColor5 == GlobalStaticValues.EMPTY_COLOR);

    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    public void SetInherit(IPaletteRibbonBack inheritBack) => _inheritBack = inheritBack;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">Palette state to use when populating.</param>
    public void PopulateFromBase(PaletteState state)
    {
        BackColor1 = GetRibbonBackColor1(state);
        BackColor2 = GetRibbonBackColor2(state);
        BackColor3 = GetRibbonBackColor3(state);
        BackColor4 = GetRibbonBackColor4(state);
        BackColor5 = GetRibbonBackColor5(state);
    }
    #endregion

    #region BackColorStyle
    /// <summary>
    /// Gets the background drawing style for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteState state) => _inheritBack.GetRibbonBackColorStyle(state);

    #endregion

    #region BackColor1
    /// <summary>
    /// Gets and sets the first background color for the ribbon item.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"First background color for the ribbon item.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Color BackColor1
    {
        get => _backColor1;

        set
        {
            if (_backColor1 != value)
            {
                _backColor1 = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBackColor1() => _backColor1 != GlobalStaticValues.EMPTY_COLOR;
    private void ResetBackColor1() => _backColor1 = GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the first background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonBackColor1(PaletteState state) =>
        BackColor1 != GlobalStaticValues.EMPTY_COLOR ? BackColor1 : _inheritBack.GetRibbonBackColor1(state);

    #endregion

    #region BackColor2
    /// <summary>
    /// Gets and sets the second background color for the ribbon item.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Second background color for the ribbon item.")]
    [DefaultValue(typeof(Color), "Empty")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Color BackColor2
    {
        get => _backColor2;

        set
        {
            if (_backColor2 != value)
            {
                _backColor2 = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBackColor2() => _backColor2 != GlobalStaticValues.EMPTY_COLOR;
    private void ResetBackColor2() => BackColor2 = GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the second background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonBackColor2(PaletteState state) =>
        BackColor2 != GlobalStaticValues.EMPTY_COLOR ? BackColor2 : _inheritBack.GetRibbonBackColor2(state);

    #endregion

    #region BackColor3
    /// <summary>
    /// Gets and sets the third background color for the ribbon item.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Third background color for the ribbon item.")]
    [DefaultValue(typeof(Color), "Empty")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Color BackColor3
    {
        get => _backColor3;

        set
        {
            if (_backColor3 != value)
            {
                _backColor3 = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBackColor3() => _backColor3 != GlobalStaticValues.EMPTY_COLOR;
    private void ResetBackColor3() => BackColor3 = GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the third background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonBackColor3(PaletteState state) =>
        BackColor3 != GlobalStaticValues.EMPTY_COLOR ? BackColor3 : _inheritBack.GetRibbonBackColor3(state);

    #endregion

    #region BackColor4
    /// <summary>
    /// Gets and sets the fourth background color for the ribbon item.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Fourth background color for the ribbon item.")]
    [DefaultValue(typeof(Color), "Empty")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Color BackColor4
    {
        get => _backColor4;

        set
        {
            if (_backColor4 != value)
            {
                _backColor4 = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBackColor4() => _backColor4 != GlobalStaticValues.EMPTY_COLOR;
    private void ResetBackColor4() => BackColor4 = GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the fourth background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonBackColor4(PaletteState state) =>
        BackColor4 != GlobalStaticValues.EMPTY_COLOR ? BackColor4 : _inheritBack.GetRibbonBackColor4(state);

    #endregion

    #region BackColor5
    /// <summary>
    /// Gets and sets the fifth background color for the ribbon item.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Fifth background color for the ribbon item.")]
    [DefaultValue(typeof(Color), "Empty")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Color BackColor5
    {
        get => _backColor5;

        set
        {
            if (_backColor5 != value)
            {
                _backColor5 = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeBackColor5() => _backColor5 != GlobalStaticValues.EMPTY_COLOR;
    private void ResetBackColor5() => BackColor5 = GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the fifth background color for the ribbon item.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetRibbonBackColor5(PaletteState state) =>
        BackColor5 != GlobalStaticValues.EMPTY_COLOR ? BackColor5 : _inheritBack.GetRibbonBackColor5(state);

    #endregion
}