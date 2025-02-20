// A facade that external systems can use to interact with the leveling system
// without referencing specific MonoBehaviours or singletons.

public interface ILevelingFacade
{
	// Registers a leveling system for a given category.
	void RegisterLevelingSystem(LevelingCategory category, IExperienceGainer xpGainer, ILevelProgression levelProgression);

	// Unregisters a leveling system for the given category.
	void UnregisterLevelingSystem(LevelingCategory category);

	// Retrieves the IExperienceGainer for the given category, if registered.
	IExperienceGainer GetExperienceGainer(LevelingCategory category);

	// Retrieves the ILevelProgression for the given category, if registered.
	ILevelProgression GetLevelProgression(LevelingCategory category);

	// Adds experience to the system for the specified category,
	// potentially triggering a level-up if thresholds are met.
	void AddExperience(LevelingCategory category, int amount);
}
