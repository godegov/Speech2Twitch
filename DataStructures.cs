using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Speech2Twitch
{
	public static class DataStructures
	{
		public struct MessageDescriptors
		{
			public string MessageSend{ get; set; }
			public string MessageSendVoice{ get; set; }
			public string MessageSendVoicePrefix{ get; set; }
			public string MessageClear{ get; set; }
			public string LiteralDot{ get; set; }
			public string LiteralComma{ get; set; }
		}

		public struct TwitchConnectionDescriptor
		{
			public string LoginName{ get; set; }
			public string StreamChannelName{ get; set; }
		}

		public struct SpeechRecognitionDescriptor
		{
			public string PathToModel{ get; set; }
			public string InpudDeviceID{ get; set; }
		}
		public class AppSettings
		{
			public MessageDescriptors Messages { get; set; }
			public TwitchConnectionDescriptor TwitchConnection { get; set; }
			public SpeechRecognitionDescriptor SpeechRecognition { get; set; }
		}
		public static class Serializer
		{
			private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
			{
				WriteIndented = true, // Читабельное форматирование
			};

			public static void Serialize(string filePath, 
				MessageDescriptors messages,
				TwitchConnectionDescriptor twitchConnection,
				SpeechRecognitionDescriptor speechRecognition)
			{
				var settings = new AppSettings
				{
					Messages = messages,
					TwitchConnection = twitchConnection,
					SpeechRecognition = speechRecognition
				};

				string json = JsonSerializer.Serialize(settings, _options);
				File.WriteAllText(filePath, json);
			}

			public static void Deserialize(string filePath,
				out MessageDescriptors messages,
				out TwitchConnectionDescriptor twitchConnection,
				out SpeechRecognitionDescriptor speechRecognition)
			{
				if (!File.Exists(filePath))
				{
					throw new FileNotFoundException($"Файл настроек {filePath} не найден", filePath);
				}

				string json = File.ReadAllText(filePath);
				var settings = JsonSerializer.Deserialize<AppSettings>(json);

				messages = settings == null ? new MessageDescriptors() : settings.Messages;
				twitchConnection = settings == null ? new TwitchConnectionDescriptor() : settings.TwitchConnection;
				speechRecognition = settings == null ? new SpeechRecognitionDescriptor() :  settings.SpeechRecognition;
			}
		}
	}
}
