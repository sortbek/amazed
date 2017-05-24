using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP.States {
    public class GoapIdleState : AbstractState {

        public GoapIdleState(GoapAgent agent) : base(agent) { }

        public override void Enter() {
            Debug.Log("AI Idling state");
        }

        public override void Execute() {
            var plan = Agent.ActionQueue;
            if (plan.Count > 0)
                Agent.StateMachine.ChangeState(GoapStateMachine.StateType.Moving);
        }

    }
}
