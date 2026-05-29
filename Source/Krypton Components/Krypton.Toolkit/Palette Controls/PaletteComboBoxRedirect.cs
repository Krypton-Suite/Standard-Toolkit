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
/// Implement storage for a combo box state.
/// </summary>
public class PaletteComboBoxRedirect : Storage
{
    #region Instance Fields
    private readonly PaletteDoubleRedirect _dropBackRedirect;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteComboBoxRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteComboBoxRedirect([DisallowNull] PaletteRedirect redirect,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(redirect != null);

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Create storage that maps onto the inherit instances
        Item = new PaletteTripleRedirect(redirect!, 
            PaletteBackStyle.ButtonListItem, 
            PaletteBorderStyle.ButtonListItem, 
            PaletteContentStyle.ButtonListItem, 
            NeedPaint);

        ComboBox = new PaletteInputControlTripleRedirect(redirect!, 
            PaletteBackStyle.InputControlStandalone,
            PaletteBorderStyle.InputControlStandalone,
            PaletteContentStyle.InputControlStandalone, 
            NeedPaint)
        {
            Content =
            {
                // Set directly to prevent a Paint redirect
                _shortTextH = PaletteRelativeAlign.Near
            }
        };

        _dropBackRedirect = new PaletteDoubleRedirect(redirect!, 
            PaletteBackStyle.ControlClient, 
            PaletteBorderStyle.ButtonStandalone,
            NeedPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => ComboBox.IsDefault &&
                                      Item.IsDefault &&
                                      DropBack.IsDefault;

    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public virtual void SetRedirector(PaletteRedirect redirect)
    {
        Item.SetRedirector(redirect);
        ComboBox.SetRedirector(redirect);
        _dropBackRedirect.SetRedirector(redirect);
    }
    #endregion

    #region SetStyles
    /// <summary>
    /// Update the combo box input control style.
    /// </summary>
    /// <param name="style">New input control style.</param>
    public void SetStyles(InputControlStyle style) => ComboBox.SetStyles(style);

    /// <summary>
    /// Update the combo box item style.
    /// </summary>
    /// <param name="style">New item style.</param>
    public void SetStyles(ButtonStyle style) => Item.SetStyles(style);

    /// <summary>
    /// Update the combo box drop background style.
    /// </summary>
    /// <param name="style">New drop background style.</param>
    public void SetStyles(PaletteBackStyle style) => _dropBackRedirect.SetStyles(style, PaletteBorderStyle.ButtonStandalone);

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">Palette state to use when populating.</param>
    public void PopulateFromBase(PaletteState state)
    {
        ComboBox.PopulateFromBase(state);
        Item.PopulateFromBase(state);
        _dropBackRedirect.PopulateFromBase(state);
    }
    #endregion

    #region ComboBox
    /// <summary>
    /// Gets access to the combo box appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining combo box appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteInputControlTripleRedirect ComboBox { get; }

    private bool ShouldSerializeComboBox() => !ComboBox.IsDefault;

    #endregion

    #region Item
    /// <summary>
    /// Gets access to the item appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining item appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect Item { get; }

    private bool ShouldSerializeItem() => !Item.IsDefault;

    #endregion

    #region DropBack
    /// <summary>
    /// Gets access to the dropdown background appearance.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining dropdown background appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack DropBack => _dropBackRedirect.Back;

    private bool ShouldSerializeDropBack() => !_dropBackRedirect.IsDefault;

    #endregion

    #region Protected
    /// <summary>
    /// Handle a change event from palette source.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="needLayout">True if a layout is also needed.</param>
    protected void OnNeedPaint(object? sender, bool needLayout) =>
        // Pass request from child to our own handler
        PerformNeedPaint(needLayout);

    #endregion
}