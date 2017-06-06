using Assets.Scripts.AI.Entity.Behaviours;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP.States {
    // Created by:
    // Hugo Kamps
    // S1084074
    public class GoapMovingState : AbstractState {
        private readonly EntityPathFollowingBehaviour _entityPathFollowingBehaviour;

        public GoapMovingState(GoapAgent agent) : base(agent) {
            _entityPathFollowingBehaviour = new EntityPathFollowingBehaviour(agent.Entity);
        }

        public override void Enter() {
            Debug.Log("AI Moving state");
            Agent.Entity.SetBehaviour(_entityPathFollowingBehaviour);
            _entityPathFollowingBehaviour.UpdateRequest(GetTargetPosition());
        }

        private Vector3 GetTargetPosition() {
            var request = Agent.ActionQueue.Peek().GetTarget();
            return request ?? Agent.transform.position;
        }

        public override void Execute() {
            if (_entityPathFollowingBehaviour.Path == null || _entityPathFollowingBehaviour.Path.Length > 0 &&
                !_entityPathFollowingBehaviour.Reached()) return;
            Agent.StateMachine.ChangeState(GoapStateMachine.StateType.Action);
        }
    }
}