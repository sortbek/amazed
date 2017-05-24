using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.GOAP.States;

namespace Assets.Scripts.AI.GOAP {
    public class GoapStateMachine {
        
        public enum StateType { Idle, Moving, Action }
        private AbstractState _currentState;
        private readonly Dictionary<StateType, AbstractState> _stateRegister;

        public GoapStateMachine(GoapAgent agent) {
            _stateRegister = new Dictionary<StateType, AbstractState>();
            _stateRegister[StateType.Action] = new GoapActionState(agent);
            _stateRegister[StateType.Idle] = new GoapIdleState(agent);
            _stateRegister[StateType.Moving] = new GoapMovingState(agent);          
        }

        public void ChangeState(StateType type) {
            AbstractState state = _stateRegister[type];
            if(_currentState != null)
                _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void Update() {
            _currentState.Execute();
        }

    }
}
