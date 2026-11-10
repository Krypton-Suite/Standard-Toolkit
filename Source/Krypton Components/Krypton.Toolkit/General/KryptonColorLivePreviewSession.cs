#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac et al. 2026- 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// State machine for an opt-in colour live preview.
/// </summary>
/// <remarks>
/// <para>
/// Call <see cref="Open"/> when a colour drop-down is shown, <see cref="Track"/> while a swatch is highlighted,
/// <see cref="Commit"/> when the user chooses a colour, and <see cref="Cancel"/> when the drop-down closes without a choice.
/// </para>
/// <para>
/// Methods do nothing while <see cref="Enabled"/> is false. <see cref="Track"/> treats <see cref="Color.Empty"/> as leaving
/// the swatches, because that is what <c>TrackingColor</c> reports. No Color is committed with <see cref="Commit"/> and is not a hover preview.
/// A <see cref="Cancel"/> after <see cref="Commit"/> is ignored.
/// </para>
/// </remarks>
public sealed class KryptonColorLivePreviewSession
{
    private bool _enabled;
    private bool _open;
    private bool _committed;
    private bool _previewing;
    private Color _committedColor;
    private Color _previewColor;

    /// <summary>
    /// Gets or sets a value indicating whether <see cref="Open"/>, <see cref="Track"/>, and <see cref="Commit"/> run.
    /// </summary>
    /// <remarks>
    /// Setting this to false while a drop-down is open cancels the preview and restores the committed colour.
    /// </remarks>
    public bool Enabled
    {
        get => _enabled;

        set
        {
            if (_enabled == value)
            {
                return;
            }

            _enabled = value;
            if (!value)
            {
                Cancel();
            }
        }
    }

    /// <summary>
    /// Gets a value indicating whether <see cref="Open"/> has been called and the drop-down has not yet committed or cancelled.
    /// </summary>
    public bool IsOpen => _open;

    /// <summary>
    /// Gets a value indicating whether a swatch colour is currently shown in place of the committed colour.
    /// </summary>
    public bool IsPreviewing => _previewing;

    /// <summary>
    /// Gets the colour that was committed when the drop-down opened, or the colour passed to the latest <see cref="Commit"/>.
    /// </summary>
    public Color CommittedColor => _committedColor;

    /// <summary>
    /// Gets the highlighted swatch while <see cref="IsPreviewing"/> is true; otherwise <see cref="CommittedColor"/>.
    /// </summary>
    public Color DisplayColor => _previewing ? _previewColor : _committedColor;

    /// <summary>
    /// Occurs when the preview, revert, commit, or cancel stage is raised.
    /// </summary>
    public event EventHandler<KryptonColorLivePreviewEventArgs>? PreviewChanged;

    /// <summary>
    /// Starts a preview session for a drop-down that is now visible.
    /// </summary>
    /// <param name="committedColor">Colour to restore if the user does not choose a new one.</param>
    public void Open(Color committedColor)
    {
        if (!_enabled)
        {
            return;
        }

        _open = true;
        _committed = false;
        _previewing = false;
        _committedColor = committedColor;
        _previewColor = committedColor;
    }

    /// <summary>
    /// Highlights a swatch, or restores the committed colour when <paramref name="color"/> is <see cref="Color.Empty"/>.
    /// </summary>
    /// <param name="color">Swatch under the pointer. <see cref="Color.Empty"/> means the pointer left the swatches.</param>
    public void Track(Color color)
    {
        if (!_enabled || !_open || _committed)
        {
            return;
        }

        if (color.IsEmpty)
        {
            Leave();
            return;
        }

        if (_previewing && _previewColor.ToArgb() == color.ToArgb())
        {
            return;
        }

        _previewing = true;
        _previewColor = color;
        Raise(KryptonColorLivePreviewAction.Preview, color);
    }

    /// <summary>
    /// Restores the committed colour while the drop-down stays open.
    /// </summary>
    public void Leave()
    {
        if (!_open || _committed || !_previewing)
        {
            return;
        }

        _previewing = false;
        _previewColor = _committedColor;
        Raise(KryptonColorLivePreviewAction.Revert, _committedColor);
    }

    /// <summary>
    /// Ends the session because the user chose a colour. A later <see cref="Cancel"/> does nothing.
    /// </summary>
    /// <param name="color">Colour the user chose, including <see cref="Color.Empty"/> for No Color.</param>
    public void Commit(Color color)
    {
        if (!_enabled || !_open || _committed)
        {
            return;
        }

        _committed = true;
        _previewing = false;
        _previewColor = color;
        Raise(KryptonColorLivePreviewAction.Commit, color);
        _committedColor = color;
        _open = false;
    }

    /// <summary>
    /// Ends the session without a choice and restores the committed colour.
    /// </summary>
    public void Cancel()
    {
        if (!_open || _committed)
        {
            return;
        }

        _previewing = false;
        _previewColor = _committedColor;
        _open = false;
        Raise(KryptonColorLivePreviewAction.Cancel, _committedColor);
    }

    private void Raise(KryptonColorLivePreviewAction action, Color color) =>
        PreviewChanged?.Invoke(this, new KryptonColorLivePreviewEventArgs(action, color, _committedColor));
}
