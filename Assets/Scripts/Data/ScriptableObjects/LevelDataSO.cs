using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataSO", menuName = "Scriptable Objects/Level Data/LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
    public int level;
	public int requiredExp; // to level to the next level

}
