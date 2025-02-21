using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "XPRewardTable", menuName = "Leveling/XP Reward Table")]
public class XPRewardTableSO : ScriptableObject
{
	[Serializable]
	public struct XPRewardEntry
	{
		public XPEventType eventType;
		public int xpAmount;
	}

	[SerializeField] private List<XPRewardEntry> rewardEntries;

	// For quick lookups, you might want a dictionary at runtime
	private Dictionary<XPEventType, int> rewardDict;

	private void OnEnable()
	{
		rewardDict = new Dictionary<XPEventType, int>();
		foreach (var entry in rewardEntries)
		{
			rewardDict[entry.eventType] = entry.xpAmount;
		}
	}

	public int GetXP(XPEventType eventType)
	{
		if (rewardDict.TryGetValue(eventType, out int xp))
		{
			return xp;
		}
		return 0;
	}
}
