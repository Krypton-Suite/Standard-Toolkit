#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 * © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
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
    private List<KryptonButton> _buttons;
    private Action<bool> _onSizeChanged;
    private KryptonTaskDialogCommonButtonTypes _commonButtons;
    #endregion

    #region Identity
    public KryptonTaskDialogElementFooterBarCommonButtonProperties(KryptonTaskDialogElementFooterBar footerBar, 
        List<KryptonButton> buttons, Action<bool> onSizeChanged)
    {
        _footerBar = footerBar;
        _buttons = buttons;
        _onSizeChanged = onSizeChanged;
        _commonButtons = KryptonTaskDialogCommonButtonTypes.None;
    }
    #endregion

    #region Public
    /// <summary>
    /// Rounds the button corners
    /// </summary>
    public bool RoundedCorners
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;

                int rounding = value ? 10 : -1;
                _buttons.ForEach(b => b.StateCommon.Border.Rounding = rounding);
                _footerBar.LayoutDirty = true;
                _onSizeChanged(false);
            }
        }
    }

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
