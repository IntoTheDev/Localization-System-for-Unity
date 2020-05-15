using Sirenix.OdinInspector;
using UnityEngine;

namespace ToolBox.Localization
{
	[System.Serializable]
	public class LocalizedString
	{
		[SerializeField, ValueDropdown("GetKeys")] private string localizationKey = "";
		[SerializeField, ReadOnly] private string value = "";
		[SerializeField, ReadOnly] private string currentLocalizationKey = "";
		[SerializeField, ReadOnly] private bool isInitialized = false;

		private string language;

		public string Value
		{
			get
			{
				string newLanguage = Localization.Language;

				if (!isInitialized)
				{
					language = newLanguage;
					value = Localization.LocalizeText(localizationKey);
					isInitialized = true;
					return value;
				}

				if (newLanguage != language)
				{
					value = Localization.LocalizeText(localizationKey);
					language = newLanguage;
					return value;
				}

				return value;
			}
			set
			{
				if (value == currentLocalizationKey)
					return;

				this.value = Localization.LocalizeText(value);
				currentLocalizationKey = value;
			}
		}

#if UNITY_EDITOR
		private string[] GetKeys() =>
			Localization.GetEditorData();
#endif
	}
}
