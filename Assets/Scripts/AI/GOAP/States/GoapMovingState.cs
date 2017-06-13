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

        private readonly Character.Character _character;

        public GoapMovingState(GoapAgent agent) : base(agent) {
            _pathFollowing = new EntityPathFollowingBehaviour(agent.Entity);
            _seek = new EntitySeekBehaviour(agent.Entity);
            _character = GameManager.Instance.Character;
            _character.NodeChanged += CharacterNodeChanged;
        }

        private void CharacterNodeChanged(object sender, EventArgs e) {
            if (Agent.Entity.GetCurrentBehaviour() != _pathFollowing) return;
            var target = GetTargetPosition();
            if (target == _character.gameObject)
                _pathFollowing.UpdateRequest(target.transform.position);
        }

        public override void Enter() {
            DetermineBehaviour(true);
            Debug.Log("AI Moving state");
        }

        private void DetermineBehaviour(bool onInit) {
            var request = GetTargetPosition();
            if (Agent.Entity.Perspective.Visible(request)) {
                Agent.Entity.SetBehaviour(_seek);
                _seek.UpdateTarget(_character.transform.position);
            }
            else {
                Agent.Entity.SetBehaviour(_pathFollowing);
                if (onInit) _pathFollowing.UpdateRequest(request.transform.position);
            }
        }

        private GameObject GetTargetPosition() {
            try {
                var request = Agent.ActionQueue.Count > 0 ? Agent.ActionQueue.Peek().GetTarget() : null;
                return request == null ? Agent.gameObject : request;
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return null;
            }
        }

        public override void Execute() {
            if (_character != null)
                DetermineBehaviour(false);
            if (!_seek.Found()) return;
            Agent.StateMachine.ChangeState(GoapStateMachine.StateType.Action);
        }
    }
}