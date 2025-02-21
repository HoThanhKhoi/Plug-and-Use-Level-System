using UnityEngine;
using TMPro;

[DefaultExecutionOrder(-30)]
public class LevelTextUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI levelText;
	[SerializeField] private LevelingCategory category = LevelingCategory.Player;

	private ILevelTracker levelProgression;

	private void Start()
	{
		// Fetch references from the facade
		levelProgression = LevelingService.Instance.GetLevelProgression(category);

		if (levelProgression == null)
		{
			Debug.LogError($"LevelTextUI: No level progression found for {category}");
			return;
		}

		// Subscribe to the onLevelChanged event
		levelProgression.onLevelChanged += UpdateLevelText;

		// Initial update
		UpdateLevelText();
	}

	private void OnDestroy()
	{
		if (levelProgression != null)
			levelProgression.onLevelChanged -= UpdateLevelText;
	}

	private void UpdateLevelText()
	{
		// Just display the level
		levelText.text = $"Level: {levelProgression.Level}";
	}
}
