using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio;
using NAudio.Wave;
using TwitchLib.Client;
using TwitchLib.Client.Models;
using Vosk;
using static System.Net.Mime.MediaTypeNames;

namespace Speech2Twitch
{
	public class SpeechRecognitionWrapper
	{
		// private fields
		private VoskRecognizer? SpeechRecognizer;
		private WaveInEvent? WaveAudioInput;
		private Model? LanguageModel;
		private readonly Action<string> OnPartialResult;
		private List<string> Message = new();
		private BadWordsFilter WordFilter = new();

		// public fields
		public delegate void OnMessage(string Data);
		public DataStructures.SpeechRecognitionDescriptor CurrentSpeechRecognitionDescriptor;
		public DataStructures.MessageDescriptors MessageDescriptors;
		public OnMessage? OnMessageSend;
		public OnMessage? OnUpdatePending;

		public SpeechRecognitionWrapper(Action<string> onPartialResult)
		{
			WaveAudioInput = new WaveInEvent();
			WaveAudioInput.WaveFormat = new WaveFormat(16000, 1);
			this.OnPartialResult = onPartialResult;
		}
		public static bool IsValidVoskModel(string path)
		{
			if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
				return false;

			// Simple Vosk attributes:
			bool hasConf = File.Exists(Path.Combine(path, "conf", "model.conf"));
			bool hasAm = Directory.Exists(Path.Combine(path, "am"));

			return hasConf && hasAm;
		}

		public void SetOptions(DataStructures.SpeechRecognitionDescriptor inSpeechRecognitionDescriptor)
		{
			if (IsValidVoskModel(inSpeechRecognitionDescriptor.PathToModel))
			{
				CurrentSpeechRecognitionDescriptor = inSpeechRecognitionDescriptor;
				LoadModelAsync(inSpeechRecognitionDescriptor.PathToModel);
			}
			else
			{
				EventLogger.Instance.Log(
					$"Папка [{inSpeechRecognitionDescriptor.PathToModel}] не содержит языковой модели Vosk. Найди ту что содержит и дай мне.");
			}
		}

		public string GetMessageString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (string s in Message)
			{
				sb.Append(s + " ");
			}
			return sb.ToString();
		}

		public bool LoadBadWordFilter(string path)
		{
			return WordFilter.LoadOrInitialize(path);
		}
		public void AddStringToMessage(List<string> message)
		{
			foreach (string s in message)
			{
				Message.Add(s);
			}
		}

		public void ConvertStringToMessage(string Data, List<string> Message)
		{
			var res = Data.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (res.Length > 0)
			{
				Message.AddRange(res);
			}
		}

		public void CheckForCommands(string InData, DataStructures.MessageDescriptors inMessageDescriptors)
		{
			List<string> RecognizedMessage = new List<string>();
			ConvertStringToMessage(InData, RecognizedMessage);
			if (RecognizedMessage.Count > 0)
			{
				for (int i=0; i < RecognizedMessage.Count; i++)
				{
					if (inMessageDescriptors.MessageClear == RecognizedMessage[i])
					{
						Message.Clear();
						if (OnUpdatePending != null)
							OnUpdatePending(GetMessageString());
						return;
					}

					if (inMessageDescriptors.MessageSend == RecognizedMessage[i])
					{
						for (int x = 0; x < i; x++)
						{
							Message.Add(RecognizedMessage[x]);
						}

						if (OnMessageSend != null)
							OnMessageSend(WordFilter.Filter(GetMessageString()));
						Message.Clear();
						if (OnUpdatePending != null)
							OnUpdatePending(GetMessageString());
						return;
					}
					if (inMessageDescriptors.MessageSendVoice == RecognizedMessage[i])
					{
						for (int x = 0; x < i; x++)
						{
							Message.Add(RecognizedMessage[x]);
						}
						if (OnMessageSend != null)
							OnMessageSend( inMessageDescriptors.MessageSendVoicePrefix + WordFilter.Filter(GetMessageString()));
						Message.Clear();
						if (OnUpdatePending != null)
							OnUpdatePending(GetMessageString());
						return;
					}
				}

				AddStringToMessage(RecognizedMessage);
				if (OnUpdatePending != null)
					OnUpdatePending(GetMessageString());
			}
		}
		public void RedirectVoskLogsTo(Action<string> logHandler)
		{
			var writer = new StringWriter();
			Console.SetError(writer);

			Task.Run(async () =>
			{
				while (true)
				{
					await Task.Delay(200);
					var logs = writer.ToString();
					if (!string.IsNullOrWhiteSpace(logs))
					{
						logHandler(logs.Trim());
						writer.GetStringBuilder().Clear(); // сброс буфера
					}
				}
			});
		}

		public Task<bool> LoadModelAsync(string modelPath)
		{
			return Task.Run(() => LoadModel(modelPath));
		}
		public bool LoadModel(string modelPath)
		{
			EventLogger.Instance.Log($"Загрузка языковой модели {modelPath}");
			if (!IsValidVoskModel(modelPath))
				throw new Exception("Папка не содержит допустимую модель Vosk");

			Vosk.Vosk.SetLogLevel(0);
			var model = new Model(modelPath);
			EventLogger.Instance.Log("Новая модель загружена");

			LanguageModel?.Dispose();
			LanguageModel = model;
			if (Start())
				EventLogger.Instance.Log("Запущен звуковой драйвер. Можно говорить.");

			return true;
		}

		public bool Start()
		{
			Vosk.Vosk.SetLogLevel(0);
			SpeechRecognizer = new VoskRecognizer(LanguageModel, 16000.0f);
			SpeechRecognizer.SetMaxAlternatives(0);
			SpeechRecognizer.SetWords(true);

			WaveAudioInput = new WaveInEvent();
			if (WaveIn.DeviceCount > 0)
			{
				WaveAudioInput.WaveFormat = new WaveFormat(16000, 1);
				WaveAudioInput.DataAvailable += OnDataAvailable;
				try
				{
					WaveAudioInput.StartRecording();
				}
				catch (MmException mmEx)
				{
					EventLogger.Instance.Log($"Ошибка NAudio: {mmEx.Message}");
					return false;
				}
				catch (InvalidOperationException invOpEx)
				{
					EventLogger.Instance.Log($"Ошибка работы с устройством записи: {invOpEx.Message}");
					return false;
				}
			}
			else
			{
				EventLogger.Instance.Log("Нету микрофонов, похоже. Воткните хотябы один, тогда буду работать");
				return false;
			}

			return true;
		}

		public void Stop()
		{
			WaveAudioInput?.StopRecording();
			WaveAudioInput?.Dispose();
			SpeechRecognizer?.Dispose();
			LanguageModel?.Dispose();
		}
		private void OnDataAvailable(object? sender, WaveInEventArgs e)
		{
			if (SpeechRecognizer != null)
			{
				if (SpeechRecognizer.AcceptWaveform(e.Buffer, e.BytesRecorded))
				{
					string resultJson = SpeechRecognizer.Result();
					string? text = JsonDocument.Parse(resultJson).RootElement.GetProperty("text").GetString();
					if (!string.IsNullOrWhiteSpace(text))
					{
						CheckForCommands(text, MessageDescriptors);
					}
				}
				else
				{
					string partialJson = SpeechRecognizer.PartialResult();
					string? partial = JsonDocument.Parse(partialJson).RootElement.GetProperty("partial").GetString();
					if (!string.IsNullOrWhiteSpace(partial))
					{
						OnPartialResult?.Invoke(partial);
					}
				}
			}
		}
	}

}
