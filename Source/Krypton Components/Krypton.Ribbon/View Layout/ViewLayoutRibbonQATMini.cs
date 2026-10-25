#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

// ReSharper disable InconsistentNaming
namespace Krypton.Ribbon;

/// <summary>
/// Layout area containing a quick access toolbar border and extra button.
/// </summary>
internal class ViewLayoutRibbonQATMini : ViewLayoutDocker
{
    #region Instance Fields

    private readonly int SEP_GAP;
    private readonly KryptonRibbon _ribbon;
    private readonly ViewDrawRibbonQATBorder _border;
    private readonly ViewLayoutRibbonQATFromRibbon _borderContents;
    private readonly ViewDrawRibbonQATExtraButtonMini _extraButton;
    private readonly ViewLayoutSeparator _extraSeparator;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonQATMini class.
    /// </summary>
    /// <param name="ribbon">Owning control instance.</param>
    /// <param name="needPaintDelegate">Delegate for notifying paint/layout changes.</param>
    public ViewLayoutRibbonQATMini([DisallowNull] KryptonRibbon ribbon,
        [DisallowNull] NeedPaintHandler needPaintDelegate)
    {
        Debug.Assert(ribbon is not null);
            
        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));

        if (needPaintDelegate is null)
        {
            throw new ArgumentNullException(nameof(needPaintDelegate));
        }

        SEP_GAP = (int)(2 * FactorDpiX);
        // Create the minibar border suitable for a caption area
        _border = new ViewDrawRibbonQATBorder(_ribbon, needPaintDelegate, true);

        // Create minibar content that synchs with ribbon collection
        _borderContents = new ViewLayoutRibbonQATFromRibbon(_ribbon, needPaintDelegate, false);
        _border.Add(_borderContents);

        // Separator gap before the extra button
        _extraSeparator = new ViewLayoutSeparator(SEP_GAP);

        // Need the extra button to show after the border area
        _extraButton = new ViewDrawRibbonQATExtraButtonMini(_ribbon, needPaintDelegate);
        _extraButton.ClickAndFinish += OnExtraButtonClick;

        // Add layout contents
        Add(_border, ViewDockStyle.Fill);
        Add(_extraSeparator, ViewDockStyle.Right);
        Add(_extraButton, ViewDockStyle.Right);

        // OwnerForm property can be intialized after _ribbon has been assigned.
        OwnerForm ??= _ribbon.FindKryptonForm();
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewLayoutRibbonQATMini:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_extraButton != null)
            {
                _extraButton.ClickAndFinish -= OnExtraButtonClick;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region OwnerForm
    /// <summary>
    /// Gets and sets the associated form instance.
    /// </summary>
    public KryptonForm? OwnerForm
    {
        get => _border.OwnerForm;
        set => _border.OwnerForm = value;
    }
    #endregion

    #region Visible
    /// <summary>
    /// Gets and sets the visible state of the element.
    /// </summary>
    public override bool Visible
    {
        get => _ribbon.Visible && base.Visible;
        set => base.Visible = value;
    }
    #endregion

    #region OverlapAppButton
    /// <summary>
    /// Should the element overlap the app button to the left.
    /// </summary>
    public bool OverlapAppButton
    {
        get => _border.OverlapAppButton;
        set => _border.OverlapAppButton = value;
    }
    #endregion

    #region GetTabKeyTips
    /// <summary>
    /// Generate a key tip info for each visible tab.
    /// </summary>
    /// <returns>Array of KeyTipInfo instances.</returns>
    public KeyTipInfo[] GetQATKeyTips()
    {
        var keyTipList = new KeyTipInfoList();

        // Add all the entries for the contents
        keyTipList.AddRange(_borderContents.GetQATKeyTips(_ribbon.FindKryptonForm()!));

        // If we have the extra button and it is in overflow appearance
        if (_extraButton is {Overflow : true })
        {
            // If integrated into the caption area then get the caption area height
            var borders = OwnerForm!.RealWindowBorders;

            // Get the screen location of the extra button
            Rectangle viewRect = _borderContents.ParentControl.RectangleToScreen(_extraButton.ClientRectangle);

            // The keytip should be centered on the bottom center of the view
            var screenPt = new Point(viewRect.Left + (viewRect.Width / 2) - borders.Left,
                viewRect.Bottom - 2 - borders.Top);

            // Create fixed key tip of '00' that invokes the extra button controller
            keyTipList.Add(new KeyTipInfo(true, "00", screenPt, 
                _extraButton.ClientRectangle, 
                _extraButton.KeyTipTarget));
        }

        return keyTipList.ToArray();
    }
    #endregion

    #region GetFirstQATView
    /// <summary>
    /// Gets the view element for the first visible and enabled quick access toolbar button.
    /// </summary>
    /// <returns></returns>
    public ViewBase GetFirstQATView()
    {
        // Find the first qat button
        ViewBase view = _borderContents.GetFirstQATView() ?? _extraButton;

        // If defined then use the extra button

        return view;
    }
    #endregion

    #region GetLastQATView
    /// <summary>
    /// Gets the view element for the first visible and enabled quick access toolbar button.
    /// </summary>
    /// <returns></returns>
    public ViewBase GetLastQATView() =>
        // Last view is the extra button if defined
        _extraButton ?? _borderContents.GetLastQATView();

    // Find the last qat button

    #endregion

    #region GetNextQATView
    /// <summary>
    /// Gets the view element the button after the one provided.
    /// </summary>
    /// <param name="qatButton">Search for entry after this view.</param>
    /// <returns>ViewBase if found; otherwise false.</returns>
    public ViewBase GetNextQATView(ViewBase qatButton)
    {
        ViewBase view = _borderContents.GetNextQATView(qatButton);

        // If no qat button is found and not already at the extra button
        if ((view == null) && (_extraButton != qatButton))
        {
            view = _extraButton;
        }

        return view!;
    }
    #endregion

    #region GetPreviousQATView
    /// <summary>
    /// Gets the view element for the button before the one provided.
    /// </summary>
    /// <param name="qatButton">Search for entry after this view.</param>
    /// <returns>ViewBase if found; otherwise false.</returns>
    public ViewBase GetPreviousQATView(ViewBase qatButton) =>
        // If on the extra button then find the right most qat button instead
        qatButton == _extraButton ? _borderContents.GetLastQATView() : _borderContents.GetPreviousQATView(qatButton);

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Scan to see if there are any visible quick access toolbar buttons
        var visibleQATButtons = _ribbon.QATButtons.Cast<IQuickAccessToolbarButton>().Any(static qatButton => qatButton.GetVisible());
            
        // Only show the border if there are some visible contents
        _border.Visible = visibleQATButtons;

        // If there are no visible buttons, then extra button must be for customization
        if (!visibleQATButtons)
        {
            if (_extraButton != null)
            {
                _extraButton.Overflow = false;
            }
        }

        return base.GetPreferredSize(context!);
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout(ViewLayoutContext context)
    {
        // Cache the provided size of our area
        Rectangle clientRect = context.DisplayRectangle;

        // If we are in the ribbon caption area line
        if (OwnerForm == null)
        {
            // Limit the width, so we do not flow over right edge of caption area
            var maxWidth = _ribbon.Width - clientRect.X;
            clientRect.Width = Math.Min(clientRect.Width, maxWidth);
        }

        // Update with modified value
        context.DisplayRectangle = clientRect;

        // Let base class layout all contents
        base.Layout(context);

        // Only if border is visible do we need to find the latest overflow value
        // otherwise it must be a customization image because there are no buttons
        if (_extraButton != null)
        {
            _extraButton.Overflow = _border.Visible && _borderContents.Overflow;
        }
    }

    private void OnExtraButtonClick(object sender, EventHandler? finishDelegate)
    {
        var button = (ViewDrawRibbonQATExtraButton)sender;

        // Convert the button rectangle to screen coordinates
        Rectangle screenRect = _ribbon.RectangleToScreen(button.ClientRectangle);

        // Only if the ribbon is on a KForm this adjustment is needed.
        if (OwnerForm is not null)
        {
            // If integrated into the caption area
            // Adjust for the height/width of borders
            Padding borders = OwnerForm!.RealWindowBorders;
            screenRect.X -= borders.Left;
            screenRect.Y -= borders.Top;
        }

        if (_extraButton is { Overflow: true })
        {
            _ribbon.DisplayQATOverflowMenu(screenRect, _borderContents, finishDelegate);
        }
        else
        {
            _ribbon.DisplayQATCustomizeMenu(screenRect, _borderContents, finishDelegate);
        }

    }
    #endregion
}