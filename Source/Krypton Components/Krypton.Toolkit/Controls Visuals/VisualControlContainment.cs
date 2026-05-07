#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Base class used for implementation of container controls using a Control as the base.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
[Designer("System.Windows.Forms.Design.ParentControlDesigner")]
public abstract class VisualControlContainment : VisualControl
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the VisualContainer class.
    /// </summary>
    protected VisualControlContainment()
    {
        // We act as a container for child controls
        SetStyle(ControlStyles.ContainerControl, true);

        // User cannot select a container control itself
        SetStyle(ControlStyles.Selectable, false);

        // Default TabStop is true in base class, we want it to be false
        TabStop = false;
    }
    #endregion

    #region Public Overrides
    /// <summary>
    /// Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DefaultValue(false)]
    public new bool TabStop
    {
        get => base.TabStop;
        set => base.TabStop = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control can accept data that the user drags onto it.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override bool AllowDrop
    {
        get => base.AllowDrop;
        set => base.AllowDrop = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control is automatically resized to display its entire contents.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override bool AutoSize
    {
        get => base.AutoSize;
        set => base.AutoSize = value;
    }

    /// <summary>
    /// Gets or sets the text associated with this control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [AllowNull]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    /// <summary>
    /// Gets or sets the background color for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    /// <summary>
    /// Gets or sets the font of the text displayed by the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [AmbientValue(null)]
    [AllowNull]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Font Font
    {
        get => base.Font;
        set => base.Font = value;
    }

    /// <summary>
    /// Gets or sets the foreground color for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color ForeColor
    {
        get => base.ForeColor;
        set => base.ForeColor = value;
    }
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected override Size DefaultSize => new Size(150, 150);

    #endregion
}