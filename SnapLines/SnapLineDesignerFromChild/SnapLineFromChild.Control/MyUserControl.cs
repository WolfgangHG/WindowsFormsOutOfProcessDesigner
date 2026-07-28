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
  [Designer("SnapLineFromChild.Designer.Server.SnapLineFromChildDesigner")]
  public partial class MyUserControl : UserControl
  {
    public MyUserControl()
    {
      InitializeComponent();
    }

    //Properties for all child controls that are source for snap lines

    internal RadioButton RadioButton1 => this.radioButton1;
    internal RadioButton RadioButton2 => this.radioButton2;
    internal RadioButton RadioButton3 => this.radioButton3;
    internal Button Button => this.button1;
    internal TextBox TextBox => this.textBox1;
  }
}
