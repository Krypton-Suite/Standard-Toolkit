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
/// Storage for form title bar button visibility and alignment values.
/// </summary>
/// <remarks>
/// Provides configuration for optional title bar buttons (New, Open, Save, Cut, Copy, Paste,
/// Undo, Redo, Page Setup, Print Preview, Print, Quick Print) and their alignment.
/// </remarks>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class FormTitleBarValues : Storage
{
    #region Instance Fields

    private KryptonFormTitleBar _formTitleBar;
    private FormTitleBarButtonVisibility _buttonVisibility;
    private FormTitleBarButtonAlignment _buttonAlignment;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="FormTitleBarValues"/> class.
    /// </summary>
    /// <param name="formTitleBar">The parent <see cref="KryptonFormTitleBar"/> control.</param>
    public FormTitleBarValues(KryptonFormTitleBar formTitleBar)
    {
        _formTitleBar = formTitleBar;
        _buttonVisibility = new FormTitleBarButtonVisibility(OnValuesChanged);
        _buttonAlignment = new FormTitleBarButtonAlignment(OnValuesChanged);

        Reset();
    }

    private void OnValuesChanged() => _formTitleBar.OnValuesChanged();

    #endregion

    #region IsDefault

    /// <summary>
    /// Gets a value indicating whether all values are default.
    /// </summary>
    /// <returns><c>true</c> if all visibility settings are false; otherwise <c>false</c>.</returns>
    [Browsable(false)]
    public override bool IsDefault => ButtonVisibility.IsDefault &&
                                      ButtonAlignment.IsDefault;

    #endregion

    #region Public

    #region Visibility

    /// <summary>
    /// Gets the button visibility configuration for the title bar.
    /// </summary>
    [Category("Visuals")]
    [Description("Button visibility settings for the title bar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public FormTitleBarButtonVisibility ButtonVisibility => _buttonVisibility;

    #endregion

    #region Button Alignments

    /// <summary>
    /// Gets the button alignment configuration for the title bar.
    /// </summary>
    [Category("Visuals")]
    [Description("Button alignment settings for the title bar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public FormTitleBarButtonAlignment ButtonAlignment => _buttonAlignment;

    #endregion

    #endregion

    #region Implementation

    /// <summary>
    /// Resets all visibility properties to their default values (all buttons hidden).
    /// </summary>
    public void Reset()
    {
        #region Visibility

        ButtonVisibility.Reset();

        #endregion

        #region Alignment

        ButtonAlignment.Reset();

        #endregion
    }

    #endregion
}