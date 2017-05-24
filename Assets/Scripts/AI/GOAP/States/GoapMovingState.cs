using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP.States {
    public class GoapMovingState : AbstractState {

        public GoapMovingState(GoapAgent agent) : base(agent) {}

        public override void Enter() {
            Debug.Log("AI Moving state");
        }

        public override void Execute() {
            // Moving to target code here.
            // target uit CurrentPlan halen (eerste waarde)
            Agent.StateMachine.ChangeState(GoapStateMachine.StateType.Action);
        }

    }
}
