# WindowsForms of-of-process Designer

This repository contains know-how about the .NET8 WinForms Out-of-process designer.


## Additional resources

For basic understanding, you should start with **two blog posts** of the designer developer:
* [State of the Windows Forms Designer for .NET Applications](https://devblogs.microsoft.com/dotnet/state-of-the-windows-forms-designer-for-net-applications/) (dated 2022-01-13)
* [Custom Controls for WinForm’s Out-Of-Process Designer](https://devblogs.microsoft.com/dotnet/custom-controls-for-winforms-out-of-process-designer/) (dated 2022-12-05)

Then there are two **Microsoft code samples**: [TileRepeater_Medium](https://github.com/microsoft/winforms-designer-extensibility/tree/main/Samples/TypeEditor/Dotnet/TileRepeater_Medium) 
and [TileRepeater_Simplified](https://github.com/microsoft/winforms-designer-extensibility/tree/main/Samples/TypeEditor/Dotnet/TileRepeater_Simplified).

The "winforms-designer-extensibility" repository contains also a **"Project Template" project** [TypeEditor]([https://github.com/microsoft/winforms-designer-extensibility/tree/main/Templates/TypeEditor])
that you have to build yourself and register it in Visual Studio. 
Note: when building the template, there is a change necessary to avoid an error: in "prepareTemplates.bat" change the call
`dotnet pack` to `dotnet pack -c Debug` (.NET 8 creates a "Release" build by default: https://github.com/microsoft/winforms-designer-extensibility/issues/30)

After having built the template, restart Visual Studio and create a new project. An entry "Windows .NET Custom Type Editor" should appear.
Now create a project. 
There should be another problem to fix: "Directory.Build.targets" requires the package reference to have a version number. It works to use `*`:
```xml
<ItemGroup Condition="'$(UseCustomTypeEditorTest)' == 'true'">
  <PackageReference Include="CustomTypeEditorTest.Package" Version="*"/>
</ItemGroup>
```


## Content of this repository

* A restored copy of the [CodeProject sample "Writing Custom Control in .NET 6"](CodeProjectSample/README.md)
* A series of samples for using [Snaplines](Snaplines/README.md)
