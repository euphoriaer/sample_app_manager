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
			checkBox1 = new CheckBox();
			checkBox2 = new CheckBox();
			FtpPortText = new TextBox();
			label1 = new Label();
			label2 = new Label();
			SuspendLayout();
			// 
			// StartUp
			// 
			StartUp.Location = new Point(138, 109);
			StartUp.Name = "StartUp";
			StartUp.Size = new Size(171, 49);
			StartUp.TabIndex = 0;
			StartUp.Text = "启动分发平台";
			StartUp.UseVisualStyleBackColor = true;
			StartUp.Click += StartUp_Click;
			// 
			// HostTextBox
			// 
			HostTextBox.Location = new Point(120, 22);
			HostTextBox.Name = "HostTextBox";
			HostTextBox.Size = new Size(253, 23);
			HostTextBox.TabIndex = 1;
			// 
			// checkBox1
			// 
			checkBox1.AutoSize = true;
			checkBox1.Location = new Point(321, 109);
			checkBox1.Name = "checkBox1";
			checkBox1.Size = new Size(52, 21);
			checkBox1.TabIndex = 2;
			checkBox1.Text = "Http";
			checkBox1.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			checkBox2.AutoSize = true;
			checkBox2.Location = new Point(321, 136);
			checkBox2.Name = "checkBox2";
			checkBox2.Size = new Size(58, 21);
			checkBox2.TabIndex = 3;
			checkBox2.Text = "Https";
			checkBox2.UseVisualStyleBackColor = true;
			// 
			// FtpPortText
			// 
			FtpPortText.Location = new Point(120, 74);
			FtpPortText.Name = "FtpPortText";
			FtpPortText.Size = new Size(253, 23);
			FtpPortText.TabIndex = 4;
			FtpPortText.TextChanged += FtpPortText_TextChanged;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(27, 22);
			label1.Name = "label1";
			label1.Size = new Size(87, 17);
			label1.TabIndex = 5;
			label1.Text = "APP Manager";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(44, 80);
			label2.Name = "label2";
			label2.Size = new Size(56, 17);
			label2.TabIndex = 6;
			label2.Text = "FTP Port";
			// 
			// Main
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(456, 170);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(FtpPortText);
			Controls.Add(checkBox2);
			Controls.Add(checkBox1);
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
		private CheckBox checkBox1;
		private CheckBox checkBox2;
		private TextBox FtpPortText;
		private Label label1;
		private Label label2;
	}
}