using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TestPlatform/StageData", order = 0)]
public class StageData : ScriptableObject
{
    public int StageNum;
    public bool isSpecialStage;
    public EMoveDirection startDirection;
    public StageObject LevelPrefab;    

    public List<ELevelObjectType> ObjectEntry = new List<ELevelObjectType>();
}

[System.Serializable]
public struct LevelObjData
{
    public ELevelObjectType Type;
    public Vector3 Position;
}
