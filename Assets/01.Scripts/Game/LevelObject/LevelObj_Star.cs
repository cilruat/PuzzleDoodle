using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObj_Star: LevelObjectBase
{
    public override ELevelObjectType Type => ELevelObjectType.STAR;
    
    public override void OnEnter(Runner runner)
    {
        Destroy(gameObject);

        GameManager.Instance.GetStar();
    }
}
