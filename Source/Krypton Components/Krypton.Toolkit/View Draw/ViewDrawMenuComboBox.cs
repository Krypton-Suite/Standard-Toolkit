#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class ViewDrawMenuComboBox : ViewComposite
{
    #region Instance Fields
    
    private readonly IContextMenuProvider _provider;
    private readonly KryptonComboBox _comboBox;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the ViewDrawMenuComboBox class.
    /// </summary>
    /// <param name="provider">Reference to provider.</param>
    /// <param name="comboBoxItem">Reference to owning combo box entry.</param>
    public ViewDrawMenuComboBox(IContextMenuProvider provider,
        KryptonContextMenuComboBox comboBoxItem)
    {
        _provider = provider;
        KryptonContextMenuComboBox = comboBoxItem;

        _comboBox = new KryptonComboBox
        {
            Text = comboBoxItem.Text,
            Enabled = provider.ProviderEnabled && comboBoxItem.Enabled,
            MinimumSize = new Size(comboBoxItem.MinimumWidth, 0)
        };

        foreach (var item in comboBoxItem.Items)
        {
            _comboBox.Items.Add(item);
        }

        if (comboBoxItem.SelectedIndex >= 0 && comboBoxItem.SelectedIndex < _comboBox.Items.Count)
        {
            _comboBox.SelectedIndex = comboBoxItem.SelectedIndex;
        }

        _comboBox.SelectedIndexChanged += OnComboBoxSelectedIndexChanged;
        _comboBox.TextChanged += OnComboBoxTextChanged;
        comboBoxItem.PropertyChanged += OnPropertyChanged;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() => $"ViewDrawMenuComboBox:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _comboBox.SelectedIndexChanged -= OnComboBoxSelectedIndexChanged;
            _comboBox.TextChanged -= OnComboBoxTextChanged;
            KryptonContextMenuComboBox.PropertyChanged -= OnPropertyChanged;

            _comboBox.Parent?.Controls.Remove(_comboBox);

            _comboBox.Dispose();
        }

        base.Dispose(disposing);
    }

    #endregion

    #region KryptonContextMenuComboBox
    
    /// <summary>
    /// Gets access to the owning combo box data model.
    /// </summary>
    public KryptonContextMenuComboBox KryptonContextMenuComboBox { get; }

    #endregion

    #region ComboBox
    
    /// <summary>
    /// Gets access to the hosted KryptonComboBox instance.
    /// </summary>
    public KryptonComboBox ComboBox => _comboBox;

    #endregion

    #region ItemEnabled
    
    /// <summary>
    /// Gets the enabled state of the item.
    /// </summary>
    public bool ItemEnabled { get; private set; }

    #endregion

    #region Layout
    
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        ItemEnabled = _provider.ProviderEnabled && KryptonContextMenuComboBox.Enabled;
        _comboBox.Enabled = ItemEnabled;

        var comboPreferred = _comboBox.GetPreferredSize(Size.Empty);
        var preferredWidth = Math.Max(KryptonContextMenuComboBox.MinimumWidth, comboPreferred.Width) + 6;
        var preferredHeight = comboPreferred.Height + 4;

        return new Size(preferredWidth, preferredHeight);
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        ClientRectangle = context.DisplayRectangle;

        if (context.Control != null && _comboBox.Parent != context.Control)
        {
            context.Control.Controls.Add(_comboBox);
        }

        _comboBox.SetBounds(
            ClientRectangle.X + 3,
            ClientRectangle.Y + 2,
            ClientRectangle.Width - 6,
            ClientRectangle.Height - 4);

        _comboBox.Visible = Visible;

        base.Layout(context);
    }
    
    #endregion

    #region Private
    
    private void OnComboBoxSelectedIndexChanged(object? sender, EventArgs e)
    {
        KryptonContextMenuComboBox.SelectedIndex = _comboBox.SelectedIndex;
    }

    private void OnComboBoxTextChanged(object? sender, EventArgs e)
    {
        KryptonContextMenuComboBox.Text = _comboBox.Text;
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(KryptonContextMenuComboBox.Text):
                if (_comboBox.Text != KryptonContextMenuComboBox.Text)
                {
                    _comboBox.Text = KryptonContextMenuComboBox.Text;
                }
                break;
            case nameof(KryptonContextMenuComboBox.Enabled):
                _comboBox.Enabled = _provider.ProviderEnabled && KryptonContextMenuComboBox.Enabled;
                break;
            case nameof(KryptonContextMenuComboBox.MinimumWidth):
                _comboBox.MinimumSize = new Size(KryptonContextMenuComboBox.MinimumWidth, 0);
                _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(true));
                break;
            case nameof(KryptonContextMenuComboBox.SelectedIndex):
                if (_comboBox.SelectedIndex != KryptonContextMenuComboBox.SelectedIndex)
                {
                    _comboBox.SelectedIndex = KryptonContextMenuComboBox.SelectedIndex;
                }
                break;
            case nameof(KryptonContextMenuComboBox.Items):
                _comboBox.Items.Clear();
                foreach (var item in KryptonContextMenuComboBox.Items)
                {
                    _comboBox.Items.Add(item);
                }
                _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(true));
                break;
        }
    }

    #endregion
}
