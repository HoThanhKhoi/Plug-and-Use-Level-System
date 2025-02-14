using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelingRegistry", menuName = "Leveling/Registry")]
public class LevelingRegistrySO : ScriptableObject
{
	private Dictionary<LevelingCategory, IExperienceGainer> xpGainers = new Dictionary<LevelingCategory, IExperienceGainer>();
	private Dictionary<LevelingCategory, ILevelProgression> levelProgressions = new Dictionary<LevelingCategory, ILevelProgression>();

	public void Register(LevelingCategory category)
	{
		if (LevelingService.Instance.TryGetLevelingSystem(category, out var xp, out var level))
		{
			xpGainers[category] = xp;
			levelProgressions[category] = level;
			Debug.Log($"LevelingRegistry: {category} registered successfully.");
		}
		else
		{
			Debug.LogError($"LevelingRegistry: No leveling system found for {category}!");
		}
	}

	public IExperienceGainer GetExperienceGainer(LevelingCategory category)
	{
		return xpGainers.TryGetValue(category, out var xp) ? xp : null;
	}

	public ILevelProgression GetLevelProgression(LevelingCategory category)
	{
		return levelProgressions.TryGetValue(category, out var level) ? level : null;
	}
}
