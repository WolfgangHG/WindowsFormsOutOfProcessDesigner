using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapLineFromChildByDeclaration.Common
{
  /// <summary>
  /// Defines Snaplines that the <see cref="Designers.DESIGNER_SNAPLINE_FROM_CHILD_BY_DECLARATION"/> copies from a childcontrol. 
  /// They are displayed for the current control when it is used in a form.
  /// 
  /// Can be defined multiple times for different child controls.
  /// 
  /// Sample:
  /// <code>
  /// <![CDATA[
  ///   [Designer(Designers.DESIGNER_SNAPLINE_FROM_CHILD_BY_DECLARATION)]
  ///   [DesignerSnapLineSource(ChildControl = nameof(label1), SnapLines = SnapLineTypeToCopy.Baseline | SnapLineTypeToCopy.Left)]
  ///   [DesignerSnapLineSource(ChildControl = nameof(textBox1), SnapLines = SnapLineTypeToCopy.Baseline)]
  ///   public partial class MyControl : UserControl
  /// ]]>
  /// </code>
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
  public class DesignerSnapLineSourceAttribute : Attribute
  {
    /// <summary>
    /// Name of the field / member variable for a child control inside the current cntrol, whose SnapLines shall be copied.
    /// The name of the child control is set by a "nameof" operator.
    /// </summary>
    public string ChildControl
    {
      get;
      set;
    }

    /// <summary>
    /// Copy these SnapLines. You can OR multiple enum values.
    /// </summary>
    public SnapLineTypeToCopy SnapLines
    {
      get;
      set;
    }
  }
}
