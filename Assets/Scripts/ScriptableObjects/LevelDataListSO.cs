using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataListSO", menuName = "Scriptable Objects/Level Data/LevelDataListSO")]
public class LevelDataListSO : ScriptableObject
{
    public List<LevelDataSO> LevelDataList;
}
