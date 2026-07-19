using Microsoft.DotNet.DesignTools.Designers.Actions;
using System.ComponentModel;
using MyButtonControl;

namespace MyButton.Designer.Server
{
  internal partial class MyButtonDesigner
  {
    private class ActionList : DesignerActionList
    {
      private const string Behavior = nameof(Behavior);
      private const string Data = nameof(Data);

      public ActionList(MyButtonDesigner designer) : base(designer.Component)
      {
      }

      public MyType MyProperty
      {
        get => ((MyButtonControl.MyButton)Component!).MyProperty;

        set =>
            TypeDescriptor.GetProperties(Component!)[nameof(MyProperty)]!
                .SetValue(Component, value);
      }

      public override DesignerActionItemCollection GetSortedActionItems()
      {
        DesignerActionItemCollection actionItems = new()
                {
                    new DesignerActionHeaderItem(Behavior),
                    new DesignerActionHeaderItem(Data),
                    new DesignerActionPropertyItem(
                        nameof(MyProperty),
                        "Empty form",
                        Behavior,
                        "Display empty form.")
                };

        return actionItems;
      }
    }
  }
}