using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("01_GameProcess")]

    public class GameProcess_Run : FsmStateAction
    {
        public override void OnEnter()
        {
            GameManager.Instance.StartRun();
        }

        public override void OnExit()
        {
        }
    }
}
