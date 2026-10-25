#region Licences

/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */

//--------------------------------------------------------------------------------
// Copyright (C) 2013-2021 JDH Software - <support@jdhsoftware.com>
//
// This program is provided to you under the terms of the Microsoft Public
// License (Ms-PL) as published at https://github.com/Cocotteseb/Krypton-OutlookGrid/blob/master/LICENSE.md
//
// Visit https://www.jdhsoftware.com and follow @jdhsoftware on Twitter
//
//--------------------------------------------------------------------------------

#endregion
namespace Krypton.Toolkit;

/// <summary>
/// Hosts a collection of KryptonDataGridViewProgressColumn cells.<br/>
/// The progress features only function within a KryptonDataGridView.<br/>
/// The connected field must have a value from 0.0 to 1 or DBNull. 
/// </summary>
[ToolboxBitmap(typeof(KryptonDataGridViewProgressColumn), "ToolboxBitmaps.KryptonProgressBar.bmp")]
public class KryptonDataGridViewProgressColumn : KryptonDataGridViewIconColumn
{
    #region Fields
    private ProgressBarSettings _progressBarSettings;
    private DataGridView? _dataGridView;
    private PaletteBase _palette;
    #endregion

    #region Classes
    /// <summary>
    /// Helper class for color properties
    /// </summary>
    public class ProgressColors
    {
        #region Fields
        private Color _color1;
        private Color _color2;
        #endregion

        #region Events            
        public event Action InvalidateColumn;
        #endregion

        #region Identity
        public ProgressColors()
        {
            _color1 = GlobalStaticValues.EMPTY_COLOR;
            _color2 = GlobalStaticValues.EMPTY_COLOR;
        }
        #endregion

        #region Private
        private void OnInvalidateColumn()
        {
            InvalidateColumn?.Invoke();
        }
        #endregion

        #region Public
        [Description("Color 1. Use as the first color for the gradient or as the custom solid color.")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color Color1 
        {
            get => _color1;

            set
            {
                if (_color1 != value)
                {
                    _color1 = value;
                    OnInvalidateColumn();
                }
            }

        }
        private bool ShouldSerializeColor1() => _color1 != GlobalStaticValues.EMPTY_COLOR;
        private void ResetColor1() => _color1 = GlobalStaticValues.EMPTY_COLOR;


        [Description("Color 2. Second color for the gradient.")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color Color2 {
            get => _color2;

            set
            {
                if (_color2 != value)
                {
                    _color2 = value;
                    OnInvalidateColumn();
                }
            }

        }
        private bool ShouldSerializeColor2() => _color2 != GlobalStaticValues.EMPTY_COLOR;
        private void ResetColor2() => _color2 = GlobalStaticValues.EMPTY_COLOR;
        #endregion

        #region Public Overrides
        public override string ToString()
        {
            return string.Empty;
        }
        #endregion
    }


    /// <summary>
    /// Progress column custom color controls
    /// </summary>
    public class ProgressBarSettings
    {
        #region Fields
        private bool _showProgressBarBorder;
        private bool _showProgressBar;
        private bool _useSolidColor;

        private Color _borderColor;
        private Color _textColor;

        private LinearGradientMode _linearGradientMode;

        private ProgressColors _progressCompleted;
        private ProgressColors _progressRemaining;

        #region Events
        public event Action InvalidateColumn;
        #endregion

        #endregion

        #region Identity
        public ProgressBarSettings()
        {
            _showProgressBar = true;
            _showProgressBarBorder = true;

            _borderColor = GlobalStaticValues.EMPTY_COLOR;
            _textColor = GlobalStaticValues.EMPTY_COLOR;

            _linearGradientMode = LinearGradientMode.Vertical;

            _progressCompleted = new ProgressColors();
            _progressRemaining = new ProgressColors();
        }
        #endregion

        #region Private 
        private void OnInvalidateColumn()
        {
            InvalidateColumn?.Invoke();
        }
        #endregion

        #region Public Overrides
        public override string ToString()
        {
            return string.Empty;
        }
        #endregion
            
        #region Public
        [Category("Appearance")]
        [Description("Control the colours for the completed part of the progressbar.")]
        [Browsable(true)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ProgressColors ProgressCompleted => _progressCompleted;


        [Category("Appearance")]
        [Description("Control the colours for the remaining part of the progressbar.")]
        [Browsable(true)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ProgressColors ProgressRemaining => _progressRemaining;


        [Category("Appearance")]
        [Description("Draw a border around the progress bar.")]
        [Browsable(true)]
        [DefaultValue(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ShowBorder {
            get => _showProgressBarBorder;

            set
            {
                if (_showProgressBarBorder != value)
                {
                    _showProgressBarBorder = value;
                    OnInvalidateColumn();
                }
            }
        }


        [Category("Appearance")]
        [Description("Draw the progress bar.")]
        [DefaultValue(true)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ShowBar {
            get => _showProgressBar;

            set
            {
                if (_showProgressBar != value)
                {
                    _showProgressBar = value;
                    OnInvalidateColumn();
                }
            }
        }


        [Category("Appearance")]
        [Description("Color for the progressbar border.")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor 
        {
            get => _borderColor;

            set
            {
                if (_borderColor != value)
                {
                    _borderColor = value;
                    OnInvalidateColumn();
                }
            }
        }
        private bool ShouldSerializeBorderColor() => _borderColor != GlobalStaticValues.EMPTY_COLOR;
        private void ResetBorderColor() => _borderColor = GlobalStaticValues.EMPTY_COLOR;


        [Category("Appearance")]
        [Description("Text color for the progress text.")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color TextColor {
            get => _textColor;

            set
            {
                if (_textColor != value)
                {
                    _textColor = value;
                    OnInvalidateColumn();
                }
            }

        }
        private bool ShouldSerializeTextColor() => _textColor != GlobalStaticValues.EMPTY_COLOR;
        private void ResetTextColor() => _textColor = GlobalStaticValues.EMPTY_COLOR;


        [Category("Appearance")]
        [Description("Direction of how the color gradient is drawn.")]
        [Browsable(true)]
        [DefaultValue(LinearGradientMode.Vertical)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public LinearGradientMode LinearGradientMode 
        { 
            get => _linearGradientMode; 
                
            set
            {
                if ( _linearGradientMode != value)
                {
                    _linearGradientMode = value;
                    OnInvalidateColumn();
                }
            }
        }


        [Description("Use the theme color or Color1 if set as a solid color.")]
        [Browsable(true)]
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool UseSolidColor 
        {
            get => _useSolidColor;

            set
            {
                if (_useSolidColor != value)
                {
                    _useSolidColor = value;
                    OnInvalidateColumn();
                }
            }
        }
        #endregion
    }
    #endregion Classes

    #region Identity
    /// <summary>
    ///Default constructor.
    /// </summary>
    public KryptonDataGridViewProgressColumn()
        : base(new KryptonDataGridViewProgressCell())
    {
        _dataGridView = null;
        _progressBarSettings = new();
        _palette = KryptonManager.CurrentGlobalPalette;
        KryptonManager.GlobalPaletteChanged += OnPaletteChanged;
    }
    #endregion

    #region Public
    [Category("Appearance")]
    [Description("Progressbar configuration.")]
    [Browsable(true)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ProgressBarSettings ProgressBar => _progressBarSettings;
    #endregion

    #region Public Overrides
    /// <inheritdoc/>
    public override object Clone()
    {
        var cloned = base.Clone() as KryptonDataGridViewProgressColumn ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("cloned"));

        cloned._progressBarSettings = _progressBarSettings;
        cloned._dataGridView = _dataGridView;
        cloned._palette = _palette;
            
        cloned.CompletedColor1 = CompletedColor1;
        cloned.CompletedColor2 = CompletedColor2;
        cloned.RemainingColor1 = RemainingColor1;
        cloned.RemainingColor2 = RemainingColor2;
        cloned.BorderColor = BorderColor;

        return cloned;
    }

    /// <inheritdoc/>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override DataGridViewCell? CellTemplate 
    {
        get => base.CellTemplate;

        set
        {
            // Ensure that the cell used for the template is a DataGridViewProgressCell.
            if (value != null && !value.GetType().IsAssignableFrom(typeof(KryptonDataGridViewProgressCell)))
            {
                throw new InvalidCastException("Must be a KryptonDataGridViewProgressCell");
            }

            base.CellTemplate = value;
        }
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"KryptonDataGridViewProgressColumn {{ Name={Name} Index={Index.ToString(CultureInfo.CurrentCulture)} }}";
    }
    #endregion

    #region Internal
    /// <summary>
    /// Completed progress Color 1
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal Color CompletedColor1 { get; set; }


    /// <summary>
    /// Completed progress Color 2
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal Color CompletedColor2 { get; set; }


    /// <summary>
    /// Remaining progress Color 1
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal Color RemainingColor1 { get; set; }


    /// <summary>
    /// Remaining progress Color 2
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal Color RemainingColor2 { get; set; }


    /// <summary>
    /// Progress border color
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal Color BorderColor { get; set; }


    /// <summary>
    /// The current active palette. This can be the KryptonManager global palette, the local or custom KryptonDataGridView palette
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal PaletteBase Palette => _palette;
    #endregion

    #region Protected Overrides
    protected override void OnDataGridViewChanged()
    {
        if (_dataGridView is not null)
        {
            ProgressBar.InvalidateColumn -= OnInvalidateColumn;
            ProgressBar.ProgressCompleted.InvalidateColumn -= OnInvalidateColumn;
            ProgressBar.ProgressRemaining.InvalidateColumn -= OnInvalidateColumn;

            if (_dataGridView is KryptonDataGridView dataGridView)
            {
                dataGridView.PaletteChanged -= OnPaletteChanged;
            }
        }

        _dataGridView = DataGridView;

        if (_dataGridView is not null)
        {
            ProgressBar.InvalidateColumn += OnInvalidateColumn;
            ProgressBar.ProgressCompleted.InvalidateColumn += OnInvalidateColumn;
            ProgressBar.ProgressRemaining.InvalidateColumn += OnInvalidateColumn;

            if (_dataGridView is KryptonDataGridView dataGridView)
            {
                dataGridView.PaletteChanged += OnPaletteChanged;
            }
        }

        // Update palette state since the grid changed
        OnPaletteChanged(null, null!);

        base.OnDataGridViewChanged();
    }
    #endregion

    #region Private
    /// <summary>
    /// Check which palette is active / to be used.
    /// </summary>
    /// <param name="sender">Not used.</param>
    /// <param name="e">Not used.</param>
    private void OnPaletteChanged(object? sender, EventArgs e)
    {
        if (_dataGridView is KryptonDataGridView dataGridView)
        {
            if (dataGridView.Palette is not null && dataGridView.PaletteMode == PaletteMode.Custom)
            {
                _palette = dataGridView.Palette;
            }
            else if (dataGridView.PaletteMode != PaletteMode.Global && dataGridView.PaletteMode != PaletteMode.Custom)
            {
                _palette = KryptonManager.GetPaletteForMode(dataGridView.PaletteMode);
            }
            else
            {
                _palette = KryptonManager.CurrentGlobalPalette;
            }
        }
        else
        {
            _palette = KryptonManager.CurrentGlobalPalette;
        }

        OnInvalidateColumn();
    }

    /// <summary>
    /// Repaint the column after settings changed.
    /// </summary>
    private void OnInvalidateColumn()
    {
        //check colours
        CompletedColor1 = ProgressBar.ProgressCompleted.Color1 == GlobalStaticValues.EMPTY_COLOR
            ? Palette.GetBackColor1(PaletteBackStyle.GridHeaderColumnList, PaletteState.Normal)
            : ProgressBar.ProgressCompleted.Color1;

        CompletedColor2 = ProgressBar.ProgressCompleted.Color2 == GlobalStaticValues.EMPTY_COLOR
            ? Palette.GetBackColor2(PaletteBackStyle.GridHeaderColumnList, PaletteState.Normal)
            : ProgressBar.ProgressCompleted.Color2;

        RemainingColor1 = ProgressBar.ProgressRemaining.Color1 == GlobalStaticValues.EMPTY_COLOR
            ? Palette.GetBackColor2(PaletteBackStyle.GridHeaderColumnList, PaletteState.Normal)
            : ProgressBar.ProgressRemaining.Color1;

        RemainingColor2 = ProgressBar.ProgressRemaining.Color2 == GlobalStaticValues.EMPTY_COLOR
            ? Palette.GetBackColor1(PaletteBackStyle.GridHeaderColumnList, PaletteState.BoldedOverride)
            : ProgressBar.ProgressRemaining.Color2;

        BorderColor = ProgressBar.BorderColor == GlobalStaticValues.EMPTY_COLOR
            ? Palette.GetBorderColor1(PaletteBorderStyle.GridHeaderColumnList, PaletteState.Normal)
            : ProgressBar.BorderColor;

        DataGridView?.InvalidateColumn(this.Index);
    }
    #endregion
}