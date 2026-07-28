using SnapLineFromChild.Control;
using System.Net.NetworkInformation;

namespace MySampleApp
{
  partial class Form1
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.myPanel = new MyUserControl();
      this.button1 = new Button();
      this.SuspendLayout();
      // 
      // myPanel
      // 
      this.myPanel.BackColor = SystemColors.Info;
      this.myPanel.Location = new Point(12, 28);
      this.myPanel.Name = "myPanel";
      this.myPanel.Size = new Size(372, 100);
      this.myPanel.TabIndex = 0;
      // 
      // button1
      // 
      this.button1.Location = new Point(408, 31);
      this.button1.Name = "button1";
      this.button1.Size = new Size(112, 23);
      this.button1.TabIndex = 1;
      this.button1.Text = "Sample button";
      this.button1.UseVisualStyleBackColor = true;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new SizeF(7F, 15F);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(543, 277);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.myPanel);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);
    }

    #endregion

    private MyUserControl myPanel;
    private Button button1;
  }
}
