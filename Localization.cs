using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ToolBox.Localization
{
	public static class Localization
	{
		private static Dictionary<string, Dictionary<string, string>> localization = new Dictionary<string, Dictionary<string, string>>();
		private static string language = "English";
		private static bool isUploaded = false;

		private static void LoadText()
		{
			string[] GetLines(StreamReader reader)
			{
				string text = reader.ReadLine();
				string[] lines = text.Split(';');
				return lines;
			}

			string filePath = Path.Combine(Application.streamingAssetsPath, "Localization.csv");

			using (StreamReader reader = new StreamReader(filePath))
			{
				string[] lines = GetLines(reader);

				int linesCount = lines.Length;
				string[] languages = new string[linesCount];

				for (int i = 1; i < linesCount; i++)
					languages[i] = lines[i];

				List<string>[] lists = new List<string>[linesCount];
				int listsCount = lists.Length;

				for (int i = 0; i < listsCount; i++)
					lists[i] = new List<string>();

				while (!reader.EndOfStream)
				{
					string[] values = GetLines(reader);

					for (int i = 0; i < listsCount; i++)
						lists[i].Add(values[i]);
				}

				string[] keyWords = new string[lists[0].Count];
				int keyWordsCount = keyWords.Length;

				for (int i = 0; i < keyWordsCount; i++)
					keyWords[i] = lists[0][i];


				for (int i = 1; i < listsCount; i++)
					localization.Add(languages[i], new Dictionary<string, string>());

				for (int i = 1; i < listsCount; i++)
				{
					int listSize = lists[i].Count;

					for (int j = 0; j < listSize; j++)
						localization[languages[i]].Add(keyWords[j], lists[i][j]);
				}

				reader.Close();
				reader.Dispose();

				isUploaded = true;
			}
		}

		private static string Localize(string key)
		{
			if (isUploaded == false)
				LoadText();

			if (!localization[language].ContainsKey(key))
				throw new KeyNotFoundException("Translation not found: " + key);

			return localization[language][key];
		}

		private static string Localize(string key, params object[] args)
		{
			string localizedText = Localize(key);

			return string.Format(localizedText, args);
		}

		public static string LocalizeText(string key) => Localize(key);

		public static string LocalizeText(string key, params object[] args) => Localize(key, args);
	}
}

