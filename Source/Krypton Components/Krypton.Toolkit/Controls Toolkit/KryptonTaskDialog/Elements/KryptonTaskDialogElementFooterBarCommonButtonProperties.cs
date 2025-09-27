#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion
    
namespace Krypton.Toolkit;

/// <summary>
/// Used internally by KryptonTaskDialogElementFooterBar
/// </summary>
public class KryptonTaskDialogElementFooterBarCommonButtonProperties 
{
    #region Fields
    private KryptonTaskDialogElementFooterBar _footerBar;
    private KryptonTaskDialogCommonButtonTypes _commonButtons;
    #endregion

    #region Identity
    public KryptonTaskDialogElementFooterBarCommonButtonProperties(KryptonTaskDialogElementFooterBar footerBar)
    {
        _footerBar = footerBar;
        _commonButtons = KryptonTaskDialogCommonButtonTypes.None;
    }
    #endregion

    #region Public
    /// <inheritdoc/>
    public KryptonTaskDialogCommonButtonTypes Buttons
    {
        get => _commonButtons;

        set
        {
            if (_commonButtons != value)
            {
                _commonButtons = value;
                _footerBar.OnCommonButtonsChanged();
            }
        }
    }

    /// <inheritdoc/>
    public KryptonTaskDialogCommonButtonTypes AcceptButton
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                _footerBar.OnCommonButtonsChanged();
            }
        }
    }

    /// <inheritdoc/>
    public KryptonTaskDialogCommonButtonTypes CancelButton
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                _footerBar.OnCommonButtonsChanged();
            }
        }
    }
    #endregion

    #region public override
    /// <summary>
    /// Not implemented
    /// </summary>
    /// <returns>String.Empty</returns>
    public sealed override string ToString()
    {
        return string.Empty;
    }
    #endregion
}
