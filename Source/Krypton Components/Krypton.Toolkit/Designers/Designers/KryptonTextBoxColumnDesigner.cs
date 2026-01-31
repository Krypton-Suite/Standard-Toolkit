#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonTextBoxColumnDesigner : ComponentDesigner
{
    #region Instance Fields
    private KryptonDataGridViewTextBoxColumn? _textBox;
    private IComponentChangeService? _changeService;
    private IDesignerHost? _designerHost;
    #endregion

    #region Public Overrides
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate the designer with.</param>
    public override void Initialize([DisallowNull] IComponent component)
    {
        // Let base class do standard stuff
        base.Initialize(component);

        Debug.Assert(component != null);

        // Cast to correct type
        _textBox = component as KryptonDataGridViewTextBoxColumn;

        // Get access to the design services
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        _designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;

        // We need to know when we are being removed
        if (_changeService != null)
        {
            _changeService.ComponentRemoving += OnComponentRemoving;
        }
    }

    /// <summary>
    /// No extra associated components for column-level specs (serialized as content).
    /// </summary>
    public override ICollection AssociatedComponents => base.AssociatedComponents;
    #endregion

    #region Implementation
    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        // If our column is being removed: clear content-serialized specs
        if ((_textBox != null) && Equals(e.Component, _textBox))
        {
            _changeService?.OnComponentChanging(_textBox, null);
            _textBox.ButtonSpecs.Clear();
            _changeService?.OnComponentChanged(_textBox, null, null, null);
        }
    }
    #endregion
}