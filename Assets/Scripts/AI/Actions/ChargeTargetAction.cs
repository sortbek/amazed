using Assets.Scripts.AI.GOAP;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.AI.Actions {
    public class ChargeTargetAction : GoapAction {
        public override Vector3? GetTarget() {
            return GameManager.Instance.Character.transform.position;
        }

        public override void Init() {
            RegisterEffect(GoapCondition.InAttackRange, true);
            RegisterPrecondition(GoapCondition.IsTired, false);
        }

        public override void Execute() {
            Debug.Log("Finding target");
        }

        public override bool Completed() {
            return Vector3.Distance(Agent.transform.position, GameManager.Instance.Character.transform.position) < 0.3f;
        }
    }
}