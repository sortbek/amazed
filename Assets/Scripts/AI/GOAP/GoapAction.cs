using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP {
    public abstract class GoapAction : MonoBehaviour{

        public Dictionary<GoapConditionKey, bool> Preconditions { get; private set; }
        public Dictionary<GoapConditionKey, bool> Effects { get; private set; }
        public int Cost { get; set; }

        protected GoapAction() {
            Preconditions = new Dictionary<GoapConditionKey, bool>();
            Effects = new Dictionary<GoapConditionKey, bool>();
        }

        protected GoapAction(int cost) : this() {
            Cost = cost;
        }

        public abstract void Perform();
        public abstract bool Complete();
        public abstract bool CanPerform();

        protected void RegisterPrecondition(GoapConditionKey key, bool condition) {
            Preconditions[key] = condition;
        }

        protected void RegisterEffect(GoapConditionKey key, bool condition) {
            Effects[key] = condition;
        }
    }
}
