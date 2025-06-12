using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace Speech2Twitch
{
	public class TwitchMessager
	{
		private TwitchClient TwitchConnectionClient = new TwitchClient();
		private DataStructures.TwitchConnectionDescriptor ConnectionDescriptor;

		public TwitchMessager()
		{
			TwitchConnectionClient.OnConnected += OnConnected;
			TwitchConnectionClient.OnError += OnError;
		}

		public bool Connect(DataStructures.TwitchConnectionDescriptor InConnectionDescriptor)
		{
			if (TwitchConnectionClient.IsConnected)
			{
				EventLogger.Instance.Log($"Отключаемся от {ConnectionDescriptor.StreamChannelName}");
				TwitchConnectionClient.Disconnect();
			}
			bool ready = true;
			if (InConnectionDescriptor.LoginName.Length == 0)
			{
				EventLogger.Instance.Log("Введите в настройках ник автора, от имени которого будут отправляться сообщения");
				ready = false;
			}

			if (InConnectionDescriptor.TwitchToken.Length == 0)
			{
				EventLogger.Instance.Log("Введите в настройках токен авторизации для твича");
				ready = false;
			}

			if (InConnectionDescriptor.StreamChannelName.Length == 0)
			{
				EventLogger.Instance.Log("Введите в настройках канал, куда будут отправляться сообщения");
				ready = false;
			}

			if (!ready)
				return false;

			ConnectionDescriptor = InConnectionDescriptor;
			var credentials = new ConnectionCredentials(InConnectionDescriptor.LoginName, InConnectionDescriptor.TwitchToken);
			TwitchConnectionClient.Initialize(credentials, InConnectionDescriptor.StreamChannelName);
			TwitchConnectionClient.Connect();
			return true;
		}

		private void OnConnected(object? sender, OnConnectedArgs e)
		{
			EventLogger.Instance.Log($"✅ Успешно подключен к чату {ConnectionDescriptor.StreamChannelName}, под именем {e.BotUsername}!");
		}

		private void OnError(object? sender, EventArgs e)
		{
			EventLogger.Instance.Log($"Не удалось подключиться {e.ToString()}");
		}
		public bool SendMessage(string Message)
		{
			if (!TwitchConnectionClient.IsConnected)
			{
				EventLogger.Instance.Log("Нет подключения к твичу");
				return false;
			}

			TwitchConnectionClient.SendMessage(ConnectionDescriptor.StreamChannelName, Message);
			return true;
		}

	}
}
