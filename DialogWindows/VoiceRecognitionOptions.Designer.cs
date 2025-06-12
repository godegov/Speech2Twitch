namespace Speech2Twitch.DialogWindows
{
	partial class VoiceRecognitionOptions
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
			PathToSpeechModel = new TextBox();
			InputDeviceNames = new ComboBox();
			label2 = new Label();
			button1 = new Button();
			BtnAccept = new Button();
			BtnCancel = new Button();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(22, 28);
			label1.Name = "label1";
			label1.Size = new Size(258, 25);
			label1.TabIndex = 0;
			label1.Text = "Где лежат невросеткины веса:";
			// 
			// PathToSpeechModel
			// 
			PathToSpeechModel.Location = new Point(22, 69);
			PathToSpeechModel.Name = "PathToSpeechModel";
			PathToSpeechModel.Size = new Size(545, 31);
			PathToSpeechModel.TabIndex = 1;
			// 
			// InputDeviceNames
			// 
			InputDeviceNames.FormattingEnabled = true;
			InputDeviceNames.Location = new Point(22, 172);
			InputDeviceNames.Name = "InputDeviceNames";
			InputDeviceNames.Size = new Size(801, 33);
			InputDeviceNames.TabIndex = 2;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(22, 131);
			label2.Name = "label2";
			label2.Size = new Size(190, 25);
			label2.TabIndex = 3;
			label2.Text = "Куда будешь бубнить:";
			// 
			// button1
			// 
			button1.Location = new Point(596, 69);
			button1.Name = "button1";
			button1.Size = new Size(227, 34);
			button1.TabIndex = 4;
			button1.Text = "Указать путь";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// BtnAccept
			// 
			BtnAccept.Location = new Point(324, 260);
			BtnAccept.Name = "BtnAccept";
			BtnAccept.Size = new Size(243, 34);
			BtnAccept.TabIndex = 5;
			BtnAccept.Text = "Принять";
			BtnAccept.UseVisualStyleBackColor = true;
			BtnAccept.Click += BtnAccept_Click;
			// 
			// BtnCancel
			// 
			BtnCancel.Location = new Point(596, 260);
			BtnCancel.Name = "BtnCancel";
			BtnCancel.Size = new Size(227, 34);
			BtnCancel.TabIndex = 6;
			BtnCancel.Text = "Отмена";
			BtnCancel.UseVisualStyleBackColor = true;
			BtnCancel.Click += BtnCancel_Click;
			// 
			// VoiceRecognitionOptions
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(843, 323);
			Controls.Add(BtnCancel);
			Controls.Add(BtnAccept);
			Controls.Add(button1);
			Controls.Add(label2);
			Controls.Add(InputDeviceNames);
			Controls.Add(PathToSpeechModel);
			Controls.Add(label1);
			Name = "VoiceRecognitionOptions";
			Text = "Настройки говорилки и писалки";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private TextBox PathToSpeechModel;
		private ComboBox InputDeviceNames;
		private Label label2;
		private Button button1;
		private Button BtnAccept;
		private Button BtnCancel;
	}
}