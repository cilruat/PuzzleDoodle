using UnityEngine;
using System.Collections;

public partial class Runner
{
    public void StartRun()
    {
        _fsm.SendEvent("RUN");
    }

    public void StartInteraction(LevelObjectBase obj)
    {   
        SetKinematic(true);

        _fsm.SendEvent("OBJECT_INTERACTION");
        _curInteractObj = obj;
    }

    public void StageClear()
    {
        _fsm.SendEvent("STAGE_CLEAR");
    }
}
