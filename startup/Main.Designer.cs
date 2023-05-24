namespace app_manager_startup
{
	partial class Main
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			StartUp = new Button();
			HostTextBox = new TextBox();
			SuspendLayout();
			// 
			// StartUp
			// 
			StartUp.Location = new Point(102, 86);
			StartUp.Name = "StartUp";
			StartUp.Size = new Size(171, 49);
			StartUp.TabIndex = 0;
			StartUp.Text = "启动分发平台";
			StartUp.UseVisualStyleBackColor = true;
			StartUp.Click += StartUp_Click;
			// 
			// HostTextBox
			// 
			HostTextBox.Location = new Point(59, 22);
			HostTextBox.Name = "HostTextBox";
			HostTextBox.Size = new Size(253, 23);
			HostTextBox.TabIndex = 1;
			// 
			// Main
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(379, 170);
			Controls.Add(HostTextBox);
			Controls.Add(StartUp);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "Main";
			Text = "AppManager";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button StartUp;
		private TextBox HostTextBox;
	}
}