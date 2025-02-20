using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelingRegistry", menuName = "Leveling/Registry")]
public class LevelingRegistrySO : ScriptableObject
{
	private Dictionary<LevelingCategory, IExperienceGainer> xpGainers 
		= new Dictionary<LevelingCategory, IExperienceGainer>();
	private Dictionary<LevelingCategory, ILevelProgression> levelProgressions 
		= new Dictionary<LevelingCategory, ILevelProgression>();

	public void Register(LevelingCategory category, IExperienceGainer xpGainer, ILevelProgression levelProgression)
	{
		if (xpGainers.ContainsKey(category))
		{
			Debug.LogWarning($"Leveling system for {category} is already stored in LevelingRegistrySO.");
			return;
		}

		xpGainers[category] = xpGainer;
		levelProgressions[category] = levelProgression;

		Debug.Log($"Leveling system for {category} stored in LevelingRegistrySO.");
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
