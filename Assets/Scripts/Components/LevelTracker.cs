using System;
using UnityEngine;

// Manages current level, references a leveling table (LevelDataListSO),
// and uses a leveling strategy (ManualLevelingStrategy) to compute required XP.

[DefaultExecutionOrder(-40)]
public class LevelTracker : MonoBehaviour, ILevelTracker
{
	[SerializeField] private int currentLevel = 1;
	[SerializeField] private LevelDataListSO levelDataList;
	public LevelDataListSO LevelDataList => levelDataList;

	[Header("Optional: Raise an SO event on level up")]
	[SerializeField] private LevelUpEventSO levelUpEvent;

	[SerializeField] private LevelingCategory category = LevelingCategory.Player;

	[SerializeField] private LevelingStrategySO levelingStrategySO;

	private ILevelingStrategy levelingStrategy;
	public event Action onLevelChanged;
	public int Level => currentLevel;


	private void Awake()
	{
		// If you need a fallback, check if levelingStrategySO is null:
		if (levelingStrategySO == null)
		{
			Debug.LogWarning("No leveling strategy assigned! Defaulting to manual strategy?");
			// levelingStrategySO = someDefaultStrategySO;
		}

		// Assign the interface reference
		levelingStrategy = levelingStrategySO;
	}


	public int RequiredExperience
	{
		get
		{
			if (levelDataList == null) return int.MaxValue;
			return levelingStrategy.CalculateRequiredExperience(levelDataList, currentLevel);
		}
	}

	public bool CanLevelUp()
	{
		if (levelDataList == null) return false;
		return currentLevel < levelDataList.LevelDataList.Count;
	}

	public void PerformLevelUp()
	{
		if (!CanLevelUp())
		{
			Debug.Log($"{category} has reached max level: {currentLevel}");
			return;
		}

		int oldLevel = currentLevel;
		currentLevel++;

		Debug.Log($"{category} leveled from {oldLevel} to {currentLevel}.");

		// STILL call onLevelChanged for direct subscribers if needed:
		onLevelChanged?.Invoke();

		// RAISE the ScriptableObject event for decoupled listeners
		if (levelUpEvent != null)
		{
			levelUpEvent.Raise(oldLevel, currentLevel, category);
		}
	}

	public void ResetLevel()
	{
		currentLevel = 1;
		Debug.Log($"{category} level reset to 1.");
	}

}
