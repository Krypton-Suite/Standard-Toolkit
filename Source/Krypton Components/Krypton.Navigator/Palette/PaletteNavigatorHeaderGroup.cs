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

namespace Krypton.Navigator;

/// <summary>
/// Implement storage for Navigator HeaderGroup states.
/// </summary>
public class PaletteNavigatorHeaderGroup : PaletteHeaderGroup
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteNavigatorHeaderGroup class.
    /// </summary>
    /// <param name="inheritHeaderGroup">Source for inheriting palette defaulted values.</param>
    /// <param name="inheritHeaderPrimary">Source for inheriting primary header defaulted values.</param>
    /// <param name="inheritHeaderSecondary">Source for inheriting secondary header defaulted values.</param>
    /// <param name="inheritHeaderBar">Source for inheriting bar header defaulted values.</param>
    /// <param name="inheritHeaderOverflow">Source for inheriting overflow header defaulted values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteNavigatorHeaderGroup(PaletteHeaderGroupRedirect inheritHeaderGroup,
        PaletteHeaderPaddingRedirect inheritHeaderPrimary,
        PaletteHeaderPaddingRedirect inheritHeaderSecondary,
        [DisallowNull] PaletteHeaderPaddingRedirect inheritHeaderBar,
        PaletteHeaderPaddingRedirect inheritHeaderOverflow,
        NeedPaintHandler needPaint)
        : base(inheritHeaderGroup, inheritHeaderPrimary,
            inheritHeaderSecondary, needPaint)
    {
        Debug.Assert(inheritHeaderBar is not null);

        if (inheritHeaderBar is null)
        {
            throw new ArgumentNullException(nameof(inheritHeaderBar));
        }
            
        // Create the palette storage
        HeaderBar = new PaletteTripleMetric(inheritHeaderBar, needPaint);
        HeaderOverflow = new PaletteTripleMetric(inheritHeaderOverflow, needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (base.IsDefault &&
                                       HeaderBar.IsDefault &&
                                       HeaderOverflow.IsDefault);

    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    /// <param name="inheritHeaderGroup">Source for inheriting.</param>
    public void SetInherit(PaletteNavigatorHeaderGroup inheritHeaderGroup)
    {
        base.SetInherit(inheritHeaderGroup);
        HeaderBar.SetInherit(inheritHeaderGroup.HeaderBar);
        HeaderOverflow.SetInherit(inheritHeaderGroup.HeaderOverflow);
    }
    #endregion

    #region HeaderBar
    /// <summary>
    /// Gets access to the bar header appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining bar header appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleMetric HeaderBar { get; }

    private bool ShouldSerializeHeaderBar() => !HeaderBar.IsDefault;

    #endregion

    #region HeaderOverflow
    /// <summary>
    /// Gets access to the overflow header appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining overflow header appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleMetric HeaderOverflow { get; }

    private bool ShouldSerializeHeaderOverflow() => !HeaderOverflow.IsDefault;

    #endregion
}