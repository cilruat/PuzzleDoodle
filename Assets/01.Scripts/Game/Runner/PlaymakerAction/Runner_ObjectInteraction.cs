using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("02_Runner")]
    public class Runner_ObjectInteraction : RunnerActionBase
    {
        public override Runner.EState FsmState => Runner.EState.OBJECT_INTERACTION;

        public override void OnEnter()
        {
            base.OnEnter();

            _runner.ChangeAnimation(ERunnerAnim.JUMP);
        }
    }
}
