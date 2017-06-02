using Assets.Scripts.AI.Entity.Behaviours;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP.States {
    // Created by:
    // Hugo Kamps
    // S1084074
    public class GoapMovingState : AbstractState {
        private PathFollowingBehaviour _pathFollowingBehaviour;

        public GoapMovingState(GoapAgent agent) : base(agent) {
            _pathFollowingBehaviour = new PathFollowingBehaviour(agent.Entity);
        }

        public override void Enter() {
            Debug.Log("AI Moving state");
            Agent.Entity.SetBehaviour(_pathFollowingBehaviour);
            _pathFollowingBehaviour.UpdateTarget();
        }

        public override void Execute() {
            _pathFollowingBehaviour.UpdateTarget();
            if (_pathFollowingBehaviour.Path == null || _pathFollowingBehaviour.Path.Length > 0 &&
                !_pathFollowingBehaviour.Reached()) return;

            Agent.StateMachine.ChangeState(GoapStateMachine.StateType.Action);
        }
    }
}