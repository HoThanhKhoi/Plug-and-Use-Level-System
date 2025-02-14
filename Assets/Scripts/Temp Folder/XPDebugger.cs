using UnityEngine;
using System.Collections;

public class XPDebugger : MonoBehaviour
{
	private IExperienceGainer playerXP;
	private ILevelProgression playerLevel;

	private void Start()
	{
		StartCoroutine(InitializeWithDelay()); // Delay initialization
	}

	private IEnumerator InitializeWithDelay()
	{
		yield return new WaitForSeconds(0.2f); // Wait for other systems to register

		LevelingAccessComponent accessComponent = GetComponent<LevelingAccessComponent>();

		if (accessComponent == null)
		{
			Debug.LogError("XPDebugger: No LevelingAccessComponent found on this GameObject.");
			yield break;
		}

		playerXP = accessComponent.GetExperienceGainer();
		playerLevel = accessComponent.GetLevelProgression();

		if (playerXP == null || playerLevel == null)
		{
			Debug.LogError("XPDebugger: Could not get XP system.");
			yield break;
		}

		playerXP.onCurrentExperienceChanged += UpdateXPDisplay;
		playerLevel.onLevelChanged += UpdateLevelDisplay;
	}

	private void UpdateXPDisplay()
	{
		Debug.Log($"[Player] XP: {playerXP.CurrentExperience}/{playerXP.RequiredExperience}");
	}

	private void UpdateLevelDisplay()
	{
		Debug.Log($"[Player] Leveled Up! New Level: {playerLevel.Level}");
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			playerXP.CurrentExperience += 1;
			Debug.Log("Added 1 XP.");
		}
	}
}
