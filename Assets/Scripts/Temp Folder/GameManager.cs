using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private LevelingRegistrySO levelingRegistry;

	private void Awake()
	{
		Debug.Log("GameManager: Assigning Leveling Registry to LevelingService.");

		if (LevelingService.Instance.HasRegistry() == false) // Check if registry is missing
		{
			LevelingService.Instance.SetRegistry(levelingRegistry);
			Debug.Log("GameManager: Leveling Registry manually assigned.");
		}
	}
}
