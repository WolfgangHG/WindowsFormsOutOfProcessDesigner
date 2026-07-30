# SnapLines

This directory contains a series of samples about the handling of snaplines in the WinForms Out-of-process designer.

Snaplines are blue (or purple) lines drawn when dragging a control near another control, and they allow aligning controls on one line.

A baseline is useful to bring the height of text of controls to the same y position:

![Baseline](images/snapline_baseline.png)

Left/Right/Top/Bottom baselines are used to align the edges of controls:

![Left snapline](images/snapline_left.png)


## The samples
* The first sample [SnapLineDesignerBasics](SnapLineDesignerBasics/README.md) demonstrates how to add custom snaplines to a control.
* The second sample [SnapLineDesignerFromChild](SnapLineDesignerFromChild/README.md) shows how you can expose snaplines 
of controls inside a custom `UserControl` on the form that contains this `UserControl`.
* The third sample [SnapLineDesignerFromChildByDeclaration](SnapLineDesignerFromChildByDeclaration/README.md) brings the feature from sample 2
to a declarative approach: it shows a general purpose designer that copies snaplines which are defined on attribute declarations of user controls.
While the designer of sample 2 can be used only for one user control and must be copied for another user control, the designer of sample 3 is useable for an unlimited number of controls.

