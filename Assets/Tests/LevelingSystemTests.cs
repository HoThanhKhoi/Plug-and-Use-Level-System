using NUnit.Framework; // Unity's testing framework
using UnityEngine;
using System;

public class LevelingSystemTests
{
	private LevelingProgression levelingProgression;
	private ManualLevelingComponent xpGainer;
	private LevelDataListSO mockLevelData;

	[SetUp] // Runs before each test
	public void Setup()
	{
		// Create a new GameObject to simulate Unity behavior
		GameObject testObject = new GameObject("TestObject");

		// Attach Leveling Components
		levelingProgression = testObject.AddComponent<LevelingProgression>();
		xpGainer = testObject.AddComponent<ManualLevelingComponent>();

		mockLevelData = ScriptableObject.CreateInstance<LevelDataListSO>();
		mockLevelData.LevelDataList = new System.Collections.Generic.List<LevelDataSO>();

		LevelDataSO level1 = ScriptableObject.CreateInstance<LevelDataSO>();
		level1.level = 1;
		level1.requiredExp = 10;

		LevelDataSO level2 = ScriptableObject.CreateInstance<LevelDataSO>();
		level2.level = 2;
		level2.requiredExp = 20;

		LevelDataSO level3 = ScriptableObject.CreateInstance<LevelDataSO>();
		level3.level = 3;
		level3.requiredExp = 30;

		mockLevelData.LevelDataList.Add(level1);
		mockLevelData.LevelDataList.Add(level2);
		mockLevelData.LevelDataList.Add(level3);

#if UNITY_EDITOR
		xpGainer.RequiredExpTable = mockLevelData;
#endif
		xpGainer.CurrentExperience = 0;

		if (levelingProgression != null && xpGainer != null)
		{
			xpGainer.onExperienceThresholdReached += (remainingXP) =>
				levelingProgression.SendMessage("HandleLevelUp", remainingXP);
		}
		else
		{
			Debug.LogError("LevelingProgression or XP Gainer is null! Test may fail.");
		}

	}

	[Test]
	public void XP_IncreasesCorrectly()
	{
		xpGainer.CurrentExperience = 5;
		Assert.AreEqual(5, xpGainer.CurrentExperience); // XP should be 5
	}

	[Test]
	public void LevelUp_WhenXPReachesThreshold()
	{
		xpGainer.CurrentExperience = 10; // Required for Level 2
		Assert.AreEqual(2, levelingProgression.Level); // Should level up to 2
	}

	[Test]
	public void LevelUp_CapsAtMax()
	{
		xpGainer.CurrentExperience = 100; // Beyond max level data
		Assert.AreEqual(3, levelingProgression.Level); // Should cap at level 3
	}

	[Test]
	public void XP_CarriesOver_AfterLevelUp()
	{
		xpGainer.CurrentExperience = 15; // 10 required, 5 extra should remain
		Assert.AreEqual(5, xpGainer.CurrentExperience); // XP should carry over
	}
}
