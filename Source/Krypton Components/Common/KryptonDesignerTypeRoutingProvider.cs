#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.WinFormsDesignerSdk;

/// <summary>
/// MEF type-routing provider compiled into each DesignToolsServer assembly.
/// Registers every <see cref="ComponentDesigner"/> in this assembly under both its
/// short name and its full name so <c>[Designer("Name")]</c> and assembly-qualified
/// <c>[Designer]</c> strings resolve without a hand-maintained catalogue.
/// </summary>
[ExportTypeRoutingDefinitionProvider]
internal sealed class KryptonDesignerTypeRoutingProvider : TypeRoutingDefinitionProvider
{
    /// <inheritdoc />
    public override IEnumerable<TypeRoutingDefinition> GetDefinitions()
    {
        var definitions = new List<TypeRoutingDefinition>();
        var designerBase = typeof(ComponentDesigner);
        foreach (var type in typeof(KryptonDesignerTypeRoutingProvider).Assembly.GetTypes())
        {
            if (type.IsAbstract || !designerBase.IsAssignableFrom(type))
            {
                continue;
            }

            definitions.Add(new TypeRoutingDefinition(TypeRoutingKinds.Designer, type.Name, type));
            if (type.FullName != null &&
                !string.Equals(type.FullName, type.Name, StringComparison.Ordinal))
            {
                definitions.Add(new TypeRoutingDefinition(TypeRoutingKinds.Designer, type.FullName, type));
            }
        }

        return definitions;
    }
}
