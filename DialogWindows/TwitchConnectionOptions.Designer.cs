namespace Speech2Twitch
{
	partial class TwitchConnectionOptions
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			label1 = new Label();
			button1 = new Button();
			button2 = new Button();
			ChannelName = new TextBox();
			label2 = new Label();
			UserLogin = new TextBox();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(26, 22);
			label1.Name = "label1";
			label1.Size = new Size(194, 25);
			label1.TabIndex = 0;
			label1.Text = "Имя канала стримера:";
			// 
			// button1
			// 
			button1.Location = new Point(552, 227);
			button1.Name = "button1";
			button1.Size = new Size(201, 34);
			button1.TabIndex = 1;
			button1.Text = "Нахрен это дерьмо";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// button2
			// 
			button2.Location = new Point(294, 227);
			button2.Name = "button2";
			button2.Size = new Size(199, 34);
			button2.TabIndex = 2;
			button2.Text = "Применить";
			button2.UseVisualStyleBackColor = true;
			button2.Click += button2_Click;
			// 
			// ChannelName
			// 
			ChannelName.Location = new Point(26, 50);
			ChannelName.Name = "ChannelName";
			ChannelName.Size = new Size(452, 31);
			ChannelName.TabIndex = 3;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(26, 116);
			label2.Name = "label2";
			label2.Size = new Size(222, 25);
			label2.TabIndex = 4;
			label2.Text = "Логин пользователя чата:";
			// 
			// UserLogin
			// 
			UserLogin.Location = new Point(26, 144);
			UserLogin.Name = "UserLogin";
			UserLogin.Size = new Size(452, 31);
			UserLogin.TabIndex = 6;
			// 
			// TwitchConnectionOptions
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 284);
			Controls.Add(UserLogin);
			Controls.Add(label2);
			Controls.Add(ChannelName);
			Controls.Add(button2);
			Controls.Add(button1);
			Controls.Add(label1);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "TwitchConnectionOptions";
			Text = "TwitchConnectionOptions";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private Button button1;
		private Button button2;
		private TextBox ChannelName;
		private Label label2;
		private TextBox UserLogin;
	}
}