#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Ribbon;
using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Issue #4550: <see cref="KryptonTextBox"/> and <see cref="KryptonSearchBox"/> in the form caption.
/// </summary>
public partial class CaptionTextBoxDemo : KryptonCaptionForm
{
    private readonly KryptonTextBox _captionText;
    private readonly KryptonSearchBox _captionSearch;
    private readonly KryptonButton _captionButton;
    private readonly KryptonComboBox _captionCombo;
    private readonly KryptonComboBox _comboEdge;
    private readonly KryptonLabel _comboEdgeLabel;
    private bool _syncing;

    public CaptionTextBoxDemo()
    {
        InitializeComponent();

        _captionText = new KryptonTextBox
        {
            Width = 180
        };
        _captionText.CueHint.CueHintText = "Type here";
        _captionText.TextChanged += OnCaptionTextChanged;

        _captionSearch = new KryptonSearchBox
        {
            Width = 220
        };
        _captionSearch.CueHint.CueHintText = "Search";
        _captionSearch.SetSearchSuggestions(new[]
        {
            "Alpha",
            "Beta",
            "Caption",
            "Krypton",
            "Search",
            "Text box"
        });
        _captionSearch.TextChanged += OnCaptionTextChanged;
        _captionSearch.Search += OnCaptionSearch;

        _captionButton = new KryptonButton
        {
            Text = @"Caption",
            AutoSize = true
        };
        _captionButton.Click += (_, __) => Log("Caption button clicked.");

        _captionCombo = new KryptonComboBox
        {
            Width = 140,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        _captionCombo.Items.AddRange(new object[] { "One", "Two", "Three" });
        _captionCombo.SelectedIndex = 0;
        _captionCombo.SelectedIndexChanged += (_, __) => Log($"Caption combo: {_captionCombo.Text}");

        _options.Height = 80;
        _comboEdge = new KryptonComboBox
        {
            DropDownWidth = 120,
            Location = new Point(68, 40),
            Size = new Size(120, 22),
            TabIndex = 6
        };
        _comboEdgeLabel = new KryptonLabel
        {
            Location = new Point(12, 42)
        };
        _comboEdgeLabel.Values.Text = @"Combo";
        _options.Controls.Add(_comboEdgeLabel);
        _options.Controls.Add(_comboEdge);
        _comboEdge.SelectedIndexChanged += OnComboEdgeChanged;

        _syncing = true;
        _editorChoice.Items.AddRange(new object[] { "Right", "Left", "Center" });
        _edgeChoice.Items.AddRange(new object[] { "Right", "Left", "Center" });
        _comboEdge.Items.AddRange(new object[] { "Right", "Left", "Center" });
        _themeChoice.Items.AddRange(new object[]
        {
            PaletteMode.Office2024Blue,
            PaletteMode.Office2024Black,
            PaletteMode.Office2024White,
            PaletteMode.Office2024Silver,
            PaletteMode.Microsoft365Blue,
            PaletteMode.Office2010Blue
        });
        _editorLabel.Values.Text = @"Search";
        _edgeLabel.Values.Text = @"Button";
        _editorChoice.SelectedIndex = 0;
        _edgeChoice.SelectedIndex = 1;
        _comboEdge.SelectedIndex = 2;
        _themeChoice.SelectedIndex = 0;
        _syncing = false;

        CreateRibbon();
        ApplyTheme(PaletteMode.Office2024Blue);

        CaptionItems.Add(new KryptonFormCaptionItem(_captionButton, FormCaptionTextBoxEdge.Left));
        CaptionItems.Add(new KryptonFormCaptionItem(_captionCombo, FormCaptionTextBoxEdge.Center));
        CaptionItems.Add(new KryptonFormCaptionItem(_captionSearch, FormCaptionTextBoxEdge.Right));
        CaptionItems.Add(new KryptonFormCaptionItem(_captionText, FormCaptionTextBoxEdge.Right));
        Log("Ready. CaptionItems hosts the button on the left, the combo in the centre, and the search and text boxes on the right.");
    }

    private void CreateRibbon()
    {
        var home = new KryptonRibbonTab { Text = @"Home", KeyTip = @"H" };
        var paste = new KryptonRibbonGroupButton { TextLine1 = @"Paste", KeyTip = @"V" };
        var cut = new KryptonRibbonGroupButton { TextLine1 = @"Cut", KeyTip = @"X" };
        var copy = new KryptonRibbonGroupButton { TextLine1 = @"Copy", KeyTip = @"C" };
        var lines = new KryptonRibbonGroupTriple();
        lines.Items!.Add(paste);
        lines.Items.Add(cut);
        lines.Items.Add(copy);
        var group = new KryptonRibbonGroup { TextLine1 = @"Clipboard" };
        group.Items.Add(lines);
        home.Groups.Add(group);
        _ribbon.RibbonTabs.Add(home);
    }

    private void OnEditorChoiceChanged(object? sender, EventArgs e)
    {
        if (!_syncing)
        {
            ApplyEditor();
        }
    }

    private void OnEdgeChoiceChanged(object? sender, EventArgs e)
    {
        if (!_syncing)
        {
            SetItemEdge(_captionButton, EdgeFromIndex(_edgeChoice.SelectedIndex));
            Log($"Button edge is {EdgeFromIndex(_edgeChoice.SelectedIndex)}.");
        }
    }

    private void OnComboEdgeChanged(object? sender, EventArgs e)
    {
        if (!_syncing)
        {
            SetItemEdge(_captionCombo, EdgeFromIndex(_comboEdge.SelectedIndex));
            Log($"Combo edge is {EdgeFromIndex(_comboEdge.SelectedIndex)}.");
        }
    }

    private void OnThemeChoiceChanged(object? sender, EventArgs e)
    {
        if (!_syncing && _themeChoice.SelectedItem is PaletteMode mode)
        {
            ApplyTheme(mode);
        }
    }

    private void ApplyEditor()
    {
        var edge = EdgeFromIndex(_editorChoice.SelectedIndex);
        SetItemEdge(_captionSearch, edge);
        SetItemEdge(_captionText, edge);
        Log($"Search and text box edge is {edge}.");
    }

    private void SetItemEdge(Control control, FormCaptionTextBoxEdge edge)
    {
        foreach (KryptonFormCaptionItem item in CaptionItems)
        {
            if (ReferenceEquals(item.Control, control))
            {
                item.Edge = edge;
                return;
            }
        }
    }

    private static FormCaptionTextBoxEdge EdgeFromIndex(int index) =>
        index switch
        {
            1 => FormCaptionTextBoxEdge.Left,
            2 => FormCaptionTextBoxEdge.Center,
            _ => FormCaptionTextBoxEdge.Right
        };

    private void ApplyTheme(PaletteMode mode)
    {
        _manager.GlobalPaletteMode = mode;
        PaletteMode = mode;
        _ribbon.PaletteMode = mode;
        Log($"Theme is {mode}.");
    }

    private void OnCaptionTextChanged(object? sender, EventArgs e)
    {
        if (sender is KryptonTextBox box)
        {
            Log($"Text: {box.Text}");
        }
    }

    private void OnCaptionSearch(object? sender, SearchEventArgs e) => Log($"Search: {e.SearchText}");

    private void Log(string message) =>
        _log.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
}
