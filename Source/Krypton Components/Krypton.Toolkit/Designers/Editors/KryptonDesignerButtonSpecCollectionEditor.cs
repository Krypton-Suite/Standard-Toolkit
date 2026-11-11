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
/// Krypton-themed designer editor for collections of <see cref="ButtonSpecAny"/>.
/// </summary>
public class KryptonDesignerButtonSpecAnyCollectionEditor : KryptonDesignerStandardCollectionEditor
{
    #region Instance Fields
    private readonly List<ButtonSpec> _sitedDuringSession = [];
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerButtonSpecAnyCollectionEditor"/> class.
    /// </summary>
    public KryptonDesignerButtonSpecAnyCollectionEditor()
        : base(typeof(ButtonSpecAny))
    {
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerButtonSpecAnyCollectionEditor"/> class.
    /// </summary>
    /// <param name="itemType">ButtonSpec item type for specialized collections.</param>
    protected KryptonDesignerButtonSpecAnyCollectionEditor(Type itemType)
        : base(itemType)
    {
    }
    #endregion

    #region Protected
    /// <inheritdoc />
    protected override Type[] CreateNewItemTypes() => [DesignerCollectionItemType];

    /// <inheritdoc />
    protected override object[] GetItems(object? editValue)
    {
        var items = base.GetItems(editValue!);
        SiteUnnamedSpecs(items);
        return items;
    }

    /// <summary>
    /// Returns the designer component name for the members list.
    /// </summary>
    /// <param name="value">Collection item.</param>
    /// <returns>
    /// <see cref="ISite.Name"/> when the spec is sited (<c>buttonSpecAny1</c>); otherwise the type name.
    /// <see cref="ButtonSpec.UniqueName"/> remains the persistence key. <see cref="ButtonSpec.Text"/> is the button caption.
    /// </returns>
    protected override string GetDisplayText(object? value)
    {
        // Site.Name is the editable designer name (buttonSpecAny1). UniqueName is a GUID persistence key.
        if (value is ButtonSpec buttonSpec)
        {
            var siteName = buttonSpec.Site?.Name;
            return string.IsNullOrEmpty(siteName) ? buttonSpec.GetType().Name : siteName!;
        }

        return base.GetDisplayText(value);
    }

    /// <inheritdoc />
    internal override void OnDesignerEditCommitted() => _sitedDuringSession.Clear();

    /// <inheritdoc />
    internal override void OnDesignerEditCancelled()
    {
        if (DesignerContext?.GetService(typeof(IDesignerHost)) is IDesignerHost host && host.Container is not null)
        {
            foreach (var spec in _sitedDuringSession)
            {
                if (spec.Site != null)
                {
                    host.Container.Remove(spec);
                }
            }
        }

        _sitedDuringSession.Clear();
    }

    /// <summary>
    /// Gives existing unsited specs a designer name so the members list and the <see cref="ButtonSpec.Name"/> property can show it.
    /// </summary>
    /// <param name="items">Items currently in the collection.</param>
    private void SiteUnnamedSpecs(object[]? items)
    {
        if (items is null)
        {
            return;
        }

        if (DesignerContext?.GetService(typeof(IDesignerHost)) is not IDesignerHost host || host.Container is null)
        {
            return;
        }

        foreach (var item in items)
        {
            if (item is ButtonSpec spec && spec.Site == null)
            {
                host.Container.Add(spec);
                _sitedDuringSession.Add(spec);
            }
        }
    }

    /// <inheritdoc />
    protected override object? SetItems(object? editValue, object[]? value)
    {
        if (editValue is IList list)
        {
            list.Clear();
            if (value is not null)
            {
                foreach (var item in value)
                {
                    list.Add(item);
                }
            }

            return editValue;
        }

        return base.SetItems(editValue, value);
    }
    #endregion
}

/// <summary>
/// Krypton-themed designer editor for collections of <see cref="ButtonSpecHeaderGroup"/>.
/// </summary>
public sealed class KryptonDesignerButtonSpecHeaderGroupCollectionEditor : KryptonDesignerButtonSpecAnyCollectionEditor
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerButtonSpecHeaderGroupCollectionEditor"/> class.
    /// </summary>
    public KryptonDesignerButtonSpecHeaderGroupCollectionEditor()
        : base(typeof(ButtonSpecHeaderGroup))
    {
    }
    #endregion
}
