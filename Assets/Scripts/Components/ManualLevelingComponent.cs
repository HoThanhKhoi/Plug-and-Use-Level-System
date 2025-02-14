using System;
using UnityEngine;

public class ManualLevelingComponent : MonoBehaviour, IExperienceGainer
{
	[SerializeField] private LevelingCategory systemCategory;
	[SerializeField] private LevelDataListSO requiredExpTable;
	[SerializeField] private int currentExp;

	public LevelDataListSO RequiredExpTable
	{
		get => requiredExpTable;
#if UNITY_EDITOR
		set => requiredExpTable = value; // Allow setting in tests
#endif
	}

	public int CurrentExperience
	{
		get => currentExp;
		set
		{
			currentExp = value;
			onCurrentExperienceChanged?.Invoke();
			CheckForLevelUp(); // Instead of leveling up here, notify LevelingSystem.
		}
	}

	public int RequiredExperience
	{
		get
		{
			var levelProgression = LevelingService.Instance.GetLevelProgression(systemCategory);
			if (levelProgression == null) return 0;

			return requiredExpTable.LevelDataList[Mathf.Clamp
				(levelProgression.Level, 0, requiredExpTable.LevelDataList.Count - 1)].requiredExp;
		}
	}

	public event Action onCurrentExperienceChanged;
	public event Action<int> onExperienceThresholdReached; // Notify when XP is enough for leveling.

	private void Start()
	{
		LevelingService.Instance.RegisterLevelingSystem(systemCategory, this, GetComponent<ILevelProgression>());
	}

	private void OnDestroy()
	{
		LevelingService.Instance.UnregisterLevelingSystem(systemCategory);
	}

	private void CheckForLevelUp()
	{
		if (currentExp >= RequiredExperience)
		{
			onExperienceThresholdReached?.Invoke(currentExp); // Notify LevelingSystem instead of handling level-up here
		}
	}
}
