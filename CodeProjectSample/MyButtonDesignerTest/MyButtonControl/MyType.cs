using System.ComponentModel;
using System.Drawing.Design;

namespace MyButtonControl
{
  [TypeConverter(typeof(MyTypeConverter))]
  [Editor("MyButtonEditor", typeof(UITypeEditor))]
  public class MyType
  {
    public string AnotherMyProperty { get; set; }

    public MyType(string value)
    {
      AnotherMyProperty = value;
    }
  }
}