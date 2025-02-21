using System;

// Represents a component that manages level progression (current level, etc.).
public interface ILevelTracker
{
	// Fired when the level changes (e.g., after leveling up).
	event Action onLevelChanged;

	// The current level (1-based, 0-based, or whichever convention you prefer).
	int Level { get; }
}
