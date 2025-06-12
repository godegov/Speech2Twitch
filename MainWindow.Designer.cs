namespace Speech2Twitch
{
    partial class MainWindow
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
			SentToTwitch = new TextBox();
			label1 = new Label();
			menuStrip1 = new MenuStrip();
			фаелToolStripMenuItem = new ToolStripMenuItem();
			TwitchOptions = new ToolStripMenuItem();
			свойтсваРаспознаванияБубнежаToolStripMenuItem = new ToolStripMenuItem();
			ключевыеФразыToolStripMenuItem = new ToolStripMenuItem();
			label2 = new Label();
			label3 = new Label();
			PendingText = new TextBox();
			LogLabel = new Label();
			LoggingTextBox = new TextBox();
			TwitchConnect = new Button();
			menuStrip1.SuspendLayout();
			SuspendLayout();
			// 
			// SentToTwitch
			// 
			SentToTwitch.Location = new Point(12, 68);
			SentToTwitch.Multiline = true;
			SentToTwitch.Name = "SentToTwitch";
			SentToTwitch.Size = new Size(1376, 225);
			SentToTwitch.TabIndex = 1;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(13, 601);
			label1.Name = "label1";
			label1.Size = new Size(131, 25);
			label1.TabIndex = 2;
			label1.Text = "Распознанное:";
			// 
			// menuStrip1
			// 
			menuStrip1.ImageScalingSize = new Size(24, 24);
			menuStrip1.Items.AddRange(new ToolStripItem[] { фаелToolStripMenuItem });
			menuStrip1.Location = new Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new Size(1400, 33);
			menuStrip1.TabIndex = 3;
			menuStrip1.Text = "menuStrip1";
			// 
			// фаелToolStripMenuItem
			// 
			фаелToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { TwitchOptions, свойтсваРаспознаванияБубнежаToolStripMenuItem, ключевыеФразыToolStripMenuItem });
			фаелToolStripMenuItem.Name = "фаелToolStripMenuItem";
			фаелToolStripMenuItem.Size = new Size(68, 29);
			фаелToolStripMenuItem.Text = "Фаел";
			// 
			// TwitchOptions
			// 
			TwitchOptions.Name = "TwitchOptions";
			TwitchOptions.Size = new Size(394, 34);
			TwitchOptions.Text = "Настройки подключения к Твичу";
			TwitchOptions.Click += TwitchOptionsClick;
			// 
			// свойтсваРаспознаванияБубнежаToolStripMenuItem
			// 
			свойтсваРаспознаванияБубнежаToolStripMenuItem.Name = "свойтсваРаспознаванияБубнежаToolStripMenuItem";
			свойтсваРаспознаванияБубнежаToolStripMenuItem.Size = new Size(394, 34);
			свойтсваРаспознаванияБубнежаToolStripMenuItem.Text = "Свойтсва распознавания бубнежа";
			свойтсваРаспознаванияБубнежаToolStripMenuItem.Click += SpeechRecognitionClick;
			// 
			// ключевыеФразыToolStripMenuItem
			// 
			ключевыеФразыToolStripMenuItem.Name = "ключевыеФразыToolStripMenuItem";
			ключевыеФразыToolStripMenuItem.Size = new Size(394, 34);
			ключевыеФразыToolStripMenuItem.Text = "Ключевые фразы";
			ключевыеФразыToolStripMenuItem.Click += MessageSendOptionsClick;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(10, 40);
			label2.Name = "label2";
			label2.Size = new Size(235, 25);
			label2.TabIndex = 4;
			label2.Text = "Отправленные сообщения:";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(12, 306);
			label3.Name = "label3";
			label3.Size = new Size(132, 25);
			label3.TabIndex = 6;
			label3.Text = "Текущий текст:";
			// 
			// PendingText
			// 
			PendingText.Location = new Point(12, 334);
			PendingText.Multiline = true;
			PendingText.Name = "PendingText";
			PendingText.Size = new Size(1376, 228);
			PendingText.TabIndex = 5;
			// 
			// LogLabel
			// 
			LogLabel.AutoSize = true;
			LogLabel.Location = new Point(12, 647);
			LogLabel.Name = "LogLabel";
			LogLabel.Size = new Size(46, 25);
			LogLabel.TabIndex = 8;
			LogLabel.Text = "Лог:";
			// 
			// LoggingTextBox
			// 
			LoggingTextBox.Location = new Point(12, 675);
			LoggingTextBox.Multiline = true;
			LoggingTextBox.Name = "LoggingTextBox";
			LoggingTextBox.Size = new Size(1376, 348);
			LoggingTextBox.TabIndex = 7;
			// 
			// TwitchConnect
			// 
			TwitchConnect.Location = new Point(1095, 593);
			TwitchConnect.Name = "TwitchConnect";
			TwitchConnect.Size = new Size(293, 40);
			TwitchConnect.TabIndex = 9;
			TwitchConnect.Text = "Подключиться к Твич";
			TwitchConnect.UseVisualStyleBackColor = true;
			TwitchConnect.Click += TwitchConnect_Click;
			// 
			// MainWindow
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1400, 1035);
			Controls.Add(TwitchConnect);
			Controls.Add(LogLabel);
			Controls.Add(LoggingTextBox);
			Controls.Add(label3);
			Controls.Add(PendingText);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(SentToTwitch);
			Controls.Add(menuStrip1);
			MainMenuStrip = menuStrip1;
			Name = "MainWindow";
			Text = "Form1";
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private TextBox SentToTwitch;
		private Label label1;
		private MenuStrip menuStrip1;
		private ToolStripMenuItem фаелToolStripMenuItem;
		private ToolStripMenuItem TwitchOptions;
		private ToolStripMenuItem свойтсваРаспознаванияБубнежаToolStripMenuItem;
		private ToolStripMenuItem ключевыеФразыToolStripMenuItem;
		private Label label2;
		private Label label3;
		private TextBox PendingText;
		private Label LogLabel;
		private TextBox LoggingTextBox;
		private Button TwitchConnect;
	}
}
