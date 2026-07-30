using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapLineFromChildByDeclaration.Common
{
  /// <summary>
  /// Describes the types of SnapLines that are copied from a single child control.
  /// 
  /// </summary>
  [Flags]
  public enum SnapLineTypeToCopy
  {
    /// <summary>
    /// Default: no SnapLine
    /// </summary>
    None = 0,
    /// <summary>
    /// Top SnapLine
    /// </summary>
    Top = 1,
    /// <summary>
    /// Bottom SnapLine
    /// </summary>
    Bottom = 2,
    /// <summary>
    /// Left SnapLine
    /// </summary>
    Left = 4,
    /// <summary>
    /// Right SnapLine
    /// </summary>
    Right = 8,
    /// <summary>
    /// A horizontal snapline typically not associated with an edge of a control.
    /// </summary>
    Horizontal = 16,
    /// <summary>
    /// A vertical snapline typically not associated with an edge of a control.
    /// </summary>
    Vertical = 32,
    /// <summary>
    /// A horizontal snapline typically associated with a primary internal feature of a control; for example, the base of the text string in a Label control.
    /// </summary>
    Baseline = 64
  }
}
