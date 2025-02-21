using UnityEngine;

public class TestEnemy : MonoBehaviour
{
	[SerializeField] private XPRewardTableSO xpTable;
	[SerializeField] private XPEventType eventType = XPEventType.Kill_WeakEnemy;
	[SerializeField] private LevelingCategory category = LevelingCategory.Player;

	public void SimulateDeath()
	{
		int xp = xpTable.GetXP(eventType);
		LevelingService.Instance.AddExperience(category, xp);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			SimulateDeath();
		}
	}
}
