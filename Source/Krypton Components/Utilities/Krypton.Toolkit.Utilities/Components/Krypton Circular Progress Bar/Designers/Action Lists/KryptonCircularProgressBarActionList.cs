#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Smart-tag action list for <see cref="KryptonCircularProgressBar"/>.
/// </summary>
internal class KryptonCircularProgressBarActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonCircularProgressBar _control;
    private readonly IComponentChangeService? _service;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonCircularProgressBarActionList"/> class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonCircularProgressBarActionList(KryptonCircularProgressBarDesigner owner)
        : base(owner.Component)
    {
        _control = (owner.Component as KryptonCircularProgressBar)!;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Smart-Tag Properties

    /// <summary>Gets or sets the current progress value.</summary>
    public int Value
    {
        get => _control.Value;
        set
        {
            if (_control.Value != value)
            {
                _service?.OnComponentChanged(_control, null, _control.Value, value);
                _control.Value = value;
            }
        }
    }

    /// <summary>Gets or sets the minimum progress value.</summary>
    public int Minimum
    {
        get => _control.Minimum;
        set
        {
            if (_control.Minimum != value)
            {
                _service?.OnComponentChanged(_control, null, _control.Minimum, value);
                _control.Minimum = value;
            }
        }
    }

    /// <summary>Gets or sets the maximum progress value.</summary>
    public int Maximum
    {
        get => _control.Maximum;
        set
        {
            if (_control.Maximum != value)
            {
                _service?.OnComponentChanged(_control, null, _control.Maximum, value);
                _control.Maximum = value;
            }
        }
    }

    /// <summary>Gets or sets the progress bar display style.</summary>
    public ProgressBarStyle Style
    {
        get => _control.Style;
        set
        {
            if (_control.Style != value)
            {
                _service?.OnComponentChanged(_control, null, _control.Style, value);
                _control.Style = value;
            }
        }
    }

    /// <summary>Gets or sets whether the numeric value is shown as centre text.</summary>
    public bool UseValueAsText
    {
        get => _control.UseValueAsText;
        set
        {
            if (_control.UseValueAsText != value)
            {
                _service?.OnComponentChanged(_control, null, _control.UseValueAsText, value);
                _control.UseValueAsText = value;
            }
        }
    }

    /// <summary>Gets or sets the centre display text.</summary>
    public string Text
    {
        get => _control.Text;
        set
        {
            if (_control.Text != value)
            {
                _service?.OnComponentChanged(_control, null, _control.Text, value);
                _control.Text = value;
            }
        }
    }

    /// <summary>Gets or sets the superscript annotation text.</summary>
    public string SuperscriptText
    {
        get => _control.SuperscriptText;
        set
        {
            if (_control.SuperscriptText != value)
            {
                _service?.OnComponentChanged(_control, null, _control.SuperscriptText, value);
                _control.SuperscriptText = value;
            }
        }
    }

    /// <summary>Gets or sets the subscript annotation text.</summary>
    public string SubscriptText
    {
        get => _control.SubscriptText;
        set
        {
            if (_control.SubscriptText != value)
            {
                _service?.OnComponentChanged(_control, null, _control.SubscriptText, value);
                _control.SubscriptText = value;
            }
        }
    }

    /// <summary>Gets or sets the value transition duration in milliseconds.</summary>
    public int AnimationSpeed
    {
        get => _control.AnimationSpeed;
        set
        {
            if (_control.AnimationSpeed != value)
            {
                _service?.OnComponentChanged(_control, null, _control.AnimationSpeed, value);
                _control.AnimationSpeed = value;
            }
        }
    }

    /// <summary>Gets or sets the easing function used when animating value changes.</summary>
    public WinFormAnimation.KnownAnimationFunctions AnimationFunction
    {
        get => _control.AnimationFunction;
        set
        {
            if (_control.AnimationFunction != value)
            {
                _service?.OnComponentChanged(_control, null, _control.AnimationFunction, value);
                _control.AnimationFunction = value;
            }
        }
    }

    /// <summary>Gets or sets the marquee rotation period in milliseconds.</summary>
    public int MarqueeAnimationSpeed
    {
        get => _control.MarqueeAnimationSpeed;
        set
        {
            if (_control.MarqueeAnimationSpeed != value)
            {
                _service?.OnComponentChanged(_control, null, _control.MarqueeAnimationSpeed, value);
                _control.MarqueeAnimationSpeed = value;
            }
        }
    }

    /// <summary>Gets or sets the outer ring thickness.</summary>
    public int OuterWidth
    {
        get => _control.OuterWidth;
        set
        {
            if (_control.OuterWidth != value)
            {
                _service?.OnComponentChanged(_control, null, _control.OuterWidth, value);
                _control.OuterWidth = value;
            }
        }
    }

    /// <summary>Gets or sets the progress arc band thickness.</summary>
    public int ProgressWidth
    {
        get => _control.ProgressWidth;
        set
        {
            if (_control.ProgressWidth != value)
            {
                _service?.OnComponentChanged(_control, null, _control.ProgressWidth, value);
                _control.ProgressWidth = value;
            }
        }
    }

    /// <summary>Gets or sets the inner ring thickness.</summary>
    public int InnerWidth
    {
        get => _control.InnerWidth;
        set
        {
            if (_control.InnerWidth != value)
            {
                _service?.OnComponentChanged(_control, null, _control.InnerWidth, value);
                _control.InnerWidth = value;
            }
        }
    }

    /// <summary>Gets or sets the progress arc start angle in degrees.</summary>
    public int StartAngle
    {
        get => _control.StartAngle;
        set
        {
            if (_control.StartAngle != value)
            {
                _service?.OnComponentChanged(_control, null, _control.StartAngle, value);
                _control.StartAngle = value;
            }
        }
    }

    /// <summary>Gets or sets whether tri-state threshold colours are used for the progress arc.</summary>
    public bool UseTriStateColors
    {
        get => _control.TriStateValues.UseTriStateColors;
        set
        {
            if (_control.TriStateValues.UseTriStateColors != value)
            {
                _service?.OnComponentChanged(_control, null, _control.TriStateValues.UseTriStateColors, value);
                _control.TriStateValues.UseTriStateColors = value;
            }
        }
    }

    /// <summary>Gets or sets the palette colour style for the progress arc fill.</summary>
    public PaletteColorStyle ValueBackColorStyle
    {
        get => _control.ValueBackColorStyle;
        set
        {
            if (_control.ValueBackColorStyle != value)
            {
                _service?.OnComponentChanged(_control, null, _control.ValueBackColorStyle, value);
                _control.ValueBackColorStyle = value;
            }
        }
    }

    /// <summary>Gets or sets the primary progress arc colour.</summary>
    public Color ProgressColor
    {
        get => _control.StateCommon.Back.Color1;
        set
        {
            if (_control.StateCommon.Back.Color1 != value)
            {
                _service?.OnComponentChanged(_control, null, _control.StateCommon.Back.Color1, value);
                _control.StateCommon.Back.Color1 = value;
            }
        }
    }

    #endregion

    #region Smart-Tag Methods

    /// <summary>
    /// Sets the control width and height to the larger of the two dimensions so the indicator stays circular.
    /// </summary>
    public void MakeSquare()
    {
        int size = Math.Max(_control.Width, _control.Height);
        if (_control.Width == size && _control.Height == size)
        {
            return;
        }

        Size newSize = new Size(size, size);
        _service?.OnComponentChanged(_control, null, _control.Size, newSize);
        _control.Size = newSize;
    }

    #endregion

    #region Public Override

    /// <inheritdoc />
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        DesignerActionItemCollection actions = new DesignerActionItemCollection();

        if (_control == null)
        {
            return actions;
        }

        actions.Add(new DesignerActionHeaderItem("Progress"));
        actions.Add(new DesignerActionPropertyItem(nameof(Value), "Value", "Progress",
            "Current progress between Minimum and Maximum."));
        actions.Add(new DesignerActionPropertyItem(nameof(Minimum), "Minimum", "Progress",
            "Lower bound of the progress range."));
        actions.Add(new DesignerActionPropertyItem(nameof(Maximum), "Maximum", "Progress",
            "Upper bound of the progress range."));
        actions.Add(new DesignerActionPropertyItem(nameof(Style), "Style", "Progress",
            "Continuous shows value; Marquee rotates the arc segment."));

        actions.Add(new DesignerActionHeaderItem("Text"));
        actions.Add(new DesignerActionPropertyItem(nameof(UseValueAsText), "Use value as text", "Text",
            "When enabled, centre text is formatted from the current Value."));
        actions.Add(new DesignerActionPropertyItem(nameof(Text), "Centre text", "Text",
            "Primary value displayed in the centre of the control."));
        actions.Add(new DesignerActionPropertyItem(nameof(SuperscriptText), "Superscript", "Text",
            "Small annotation drawn beside the top of the centre text."));
        actions.Add(new DesignerActionPropertyItem(nameof(SubscriptText), "Subscript", "Text",
            "Small annotation drawn beside the bottom of the centre text."));

        actions.Add(new DesignerActionHeaderItem("Animation"));
        actions.Add(new DesignerActionPropertyItem(nameof(AnimationSpeed), "Value speed (ms)", "Animation",
            "Duration of animated transitions when Value changes. Zero disables animation."));
        actions.Add(new DesignerActionPropertyItem(nameof(AnimationFunction), "Easing", "Animation",
            "Easing curve applied to value transitions."));
        actions.Add(new DesignerActionPropertyItem(nameof(MarqueeAnimationSpeed), "Marquee speed (ms)", "Animation",
            "Rotation period when Style is Marquee."));

        actions.Add(new DesignerActionHeaderItem("Layout"));
        actions.Add(new DesignerActionPropertyItem(nameof(OuterWidth), "Outer ring width", "Layout",
            "Thickness of the outer decorative ring. Zero hides the ring."));
        actions.Add(new DesignerActionPropertyItem(nameof(ProgressWidth), "Progress width", "Layout",
            "Thickness of the progress arc band."));
        actions.Add(new DesignerActionPropertyItem(nameof(InnerWidth), "Inner ring width", "Layout",
            "Thickness of the inner decorative ring. Zero hides the ring."));
        actions.Add(new DesignerActionPropertyItem(nameof(StartAngle), "Start angle", "Layout",
            "Progress arc origin in degrees (GDI+ convention)."));
        actions.Add(new DesignerActionMethodItem(this, nameof(MakeSquare), "Make square", "Layout",
            "Sets Width and Height to the larger dimension so the control renders as a true circle."));

        actions.Add(new DesignerActionHeaderItem("Visuals"));
        actions.Add(new DesignerActionPropertyItem(nameof(ProgressColor), "Progress colour", "Visuals",
            "Primary colour of the progress arc (StateCommon.Back.Color1)."));
        actions.Add(new DesignerActionPropertyItem(nameof(ValueBackColorStyle), "Progress colour style", "Visuals",
            "Palette drawing style for the progress arc fill."));
        actions.Add(new DesignerActionPropertyItem(nameof(UseTriStateColors), "Use tri-state colours", "Visuals",
            "When enabled, progress colour changes by low/medium/high thresholds (TriStateValues)."));

        return actions;
    }

    #endregion
}
