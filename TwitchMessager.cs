using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.WinForms;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace Speech2Twitch
{
	public class TwitchOAuthForm : Form
	{
		private WebView2 webView;

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string? AccessToken { get; private set; }

		private string ClientID;
		public TwitchOAuthForm(ref DataStructures.TwitchConnectionDescriptor InConnectionDescriptor)
		{
			this.ClientID = "m42egaaii8j6ce1y2xuymn4xto5x7x";
			this.Text = "Twitch Authorization";
			this.Width = 800;
			this.Height = 1000;

			webView = new WebView2
			{
				Dock = DockStyle.Fill
			};
			webView.NavigationCompleted += WebView_NavigationCompleted;
			this.Controls.Add(webView);

			this.Load += TwitchOAuthForm_Load;
		}

		private async void TwitchOAuthForm_Load(object? sender, EventArgs e)
		{
			await webView.EnsureCoreWebView2Async();

			string redirectUri = "http://localhost";  // должен быть в настройках приложения на dev.twitch.tv
			string scope = "chat:read+chat:edit";
			string authUrl = $"https://id.twitch.tv/oauth2/authorize" +
			                 $"?client_id={ClientID}" +
			                 $"&redirect_uri={redirectUri}" +
			                 $"&response_type=token" +
			                 $"&scope={scope}";

			webView.Source = new Uri(authUrl);
		}

		private void WebView_NavigationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
		{
			var uri = webView.Source;
			if (uri != null && uri.Fragment.Contains("access_token="))
			{
				var parts = uri.Fragment.TrimStart('#').Split('&');
				foreach (var part in parts)
				{
					if (part.StartsWith("access_token="))
					{
						AccessToken = part.Substring("access_token=".Length);
						EventLogger.Instance.Log("Токен получен!");
						this.DialogResult = DialogResult.OK;
						this.Close();
						break;
					}
				}
			}
			else
			{
				if (uri != null)
				{
					if (uri.Fragment.Length > 0)
						EventLogger.Instance.Log($"Не удалось получить токен: {uri.Fragment}");
				}
				else
					EventLogger.Instance.Log($"Не удалось получить токен: e.Url был null");
			}
		}
	}
	public class TwitchMessager
	{
		private TwitchClient? TwitchConnectionClient;
		private DataStructures.TwitchConnectionDescriptor ConnectionDescriptor;
		private string ActiveToken;
		public TwitchMessager()
		{
			ActiveToken = "";
		}

		public bool Connect(DataStructures.TwitchConnectionDescriptor InConnectionDescriptor, IWin32Window? owner)
		{
			if (TwitchConnectionClient != null && TwitchConnectionClient.IsConnected)
			{
				EventLogger.Instance.Log($"Отключаемся от {ConnectionDescriptor.StreamChannelName}");
				TwitchConnectionClient.OnConnected -= OnConnected;
				TwitchConnectionClient.OnError -= OnError;
				TwitchConnectionClient.Disconnect();
				TwitchConnectionClient = null;
			}
			bool ready = true;
			if (InConnectionDescriptor.LoginName.Length == 0)
			{
				EventLogger.Instance.Log("Введите в настройках ник автора, от имени которого будут отправляться сообщения");
				ready = false;
			}

			if (InConnectionDescriptor.StreamChannelName.Length == 0)
			{
				EventLogger.Instance.Log("Введите в настройках канал, куда будут отправляться сообщения");
				ready = false;
			}

			if (!ready)
				return false;

			ConnectionDescriptor.LoginName = InConnectionDescriptor.LoginName;
			ConnectionDescriptor.StreamChannelName = InConnectionDescriptor.StreamChannelName;
			if (ActiveToken.Length < 1)
			{
				ShowAuthDialog(ref ConnectionDescriptor, owner);
			}

			TwitchConnectionClient = new TwitchClient();
			var credentials = new ConnectionCredentials(ConnectionDescriptor.LoginName, ActiveToken);
			TwitchConnectionClient.Initialize(credentials, ConnectionDescriptor.StreamChannelName);
			TwitchConnectionClient.OnConnected += OnConnected;
			TwitchConnectionClient.OnError += OnError;
			TwitchConnectionClient.Connect();
			return true;
		}
		public void ShowAuthDialog(ref DataStructures.TwitchConnectionDescriptor InConnectionDescriptor, IWin32Window? owner)
		{
			var authForm = new TwitchOAuthForm(ref InConnectionDescriptor);
			authForm.StartPosition = FormStartPosition.CenterParent;
			if (authForm.ShowDialog(owner) == DialogResult.OK)
			{
				ActiveToken = authForm.AccessToken ?? "";
			}
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
			if (TwitchConnectionClient == null || !TwitchConnectionClient.IsConnected)
			{
				EventLogger.Instance.Log("Нет подключения к твичу");
				return false;
			}

			TwitchConnectionClient.SendMessage(ConnectionDescriptor.StreamChannelName, Message);
			return true;
		}

	}
}
