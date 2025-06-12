namespace Speech2Twitch.DialogWindows
{
	partial class MessageMarkupOptions
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
			SendMessage = new TextBox();
			SendVoiceMessage = new TextBox();
			label2 = new Label();
			VoiceMessagePrefix = new TextBox();
			label3 = new Label();
			ClearMessage = new TextBox();
			label4 = new Label();
			PutDot = new TextBox();
			label5 = new Label();
			PutComma = new TextBox();
			label6 = new Label();
			button1 = new Button();
			button2 = new Button();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 34);
			label1.Name = "label1";
			label1.Size = new Size(197, 25);
			label1.TabIndex = 0;
			label1.Text = "Отправить то что есть:";
			// 
			// SendMessage
			// 
			SendMessage.Location = new Point(12, 72);
			SendMessage.Name = "SendMessage";
			SendMessage.Size = new Size(714, 31);
			SendMessage.TabIndex = 1;
			// 
			// SendVoiceMessage
			// 
			SendVoiceMessage.Location = new Point(12, 157);
			SendVoiceMessage.Name = "SendVoiceMessage";
			SendVoiceMessage.Size = new Size(714, 31);
			SendVoiceMessage.TabIndex = 3;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(12, 119);
			label2.Name = "label2";
			label2.Size = new Size(305, 25);
			label2.TabIndex = 2;
			label2.Text = "Отправить через голосовой режим";
			// 
			// VoiceMessagePrefix
			// 
			VoiceMessagePrefix.Location = new Point(12, 251);
			VoiceMessagePrefix.Name = "VoiceMessagePrefix";
			VoiceMessagePrefix.Size = new Size(714, 31);
			VoiceMessagePrefix.TabIndex = 5;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(12, 213);
			label3.Name = "label3";
			label3.Size = new Size(283, 25);
			label3.TabIndex = 4;
			label3.Text = "Префикс голосового сообщения";
			// 
			// ClearMessage
			// 
			ClearMessage.Location = new Point(12, 344);
			ClearMessage.Name = "ClearMessage";
			ClearMessage.Size = new Size(714, 31);
			ClearMessage.TabIndex = 7;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(12, 306);
			label4.Name = "label4";
			label4.Size = new Size(185, 25);
			label4.TabIndex = 6;
			label4.Text = "Очистить сообщение";
			// 
			// PutDot
			// 
			PutDot.Location = new Point(12, 442);
			PutDot.Name = "PutDot";
			PutDot.Size = new Size(714, 31);
			PutDot.TabIndex = 9;
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new Point(12, 404);
			label5.Name = "label5";
			label5.Size = new Size(144, 25);
			label5.TabIndex = 8;
			label5.Text = "поставить точку";
			// 
			// PutComma
			// 
			PutComma.Location = new Point(12, 534);
			PutComma.Name = "PutComma";
			PutComma.Size = new Size(714, 31);
			PutComma.TabIndex = 11;
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new Point(12, 496);
			label6.Name = "label6";
			label6.Size = new Size(165, 25);
			label6.TabIndex = 10;
			label6.Text = "поставить запятую";
			// 
			// button1
			// 
			button1.Location = new Point(346, 600);
			button1.Name = "button1";
			button1.Size = new Size(161, 34);
			button1.TabIndex = 12;
			button1.Text = "Применить";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// button2
			// 
			button2.Location = new Point(523, 600);
			button2.Name = "button2";
			button2.Size = new Size(161, 34);
			button2.TabIndex = 13;
			button2.Text = "Отмена";
			button2.UseVisualStyleBackColor = true;
			button2.Click += button2_Click;
			// 
			// MessageMarkupOptions
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(738, 664);
			Controls.Add(button2);
			Controls.Add(button1);
			Controls.Add(PutComma);
			Controls.Add(label6);
			Controls.Add(PutDot);
			Controls.Add(label5);
			Controls.Add(ClearMessage);
			Controls.Add(label4);
			Controls.Add(VoiceMessagePrefix);
			Controls.Add(label3);
			Controls.Add(SendVoiceMessage);
			Controls.Add(label2);
			Controls.Add(SendMessage);
			Controls.Add(label1);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "MessageMarkupOptions";
			Text = "Ключевые слова из текста";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private TextBox SendMessage;
		private TextBox SendVoiceMessage;
		private Label label2;
		private TextBox VoiceMessagePrefix;
		private Label label3;
		private TextBox ClearMessage;
		private Label label4;
		private TextBox PutDot;
		private Label label5;
		private TextBox PutComma;
		private Label label6;
		private Button button1;
		private Button button2;
	}
}