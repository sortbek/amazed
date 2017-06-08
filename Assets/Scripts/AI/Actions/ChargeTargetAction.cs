using Assets.Scripts.AI.GOAP;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.AI.Actions {
    public class ChargeTargetAction : GoapAction {

        public override GameObject GetTarget() {
            return GameManager.Instance.Character.gameObject;
        }

        public override void Init() {
            RegisterEffect(GoapCondition.NearTarget, true);
            RegisterPrecondition(GoapCondition.NearTarget, false);
            RegisterPrecondition(GoapCondition.IsDamaged, false);
        }

        public override void Execute() {
            Debug.Log("Finding target");
        }

        public override bool Completed() {
            return true;
        }
    }
}