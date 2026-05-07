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

/// <summary>Storage of user supplied font values, not used by Krypton.</summary>
public class KryptonPaletteFont : Storage
{
    #region Instance Fields

    private KryptonPaletteCommon _paletteCommon;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonPaletteFont" /> class.</summary>
    /// <param name="redirector">Palette redirector for sourcing inherited values</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteFont([DisallowNull] PaletteRedirect redirector, NeedPaintHandler needPaint)
    {
        NeedPaint = needPaint;

        Debug.Assert(redirector != null);
        ResetCommonLongTextFont();
        ResetCommonShortTextFont();
    }

    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (CommonLongTextFont == new Font("Segoe UI", 9f)) && (CommonShortTextFont == new Font("Segoe UI", 9f));

    #endregion

    #region CommonLongTextFont
    /// <summary>
    /// Gets and sets a user supplied font value.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"User supplied font value.")]
    [RefreshProperties(RefreshProperties.All)]
    [DisallowNull]
    public Font? CommonLongTextFont 
    { 
        get => _paletteCommon.StateCommon.Content.LongText.Font; 
        set => _paletteCommon.StateCommon.Content.LongText.Font = value; 
    }
    /// <summary>
    /// Resets the CommonLongTextFont property to its default value.
    /// </summary>
    public void ResetCommonLongTextFont() => CommonLongTextFont = new Font("Segoe UI", 9f);

    #endregion

    #region CommonShortTextFont
    /// <summary>
    /// Gets and sets a user supplied font value.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"User supplied font value.")]
    [RefreshProperties(RefreshProperties.All)]
    [DisallowNull]
    public Font? CommonShortTextFont 
    { 
        get => _paletteCommon.StateCommon.Content.ShortText.Font; 
        set => _paletteCommon.StateCommon.Content.ShortText.Font = value; 
    }

    /// <summary>
    /// Resets the CommonShortTextFont property to its default value.
    /// </summary>
    public void ResetCommonShortTextFont() => CommonShortTextFont = new Font("Segoe UI", 9f);

    #endregion
}