using Assets.Scripts.AI.GOAP;
using Assets.Scripts.World;
using UnityEngine;
using Animation = Assets.Scripts.AI.Entity.Animation;

namespace Assets.Scripts.AI.Actions {
    public class DoAttackAction : GoapAction{
        public override void Execute() {
            Agent.Entity.PlayAnimation(Animation.Attack1);
            Rotate();
        }

        private void Rotate() {
            var dir = GameManager.Instance.Character.transform.position;
            dir.y = Agent.Entity.transform.position.y;
            Agent.Entity.Rotate(dir);
        }

        public override bool Completed() {
            // check if player is still in range
            // if animation is finished and player is still in range redo this action.
            Rotate();
            return Vector3.Distance(Agent.transform.position, GameManager.Instance.Character.transform.position) >= 4.5f;
        }

        public override GameObject GetTarget() {
            return null;
        }

        public override void Init() {
            RegisterEffect(GoapCondition.NearTarget, false);
            RegisterEffect(GoapCondition.InAttackRange, true);
            RegisterPrecondition(GoapCondition.NearTarget, true);
            RegisterPrecondition(GoapCondition.IsDamaged, false);
        }
    }
}