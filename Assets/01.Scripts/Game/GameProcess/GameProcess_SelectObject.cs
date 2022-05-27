using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("01_GameProcess")]

    public class GameProcess_SelectObject : FsmStateAction
    {
        public override void OnEnter()
        {
            GameUIManager.Instance.OpenLevelObjectList();
        }

        public override void OnExit()
        {
        }
    }
}
