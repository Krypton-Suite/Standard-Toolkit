#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion
    
namespace Krypton.Toolkit;

/// <summary>
/// Used internally by KryptonTaskDialogElementFooterBar
/// </summary>
public class KryptonTaskDialogElementFooterBarCommonButtonProperties :
        IKryptonTaskDialogElementPropertyChanged<KryptonTaskDialogElementFooterBar.CommonButtonsProperties>
{
    #region Fields
    #endregion

    #region Events
    /// <inheritdoc/>
    public event Action<KryptonTaskDialogElementFooterBar.CommonButtonsProperties>? PropertyChanged;
    #endregion

    #region Identity
    public KryptonTaskDialogElementFooterBarCommonButtonProperties()
    {
    }
    #endregion

    #region Public
    /// <inheritdoc/>
    public KryptonTaskDialogCommonButtonTypes Buttons
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged(KryptonTaskDialogElementFooterBar.CommonButtonsProperties.Buttons);
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
                OnPropertyChanged(KryptonTaskDialogElementFooterBar.CommonButtonsProperties.AcceptButton);
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
                OnPropertyChanged(KryptonTaskDialogElementFooterBar.CommonButtonsProperties.CancelButton);
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

    #region Private
    private void OnPropertyChanged(KryptonTaskDialogElementFooterBar.CommonButtonsProperties property)
    {
        PropertyChanged?.Invoke(property);
    }
    #endregion

}
