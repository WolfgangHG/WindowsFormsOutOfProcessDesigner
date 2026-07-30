using SnapLineFromChildByDeclaration.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnapLineFromChild.Control
{
  [Designer(Designers.DESIGNER_SNAPLINE_FROM_CHILD_BY_DECLARATION)]
  [DesignerSnapLineSource(ChildControl = nameof(radioButton1), SnapLines = SnapLineTypeToCopy.Baseline)]
  [DesignerSnapLineSource(ChildControl = nameof(radioButton2), SnapLines = SnapLineTypeToCopy.Baseline)]
  [DesignerSnapLineSource(ChildControl = nameof(radioButton3), SnapLines = SnapLineTypeToCopy.Baseline)]
  [DesignerSnapLineSource(ChildControl = nameof(button1), SnapLines = SnapLineTypeToCopy.Left | SnapLineTypeToCopy.Right)]
  [DesignerSnapLineSource(ChildControl = nameof(textBox1), SnapLines = SnapLineTypeToCopy.Left)]
  public partial class MyInnerUserControl : UserControl
  {
    public MyInnerUserControl()
    {
      InitializeComponent();
    }
  }
}
