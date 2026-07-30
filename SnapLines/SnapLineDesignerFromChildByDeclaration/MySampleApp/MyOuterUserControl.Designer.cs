namespace MySampleApp
{
  partial class MyOuterUserControl
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.myInnerUserControl1 = new SnapLineFromChild.Control.MyInnerUserControl();
      this.label1 = new Label();
      this.textBox1 = new TextBox();
      this.checkBox1 = new CheckBox();
      this.SuspendLayout();
      // 
      // myInnerUserControl1
      // 
      this.myInnerUserControl1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.myInnerUserControl1.BackColor = SystemColors.Info;
      this.myInnerUserControl1.Location = new Point(0, 0);
      this.myInnerUserControl1.Name = "myInnerUserControl1";
      this.myInnerUserControl1.Size = new Size(436, 77);
      this.myInnerUserControl1.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new Point(0, 85);
      this.label1.Name = "label1";
      this.label1.Size = new Size(38, 15);
      this.label1.TabIndex = 2;
      this.label1.Text = "label1";
      // 
      // textBox1
      // 
      this.textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.textBox1.Location = new Point(216, 82);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(220, 23);
      this.textBox1.TabIndex = 3;
      // 
      // checkBox1
      // 
      this.checkBox1.AutoSize = true;
      this.checkBox1.Location = new Point(101, 84);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new Size(82, 19);
      this.checkBox1.TabIndex = 4;
      this.checkBox1.Text = "checkBox1";
      this.checkBox1.UseVisualStyleBackColor = true;
      // 
      // MyOuterUserControl
      // 
      this.AutoScaleDimensions = new SizeF(7F, 15F);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add(this.checkBox1);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.myInnerUserControl1);
      this.Name = "MyOuterUserControl";
      this.Size = new Size(436, 108);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    #endregion

    private SnapLineFromChild.Control.MyInnerUserControl myInnerUserControl1;
    private Label label1;
    private TextBox textBox1;
    private CheckBox checkBox1;
  }
}
