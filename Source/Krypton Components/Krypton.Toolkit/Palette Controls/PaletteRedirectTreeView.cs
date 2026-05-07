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
/// Redirects requests for tree view images from the TreeViewImages instance.
/// </summary>
public class PaletteRedirectTreeView : PaletteRedirect
{
    #region Instance Fields
    private readonly TreeViewImages _plusMinusImages;
    private readonly CheckBoxImages _checkboxImages;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRedirectTreeView class.
    /// </summary>
    /// <param name="plusMinusImages">Reference to source of tree view images.</param>
    /// <param name="checkBoxImages">Reference to source of check box images.</param>
    public PaletteRedirectTreeView(TreeViewImages plusMinusImages,
        CheckBoxImages checkBoxImages)
        : this(null, plusMinusImages, checkBoxImages)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteRedirectTreeView class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="plusMinusImages">Reference to source of tree view images.</param>
    /// <param name="checkBoxImages">Reference to source of check box images.</param>
    public PaletteRedirectTreeView(PaletteBase? target,
        [DisallowNull] TreeViewImages plusMinusImages,
        CheckBoxImages checkBoxImages)
        : base(target)
    {
        Debug.Assert(plusMinusImages != null);

        // Remember incoming targets
        _plusMinusImages = plusMinusImages!;
        _checkboxImages = checkBoxImages;
    }
    #endregion

    #region Images
    /// <summary>
    /// Gets a tree view image appropriate for the provided state.
    /// </summary>
    /// <param name="expanded">Is the node expanded</param>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetTreeViewImage(bool expanded)
    {
        Image? retImage = (expanded ? _plusMinusImages.Minus : _plusMinusImages.Plus) ?? Target?.GetTreeViewImage(expanded);

        // Not found, then inherit from target

        return retImage;
    }

    /// <summary>
    /// Gets a check box image appropriate for the provided state.
    /// </summary>
    /// <param name="enabled">Is the check box enabled.</param>
    /// <param name="checkState">Is the check box checked/unchecked/indeterminate.</param>
    /// <param name="tracking">Is the check box being hot tracked.</param>
    /// <param name="pressed">Is the check box being pressed.</param>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetCheckBoxImage(bool enabled,
        CheckState checkState,
        bool tracking,
        bool pressed)
    {
        Image? retImage;

        // Get the state specific image
        switch (checkState)
        {
            default:
            case CheckState.Unchecked:
                if (!enabled)
                {
                    retImage = _checkboxImages.UncheckedDisabled;
                }
                else if (pressed)
                {
                    retImage = _checkboxImages.UncheckedPressed;
                }
                else if (tracking)
                {
                    retImage = _checkboxImages.UncheckedTracking;
                }
                else
                {
                    retImage = _checkboxImages.UncheckedNormal;
                }

                break;
            case CheckState.Checked:
                if (!enabled)
                {
                    retImage = _checkboxImages.CheckedDisabled;
                }
                else if (pressed)
                {
                    retImage = _checkboxImages.CheckedPressed;
                }
                else if (tracking)
                {
                    retImage = _checkboxImages.CheckedTracking;
                }
                else
                {
                    retImage = _checkboxImages.CheckedNormal;
                }

                break;
            case CheckState.Indeterminate:
                if (!enabled)
                {
                    retImage = _checkboxImages.IndeterminateDisabled;
                }
                else if (pressed)
                {
                    retImage = _checkboxImages.IndeterminatePressed;
                }
                else if (tracking)
                {
                    retImage = _checkboxImages.IndeterminateTracking;
                }
                else
                {
                    retImage = _checkboxImages.IndeterminateNormal;
                }

                break;
        }

        // Not found, then get the common image
        retImage ??= _checkboxImages.Common;

        // Not found, then inherit from target
        return retImage ?? Target?.GetCheckBoxImage(enabled, checkState, tracking, pressed);
    }
    #endregion
}