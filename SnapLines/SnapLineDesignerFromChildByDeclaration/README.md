# SnapLines, part 3

This is the third part of a series of samples about the handling of snaplines in the WinForms Out-of-process designer.

See [part 1](../SnapLineDesignerBasics/README.md) for the basic concepts.


This project elevates the approach to copy snaplines from a child control of a `UserControl` (see [part 2](../SnapLineDesignerFromChild/README.md)) to a declarative solution.

# Overview

If you apply the sample (see [part 2](../SnapLineDesignerFromChild/README.md)), you have to create a designer implementation for each `UserControl` where
you want to show snaplines of child controls on consumer forms. This creates a lot of non-resusable code.

This sample shows a declarative approach: on the `UserControl`, you define the designer and an attribute `DesignerSnapLineSource`, which defines snap line sources
and the snap lines that shall be copied from this control.

The sample `UserControl` of part 2 could be defined like this:

```c#
[Designer(Designers.DESIGNER_SNAPLINE_FROM_CHILD_BY_DECLARATION)]
[DesignerSnapLineSource(ChildControl = nameof(radioButton1), SnapLines = SnapLineTypeToCopy.Baseline)]
[DesignerSnapLineSource(ChildControl = nameof(radioButton2), SnapLines = SnapLineTypeToCopy.Baseline)]
[DesignerSnapLineSource(ChildControl = nameof(radioButton3), SnapLines = SnapLineTypeToCopy.Baseline)]
[DesignerSnapLineSource(ChildControl = nameof(button1), SnapLines = SnapLineTypeToCopy.Left | SnapLineTypeToCopy.Right)]
[DesignerSnapLineSource(ChildControl = nameof(textBox1), SnapLines = SnapLineTypeToCopy.Left)]
public partial class MyUserControl : UserControl
{
  public MyInnerUserControl()
  {
    InitializeComponent();
  }
}
```

So you have one designer implementation (here: `SnapLineFromChildByDeclaration.Designer.Server.SnapLineFromChildByDeclarationDesigner`),
and can use it on a unlimited number of controls in different projects.

Conceptually, you could also nest `UserControls` with custom snaplines and provide the snaplines of the inner control to the outer control.
But this requires more work in the designer implementation, see below.


# The solution structure

First you should read the [full designer sample with custom properties](../../CodeProjectSample/README.md) to understand the project
structure and the Nuget package handling. But our approach is simpler, we don't need the .NET 4.8 "Client" project, and thus the "Protocol" project
is also not necessary.

Also, I renamed the `.Control` project to `.Common` here, as it contains no control, but only a attribute class.

My sample consists of three projects:
* SnapLineFromChildByDeclaration.Common: this library contains the attribute `DesignerSnapLineSourceAttribute`, an enum the duplicates
`SnapLineType` enum, and a class `Designers` that simply defines the designer class name.
* SnapLineFromChildByDeclaration.Designer.Server: the WinForms designer project (server side). It contains the design server implementation.
* SnapLineFromChildByDeclaration.Package: creates a Nuget Package containing the `SnapLineFromChildByDeclaration.Common` and `SnapLineFromChildByDeclaration.Designer.Server` dlls.

There is also a sample app project `MySampleApp` which contains two user controls that expose snaplines of child controls.

# Project "SnapLineFromChildByDeclaration.Common"

It mainly contains a class `DesignerSnapLineSourceAttribute`:

```c#
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DesignerSnapLineSourceAttribute : Attribute
{
  public string ChildControl
  {
    get;
    set;
  }

  public SnapLineTypeToCopy SnapLines
  {
    get;
    set;
  }
}
```

This attribute can be set multiple times on a `UserControl` class. It defines a child control whose name (member variable name,
**not** control name - use the `nameof` operator to set the field name) whose snap lines are to be copied (property `SnapLines`). 
This property can have multiple enum values. 

As the original `SnapLineType` enum has no "Flags" attribute and also exists in two places (`System.Windows.Forms.Design.Behavior` and 
`Microsoft.DotNet.DesignTools.Designers.Behaviors`), I duplicated it as `SnapLineTypeToCopy`:

```c#

[Flags]
public enum SnapLineTypeToCopy
{
  None = 0,
  Top = 1,
  Bottom = 2,
  Left = 4,
  Right = 8,
  Horizontal = 16,
  Vertical = 32,
  Baseline = 64
}
```

I also copied `Horizontal` and `Vertical` values though I don't think they are helpful.



# Project "SnapLineFromChildByDeclaration.Designer.Server"

It contains the designer implementation.

The core code is this:

```c#
internal partial class SnapLineFromChildByDeclarationDesigner : ControlDesigner
{
  public override IList<SnapLine> SnapLines
{
  get
  {
    IList<SnapLine> snaplines = base.SnapLines;

    try
    {
      Control controlRoot = base.Control;
      //Find attribute "SnapLineSource" - might be set multiple times:
      IEnumerable<DesignerSnapLineSourceAttribute> attributes = controlRoot.GetType().GetCustomAttributes<DesignerSnapLineSourceAttribute>();
       
      //If this designer is used, the control has to define the attribute at least once.
      if (attributes.Count() == 0)
      {
        throw new InvalidOperationException($"Type {controlRoot.GetType().FullName} does not define an attribute {nameof(DesignerSnapLineSourceAttribute)}");
      }
      foreach (DesignerSnapLineSourceAttribute snapLineSource in attributes)
      {
        //Find the member variable in the control:
        FieldInfo fieldForControl = controlRoot.GetType().GetField(snapLineSource.ChildControl, BindingFlags.Instance | BindingFlags.NonPublic);
        if (fieldForControl == null)
        {
          throw new InvalidOperationException($"Type {controlRoot.GetType().FullName}: found no field / member variable \"{snapLineSource.ChildControl}\".");
        }

        Control controlChild = (Control)fieldForControl.GetValue(controlRoot);

        //Create designer
        IDesigner designer = TypeDescriptor.CreateDesigner(controlChild, typeof(IDesigner));;
        
        designer.Initialize(controlChild);

        //Fetch snapLines of the designer. In the "old" designer, this must be done by reflection:
        IList<SnapLine> listSnapLines = CopySnapLines(designer);
        
        //Now copy the snaplines that are defines in the attribute:
        foreach (SnapLine line in listSnapLines)
        {
          //Copy snaplines
          ...
        }
        finally
        {
          //Dispose the child control designer:
          if (designer != null)
          {
            designer.Dispose();
          }
        }
      }
    }
    catch (Exception ex)
    {
      this.DisplayError(ex.ToString());
    }

    return snaplines;
  }
}
}
```

* The code looks for `DesignerSnapLineSource` attributes defined on the root control.
* For each attribute, it tries to find the child control: it uses reflection to find a field with that name.
* Then it creates a designer for this child and initializes it.
* Now, snap lines are copied from this child control. The code uses reflection, as the child control designer returns a different
`SnapLine` class, see code in [part 2](../SnapLineDesignerFromChild/README.md) sample.


The code for copying the snap line uses a helper function `ToSnapLineTypeToCopy` that coverts a `Microsoft.DotNet.DesignTools.Designers.Behaviors.SnapLineType` to
the value of my own enum `SnapLineTypeToCopy`:

```c#
private static SnapLineTypeToCopy ToSnapLineTypeToCopy(SnapLineType snapLineType)
{
  return _snapLineType switch
  {
    SnapLineType.Top => SnapLineTypeToCopy.Top,
    SnapLineType.Left => SnapLineTypeToCopy.Left,
    SnapLineType.Right => SnapLineTypeToCopy.Right,
    SnapLineType.Bottom => SnapLineTypeToCopy.Bottom,
    SnapLineType.Baseline => SnapLineTypeToCopy.Baseline,
    SnapLineType.Horizontal => SnapLineTypeToCopy.Horizontal,
    SnapLineType.Vertical => SnapLineTypeToCopy.Vertical,
    _ => throw new NotImplementedException($"Unsupported enum value: {snapLineType}")

  };
}
```

The code for copying a snap line looks like this:

```c#
SnapLineType snapLineType = line.SnapLineType;

//Convert the SnapLineType to the enum that is defined on the attribute.
SnapLineTypeToCopy snapLineTypesToCopy = ToSnapLineTypeToCopy(snapLineType);
//Does the attribute define this snap line type?
if (snapLineSource.SnapLines.HasFlag(snapLineTypesToCopy) == true)
{
  int offset = line.Offset;
  SnapLinePriority snapLinePriority = line.Priority;

  //Calculate full offset of the child control inside nested controls:
  if (snapLineTypesToCopy == SnapLineTypeToCopy.Top || snapLineTypesToCopy == SnapLineTypeToCopy.Baseline ||
    snapLineTypesToCopy == SnapLineTypeToCopy.Bottom)
  {
    //Horizontal line: calculate offset in parent control
  }
  else if (snapLineTypesToCopy == SnapLineTypeToCopy.Left ||
    snapLineTypesToCopy == SnapLineTypeToCopy.Right)
  {
    //Vertical line: calculate offset in parent control
  }
  else
  {
    //Not supported?
    throw new NotSupportedException($"SnapLineType {snapLineType} not supported.");
  }

  SnapLine snapLine = new SnapLine(snapLineType, offset, snapLinePriority);
  snaplines.Add(snapLine);

  //Left snapline: also add a "right" SnapLine.
  //Otherwise they are not displayed of a control is moved from the right to the snapline position.
  if (snapLineType == SnapLineType.Left)
  {
    snapLine = new SnapLine(SnapLineType.Right, offset, snapLinePriority);
    snaplines.Add(snapLine);
  }
  else if (snapLineType == SnapLineType.Right)
  {
    //Duplicate right snapline:
    snapLine = new SnapLine(SnapLineType.Left, offset, snapLinePriority);
    snaplines.Add(snapLine);
  }
}
```

The tricky part here is to calculate the offset: a child control can be placed on another container (`Panel` or `GroupBox`), 
so that the x or y location in this container most be added to the snap line offset. This is done in a loop until the current parent control is our root control:
```c#
int offset = line.Offset;

Control controlCurrent = controlChild;
do
{
  offset += controlCurrent.Location.Y;

  controlCurrent = controlCurrent.Parent;
}
while (controlCurrent != controlRoot);
```

For vertical lines, the same must be done with the X location of all child controls.

Finally, the code duplicates vertical snap lines (`Left` and `Right`), see explanation in part 2 of the sample.


## Copying snaplines from controls with a `SnapLineFromChildByDeclarationDesigner`

So you created a control (in my sample it is named `MyInnerUserControl`) which declares this snap line designer. 
Somewhere else, you create another `UserControl` (e.g. `MyOuterUserControl`) that contains `MyInnerUserControl` and further controls.
And now, you want to return the snap lines of the inner control and some snap lines from the additional controls.

In my sample, the declaration is this:
```c#
[Designer(Designers.DESIGNER_SNAPLINE_FROM_CHILD_BY_DECLARATION)]
//Snaplines to copy from the child control: all base lines and left/right snaplines
[DesignerSnapLineSource(ChildControl = nameof(myInnerUserControl1), SnapLines = SnapLineTypeToCopy.Baseline | SnapLineTypeToCopy.Left | SnapLineTypeToCopy.Right)]
//Additionally: baseline of label
[DesignerSnapLineSource(ChildControl = nameof(label1), SnapLines = SnapLineTypeToCopy.Baseline)]
//Left line of checkbox (adds an additional column of controls)
[DesignerSnapLineSource(ChildControl = nameof(checkBox1), SnapLines = SnapLineTypeToCopy.Left)]
public partial class MyOuterUserControl : UserControl
```

When launching the designer of the form that uses `MyOuterUserControl`, you will run into an error because 
`TypeDescriptor.CreateDesigner` returns null. I found that you have to handle designers differently that follow the new approach:

```c#
IDesigner designer = null;
bool mustResetSite = false;

try
{
  //Check whether the control (oder a parent control) defines the "SnapLineFromChildByDeclarationDesigner".
  IEnumerable<DesignerAttribute> designerAttributes = controlChild.GetType().GetCustomAttributes<DesignerAttribute>();
  DesignerAttribute designerAttributeFirst = designerAttributes.FirstOrDefault();

  bool isOldDesigner;
  if (designerAttributeFirst != null && designerAttributeFirst.DesignerTypeName == this.GetType().FullName)
  {
    isOldDesigner = false;
    designer = new SnapLineFromChildByDeclarationDesigner();
    //We have to set the "Site" to the child control on order to make the following "Initialize" work.
    if (controlChild.Site == null)
    {
      controlChild.Site = base.Control.Site;
      mustResetSite = true;
    }
  }
  else
  {
    isOldDesigner = true;
    designer = TypeDescriptor.CreateDesigner(controlChild, typeof(IDesigner));
  }
  
  designer.Initialize(controlChild);
  
  //Fetch snapLines of the designer. In the "old" designer, this must be done by reflection:
  IList<SnapLine> listSnapLines;
  if (isOldDesigner == true)
  {
    listSnapLines = CopySnapLines(designer);
  }
  else
  {
    //"New" designer: We can simply fetch the property.
    listSnapLines = ((SnapLineFromChildByDeclarationDesigner)designer).SnapLines;
  }
}
finally
{
  //Dispose the child control designer:
  if (designer != null)
  {
    designer.Dispose();
  }
  //Also reset the Site, otherwise the child control would be added to the parent control.
  if (mustResetSite == true)
  {
    controlChild.Site = null;
  }
}
```

* The designer instance must be created by yourself:
  ```c#
  designer = new SnapLineFromChildByDeclarationDesigner();
  ```
* I also set the `Site` property of the child control - otherwise the call to `designer.Initialize()` would have failed:
  ```c#
  controlChild.Site = base.Control.Site;
  ```
  Note: this is a hack that I found by try and error, don't know whether this is correct ;-).
  I also do this only if the child control does not have a `Site` already - this check is probably not necessary.
* Now, you can copy the snap lines, which is rather simple here:
  ```c#
  listSnapLines = ((SnapLineFromChildByDeclarationDesigner)designer).SnapLines;
  ```
* In the end, I dispose the designer.
* And if in step 2 a `Site` was set (which probably happens always), it must be reset again. If you don't do it, an instance of the child control would be added
  to the current control/form.

Note: if you want to grab snap lines from controls that define other "new" out-of-process designers, you have to add code
for creating the designer instance. 

# Project "SnapLineFromChildByDeclaration.Package"

This project creates the Nuget Package containing the files `SnapLineFromChildByDeclaration.Common.dll` and `SnapLineFromChildByDeclaration.Designer.Server.dll`.

In "Nuget.config" of the solution, I defined the "bin\Debug" directory as package source for the sample app.

# The sample app

The sample app contains two user controls: 

* `MyInnerUserControl` is the well known control from part 2, which has a lot of child controls whose snap lines are exposed.
* `MyOuterUserControl` is another user control that contains `MyInnerUserControl` and additional controls. The snap lines of
the inner user control and snap lines from the additional controls are exposed.



# Building the sample

This is a bit tricky. When you first open the sample, there will be a Nuget error that package "SnapLineFromChildByDeclaration.Package" cannot be resolved.
So first built the project "SnapLineFromChildByDeclaration.Package", then build the project "MySampleApp".

If you make modifications to code in either "SnapLineFromChildByDeclaration.Common" or "SnapLineFromChildByDeclaration.Designer.Server", it is a bit more tricky:

* Build the project "SnapLineFromChildByDeclaration.Package" 
* close open designer of "Form1" in project "MySampleApp".
* check in task manager that no process "DesignToolsServer.exe" is running. If you find one, kill it.
* In your local nuget cache, kill the directory "%USERPROFILE%\.nuget\packages\snaplinefromchildbydeclaration.package\1.0.0"
* Build the project "MySampleApp". You might also check that the Nuget cache now contains recent versions of "SnapLineFromChildByDeclaration.Common.dll" 
(in "lib\net9.0\") and "SnapLineFromChildByDeclaration.Designer.Server.dll" (in "lib\net9.0\Design\WinForms\Server")

Now you can open "Form1.cs" again in designer.