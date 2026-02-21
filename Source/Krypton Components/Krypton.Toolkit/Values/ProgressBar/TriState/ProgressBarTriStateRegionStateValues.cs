#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Holds Back and Content properties for one state of a progress bar threshold region (Common, Normal, or Disabled).
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ProgressBarTriStateRegionStateValues : Storage
{
    #region Instance Fields

    private readonly ProgressBarTriStateRegionAppearanceValues _parent;
    private readonly ProgressBarTriStateRegionBackValues _back;
    private readonly ProgressBarTriStateRegionContentValues _content;
    private readonly Color _defaultBackColor1;
    private Color _originalContentColor1;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the ProgressBarThresholdRegionState class.
    /// </summary>
    /// <param name="parent">Reference to owning region appearance.</param>
    /// <param name="defaultBackColor1">Default background Color1 for this state.</param>
    public ProgressBarTriStateRegionStateValues(ProgressBarTriStateRegionAppearanceValues parent, Color defaultBackColor1)
    {
        _parent = parent;
        _defaultBackColor1 = defaultBackColor1;
        _back = new ProgressBarTriStateRegionBackValues(this, defaultBackColor1);
        _content = new ProgressBarTriStateRegionContentValues(this);
        _originalContentColor1 = Color.Empty;
    }

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    public override bool IsDefault => _back.Color1 == _defaultBackColor1 &&
                             _back.IsDefault &&
                             _content.IsDefault;

    #endregion

    #region Public

    /// <summary>
    /// Gets access to the background properties.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Background properties for this state.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ProgressBarTriStateRegionBackValues Back => _back;

    private bool ShouldSerializeBack() => !_back.IsDefault;

    /// <summary>
    /// Gets access to the content (text) properties.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Content (text) properties for this state.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ProgressBarTriStateRegionContentValues Content => _content;

    private bool ShouldSerializeContent() => !_content.IsDefault;

    /// <summary>
    /// Resets all values to their default.
    /// </summary>
    public void Reset()
    {
        // Reset Back (this will set Color1 to Empty, then we restore it to default)
        _back.Reset();
        _back.Color1 = _defaultBackColor1;
        _content.Reset();
        _originalContentColor1 = Color.Empty;
    }

    #endregion

    #region Internal

    /// <summary>
    /// Called when a back property changes. Notifies the parent.
    /// </summary>
    internal void OnBackChanged()
    {
        // If UseOppositeTextColors is enabled and we're in StateCommon, update Content.Color1
        if (_parent.Parent.UseOppositeTextColors && ReferenceEquals(_parent.StateCommon, this) && _back.Color1 != Color.Empty)
        {
            _content.Color1 = GetOppositeColor(_back.Color1);
        }

        _parent.OnStateChanged(this);
    }

    /// <summary>
    /// Called when a content property changes. Notifies the parent.
    /// </summary>
    internal void OnContentChanged()
    {
        _parent.OnStateChanged(this);
    }

    /// <summary>
    /// Called when UseOppositeTextColors is enabled: stores current Content.Color1 and sets it to opposite of Back.Color1.
    /// </summary>
    internal void EnableOppositeTextColors()
    {
        if (ReferenceEquals(_parent.StateCommon, this) && _back.Color1 != Color.Empty)
        {
            _originalContentColor1 = _content.Color1;
            _content.Color1 = GetOppositeColor(_back.Color1);
        }
    }

    /// <summary>
    /// Called when UseOppositeTextColors is disabled: restores the stored Content.Color1.
    /// </summary>
    internal void RestoreOriginalTextColor()
    {
        if (ReferenceEquals(_parent.StateCommon, this))
        {
            _content.Color1 = _originalContentColor1;
            _originalContentColor1 = Color.Empty;
        }
    }

    /// <summary>
    /// Copies all properties from another state.
    /// </summary>
    /// <param name="source">The state to copy from.</param>
    internal void AssignFrom(ProgressBarTriStateRegionStateValues source)
    {
        _back.Color1 = source._back.Color1;
        _back.Color2 = source._back.Color2;
        _back.ColorStyle = source._back.ColorStyle;
        _back.ColorAlign = source._back.ColorAlign;
        _back.ColorAngle = source._back.ColorAngle;
        _back.Image = source._back.Image;
        _back.ImageStyle = source._back.ImageStyle;
        _back.ImageAlign = source._back.ImageAlign;
        _content.Color1 = source._content.Color1;
        _content.Color2 = source._content.Color2;
        _content.ColorStyle = source._content.ColorStyle;
        _content.ColorAlign = source._content.ColorAlign;
        _content.ColorAngle = source._content.ColorAngle;
    }

    #endregion

    #region Helpers

    private static Color GetOppositeColor(Color color) =>
        Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);

    #endregion

    #region Public Overrides

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticValues.DEFAULT_EMPTY_STRING;

    #endregion
}
