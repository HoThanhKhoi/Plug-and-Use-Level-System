using UnityEngine;

[CreateAssetMenu(fileName = "FormulaStrategy", menuName = "Leveling/Strategies/Formula")]
public class FormulaLevelingStrategySO : LevelingStrategySO
{
	[SerializeField]
	private AnimationCurve xpCurve = new AnimationCurve(
	new Keyframe(1, 1000),
	new Keyframe(50, 100000)
	);

	public override int CalculateRequiredExperience(LevelDataListSO dataList, int currentLevel)
	{
		return Mathf.RoundToInt(xpCurve.Evaluate(currentLevel));
	}

}
