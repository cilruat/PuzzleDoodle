using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObj_ChangeDirection : LevelObjectBase
{
    public override ELevelObjectType Type => ELevelObjectType.CHANGE_DIRECTION;

    public override void OnEnter(Runner runner)
    {
        runner.ChangeMoveDirection();
    }
}
