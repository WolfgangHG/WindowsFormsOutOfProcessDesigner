namespace SnapLineFromChild.Control
{
  partial class MyInnerUserControl
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
      this.button1 = new Button();
      this.radioButton1 = new RadioButton();
      this.radioButton2 = new RadioButton();
      this.radioButton3 = new RadioButton();
      this.textBox1 = new TextBox();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Location = new Point(216, 3);
      this.button1.Name = "button1";
      this.button1.Size = new Size(75, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "button1";
      this.button1.UseVisualStyleBackColor = true;
      // 
      // radioButton1
      // 
      this.radioButton1.AutoSize = true;
      this.radioButton1.Location = new Point(0, 5);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new Size(94, 19);
      this.radioButton1.TabIndex = 1;
      this.radioButton1.TabStop = true;
      this.radioButton1.Text = "radioButton1";
      this.radioButton1.UseVisualStyleBackColor = true;
      // 
      // radioButton2
      // 
      this.radioButton2.AutoSize = true;
      this.radioButton2.Location = new Point(0, 30);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new Size(94, 19);
      this.radioButton2.TabIndex = 2;
      this.radioButton2.TabStop = true;
      this.radioButton2.Text = "radioButton2";
      this.radioButton2.UseVisualStyleBackColor = true;
      // 
      // radioButton3
      // 
      this.radioButton3.AutoSize = true;
      this.radioButton3.Location = new Point(0, 55);
      this.radioButton3.Name = "radioButton3";
      this.radioButton3.Size = new Size(94, 19);
      this.radioButton3.TabIndex = 3;
      this.radioButton3.TabStop = true;
      this.radioButton3.Text = "radioButton3";
      this.radioButton3.UseVisualStyleBackColor = true;
      // 
      // textBox1
      // 
      this.textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.textBox1.Location = new Point(297, 4);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(139, 23);
      this.textBox1.TabIndex = 4;
      // 
      // MyInnerUserControl
      // 
      this.AutoScaleDimensions = new SizeF(7F, 15F);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.radioButton3);
      this.Controls.Add(this.radioButton2);
      this.Controls.Add(this.radioButton1);
      this.Controls.Add(this.button1);
      this.Name = "MyInnerUserControl";
      this.Size = new Size(436, 81);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.RadioButton radioButton1;
    private System.Windows.Forms.RadioButton radioButton2;
    private System.Windows.Forms.RadioButton radioButton3;
    private System.Windows.Forms.TextBox textBox1;
  }
}
