using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Interfaces;

namespace Speech2Twitch
{
	public class EventLogger
	{
		public static EventLogger Instance = new EventLogger();

		public EventLogger()
		{}
		private const int MaxLogLines = 16;
		private TextBox? txtLog;

		public void SetTextBox(TextBox InTextBox)
		{
			txtLog = InTextBox;
		}
		public void Log(string message)
		{
			if (txtLog == null)
			{
				Console.WriteLine(message);
				return;
			}
			if (txtLog.InvokeRequired)
			{
				txtLog.Invoke(new Action(() => Log(message)));
				return;
			}

			// Добавляем новую строку
			txtLog.AppendText($"{message}{Environment.NewLine}");

			// Ограничение по числу строк
			var lines = txtLog.Lines;
			if (lines.Length > MaxLogLines)
			{
				// Обрезаем лишние строки (оставляем последние MaxLogLines)
				txtLog.Lines = lines.Skip(lines.Length - MaxLogLines).ToArray();
			}
		}
	}
}
