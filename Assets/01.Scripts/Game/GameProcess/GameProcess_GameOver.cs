using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("01_GameProcess")]

    public class GameProcess_GameOver : FsmStateAction
    {
        public override void OnEnter()
        {
            GameManager.Instance.RestartStage();
        }

        public override void OnExit()
        {
        }
    }
}
