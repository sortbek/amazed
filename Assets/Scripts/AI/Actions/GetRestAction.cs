using Assets.Scripts.AI.GOAP;
using UnityEngine;

namespace Assets.Scripts.AI.Actions {
    public class GetRestAction : GoapAction{
        public override Vector3? GetTarget() {
            return null;
        }

        public override void Init() {
            RegisterPrecondition(GoapCondition.IsTired, true);
            RegisterEffect(GoapCondition.IsTired, false);
        }

        public override void Execute() {
            Debug.Log("Getting rest");
        }

        public override bool Completed() {
            return true;
        }
    }
}
