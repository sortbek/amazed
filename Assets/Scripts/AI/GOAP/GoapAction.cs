using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP {

    // Created by:
    // Eelco Eikelboom
    // S1080542
    public abstract class GoapAction : MonoBehaviour {

        public Dictionary<GoapCondition, bool> Preconditions { get; private set; }
        public Dictionary<GoapCondition, bool> Effects { get; private set; }

        protected GoapAgent Agent { get; private set; }
        protected GoapAction() {
            Preconditions = new Dictionary<GoapCondition, bool>();
            Effects = new Dictionary<GoapCondition, bool>();
        }

        protected void RegisterPrecondition(GoapCondition condition, bool val) {
            Preconditions[condition] = val;
        }

        protected void RegisterEffect(GoapCondition cond, bool result) {
            Effects[cond] = result;
        }

        void Awake() {
            Agent = GetComponent<GoapAgent>();
            Init();
        }

        public abstract Vector3? GetTarget();

        public abstract void Init();
        public abstract void Execute();
        public abstract bool Completed();
    }
}
