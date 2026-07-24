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
/// Enhanced ToolStripMenuItem with the ability to display radio button for checked item.
/// <br></br>
/// If CheckOnClick property is set to true and CheckMarkDisplayStyle is set to RadioButton, context
/// menu strip behaves similar way as GroupBox with RadioButton controls.
/// Within same group only one item can be selected.
/// </summary>
[ToolboxBitmap(typeof(ToolStripMenuItem)), RefreshProperties(RefreshProperties.Repaint), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
public class KryptonEnhancedToolStripMenuItem : ToolStripMenuItem
{
    #region Variables
    private readonly EnhancedMenuItemValues _values;
    #endregion

    #region Properties

    /// <summary>
    /// Gets the expandable configuration values for designer and runtime use.
    /// </summary>
    [Category("Behavior")]
    [Description("Associated enum value, check mark display style, and radio group settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public EnhancedMenuItemValues MenuItemValues => _values;

    private bool ShouldSerializeMenuItemValues() => !_values.IsDefault;

    private void ResetMenuItemValues() => _values.Reset();

    /// <summary>
    /// Menu items with radio button display can be used to bind enum values.
    /// This property can be used to store associated Enum value.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Enum? AssociatedEnumValue { get => _values.AssociatedEnumValue; set => _values.AssociatedEnumValue = value; }

    /// <summary>
    /// Switches between CheckkBox or RadioButton style.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new CheckMarkDisplayStyle DisplayStyle { get => _values.DisplayStyle; set => _values.DisplayStyle = value; }

    /// <summary>
    /// In order to provide behavior similar to RadioButton group, we need a way to mark groups. 
    /// This property is used for this purpose. All menu items with identical RadioButtonGroupName belong to the same group.
    /// It means that clicking one item within group de-selects previously selected item and 
    /// selects clicked item (only one item can be selected).
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? RadioButtonGroupName { get => _values.RadioButtonGroupName; set => _values.RadioButtonGroupName = value; }
    #endregion

    #region Constructor
    public KryptonEnhancedToolStripMenuItem()
    {
        _values = new EnhancedMenuItemValues(this);

        CheckOnClick = true;
    }
    #endregion

    #region Overrides
    protected override void OnClick(EventArgs e)
    {
        if (DisplayStyle == CheckMarkDisplayStyle.RadioButton && CheckOnClick)
        {
            ToolStrip? toolStrip = GetCurrentParent();

            if (toolStrip != null)
            {
                foreach (ToolStripItem items in toolStrip.Items)
                {
                    if (items is KryptonEnhancedToolStripMenuItem menuItem)
                    {
                        if (menuItem is { DisplayStyle: CheckMarkDisplayStyle.RadioButton, CheckOnClick: true } &&
                            menuItem.RadioButtonGroupName == RadioButtonGroupName)
                        {
                            menuItem.Checked = false;
                        }
                    }
                }
            }
        }

        base.OnClick(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        //if CheckMarkDisplayStyle is equal RadioButton additional paining or radio button is needed
        if (DisplayStyle == CheckMarkDisplayStyle.RadioButton)
        {
            //Find location of radio button
            Size radioButtonSize = RadioButtonRenderer.GetGlyphSize(e.Graphics, RadioButtonState.CheckedNormal);
            int radioButtonX = ContentRectangle.X + 3;
            int radioButtonY = ContentRectangle.Y + (ContentRectangle.Height - radioButtonSize.Height) / 2;

            //Find state of radio button
            RadioButtonState state = RadioButtonState.CheckedNormal;
            if (Checked)
            {
                if (Pressed)
                {
                    state = RadioButtonState.CheckedPressed;
                }
                else if (Selected)
                {
                    state = RadioButtonState.CheckedHot;
                }
            }
            else
            {
                if (Pressed)
                {
                    state = RadioButtonState.UncheckedPressed;
                }
                else if (Selected)
                {
                    state = RadioButtonState.UncheckedHot;
                }
                else
                {
                    state = RadioButtonState.UncheckedNormal;
                }
            }

            //Draw RadioButton in proper state (Checked/Unchecked; Hot/Normal/Pressed)
            RadioButtonRenderer.DrawRadioButton(e.Graphics, new Point(radioButtonX, radioButtonY), state);

        }
    }
    #endregion
}