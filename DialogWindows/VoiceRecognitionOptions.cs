using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Speech2Twitch.DialogWindows
{
	public partial class VoiceRecognitionOptions : Form
	{
		public delegate void OnSetNewValues(DataStructures.SpeechRecognitionDescriptor inSpeechRecognitionDescriptor);
		private OnSetNewValues NewValuesSet;
		public VoiceRecognitionOptions(ref DataStructures.SpeechRecognitionDescriptor inSpeechRecognitionDescriptor, OnSetNewValues InNewValuesSet)
		{
			NewValuesSet = InNewValuesSet;
			InitializeComponent();
			this.PathToSpeechModel.Text = inSpeechRecognitionDescriptor.PathToModel;
			this.InputDeviceNames.Text = inSpeechRecognitionDescriptor.InpudDeviceID;
			this.InputDeviceNames.Enabled = false;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.PathToSpeechModel.Text = ChooseModelFolder();
		}

		public string ChooseModelFolder()
		{
			using var dialog = new FolderBrowserDialog
			{
				Description = "Выберите папку с моделью Vosk",
				UseDescriptionForTitle = true,
				ShowNewFolderButton = false
			};

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				var selectedPath = dialog.SelectedPath;
				return selectedPath;
			}

			return "";
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void BtnAccept_Click(object sender, EventArgs e)
		{
			DataStructures.SpeechRecognitionDescriptor tmp = new DataStructures.SpeechRecognitionDescriptor();
			tmp.PathToModel = this.PathToSpeechModel.Text;
			tmp.InpudDeviceID = this.InputDeviceNames.Text;
			NewValuesSet(tmp);
			this.Close();
		}
	}
}
