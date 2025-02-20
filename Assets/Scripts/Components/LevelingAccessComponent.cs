using UnityEngine;
using System.Collections;

public class LevelingAccessComponent : MonoBehaviour
{
	private IExperienceGainer experienceGainer;
	private ILevelProgression levelProgression;

	private void Start()
	{
		StartCoroutine(FindLevelingSystem());
	}

	private IEnumerator FindLevelingSystem()
	{
		int retries = 5;

		while (retries > 0)
		{
			if (LevelingService.Instance.TryGetLevelingSystem(LevelingCategory.Player, out experienceGainer, out levelProgression))
			{
				Debug.Log($"LevelingAccessPoint for {LevelingCategory.Player} successfully registered.");
				yield break;
			}

			retries--;
			yield return new WaitForSeconds(0.1f); // Wait before retrying
		}

		Debug.LogError($"No leveling system found for {LevelingCategory.Player}!");
	}

	public IExperienceGainer GetExperienceGainer() => experienceGainer;
	public ILevelProgression GetLevelProgression() => levelProgression;
}
