using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private LevelingRegistrySO levelingRegistry;

	private void Awake()
	{
		if (levelingRegistry != null)
		{
			Debug.Log("GameManager: Assigning Leveling Registry to LevelingService.");
			LevelingService.Instance.SetRegistry(levelingRegistry);
		}
	}
}
