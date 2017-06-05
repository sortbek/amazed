using Assets.Scripts.AI.GOAP;
using UnityEngine;
using Animation = Assets.Scripts.AI.Entity.Animation;

namespace Assets.Scripts.AI.Actions {
    public class DoAttackAction : GoapAction {
        public override void Execute() {
            Debug.Log("Attacking!");
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