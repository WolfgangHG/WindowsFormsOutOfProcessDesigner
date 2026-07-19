using Microsoft.DotNet.DesignTools.Client.TypeRouting;
using System.Collections.Generic;

namespace MyButton.Designer.Client
{
  [ExportTypeRoutingDefinitionProvider]
  internal class TypeRoutingProvider : TypeRoutingDefinitionProvider
  {
    public override IEnumerable<TypeRoutingDefinition> GetDefinitions()
    {
      return new[]
      {
                new TypeRoutingDefinition(
                    TypeRoutingKinds.Editor,
                    nameof(MyButtonEditor),
                    typeof(MyButtonEditor)
                )
            };
    }
  }
}