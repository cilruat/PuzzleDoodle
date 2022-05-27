using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObj_Shift : LevelObjectBase
{
    public EMoveDirection Direction;

    public override ELevelObjectType Type => (Direction == EMoveDirection.LEFT) ? ELevelObjectType.SHIFT_LEFT : ELevelObjectType.SHIFT_RIGHT;

    public override void OnEnter(Runner runner)
    {
        runner.transform.position = transform.position;
        runner.Shift(Direction);
    }
}
