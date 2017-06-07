using Assets.Scripts.AI.GOAP;
using Assets.Scripts.World;
using UnityEngine;
using Animation = Assets.Scripts.AI.Entity.Animation;

namespace Assets.Scripts.AI.Actions {
    public class DoAttackAction : GoapAction{
        public override void Execute() {
            Debug.Log("Attacking!");

            Agent.Entity.PlayAnimation(Animation.Attack1);
            //attack
        }

        public override bool Completed() {
            // check if player is still in range
            // if animation is finished and player is still in range redo this action.
            
            Agent.Entity.Rotate(GameManager.Instance.Character.transform.position, 5.0f);
            return (Vector3.Distance(Agent.transform.position, GameManager.Instance.Character.transform.position) >=
                4.0f) ;
        }

        public override GameObject GetTarget() {
            return null;
        }

        public override void Init() {
            RegisterPrecondition(GoapCondition.InAttackRange, true);
            RegisterPrecondition(GoapCondition.IsTired, false);
            RegisterPrecondition(GoapCondition.IsDamaged, false);
        }
    }
}