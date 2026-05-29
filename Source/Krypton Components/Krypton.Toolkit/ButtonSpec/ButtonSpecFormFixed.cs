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
/// Implementation for the fixed navigator buttons.
/// </summary>
[TypeConverter(typeof(ButtonSpecFormFixedConverter))]
public abstract class ButtonSpecFormFixed : ButtonSpec
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecFormFixed class.
    /// </summary>
    /// <param name="form">Reference to owning krypton form.</param>
    /// <param name="fixedStyle">Fixed style to use.</param>
    protected ButtonSpecFormFixed([DisallowNull] KryptonForm form,
        PaletteButtonSpecStyle fixedStyle)
    {
        Debug.Assert(form != null);

        // Remember back reference to owning navigator.
        KryptonForm = form!;

        // Fix the type
        ProtectedType = fixedStyle;
    }      
    #endregion   

    #region AllowComponent
    /// <summary>
    /// Can a component be associated with the view.
    /// </summary>
    public override bool AllowComponent => false;

    #endregion

    #region KryptonForm
    /// <summary>
    /// Gets access to the owning krypton form.
    /// </summary>
    protected KryptonForm KryptonForm { get; }

    #endregion

    #region ButtonSpecType
    /// <summary>
    /// Gets and sets the actual type of the button.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual PaletteButtonSpecStyle ButtonSpecType
    {
        get => ProtectedType;
        set => ProtectedType = value;
    }
    #endregion
}