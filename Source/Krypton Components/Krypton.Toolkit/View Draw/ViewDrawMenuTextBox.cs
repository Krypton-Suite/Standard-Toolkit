#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class ViewDrawMenuTextBox : ViewComposite
{
    #region Instance Fields

    private readonly IContextMenuProvider _provider;
    private readonly KryptonTextBox _textBox;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the ViewDrawMenuTextBox class.
    /// </summary>
    /// <param name="provider">Reference to provider.</param>
    /// <param name="textBoxItem">Reference to owning text box entry.</param>
    public ViewDrawMenuTextBox(IContextMenuProvider provider,
        KryptonContextMenuTextBox textBoxItem)
    {
        _provider = provider;
        KryptonContextMenuTextBox = textBoxItem;

        _textBox = new KryptonTextBox
        {
            Text = textBoxItem.Text,
            Enabled = provider.ProviderEnabled && textBoxItem.Enabled,
            MinimumSize = new Size(textBoxItem.MinimumWidth, 0)
        };

        _textBox.TextChanged += OnTextBoxTextChanged;
        textBoxItem.PropertyChanged += OnPropertyChanged;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() => $"ViewDrawMenuTextBox:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _textBox.TextChanged -= OnTextBoxTextChanged;
            KryptonContextMenuTextBox.PropertyChanged -= OnPropertyChanged;

            _textBox.Parent?.Controls.Remove(_textBox);

            _textBox.Dispose();
        }

        base.Dispose(disposing);
    }

    #endregion

    #region KryptonContextMenuTextBox

    /// <summary>
    /// Gets access to the owning text box data model.
    /// </summary>
    public KryptonContextMenuTextBox KryptonContextMenuTextBox { get; }

    #endregion

    #region TextBox

    /// <summary>
    /// Gets access to the hosted KryptonTextBox instance.
    /// </summary>
    public KryptonTextBox TextBox => _textBox;

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

        ItemEnabled = _provider.ProviderEnabled && KryptonContextMenuTextBox.Enabled;
        _textBox.Enabled = ItemEnabled;

        // Height from the text box preferred size; width from MinimumWidth setting plus padding
        var textBoxPreferred = _textBox.GetPreferredSize(Size.Empty);
        var preferredWidth = Math.Max(KryptonContextMenuTextBox.MinimumWidth, textBoxPreferred.Width) + 6;
        var preferredHeight = textBoxPreferred.Height + 4;

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

        // Ensure the text box lives inside the menu popup control
        if (context.Control != null && _textBox.Parent != context.Control)
        {
            context.Control.Controls.Add(_textBox);
        }

        // Position and show the text box within the allocated client area
        _textBox.SetBounds(
            ClientRectangle.X + 3,
            ClientRectangle.Y + 2,
            ClientRectangle.Width - 6,
            ClientRectangle.Height - 4);

        _textBox.Visible = Visible;

        base.Layout(context);
    }

    #endregion

    #region Private

    private void OnTextBoxTextChanged(object? sender, EventArgs e) => KryptonContextMenuTextBox.Text = _textBox.Text;

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(KryptonContextMenuTextBox.Text):
                if (_textBox.Text != KryptonContextMenuTextBox.Text)
                {
                    _textBox.Text = KryptonContextMenuTextBox.Text;
                }
                break;
            case nameof(KryptonContextMenuTextBox.Enabled):
                _textBox.Enabled = _provider.ProviderEnabled && KryptonContextMenuTextBox.Enabled;
                break;
            case nameof(KryptonContextMenuTextBox.MinimumWidth):
                _textBox.MinimumSize = new Size(KryptonContextMenuTextBox.MinimumWidth, 0);
                _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(true));
                break;
        }
    }

    #endregion
}