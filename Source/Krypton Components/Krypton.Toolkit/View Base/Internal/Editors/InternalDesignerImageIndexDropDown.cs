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
/// Drop-down list for selecting an image index or key from an <see cref="ImageList"/>.
/// </summary>
internal partial class InternalDesignerImageIndexDropDown : UserControl
{
    #region Instance Fields
    private bool _useKeys;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="InternalDesignerImageIndexDropDown"/> class for the WinForms designer.
    /// </summary>
    public InternalDesignerImageIndexDropDown()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="InternalDesignerImageIndexDropDown"/> class.
    /// </summary>
    /// <param name="imageList">Image list to display.</param>
    /// <param name="currentValue">Current property value.</param>
    public InternalDesignerImageIndexDropDown(ImageList imageList, object? currentValue)
    {
        _useKeys = currentValue is string;

        InitializeComponent();

        Size = new Size(180, Math.Min(240, 24 + (imageList.Images.Count + 1) * 18));

        if (_useKeys)
        {
            _listBox.Items.Add(new ImageKeyItem("(none)", string.Empty));
            foreach (string? keyValue in imageList.Images.Keys)
            {
                var key = keyValue ?? string.Empty;
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
