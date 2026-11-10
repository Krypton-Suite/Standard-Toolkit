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
/// Contains plate label settings for a <see cref="KryptonKnobAlternate"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KnobPlateLabelsValues : Storage
{
    #region Instance Fields
    private readonly KryptonKnobAlternate _owner;
    private readonly KnobPlateLabel _label1;
    private readonly KnobPlateLabel _label2;
    private readonly KnobPlateLabel _label3;
    private readonly KnobPlateLabel _label4;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KnobPlateLabelsValues"/> class.
    /// </summary>
    /// <param name="owner">Reference to owning control.</param>
    public KnobPlateLabelsValues(KryptonKnobAlternate owner)
    {
        _owner = owner;
        _label1 = new KnobPlateLabel(InvalidateOwner);
        _label2 = new KnobPlateLabel(InvalidateOwner);
        _label3 = new KnobPlateLabel(InvalidateOwner);
        _label4 = new KnobPlateLabel(InvalidateOwner);
    }
    #endregion

    #region IsDefault
    /// <inheritdoc />
    public override bool IsDefault =>
        _label1.IsDefault &&
        _label2.IsDefault &&
        _label3.IsDefault &&
        _label4.IsDefault;
    #endregion

    #region Public
    /// <summary>
    /// Gets the first plate label slot.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"First plate label.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KnobPlateLabel Label1 => _label1;

    private bool ShouldSerializeLabel1() => !Label1.IsDefault;

    /// <summary>
    /// Gets the second plate label slot.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Second plate label.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KnobPlateLabel Label2 => _label2;

    private bool ShouldSerializeLabel2() => !Label2.IsDefault;

    /// <summary>
    /// Gets the third plate label slot.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Third plate label.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KnobPlateLabel Label3 => _label3;

    private bool ShouldSerializeLabel3() => !Label3.IsDefault;

    /// <summary>
    /// Gets the fourth plate label slot.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Fourth plate label.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KnobPlateLabel Label4 => _label4;

    private bool ShouldSerializeLabel4() => !Label4.IsDefault;

    /// <summary>
    /// Gets all configured labels.
    /// </summary>
    internal IEnumerable<KnobPlateLabel> GetVisibleLabels()
    {
        if (Label1.Visible && !string.IsNullOrEmpty(Label1.Text))
        {
            yield return Label1;
        }

        if (Label2.Visible && !string.IsNullOrEmpty(Label2.Text))
        {
            yield return Label2;
        }

        if (Label3.Visible && !string.IsNullOrEmpty(Label3.Text))
        {
            yield return Label3;
        }

        if (Label4.Visible && !string.IsNullOrEmpty(Label4.Text))
        {
            yield return Label4;
        }
    }

    /// <summary>
    /// Clears all plate labels.
    /// </summary>
    public void Clear()
    {
        Label1.Text = string.Empty;
        Label1.Visible = false;
        Label2.Text = string.Empty;
        Label2.Visible = false;
        Label3.Text = string.Empty;
        Label3.Visible = false;
        Label4.Text = string.Empty;
        Label4.Visible = false;
    }

    /// <summary>
    /// Applies a RUN / STOP label preset using the control start and end angles.
    /// </summary>
    public void ApplyRunStopPreset()
    {
        Clear();
        Label1.Text = @"RUN";
        Label1.Color = Color.Red;
        Label1.Angle = _owner.GetEndAngle() - 20f;
        Label1.Visible = true;
        Label2.Text = @"STOP";
        Label2.Color = Color.Black;
        Label2.Angle = _owner.GetStartAngle() + 20f;
        Label2.Visible = true;
    }

    /// <summary>
    /// Applies an OFF / ON label preset using the control start and end angles.
    /// </summary>
    public void ApplyOffOnPreset()
    {
        Clear();
        Label1.Text = @"ON";
        Label1.Color = Color.Black;
        Label1.Angle = _owner.GetEndAngle() - 20f;
        Label1.Visible = true;
        Label2.Text = @"OFF";
        Label2.Color = Color.Black;
        Label2.Angle = _owner.GetStartAngle() + 20f;
        Label2.Visible = true;
    }

    /// <summary>
    /// Resets all plate labels to defaults.
    /// </summary>
    public void Reset() => Clear();
    #endregion

    #region Implementation
    /// <inheritdoc />
    public override string ToString() => IsDefault ? string.Empty : @"Modified";

    private void InvalidateOwner() => _owner.OnPlateLabelsChanged();
    #endregion
}
