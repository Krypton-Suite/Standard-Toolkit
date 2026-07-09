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
/// Krypton-themed Select Resource dialog for choosing an <see cref="Image"/>.
/// </summary>
internal partial class KryptonDesignerSelectResourceForm : KryptonForm
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

        InitializeComponent();

        Text = @"Select Resource";
        ControlBox = false;
        FormBorderStyle = FormBorderStyle.Sizable;
        MinimizeBox = false;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = KryptonDesignerEditorDpi.Scale(this, new Size(520, 420));
        MinimumSize = KryptonDesignerEditorDpi.Scale(this, new Size(460, 360));

        AcceptButton = _buttonOk;
        CancelButton = _buttonCancel;

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

    private void ClearImage()
    {
        _selectedImage = null;
        _importedImage = null;
        _importedPath = null;
        UpdatePreview();
        if (_source == ResourceSource.Local)
        {
            PopulateLocalEntries();
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
