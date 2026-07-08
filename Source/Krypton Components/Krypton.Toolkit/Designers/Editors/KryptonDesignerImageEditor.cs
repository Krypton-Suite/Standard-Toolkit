#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Krypton-themed designer editor for <see cref="Image"/> properties (Select Resource).
/// </summary>
public sealed class KryptonDesignerImageEditor : UITypeEditor
{
    #region Identity
    /// <inheritdoc />
    public override bool GetPaintValueSupported(ITypeDescriptorContext? context) => true;

    /// <inheritdoc />
    public override void PaintValue(PaintValueEventArgs e)
    {
        if (e.Value is Image image)
        {
            var bounds = e.Bounds;
            bounds.Width = Math.Max(bounds.Width - 1, 0);
            bounds.Height = Math.Max(bounds.Height - 1, 0);
            e.Graphics.DrawRectangle(SystemPens.WindowFrame, bounds);
            e.Graphics.DrawImage(image, e.Bounds);
        }
    }

    /// <inheritdoc />
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) =>
        context?.Instance is not null ? UITypeEditorEditStyle.Modal : base.GetEditStyle(context);

    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (context?.Instance is null
            || provider.GetService(typeof(IWindowsFormsEditorService)) is not IWindowsFormsEditorService editorService)
        {
            return value;
        }

        using var form = new KryptonDesignerSelectResourceForm(context, value as Image);
        KryptonDesignerEditorTheme.ApplyFromContext(form, context);

        if (editorService.ShowDialog(form) == DialogResult.OK)
        {
            context.OnComponentChanged();
            return form.SelectedImage;
        }

        return value;
    }
    #endregion
}

/// <summary>
/// Krypton-themed Select Resource dialog for choosing an <see cref="Image"/>.
/// </summary>
internal sealed class KryptonDesignerSelectResourceForm : KryptonForm
{
    #region Types
    private enum ResourceSource
    {
        Local,
        Project,
        Import
    }

    private sealed class ResourceEntry
    {
        public ResourceEntry(string name, Image? image)
        {
            Name = name;
            Image = image;
        }

        public string Name { get; }

        public Image? Image { get; }

        public override string ToString() => Name;
    }
    #endregion

    #region Instance Fields
    private readonly ITypeDescriptorContext _context;
    private readonly KryptonRadioButton _radioLocal;
    private readonly KryptonRadioButton _radioProject;
    private readonly KryptonRadioButton _radioImport;
    private readonly KryptonListBox _resourceList;
    private readonly PictureBox _preview;
    private readonly KryptonLabel _previewLabel;
    private readonly KryptonButton _buttonImport;
    private readonly KryptonButton _buttonClear;
    private Image? _selectedImage;
    private Image? _importedImage;
    private string? _importedPath;
    private readonly Image? _originalImage;
    private ResourceSource _source = ResourceSource.Local;
    private bool _updating;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerSelectResourceForm"/> class.
    /// </summary>
    /// <param name="context">Designer context.</param>
    /// <param name="currentImage">Current property value.</param>
    public KryptonDesignerSelectResourceForm(ITypeDescriptorContext context, Image? currentImage)
    {
        _context = context;
        _originalImage = currentImage;
        _selectedImage = currentImage;

        Text = @"Select Resource";
        ControlBox = false;
        FormBorderStyle = FormBorderStyle.Sizable;
        MinimizeBox = false;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = KryptonDesignerEditorDpi.Scale(this, new Size(520, 420));
        MinimumSize = KryptonDesignerEditorDpi.Scale(this, new Size(460, 360));

        _radioLocal = CreateRadio(@"&Local resource", ResourceSource.Local);
        _radioProject = CreateRadio(@"&Project resource file", ResourceSource.Project);
        _radioImport = CreateRadio(@"&Import", ResourceSource.Import);

        _resourceList = new KryptonListBox
        {
            Dock = DockStyle.Fill
        };
        _resourceList.SelectedIndexChanged += (_, _) => OnResourceSelected();

        _preview = new PictureBox
        {
            Dock = DockStyle.Fill,
            BorderStyle = BorderStyle.FixedSingle,
            SizeMode = PictureBoxSizeMode.Zoom,
            BackColor = SystemColors.Window
        };

        _previewLabel = new KryptonLabel
        {
            AutoSize = true,
            Values = { Text = @"Preview:" }
        };

        _buttonImport = new KryptonButton
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Values = { Text = @"Import..." }
        };
        _buttonImport.Click += (_, _) => ImportImage();

        _buttonClear = new KryptonButton
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Values = { Text = @"Clear" }
        };
        _buttonClear.Click += (_, _) =>
        {
            _selectedImage = null;
            _importedImage = null;
            _importedPath = null;
            UpdatePreview();
            if (_source == ResourceSource.Local)
            {
                PopulateLocalEntries();
            }
        };

        var okButton = new KryptonButton
        {
            DialogResult = DialogResult.OK,
            Values = { Text = KryptonManager.Strings.GeneralStrings.OK }
        };
        var cancelButton = new KryptonButton
        {
            DialogResult = DialogResult.Cancel,
            Values = { Text = KryptonManager.Strings.GeneralStrings.Cancel }
        };

        BuildLayout(okButton, cancelButton);

        AcceptButton = okButton;
        CancelButton = cancelButton;

        _source = currentImage is null ? ResourceSource.Local : ResourceSource.Local;
        _updating = true;
        _radioLocal.Checked = true;
        _updating = false;
        ApplySource();
        UpdatePreview();
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the image selected by the user.
    /// </summary>
    public Image? SelectedImage => _selectedImage;
    #endregion

    #region Implementation
    private KryptonRadioButton CreateRadio(string text, ResourceSource source)
    {
        var radio = new KryptonRadioButton
        {
            AutoSize = true,
            Values = { Text = text }
        };
        radio.CheckedChanged += (_, _) =>
        {
            if (!_updating && radio.Checked)
            {
                _source = source;
                ApplySource();
            }
        };
        return radio;
    }

    private void BuildLayout(KryptonButton okButton, KryptonButton cancelButton)
    {
        var sourceGroup = new KryptonGroupBox
        {
            Dock = DockStyle.Top,
            Height = KryptonDesignerEditorDpi.Scale(this, 110),
            Values = { Heading = @"Select resource source" }
        };

        var sourceLayout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Padding = new Padding(3),
            BackColor = Color.Transparent
        };
        sourceLayout.Controls.Add(_radioLocal);
        sourceLayout.Controls.Add(_radioProject);
        sourceLayout.Controls.Add(_radioImport);
        sourceGroup.Panel.Controls.Add(sourceLayout);

        var listGroup = new KryptonGroupBox
        {
            Dock = DockStyle.Fill,
            Values = { Heading = @"Resource" }
        };
        listGroup.Panel.Controls.Add(_resourceList);

        var previewPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            BackColor = Color.Transparent
        };
        previewPanel.RowStyles.Add(new RowStyle());
        previewPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        previewPanel.Controls.Add(_previewLabel, 0, 0);
        previewPanel.Controls.Add(_preview, 0, 1);

        var actionButtons = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            AutoSize = true,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            Padding = new Padding(0, 6, 0, 0),
            BackColor = Color.Transparent
        };
        actionButtons.Controls.Add(_buttonImport);
        actionButtons.Controls.Add(_buttonClear);

        var body = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 2,
            BackColor = Color.Transparent
        };
        body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
        body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
        body.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        body.RowStyles.Add(new RowStyle());
        body.Controls.Add(listGroup, 0, 0);
        body.Controls.Add(previewPanel, 1, 0);
        body.Controls.Add(actionButtons, 0, 1);
        body.SetColumnSpan(actionButtons, 2);

        var content = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            BackColor = Color.Transparent
        };
        content.RowStyles.Add(new RowStyle());
        content.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        content.Controls.Add(sourceGroup, 0, 0);
        content.Controls.Add(body, 0, 1);

        Controls.Add(KryptonDesignerEditorContentPanel.Create(this, content));
        Controls.Add(KryptonDesignerEditorButtonBar.Create(this, okButton, cancelButton));
    }

    private void ApplySource()
    {
        switch (_source)
        {
            case ResourceSource.Project:
                _buttonImport.Enabled = false;
                PopulateProjectEntries();
                break;
            case ResourceSource.Import:
                _buttonImport.Enabled = true;
                PopulateImportEntries();
                break;
            default:
                _buttonImport.Enabled = true;
                PopulateLocalEntries();
                break;
        }

        UpdatePreview();
    }

    private void PopulateLocalEntries()
    {
        _resourceList.Items.Clear();
        _resourceList.Items.Add(new ResourceEntry(@"(none)", null));
        if (_originalImage is not null)
        {
            _resourceList.Items.Add(new ResourceEntry(@"(current)", _originalImage));
        }

        if (_importedImage is not null)
        {
            _resourceList.Items.Add(new ResourceEntry(@"(imported)", _importedImage));
        }

        SelectEntryMatching(_selectedImage);
    }

    private void PopulateProjectEntries()
    {
        _resourceList.Items.Clear();
        _resourceList.Items.Add(new ResourceEntry(@"(none)", null));

        foreach (var entry in EnumerateProjectImages(_context))
        {
            _resourceList.Items.Add(new ResourceEntry(entry.Name, entry.Image));
        }

        if (_resourceList.Items.Count == 1)
        {
            _resourceList.Items.Add(new ResourceEntry(@"(no project images found)", null));
        }

        SelectEntryMatching(_selectedImage);
    }

    private void PopulateImportEntries()
    {
        _resourceList.Items.Clear();
        _resourceList.Items.Add(new ResourceEntry(@"(none)", null));
        if (_importedImage is not null)
        {
            var name = string.IsNullOrWhiteSpace(_importedPath)
                ? @"imported"
                : Path.GetFileName(_importedPath);
            _resourceList.Items.Add(new ResourceEntry(name, _importedImage));
            _resourceList.SelectedIndex = 1;
            _selectedImage = _importedImage;
        }
        else
        {
            _resourceList.SelectedIndex = 0;
            _selectedImage = null;
        }
    }

    private void SelectEntryMatching(Image? image)
    {
        if (image is null)
        {
            _resourceList.SelectedIndex = _resourceList.Items.Count > 0 ? 0 : -1;
            return;
        }

        for (var i = 0; i < _resourceList.Items.Count; i++)
        {
            if (_resourceList.Items[i] is ResourceEntry entry && ReferenceEquals(entry.Image, image))
            {
                _resourceList.SelectedIndex = i;
                return;
            }
        }

        // Fall back to current/original entry when present.
        for (var i = 0; i < _resourceList.Items.Count; i++)
        {
            if (_resourceList.Items[i] is ResourceEntry { Name: @"(current)" or @"(imported)" })
            {
                _resourceList.SelectedIndex = i;
                return;
            }
        }

        if (_resourceList.Items.Count > 0)
        {
            _resourceList.SelectedIndex = 0;
        }
    }

    private void OnResourceSelected()
    {
        if (_resourceList.SelectedItem is ResourceEntry entry)
        {
            _selectedImage = entry.Image;
            UpdatePreview();
        }
    }

    private void UpdatePreview()
    {
        _preview.Image = _selectedImage;
        _previewLabel.Values.Text = _selectedImage is null
            ? @"Preview:"
            : $@"Preview: {_selectedImage.Width} × {_selectedImage.Height}";
    }

    private void ImportImage()
    {
        using var dialog = new KryptonOpenFileDialog
        {
            Title = @"Import Image",
            Filter = @"Image Files|*.bmp;*.gif;*.jpg;*.jpeg;*.png;*.ico;*.emf;*.wmf|All Files|*.*",
            Multiselect = false
        };

        if (dialog.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.FileName))
        {
            return;
        }

        try
        {
            using var stream = File.OpenRead(dialog.FileName);
            var buffer = new byte[stream.Length];
            _ = stream.Read(buffer, 0, buffer.Length);
            using var memory = new MemoryStream(buffer);
            var loaded = Image.FromStream(memory);
            _importedImage = (Image)loaded.Clone();
            _importedPath = dialog.FileName;
            _selectedImage = _importedImage;

            if (_source != ResourceSource.Import)
            {
                _updating = true;
                _radioImport.Checked = true;
                _source = ResourceSource.Import;
                _updating = false;
            }

            ApplySource();
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show(this,
                $@"Could not import image:{Environment.NewLine}{ex.Message}",
                @"Select Resource",
                KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Error);
        }
    }

    private static IEnumerable<ResourceEntry> EnumerateProjectImages(ITypeDescriptorContext context)
    {
        if (context.GetService(typeof(IDesignerHost)) is not IDesignerHost host
            || host.RootComponent?.GetType() is not Type rootType
            || rootType.Assembly is not Assembly assembly)
        {
            yield break;
        }

        var candidates = new List<Type>();
        var propertiesNs = rootType.Namespace is null ? @"Properties.Resources" : $"{rootType.Namespace}.Properties.Resources";
        var named = assembly.GetType(propertiesNs, throwOnError: false);
        if (named is not null)
        {
            candidates.Add(named);
        }

        try
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.Name == @"Resources"
                    && type.Namespace is not null
                    && type.Namespace.EndsWith(@".Properties", StringComparison.Ordinal))
                {
                    candidates.Add(type);
                }
            }
        }
        catch (ReflectionTypeLoadException)
        {
            // Ignore partially loaded designer project assemblies.
        }

        var seen = new HashSet<string>(StringComparer.Ordinal);
        foreach (var resourcesType in candidates.Distinct())
        {
            PropertyInfo[] properties;
            try
            {
                properties = resourcesType.GetProperties(BindingFlags.Public | BindingFlags.Static);
            }
            catch (Exception)
            {
                continue;
            }

            foreach (var property in properties)
            {
                if (!typeof(Image).IsAssignableFrom(property.PropertyType) || !seen.Add(property.Name))
                {
                    continue;
                }

                Image? image = null;
                try
                {
                    image = property.GetValue(null, null) as Image;
                }
                catch (Exception)
                {
                    // Skip resources that cannot be loaded at design time.
                }

                if (image is not null)
                {
                    yield return new ResourceEntry(property.Name, image);
                }
            }
        }
    }
    #endregion
}
