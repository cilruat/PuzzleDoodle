using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("02_Runner")]
    public class Runner_Draw : RunnerActionBase
    {
        public override Runner.EState FsmState => Runner.EState.DRAW;

        public override void OnEnter()
        {
            base.OnEnter();

            _runner.ChangeAnimation(ERunnerAnim.DRAW);
        }
    }
}
