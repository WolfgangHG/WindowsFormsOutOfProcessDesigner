using Microsoft.DotNet.DesignTools.Designers;
using Microsoft.DotNet.DesignTools.Designers.Behaviors;
using SnapLineFromChild.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;

namespace SnapLineFromChild.Designer.Server
{
  internal partial class SnapLineFromChildDesigner : ControlDesigner
  {
    public override IList<SnapLine> SnapLines
    {
      get
      {
        IList<SnapLine> snapLines = base.SnapLines;

        MyUserControl userControl = (MyUserControl)this.Control;
        try
        {
          //Now create a designer for each child control and copy the requestes snap lines:
          #region Radiobutton 1
          IDesigner designer = TypeDescriptor.CreateDesigner(userControl.RadioButton1, typeof(IDesigner));
          if (designer == null)
          {
            throw new InvalidOperationException($"Could not create a Designer for ChildControl {userControl.RadioButton1}.");
          }

          designer.Initialize(userControl.RadioButton1);

          IList<SnapLine> snapLinesChild = CopySnapLines(designer);

          foreach (SnapLine snapLine in snapLinesChild)
          {
            //Radiobutton: we copy only the baseline.
            if (snapLine.SnapLineType == SnapLineType.Baseline)
            {
              int offset = snapLine.Offset + userControl.RadioButton1.Location.Y;
              SnapLine snapLineNew = new SnapLine(snapLine.SnapLineType, offset, snapLine.Filter, snapLine.Priority);
              snapLines.Add(snapLineNew);
            }
          }
          #endregion

          #region Radiobutton 2
          designer = TypeDescriptor.CreateDesigner(userControl.RadioButton2, typeof(IDesigner));
          if (designer == null)
          {
            throw new InvalidOperationException($"Could not create a Designer for ChildControl {userControl.RadioButton2}.");
          }


          designer.Initialize(userControl.RadioButton2);

          snapLinesChild = CopySnapLines(designer);

          foreach (SnapLine snapLine in snapLinesChild)
          {
            //Radiobutton: we copy only the baseline.
            if (snapLine.SnapLineType == SnapLineType.Baseline)
            {
              int offset = snapLine.Offset + userControl.RadioButton2.Location.Y;
              SnapLine snapLineNew = new SnapLine(snapLine.SnapLineType, offset, snapLine.Filter, snapLine.Priority);
              snapLines.Add(snapLineNew);
            }
          }
          #endregion

          #region Radiobutton 3
          designer = TypeDescriptor.CreateDesigner(userControl.RadioButton3, typeof(IDesigner));
          if (designer == null)
          {
            throw new InvalidOperationException($"Could not create a Designer for ChildControl {userControl.RadioButton3}.");
          }


          designer.Initialize(userControl.RadioButton3);

          snapLinesChild = CopySnapLines(designer);

          foreach (SnapLine snapLine in snapLinesChild)
          {
            //Radiobutton: we copy only the baseline.
            if (snapLine.SnapLineType == SnapLineType.Baseline)
            {
              int offset = snapLine.Offset + userControl.RadioButton3.Location.Y;
              SnapLine snapLineNew = new SnapLine(snapLine.SnapLineType, offset, snapLine.Filter, snapLine.Priority);
              snapLines.Add(snapLineNew);
            }
          }
          #endregion


          #region Button
          designer = TypeDescriptor.CreateDesigner(userControl.Button, typeof(IDesigner));
          if (designer == null)
          {
            throw new InvalidOperationException($"Could not create a Designer for ChildControl {userControl.Button}.");
          }


          designer.Initialize(userControl.Button);

          snapLinesChild = CopySnapLines(designer);

          foreach (SnapLine snapLine in snapLinesChild)
          {
            //Button: we copy left and right Snapline:
            if (snapLine.SnapLineType == SnapLineType.Left || snapLine.SnapLineType == SnapLineType.Right)
            {
              //Increase offset by x location of the button:
              int offset = snapLine.Offset + userControl.Button.Location.X;
              SnapLine snapLineNew = new SnapLine(snapLine.SnapLineType, offset, snapLine.Filter, snapLine.Priority);
              snapLines.Add(snapLineNew);
              //Also create a partner snapline (left + right).
              if (snapLine.SnapLineType == SnapLineType.Left)
              {
                SnapLine snapLineNew2 = new SnapLine(SnapLineType.Right, offset, snapLine.Filter, snapLine.Priority);
                snapLines.Add(snapLineNew2);
              }
              else if (snapLine.SnapLineType == SnapLineType.Right)
              {
                SnapLine snapLineNew2 = new SnapLine(SnapLineType.Left, offset, snapLine.Filter, snapLine.Priority);
                snapLines.Add(snapLineNew2);
              }

            }
          }
          #endregion

          #region TextBox
          designer = TypeDescriptor.CreateDesigner(userControl.TextBox, typeof(IDesigner));
          if (designer == null)
          {
            throw new InvalidOperationException($"Could not create a Designer for ChildControl {userControl.TextBox}.");
          }


          designer.Initialize(userControl.TextBox);

          snapLinesChild = CopySnapLines(designer);

          foreach (SnapLine snapLine in snapLinesChild)
          {
            //TextBox: we copy only the left Snapline:
            if (snapLine.SnapLineType == SnapLineType.Left)
            {
              //Increase offset by x location of the button:
              int offset = snapLine.Offset + userControl.TextBox.Location.X;
              SnapLine snapLineNew = new SnapLine(snapLine.SnapLineType, offset, snapLine.Filter, snapLine.Priority);
              snapLines.Add(snapLineNew);
              //Also create a partner snapline (right).
              SnapLine snapLineNew2 = new SnapLine(SnapLineType.Right, offset, snapLine.Filter, snapLine.Priority);
              snapLines.Add(snapLineNew2);
            }
          }
          #endregion
        }
        catch (Exception ex)
        {
          this.DisplayError(ex.ToString());
        }
        return snapLines;
      }
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