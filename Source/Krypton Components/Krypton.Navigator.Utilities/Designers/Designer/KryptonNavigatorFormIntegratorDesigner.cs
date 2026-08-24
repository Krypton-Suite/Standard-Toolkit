#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Designer for the <see cref="KryptonNavigatorFormIntegrator"/> component.
/// </summary>
internal class KryptonNavigatorFormIntegratorDesigner : ComponentDesigner
{
    /// <inheritdoc />
    public override void Initialize(IComponent component)
    {
        base.Initialize(component);

        if (component is not KryptonNavigatorFormIntegrator { Site: not null } integrator)
        {
            return;
        }

        var host = (IDesignerHost?)GetService(typeof(IDesignerHost));
        if (host?.RootComponent is not KryptonForm form || integrator.Form != null)
        {
            return;
        }

        var formProp = TypeDescriptor.GetProperties(integrator)[nameof(KryptonNavigatorFormIntegrator.Form)];
        formProp?.SetValue(integrator, form);

        // Prefer a navigator already on the form when one exists.
        if (integrator.Navigator == null)
        {
            foreach (Control child in form.Controls)
            {
                if (child is KryptonNavigator navigator)
                {
                    var navProp = TypeDescriptor.GetProperties(integrator)[nameof(KryptonNavigatorFormIntegrator.Navigator)];
                    navProp?.SetValue(integrator, navigator);
                    break;
                }
            }
        }
    }
}
