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

// ReSharper disable MemberCanBePrivate.Global

namespace Krypton.Toolkit;

/// <summary>
/// Combines button functionality with the styling features of the Krypton Toolkit.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonButton), "ToolboxBitmaps.KryptonButton.bmp")]
[DefaultEvent(nameof(Click))]
[DefaultProperty(nameof(Text))]
[DesignerCategory(@"code")]
[Description(@"Raises an event when the user clicks it.")]
[Designer(typeof(KryptonButtonDesigner))]
public class KryptonButton : KryptonDropButton
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonButton class.
    /// </summary>
    public KryptonButton()
    {
        // Create the view button instance
        _drawButton.DropDown = false;
        _drawButton.Splitter = false;

        // Create a button controller to handle button style behaviour
        _buttonController.BecomesFixed = false;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the visual orientation of the control
    /// </summary>
    [Browsable(true)]
    [Localizable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(VisualOrientation.Top)]
    public virtual VisualOrientation Orientation
    {
        // Backward compatible fix.
        get => ButtonOrientation;
        set => ButtonOrientation = value;
    }

    /// <summary>Gets or sets a value indicating whether [show split option].</summary>
    /// <value><c>true</c> if [show split option]; otherwise, <c>false</c>.</value>

    [Category(@"Visuals")]
    [DefaultValue(false)]
    [Description(@"Displays the split/dropdown option.")]
    public bool ShowSplitOption
    {
        // Backward compatible fix.
        get => base.Splitter;
        set
        {
            _drawButton.DropDown = value;
            base.Splitter = value;
        }
    }

    [Browsable(false)]
    [Localizable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    public new bool Splitter
    {
        get => base.Splitter;
        set => base.Splitter = value;
    }
    #endregion
}