using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Speech2Twitch.DataStructures;

namespace Speech2Twitch.DialogWindows
{
	public partial class MessageMarkupOptions : Form
	{
		public delegate void OnSetNewValues(DataStructures.MessageDescriptors inSpeechRecognitionDescriptor);
		private OnSetNewValues NewValuesSet;
		public MessageMarkupOptions(ref DataStructures.MessageDescriptors inSpeechRecognitionDescriptor, OnSetNewValues InNewValuesSet)
		{
			NewValuesSet = InNewValuesSet;
			InitializeComponent();
			this.SendMessage.Text = inSpeechRecognitionDescriptor.MessageSend;
			this.SendVoiceMessage.Text = inSpeechRecognitionDescriptor.MessageSendVoice;
			this.VoiceMessagePrefix.Text = inSpeechRecognitionDescriptor.MessageSendVoicePrefix;
			this.ClearMessage.Text = inSpeechRecognitionDescriptor.MessageClear;
			this.PutDot.Text = inSpeechRecognitionDescriptor.LiteralDot;
			this.PutComma.Text = inSpeechRecognitionDescriptor.LiteralComma;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			DataStructures.MessageDescriptors tmp = new DataStructures.MessageDescriptors();
			tmp.MessageSend = this.SendMessage.Text;
			tmp.MessageSendVoice = this.SendVoiceMessage.Text;
			tmp.MessageSendVoicePrefix = this.VoiceMessagePrefix.Text;
			tmp.MessageClear = this.ClearMessage.Text;
			tmp.LiteralDot = this.PutDot.Text;
			tmp.LiteralComma = this.PutComma.Text;
			NewValuesSet(tmp);
			this.Close();
		}
	}
}
