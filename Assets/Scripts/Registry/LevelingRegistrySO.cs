using System.Collections.Generic;
using UnityEngine;

// Optional: Stores references to leveling systems (XP + progression) for easy debugging or cross-scene usage.
// If you don't need persistent references, you can skip this and rely on the facade's runtime dictionaries only.

[CreateAssetMenu(fileName = "LevelingRegistry", menuName = "Leveling/Registry")]
public class LevelingRegistrySO : ScriptableObject
{
	// Example dictionaries if you want to store references in a ScriptableObject.
	private Dictionary<LevelingCategory, IExperienceGainer> xpGainers
		= new Dictionary<LevelingCategory, IExperienceGainer>();

	private Dictionary<LevelingCategory, ILevelProgression> levelProgressions
		= new Dictionary<LevelingCategory, ILevelProgression>();

	public void Register(LevelingCategory category, IExperienceGainer xp, ILevelProgression progression)
	{
		if (xpGainers.ContainsKey(category))
		{
			Debug.LogWarning($"Leveling system for {category} is already stored in LevelingRegistrySO.");
			return;
		}

		xpGainers[category] = xp;
		levelProgressions[category] = progression;

		Debug.Log($"Leveling system for {category} stored in LevelingRegistrySO.");
	}

	public IExperienceGainer GetExperienceGainer(LevelingCategory category)
	{
		xpGainers.TryGetValue(category, out var xp);
		return xp;
	}

	public ILevelProgression GetLevelProgression(LevelingCategory category)
	{
		levelProgressions.TryGetValue(category, out var prog);
		return prog;
	}

	public void Unregister(LevelingCategory category)
	{
		xpGainers.Remove(category);
		levelProgressions.Remove(category);
		Debug.Log($"Leveling system for {category} unregistered from LevelingRegistrySO.");
	}
}
