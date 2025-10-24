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
/// Special panel used in the KryptonGroup and KryptonHeaderGroup controls.
/// </summary>
public class KryptonGroupBoxPanel : KryptonGroupPanel
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonGroupPanel class.
    /// </summary>
    /// <param name="alignControl">Container control for alignment.</param>
    /// <param name="stateCommon">Common appearance state to inherit from.</param>
    /// <param name="stateDisabled">Disabled appearance state.</param>
    /// <param name="stateNormal">Normal appearance state.</param>
    /// <param name="layoutHandler">Callback delegate for layout processing.</param>
    public KryptonGroupBoxPanel(Control alignControl,
        [DisallowNull] PaletteDoubleRedirect stateCommon,
        [DisallowNull] PaletteDouble stateDisabled,
        [DisallowNull] PaletteDouble stateNormal,
        NeedPaintHandler layoutHandler)
        : base(alignControl, stateCommon, stateDisabled, stateNormal, layoutHandler)
    {
    }
    #endregion

    #region Public
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override KryptonContextMenu? KryptonContextMenu
    {
        get => base.KryptonContextMenu;
        set { /* Ignore request */ }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override ContextMenuStrip? ContextMenuStrip
    {
        get => base.ContextMenuStrip;
        set { /* Ignore request */ }
    }

    #endregion public

}