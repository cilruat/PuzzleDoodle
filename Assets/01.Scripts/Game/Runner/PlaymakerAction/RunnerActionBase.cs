using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("02_Runner")]

    public abstract class RunnerActionBase : FsmStateAction
    {
        public abstract Runner.EState FsmState { get; }

        protected Runner _runner;

        public override void OnEnter()
        {
            if (_runner == null)
                _runner = Owner.GetComponent<Runner>();

            _runner.FsmState = FsmState;
        }
    }
}
