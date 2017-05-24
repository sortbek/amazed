using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP.States {
    public class GoapActionState : AbstractState {

        private GoapAction _current;
        private bool _running;
        public GoapActionState(GoapAgent agent) : base(agent) {
            _current = null;
            _running = false;
        }

        public override void Enter() {
            Debug.Log("Action state");
        }

        public override void Execute() {
            if (!_running) {
                _running = true;
                _current = Agent.ActionQueue.Dequeue();
                _current.Execute();
            }else {
                if (!_current.Completed()) return;
                Agent.StateMachine.ChangeState(Agent.ActionQueue.Count > 0
                    ? GoapStateMachine.StateType.Moving
                    : GoapStateMachine.StateType.Idle);
                _running = false;
            }
        }
    }
}
