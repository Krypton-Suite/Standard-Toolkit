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
public class KryptonTaskDialogElementFooterBarFooterProperties :
    IKryptonTaskDialogElementFooterBar,
    IKryptonTaskDialogElementIconType
{
    #region Fields
    private KryptonTaskDialogElementFooterBar _footerBar;
    private KryptonWrapLabel _footNoteText;
    private string _expanderExpandedText;
    private string _expanderCollapsedText;
    private bool _enableExpanderControls;
    private Action<bool> _onSizeChanged;
    private Action _updateExpanderText;
    Action _updateExpanderEnabledState;
    Action _updateFootNoteIcon;
    #endregion

    #region Identity
    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="footerBar">FooterBar instance</param>
    /// <param name="footNoteText">KryptonWarpLabel instance.</param>
    /// <param name="onSizeChanged">FooterBar OnsizeChanged callback.</param>
    /// <param name="updateExpanderText">FooterBar updateExpanderText callback.</param>
    /// <param name="updateExpanderEnabledState">FooterBar updateExpanderEnabledState callback.</param>
    /// <param name="updateFootNoteIcon">FooterBar updateFootNoteIcon callback.</param>
    public KryptonTaskDialogElementFooterBarFooterProperties(KryptonTaskDialogElementFooterBar footerBar, KryptonWrapLabel footNoteText, 
        Action<bool> onSizeChanged, Action updateExpanderText, Action updateExpanderEnabledState, Action updateFootNoteIcon
        )
    {
        _footerBar                  = footerBar;
        _footNoteText               = footNoteText;
        _onSizeChanged              = onSizeChanged;
        _updateExpanderText         = updateExpanderText;
        _updateExpanderEnabledState = updateExpanderEnabledState;
        _updateFootNoteIcon         = updateFootNoteIcon;
        _expanderCollapsedText      = string.Empty;
        _expanderExpandedText       = string.Empty;
        _enableExpanderControls     = false;
    }
    #endregion

    #region Public
    /// <inheritdoc/>
    public string FootNoteText
    {
        get => _footNoteText.Text;

        set
        {
            if (_footNoteText.Text != value)
            {
                _footNoteText.Text = value;
                _footNoteText.Invalidate();
                _footerBar.LayoutDirty = true;
                _onSizeChanged(false);
            }
        }
    }

    /// <inheritdoc/>
    public string ExpanderExpandedText
    {
        get => _expanderExpandedText;

        set
        {
            if (_expanderExpandedText != value)
            {
                _expanderExpandedText = value;
                _footerBar.LayoutDirty = true;
                _updateExpanderText();
            }
        }
    }

    /// <inheritdoc/>
    public string ExpanderCollapsedText
    {
        get => _expanderCollapsedText;

        set
        {
            if (_expanderCollapsedText != value)
            {
                _expanderCollapsedText = value;
                _footerBar.LayoutDirty = true;
                _updateExpanderText();
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
                _footerBar.LayoutDirty = true;
                _updateFootNoteIcon();
            }
        }
    }

    /// <inheritdoc/>
    public bool EnableExpanderControls
    {
        get => _enableExpanderControls;

        set
        {
            if (_enableExpanderControls != value)
            {
                _enableExpanderControls = value;
                _footerBar.LayoutDirty = true;
                _updateExpanderEnabledState();
                _onSizeChanged(false);
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
