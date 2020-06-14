using Sirenix.OdinInspector;
using UnityEngine;

namespace ToolBox.Localization
{
	[System.Serializable]
	public class LocalizedString
	{
		[SerializeField, ValueDropdown("GetKeys")] private string _localizationKey = "";
		[SerializeField, ReadOnly] private string _value = "";
		[SerializeField, ReadOnly] private string _currentLocalizationKey = "";
		[SerializeField, ReadOnly] private bool _isInitialized = false;

		private string _language = "";

		public string Value
		{
			get
			{
				string newLanguage = Localization.Language;

				if (!_isInitialized)
				{
					_language = newLanguage;
					_value = Localization.LocalizeText(_localizationKey);
					_isInitialized = true;

					return _value;
				}

				if (newLanguage != _language)
				{
					_value = Localization.LocalizeText(_localizationKey);
					_language = newLanguage;

					return _value;
				}

				return _value;
			}
			set
			{
				if (value == _currentLocalizationKey)
					return;

				_value = Localization.LocalizeText(value);
				_currentLocalizationKey = value;
			}
		}

#if UNITY_EDITOR
		private string[] GetKeys() =>
			Localization.GetEditorData();
#endif
	}
}
