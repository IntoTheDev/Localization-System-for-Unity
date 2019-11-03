using TMPro;
using UnityEngine;

namespace ToolBox.Localization
{
	public class LocalizationTMP : MonoBehaviour
	{
		[SerializeField] private TMP_Text textComponent = null;
		[SerializeField] private string localizationKey = "";

		private void Start() => textComponent.text = Localization.LocalizeText(localizationKey);
	}
}
