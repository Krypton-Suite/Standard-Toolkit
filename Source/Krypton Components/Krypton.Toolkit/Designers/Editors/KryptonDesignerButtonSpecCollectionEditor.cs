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
        // Site.Name is the designer host name (buttonSpecAny1). UniqueName is a GUID persistence key and must not label the list.
        if (value is ButtonSpec buttonSpec)
        {
            var siteName = buttonSpec.Site?.Name;
            return string.IsNullOrEmpty(siteName) ? buttonSpec.GetType().Name : siteName!;
        }

        return base.GetDisplayText(value);
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
