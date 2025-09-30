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
public class KryptonTaskDialogElementFooterBarFooterProperties :
    IKryptonTaskDialogElementFooterBar,
    IKryptonTaskDialogElementIconType,
    IKryptonTaskDialogElementPropertyChanged<KryptonTaskDialogElementFooterBar.FooterBarProperties>
{
    #region Events
    /// <inheritdoc/>
    public event Action<KryptonTaskDialogElementFooterBar.FooterBarProperties>? PropertyChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Default constructor.
    /// </summary>
    public KryptonTaskDialogElementFooterBarFooterProperties()
    {
        FootNoteText = string.Empty;
        ExpanderExpandedText = string.Empty;
        ExpanderCollapsedText = string.Empty;
    }
    #endregion

    #region Private
    private void OnPropertyChanged(KryptonTaskDialogElementFooterBar.FooterBarProperties property)
    {
        PropertyChanged?.Invoke(property);
    }
    #endregion

    #region Public
    /// <inheritdoc/>
    public string FootNoteText
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged(KryptonTaskDialogElementFooterBar.FooterBarProperties.FootNoteText);
            }
        }
    }

    /// <inheritdoc/>
    public string ExpanderExpandedText
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged(KryptonTaskDialogElementFooterBar.FooterBarProperties.ExpanderExpandedText);
            }
        }
    }

    /// <inheritdoc/>
    public string ExpanderCollapsedText
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged(KryptonTaskDialogElementFooterBar.FooterBarProperties.ExpanderCollapsedText);
            }
        }
    }

    /// <summary>
    /// Icon used to decorate the footnote.
    /// </summary>
    public KryptonTaskDialogIconType IconType
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged(KryptonTaskDialogElementFooterBar.FooterBarProperties.IconType);
            }
        }
    }

    /// <inheritdoc/>
    public bool EnableExpanderControls
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged(KryptonTaskDialogElementFooterBar.FooterBarProperties.EnableExpanderControls);
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
