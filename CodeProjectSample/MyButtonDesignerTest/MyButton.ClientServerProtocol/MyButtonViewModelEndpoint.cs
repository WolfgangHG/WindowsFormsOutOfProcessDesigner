using System.Composition;
using Microsoft.DotNet.DesignTools.Protocol.DataPipe;
using Microsoft.DotNet.DesignTools.Protocol.Endpoints;

namespace MyButton.ClientServerProtocol
{
  [Shared]
  [ExportEndpoint]
  public class MyButtonViewModelEndpoint :
         Endpoint<MyButtonViewModelRequest, MyButtonViewModelResponse>
  {
    public override string Name => EndpointNames.MyButtonViewModel;

    protected override MyButtonViewModelRequest
                       CreateRequest(IDataPipeReader reader)
        => new(reader);

    protected override MyButtonViewModelResponse
                       CreateResponse(IDataPipeReader reader)
        => new(reader);
  }
}