using System;

public interface ILevelProgression
{
	event Action onLevelChanged;
	public int Level { get; }
}