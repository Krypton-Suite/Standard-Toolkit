#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Designer for the <see cref="KryptonTabbedMdiManager"/> component.
/// </summary>
internal class KryptonTabbedMdiManagerDesigner : ComponentDesigner
{
    #region Public Overrides

    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    public override void Initialize(IComponent component)
    {
        base.Initialize(component);

        if (component is KryptonTabbedMdiManager { Site: not null } manager)
        {
            var host = (IDesignerHost?)GetService(typeof(IDesignerHost));
            if (host?.RootComponent is Form form && manager.ParentForm == null)
            {
                // Auto-assign parent form when dropped on a form
                var parentFormProp = TypeDescriptor.GetProperties(manager)[nameof(KryptonTabbedMdiManager.ParentForm)];
                parentFormProp?.SetValue(manager, form);
            }
        }
    }

    #endregion
}