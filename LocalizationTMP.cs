using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace ToolBox.Localization
{
	public class LocalizationTMP : MonoBehaviour
	{
		[SerializeField, Required, ChildGameObjectsOnly] private TMP_Text _textComponent = null;
		[SerializeField] private LocalizedString _text = default;

		private void OnEnable() =>
			_textComponent.text = _text.Value;

		public void Translate() =>
			_textComponent.text = _text.Value;
	}
}
