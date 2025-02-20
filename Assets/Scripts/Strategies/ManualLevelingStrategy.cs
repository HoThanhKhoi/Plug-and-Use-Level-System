using UnityEngine;

[CreateAssetMenu(fileName = "ManualStrategy", menuName = "Leveling/Strategies/Manual")]
public class ManualLevelingStrategySO : LevelingStrategySO
{
	// If needed, you can reference a default LevelDataListSO or keep it null
	// [SerializeField] private LevelDataListSO defaultDataList;

	public override int CalculateRequiredExperience(LevelDataListSO levelDataList, int currentLevel)
	{
		if (levelDataList == null ||
			levelDataList.LevelDataList == null ||
			levelDataList.LevelDataList.Count == 0)
		{
			Debug.LogWarning("ManualLevelingStrategySO: Level data list is null or empty.");
			return 0;
		}

		int clampedLevel = Mathf.Clamp(currentLevel, 0, levelDataList.LevelDataList.Count - 1);
		return levelDataList.LevelDataList[clampedLevel].requiredExp;
	}
}
