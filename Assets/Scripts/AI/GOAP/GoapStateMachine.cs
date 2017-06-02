using System.Collections.Generic;
using Assets.Scripts.AI.GOAP.States;

namespace Assets.Scripts.AI.GOAP {
    // Created by:
    // Eelco Eikelboom
    // S1080542
    public class GoapStateMachine {
        public enum StateType {
            Idle,
            Moving,
            Action
        }

        private readonly Dictionary<StateType, AbstractState> _stateRegister;
        private AbstractState _currentState;

        public GoapStateMachine(GoapAgent agent) {
            _stateRegister = new Dictionary<StateType, AbstractState>();
            _stateRegister[StateType.Action] = new GoapActionState(agent);
            _stateRegister[StateType.Idle] = new GoapIdleState(agent);
            _stateRegister[StateType.Moving] = new GoapMovingState(agent);
        }

        public void ChangeState(StateType type) {
            var state = _stateRegister[type];
            _currentState = state;
            _currentState.Enter();
        }

        public void Update() {
            if (_currentState != null)
                _currentState.Execute();
        }
    }
}