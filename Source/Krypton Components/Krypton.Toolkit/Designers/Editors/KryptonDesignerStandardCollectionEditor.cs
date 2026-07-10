#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Base class for Krypton-themed standard list/property-grid collection editors.
/// </summary>
public abstract class KryptonDesignerStandardCollectionEditor : KryptonDesignerCollectionEditor
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerStandardCollectionEditor"/> class.
    /// </summary>
    /// <param name="collectionType">Collection item type.</param>
    protected KryptonDesignerStandardCollectionEditor(Type collectionType)
        : base(collectionType)
    {
    }
    #endregion

    #region Protected
    /// <inheritdoc />
    protected override VisualDesignerCollectionForm CreateKryptonDesignerCollectionForm() =>
        new VisualStandardCollectionForm(this);

    /// <summary>
    /// Allows specialized editors to handle item removal before the item is destroyed.
    /// </summary>
    /// <param name="item">Item being removed.</param>
    internal virtual void OnDesignerItemRemoving(object? item)
    {
    }
    #endregion
}

/// <summary>
/// Minimal site wrapper so the property grid can access designer services.
/// </summary>
internal sealed class KryptonDesignerPropertyGridSite : ISite, IServiceProvider
{
    #region Instance Fields
    private readonly ITypeDescriptorContext? _context;
    private readonly IComponent _component;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerPropertyGridSite"/> class.
    /// </summary>
    /// <param name="context">Designer context.</param>
    /// <param name="component">Component shown in the property grid.</param>
    public KryptonDesignerPropertyGridSite(ITypeDescriptorContext? context, IComponent component)
    {
        _context = context;
        _component = component;
    }
    #endregion

    #region ISite
    /// <inheritdoc />
    public IComponent Component => _component;

    /// <inheritdoc />
    public IContainer? Container => null;

    /// <inheritdoc />
    public bool DesignMode => true;

    /// <inheritdoc />
    public string? Name { get; set; }
    #endregion

    #region IServiceProvider
    /// <inheritdoc />
    public object? GetService(Type serviceType) => _context?.GetService(serviceType);
    #endregion
}
