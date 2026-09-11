#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Hosts Mini Toolbar items as themed Krypton controls in a horizontal strip.
/// </summary>
internal sealed class MiniToolbarStrip : FlowLayoutPanel
{
    #region Static Fields

    private const int ButtonSize = 22;
    private const int SplitButtonWidth = 36;
    private const int StripPadding = 2;

    #endregion

    #region Instance Fields

    private readonly KryptonMiniToolbar _owner;
    private readonly ToolTip _toolTip;
    private IDisposable? _backMemento;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="MiniToolbarStrip"/> class.
    /// </summary>
    /// <param name="owner">Owning Mini Toolbar.</param>
    public MiniToolbarStrip(KryptonMiniToolbar owner)
    {
        _owner = owner;
        _toolTip = new ToolTip();
        WrapContents = false;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Padding = new Padding(StripPadding);
        Margin = Padding.Empty;
        FlowDirection = FlowDirection.LeftToRight;
        DoubleBuffered = true;
        ApplyChromeColors();
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _toolTip.Dispose();
            ClearHosts();
            _backMemento?.Dispose();
            _backMemento = null;
        }

        base.Dispose(disposing);
    }

    /// <inheritdoc />
    protected override void OnPaintBackground(PaintEventArgs e)
    {
        PaletteBase palette = _owner.ResolvePalette();
        IRenderer renderer = palette.GetRenderer();
        IPaletteBack back = _owner.StateCommon.ControlOuter.Back;
        if (back.GetBackDraw(PaletteState.Normal) != InheritBool.True)
        {
            using var brush = new SolidBrush(_owner.GetChromeBackColor());
            e.Graphics.FillRectangle(brush, ClientRectangle);
            return;
        }

        using var context = new RenderContext(this, e.Graphics, ClientRectangle, renderer);
        using var path = new GraphicsPath();
        var rectF = (RectangleF)ClientRectangle;
        rectF.Offset(-0.25f, -0.25f);
        path.AddRectangle(rectF);
        _backMemento = renderer.RenderStandardBack.DrawBack(context, ClientRectangle, path, back,
            VisualOrientation.Top, PaletteState.Normal, _backMemento);
    }

    /// <inheritdoc />
    public override Size GetPreferredSize(Size proposedSize)
    {
        // Measure a single unwrapped row. FlowLayoutPanel.GetPreferredSize(Size.Empty)
        // otherwise uses the default ~200px width as a wrap constraint.
        var width = Padding.Horizontal;
        var height = Padding.Vertical;
        foreach (Control control in Controls)
        {
            Size size = control.GetPreferredSize(new Size(int.MaxValue, 0));
            var controlWidth = Math.Max(control.Width, size.Width);
            if (control.MinimumSize.Width > 0)
            {
                controlWidth = Math.Max(controlWidth, control.MinimumSize.Width);
            }

            var controlHeight = Math.Max(control.Height, size.Height);
            if (control.MinimumSize.Height > 0)
            {
                controlHeight = Math.Max(controlHeight, control.MinimumSize.Height);
            }

            width += controlWidth + control.Margin.Horizontal;
            height = Math.Max(height, controlHeight + control.Margin.Vertical + Padding.Vertical);
        }

        return new Size(Math.Max(1, width), Math.Max(ButtonSize + Padding.Vertical, height));
    }

    #endregion

    #region Public

    /// <summary>
    /// Rebuilds hosted controls from the current Mini Toolbar items.
    /// </summary>
    public void Rebuild()
    {
        ClearHosts();
        foreach (KryptonMiniToolbarItemBase item in _owner.Items)
        {
            if (!item.Visible)
            {
                continue;
            }

            Control? host = CreateHost(item);
            if (host != null)
            {
                ApplyHostPalette(host);
                Controls.Add(host);
            }
        }

        ApplyChromeColors();
    }

    /// <summary>
    /// Applies the owning Mini Toolbar palette to chrome and hosted controls.
    /// </summary>
    public void ApplyPalette()
    {
        ApplyChromeColors();
        foreach (Control control in Controls)
        {
            ApplyHostPalette(control);
        }

        Invalidate(true);
    }

    #endregion

    #region Implementation

    private void ClearHosts()
    {
        while (Controls.Count > 0)
        {
            Control control = Controls[0];
            Controls.RemoveAt(0);
            control.Dispose();
        }
    }

    private Control? CreateHost(KryptonMiniToolbarItemBase item)
    {
        switch (item)
        {
            case KryptonMiniToolbarButton button:
                return CreateButton(button);
            case KryptonMiniToolbarSplitButton split:
                return CreateSplit(split);
            case KryptonMiniToolbarComboBox combo:
                return CreateCombo(combo);
            case KryptonMiniToolbarSeparator:
                return CreateSeparator();
            case KryptonMiniToolbarGallery gallery:
                return CreateGallery(gallery);
            default:
                return null;
        }
    }

    private Control CreateButton(KryptonMiniToolbarButton item)
    {
        if (item.ButtonType == KryptonMiniToolbarButtonType.Check)
        {
            var check = new KryptonCheckButton
            {
                AutoSize = false,
                ButtonStyle = ButtonStyle.LowProfile,
                Size = new Size(ButtonSize, ButtonSize),
                MinimumSize = new Size(ButtonSize, ButtonSize),
                MaximumSize = new Size(ButtonSize, ButtonSize),
                Enabled = item.Enabled,
                Checked = item.Checked,
                Margin = new Padding(1),
                KryptonCommand = item.KryptonCommand
            };
            ApplyImageAndText(check.Values, item);
            ApplyToolTip(check, item);
            check.Click += (_, _) =>
            {
                if (item.CheckOnClick && item.KryptonCommand == null)
                {
                    item.Checked = check.Checked;
                }

                item.PerformClick();
                _owner.OnItemActivated(item);
            };
            return check;
        }

        var push = new KryptonButton
        {
            AutoSize = false,
            ButtonStyle = ButtonStyle.LowProfile,
            Size = new Size(ButtonSize, ButtonSize),
            MinimumSize = new Size(ButtonSize, ButtonSize),
            MaximumSize = new Size(ButtonSize, ButtonSize),
            Enabled = item.Enabled,
            Margin = new Padding(1),
            KryptonCommand = item.KryptonCommand
        };
        ApplyImageAndText(push.Values, item);
        ApplyToolTip(push, item);
        push.Click += (_, _) =>
        {
            item.PerformClick();
            _owner.OnItemActivated(item);
        };
        return push;
    }

    private Control CreateSplit(KryptonMiniToolbarSplitButton item)
    {
        var hasDropDown = item.KryptonContextMenu != null;
        var drop = new KryptonDropButton
        {
            AutoSize = false,
            ButtonStyle = ButtonStyle.LowProfile,
            Size = new Size(hasDropDown || item.KryptonCommand != null ? SplitButtonWidth : ButtonSize, ButtonSize),
            MinimumSize = new Size(hasDropDown || item.KryptonCommand != null ? SplitButtonWidth : ButtonSize, ButtonSize),
            Enabled = item.Enabled,
            Margin = new Padding(1),
            Splitter = hasDropDown || item.KryptonCommand != null,
            KryptonCommand = item.KryptonCommand,
            KryptonContextMenu = item.KryptonContextMenu
        };
        ApplyImageAndText(drop.Values, item);
        ApplyToolTip(drop, item);
        drop.Click += (_, _) =>
        {
            item.PerformClick();
            _owner.OnItemActivated(item);
        };
        return drop;
    }

    private Control CreateCombo(KryptonMiniToolbarComboBox item)
    {
        var combo = new KryptonComboBox
        {
            AutoSize = false,
            AlwaysActive = false,
            InputControlStyle = InputControlStyle.Ribbon,
            DropButtonStyle = ButtonStyle.InputControl,
            Width = item.Width,
            MinimumSize = new Size(item.Width, 0),
            MaximumSize = new Size(item.Width, 0),
            DropDownStyle = item.DropDownStyle,
            DropDownWidth = Math.Max(item.Width + 32, 160),
            Enabled = item.Enabled,
            TabStop = false,
            Margin = new Padding(2, 1, 2, 1)
        };
        combo.ComboBox.IntegralHeight = false;
        foreach (object entry in item.Items)
        {
            combo.Items.Add(entry);
        }

        if (!string.IsNullOrEmpty(item.Text))
        {
            combo.Text = item.Text;
        }
        else if (item.SelectedIndex >= 0 && item.SelectedIndex < combo.Items.Count)
        {
            combo.SelectedIndex = item.SelectedIndex;
        }

        combo.SelectedIndexChanged += (_, _) =>
        {
            item.SelectedIndex = combo.SelectedIndex;
            item.SelectedItem = combo.SelectedItem;
            item.Text = combo.Text;
            item.RaiseSelectedIndexChanged();
            _owner.OnItemActivated(item);
        };
        combo.TextChanged += (_, _) =>
        {
            item.Text = combo.Text;
            item.RaiseTextChanged();
        };
        ApplyToolTip(combo, item);
        return combo;
    }

    private Control CreateSeparator()
    {
        var edge = new KryptonBorderEdge
        {
            Orientation = Orientation.Vertical,
            BorderStyle = PaletteBorderStyle.SeparatorLowProfile,
            Width = 1,
            Height = ButtonSize,
            Margin = new Padding(4, 4, 4, 4)
        };
        return edge;
    }

    private Control CreateGallery(KryptonMiniToolbarGallery item)
    {
        var host = new FlowLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            WrapContents = false,
            FlowDirection = FlowDirection.LeftToRight,
            BackColor = Color.Transparent,
            Margin = new Padding(1),
            Padding = Padding.Empty
        };

        if (item.ImageList == null || item.ImageList.Images.Count == 0)
        {
            return host;
        }

        var start = Math.Max(0, item.ImageIndexStart);
        var end = item.ImageIndexEnd < 0 ? item.ImageList.Images.Count - 1 : Math.Min(item.ImageIndexEnd, item.ImageList.Images.Count - 1);
        var shown = 0;
        for (var i = start; i <= end && shown < item.MaxVisibleItems; i++)
        {
            var imageIndex = i;
            Size imageSize = item.ImageList.ImageSize;
            var tileSize = new Size(Math.Max(ButtonSize, imageSize.Width + 4), Math.Max(ButtonSize, imageSize.Height + 4));
            var galleryButton = new KryptonCheckButton
            {
                AutoSize = false,
                ButtonStyle = ButtonStyle.LowProfile,
                Size = tileSize,
                MinimumSize = tileSize,
                MaximumSize = tileSize,
                Margin = new Padding(0),
                Checked = item.SelectedIndex == imageIndex
            };
            galleryButton.Values.Image = item.ImageList.Images[imageIndex];
            galleryButton.Values.Text = string.Empty;
            galleryButton.Click += (_, _) =>
            {
                item.SelectedIndex = imageIndex;
                item.PerformClick();
                _owner.OnItemActivated(item);
            };
            galleryButton.MouseEnter += (_, _) =>
                item.RaiseTrackingImage(new ImageSelectEventArgs(item.ImageList, imageIndex));
            galleryButton.MouseLeave += (_, _) =>
                item.RaiseTrackingImage(new ImageSelectEventArgs(item.ImageList, -1));
            host.Controls.Add(galleryButton);
            shown++;
        }

        return host;
    }

    private static void ApplyImageAndText(ButtonValues values, KryptonMiniToolbarItemBase item)
    {
        values.Image = item.Image;
        values.Text = item.Image != null ? string.Empty : item.Text;
        values.ExtraText = string.Empty;
    }

    private void ApplyToolTip(Control control, KryptonMiniToolbarItemBase item)
    {
        if (!string.IsNullOrEmpty(item.ToolTipText))
        {
            _toolTip.SetToolTip(control, item.ToolTipText);
        }
    }

    private void ApplyChromeColors() => BackColor = _owner.GetChromeBackColor();

    private void ApplyHostPalette(Control control)
    {
        if (control is VisualControlBase visual)
        {
            if (_owner.LocalCustomPalette != null)
            {
                visual.LocalCustomPalette = _owner.LocalCustomPalette;
            }
            else
            {
                visual.PaletteMode = _owner.PaletteMode;
            }

            if (visual is not KryptonComboBox)
            {
                visual.BackColor = _owner.GetChromeBackColor();
            }

            return;
        }

        control.BackColor = _owner.GetChromeBackColor();
        foreach (Control child in control.Controls)
        {
            ApplyHostPalette(child);
        }
    }

    #endregion
}
