using System;

// Represents a component that tracks and emits experience changes.
public interface IExperienceTracker
{
	// Fired when CurrentExperience is changed (useful for UI or other listeners).
	event Action onCurrentExperienceChanged;

	// The current accumulated experience.
	int CurrentExperience { get; set; }

	// The actual level data is handled by ILevelProgression.
	// But if you need data references here, you can return them.
	LevelDataListSO GetLevelDataList();
}
