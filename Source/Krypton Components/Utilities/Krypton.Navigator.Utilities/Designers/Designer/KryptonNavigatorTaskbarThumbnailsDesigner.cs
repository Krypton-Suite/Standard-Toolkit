#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Designer for the <see cref="KryptonNavigatorTaskbarThumbnails"/> component.
/// </summary>
internal class KryptonNavigatorTaskbarThumbnailsDesigner : ComponentDesigner
{
    private DesignerActionListCollection? _actionLists;

    /// <inheritdoc />
    public override void Initialize(IComponent component)
    {
        base.Initialize(component);

        if (component is KryptonNavigatorTaskbarThumbnails { Site: not null } thumbnails)
        {
            var host = (IDesignerHost?)GetService(typeof(IDesignerHost));
            if (host?.RootComponent is Form form && thumbnails.Navigator == null)
            {
                KryptonNavigator? navigator = FindNavigator(form);
                if (navigator != null)
                {
                    var navigatorProp = TypeDescriptor.GetProperties(thumbnails)[nameof(KryptonNavigatorTaskbarThumbnails.Navigator)];
                    navigatorProp?.SetValue(thumbnails, navigator);
                }
            }
        }
    }

    /// <inheritdoc />
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            _actionLists ??= new DesignerActionListCollection
            {
                new KryptonNavigatorTaskbarThumbnailsActionList(this)
            };

            return _actionLists;
        }
    }

    private static KryptonNavigator? FindNavigator(Control root)
    {
        if (root is KryptonNavigator navigator)
        {
            return navigator;
        }

        foreach (Control child in root.Controls)
        {
            KryptonNavigator? found = FindNavigator(child);
            if (found != null)
            {
                return found;
            }
        }

        return null;
    }
}
