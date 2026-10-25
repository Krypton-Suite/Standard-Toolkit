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

internal class VisualPopupQATOverflow : VisualPopup
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly ViewDrawRibbonQATOverflow _viewQAT;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the VisualPopupQATOverflow class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="contents">Reference to original contents which has overflow items.</param>
    /// <param name="renderer">Drawing renderer.</param>
    public VisualPopupQATOverflow([DisallowNull] KryptonRibbon ribbon,
        [DisallowNull] ViewLayoutRibbonQATContents? contents,
        IRenderer renderer)
        : base(renderer, true)
    {
        Debug.Assert(ribbon is not null);

        // Remember references needed later
        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));

        // Create a view element for drawing the group
        _viewQAT = new ViewDrawRibbonQATOverflow(ribbon, NeedPaintDelegate);

        // Create and add the element used to synch and draw the actual contents
        ViewQATContents = new ViewLayoutRibbonQATFromOverflow(this, ribbon,
            NeedPaintDelegate,
            true, contents);
        _viewQAT.Add(ViewQATContents);

        // Attach the root to the view manager instance
        ViewManager = new ViewRibbonQATOverflowManager(ribbon, this, ViewQATContents, _viewQAT);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Ensure the manager believes the mouse has left the area
            ViewManager?.MouseLeave(EventArgs.Empty);

            // Remove all child controls so they do not become disposed
            for (var i = Controls.Count - 1; i >= 0; i--)
            {
                Controls.RemoveAt(0);
            }

            // If this group is being dismissed with key tips showing
            if (_ribbon is { InKeyboardMode: true, KeyTipMode: KeyTipMode.PopupQATOverflow })
            {
                // Revert back to key tips for selected tab
                _ribbon.KeyTipMode = KeyTipMode.Root;
                _ribbon.SetKeyTips(_ribbon.GenerateKeyTipsAtTopLevel(), KeyTipMode.Root);
            }
        }
        base.Dispose(disposing);
    }
    #endregion

    #region ViewOverflowManager
    /// <summary>
    /// Gets the qat overflow manager.
    /// </summary>
    public ViewRibbonQATOverflowManager? ViewOverflowManager => ViewManager as ViewRibbonQATOverflowManager;

    #endregion

    #region ViewQATContents
    /// <summary>
    /// Gets access to the quick access toolbar contents view.
    /// </summary>
    public ViewLayoutRibbonQATContents ViewQATContents { get; }

    #endregion

    #region SetFirstFocusItem
    /// <summary>
    /// Set focus to the first focus item inside the popup group.
    /// </summary>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public void SetFirstFocusItem()
    {
        ViewOverflowManager!.FocusView = ViewQATContents.GetFirstQATView();
        PerformNeedPaint(false);
    }
    #endregion

    #region SetLastFocusItem
    /// <summary>
    /// Set focus to the last focus item inside the popup group.
    /// </summary>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public void SetLastFocusItem()
    {
        ViewOverflowManager!.FocusView = ViewQATContents.GetLastQATView();
        PerformNeedPaint(false);
    }
    #endregion

    #region SetNextFocusItem
    /// <summary>
    /// Set focus to the next focus item inside the popup group.
    /// </summary>
    public void SetNextFocusItem()
    {
        // Find the next item in sequence
        ViewBase view = ViewQATContents.GetNextQATView(ViewOverflowManager!.FocusView!);

        // Rotate around to the first item
        if (view == null)
        {
            SetFirstFocusItem();
        }
        else
        {
            ViewOverflowManager.FocusView = view;
            PerformNeedPaint(false);
        }
    }
    #endregion

    #region SetPreviousFocusItem
    /// <summary>
    /// Set focus to the previous focus item inside the popup group.
    /// </summary>
    public void SetPreviousFocusItem()
    {
        // Find the previous item in sequence
        ViewBase view = ViewQATContents.GetPreviousQATView(ViewOverflowManager!.FocusView!);

        // Rotate around to the last item
        if (view is null)
        {
            SetLastFocusItem();
        }
        else
        {
            ViewOverflowManager.FocusView = view;
            PerformNeedPaint(false);
        }
    }
    #endregion

    #region ShowCalculatingSize
    /// <summary>
    /// Show the quick access toolbar popup relative to the parent area.
    /// </summary>
    /// <param name="parentScreenRect">Screen rectangle of the parent.</param>
    /// <param name="finishDelegate">Delegate fired when popup dismissed.</param>
    public void ShowCalculatingSize(Rectangle parentScreenRect,
        EventHandler? finishDelegate)
    {
        Size popupSize;

        // Find the size the quick access toolbar requests to be
        using (var context = new ViewLayoutContext(this, Renderer))
        {
            popupSize = _viewQAT.GetPreferredSize(context);
        }

        DismissedDelegate = finishDelegate;

        // Request we be shown below the parent screen rect
        Show(new Point(parentScreenRect.Left, parentScreenRect.Bottom), popupSize);
    }
    #endregion

    #region Protected
    /// <summary>
    /// Gets the creation parameters.
    /// </summary>
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.Style |= (int)PI.WS_.CLIPCHILDREN;
            return cp;
        }
    }
    /// <summary>
    /// Raises the Layout event.
    /// </summary>
    /// <param name="levent">An EventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
        // Let base class calculate fill rectangle
        base.OnLayout(levent);
        var borderRounding = _ribbon.RibbonShape switch
        {
            PaletteRibbonShape.Office2010 => 1,
            _ => 2
        };

        // Update the region of the popup to be the border path
        using GraphicsPath roundPath = CommonHelper.RoundedRectanglePath(ClientRectangle, borderRounding);
        Region = new Region(roundPath);
    }

    /// <summary>
    /// Raises the KeyPress event.
    /// </summary>
    /// <param name="e">An KeyPressEventArgs that contains the event data.</param>
    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        // If in keyboard mode then pass character onto the key tips
        if (_ribbon is { InKeyboardMode: true, InKeyTipsMode: true })
        {
            _ribbon.AppendKeyTipPress(char.ToUpper(e.KeyChar));
        }

        base.OnKeyPress(e);
    }
    #endregion
}