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
/// Implement storage for GroupBox states.
/// </summary>
public class PaletteGroupBox : PaletteDouble
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteGroupBox class.
    /// </summary>
    /// <param name="inherit">Source for inheriting palette defaulted values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteGroupBox(PaletteGroupBoxRedirect inherit,
        NeedPaintHandler needPaint)
        : base(inherit, needPaint) =>
        Content = new PaletteContent(inherit.PaletteContent, needPaint);

    #endregion

    #region Content
    /// <summary>
    /// Gets access to the content palette details.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining content appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContent Content { get; }

    private bool ShouldSerializeContent() => !Content.IsDefault;

    /// <summary>
    /// Gets the content palette.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IPaletteContent PaletteContent => Content;

    #endregion
}