#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class ViewDrawMenuProgressBar : ViewComposite
{
    #region Instance Fields
    private readonly IContextMenuProvider _provider;
    private readonly KryptonProgressBar _progressBar;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawMenuProgressBar class.
    /// </summary>
    /// <param name="provider">Reference to provider.</param>
    /// <param name="progressBarItem">Reference to owning progress bar entry.</param>
    public ViewDrawMenuProgressBar(IContextMenuProvider provider,
        KryptonContextMenuProgressBar progressBarItem)
    {
        _provider = provider;
        KryptonContextMenuProgressBar = progressBarItem;

        _progressBar = progressBarItem.ProgressBar;
        _progressBar.Enabled = provider.ProviderEnabled && progressBarItem.Enabled;
        _progressBar.MinimumSize = new Size(progressBarItem.MinimumWidth, 0);

        progressBarItem.PropertyChanged += OnPropertyChanged;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() => $"ViewDrawMenuProgressBar:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            KryptonContextMenuProgressBar.PropertyChanged -= OnPropertyChanged;

            _progressBar.Parent?.Controls.Remove(_progressBar);
        }

        base.Dispose(disposing);
    }

    #endregion

    #region KryptonContextMenuProgressBar
    /// <summary>
    /// Gets access to the owning progress bar data model.
    /// </summary>
    public KryptonContextMenuProgressBar KryptonContextMenuProgressBar { get; }

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

        ItemEnabled = _provider.ProviderEnabled && KryptonContextMenuProgressBar.Enabled;
        _progressBar.Enabled = ItemEnabled;

        var preferred = _progressBar.GetPreferredSize(Size.Empty);
        var preferredWidth = Math.Max(KryptonContextMenuProgressBar.MinimumWidth, preferred.Width) + 6;
        var preferredHeight = preferred.Height + 4;

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

        if (context.Control != null && _progressBar.Parent != context.Control)
        {
            context.Control.Controls.Add(_progressBar);
        }

        _progressBar.SetBounds(
            ClientRectangle.X + 3,
            ClientRectangle.Y + 2,
            ClientRectangle.Width - 6,
            ClientRectangle.Height - 4);

        _progressBar.Visible = Visible;

        base.Layout(context);
    }
    #endregion

    #region Private
    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(KryptonContextMenuProgressBar.Enabled):
                _progressBar.Enabled = _provider.ProviderEnabled && KryptonContextMenuProgressBar.Enabled;
                break;
            case nameof(KryptonContextMenuProgressBar.MinimumWidth):
                _progressBar.MinimumSize = new Size(KryptonContextMenuProgressBar.MinimumWidth, 0);
                _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(true));
                break;
        }
    }
    #endregion
}
