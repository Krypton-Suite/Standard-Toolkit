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
/// Krypton-themed designer editor for image-list indices and keys.
/// </summary>
public sealed class KryptonDesignerImageIndexEditor : UITypeEditor
{
    #region Instance Fields
    private ImageList? _currentImageList;
    private object? _currentInstance;
    private UITypeEditor? _imageEditor;
    #endregion

    #region Identity
    /// <inheritdoc />
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) =>
        context?.Instance is not null ? UITypeEditorEditStyle.DropDown : base.GetEditStyle(context);

    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (context?.Instance is null
            || provider.GetService(typeof(IWindowsFormsEditorService)) is not IWindowsFormsEditorService editorService)
        {
            return value;
        }

        var imageList = GetImageList(context);
        if (imageList is null)
        {
            return value;
        }

        using var dropDown = new KryptonDesignerImageIndexDropDown(imageList, value);
        editorService.DropDownControl(dropDown);
        return dropDown.SelectedValue ?? value;
    }

    /// <inheritdoc />
    public override bool GetPaintValueSupported(ITypeDescriptorContext? context) =>
        ImageEditor.GetPaintValueSupported(context);

    /// <inheritdoc />
    public override void PaintValue(PaintValueEventArgs e)
    {
        if (e.Context is null)
        {
            return;
        }

        Image? image = null;
        if (e.Value is int index)
        {
            image = GetImage(e.Context, index, null, true);
        }
        else if (e.Value is string key)
        {
            image = GetImage(e.Context, -1, key, false);
        }

        if (image is not null)
        {
            ImageEditor.PaintValue(new PaintValueEventArgs(e.Context, image, e.Graphics, e.Bounds));
        }
    }
    #endregion

    #region Implementation
    private UITypeEditor ImageEditor =>
        _imageEditor ??= TypeDescriptor.GetEditor(typeof(Image), typeof(UITypeEditor)) as UITypeEditor ?? new UITypeEditor();

    private ImageList? GetImageList(ITypeDescriptorContext context)
    {
        var instance = context.Instance;
        if (instance is null || context.PropertyDescriptor is null)
        {
            return null;
        }

        var owner = instance;
        var property = GetImageListProperty(context.PropertyDescriptor, ref owner);
        return property?.GetValue(owner) as ImageList;
    }

    private Image? GetImage(ITypeDescriptorContext context, int index, string? key, bool useIntIndex)
    {
        var instance = context.Instance;
        if (instance is object[] || (index < 0 && key is null))
        {
            return null;
        }

        if (_currentImageList is null || !ReferenceEquals(instance, _currentInstance))
        {
            _currentInstance = instance;
            _currentImageList = GetImageList(context);
        }

        if (_currentImageList is null)
        {
            return null;
        }

        if (useIntIndex)
        {
            return index >= 0 && index < _currentImageList.Images.Count
                ? _currentImageList.Images[index]
                : null;
        }

        return _currentImageList.Images[key!];
    }

    private static PropertyDescriptor? GetImageListProperty(PropertyDescriptor currentComponent, ref object? instance)
    {
        if (instance is object[]
            || currentComponent.Attributes[typeof(RelatedImageListAttribute)] is not RelatedImageListAttribute imageListAttribute
            || imageListAttribute.RelatedImageList is null)
        {
            return null;
        }

        var parentInstance = instance;
        var pathInfo = imageListAttribute.RelatedImageList.Split('.');
        for (var i = 0; i < pathInfo.Length; i++)
        {
            if (parentInstance is null)
            {
                break;
            }

            var property = TypeDescriptor.GetProperties(parentInstance)[pathInfo[i]];
            if (property is null)
            {
                break;
            }

            if (i == pathInfo.Length - 1)
            {
                if (typeof(ImageList).IsAssignableFrom(property.PropertyType))
                {
                    instance = parentInstance;
                    return property;
                }
            }
            else
            {
                parentInstance = property.GetValue(parentInstance);
            }
        }

        return null;
    }
    #endregion
}

/// <summary>
/// Drop-down list for selecting an image index or key from an <see cref="ImageList"/>.
/// </summary>
internal sealed class KryptonDesignerImageIndexDropDown : UserControl
{
    #region Instance Fields
    private readonly KryptonListBox _listBox;
    private readonly bool _useKeys;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerImageIndexDropDown"/> class.
    /// </summary>
    /// <param name="imageList">Image list to display.</param>
    /// <param name="currentValue">Current property value.</param>
    public KryptonDesignerImageIndexDropDown(ImageList imageList, object? currentValue)
    {
        _useKeys = currentValue is string;
        _listBox = new KryptonListBox
        {
            Dock = DockStyle.Fill
        };

        BorderStyle = BorderStyle.FixedSingle;
        Size = new Size(180, Math.Min(240, 24 + (imageList.Images.Count + 1) * 18));

        if (_useKeys)
        {
            _listBox.Items.Add(new ImageKeyItem("(none)", string.Empty));
            foreach (string key in imageList.Images.Keys)
            {
                _listBox.Items.Add(new ImageKeyItem(key, key));
            }

            var selectedKey = currentValue as string ?? string.Empty;
            for (var i = 0; i < _listBox.Items.Count; i++)
            {
                if (_listBox.Items[i] is ImageKeyItem item && item.Key == selectedKey)
                {
                    _listBox.SelectedIndex = i;
                    break;
                }
            }
        }
        else
        {
            _listBox.Items.Add(new ImageIndexItem("(none)", -1));
            for (var i = 0; i < imageList.Images.Count; i++)
            {
                _listBox.Items.Add(new ImageIndexItem($"{i}", i));
            }

            var selectedIndex = currentValue is int index ? index : -1;
            for (var i = 0; i < _listBox.Items.Count; i++)
            {
                if (_listBox.Items[i] is ImageIndexItem item && item.Index == selectedIndex)
                {
                    _listBox.SelectedIndex = i;
                    break;
                }
            }
        }

        Controls.Add(_listBox);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the selected image index or key.
    /// </summary>
    public object? SelectedValue
    {
        get
        {
            if (_listBox.SelectedItem is ImageIndexItem indexItem)
            {
                return indexItem.Index;
            }

            if (_listBox.SelectedItem is ImageKeyItem keyItem)
            {
                return string.IsNullOrEmpty(keyItem.Key) ? string.Empty : keyItem.Key;
            }

            return null;
        }
    }
    #endregion

    #region Types
    private sealed class ImageIndexItem
    {
        public ImageIndexItem(string text, int index)
        {
            Text = text;
            Index = index;
        }

        public string Text { get; }

        public int Index { get; }

        public override string ToString() => Text;
    }

    private sealed class ImageKeyItem
    {
        public ImageKeyItem(string text, string key)
        {
            Text = text;
            Key = key;
        }

        public string Text { get; }

        public string Key { get; }

        public override string ToString() => Text;
    }
    #endregion
}
