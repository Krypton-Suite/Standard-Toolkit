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

namespace Krypton.Ribbon;

/// <summary>
/// Extends the ViewComposite by creating and laying out elements to represent individual QAT entries.
/// </summary>
internal abstract class ViewLayoutRibbonQATContents : ViewComposite
{
    #region Classes
    private class QATButtonToView : Dictionary<IQuickAccessToolbarButton, ViewDrawRibbonQATButton>;
    #endregion

    #region Instance Fields

    private readonly NeedPaintHandler _needPaint;
    private QATButtonToView _qatButtonToView;
    private ViewDrawRibbonQATExtraButton? _extraButton;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonQATContents class.
    /// </summary>
    /// <param name="ribbon">Owning ribbon control instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="showExtraButton">Should the extra button be shown.</param>
    public ViewLayoutRibbonQATContents([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] NeedPaintHandler? needPaint,
        bool showExtraButton)
    {
        Debug.Assert(ribbon is not null);
        Debug.Assert(needPaint is not null);

        Ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        _needPaint = needPaint ?? throw new ArgumentNullException(nameof(needPaint));

        // Create initial lookup table
        _qatButtonToView = new QATButtonToView();

        // Create the extra button for customization/overflow
        if (showExtraButton)
        {
            _extraButton = new ViewDrawRibbonQATExtraButton(ribbon, needPaint);
            _extraButton.ClickAndFinish += OnExtraButtonClick;
        }
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutRibbonQATContents:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Clear();

            foreach (ViewDrawRibbonQATButton view in _qatButtonToView.Values)
            {
                view.Dispose();
            }

            _qatButtonToView.Clear();

            if (_extraButton != null)
            {
                _extraButton.ClickAndFinish -= OnExtraButtonClick;
                _extraButton = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Ribbon
    /// <summary>
    /// Gets access to the ribbon control instance.
    /// </summary>
    public KryptonRibbon Ribbon { get; }

    #endregion

    #region GetTabKeyTips
    /// <summary>
    /// Generate a key tip info for each visible tab.
    /// </summary>
    /// <param name="ownerForm">KryptonForm instance that owns this view.</param>
    /// <returns>Array of KeyTipInfo instances.</returns>
    public KeyTipInfo[] GetQATKeyTips(KryptonForm ownerForm)
    {
        // ownerForm cannot be null
        if (ownerForm is null)
        {
            throw new ArgumentNullException(nameof(ownerForm));
        }

        // Create all the list of all possible QAT key tip strings
        var keyTipsPool = new Stack<string>();

        // Then use the alphanumeric 0A - 0Z
        for (var i = 25; i >= 0; i--)
        {
            keyTipsPool.Push($"0{(char)(65 + i)}");
        }

        // Then use the number 09 - 01
        for (var i = 1; i <= 9; i++)
        {
            keyTipsPool.Push($"0{i}");
        }

        // Start with the number 1 - 9
        for (var i = 9; i >= 1; i--)
        {
            keyTipsPool.Push(i.ToString());
        }

        // If integrated into the caption area then get the caption area height
        var borders = ownerForm.RealWindowBorders;

        var keyTipList = new KeyTipInfoList();

        foreach (ViewBase child in this)
        {
            // If visible and we have another key tip available on stack
            if (child.Visible
                && (keyTipsPool.Count > 0)
                && (child is ViewDrawRibbonQATButton viewQAT)
               )
            {
                // Get the screen location of the view tab
                Rectangle viewRect = ParentControl.RectangleToScreen(viewQAT.ClientRectangle);

                // The keytip should be centered on the bottom center of the view
                var screenPt = new Point(viewRect.Left + (viewRect.Width / 2) - borders.Left,
                    viewRect.Bottom - 2 - borders.Top);

                // Create new key tip that invokes the qat controller
                keyTipList.Add(new KeyTipInfo(viewQAT.Enabled, keyTipsPool.Pop(), screenPt,
                    viewQAT.ClientRectangle, viewQAT.KeyTipTarget));
            }
        }

        // If we have the extra button and it is in overflow appearance
        if (_extraButton is { Overflow: true })
        {
            // Get the screen location of the extra button
            Rectangle viewRect = ParentControl.RectangleToScreen(_extraButton.ClientRectangle);

            // The keytip should be centered on the bottom center of the view
            var screenPt = new Point(viewRect.Left + (viewRect.Width / 2) - borders.Left,
                viewRect.Bottom - 2 - borders.Top);

            // Create fixed key tip of '00' that invokes the extra button controller
            keyTipList.Add(new KeyTipInfo(true, "00", screenPt, _extraButton.ClientRectangle, _extraButton.KeyTipTarget));
        }

        return keyTipList.ToArray();
    }
    #endregion

    #region Overflow
    /// <summary>
    /// Gets a value indicating if overflowing is occurring.
    /// </summary>
    public bool Overflow { get; private set; }

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        // Sync to represent the current ribbon QAT buttons
        SyncChildren(false);

        var preferredSize = Size.Empty;

        // Find total width and maximum height across all child elements
        for (var i = 0; i < Count; i++)
        {
            ViewBase? child = this[i];

            // Only interested in visible items that are not the extra button
            if (child != _extraButton)
            {
                // Cast child to correct type
                var view = child as ViewDrawRibbonQATButton;

                // If the quick access toolbar button wants to be visible
                if (view!.QATButton.GetVisible() || Ribbon.InDesignHelperMode)
                {
                    // Cache preferred size of the child
                    Size childSize = child!.GetPreferredSize(context);

                    // Only need extra processing for children that have some width
                    if (childSize.Width > 0)
                    {
                        // Always add on to the width
                        preferredSize.Width += childSize.Width;

                        // Find maximum height encountered
                        preferredSize.Height = Math.Max(preferredSize.Height, childSize.Height);
                    }
                }
            }
        }

        if (_extraButton != null)
        {
            // Cache preferred size of the child
            Size childSize = _extraButton.GetPreferredSize(context);

            // Only need extra processing for children that have some width
            if (childSize.Width > 0)
            {
                // Always add on to the width
                preferredSize.Width += childSize.Width;

                // Find maximum height encountered
                preferredSize.Height = Math.Max(preferredSize.Height, childSize.Height);
            }
        }

        return preferredSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Sync to represent the current ribbon QAT buttons
        SyncChildren(true);

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        var x = ClientLocation.X;
        var right = ClientRectangle.Right;

        // If we need to show the extra button
        if (_extraButton != null)
        {
            // Find size of the extra button
            Size childSize = _extraButton.GetPreferredSize(context);

            // Make sure there is always enough room for it at the right hand side
            right -= childSize.Width;
        }

        var y = ClientLocation.Y;
        var height = ClientHeight;
        Overflow = false;

        // Are there any children to layout?
        if (Count > 0)
        {
            // Position each item from left to right taking up entire height
            for (var i = 0; i < Count; i++)
            {
                ViewBase? child = this[i];

                // We only position visible items, and we always ignore the extra button
                if (child != _extraButton)
                {
                    if (child!.Visible)
                    {
                        // Cache preferred size of the child
                        Size childSize = this[i]!.GetPreferredSize(context);

                        // Is there enough width for this item to be displayed
                        if ((childSize.Width + x) <= right)
                        {
                            // Define display rectangle for the group
                            context.DisplayRectangle = new Rectangle(x, y, childSize.Width, height);

                            // Position the element
                            this[i]!.Layout(context);

                            // Move across to next position
                            x += childSize.Width;
                        }
                        else
                        {
                            // Hide the child, not enough for it
                            child.Visible = false;

                            // Need to use the extra button as an overflow button
                            Overflow = true;
                        }
                    }
                    else
                    {
                        // Cast child to correct type
                        var view = (ViewDrawRibbonQATButton)child;

                        // If the quick access toolbar button wants to be visible
                        if (view.QATButton.GetVisible() || Ribbon.InDesignHelperMode)
                        {
                            Overflow = true;
                        }
                    }
                }
            }
        }

        // Do we need to position the extra button?
        if (_extraButton != null)
        {
            // Cache preferred size of the child
            Size childSize = _extraButton.GetPreferredSize(context);

            // Is there enough width for this item to be displayed
            if ((childSize.Width + x) <= ClientRectangle.Right)
            {
                // Define display rectangle for the group
                context.DisplayRectangle = new Rectangle(x, y, childSize.Width, height);

                // Position the element
                _extraButton.Layout(context);

                // Move across to next position
                x += childSize.Width;
            }

            // Should button show as overflow or customization
            _extraButton.Overflow = Overflow;
        }

        // Update our own size to reflect how wide we actually need to be for all the children
        ClientRectangle = new Rectangle(ClientLocation, new Size(x - ClientLocation.X, ClientHeight));

        // Update the display rectangle we allocated for use by parent
        context.DisplayRectangle = new Rectangle(ClientLocation, new Size(x - ClientLocation.X, ClientHeight));
    }
    #endregion

    #region DisplayButtons
    /// <summary>
    /// Returns a collection of all the quick access toolbar definitions.
    /// </summary>
    public abstract IQuickAccessToolbarButton[] QATButtons { get; }
    #endregion

    #region ViewForButton
    /// <summary>
    /// Gets access to the view used to display the provided button definition.
    /// </summary>
    /// <param name="qatButton"></param>
    /// <returns>Element that matches button; otherwise null</returns>
    public ViewBase? ViewForButton(IQuickAccessToolbarButton qatButton) =>
        _qatButtonToView.ContainsKey(qatButton) ? _qatButtonToView[qatButton] : null;

    #endregion

    #region GetFirstQATView
    /// <summary>
    /// Gets the view element for the first visible and enabled quick access toolbar button.
    /// </summary>
    /// <returns>ViewBase if found; otherwise false.</returns>
    public ViewBase GetFirstQATView()
    {
        // Scan all the buttons looking for one that is enabled and visible
        foreach (ViewBase qatView in _qatButtonToView.Values)
        {
            if (qatView is { Visible: true, Enabled: true })
            {
                return qatView;
            }
        }

        // If showing the extra button, then use that
        return _extraButton!;
    }
    #endregion

    #region GetLastQATView
    /// <summary>
    /// Gets the view element for the first visible and enabled quick access toolbar button.
    /// </summary>
    /// <returns></returns>
    public ViewBase GetLastQATView()
    {
        // If showing the extra button, then use that
        if (_extraButton != null)
        {
            return _extraButton;
        }

        // Extract the set of views into an array
        var qatViews = new ViewDrawRibbonQATButton[_qatButtonToView.Count];
        _qatButtonToView.Values.CopyTo(qatViews, 0);

        // Search the list in reverse order
        for (var i = qatViews.Length - 1; i >= 0; i--)
        {
            // Extract the correct view to test
            ViewDrawRibbonQATButton qatView = qatViews[i];

            // QAT button must be visible and enabled
            if (qatView is { Visible: true, Enabled: true })
            {
                return qatView;
            }
        }

        return null!;
    }
    #endregion

    #region GetNextQATView
    /// <summary>
    /// Gets the view element the button after the one provided.
    /// </summary>
    /// <param name="qatButton">Search for entry after this view.</param>
    /// <returns>ViewBase if found; otherwise false.</returns>
    public ViewBase GetNextQATView(ViewBase qatButton)
    {
        var found = false;

        // Find the one after the target view
        foreach (ViewBase qatView in _qatButtonToView.Values)
        {
            if (!found)
            {
                found = qatView == qatButton;
            }
            else if (qatView is { Visible: true, Enabled: true })
            {
                return qatView;
            }
        }

        // If showing the extra button, then use that
        if ((qatButton != _extraButton) && (_extraButton != null))
        {
            return _extraButton;
        }

        return null!;
    }
    #endregion

    #region GetPreviousQATView
    /// <summary>
    /// Gets the view element for the button before the one provided.
    /// </summary>
    /// <param name="qatButton">Search for entry after this view.</param>
    /// <returns>ViewBase if found; otherwise false.</returns>
    public ViewBase GetPreviousQATView(ViewBase qatButton)
    {
        // If the provided view is the extra button, then implicitly already found previous entry
        var found = (qatButton != null) && (qatButton == _extraButton);

        // Extract the set of views into an array
        var qatViews = new ViewDrawRibbonQATButton[_qatButtonToView.Count];
        _qatButtonToView.Values.CopyTo(qatViews, 0);

        // Search the list in reverse order
        for (var i = qatViews.Length - 1; i >= 0; i--)
        {
            // Extract the correct view to test
            ViewDrawRibbonQATButton qatView = qatViews[i];

            if (!found)
            {
                found = qatView == qatButton;
            }
            else if (qatView is { Visible: true, Enabled: true })
            {
                return qatView;
            }
        }

        return null!;
    }
    #endregion

    #region ParentControl
    /// <summary>
    /// Gets a reference to the owning control of this element.
    /// </summary>
    /// <returns>Control reference.</returns>
    public virtual Control ParentControl => Ribbon;

    #endregion

    #region Implementation
    private void SyncChildren(bool layout)
    {
        // Remove all child elements
        Clear();

        // Create a new lookup that reflects any changes in QAT buttons
        var regenerate = new QATButtonToView();

        // Get an array with all the buttons to be considered for display
        var qatButtons = QATButtons;

        // Make sure we have a view element to match each QAT button definition
        foreach (IQuickAccessToolbarButton qatButton in qatButtons)
        {
            // Get the currently cached view for the button
            if (!_qatButtonToView.TryGetValue(qatButton, out var view))
            {
                // If a new button, create a view for it now
                view = new ViewDrawRibbonQATButton(Ribbon, qatButton, _needPaint);
            }

            // Add to the lookup for future reference
            regenerate.Add(qatButton, view);
        }

        // Add child elements appropriate for each qat button
        foreach (IQuickAccessToolbarButton qatButton in qatButtons)
        {
            // Does the layout processing require the view to be updated
            if (layout)
            {
                // Update the enabled/visible state of the button
                regenerate[qatButton].Enabled = Ribbon.InDesignHelperMode || qatButton.GetEnabled();
                regenerate[qatButton].Visible = Ribbon.InDesignHelperMode || qatButton.GetVisible();
            }

            // Always add the group view
            Add(regenerate[qatButton]);

            // Remove entries we are still using
            if (_qatButtonToView.ContainsKey(qatButton))
            {
                _qatButtonToView.Remove(qatButton);
            }
        }

        // Dispose of views no longer required
        foreach (ViewDrawRibbonQATButton view in _qatButtonToView.Values)
        {
            view.Dispose();
        }

        // No longer need the old lookup
        _qatButtonToView = regenerate;

        // Always add the customization/overflow button last
        if (_extraButton != null)
        {
            Add(_extraButton);
        }
    }

    private void OnExtraButtonClick(object sender, EventHandler? finishDelegate)
    {
        var button = (ViewDrawRibbonQATExtraButton)sender;

        // Convert the button rectangle to screen coordinates
        Rectangle screenRect = ParentControl.RectangleToScreen(button.ClientRectangle);

        if (_extraButton is { Overflow: true })
        {
            Ribbon.DisplayQATOverflowMenu(screenRect, this, finishDelegate);
        }
        else
        {
            Ribbon.DisplayQATCustomizeMenu(screenRect, this, finishDelegate);
        }
    }
    #endregion
}