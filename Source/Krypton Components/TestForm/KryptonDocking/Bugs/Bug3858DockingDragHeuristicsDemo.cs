#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Docking;
using Krypton.Navigator;
using Krypton.Toolkit;
using Krypton.Workspace;

namespace TestForm;

/// <summary>
/// Demo for Issue #3858: docking drag-target priority with nested cells, auto-hide strips,
/// and Rounded / Square / Solid drag feedback modes.
/// </summary>
public partial class Bug3858DockingDragHeuristicsDemo : KryptonForm
{
    private readonly KryptonManager _kryptonManager = new();
    private int _pageCounter;
    private KryptonCustomPaletteBase? _feedbackPalette;
    private PaletteMode _savedPaletteMode;
    private KryptonCustomPaletteBase? _savedCustomPalette;

    public Bug3858DockingDragHeuristicsDemo()
    {
        InitializeComponent();
        InitializeDocking();
        SeedLayout();
        UpdateStatus("Ready. Drag tabs onto nested cells, control edges, and auto-hide strips.");
    }

    private void InitializeDocking()
    {
        kryptonDockingManager1.ManageControl("Control", kryptonPanel1);
        kryptonDockingManager1.ManageWorkspace("Workspace", kryptonDockableWorkspace1);
        kryptonDockingManager1.ManageFloating("Floating", this);
    }

    private void SeedLayout()
    {
        // Nested side-by-side workspace cells so drag indicators compete across cell vs control edges.
        kryptonDockableWorkspace1.Root.Children!.Clear();
        var leftCell = new KryptonWorkspaceCell { UniqueName = "Bug3858_LeftCell" };
        var rightCell = new KryptonWorkspaceCell { UniqueName = "Bug3858_RightCell" };
        kryptonDockableWorkspace1.Root.Children.AddRange(new Component[] { leftCell, rightCell });

        var leftPage = CreatePage("Left Cell", Color.LightBlue);
        var rightPage = CreatePage("Right Cell", Color.LightGreen);
        leftCell.Pages.Add(leftPage);
        rightCell.Pages.Add(rightPage);

        // Edge-docked page that can be auto-hidden (exercises strip targets near control edges).
        var toolbox = CreatePage("Toolbox", Color.Lavender);
        kryptonDockingManager1.AddDockspace("Control", DockingEdge.Left, new[] { toolbox });

        UpdateStatus("Seeded nested left/right cells and a left docked Toolbox (use pin to auto-hide).");
    }

    private KryptonPage CreatePage(string name, Color backColor)
    {
        var page = new KryptonPage
        {
            Name = $"Page_{_pageCounter++}",
            Text = name,
            TextTitle = name,
            TextDescription = $"Document: {name}",
            UniqueName = $"Unique_{name}_{Guid.NewGuid():N}",
            MinimumSize = new Size(150, 150)
        };

        page.SetFlags(KryptonPageFlags.AllowConfigSave | KryptonPageFlags.DockingAllowDocked |
                      KryptonPageFlags.DockingAllowFloating | KryptonPageFlags.DockingAllowAutoHidden |
                      KryptonPageFlags.DockingAllowWorkspace);

        var panel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            BackColor = backColor,
            StateCommon = { Color1 = backColor }
        };

        var label = new KryptonLabel
        {
            Text = $"{name}\n\nDrag this tab.\n• Outer edge diamonds = control dock\n• Centre diamond = transfer into cell\n• Escape cancels drag",
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.NormalPanel,
            StateCommon =
            {
                LongText =
                {
                    Font = new Font("Segoe UI", 9F),
                    TextH = PaletteRelativeAlign.Center,
                    TextV = PaletteRelativeAlign.Center
                }
            }
        };

        panel.Controls.Add(label);
        page.Controls.Add(panel);
        page.Size = new Size(300, 250);

        return page;
    }

    private void BtnAddDocument_Click(object? sender, EventArgs e)
    {
        var names = new[] { "Document A", "Document B", "Document C", "Output", "Watch" };
        var colors = new[] { Color.LightBlue, Color.LightGreen, Color.LightYellow, Color.PaleTurquoise, Color.MistyRose };
        var index = Math.Min(_pageCounter % names.Length, colors.Length - 1);
        var page = CreatePage(names[index], colors[index]);
        kryptonDockingManager1.AddToWorkspace("Workspace", new[] { page });
        UpdateStatus($"Added '{names[index]}' to workspace. Drag tabs to exercise target priority.");
    }

    private void BtnAddDocked_Click(object? sender, EventArgs e)
    {
        var page = CreatePage($"Docked {_pageCounter}", Color.Honeydew);
        kryptonDockingManager1.AddDockspace("Control", DockingEdge.Right, new[] { page });
        UpdateStatus($"Added right-edge docked page '{page.Text}'. Pin it to create an auto-hide strip.");
    }

    private void BtnAutoHideToolbox_Click(object? sender, EventArgs e)
    {
        KryptonPage? toolbox = kryptonDockingManager1.Pages.FirstOrDefault(p => p.Text == "Toolbox");
        if (toolbox == null)
        {
            UpdateStatus("No Toolbox page found. Use Reset Layout, then try again.");
            return;
        }

        kryptonDockingManager1.MakeAutoHiddenRequest(toolbox.UniqueName);
        UpdateStatus("Requested auto-hide for Toolbox. Hover the left strip, then drag another tab near that edge.");
    }

    private void BtnResetLayout_Click(object? sender, EventArgs e)
    {
        foreach (KryptonPage page in kryptonDockingManager1.Pages.ToArray())
        {
            kryptonDockingManager1.RemovePage(page, true);
        }

        _pageCounter = 0;
        SeedLayout();
        UpdateStatus("Layout reset.");
    }

    private void RadioFeedback_CheckedChanged(object? sender, EventArgs e)
    {
        if (sender is not KryptonRadioButton { Checked: true } radio)
        {
            return;
        }

        PaletteDragFeedback feedback = radio.Name switch
        {
            nameof(radioSquare) => PaletteDragFeedback.Square,
            nameof(radioSolid) => PaletteDragFeedback.Block,
            _ => PaletteDragFeedback.Rounded
        };

        ApplyFeedbackMode(feedback);
        UpdateStatus($"Drag feedback set to {feedback}. Start a drag to see docking diamonds (Rounded/Square) or solid hot areas (Block).");
    }

    private void ApplyFeedbackMode(PaletteDragFeedback feedback)
    {
        if (_feedbackPalette == null)
        {
            _savedPaletteMode = KryptonManager.CurrentGlobalPaletteMode;
            _savedCustomPalette = _kryptonManager.GlobalCustomPalette;
            _feedbackPalette = new KryptonCustomPaletteBase
            {
                BasePaletteMode = _savedPaletteMode == PaletteMode.Custom
                    ? PaletteMode.Office2010Blue
                    : _savedPaletteMode
            };
        }

        _feedbackPalette.DragDrop.Feedback = feedback;
        _kryptonManager.GlobalCustomPalette = _feedbackPalette;
    }

    private void Bug3858DockingDragHeuristicsDemo_FormClosed(object? sender, FormClosedEventArgs e)
    {
        if (_feedbackPalette == null)
        {
            return;
        }

        if (_savedCustomPalette != null)
        {
            _kryptonManager.GlobalCustomPalette = _savedCustomPalette;
        }
        else
        {
            _kryptonManager.GlobalCustomPalette = null;
            _kryptonManager.GlobalPaletteMode = _savedPaletteMode;
        }

        _feedbackPalette.Dispose();
        _feedbackPalette = null;
    }

    private void UpdateStatus(string message)
    {
        var timestamp = DateTime.Now.ToString("HH:mm:ss");
        kryptonTextBoxStatus.AppendText($"[{timestamp}] {message}\r\n");
        kryptonTextBoxStatus.SelectionStart = kryptonTextBoxStatus.Text.Length;
        kryptonTextBoxStatus.ScrollToCaret();
    }
}
