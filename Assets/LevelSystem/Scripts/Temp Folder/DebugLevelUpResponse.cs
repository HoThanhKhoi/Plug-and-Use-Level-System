using UnityEngine;

public class DebugLevelUpResponse : MonoBehaviour
{
	public void OnPlayerLevelUp(int oldLevel, int newLevel, LevelingCategory category)
	{
		Debug.Log($"[DebugLevelUpResponse] {category} leveled from {oldLevel} to {newLevel}!");
	}
}
