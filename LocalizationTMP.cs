using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace ToolBox.Localization
{
	public class LocalizationTMP : MonoBehaviour
	{
		[SerializeField, Required, ChildGameObjectsOnly] private TMP_Text textComponent = null;
		[SerializeField] private LocalizedString text = default;

		private void OnEnable() =>
			textComponent.text = text.Value;

		public void Translate() =>
			textComponent.text = text.Value;
	}
}
