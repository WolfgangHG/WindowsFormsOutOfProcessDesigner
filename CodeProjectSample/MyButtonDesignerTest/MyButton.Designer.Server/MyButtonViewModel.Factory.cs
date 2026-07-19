using Microsoft.DotNet.DesignTools.ViewModels;
using System;
using MyButton.ClientServerProtocol;

namespace MyButton.Designer.Server
{
  internal partial class MyButtonViewModel
  {
    [ExportViewModelFactory(ViewModelNames.MyButtonViewModel)]
    private class Factory : ViewModelFactory<MyButtonViewModel>
    {
      protected override MyButtonViewModel CreateViewModel
                                 (IServiceProvider provider)
          => new(provider);
    }
  }
}