using Microsoft.DotNet.DesignTools.Designers;
using Microsoft.DotNet.DesignTools.Designers.Behaviors;
using Microsoft.DotNet.DesignTools.Protocol.DataPipe.Serialization;
using SnapLineFromChildByDeclaration.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SnapLineFromChildByDeclaration.Designer.Server
{
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

            //Because the designer might be initialized from different sources, we cannot define the variable with "using", but have to dispose it ourselves.
            IDesigner designer = null;
            bool mustResetSite = false;

            try
            {
              //Check whether the control (oder a parent control) defines the "SnapLineFromChildByDeclarationDesigner".
              //If yes: "TypeDescriptor.CreateDesigner" would return null.
              //So we have to create this designer directly.
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
                if (designer == null)
                {
                  throw new InvalidOperationException($"Could not create a designer for child control {snapLineSource.ChildControl}.");
                }
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

              //Now copy the snaplines that are defines in the attribute:
              foreach (SnapLine line in listSnapLines)
              {
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
                    //Horizontal line:

                    //Add the y location of all parent controls before the (current) root control:
                    Control controlCurrent = controlChild;
                    do
                    {
                      offset += controlCurrent.Location.Y;

                      controlCurrent = controlCurrent.Parent;
                    }
                    while (controlCurrent != controlRoot);
                  }
                  else if (snapLineTypesToCopy == SnapLineTypeToCopy.Left ||
                    snapLineTypesToCopy == SnapLineTypeToCopy.Right)
                  {
                    //Vertical line:

                    //Add the x location of all parent controls before the (current) root control:
                    Control controlCurrent = controlChild;
                    do
                    {
                      offset += controlCurrent.Location.X;

                      controlCurrent = controlCurrent.Parent;
                    }
                    while (controlCurrent != controlRoot);
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
          }
        }
        catch (Exception ex)
        {
          this.DisplayError(ex.ToString());
        }

        return snaplines;
      }
    }


    /// <summary>
    /// Convert the WinFormsDesigner enum "SnapLineType" to the "SnapLineTypeToCopy" enum.
    /// </summary>
    /// <param name="snapLineType"></param>
    /// <returns></returns>
    private static SnapLineTypeToCopy ToSnapLineTypeToCopy(SnapLineType snapLineType)
    {
      return snapLineType switch
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

    /// <summary>
    /// Copies SnapLines from the old Designer to the "SnapLine" class of the Out-of-process designer.
    /// </summary>
    private static List<SnapLine> CopySnapLines(IDesigner _designer)
    {
      //Invoke the property "SnapLines" from the child control designer:
      PropertyInfo propSnaplInes = _designer.GetType().GetProperty("SnapLines", BindingFlags.Instance | BindingFlags.Public);
      object objSnapLines = propSnaplInes.GetValue(_designer);
      IList listSnapLines = (IList)objSnapLines;

      //Now copy the SnapLine objects property by property:
      List<SnapLine> listSnapLinesNew = new List<SnapLine>();
      foreach (object line in listSnapLines)
      {
        PropertyInfo propInfoType = line.GetType().GetProperty("SnapLineType", BindingFlags.Instance | BindingFlags.Public);
        object objType = propInfoType.GetValue(line);
        SnapLineType snapLineType = Enum.Parse<SnapLineType>(objType.ToString());


        PropertyInfo propInfoOffset = line.GetType().GetProperty("Offset", BindingFlags.Instance | BindingFlags.Public);
        int offset = (int)propInfoOffset.GetValue(line);

        PropertyInfo propInfoPriority = line.GetType().GetProperty("Priority", BindingFlags.Instance | BindingFlags.Public);
        object objPrioriy = propInfoPriority.GetValue(line);
        //Convert to Enum to String and back and convert it this way:
        SnapLinePriority snapLinePriority = Enum.Parse<SnapLinePriority>(objPrioriy.ToString());

        listSnapLinesNew.Add(new SnapLine(snapLineType, offset, snapLinePriority));
      }

      return listSnapLinesNew;
    }
  }
}