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
/// Push or check button on a <see cref="KryptonMiniToolbar"/>.
/// </summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[DesignerCategory(@"code")]
[DefaultProperty(nameof(Text))]
[DefaultEvent(nameof(Click))]
public class KryptonMiniToolbarButton : KryptonMiniToolbarItemBase
{
    #region Nested

    private sealed class KryptonCommandReferenceConverter : ReferenceConverter
    {
        public KryptonCommandReferenceConverter()
            : base(typeof(KryptonCommand))
        {
        }
    }

    #endregion

    #region Instance Fields

    private bool _checked;
    private bool _checkOnClick;
    private KryptonMiniToolbarButtonType _buttonType;
    private KryptonCommand? _command;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the <see cref="Checked"/> property changes.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the Checked property changes.")]
    public event EventHandler? CheckedChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonMiniToolbarButton"/> class.
    /// </summary>
    public KryptonMiniToolbarButton()
    {
        _buttonType = KryptonMiniToolbarButtonType.Push;
        _checkOnClick = true;
    }

    /// <inheritdoc />
    public override string ToString() => string.IsNullOrEmpty(Text) ? "(Button)" : Text;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets whether the button is a push or check button.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Push or check button behaviour.")]
    [DefaultValue(KryptonMiniToolbarButtonType.Push)]
    public KryptonMiniToolbarButtonType ButtonType
    {
        get => _buttonType;
        set
        {
            if (_buttonType != value)
            {
                _buttonType = value;
                OnPropertyChanged(nameof(ButtonType));
            }
        }
    }

    /// <summary>
    /// Gets or sets whether the button is checked. Only used when <see cref="ButtonType"/> is Check.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates whether the check button is pressed.")]
    [DefaultValue(false)]
    public bool Checked
    {
        get => _checked;
        set
        {
            if (_checked != value)
            {
                _checked = value;
                OnPropertyChanged(nameof(Checked));
                CheckedChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets or sets whether clicking a check button toggles <see cref="Checked"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether clicking a check button toggles Checked.")]
    [DefaultValue(true)]
    public bool CheckOnClick
    {
        get => _checkOnClick;
        set
        {
            if (_checkOnClick != value)
            {
                _checkOnClick = value;
                OnPropertyChanged(nameof(CheckOnClick));
            }
        }
    }

    /// <summary>
    /// Gets or sets the command bound to this button.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Command bound to this Mini Toolbar button.")]
    [DefaultValue(null)]
    [TypeConverter(typeof(KryptonCommandReferenceConverter))]
    public KryptonCommand? KryptonCommand
    {
        get => _command;
        set
        {
            if (_command != value)
            {
                _command = value;
                OnPropertyChanged(nameof(KryptonCommand));
            }
        }
    }

    #endregion
}
