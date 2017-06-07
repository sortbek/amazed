using System;
using Assets.Scripts.AI.Entity.Behaviours;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP.States {
    // Created by:
    // Hugo Kamps
    // S1084074
    public class GoapMovingState : AbstractState {

        private readonly EntityPathFollowingBehaviour _pathFollowing;
        private readonly EntitySeekBehaviour _seek;
        private Character.Character _character;

        public GoapMovingState(GoapAgent agent) : base(agent) {
            _pathFollowing = new EntityPathFollowingBehaviour(agent.Entity);
            _seek = new EntitySeekBehaviour(agent.Entity);
        }

        private void CharacterNodeChanged(object sender, EventArgs e) {
            if (Agent.Entity.GetCurrentBehaviour() == _seek) return;
            var target = GetTargetPosition();
            if(target == _character.gameObject)
                _pathFollowing.UpdateRequest(target.transform.position);
        }

        public override void Enter() {
            DetermineBehaviour(true);
            if (_character == null)
                (_character = GameManager.Instance.Character).NodeChanged += CharacterNodeChanged;
            Debug.Log("AI Moving state");
        }

        private void DetermineBehaviour(bool onInit) {
            var request = GetTargetPosition();
            if (Agent.Entity.Perspective.Visible(request)) {
                Agent.Entity.SetBehaviour(_seek);
                _seek.UpdateTarget(_character.transform.position);
            } else {
                Agent.Entity.SetBehaviour(_pathFollowing);
                if(onInit) _pathFollowing.UpdateRequest(request.transform.position);
            }
        }

        private GameObject GetTargetPosition() {
            var request = Agent.ActionQueue.Count > 0 ? Agent.ActionQueue.Peek().GetTarget() : null;
            return request ?? Agent.gameObject;
        }

        public override void Execute() {
            DetermineBehaviour(false);
            if (!_seek.Found()) return;
            Agent.StateMachine.ChangeState(GoapStateMachine.StateType.Action);
        }

    }
}