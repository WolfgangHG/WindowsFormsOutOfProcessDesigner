using Microsoft.DotNet.DesignTools.Designers;
using Microsoft.DotNet.DesignTools.Designers.Actions;

namespace MyButton.Designer.Server
{
  internal partial class MyButtonDesigner : ControlDesigner
  {
    public override DesignerActionListCollection ActionLists
        => new()
        {
                new ActionList(this)
        };
  }
}