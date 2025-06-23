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
/// Implement storage for palette border, background and content for the data grid view states.
/// </summary>
public class PaletteDataGridViewTripleStates : Storage,
    IPaletteTriple
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteDataGridViewTripleStates class.
    /// </summary>
    /// <param name="inherit">Source for inheriting values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteDataGridViewTripleStates([DisallowNull] IPaletteTriple inherit,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(inherit != null);

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Create storage that maps onto the inherit instances
        Back = new PaletteBack(inherit!.PaletteBack, needPaint);
        Border = new PaletteBorder(inherit.PaletteBorder!, needPaint);
        Content = new PaletteDataGridViewContentStates(inherit.PaletteContent!, needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => Back.IsDefault &&
                                      Border.IsDefault &&
                                      Content.IsDefault;

    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    public void SetInherit(IPaletteTriple inherit)
    {
        Back.SetInherit(inherit.PaletteBack);
        Border.SetInherit(inherit.PaletteBorder!);
        Content.SetInherit(inherit.PaletteContent!);
    }
    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">Palette state to use when populating.</param>
    public void PopulateFromBase(PaletteState state)
    {
        Back.PopulateFromBase(state);
        Border.PopulateFromBase(state);
        Content.PopulateFromBase(state);
    }
    #endregion

    #region Back
    /// <summary>
    /// Gets access to the background palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining background appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBack Back { get; }

    private bool ShouldSerializeBack() => !Back.IsDefault;

    /// <summary>
    /// Gets the background palette.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IPaletteBack PaletteBack => Back;

    #endregion

    #region Border
    /// <summary>
    /// Gets access to the border palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining border appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteBorder Border { get; }

    private bool ShouldSerializeBorder() => !Border.IsDefault;

    /// <summary>
    /// Gets the border palette.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IPaletteBorder PaletteBorder => Border;

    #endregion

    #region Content
    /// <summary>
    /// Gets access to the content palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining content appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDataGridViewContentStates Content { get; }

    private bool ShouldSerializeContent() => !Content.IsDefault;

    /// <summary>
    /// Gets the content palette.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IPaletteContent PaletteContent => Content;

    #endregion

    #region Implementation
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