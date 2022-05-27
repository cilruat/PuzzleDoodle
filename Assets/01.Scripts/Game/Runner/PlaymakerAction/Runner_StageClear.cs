using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("02_Runner")]
    public class Runner_StageClear : RunnerActionBase
    {
        public override Runner.EState FsmState => Runner.EState.STAGE_CLEAR;

        public override void OnEnter()
        {
            base.OnEnter();

            //_runner.ChangeAnimation(ERunnerAnim.St);
        }
    }
}
