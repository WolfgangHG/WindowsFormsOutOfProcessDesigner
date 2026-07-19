using Microsoft.DotNet.DesignTools.Protocol.DataPipe;
using Microsoft.DotNet.DesignTools.Protocol;
using Microsoft.DotNet.DesignTools.Protocol.Endpoints;
using System;

namespace MyButton.ClientServerProtocol
{
  public class MyButtonViewModelRequest : Request
  {
    public SessionId SessionId { get; private set; }
    public object? MyPropertyEditorProxy { get; private set; }

    public MyButtonViewModelRequest() { }

    public MyButtonViewModelRequest(SessionId sessionId, object? myProxy)
    {
      SessionId = sessionId.IsNull ?
      throw new ArgumentNullException(nameof(sessionId)) : sessionId;
      MyPropertyEditorProxy = myProxy;
    }

    public MyButtonViewModelRequest(IDataPipeReader reader) : base(reader) { }

    protected override void ReadProperties(IDataPipeReader reader)
    {
      SessionId = reader.ReadSessionId(nameof(SessionId));
      MyPropertyEditorProxy = reader.ReadObject(nameof(MyPropertyEditorProxy));
    }

    protected override void WriteProperties(IDataPipeWriter writer)
    {
      writer.Write(nameof(SessionId), SessionId);
      writer.WriteObject(nameof(MyPropertyEditorProxy), MyPropertyEditorProxy);
    }
  }
}