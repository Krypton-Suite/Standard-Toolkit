#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Backstage view implementation using KryptonSplitContainer for professional layout.
/// </summary>
internal class VisualPopupBackstage : KryptonSplitContainer 
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private KryptonRibbonBackstagePage? _selectedPage;
    private readonly List<KryptonButton> _tabButtons;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the VisualPopupBackstage class.
    /// </summary>
    /// <param name="ribbon">Owning ribbon instance.</param>
    /// <param name="palette">Local palette setting to use initially.</param>
    /// <param name="paletteMode">Palette mode setting to use initially.</param>
    /// <param name="redirector">Redirector used for obtaining palette values.</param>
    /// <param name="ownerBounds">Screen bounds of the owning control.</param>
    /// <param name="keyboardActivated">Was the backstage activated by a keyboard action.</param>
    public VisualPopupBackstage(KryptonRibbon ribbon,
        PaletteBase? palette,
        PaletteMode paletteMode,
        PaletteRedirect redirector,
        Rectangle ownerBounds,
        bool keyboardActivated)
    {
        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        _tabButtons = new List<KryptonButton>();

        // Configure the split container for backstage layout
        Dock = DockStyle.Fill;
        Orientation = Orientation.Vertical;
        FixedPanel = FixedPanel.Panel1;
        SplitterDistance = _ribbon.BackstageValues.NavigationWidth;
        SplitterWidth = 1;
        IsSplitterFixed = !_ribbon.BackstageValues.AllowNavigationResize;

        // Configure Panel1 (navigation area) with alternate style
        Panel1.PanelBackStyle = PaletteBackStyle.PanelAlternate;
        Panel1.Padding = new Padding(20); // Increased padding for better spacing

        // Configure Panel2 (content area) with client style  
        Panel2.PanelBackStyle = PaletteBackStyle.PanelClient;
        Panel2.Padding = new Padding(30);

        // Set up navigation and content
        SetupNavigation();
        SetupCloseButton();

        // Select the first visible page
        SelectFirstVisiblePage();
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the selected backstage page.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonRibbonBackstagePage? SelectedPage
    {
        get => _selectedPage;
        set
        {
            if (_selectedPage != value)
            {
                _selectedPage = value;
                UpdateTabSelection();
                UpdateContentPanel();
                // Update the ribbon's selected backstage page which will fire the event
                _ribbon.SelectedBackstagePage = _selectedPage;
            }
        }
    }

    /// <summary>
    /// Close the backstage view.
    /// </summary>
    public void Close() => _ribbon.HideBackstage();

    /// <summary>
    /// Refresh the navigation buttons to reflect changes in the backstage pages collection.
    /// </summary>
    public void RefreshNavigation()
    {
        SetupNavigation();
        UpdateTabSelection();
    }

    /// <summary>
    /// Handles key down events for the backstage view.
    /// </summary>
    /// <param name="msg">The Windows message.</param>
    /// <param name="keyData">The key data.</param>
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        // Handle ESC key to close backstage
        if (keyData == Keys.Escape)
        {
            Close();
            return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }
    #endregion

    #region Implementation
    private void SetupNavigation()
    {
        // Clear existing navigation buttons (but preserve close button)
        var closeButton = Panel1.Controls.OfType<KryptonButton>().FirstOrDefault(b => b.Text == "← Back");
        Panel1.Controls.Clear();
        _tabButtons.Clear();

        int yPos = 10; // Start position within the padding
        foreach (var page in _ribbon.BackstagePages)
        {
            if (!page.Visible) continue;

            var button = new KryptonButton
            {
                Text = page.Text,
                Size = new Size(_ribbon.BackstageValues.NavigationWidth - 40, 45), // Account for 20px padding on each side
                Location = new Point(0, yPos),
                ButtonStyle = ButtonStyle.NavigatorStack,
                Tag = page,
                TabStop = true
            };

            button.Click += OnTabButtonClick;
            Panel1.Controls.Add(button);
            _tabButtons.Add(button);

            yPos += 55; // More space between buttons (45px button + 10px gap)
        }

        // Re-add the close button if it existed
        if (closeButton != null)
        {
            Panel1.Controls.Add(closeButton);
        }
    }

    private void SetupCloseButton()
    {
        var closeButton = new KryptonButton
        {
            Text = "← Back",
            Size = new Size(_ribbon.BackstageValues.NavigationWidth - 40, 40),
            Location = new Point(0, Panel1.Height - 50),
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
            ButtonStyle = ButtonStyle.NavigatorStack
        };

        closeButton.Click += (s, e) => Close();
        Panel1.Controls.Add(closeButton);
    }

    private void OnTabButtonClick(object? sender, EventArgs e)
    {
        if (sender is KryptonButton button && button.Tag is KryptonRibbonBackstagePage page)
        {
            SelectedPage = page;
        }
    }

    private void UpdateTabSelection()
    {
        foreach (var button in _tabButtons)
        {
            if (button.Tag == _selectedPage)
            {
                // Use checked appearance for selected button
                button.StateNormal.Back.Draw = InheritBool.True;
                button.StateNormal.Back.ColorStyle = PaletteColorStyle.Solid;
            }
            else
            {
                // Use normal appearance for unselected buttons
                button.StateNormal.Back.Draw = InheritBool.Inherit;
                button.StateNormal.Back.ColorStyle = PaletteColorStyle.Inherit;
            }
        }
    }

    private void UpdateContentPanel()
    {
        Panel2.Controls.Clear();

        if (_selectedPage?.ContentPanel != null)
        {
            _selectedPage.ContentPanel.Dock = DockStyle.Fill;
            Panel2.Controls.Add(_selectedPage.ContentPanel);
        }
        else if (_selectedPage != null)
        {
            var titleLabel = new KryptonLabel
            {
                Text = _selectedPage.TextTitle,
                Location = new Point(0, 0),
                AutoSize = true,
                LabelStyle = LabelStyle.TitlePanel
            };
            Panel2.Controls.Add(titleLabel);

            if (!string.IsNullOrEmpty(_selectedPage.TextDescription))
            {
                var descLabel = new KryptonLabel
                {
                    Text = _selectedPage.TextDescription,
                    Location = new Point(0, 50),
                    AutoSize = true,
                    MaximumSize = new Size(Panel2.Width - 60, 0),
                    LabelStyle = LabelStyle.NormalPanel
                };
                Panel2.Controls.Add(descLabel);
            }
        }
    }

    private void SelectFirstVisiblePage()
    {
        var firstVisible = _ribbon.BackstagePages.FirstOrDefault(p => p.Visible);
        if (firstVisible != null)
        {
            SelectedPage = firstVisible;
        }
    }
    #endregion
}