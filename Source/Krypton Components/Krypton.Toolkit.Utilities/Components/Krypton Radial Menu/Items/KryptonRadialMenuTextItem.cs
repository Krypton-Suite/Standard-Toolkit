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
/// Radial menu item that edits a string value in an editor ring when activated.
/// </summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[DesignerCategory(@"code")]
[DefaultProperty(nameof(Text))]
[DefaultEvent(nameof(TextChanged))]
public class KryptonRadialMenuTextItem : KryptonRadialMenuItemBase
{
    #region Instance Fields

    private string _text;
    private string _label;
    private string _draftText;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when <see cref="Text"/> changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the Text property changes.")]
    public event EventHandler? TextChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRadialMenuTextItem"/> class.
    /// </summary>
    public KryptonRadialMenuTextItem()
    {
        _label = @"Text";
        _text = string.Empty;
        _draftText = string.Empty;
    }

    /// <inheritdoc />
    public override string ToString() => (string.IsNullOrEmpty(Label) ? "(Radial Text)" : Label)!;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the sector label.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Label displayed on the text item sector.")]
    [DefaultValue(@"Text")]
    [Localizable(true)]
    public string? Label
    {
        get => _label;
        set
        {
            value ??= string.Empty;
            if (_label != value)
            {
                _label = value;
                OnPropertyChanged(nameof(Label));
            }
        }
    }

    /// <summary>
    /// Gets or sets the editable text value.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Editable text value.")]
    [DefaultValue("")]
    [Localizable(true)]
    public string? Text
    {
        get => _text;
        set
        {
            value ??= string.Empty;
            if (_text != value)
            {
                _text = value;
                _draftText = value;
                OnPropertyChanged(nameof(Text));
                TextChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets or sets the draft text while the editor ring is open.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? DraftText
    {
        get => _draftText;
        set => _draftText = value ?? string.Empty;
    }

    /// <inheritdoc />
    [Browsable(false)]
    public override bool HasChildren => true;

    /// <summary>
    /// Begins an edit session by copying <see cref="Text"/> into <see cref="DraftText"/>.
    /// </summary>
    public void BeginEdit() => _draftText = _text;

    /// <summary>
    /// Commits <see cref="DraftText"/> into <see cref="Text"/>.
    /// </summary>
    public void CommitEdit() => Text = _draftText;

    /// <summary>
    /// Cancels the draft and restores <see cref="DraftText"/> from <see cref="Text"/>.
    /// </summary>
    public void CancelEdit() => _draftText = _text;

    #endregion
}
