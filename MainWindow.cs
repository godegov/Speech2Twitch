using NAudio.Wave;
using Vosk;
using System.Text.Json;
using Speech2Twitch.DialogWindows;

namespace Speech2Twitch
{
	public partial class MainWindow : Form
	{
		// Some default stuff
		static public DataStructures.TwitchConnectionDescriptor DefaulTwitchConnectionDescriptor = new()
		{ LoginName = "", TwitchToken = "", StreamChannelName = "Margaret_Wolf" };
		static public DataStructures.MessageDescriptors DefaulMessageDescriptors = new()
		{ MessageSend = "отправить", MessageSendVoice = "озвучить", MessageSendVoicePrefix = "!/", MessageClear = "очистить", LiteralDot = "точка", LiteralComma = "запятая" };
		static public DataStructures.SpeechRecognitionDescriptor DefaulSpeechRecognitionDescriptor = new()
		{ PathToModel = "", InpudDeviceID = "" };

		private SpeechRecognitionWrapper SRWrapper;
		private TwitchMessager TwitchMsgr = new TwitchMessager();

		private TwitchConnectionOptions? twitchConnectionOptions;
		private MessageMarkupOptions? messageMarkupOptions;
		private VoiceRecognitionOptions? SpeechRecognitionOptions;

		private DataStructures.TwitchConnectionDescriptor TwitchConnectionDescriptor = DefaulTwitchConnectionDescriptor;
		private DataStructures.MessageDescriptors MessageDescriptors = DefaulMessageDescriptors;
		private DataStructures.SpeechRecognitionDescriptor SpeechRecognitionDescriptor = DefaulSpeechRecognitionDescriptor;
		private string ConfigPath;
		public MainWindow()
		{
			this.Name = "Speech to Twitch";
			string configFolder = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				"SpeechToTwitch"
			);

			ConfigPath = Path.Combine(configFolder, "settings.json");

			if (Directory.Exists(configFolder) && File.Exists(ConfigPath))
			{
				DataStructures.Serializer.Deserialize(ConfigPath, out MessageDescriptors, out TwitchConnectionDescriptor, out SpeechRecognitionDescriptor);
			}
			else
			{
				Directory.CreateDirectory(configFolder);
				DataStructures.Serializer.Serialize(ConfigPath, MessageDescriptors, TwitchConnectionDescriptor, SpeechRecognitionDescriptor);
			}


			SRWrapper = new SpeechRecognitionWrapper(onPartialResult: text => Invoke(() => label1.Text = text));
			SRWrapper.OnMessageSend = OnMessageSend;
			SRWrapper.OnUpdatePending = OnUpdatePending;
			SRWrapper.MessageDescriptors = MessageDescriptors;
			SRWrapper.CurrentSpeechRecognitionDescriptor = SpeechRecognitionDescriptor;
			SRWrapper.LoadBadWordFilter(configFolder);

			InitializeComponent();
			this.StartPosition = FormStartPosition.CenterScreen;
			EventLogger.Instance.SetTextBox(LoggingTextBox);
			LoggingTextBox.Multiline = true;
			LoggingTextBox.ScrollBars = ScrollBars.Vertical;
			LoggingTextBox.ReadOnly = true;
			LoggingTextBox.WordWrap = false;
			LoggingTextBox.Font = new Font("Consolas", 9);

			if (SpeechRecognitionWrapper.IsValidVoskModel(SpeechRecognitionDescriptor.PathToModel))
			{
				SRWrapper.LoadModelAsync(SpeechRecognitionDescriptor.PathToModel);
			}
			//TwitchMsgr.Connect(TwitchConnectionDescriptor);
		}

		private void OnUpdatePending(string InData)
		{
			if (PendingText.InvokeRequired)
			{
				PendingText.Invoke(new Action(() =>
				{
					PendingText.Text = InData;
				}));
			}
			else
			{
				PendingText.Text = InData;
			}
		}
		private void OnMessageSend(string InData)
		{
			if (PendingText.InvokeRequired || SentToTwitch.InvokeRequired)
			{
				PendingText.Invoke(new Action(() =>
				{
					PendingText.Clear();
					SentToTwitch.Text = InData;
					TwitchMsgr.SendMessage(InData);
				}));
			}
			else
			{
				PendingText.Clear();
				SentToTwitch.Text = InData;
				TwitchMsgr.SendMessage(InData);
			}
		}
		private void button1_Click(object sender, EventArgs e)
		{
			SRWrapper.Start();
		}

		private void TwitchOptionsClick(object sender, EventArgs e)
		{
			twitchConnectionOptions = new TwitchConnectionOptions(ref TwitchConnectionDescriptor, OnTwitchDescriptorChanged);
			twitchConnectionOptions.StartPosition = FormStartPosition.CenterParent;
			twitchConnectionOptions.ShowDialog(this);
		}

		private void OnTwitchDescriptorChanged(DataStructures.TwitchConnectionDescriptor NewTwitchConnectionDescriptor)
		{
			this.TwitchConnectionDescriptor = NewTwitchConnectionDescriptor;
			DataStructures.Serializer.Serialize(ConfigPath, MessageDescriptors, TwitchConnectionDescriptor, SpeechRecognitionDescriptor);
			TwitchMsgr.Connect(TwitchConnectionDescriptor);
		}

		private void MessageSendOptionsClick(object sender, EventArgs e)
		{
			messageMarkupOptions = new MessageMarkupOptions(ref MessageDescriptors, OnMessageSendChanged);
			messageMarkupOptions.StartPosition = FormStartPosition.CenterParent;
			messageMarkupOptions.ShowDialog(this);
		}

		private void OnMessageSendChanged(DataStructures.MessageDescriptors MessageDescriptors)
		{
			this.MessageDescriptors = MessageDescriptors;
			DataStructures.Serializer.Serialize(ConfigPath, this.MessageDescriptors, TwitchConnectionDescriptor, SpeechRecognitionDescriptor);
			SRWrapper.MessageDescriptors = MessageDescriptors;
		}

		private void SpeechRecognitionClick(object sender, EventArgs e)
		{
			SpeechRecognitionOptions = new VoiceRecognitionOptions(ref SpeechRecognitionDescriptor, OnSpeechRecognitionChanged);
			SpeechRecognitionOptions.StartPosition = FormStartPosition.CenterParent;
			SpeechRecognitionOptions.ShowDialog(this);
		}

		private void OnSpeechRecognitionChanged(DataStructures.SpeechRecognitionDescriptor NewTwitchConnectionDescriptor)
		{
			this.SpeechRecognitionDescriptor = NewTwitchConnectionDescriptor;
			if (SpeechRecognitionWrapper.IsValidVoskModel(SpeechRecognitionDescriptor.PathToModel))
			{
				DataStructures.Serializer.Serialize(ConfigPath, MessageDescriptors, TwitchConnectionDescriptor, SpeechRecognitionDescriptor);
				SRWrapper.CurrentSpeechRecognitionDescriptor = NewTwitchConnectionDescriptor;
				SRWrapper.LoadModelAsync(SpeechRecognitionDescriptor.PathToModel);
			}
		}

		private void TwitchConnect_Click(object sender, EventArgs e)
		{
			TwitchMsgr.Connect(TwitchConnectionDescriptor);
		}
	}
}
