using Assets.Scripts.AI.GOAP;
using UnityEngine;
using Animation = Assets.Scripts.AI.Entity.Animation;

namespace Assets.Scripts.AI.Actions {
    public class DoAttackAction : GoapAction{
        private Character.Character _player;
        
        public override void Execute() {
            Debug.Log("Attacking!");
            _player = GetComponent<Character.Character>();
            Agent.Entity.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(Agent.Entity.transform.forward,
                _player.transform.position - Agent.Entity.transform.position,
                Time.deltaTime * 10.0f, 0.0f));
            Agent.Entity.PlayAnimation(Animation.attack1);
            //attack
        }

        public override bool Completed() {
            //check if player is damaged
            return true;
        }

        public override Vector3? GetTarget() {
            return null;
        }

        public override void Init() {
            RegisterPrecondition(GoapCondition.InAttackRange, true);
            RegisterPrecondition(GoapCondition.IsTired, false);
            RegisterPrecondition(GoapCondition.IsDamaged, false);
        }
    }
}