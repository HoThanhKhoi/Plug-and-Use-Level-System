using System.Collections.Generic;
using UnityEngine;

// A concrete facade that implements ILevelingFacade, acting as the single "door" for
// registering, looking up, and updating leveling systems at runtime.
// Optionally references LevelingRegistrySO for persistent or cross-scene storage.
public class LevelingService : MonoBehaviour, ILevelingFacade
{
	public static LevelingService Instance { get; private set; }

	[Header("Optional Registry")]
	[SerializeField] private LevelingRegistrySO levelingRegistry;

	// Runtime dictionaries for quick lookups
	private Dictionary<LevelingCategory, IExperienceGainer> experienceGainers
		= new Dictionary<LevelingCategory, IExperienceGainer>();

	private Dictionary<LevelingCategory, ILevelProgression> levelProgressions
		= new Dictionary<LevelingCategory, ILevelProgression>();

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);

		// If you want to auto-search for a registry:
		if (!levelingRegistry)
		{
			levelingRegistry = FindObjectOfType<LevelingRegistrySO>();
		}
	}



	public void ResetSystem(LevelingCategory category)
	{
		if (experienceGainers.TryGetValue(category, out var xpGainer))
		{
			(xpGainer as ExperienceTracker)?.ResetExperience();
		}

		if (levelProgressions.TryGetValue(category, out var prog))
		{
			(prog as LevelTracker)?.ResetLevel();
		}
	}

	#region ILevelingFacade Implementation

	public void RegisterLevelingSystem(LevelingCategory category, IExperienceGainer xpGainer, ILevelProgression progression)
	{
		if (experienceGainers.ContainsKey(category))
		{
			Debug.LogWarning($"Leveling system for {category} is already registered.");
			return;
		}

		experienceGainers[category] = xpGainer;
		levelProgressions[category] = progression;

		// Also store in the registry if assigned
		if (levelingRegistry)
		{
			levelingRegistry.Register(category, xpGainer, progression);
		}

		Debug.Log($"Registered leveling system for {category}.");
	}

	public void UnregisterLevelingSystem(LevelingCategory category)
	{
		experienceGainers.Remove(category);
		levelProgressions.Remove(category);

		if (levelingRegistry)
		{
			levelingRegistry.Unregister(category);
		}

		Debug.Log($"Unregistered leveling system for {category}.");
	}

	public IExperienceGainer GetExperienceGainer(LevelingCategory category)
	{
		experienceGainers.TryGetValue(category, out var xp);
		return xp;
	}

	public ILevelProgression GetLevelProgression(LevelingCategory category)
	{
		levelProgressions.TryGetValue(category, out var prog);
		return prog;
	}

	// Adds XP to the specified category, checks if threshold is reached, and triggers a level-up if so.
	public void AddExperience(LevelingCategory category, int amount)
	{
		if (!experienceGainers.TryGetValue(category, out var xpGainer) ||
			!levelProgressions.TryGetValue(category, out var progression))
		{
			Debug.LogWarning($"No leveling system found for {category} when adding XP.");
			return;
		}

		// Increase XP
		xpGainer.CurrentExperience += amount;
		Debug.Log($"Added {amount} XP to {category}. Current XP: {xpGainer.CurrentExperience}");

		// Check thresholds
		var levelingProg = progression as LevelTracker;
		if (levelingProg == null)
		{
			Debug.LogWarning($"Level progression for {category} is not a LevelingProgression component.");
			return;
		}

		// While XP is sufficient to level up, keep leveling
		while (xpGainer.CurrentExperience >= levelingProg.RequiredExperience &&
			   levelingProg.CanLevelUp())
		{
			xpGainer.CurrentExperience -= levelingProg.RequiredExperience;
			levelingProg.PerformLevelUp();
		}
	}

	#endregion
}
