using UnityEngine;

public abstract class LevelingStrategySO : ScriptableObject, ILevelingStrategy
{
	public abstract int CalculateRequiredExperience(LevelDataListSO levelDataList, int currentLevel);
}
