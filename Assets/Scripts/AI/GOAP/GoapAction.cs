using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP {
    // Created by:
    // Eelco Eikelboom
    // S1080542
    public abstract class GoapAction : MonoBehaviour {
        protected GoapAction() {
            Preconditions = new Dictionary<GoapCondition, bool>();
            Effects = new Dictionary<GoapCondition, bool>();
        }

        public Dictionary<GoapCondition, bool> Preconditions { get; private set; }
        public Dictionary<GoapCondition, bool> Effects { get; private set; }

        public GoapAgent Agent { get; set; }

        protected void RegisterPrecondition(GoapCondition condition, bool val) {
            Preconditions[condition] = val;
        }

        protected void RegisterEffect(GoapCondition cond, bool result) {
            Effects[cond] = result;
        }

        private void Awake() {
            Agent = GetComponent<GoapAgent>();
            Init();
        }

        public abstract GameObject GetTarget();

        public abstract void Init();
        public abstract void Execute();
        public abstract bool Completed();
    }
}