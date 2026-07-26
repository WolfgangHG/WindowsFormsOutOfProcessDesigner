using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace SnapLineBasicsControl
{
  [Designer("SnapLineBasics.Designer.Server.MyPanelDesigner")]
  public class MyPanel : UserControl
  {

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);

      //Horizontal:
      e.Graphics.DrawLine (Pens.Gray, 10, 10, this.Width - 10, 10);
      e.Graphics.DrawLine(Pens.Gray, 10, 30, this.Width - 10, 30);

      //Vertical:
      e.Graphics.DrawLine(Pens.Gray, 10, 10, 10, this.Height - 10);
      e.Graphics.DrawLine(Pens.Gray, 30, 10, 30, this.Height - 10);
    }
  }
}
