using Microsoft.DotNet.DesignTools.Protocol.DataPipe;
using Microsoft.DotNet.DesignTools.Protocol.Endpoints;
using System;
using System.Diagnostics.CodeAnalysis;

namespace MyButton.ClientServerProtocol
{
  public class MyButtonViewModelResponse : Response
  {
    [AllowNull]
    public object ViewModel { get; private set; }

    [AllowNull]
    public object MyProperty { get; private set; }

    public MyButtonViewModelResponse() { }

    public MyButtonViewModelResponse(object viewModel, object myProperty)
    {
      ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
      MyProperty = myProperty;
    }

    public MyButtonViewModelResponse(object viewModel)
    {
      ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    public MyButtonViewModelResponse(IDataPipeReader reader) : base(reader) { }

    protected override void ReadProperties(IDataPipeReader reader)
    {
      ViewModel = reader.ReadObject(nameof(ViewModel));
    }

    protected override void WriteProperties(IDataPipeWriter writer)
    {
      writer.WriteObject(nameof(ViewModel), ViewModel);
      writer.WriteObject(nameof(MyProperty), MyProperty);
    }
  }
}