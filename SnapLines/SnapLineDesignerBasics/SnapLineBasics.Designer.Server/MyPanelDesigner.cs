using Microsoft.DotNet.DesignTools.Designers;
using Microsoft.DotNet.DesignTools.Designers.Behaviors;
using System.Collections.Generic;

namespace SnapLineBasics.Designer.Server
{
  internal partial class MyPanelDesigner : ControlDesigner
  {
    public override IList<SnapLine> SnapLines
    {
      get
      {
        IList<SnapLine> snapLines = base.SnapLines;

        //Create snaplines according to the horizontal lines drawn in the panel:
        //Always create top/bottom pairs so the top edge and bottom edge of a control can snap.
        snapLines.Add(new SnapLine(SnapLineType.Top, 10));
        snapLines.Add(new SnapLine(SnapLineType.Bottom, 10));
        snapLines.Add(new SnapLine(SnapLineType.Top, 30));
        snapLines.Add(new SnapLine(SnapLineType.Bottom, 30));

        //Same for the vertical lines:
        snapLines.Add(new SnapLine(SnapLineType.Left, 10));
        snapLines.Add(new SnapLine(SnapLineType.Right, 10));
        snapLines.Add(new SnapLine(SnapLineType.Left, 30));
        snapLines.Add(new SnapLine(SnapLineType.Right, 30));

        return snapLines;
      }
    }
  }
}