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

namespace Krypton.Navigator;

/// <summary>
/// ButtonSpecNavigator specific implementation of a button specification.
/// </summary>
public class ButtonSpecNavigator : ButtonSpecHeaderGroup
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecNavigator class.
    /// </summary>
    public ButtonSpecNavigator() => ProtectedType = NavigatorToPaletteType(PaletteNavButtonSpecStyle.Generic);

    #endregion

    #region Type
    /// <summary>
    /// Gets and sets the button type.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
    public new PaletteButtonSpecStyle Type
    {
        get => ProtectedType;
        set => ProtectedType = value;
    }
    #endregion

    #region TypeRestricted
    /// <summary>
    /// Gets and sets the button type.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"Defines a restricted type for a navigator button spec.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(PaletteNavButtonSpecStyle), "Generic")]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Content)]
    public PaletteNavButtonSpecStyle TypeRestricted
    {
        get => PaletteTypeToNavigator(ProtectedType);

        set
        {
            ProtectedType = NavigatorToPaletteType(value);
            OnButtonSpecPropertyChanged(nameof(Type));
        }
    }

    /// <summary>
    /// Resets the TypeRestricted property to its default value.
    /// </summary>
    public void ResetTypeRestricted() => TypeRestricted = PaletteNavButtonSpecStyle.Generic;
    #endregion

    #region Implementation
    private PaletteButtonSpecStyle NavigatorToPaletteType(PaletteNavButtonSpecStyle type)
    {
        switch (type)
        {
            case PaletteNavButtonSpecStyle.Generic:
                return PaletteButtonSpecStyle.Generic;
            case PaletteNavButtonSpecStyle.ArrowUp:
                return PaletteButtonSpecStyle.ArrowUp;
            case PaletteNavButtonSpecStyle.ArrowRight:
                return PaletteButtonSpecStyle.ArrowRight;
            case PaletteNavButtonSpecStyle.ArrowLeft:
                return PaletteButtonSpecStyle.ArrowLeft;
            case PaletteNavButtonSpecStyle.ArrowDown:
                return PaletteButtonSpecStyle.ArrowDown;
            case PaletteNavButtonSpecStyle.DropDown:
                return PaletteButtonSpecStyle.DropDown;
            case PaletteNavButtonSpecStyle.PinVertical:
                return PaletteButtonSpecStyle.PinVertical;
            case PaletteNavButtonSpecStyle.PinHorizontal:
                return PaletteButtonSpecStyle.PinHorizontal;
            case PaletteNavButtonSpecStyle.FormClose:
                return PaletteButtonSpecStyle.FormClose;
            case PaletteNavButtonSpecStyle.FormMax:
                return PaletteButtonSpecStyle.FormMax;
            case PaletteNavButtonSpecStyle.FormMin:
                return PaletteButtonSpecStyle.FormMin;
            case PaletteNavButtonSpecStyle.FormRestore:
                return PaletteButtonSpecStyle.FormRestore;
            case PaletteNavButtonSpecStyle.FormHelp:
                return PaletteButtonSpecStyle.FormHelp;
            case PaletteNavButtonSpecStyle.PendantClose:
                return PaletteButtonSpecStyle.PendantClose;
            case PaletteNavButtonSpecStyle.PendantMin:
                return PaletteButtonSpecStyle.PendantMin;
            case PaletteNavButtonSpecStyle.PendantRestore:
                return PaletteButtonSpecStyle.PendantRestore;
            case PaletteNavButtonSpecStyle.WorkspaceMaximize:
                return PaletteButtonSpecStyle.WorkspaceMaximize;
            case PaletteNavButtonSpecStyle.WorkspaceRestore:
                return PaletteButtonSpecStyle.WorkspaceRestore;
            case PaletteNavButtonSpecStyle.RibbonMinimize:
                return PaletteButtonSpecStyle.RibbonMinimize;
            case PaletteNavButtonSpecStyle.RibbonExpand:
                return PaletteButtonSpecStyle.RibbonExpand;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(type.ToString());
                return PaletteButtonSpecStyle.Generic;
        }
    }

    private PaletteNavButtonSpecStyle PaletteTypeToNavigator(PaletteButtonSpecStyle type)
    {
        switch (type)
        {
            case PaletteButtonSpecStyle.Generic:
                return PaletteNavButtonSpecStyle.Generic;
            case PaletteButtonSpecStyle.ArrowUp:
                return PaletteNavButtonSpecStyle.ArrowUp;
            case PaletteButtonSpecStyle.ArrowRight:
                return PaletteNavButtonSpecStyle.ArrowRight;
            case PaletteButtonSpecStyle.ArrowLeft:
                return PaletteNavButtonSpecStyle.ArrowLeft;
            case PaletteButtonSpecStyle.ArrowDown:
                return PaletteNavButtonSpecStyle.ArrowDown;
            case PaletteButtonSpecStyle.DropDown:
                return PaletteNavButtonSpecStyle.DropDown;
            case PaletteButtonSpecStyle.PinVertical:
                return PaletteNavButtonSpecStyle.PinVertical;
            case PaletteButtonSpecStyle.PinHorizontal:
                return PaletteNavButtonSpecStyle.PinHorizontal;
            case PaletteButtonSpecStyle.FormClose:
                return PaletteNavButtonSpecStyle.FormClose;
            case PaletteButtonSpecStyle.FormMax:
                return PaletteNavButtonSpecStyle.FormMax;
            case PaletteButtonSpecStyle.FormMin:
                return PaletteNavButtonSpecStyle.FormMin;
            case PaletteButtonSpecStyle.FormRestore:
                return PaletteNavButtonSpecStyle.FormRestore;
            case PaletteButtonSpecStyle.FormHelp:
                return PaletteNavButtonSpecStyle.FormHelp;
            case PaletteButtonSpecStyle.PendantClose:
                return PaletteNavButtonSpecStyle.PendantClose;
            case PaletteButtonSpecStyle.PendantMin:
                return PaletteNavButtonSpecStyle.PendantMin;
            case PaletteButtonSpecStyle.PendantRestore:
                return PaletteNavButtonSpecStyle.PendantRestore;
            case PaletteButtonSpecStyle.WorkspaceMaximize:
                return PaletteNavButtonSpecStyle.WorkspaceMaximize;
            case PaletteButtonSpecStyle.WorkspaceRestore:
                return PaletteNavButtonSpecStyle.WorkspaceRestore;
            case PaletteButtonSpecStyle.RibbonMinimize:
                return PaletteNavButtonSpecStyle.RibbonMinimize;
            case PaletteButtonSpecStyle.RibbonExpand:
                return PaletteNavButtonSpecStyle.RibbonExpand;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(type.ToString());
                return PaletteNavButtonSpecStyle.Generic;
        }
    }
    #endregion
}