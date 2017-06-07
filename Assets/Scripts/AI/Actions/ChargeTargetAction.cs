using Assets.Scripts.AI.GOAP;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.AI.Actions {
    public class ChargeTargetAction : GoapAction {

        public override GameObject GetTarget() {
            return GameManager.Instance.Character.gameObject;
        }

        public override void Init() {
            RegisterEffect(GoapCondition.InAttackRange, true);
            RegisterPrecondition(GoapCondition.IsTired, false);
        }

        public override void Execute() {
            Debug.Log("Finding target");
        }

        public override bool Completed() {
            return true;
        }
    }
}