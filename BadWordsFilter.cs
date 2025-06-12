using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Speech2Twitch
{
	public class BadWordsFilter
	{
		private static readonly string DefaultPath = "badwords.json";
		private List<string> BadWords = new();
		private List<Regex> BadWordRegexes = new();

		public BadWordsFilter(){}

		public bool LoadOrInitialize(string path)
		{
			string FullPath = path.EndsWith('\\') ? path + DefaultPath : path + "\\" + DefaultPath;
			if (File.Exists(FullPath))
			{
				try
				{
					string json = File.ReadAllText(FullPath);
					var list = JsonSerializer.Deserialize<List<string>>(json);
					if (list != null)
						BadWords = list;
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine($"Ошибка загрузки слов из файла: {ex.Message}");
					BadWords = GetDefaultBadWords();
				}
			}
			else
			{
				BadWords = GetDefaultBadWords();
				Save(FullPath);
				return false;
			}

			CompileRegexes();
			return true;
		}

		public void Save(string path)
		{
			var json = JsonSerializer.Serialize(BadWords, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
			File.WriteAllText(path, json);
		}

		public string Filter(string input)
		{
			foreach (var regex in BadWordRegexes)
			{
				input = regex.Replace(input, "[ПЛОХОЕ СЛОВО]");
			}

			return input;
		}

		public void AddWord(string word)
		{
			if (!BadWords.Contains(word, StringComparer.OrdinalIgnoreCase))
			{
				BadWords.Add(word);
				CompileRegexes();
			}
		}

		public void RemoveWord(string word)
		{
			BadWords.RemoveAll(w => string.Equals(w, word, StringComparison.OrdinalIgnoreCase));
			CompileRegexes();
		}

		private void CompileRegexes()
		{
			BadWordRegexes = new List<Regex>();
			foreach (var word in BadWords)
			{
				var pattern = Regex.Escape(word);
				BadWordRegexes.Add(new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled));
			}
		}

		private static List<string> GetDefaultBadWords()
		{
			return new List<string>
			{
				"бля",
				"сука",
				"хуй",
				"хуя",
				"хуе",
				"пизд",
				"ебат",
				"еби",
				"ебет",
				"ебёт",
				"ебан",
				"гандон",
				"мудак",
				"долбо",
				"пидор",
				"чмо"
			};
		}
	}
}

