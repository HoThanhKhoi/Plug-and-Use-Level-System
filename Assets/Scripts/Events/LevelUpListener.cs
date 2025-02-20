using UnityEngine;
using UnityEngine.Events;

public class LevelUpListener : MonoBehaviour
{
	[SerializeField] private LevelUpEventSO levelUpEvent;

	// Exposed in inspector so you can add multiple responses
	[SerializeField] private UnityEvent<int, int, LevelingCategory> onLevelUpResponse;

	private void OnEnable()
	{
		if (levelUpEvent != null)
		{
			levelUpEvent.RegisterListener(OnEventRaised);
		}
	}

	private void OnDisable()
	{
		if (levelUpEvent != null)
		{
			levelUpEvent.UnregisterListener(OnEventRaised);
		}
	}

	private void OnEventRaised(int oldLevel, int newLevel, LevelingCategory category)
	{
		// Trigger whatever responses are configured in the inspector
		onLevelUpResponse.Invoke(oldLevel, newLevel, category);
	}
}
