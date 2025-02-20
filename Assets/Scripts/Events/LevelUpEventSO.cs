using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Level Up Event")]
public class LevelUpEventSO : ScriptableObject
{
	// A UnityEvent that includes oldLevel, newLevel, and category
	private UnityEvent<int, int, LevelingCategory> onLevelUp
		= new UnityEvent<int, int, LevelingCategory>();

	public void Raise(int oldLevel, int newLevel, LevelingCategory category)
	{
		onLevelUp.Invoke(oldLevel, newLevel, category);
	}

	public void RegisterListener(UnityAction<int, int, LevelingCategory> listener)
	{
		onLevelUp.AddListener(listener);
	}

	public void UnregisterListener(UnityAction<int, int, LevelingCategory> listener)
	{
		onLevelUp.RemoveListener(listener);
	}
}
