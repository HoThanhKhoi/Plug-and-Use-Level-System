using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-30)]
public class ExpBarUI : MonoBehaviour
{
	[SerializeField] private Slider xpBar;
	[SerializeField] private LevelingCategory category = LevelingCategory.Player;

	private IExperienceTracker xpGainer;
	private ILevelTracker levelProgression;

	private void Start()
	{
		xpGainer = LevelingService.Instance.GetExperienceGainer(category);
		levelProgression = LevelingService.Instance.GetLevelProgression(category);

		if (xpGainer == null || levelProgression == null)
		{
			Debug.LogError($"ExpBarUI: No leveling system found for {category}");
			return;
		}

		// Subscribe to XP and level changes
		xpGainer.onCurrentExperienceChanged += UpdateBar;
		levelProgression.onLevelChanged += UpdateBar;

		UpdateBar();
	}

	private void OnDestroy()
	{
		if (xpGainer != null) xpGainer.onCurrentExperienceChanged -= UpdateBar;
		if (levelProgression != null) levelProgression.onLevelChanged -= UpdateBar;
	}

	private void UpdateBar()
	{
		int currentXP = xpGainer.CurrentExperience;
		// We can cast to LevelTracker to access RequiredExperience if needed
		var tracker = levelProgression as LevelTracker;
		if (tracker == null)
		{
			xpBar.value = 0;
			return;
		}

		int requiredXP = tracker.RequiredExperience;
		// Simple fraction
		if (requiredXP <= 0) requiredXP = 1; // avoid division by zero

		float fillAmount = (float)currentXP / requiredXP;
		xpBar.value = Mathf.Clamp01(fillAmount);
	}
}
