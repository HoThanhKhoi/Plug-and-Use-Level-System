using UnityEngine;
using System.Collections.Generic;

public class LevelingService : MonoBehaviour
{
	public static LevelingService Instance { get; private set; }

	[SerializeField] private LevelingRegistrySO levelingRegistry;
	private Dictionary<LevelingCategory, IExperienceGainer> experienceGainers = new();
	private Dictionary<LevelingCategory, ILevelProgression> levelProgressions = new();

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);

		EnsureRegistryExists();
	}


	private void EnsureRegistryExists()
	{
		if (levelingRegistry == null)
		{
			levelingRegistry = FindAnyObjectByType<LevelingRegistrySO>(); // Auto-search for registry
		}

		if (levelingRegistry == null)
		{
			Debug.LogWarning("LevelingService: No LevelingRegistrySO found! If using a GameManager, ensure it assigns one.");
			return;
		}
		else
		{
			Debug.Log("LevelingService: Leveling Registry found successfully.");
		}
	}

	// Allows GameManager to explicitly assign the registry
	public void SetRegistry(LevelingRegistrySO registry)
	{
		if (registry == null)
		{
			Debug.LogError("LevelingService: Received null registry!");
			return;
		}

		levelingRegistry = registry;
		Debug.Log("LevelingService: Leveling Registry manually assigned.");
	}

	public bool TryGetLevelingSystem(LevelingCategory category, out IExperienceGainer xpGainer, out ILevelProgression levelProgress)
	{
		xpGainer = null;
		levelProgress = null;
		return experienceGainers.TryGetValue(category, out xpGainer) &&
			   levelProgressions.TryGetValue(category, out levelProgress);
	}

	public void RegisterLevelingSystem(LevelingCategory category, IExperienceGainer xpGainer, ILevelProgression levelProgression)
	{
		if (experienceGainers.ContainsKey(category))
		{
			Debug.LogWarning($"Leveling system for {category} is already registered.");
			return;
		}

		experienceGainers[category] = xpGainer;
		levelProgressions[category] = levelProgression;

		Debug.Log($"Registered leveling system for {category}.");

		// Ensure LevelingRegistrySO also stores this system reference
		if (levelingRegistry != null)
		{
			levelingRegistry.Register(category, xpGainer, levelProgression);
		}
		else
		{
			Debug.LogWarning("LevelingRegistrySO is missing! Make sure it is assigned.");
		}
	}


	public void UnregisterLevelingSystem(LevelingCategory category)
	{
		if (experienceGainers.ContainsKey(category))
		{
			experienceGainers.Remove(category);
			levelProgressions.Remove(category);
			Debug.Log($"Unregistered leveling system for {category}.");
		}
	}

	public IExperienceGainer GetExperienceGainer(LevelingCategory category)
	{
		if (levelingRegistry == null) EnsureRegistryExists();
		return experienceGainers.TryGetValue(category, out var xpGainer) ? xpGainer : null;
	}

	public ILevelProgression GetLevelProgression(LevelingCategory category)
	{
		if (levelingRegistry == null) EnsureRegistryExists();
		return levelProgressions.TryGetValue(category, out var levelProgress) ? levelProgress : null;
	}

	public bool HasRegistry()
	{
		return levelingRegistry != null;
	}
}
