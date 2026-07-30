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

namespace MySampleApp
{
  /// <summary>
  /// Contains the <see cref="MyInnerUserControl"/> and some more controls.
  /// Copies snaplines from the inner control and adds more.
  /// </summary>
  [Designer(Designers.DESIGNER_SNAPLINE_FROM_CHILD_BY_DECLARATION)]
  //Snaplines to copy from the child control: all base lines and left/right snaplines
  [DesignerSnapLineSource(ChildControl = nameof(myInnerUserControl1), SnapLines = SnapLineTypeToCopy.Baseline | SnapLineTypeToCopy.Left | SnapLineTypeToCopy.Right)]
  //Additionally: baseline of label
  [DesignerSnapLineSource(ChildControl = nameof(label1), SnapLines = SnapLineTypeToCopy.Baseline)]
  //Left line of checkbox (adds an additional column of controls)
  [DesignerSnapLineSource(ChildControl = nameof(checkBox1), SnapLines = SnapLineTypeToCopy.Left)]
  public partial class MyOuterUserControl : UserControl
  {
    public MyOuterUserControl()
    {
      InitializeComponent();
    }
  }
}
