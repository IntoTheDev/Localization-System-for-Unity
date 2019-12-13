using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace ToolBox.Localization
{
	[DisallowMultipleComponent, RequireComponent(typeof(TMP_Text))]
	public class LocalizationTMP : MonoBehaviour
	{
		[SerializeField, FoldoutGroup("Components")] private TMP_Text textComponent = null;
		[SerializeField, FoldoutGroup("Data")] private string localizationKey = "";
#if UNITY_EDITOR
		[SerializeField, ReadOnly, FoldoutGroup("Debug")] private string debugText = "";
#endif

		private void Start() => textComponent.text = Localization.LocalizeText(localizationKey);

#if UNITY_EDITOR
		[Button("Localize"), FoldoutGroup("Debug")]
		private void Localize() => debugText = Localization.LocalizeText(localizationKey);
#endif
	}
}
