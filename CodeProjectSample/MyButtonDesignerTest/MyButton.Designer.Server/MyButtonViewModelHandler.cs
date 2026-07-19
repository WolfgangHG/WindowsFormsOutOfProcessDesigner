using Microsoft.DotNet.DesignTools.Protocol.Endpoints;
using MyButton.ClientServerProtocol;

namespace MyButton.Designer.Server
{
  [ExportRequestHandler(EndpointNames.MyButtonViewModel)]
  public class MyButtonViewModelHandler :
         RequestHandler<MyButtonViewModelRequest, MyButtonViewModelResponse>
  {
    public override MyButtonViewModelResponse HandleRequest
                    (MyButtonViewModelRequest request)
    {
      var designerHost = GetDesignerHost(request.SessionId);

      var viewModel = CreateViewModel<MyButtonViewModel>(designerHost);

      return viewModel.Initialize(request.MyPropertyEditorProxy!);
    }
  }
}