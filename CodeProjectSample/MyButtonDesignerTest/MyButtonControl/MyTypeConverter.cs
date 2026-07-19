using System;
using System.ComponentModel;
using System.Globalization;

namespace MyButtonControl
{
  internal class MyTypeConverter : TypeConverter
  {
    public override bool CanConvertTo
           (ITypeDescriptorContext context, Type destinationType)
    {
      return true;
    }

    public override bool CanConvertFrom
           (ITypeDescriptorContext context, Type sourceType)
    {
      return true;
    }

    public override object ConvertFrom
           (ITypeDescriptorContext context, CultureInfo culture, object value)
    {
      if (value is null)
      {
        return string.Empty;
      }
      return new MyType(value.ToString());
    }

    public override object ConvertTo(ITypeDescriptorContext context,
                    CultureInfo culture, object value, Type destinationType)
    {
      return ((MyType)value)?.AnotherMyProperty;
    }
  }
}