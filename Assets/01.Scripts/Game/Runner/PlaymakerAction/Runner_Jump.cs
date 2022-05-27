using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("02_Runner")]
    public class Runner_Jump : RunnerActionBase
    {
        public override Runner.EState FsmState => Runner.EState.JUMP;

        public override void OnEnter()
        {
            base.OnEnter();

            _runner.ChangeAnimation(ERunnerAnim.JUMP);
        }
        public override void OnUpdate()
        {
            if (_runner.IsGround)
                Fsm.Event("RUN");
        }
    }
}
