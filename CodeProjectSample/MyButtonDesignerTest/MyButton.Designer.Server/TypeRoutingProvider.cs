using Microsoft.DotNet.DesignTools.TypeRouting;
using System.Collections.Generic;

namespace MyButton.Designer.Server
{
  [ExportTypeRoutingDefinitionProvider]
  internal class TypeRoutingProvider : TypeRoutingDefinitionProvider
  {
    public override IEnumerable<TypeRoutingDefinition> GetDefinitions()
        => new[]
        {
                new TypeRoutingDefinition(
                    TypeRoutingKinds.Designer,
                    nameof(MyButtonDesigner),
                    typeof(MyButtonDesigner))
        };
  }
}