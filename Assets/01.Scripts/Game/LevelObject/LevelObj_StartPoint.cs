using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObj_StartPoint : LevelObjectBase
{
    public override ELevelObjectType Type => ELevelObjectType.START_POINT;
    
    public override void OnEnter(Runner runner)
    {
    }
}
