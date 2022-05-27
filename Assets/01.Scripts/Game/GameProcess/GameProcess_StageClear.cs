using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("01_GameProcess")]

    public class GameProcess_StageClear : FsmStateAction
    {
        public override void OnEnter()
        {
            GameManager.Instance.StageClear();
        }

        public override void OnExit()
        {
        }
    }
}
