#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// ToolStripMenuItem with Krypton-style per-item palette overrides for parity with KryptonContextMenuItem.
/// </summary>
public class KryptonToolStripMenuItem : ToolStripMenuItem
{
    public KryptonToolStripMenuItem() => InitializeStates();

    public KryptonToolStripMenuItem(string text) : base(text) => InitializeStates();

    public KryptonToolStripMenuItem(string text, Image image, EventHandler onClick) : base(text, image, onClick) => InitializeStates();

    public KryptonToolStripMenuItem(string text, Image image, params ToolStripItem[] dropDownItems)
        : base(text, image, dropDownItems) => InitializeStates();

    private void InitializeStates()
    {
        StateCommon = new PaletteContextMenuItemStateRedirect();
        StateNormal = new PaletteContextMenuItemState(StateCommon);
        StateHighlight = new PaletteContextMenuItemStateHighlight(StateCommon);
        StateChecked = new PaletteContextMenuItemStateChecked(StateCommon);
        StateDisabled = new PaletteContextMenuItemState(StateCommon);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PaletteContextMenuItemStateRedirect StateCommon { get; private set; }

    [Category(@"Visuals")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContextMenuItemState StateNormal { get; private set; }
    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    [Category(@"Visuals")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContextMenuItemStateHighlight StateHighlight { get; private set; }
    private bool ShouldSerializeStateHighlight() => !StateHighlight.IsDefault;

    [Category(@"Visuals")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContextMenuItemStateChecked StateChecked { get; private set; }
    private bool ShouldSerializeStateChecked() => !StateChecked.IsDefault;

    [Category(@"Visuals")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteContextMenuItemState StateDisabled { get; private set; }
    private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;
}
