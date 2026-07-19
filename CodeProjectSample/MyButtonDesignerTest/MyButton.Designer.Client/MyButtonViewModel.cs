using System;
using Microsoft.DotNet.DesignTools.Client.Proxies;
using Microsoft.DotNet.DesignTools.Client;
using Microsoft.DotNet.DesignTools.Client.Views;
using MyButton.ClientServerProtocol;

namespace MyButton.Designer.Client
{
  internal partial class MyButtonViewModel : ViewModelClient
  {
    [ExportViewModelClientFactory(ViewModelNames.MyButtonViewModel)]
    private class Factory : ViewModelClientFactory<MyButtonViewModel>
    {
      protected override MyButtonViewModel CreateViewModelClient
                         (ObjectProxy? viewModel)
          => new(viewModel);
    }

    private MyButtonViewModel(ObjectProxy? viewModel)
        : base(viewModel)
    {
      if (viewModel is null)
      {
        throw new NullReferenceException(nameof(viewModel));
      }
    }

    public static MyButtonViewModel Create(
        IServiceProvider provider,
        object? templateAssignmentProxy)
    {
      var session = provider.GetRequiredService<DesignerSession>();
      var client = provider.GetRequiredService<IDesignToolsClient>();

      var createViewModelEndpointSender =
          client.Protocol.GetEndpoint
                 <MyButtonViewModelEndpoint>().GetSender(client);

      var response =
          createViewModelEndpointSender.SendRequest
                    (new MyButtonViewModelRequest(session.Id,
              templateAssignmentProxy));
      var viewModel = (ObjectProxy)response.ViewModel!;

      var clientViewModel = provider.CreateViewModelClient<MyButtonViewModel>
                                                                 (viewModel);

      return clientViewModel;
    }

    public object? MyProperty
    {
      get => ViewModelProxy?.GetPropertyValue(nameof(MyProperty));
      set => ViewModelProxy?.SetPropertyValue(nameof(MyProperty), value);
    }
  }
}