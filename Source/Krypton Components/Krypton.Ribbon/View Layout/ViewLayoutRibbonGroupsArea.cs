#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Contains all the layout of the groups area.
/// </summary>
internal class ViewLayoutRibbonGroupsArea : ViewDrawPanel
{
    #region Static Fields
    private static readonly Padding _preferredNormalPadding = new Padding(0, 0, 1, 0);
    private static readonly Padding _preferredMinimizedPadding = new Padding(0, 1, 1, 0);
    private static readonly Padding _layoutNormalPadding = new Padding(0, -1, 1, 1);
    private static readonly Padding _layoutMinimizedPadding = new Padding(0, 0, 1, 1);
    #endregion

    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly PaletteBackInheritRedirect _backInherit;
    private readonly ViewLayoutDocker _groupsDocker;
    private readonly ViewLayoutCenter _compactStrip;
    private ButtonSpecManagerLayoutRibbon? _compactButtonManager;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonGroupsArea class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="redirect">Reference to redirector for palette settings.</param>
    /// <param name="needPaintDelegate">Delegate for notifying paint/layout changes.</param>
    public ViewLayoutRibbonGroupsArea([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] PaletteRedirect? redirect,
        [DisallowNull] NeedPaintHandler? needPaintDelegate)
    {
        Debug.Assert(ribbon is not null);
        Debug.Assert(redirect is not null);
        Debug.Assert(needPaintDelegate is not null);

        // Remember the incoming reference
        _ribbon = ribbon ?? ThrowHelper.ThrowArgumentNullException(ribbon);

        if (redirect is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(redirect));
        }

        if (needPaintDelegate is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(needPaintDelegate));
        }


        // Create access to the redirector and use as our palette source
        _backInherit = new PaletteBackInheritRedirect(redirect, PaletteBackStyle.PanelClient);
        SetPalettes(_backInherit);

        // Create and add the only child we need, the groups area border element
        ViewGroups = new ViewDrawRibbonGroupsBorderSynch(ribbon, needPaintDelegate);
        var compactButtons = new ViewLayoutDocker();
        _compactStrip = new ViewLayoutCenter(compactButtons)
        {
            Visible = ShowCompactOnGroups(ribbon)
        };
        _groupsDocker = new ViewLayoutDocker();
        _groupsDocker.Add(ViewGroups, ViewDockStyle.Fill);
        _groupsDocker.Add(_compactStrip, DockForCompact(ribbon));
        Add(_groupsDocker);
        CreateCompactButton(redirect, needPaintDelegate, compactButtons);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutRibbonGroupsArea:{Id}";

    #endregion

    #region ViewGroups
    /// <summary>
    /// Gets access to the groups border view.
    /// </summary>
    public ViewDrawRibbonGroupsBorderSynch ViewGroups { get; }

    #endregion

    #region RecreateButtons
    /// <summary>
    /// Recreates the compact-mode arrow after compact mode, visibility, or placement changes.
    /// </summary>
    public void RecreateButtons()
    {
        _compactStrip.Visible = ShowCompactOnGroups(_ribbon);
        _groupsDocker.SetDock(_compactStrip, DockForCompact(_ribbon));
        _compactButtonManager?.RecreateButtons();
    }
    #endregion

    #region Implementation
    private static bool ShowCompactOnGroups(KryptonRibbon ribbon) =>
        ribbon.ShowCompactButton && ribbon.CompactButtonLocation != CompactButtonLocation.Tabs;

    private static ViewDockStyle DockForCompact(KryptonRibbon ribbon) =>
        ribbon.CompactButtonLocation == CompactButtonLocation.GroupsTrailing
            ? ViewDockStyle.Right
            : ViewDockStyle.Bottom;

    private void CreateCompactButton(PaletteRedirect redirect, NeedPaintHandler needPaint, ViewLayoutDocker compactButtons)
    {
        // Groups host. Hidden on the tab-row placement, and unless ShowCompactButton is set.
        var fixedSpecs = new ViewLayoutRibbonTabsArea.RibbonButtonSpecFixedCollection(_ribbon);
        fixedSpecs.Add(new ButtonSpecCompactRibbon(_ribbon, true));
        fixedSpecs.Add(new ButtonSpecCompactRibbon(_ribbon, false));
        _compactButtonManager = new ButtonSpecManagerLayoutRibbon(_ribbon,
            redirect,
            new ViewLayoutRibbonTabsArea.RibbonButtonSpecFixedCollection(_ribbon),
            fixedSpecs,
            [compactButtons],
            [_ribbon.StateCommon],
            [PaletteMetricInt.None],
            [PaletteMetricPadding.RibbonButtonPadding],
            _ribbon.CreateToolStripRenderer,
            needPaint);
        _compactButtonManager.RecreateButtons();
    }
    #endregion

    #region BackStyle
    /// <summary>
    /// Gets and sets the background style.
    /// </summary>
    public PaletteBackStyle BackStyle
    {
        get => _backInherit.Style;
        set => _backInherit.Style = value;
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        // Get the preferred size of the contained content
        var preferredSize = new Size(0, _ribbon.CalculatedValues.GroupsHeight);
        bool belowGroups = _ribbon.ShowCompactButton && _ribbon.CompactButtonLocation == CompactButtonLocation.BelowGroups;
        _ribbon.CompactBelowGroupsReserve = belowGroups ? _compactStrip.GetPreferredSize(context).Height : 0;
        if (belowGroups)
        {
            preferredSize.Height += _ribbon.CompactBelowGroupsReserve;
        }

        // Add on the padding we need around edges
        if (_ribbon.RealMinimizedMode)
        {
            return new Size(preferredSize.Width + _preferredMinimizedPadding.Horizontal,
                preferredSize.Height + _preferredMinimizedPadding.Vertical);
        }
        else
        {
            return new Size(preferredSize.Width + _preferredNormalPadding.Horizontal,
                preferredSize.Height + _preferredNormalPadding.Vertical);
        }
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        // Find the correct padding to use
        Padding padding = _ribbon.RealMinimizedMode ? _layoutMinimizedPadding : _layoutNormalPadding;

        // Normal mode uses Top=-1 so the groups top border overlaps the bottom of the tab strip.
        // When the tab/pendant band has collapsed (e.g. ShowTabHeaders and ShowMinimizeButton both
        // false under an integrated caption), that overlap clips the top border out of the ribbon (#331).
        if (!_ribbon.RealMinimizedMode && (ClientLocation.Y <= 0))
        {
            padding = new Padding(padding.Left, 0, padding.Right, padding.Bottom);
        }

        // Reduce display rect by our border size
        context.DisplayRectangle = new Rectangle(ClientLocation.X + padding.Left,
            ClientLocation.Y + padding.Top,
            ClientWidth - padding.Horizontal,
            ClientHeight - padding.Vertical);

        // Below the groups, the arrow is placed inside the card after the groups have taken the rest.
        // Hide it so the docker does not park a full-width bar under the card.
        bool placeInsideCard = _ribbon.ShowCompactButton && _ribbon.CompactButtonLocation == CompactButtonLocation.BelowGroups;
        _ribbon.CompactBelowGroupsReserve = placeInsideCard ? _compactStrip.GetPreferredSize(context).Height : 0;
        if (placeInsideCard)
        {
            _compactStrip.Visible = false;
        }

        // Let contained content element be laid out
        base.Layout(context);

        if (placeInsideCard)
        {
            PlaceCompactStripInsideCard(context);
        }

        // Put back the original display value now we have finished
        context.DisplayRectangle = ClientRectangle;
    }

    /// <summary>
    /// Places the compact arrow at the trailing edge of the groups card.
    /// </summary>
    private void PlaceCompactStripInsideCard(ViewLayoutContext context)
    {
        Rectangle border = ViewGroups.ClientRectangle;
        Rectangle host = border;
        if (_ribbon.RibbonShape == PaletteRibbonShape.Office2024 &&
            RenderStandard.TryGetRibbonGroupArea2024Card(border, _ribbon.DeviceDpi / 96f, out Rectangle card))
        {
            host = card;
        }
        else
        {
            Padding pad = ViewGroups.BorderPadding;
            host = new Rectangle(border.X + pad.Left,
                border.Y,
                Math.Max(0, border.Width - pad.Horizontal),
                border.Height);
        }

        Size preferred = _compactStrip.GetPreferredSize(context);
        int height = Math.Max(preferred.Height, _ribbon.CompactBelowGroupsReserve);
        int width = preferred.Width > 0 ? preferred.Width : height;
        if (width > host.Width)
        {
            width = host.Width;
        }

        // Keep the glyph off the rounded corner.
        int inset = Math.Max(2, (int)(6 * _ribbon.DeviceDpi / 96f));
        int x = _ribbon.RightToLeft == RightToLeft.Yes
            ? host.X + inset
            : host.Right - width - inset;
        if (x < host.X)
        {
            x = host.X;
        }

        var stripRect = new Rectangle(x, host.Bottom - height, width, height);
        context.DisplayRectangle = stripRect;
        _compactStrip.Visible = true;
        _compactStrip.Layout(context);
    }
    #endregion
}