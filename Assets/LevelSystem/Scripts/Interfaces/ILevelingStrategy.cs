using UnityEngine;

// A strategy that calculates required XP for a given level.
// Different implementations can do manual table lookups, formulas, etc.

public interface ILevelingStrategy
{
	// Calculates the required experience for the next level.
	// <param name="levelDataList">A ScriptableObject holding level data (XP thresholds).</param>
	// <param name="currentLevel">The current level.</param>
	// <returns>The required XP for the next level.</returns>
	int CalculateRequiredExperience(LevelDataListSO levelDataList, int currentLevel);
}
