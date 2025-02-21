using System;
using UnityEngine;

// Tracks experience points. Does not handle leveling logic directly.

[DefaultExecutionOrder(-40)]
public class ExperienceTracker : MonoBehaviour, IExperienceTracker
{
	[SerializeField] private LevelingCategory systemCategory;
	[SerializeField] private int currentExp;

	public event Action onCurrentExperienceChanged;

	public int CurrentExperience
	{
		get => currentExp;
		set
		{
			currentExp = value;
			onCurrentExperienceChanged?.Invoke();
		}
	}

	private void Awake()
	{
		// Find the progression on the same GameObject
		var progression = GetComponent<ILevelTracker>();
		if (progression == null)
		{
			Debug.LogError("ManualLevelingComponent: No ILevelProgression component found on this GameObject!");
			return;
		}

		// Register this pair (XP + progression) with the facade (LevelingService)
		LevelingService.Instance.RegisterLevelingSystem(systemCategory, this, progression);
	}

	private void OnDestroy()
	{
		if (LevelingService.Instance != null)
		{
			LevelingService.Instance.UnregisterLevelingSystem(systemCategory);
		}
	}

	public LevelDataListSO GetLevelDataList()
	{
		// Not used in this design, as the progression/strategy handle the data.
		return null;
	}

	public void ResetExperience()
	{
		CurrentExperience = 0;
		Debug.Log($"{systemCategory} XP reset to 0.");
	}

}
