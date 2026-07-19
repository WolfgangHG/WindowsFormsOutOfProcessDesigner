using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace MyButton.Designer.Client
{
  public class MyButtonEditor : UITypeEditor
  {

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        => UITypeEditorEditStyle.Modal;

    public override object? EditValue(
        ITypeDescriptorContext context,
        IServiceProvider provider,
        object? value)
    {
      if (provider is null)
      {
        return value;
      }


      Form myTestForm;
      myTestForm = new Form();
      var editorService =
          provider.GetRequiredService<IWindowsFormsEditorService>();
      editorService.ShowDialog(myTestForm);

      MyButtonViewModel viewModelClient =
                        MyButtonViewModel.Create(provider, "test");
      return viewModelClient.MyProperty;
    }
  }
}