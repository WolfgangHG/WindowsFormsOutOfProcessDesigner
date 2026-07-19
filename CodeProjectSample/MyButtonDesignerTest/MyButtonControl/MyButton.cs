using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Windows.Forms;

namespace MyButtonControl
{
  [Designer("MyButtonDesigner"),
   ComplexBindingProperties("DataSource")]
  public class MyButton : Button
  {
    public MyType MyProperty { get; set; }
  }
}
