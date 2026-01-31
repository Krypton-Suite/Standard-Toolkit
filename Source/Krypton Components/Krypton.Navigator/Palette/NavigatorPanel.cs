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

// ReSharper disable VirtualMemberCallInConstructor
namespace Krypton.Navigator;

/// <summary>
/// Storage for panel related properties.
/// </summary>
public class NavigatorPanel : Storage
{
    #region Instance Fields
    private readonly KryptonNavigator _navigator;
    private PaletteBackStyle _panelBackStyle;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the NavigatorPanel class.
    /// </summary>
    /// <param name="navigator">Reference to owning navigator instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public NavigatorPanel([DisallowNull] KryptonNavigator navigator,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(navigator is not null);

        // Remember back reference
        _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Default values
        _panelBackStyle = PaletteBackStyle.PanelClient;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (PanelBackStyle == PaletteBackStyle.PanelClient);

    #endregion

    #region PanelBackStyle
    /// <summary>
    /// Gets and sets the panel back style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Panel back style.")]
    //[DefaultValue(typeof(PaletteBackStyle), "PanelClient")]
    public PaletteBackStyle PanelBackStyle
    {
        get => _panelBackStyle;

        set
        {
            if (_panelBackStyle != value)
            {
                _panelBackStyle = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(PanelBackStyle));
            }
        }
    }
    #endregion
}