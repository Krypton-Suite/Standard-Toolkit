#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Storage for application button related properties.
/// </summary>
public class PaletteRibbonImages : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRibbonImages class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="needPaint">Paint delegate.</param>
    public PaletteRibbonImages([DisallowNull] PaletteRedirect redirect,
        [DisallowNull] NeedPaintHandler needPaint)
    {
        Debug.Assert(redirect != null);
        Debug.Assert(needPaint != null);

        CheckBox = new CheckBoxImages(needPaint);
        RadioButton = new RadioButtonImages(needPaint);
        InternalCheckBox = new PaletteRedirectCheckBox(redirect, CheckBox);
        InternalRadioButton = new PaletteRedirectRadioButton(redirect, RadioButton);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => CheckBox.IsDefault &&
                                      RadioButton.IsDefault;

    #endregion

    #region CheckBox
    /// <summary>
    /// Gets and sets the ribbon check box images.
    /// </summary>
    [Category(@"Values")]
    [Description(@"Ribbon check box images.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public CheckBoxImages CheckBox { get; }

    private bool ShouldSerializeCheckBox() => !CheckBox.IsDefault;

    #endregion

    #region RadioButton
    /// <summary>
    /// Gets and sets the ribbon radio button images.
    /// </summary>
    [Category(@"Values")]
    [Description(@"Ribbon radio button images.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadioButtonImages RadioButton { get; }

    private bool ShouldSerializeRadioButton() => !RadioButton.IsDefault;

    #endregion

    #region Implementation
    internal PaletteRedirectCheckBox InternalCheckBox { get; }

    internal PaletteRedirectRadioButton InternalRadioButton { get; }

    #endregion
}