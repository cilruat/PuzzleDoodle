using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("02_Runner")]
    public class Runner_Run : RunnerActionBase
    {
        public override Runner.EState FsmState => Runner.EState.RUN;

        public override void OnEnter()
        {
            base.OnEnter();

            _runner.ChangeAnimation(ERunnerAnim.RUN);
        }

        public override void OnUpdate()
        {
        }
    }
}
