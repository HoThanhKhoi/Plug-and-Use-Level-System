using System;
using UnityEngine;

public class LevelingProgression : MonoBehaviour, ILevelProgression
{
	[SerializeField] private int currentLevel = 1;
	private IExperienceGainer experienceGainer;
	private LevelDataListSO levelDataList; // Store reference to ScriptableObject

	public int Level => currentLevel;
	public event Action onLevelChanged;

	private void Start()
	{
		experienceGainer = GetComponent<IExperienceGainer>();

		if (experienceGainer != null)
		{
			levelDataList = experienceGainer?.GetLevelDataList();
			experienceGainer.onExperienceThresholdReached += HandleLevelUp;
		}
		else
		{
			Debug.LogError($"No IExperienceGainer found on GameObject {gameObject.name}!");
		}
	}

	private void OnDestroy()
	{
		if (experienceGainer != null)
		{
			experienceGainer.onExperienceThresholdReached -= HandleLevelUp;
		}
	}

	private void HandleLevelUp(int remainingXP)
	{
		if (levelDataList == null || currentLevel >= levelDataList.LevelDataList.Count)
		{
			Debug.Log("Max level reached! Cannot level up further.");
			return; // Stop leveling up if max level reached
		}

		currentLevel++;
		Debug.Log($"Level Up! New Level: {currentLevel}");
		onLevelChanged?.Invoke();

		// Subtract required XP instead of resetting to 0
		int xpAfterLevelUp = remainingXP - experienceGainer.RequiredExperience;
		experienceGainer.CurrentExperience = Mathf.Max(xpAfterLevelUp, 0);
	}
}
