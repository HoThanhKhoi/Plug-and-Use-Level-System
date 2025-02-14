using System;

public interface IExperienceGainer
{
	event Action onCurrentExperienceChanged;
	event Action<int> onExperienceThresholdReached; // Notifies when XP reaches level-up threshold

	int CurrentExperience { get; set; }
	int RequiredExperience { get; } // XP needed for the next level
}
