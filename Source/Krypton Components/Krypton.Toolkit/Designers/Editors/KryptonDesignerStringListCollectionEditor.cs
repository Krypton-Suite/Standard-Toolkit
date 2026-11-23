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
/// Krypton-themed designer editor for <see cref="List{T}"/> of <see cref="string"/> properties.
/// </summary>
public sealed class KryptonDesignerStringListCollectionEditor : KryptonDesignerStandardCollectionEditor
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerStringListCollectionEditor"/> class.
    /// </summary>
    public KryptonDesignerStringListCollectionEditor()
        : base(typeof(string))
    {
    }
    #endregion

    #region Protected
    /// <inheritdoc />
    protected override object? SetItems(object? editValue, object[]? value)
    {
        if (editValue is IList<string> stringList)
        {
            stringList.Clear();
            if (value is not null)
            {
                foreach (var item in value)
                {
                    stringList.Add(item?.ToString() ?? string.Empty);
                }
            }

            return editValue;
        }

        return base.SetItems(editValue, value);
    }
    #endregion
}
