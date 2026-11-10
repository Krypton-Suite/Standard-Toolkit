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
/// Split / drop-down button on a <see cref="KryptonMiniToolbar"/>. The drop-down is a <see cref="KryptonContextMenu"/>.
/// </summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[DesignerCategory(@"code")]
[DefaultProperty(nameof(Text))]
[DefaultEvent(nameof(Click))]
public class KryptonMiniToolbarSplitButton : KryptonMiniToolbarItemBase
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

    private KryptonCommand? _command;
    private KryptonContextMenu? _kryptonContextMenu;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonMiniToolbarSplitButton"/> class.
    /// </summary>
    public KryptonMiniToolbarSplitButton()
    {
    }

    /// <inheritdoc />
    public override string ToString() => string.IsNullOrEmpty(Text) ? "(SplitButton)" : Text;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the command executed when the button body is clicked.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Command executed when the button body is clicked.")]
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

    /// <summary>
    /// Gets or sets the context menu shown from the drop-down portion.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Context menu shown from the drop-down portion.")]
    [DefaultValue(null)]
    public KryptonContextMenu? KryptonContextMenu
    {
        get => _kryptonContextMenu;
        set
        {
            if (_kryptonContextMenu != value)
            {
                _kryptonContextMenu = value;
                OnPropertyChanged(nameof(KryptonContextMenu));
            }
        }
    }

    #endregion
}
