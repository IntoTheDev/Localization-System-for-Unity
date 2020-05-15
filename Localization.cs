using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace ToolBox.Localization
{
	public static class Localization
	{
		[FilePath] private static string fileName = "Localization.csv";

		[NonSerialized] private static Dictionary<string, Dictionary<string, string>> localization = new Dictionary<string, Dictionary<string, string>>();
		[NonSerialized] public static string Language = "RUS";
		[NonSerialized] private static bool isUploaded = false;

		[NonSerialized] private static bool isEditorReady = false;
		[NonSerialized] private static string[] keys = null;
		[NonSerialized] private static string[] languages = null;

		public const string SAVE_KEY = "Language";

		private static void LoadText()
		{
			Language = PlayerPrefs.GetString(SAVE_KEY, "RUS");

			string[] GetLines(StreamReader reader)
			{
				string text = reader.ReadLine();
				string[] lines = text.Split(';');
				return lines;
			}

			string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

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

		public static string LocalizeText(string key) =>
			Localize(key);

		public static string LocalizeText(string key, params object[] args) =>
			Localize(key, args);

#if UNITY_EDITOR
		public static string[] GetEditorData()
		{
			if (isEditorReady)
				return keys;

			string[] GetLines(StreamReader reader)
			{
				string text = reader.ReadLine();
				string[] lines = text.Split(';');
				return lines;
			}

			string filePath = Path.Combine(fileName);

			using (StreamReader reader = new StreamReader(filePath))
			{
				string[] lines = GetLines(reader);

				int linesCount = lines.Length;

				languages = new string[linesCount];

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

				keys = new string[lists[0].Count];
				int keysCount = keys.Length;

				for (int i = 0; i < keysCount; i++)
					keys[i] = lists[0][i];

				isEditorReady = true;
				return keys;
			}
		}

		[Button(ButtonSizes.Medium)]
		private static void Reload() =>
			isEditorReady = false;
#endif

		private static string Localize(string key)
		{
			if (isUploaded == false)
				LoadText();

			if (localization[Language].TryGetValue(key, out string value))
				return value;
			else
				throw new KeyNotFoundException("Translation not found: " + key);
		}

		private static string Localize(string key, params object[] args)
		{
			string localizedText = Localize(key);

			return string.Format(localizedText, args);
		}
	}
}

