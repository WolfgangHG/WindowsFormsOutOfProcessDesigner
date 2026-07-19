using Microsoft.DotNet.DesignTools.ViewModels;
using System;
using System.Diagnostics.CodeAnalysis;
using MyButton.ClientServerProtocol;
using MyButtonControl;

namespace MyButton.Designer.Server
{
  internal partial class MyButtonViewModel : ViewModel
  {
    public MyButtonViewModel(IServiceProvider provider) : base(provider)
    {
    }

    public MyButtonViewModelResponse Initialize(object myProperty)
    {
      MyProperty = new MyType(myProperty.ToString());
      return new MyButtonViewModelResponse(this, MyProperty);
    }

    [AllowNull]
    public MyType MyProperty { get; set; }
  }
}