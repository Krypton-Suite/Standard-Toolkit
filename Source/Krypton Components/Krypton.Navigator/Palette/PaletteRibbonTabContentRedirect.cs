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
/// Storage for general ribbon values.
/// </summary>
public class PaletteRibbonTabContentRedirect : Storage
{
    #region Instance Fields
    private readonly PaletteNavContent _content;
    private readonly PaletteRibbonDoubleRedirect _drawRedirect;
    private readonly PaletteContentInheritRedirect _contentInherit;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRibbonTabContentRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteRibbonTabContentRedirect([DisallowNull] PaletteRedirect redirect,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(redirect is not null);

        if (redirect is null)
        {
            throw new ArgumentNullException(nameof(redirect));
        }
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        _drawRedirect = new PaletteRibbonDoubleRedirect(redirect,
            PaletteRibbonBackStyle.RibbonTab,
            PaletteRibbonTextStyle.RibbonTab,
            needPaint);

        _contentInherit = new PaletteContentInheritRedirect(redirect);
        _content = new PaletteNavContent(_contentInherit, needPaint);
    }
    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect redirect)
    {
        _drawRedirect.SetRedirector(redirect);
        _contentInherit.SetRedirector(redirect);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (TabDraw.IsDefault && Content.IsDefault);

    #endregion

    #region TabDraw
    /// <summary>
    /// Gets access to the tab drawing appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tab drawing appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonDoubleRedirect TabDraw => _drawRedirect;

    private bool ShouldSerializeTabDraw() => !_drawRedirect.IsDefault;

    #endregion

    #region Content
    /// <summary>
    /// Gets access to the tab content appearance.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tab content appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteNavContent Content => _content;

    private bool ShouldSerializeContent() => !_content.IsDefault;

    #endregion
}