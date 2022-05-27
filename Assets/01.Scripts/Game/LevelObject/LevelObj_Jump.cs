using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObj_Jump : LevelObjectBase
{
    public override ELevelObjectType Type => ELevelObjectType.JUMP;
        
    public override void OnEnter(Runner runner)
    {
        transform.DOShakeScale(1f, 0.5f);

        runner.transform.position = transform.position + new Vector3(0f, .25f);
        runner.SpringJump();
    }
}
